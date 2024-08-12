using System.Security.Cryptography;
using System.Text;

namespace LoanCentral.API.Utility.Helper;
public static class EncodeDecode
{
    public static string EncryptString(string text)
    {
        string keyString = "AspireSoftwareConsultancy";
        var key = Encoding.UTF8.GetBytes(keyString);

        using (var aesAlg = Aes.Create())
        {
            using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
            {
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }

                    var iv = aesAlg.IV;

                    var decryptedContent = msEncrypt.ToArray();

                    var result = new byte[iv.Length + decryptedContent.Length];

                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                    return Convert.ToBase64String(result);
                }
            }
        }
    }
    public static string DecryptString(string cipherText)
    {

        string keyString = "AspireSoftwareConsultancy";
        var fullCipher = Convert.FromBase64String(cipherText);

        var iv = new byte[16];
        var cipher = new byte[16];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
        var key = Encoding.UTF8.GetBytes(keyString);

        using (var aesAlg = Aes.Create())
        {
            using (var decryptor = aesAlg.CreateDecryptor(key, iv))
            {
                string result;
                using (var msDecrypt = new MemoryStream(cipher))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return result;
            }
        }
    }
    public static string Encrypt(string input)
    {
        string key = "AscDevs12345AscDevs12345";
        byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
        TripleDESCryptoServiceProvider tripleDES = new();
        byte[] getBystsResult = UTF8Encoding.UTF8.GetBytes(key);
        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDES.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
    public static string Decrypt(string input)
    {
        string key = "AscDevs12345AscDevs12345";
        byte[] inputArray = Convert.FromBase64String(input);
        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDES.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    public static string timeActivity(DateTime yourDate)
    {
        TimeSpan oSpan = DateTime.Now.Subtract(yourDate);
        double TotalMinutes = oSpan.TotalMinutes;
        string Suffix = " ago";

        if (TotalMinutes < 0.0)
        {
            TotalMinutes = Math.Abs(TotalMinutes);
            Suffix = " from now";
        }

        var aValue = new SortedList<double, Func<string>>();
        aValue.Add(0.75, () => "less than a minute");
        aValue.Add(1.5, () => "a minute");
        aValue.Add(45, () => string.Format("{0} minutes", Math.Round(TotalMinutes)));
        aValue.Add(90, () => "an hour");
        aValue.Add(1440, () => string.Format("{0} hours", Math.Round(Math.Abs(oSpan.TotalHours)))); // 60 * 24
        aValue.Add(2880, () => "a day"); // 60 * 48
        aValue.Add(43200, () => string.Format("{0} days", Math.Floor(Math.Abs(oSpan.TotalDays)))); // 60 * 24 * 30
        aValue.Add(86400, () => "a month"); // 60 * 24 * 60
        aValue.Add(525600, () => string.Format("{0} months", Math.Floor(Math.Abs(oSpan.TotalDays / 30)))); // 60 * 24 * 365 
        aValue.Add(1051200, () => "a year"); // 60 * 24 * 365 * 2
        aValue.Add(double.MaxValue, () => string.Format("{0} years", Math.Floor(Math.Abs(oSpan.TotalDays / 365))));

        return aValue.First(n => TotalMinutes < n.Key).Value.Invoke() + Suffix;
    }
}



