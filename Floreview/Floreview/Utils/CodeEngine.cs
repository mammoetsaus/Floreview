using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Floreview.Utils
{
    public abstract class CodeEngine
    {
        public static String GenerateRandomCode(int totalChars)
        {
            StringBuilder builder = new StringBuilder(totalChars);

            for (int i = 0; i < totalChars; i++)
            {
                Random r = new Random(GetCryptographicallyRandomInt());

                int next = r.Next(GetAllowableCharacters().Length);
                char c = GetAllowableCharacters()[next];

                builder.Append(c);
            }

            return builder.ToString();
        }

        private static Int32 GetCryptographicallyRandomInt()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetBytes(randomBytes);

            Int32 randomInteger = BitConverter.ToInt32(randomBytes, 0);

            return randomInteger;
        }

        private static char[] GetAllowableCharacters()
        {
            String chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";

            return chars.ToCharArray();
        }
    }
}