using System.Security.Cryptography;
using System.Text;

namespace ThirasaraTest
{
    public class Hashing
    {
        public Hashing() { }

        public byte[] CalculateSHA1Hash(string password)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                return sha1.ComputeHash(inputBytes);
            }
        }

        public bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }
            return true;
        }
    }
}
