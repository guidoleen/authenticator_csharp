using System;

namespace OAuthDb
{
    public class OAuthDbUtil
    {
        // Token creation
        public String CreateTokenOnTimeStamp()
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

        public DateTime EncodeTokenOnTimeStamp(String token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
          
            return when;
        }

        public DateTime CreateDateTimeNow()
        {
            return DateTime.Now;
        }
        // End token creation
    }
}