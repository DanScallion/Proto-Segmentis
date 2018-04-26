using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;

namespace Segmentis
{
    public static class IDgenerator
    {

        public static string HashString(string toHash)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(toHash));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static string CreateID()
        {
            System.Random rng = new System.Random();

            float randomNum1 = rng.Next(0000000, 1000000);
            float randomNum2 = rng.Next(0000000, 2000000);
            float numToHash = ((randomNum1 + randomNum2) * randomNum1) / randomNum2;

            return HashString(numToHash.ToString());
        }
    }
}
