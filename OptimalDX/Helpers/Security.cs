using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OptimalDX.Helpers
{
	public class Security
	{
		private readonly string _encryptionKey = System.Configuration.ConfigurationManager.AppSettings["EncryptionKey"];
		private readonly string _encryptionIV = System.Configuration.ConfigurationManager.AppSettings["EncryptionIV"];

		public string EncryptString(string plainText)
		{
			byte[] keyBytes = Encoding.ASCII.GetBytes(_encryptionKey);
			byte[] iv = Encoding.UTF8.GetBytes(_encryptionIV);

			byte[] encryptedPlainText = Encoding.UTF8.GetBytes(plainText);

			using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
			{
				aes.Key = keyBytes;
				aes.IV = iv;

				// Criptografar os dados
				byte[] encryptedContent;
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						csEncrypt.Write(encryptedPlainText, 0, encryptedPlainText.Length);
						csEncrypt.FlushFinalBlock();
						encryptedContent = msEncrypt.ToArray();
					}
				}

				return Convert.ToBase64String(encryptedContent);
			}

		}

		public string DecryptString(string encryptedContent)
		{
			string plainText = "";
			byte[] keyBytes = Encoding.ASCII.GetBytes(_encryptionKey);
			byte[] iv = Encoding.UTF8.GetBytes(_encryptionIV);

			using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
			{
				aes.Key = keyBytes;
				aes.IV = iv;

				byte[] byteArray = Convert.FromBase64String(encryptedContent);

				using (MemoryStream msDecrypt = new MemoryStream(byteArray))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							plainText = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return plainText;
		}


	}
}