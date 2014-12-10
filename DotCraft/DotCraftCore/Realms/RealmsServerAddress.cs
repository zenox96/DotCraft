using System;
using System.Collections;

namespace DotCraftCore.Realms
{

	using Attributes = javax.naming.directory.Attributes;
	using InitialDirContext = javax.naming.directory.InitialDirContext;

	public class RealmsServerAddress
	{
		private readonly string host;
		private readonly int port;
		private const string __OBFID = "CL_00001864";

		protected internal RealmsServerAddress(string p_i1121_1_, int p_i1121_2_)
		{
			this.host = p_i1121_1_;
			this.port = p_i1121_2_;
		}

		public virtual string Host
		{
			get
			{
				return this.host;
			}
		}

		public virtual int Port
		{
			get
			{
				return this.port;
			}
		}

		public static RealmsServerAddress parseString(string p_parseString_0_)
		{
			if (p_parseString_0_ == null)
			{
				return null;
			}
			else
			{
				string[] var1 = StringHelperClass.StringSplit(p_parseString_0_, ":", true);

				if (p_parseString_0_.StartsWith("["))
				{
					int var2 = p_parseString_0_.IndexOf("]");

					if (var2 > 0)
					{
						string var3 = p_parseString_0_.Substring(1, var2 - 1);
						string var4 = p_parseString_0_.Substring(var2 + 1).Trim();

						if (var4.StartsWith(":") && var4.Length > 0)
						{
							var4 = var4.Substring(1);
							var1 = new string[] {var3, var4};
						}
						else
						{
							var1 = new string[] {var3};
						}
					}
				}

				if (var1.Length > 2)
				{
					var1 = new string[] {p_parseString_0_};
				}

				string var5 = var1[0];
				int var6 = var1.Length > 1 ? parseInt(var1[1], 25565) : 25565;

				if (var6 == 25565)
				{
					string[] var7 = lookupSrv(var5);
					var5 = var7[0];
					var6 = parseInt(var7[1], 25565);
				}

				return new RealmsServerAddress(var5, var6);
			}
		}

		private static string[] lookupSrv(string p_lookupSrv_0_)
		{
			try
			{
				string var1 = "com.sun.jndi.dns.DnsContextFactory";
				Type.GetType("com.sun.jndi.dns.DnsContextFactory");
				Hashtable var2 = new Hashtable();
				var2.Add("java.naming.factory.initial", "com.sun.jndi.dns.DnsContextFactory");
				var2.Add("java.naming.provider.url", "dns:");
				var2.Add("com.sun.jndi.dns.timeout.retries", "1");
				InitialDirContext var3 = new InitialDirContext(var2);
				Attributes var4 = var3.getAttributes("_minecraft._tcp." + p_lookupSrv_0_, new string[] {"SRV"});
				string[] var5 = var4.get("srv").get().ToString().Split(" ", 4);
				return new string[] {var5[3], var5[2]};
			}
			catch (Exception var6)
			{
				return new string[] {p_lookupSrv_0_, int.ToString(25565)};
			}
		}

		private static int parseInt(string p_parseInt_0_, int p_parseInt_1_)
		{
			try
			{
				return Convert.ToInt32(p_parseInt_0_.Trim());
			}
			catch (Exception var3)
			{
				return p_parseInt_1_;
			}
		}
	}

}