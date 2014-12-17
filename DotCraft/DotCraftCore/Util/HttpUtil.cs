using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace DotCraftCore.nUtil
{

	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class HttpUtil
	{
	/// <summary> The number of download threads that we have started so far.  </summary>
		private static readonly AtomicInteger downloadThreadsStarted = new AtomicInteger(0);
		private static readonly Logger logger = LogManager.Logger;
		

///    
///     <summary> * Builds an encoded HTTP POST content string from a string map </summary>
///     
		public static string buildPostString(IDictionary p_76179_0_)
		{
			StringBuilder var1 = new StringBuilder();
			IEnumerator var2 = p_76179_0_.GetEnumerator();

			while(var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;

				if(var1.Length > 0)
				{
					var1.Append('&');
				}

				try
				{
					var1.Append(URLEncoder.encode((string)var3.Key, "UTF-8"));
				}
				catch (UnsupportedEncodingException var6)
				{
					var6.printStackTrace();
				}

				if(var3.Value != null)
				{
					var1.Append('=');

					try
					{
						var1.Append(URLEncoder.encode(var3.Value.ToString(), "UTF-8"));
					}
					catch (UnsupportedEncodingException var5)
					{
						var5.printStackTrace();
					}
				}
			}

			return var1.ToString();
		}

		public static string func_151226_a(URL p_151226_0_, IDictionary p_151226_1_, bool p_151226_2_)
		{
			return func_151225_a(p_151226_0_, buildPostString(p_151226_1_), p_151226_2_);
		}

		private static string func_151225_a(URL p_151225_0_, string p_151225_1_, bool p_151225_2_)
		{
			try
			{
				Proxy var3 = MinecraftServer.Server == null ? null : MinecraftServer.Server.ServerProxy;

				if(var3 == null)
				{
					var3 = Proxy.NO_PROXY;
				}

				HttpURLConnection var4 = (HttpURLConnection)p_151225_0_.openConnection(var3);
				var4.RequestMethod = "POST";
				var4.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
				var4.setRequestProperty("Content-Length", "" + p_151225_1_.Bytes.length);
				var4.setRequestProperty("Content-Language", "en-US");
				var4.UseCaches = false;
				var4.DoInput = true;
				var4.DoOutput = true;
				DataOutputStream var5 = new DataOutputStream(var4.OutputStream);
				var5.writeBytes(p_151225_1_);
				var5.flush();
				var5.close();
				BufferedReader var6 = new BufferedReader(new InputStreamReader(var4.InputStream));
				StringBuilder var8 = new StringBuilder();
				string var7;

				while((var7 = var6.readLine()) != null)
				{
					var8.Append(var7);
					var8.Append('\r');
				}

				var6.close();
				return var8.ToString();
			}
			catch (Exception var9)
			{
				if(!p_151225_2_)
				{
					logger.error("Could not post to " + p_151225_0_, var9);
				}

				return "";
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public static void func_151223_a(final File p_151223_0_, final String p_151223_1_, final HttpUtil.DownloadListener p_151223_2_, final Map p_151223_3_, final int p_151223_4_, final IProgressUpdate p_151223_5_, final Proxy p_151223_6_)
		public static void func_151223_a(File p_151223_0_, string p_151223_1_, HttpUtil.DownloadListener p_151223_2_, IDictionary p_151223_3_, int p_151223_4_, IProgressUpdate p_151223_5_, Proxy p_151223_6_)
		{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: Thread var7 = new Thread(new Runnable() {  public void run() { URLConnection var1 = null; InputStream var2 = null; DataOutputStream var3 = null; if (p_151223_5_ != null) { p_151223_5_.resetProgressAndMessage("Downloading Texture Pack"); p_151223_5_.resetProgresAndWorkingMessage("Making Request..."); } try { byte[] var4 = new byte[4096]; URL var5 = new URL(p_151223_1_); var1 = var5.openConnection(p_151223_6_); float var6 = 0.0F; float var7 = (float)p_151223_3_.entrySet().size(); Iterator var8 = p_151223_3_.entrySet().iterator(); while (var8.hasNext()) { Entry var9 = (Entry)var8.next(); var1.setRequestProperty((String)var9.getKey(), (String)var9.getValue()); if (p_151223_5_ != null) { p_151223_5_.setLoadingProgress((int)(++var6 / var7 * 100.0F)); } } var2 = var1.getInputStream(); var7 = (float)var1.getContentLength(); int var28 = var1.getContentLength(); if (p_151223_5_ != null) { p_151223_5_.resetProgresAndWorkingMessage(String.format("Downloading file (%.2f MB)...", new Object[] {Float.valueOf(var7 / 1000.0F / 1000.0F)})); } if (p_151223_0_.exists()) { long var29 = p_151223_0_.length(); if (var29 == (long)var28) { p_151223_2_.func_148522_a(p_151223_0_); if (p_151223_5_ != null) { p_151223_5_.func_146586_a(); } return; } HttpUtil.logger.warn("Deleting " + p_151223_0_ + " as it does not match what we currently have (" + var28 + " vs our " + var29 + ")."); p_151223_0_.delete(); } else if (p_151223_0_.getParentFile() != null) { p_151223_0_.getParentFile().mkdirs(); } var3 = new DataOutputStream(new FileOutputStream(p_151223_0_)); if (p_151223_4_ > 0 && var7 > (float)p_151223_4_) { if (p_151223_5_ != null) { p_151223_5_.func_146586_a(); } throw new IOException("Filesize is bigger than maximum allowed (file is " + var6 + ", limit is " + p_151223_4_ + ")"); } boolean var30 = false; int var31; while ((var31 = var2.read(var4)) >= 0) { var6 += (float)var31; if (p_151223_5_ != null) { p_151223_5_.setLoadingProgress((int)(var6 / var7 * 100.0F)); } if (p_151223_4_ > 0 && var6 > (float)p_151223_4_) { if (p_151223_5_ != null) { p_151223_5_.func_146586_a(); } throw new IOException("Filesize was bigger than maximum allowed (got >= " + var6 + ", limit was " + p_151223_4_ + ")"); } var3.write(var4, 0, var31); } p_151223_2_.func_148522_a(p_151223_0_); if (p_151223_5_ != null) { p_151223_5_.func_146586_a(); } } catch (Throwable var26) { var26.printStackTrace(); } finally { try { if (var2 != null) { var2.close(); } } catch (IOException var25) { ; } try { if (var3 != null) { var3.close(); } } catch (IOException var24) { ; } } } }, "File Downloader #" + downloadThreadsStarted.incrementAndGet());
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'entrySet' method:
			Thread var7 = new Thread(new Runnable() {  public void run() { URLConnection var1 = null; InputStream var2 = null; DataOutputStream var3 = null; if(p_151223_5_ != null) { p_151223_5_.resetProgressAndMessage("Downloading Texture Pack"); p_151223_5_.resetProgresAndWorkingMessage("Making Request..."); } try { sbyte[] var4 = new sbyte[4096]; URL var5 = new URL(p_151223_1_); var1 = var5.openConnection(p_151223_6_); float var6 = 0.0F; float var7 = (float)p_151223_3_.entrySet().size(); IEnumerator var8 = p_151223_3_.GetEnumerator(); while(var8.hasNext()) { Entry var9 = (Entry)var8.next(); var1.setRequestProperty((string)var9.Key, (string)var9.Value); if(p_151223_5_ != null) { p_151223_5_.setLoadingProgress((int)(++var6 / var7 * 100.0F)); } } var2 = var1.InputStream; var7 = (float)var1.ContentLength; int var28 = var1.ContentLength; if(p_151223_5_ != null) { p_151223_5_.resetProgresAndWorkingMessage(string.Format("Downloading file ({0:F2} MB)...", new object[] {Convert.ToSingle(var7 / 1000.0F / 1000.0F)})); } if(p_151223_0_.exists()) { long var29 = p_151223_0_.Length; if(var29 == (long)var28) { p_151223_2_.func_148522_a(p_151223_0_); if(p_151223_5_ != null) { p_151223_5_.func_146586_a(); } return; } HttpUtil.logger.warn("Deleting " + p_151223_0_ + " as it does not match what we currently have (" + var28 + " vs our " + var29 + ")."); p_151223_0_.delete(); } else if(p_151223_0_.ParentFile != null) { p_151223_0_.ParentFile.mkdirs(); } var3 = new DataOutputStream(new FileOutputStream(p_151223_0_)); if(p_151223_4_ > 0 && var7 > (float)p_151223_4_) { if(p_151223_5_ != null) { p_151223_5_.func_146586_a(); } throw new IOException("Filesize is bigger than maximum allowed (file is " + var6 + ", limit is " + p_151223_4_ + ")"); } bool var30 = false; int var31; while((var31 = var2.read(var4)) >= 0) { var6 += (float)var31; if(p_151223_5_ != null) { p_151223_5_.setLoadingProgress((int)(var6 / var7 * 100.0F)); } if(p_151223_4_ > 0 && var6 > (float)p_151223_4_) { if(p_151223_5_ != null) { p_151223_5_.func_146586_a(); } throw new IOException("Filesize was bigger than maximum allowed (got >= " + var6 + ", limit was " + p_151223_4_ + ")"); } var3.write(var4, 0, var31); } p_151223_2_.func_148522_a(p_151223_0_); if(p_151223_5_ != null) { p_151223_5_.func_146586_a(); } } catch (Exception var26) { var26.printStackTrace(); } finally { try { if(var2 != null) { var2.close(); } } catch (IOException var25) { ; } try { if(var3 != null) { var3.close(); } } catch (IOException var24) { ; } } } }, "File Downloader #" + downloadThreadsStarted.incrementAndGet());
			var7.Daemon = true;
			var7.Start();
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static int func_76181_a() throws IOException
		public static int func_76181_a()
		{
			ServerSocket var0 = null;
			bool var1 = true;
			int var10;

			try
			{
				var0 = new ServerSocket(0);
				var10 = var0.LocalPort;
			}
			finally
			{
				try
				{
					if(var0 != null)
					{
						var0.close();
					}
				}
				catch (IOException var8)
				{
					;
				}
			}

			return var10;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static String func_152755_a(URL p_152755_0_) throws IOException
		public static string func_152755_a(URL p_152755_0_)
		{
			HttpURLConnection var1 = (HttpURLConnection)p_152755_0_.openConnection();
			var1.RequestMethod = "GET";
			BufferedReader var2 = new BufferedReader(new InputStreamReader(var1.InputStream));
			StringBuilder var4 = new StringBuilder();
			string var3;

			while((var3 = var2.readLine()) != null)
			{
				var4.Append(var3);
				var4.Append('\r');
			}

			var2.close();
			return var4.ToString();
		}

		public interface DownloadListener
		{
			void func_148522_a(File p_148522_1_);
		}
	}

}