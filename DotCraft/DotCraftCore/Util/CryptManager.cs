using System;

namespace DotCraftCore.Util
{

	using BadPaddingException = javax.crypto.BadPaddingException;
	using Cipher = javax.crypto.Cipher;
	using IllegalBlockSizeException = javax.crypto.IllegalBlockSizeException;
	using KeyGenerator = javax.crypto.KeyGenerator;
	using NoSuchPaddingException = javax.crypto.NoSuchPaddingException;
	using SecretKey = javax.crypto.SecretKey;
	using IvParameterSpec = javax.crypto.spec.IvParameterSpec;
	using SecretKeySpec = javax.crypto.spec.SecretKeySpec;

	public class CryptManager
	{
		private const string __OBFID = "CL_00001483";

///    
///     <summary> * Generate a new shared secret AES key from a secure random source </summary>
///     
		public static SecretKey createNewSharedKey()
		{
			try
			{
				KeyGenerator var0 = KeyGenerator.getInstance("AES");
				var0.init(128);
				return var0.generateKey();
			}
			catch (NoSuchAlgorithmException var1)
			{
				throw new Error(var1);
			}
		}

		public static KeyPair createNewKeyPair()
		{
			try
			{
				KeyPairGenerator var0 = KeyPairGenerator.getInstance("RSA");
				var0.initialize(1024);
				return var0.generateKeyPair();
			}
			catch (NoSuchAlgorithmException var1)
			{
				var1.printStackTrace();
				System.err.println("Key pair generation failed!");
				return null;
			}
		}

///    
///     <summary> * Compute a serverId hash for use by sendSessionRequest() </summary>
///     
		public static sbyte[] getServerIdHash(string p_75895_0_, PublicKey p_75895_1_, SecretKey p_75895_2_)
		{
			try
			{
				return digestOperation("SHA-1", new sbyte[][] {p_75895_0_.getBytes("ISO_8859_1"), p_75895_2_.Encoded, p_75895_1_.Encoded});
			}
			catch (UnsupportedEncodingException var4)
			{
				var4.printStackTrace();
				return null;
			}
		}

///    
///     <summary> * Compute a message digest on arbitrary byte[] data </summary>
///     
		private static sbyte[] digestOperation(string p_75893_0_, params sbyte[][] p_75893_1_)
		{
			try
			{
				MessageDigest var2 = MessageDigest.getInstance(p_75893_0_);
				sbyte[][] var3 = p_75893_1_;
				int var4 = p_75893_1_.length;

				for(int var5 = 0; var5 < var4; ++var5)
				{
					sbyte[] var6 = var3[var5];
					var2.update(var6);
				}

				return var2.digest();
			}
			catch (NoSuchAlgorithmException var7)
			{
				var7.printStackTrace();
				return null;
			}
		}

///    
///     <summary> * Create a new PublicKey from encoded X.509 data </summary>
///     
		public static PublicKey decodePublicKey(sbyte[] p_75896_0_)
		{
			try
			{
				X509EncodedKeySpec var1 = new X509EncodedKeySpec(p_75896_0_);
				KeyFactory var2 = KeyFactory.getInstance("RSA");
				return var2.generatePublic(var1);
			}
			catch (NoSuchAlgorithmException var3)
			{
				;
			}
			catch (InvalidKeySpecException var4)
			{
				;
			}

			System.err.println("Public key reconstitute failed!");
			return null;
		}

///    
///     <summary> * Decrypt shared secret AES key using RSA private key </summary>
///     
		public static SecretKey decryptSharedKey(PrivateKey p_75887_0_, sbyte[] p_75887_1_)
		{
			return new SecretKeySpec(decryptData(p_75887_0_, p_75887_1_), "AES");
		}

///    
///     <summary> * Encrypt byte[] data with RSA public key </summary>
///     
		public static sbyte[] encryptData(Key p_75894_0_, sbyte[] p_75894_1_)
		{
			return cipherOperation(1, p_75894_0_, p_75894_1_);
		}

///    
///     <summary> * Decrypt byte[] data with RSA private key </summary>
///     
		public static sbyte[] decryptData(Key p_75889_0_, sbyte[] p_75889_1_)
		{
			return cipherOperation(2, p_75889_0_, p_75889_1_);
		}

///    
///     <summary> * Encrypt or decrypt byte[] data using the specified key </summary>
///     
		private static sbyte[] cipherOperation(int p_75885_0_, Key p_75885_1_, sbyte[] p_75885_2_)
		{
			try
			{
				return createTheCipherInstance(p_75885_0_, p_75885_1_.Algorithm, p_75885_1_).doFinal(p_75885_2_);
			}
			catch (IllegalBlockSizeException var4)
			{
				var4.printStackTrace();
			}
			catch (BadPaddingException var5)
			{
				var5.printStackTrace();
			}

			System.err.println("Cipher data failed!");
			return null;
		}

///    
///     <summary> * Creates the Cipher Instance. </summary>
///     
		private static Cipher createTheCipherInstance(int p_75886_0_, string p_75886_1_, Key p_75886_2_)
		{
			try
			{
				Cipher var3 = Cipher.getInstance(p_75886_1_);
				var3.init(p_75886_0_, p_75886_2_);
				return var3;
			}
			catch (InvalidKeyException var4)
			{
				var4.printStackTrace();
			}
			catch (NoSuchAlgorithmException var5)
			{
				var5.printStackTrace();
			}
			catch (NoSuchPaddingException var6)
			{
				var6.printStackTrace();
			}

			System.err.println("Cipher creation failed!");
			return null;
		}

		public static Cipher func_151229_a(int p_151229_0_, Key p_151229_1_)
		{
			try
			{
				Cipher var2 = Cipher.getInstance("AES/CFB8/NoPadding");
				var2.init(p_151229_0_, p_151229_1_, new IvParameterSpec(p_151229_1_.Encoded));
				return var2;
			}
			catch (GeneralSecurityException var3)
			{
				throw new Exception(var3);
			}
		}
	}

}