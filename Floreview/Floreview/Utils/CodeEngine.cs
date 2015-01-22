using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Floreview.Utils
{
    public class CodeEngine
    {
        #region Fields & Props
        private const int LENGTH = 4;
        #endregion

        #region Functions
        public String GenerateRandomCode()
        {
            StringBuilder builder = new StringBuilder(LENGTH);

            for (int i = 0; i < LENGTH; i++)
            {
                Random r = new Random(GetCryptographicallyRandomInt());

                int next = r.Next(GetAllowableCharacters().Length);
                char c = GetAllowableCharacters()[next];

                builder.Append(c);
            }

            return builder.ToString();
        }

        private Int32 GetCryptographicallyRandomInt()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetBytes(randomBytes);

            Int32 randomInteger = BitConverter.ToInt32(randomBytes, 0);

            return randomInteger;
        }

        private char[] GetAllowableCharacters()
        {
            String chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";

            return chars.ToCharArray();
        }
        #endregion
    }
}