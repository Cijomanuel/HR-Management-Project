using System.Security.Cryptography;
using System.Text;

namespace HrManagementSystem.Core
{
	public static class EncryptionAesCryptoServiceUtility
	{
		// Based on specification for encryption created by the (NIST) (National Institute of Standards and Technology - US) in 2001
		// AesCryptoServiceProvider uses a library which is FIPS (Federal Information Processing Standard) compliant

		private const int _iterations = 10000;
		private const int _saltBytes = 32;
		private const int _ivBytes = 16;

		public static string DecryptString(string text, string _password)
		{
			var textBytesWithSaltAndIv = Convert.FromBase64String(text);
			var saltStringBytes = textBytesWithSaltAndIv.Take(_saltBytes).ToArray();
			using (var rfc = new Rfc2898DeriveBytes(_password, saltStringBytes, _iterations))
			{
				var keyBytes = rfc.GetBytes(_saltBytes);
				using (var key = Aes.Create())
				{
					key.BlockSize = 128;
					key.Mode = CipherMode.CBC;
					key.Padding = PaddingMode.PKCS7;

					var ivStringBytes = textBytesWithSaltAndIv.Skip(_saltBytes).Take(_ivBytes).ToArray();
					var textBytes = textBytesWithSaltAndIv.Skip(_saltBytes + _ivBytes).Take(textBytesWithSaltAndIv.Length - (_saltBytes + _ivBytes)).ToArray();
					using (var decryptor = key.CreateDecryptor(keyBytes, ivStringBytes))
					using (var stream = new MemoryStream(textBytes))
					using (var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
					{
						var plainTextBytes = new byte[textBytes.Length];
						int read;
						int totalRead = 0;
						do
						{
							read = cryptoStream.Read(plainTextBytes, totalRead, plainTextBytes.Length - totalRead);
							totalRead += read;
						} while (read != 0);
						var actualTextBytes = Array.Empty<byte>();
						if (totalRead > 0)
						{
							Array.Resize(ref actualTextBytes, totalRead);
							Array.Copy(plainTextBytes, actualTextBytes, totalRead);
						}
						stream.Close();
						cryptoStream.Close();

						return Encoding.UTF8.GetString(actualTextBytes);
					}
				}
			}
		}

		public static string EncryptString(string text, string _password)
		{
			var randomsalt = GenerateBytes(32);
			using (var rfc = new Rfc2898DeriveBytes(_password, randomsalt, _iterations))
			{
				var keyBytes = rfc.GetBytes(_saltBytes);
				using (var key = Aes.Create())
				{
					var ivStringBytes = GenerateBytes(16);
					key.BlockSize = 128;
					key.Mode = CipherMode.CBC;
					key.Padding = PaddingMode.PKCS7;
					using (var encryptor = key.CreateEncryptor(keyBytes, ivStringBytes))
					{
						using (var stream = new MemoryStream())
						{
							using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
							{
								var textBytes = Encoding.UTF8.GetBytes(text);
								cryptoStream.Write(textBytes, 0, textBytes.Length);
								cryptoStream.FlushFinalBlock();
								var cipherTextBytes = randomsalt;
								cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
								cipherTextBytes = cipherTextBytes.Concat(stream.ToArray()).ToArray();
								stream.Close();
								cryptoStream.Close();

								return Convert.ToBase64String(cipherTextBytes);
							}
						}
					}
				}
			}
		}

		private static byte[] GenerateBytes(int size)
		{
			return RandomNumberGenerator.GetBytes(size);
		}
	}
}
