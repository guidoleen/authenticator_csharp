using System;

namespace OAuthDb
{
    public class OAuthDbUtil
    {
        // Token credential creation
        public String CreateTokenCredentialOnTimeStamp()
        {
            byte[] time = BitConverter.GetBytes(this.CreateDateTimeNow().ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray(); // Unique key
            int iL = time.Length + key.Length;
            byte[] concat = new byte[iL];
            int i = 0;

            for (; i < time.Length; i++)
            {
                concat[i] = time[i];
            }
            for ( int j = 0; i < iL && j < key.Length; i++, j++)
            {
                concat[i] = key[j];
            }
            return Convert.ToBase64String(concat);
        }

        public DateTime EncodeTokenCredentialOnTimeStamp(String token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
          
            return when;
        }
        // End token creation

        public DateTime CreateDateTimeNow()
        {
            return DateTime.Now;
        }

        // Create datetime object from 1970 jan 01
        private static double GetTimeFromJan1970()
        {
            var timeFromJan1970 =
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) -
                new DateTime().ToUniversalTime();

            return timeFromJan1970.TotalSeconds;
        }
    }
}