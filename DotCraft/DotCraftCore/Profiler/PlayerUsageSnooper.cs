using System;
using System.Collections;

namespace DotCraftCore.Profiler
{

	using Maps = com.google.common.collect.Maps;
	using HttpUtil = DotCraftCore.Util.HttpUtil;

	public class PlayerUsageSnooper
	{
		private readonly IDictionary field_152773_a = Maps.newHashMap();
		private readonly IDictionary field_152774_b = Maps.newHashMap();
		private readonly string uniqueID = UUID.randomUUID().ToString();

	/// <summary> URL of the server to send the report to  </summary>
		private readonly URL serverUrl;
		private readonly IPlayerUsage playerStatsCollector;

	/// <summary> set to fire the snooperThread every 15 mins  </summary>
		private readonly Timer threadTrigger = new Timer("Snooper Timer", true);
		private readonly object syncLock = new object();
		private readonly long minecraftStartTimeMilis;
		private bool isRunning;

	/// <summary> incremented on every getSelfCounterFor  </summary>
		private int selfCounter;
		private const string __OBFID = "CL_00001515";

		public PlayerUsageSnooper(string p_i1563_1_, IPlayerUsage p_i1563_2_, long p_i1563_3_)
		{
			try
			{
				this.serverUrl = new URL("http://snoop.minecraft.net/" + p_i1563_1_ + "?version=" + 2);
			}
			catch (MalformedURLException var6)
			{
				throw new System.ArgumentException();
			}

			this.playerStatsCollector = p_i1563_2_;
			this.minecraftStartTimeMilis = p_i1563_3_;
		}

///    
///     <summary> * Note issuing start multiple times is not an error. </summary>
///     
		public virtual void startSnooper()
		{
			if (!this.isRunning)
			{
				this.isRunning = true;
				this.func_152766_h();
				this.threadTrigger.schedule(new TimerTask() { private static final string __OBFID = "CL_00001516"; public void run() { if (PlayerUsageSnooper.playerStatsCollector.SnooperEnabled) { Hashtable var1; synchronized (PlayerUsageSnooper.syncLock) { var1 = new Hashtable(PlayerUsageSnooper.field_152774_b); if (PlayerUsageSnooper.selfCounter == 0) { var1.putAll(PlayerUsageSnooper.field_152773_a); } var1.put("snooper_count", Convert.ToInt32(PlayerUsageSnooper.access$308(PlayerUsageSnooper.this))); var1.put("snooper_token", PlayerUsageSnooper.uniqueID); } HttpUtil.func_151226_a(PlayerUsageSnooper.serverUrl, var1, true); } } }, 0L, 900000L);
			}
		}

		private void func_152766_h()
		{
			this.addJvmArgsToSnooper();
			this.func_152768_a("snooper_token", this.uniqueID);
			this.func_152767_b("snooper_token", this.uniqueID);
			this.func_152767_b("os_name", System.getProperty("os.name"));
			this.func_152767_b("os_version", System.getProperty("os.version"));
			this.func_152767_b("os_architecture", System.getProperty("os.arch"));
			this.func_152767_b("java_version", System.getProperty("java.version"));
			this.func_152767_b("version", "1.7.10");
			this.playerStatsCollector.addServerTypeToSnooper(this);
		}

		private void addJvmArgsToSnooper()
		{
			RuntimeMXBean var1 = ManagementFactory.RuntimeMXBean;
			IList var2 = var1.InputArguments;
			int var3 = 0;
			IEnumerator var4 = var2.GetEnumerator();

			while (var4.MoveNext())
			{
				string var5 = (string)var4.Current;

				if (var5.StartsWith("-X"))
				{
					this.func_152768_a("jvm_arg[" + var3++ + "]", var5);
				}
			}

			this.func_152768_a("jvm_args", Convert.ToInt32(var3));
		}

		public virtual void addMemoryStatsToSnooper()
		{
			this.func_152767_b("memory_total", Convert.ToInt64(Runtime.Runtime.totalMemory()));
			this.func_152767_b("memory_max", Convert.ToInt64(Runtime.Runtime.maxMemory()));
			this.func_152767_b("memory_free", Convert.ToInt64(Runtime.Runtime.freeMemory()));
			this.func_152767_b("cpu_cores", Convert.ToInt32(Runtime.Runtime.availableProcessors()));
			this.playerStatsCollector.addServerStatsToSnooper(this);
		}

		public virtual void func_152768_a(string p_152768_1_, object p_152768_2_)
		{
			object var3 = this.syncLock;

			lock (this.syncLock)
			{
				this.field_152774_b.Add(p_152768_1_, p_152768_2_);
			}
		}

		public virtual void func_152767_b(string p_152767_1_, object p_152767_2_)
		{
			object var3 = this.syncLock;

			lock (this.syncLock)
			{
				this.field_152773_a.Add(p_152767_1_, p_152767_2_);
			}
		}

		public virtual IDictionary CurrentStats
		{
			get
			{
				LinkedHashMap var1 = new LinkedHashMap();
				object var2 = this.syncLock;
	
				lock (this.syncLock)
				{
					this.addMemoryStatsToSnooper();
					IEnumerator var3 = this.field_152773_a.GetEnumerator();
					Entry var4;
	
					while (var3.MoveNext())
					{
						var4 = (Entry)var3.Current;
						var1.put(var4.Key, var4.Value.ToString());
					}
	
					var3 = this.field_152774_b.GetEnumerator();
	
					while (var3.MoveNext())
					{
						var4 = (Entry)var3.Current;
						var1.put(var4.Key, var4.Value.ToString());
					}
	
					return var1;
				}
			}
		}

		public virtual bool isSnooperRunning()
		{
			get
			{
				return this.isRunning;
			}
		}

		public virtual void stopSnooper()
		{
			this.threadTrigger.cancel();
		}

		public virtual string UniqueID
		{
			get
			{
				return this.uniqueID;
			}
		}

///    
///     <summary> * Returns the saved value of System#currentTimeMillis when the game started </summary>
///     
		public virtual long MinecraftStartTimeMillis
		{
			get
			{
				return this.minecraftStartTimeMilis;
			}
		}

		static int access$308(PlayerUsageSnooper p_access)
		{
			return p_access$308_0_.selfCounter++;
		}
	}

}