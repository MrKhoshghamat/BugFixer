using System.Security.Cryptography;
using System.Text;

namespace BugFixer.Application.Security;

public class PasswordHelper
{
    public static string EncodePasswordMd5(string pass)
    {
        Byte[] originalBytes;
        Byte[] encodeBytes;
        MD5 md5;

        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(pass);
        encodeBytes = md5.ComputeHash(originalBytes);

        return BitConverter.ToString(encodeBytes);
    }
}