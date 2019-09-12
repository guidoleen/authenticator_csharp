using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace OAuthDb
{
    public class TokenJWT
    {
        private JwtSecurityTokenHandler tokenHandler;
        private String UserId = "";
        private String UniqueKey = "";

        public TokenJWT(String UserId, String UniqueKey)
        {
            this.tokenHandler = new JwtSecurityTokenHandler();
            this.UserId = UserId;
            this.UniqueKey = UniqueKey;
        }

        public String CreateTokenJWT(String SecretKey)
        {
            String jwtToken = "{}";
            String secretKey = SecretKey;
            Console.Write(secretKey + "\n");

            JwtSecurityToken secToken = new JwtSecurityToken(
                signingCredentials: this.signingCredentials(this.secKey(secretKey)),
                issuer: OAuthDbCONST.JWT_ISS,
                audience: OAuthDbCONST.JWT_AUD,
                expires: this.GetTimeForJwtToken(OAuthDbCONST.JWT_XTRA_DAY, OAuthDbCONST.JWT_XTRA_MINUTE),
                claims: this.createClaims()
            );
                                        
            try
            {
                jwtToken = this.tokenHandler.WriteToken(secToken);
            }
            catch(System.ArgumentOutOfRangeException ee)
            {
                jwtToken = "{" + ee.ToString() + "}";
            }
            return jwtToken;
        }

        // Create claims for token
        private Claim[] createClaims()
        {
            String hasRoles = new RoleDAO().displayActionsOrRole(Convert.ToInt32(this.UserId));
            String userIdEncryp = new EncryptionDecryption(this.UniqueKey).Encrypt(this.UserId);

            Claim[] claim =
            {
                new Claim("hasRoles", hasRoles),
                new Claim("userId", userIdEncryp),
                new Claim( JwtRegisteredClaimNames.Iat, this.GetTimeForJwtTokenFromJan1970().ToString(), ClaimValueTypes.Integer32 )
            };
            return claim;
        }

        private DateTime GetTimeForJwtToken(int xtraDay, int xtraMin)
        {
            DateTime dtt = DateTime.Now.AddDays(xtraDay);
            dtt = dtt.AddMinutes(xtraMin);

            return dtt; //  Convert.ToInt32(dtt.ToString("yyyyMMdd"));
        }

        private int GetTimeForJwtTokenFromJan1970()
        {
            TimeSpan span = (new OAuthDbUtil().CreateDateTimeNow() - new DateTime(1970, 1, 1, 0, 0, 0, 0));
            return Convert.ToInt32(Math.Round(span.TotalSeconds));
        }

        // Create Symmetric Security key
        private SymmetricSecurityKey secKey(String seckey)
        {
            return new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(
                            seckey)
                            );
        }

        // Create Signing Credentials from SecurityKey
        private SigningCredentials signingCredentials(SymmetricSecurityKey seckey)
        {
            return new SigningCredentials(seckey, SecurityAlgorithms.HmacSha256);
        }

        // Validate the token
        public bool IsValidJwtToken(string strtoken, string strKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = this.GetValidationParameters(strKey);

            SecurityToken validatedToken;
            IPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(strtoken, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception ee)
            {
                Console.Write(ee.ToString());
                return false;
            }
        }

        private TokenValidationParameters GetValidationParameters(string strKey)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = OAuthDbCONST.JWT_ISS,
                ValidAudience = OAuthDbCONST.JWT_AUD,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strKey)) // The same key as the one that generate the token
            };
        }
    }
}
//System.IdentityModel;  
//System.Security
// Nuget  install-package  "System.IdentityModel.Tokens.Jwt" 
//// https://stackoverflow.com/questions/29355384/when-is-jwtsecuritytokenhandler-validatetoken-actually-valid
//// https://stackoverflow.com/questions/29715178/complex-json-web-token-array-in-webapi-with-owin