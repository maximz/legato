using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Legato.Helpers
{
    /// <summary>
    /// Generates unique string IDs; essentially, these are like the IDs of bit.ly short links.
    /// </summary>
    public class UniqueStringIDGenerator
    {
        // from http://stackoverflow.com/questions/1275492/how-can-i-create-an-unique-random-sequence-of-characters-in-c/1275824#1275824

        private string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        // or whatever you want.  Include more characters 
        // for more combinations and shorter URLs

        public UniqueStringIDGenerator()
        {

        }
        public UniqueStringIDGenerator(string customAlphabet)
        {
            alphabet = customAlphabet.Trim();
        }

        /// <summary>
        /// To create a unique string ID, supply the integer ID of this record from your database.
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public string Encode(int databaseId)
        {
            string encodedValue = String.Empty;
			//int encodingBase = alphabet.Length; // if alphabet is a-z, encodingBase is 26 (base26 encoding).
            while (databaseId > 1) // while (databaseId > encodingBase)
            {
                int remainder;
                encodedValue += alphabet[Math.DivRem(databaseId, alphabet.Length,
                    out remainder) - 1].ToString();
                databaseId = remainder;
            }
            return encodedValue;
        }

        public int Decode(string code)
        {
            int returnValue = 0;

            for (int thisPosition = 0; thisPosition < code.Length; thisPosition++)
            {
                char thisCharacter = code[thisPosition];

                returnValue += alphabet.IndexOf(thisCharacter) * (int)Math.Pow(alphabet.Length, code.Length - thisPosition - 1);
            }
            return returnValue;
        }
    }
}