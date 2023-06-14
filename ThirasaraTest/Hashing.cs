using System.Security.Cryptography;
using System.Text;

namespace ThirasaraTest
{
    public class Hashing
    {
        //Passwords will be hashed using SHA-1 algorithm

        public byte[] CalculateSHA1Hash(string password)
        {
            using (var sha1 = new SHA1Managed())
            {
                var inputBytes = Encoding.UTF8.GetBytes(password);
                return sha1.ComputeHash(inputBytes);
            }
        }

        public bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (var i = 0; i < array1.Length; i++)
                if (array1[i] != array2[i])
                    return false;
            return true;
        }
    }
}