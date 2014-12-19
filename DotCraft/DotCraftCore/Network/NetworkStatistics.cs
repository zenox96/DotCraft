using System;

namespace DotCraftCore.nNetwork
{

	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;
	using Marker = org.apache.logging.log4j.Marker;
	using MarkerManager = org.apache.logging.log4j.MarkerManager;

	public class NetworkStatistics
	{
		private static readonly Logger field_152478_a = LogManager.Logger;
		private static readonly Marker field_152479_b = MarkerManager.getMarker("NETSTAT_MARKER", NetworkManager.field_152461_c);
		private NetworkStatistics.Tracker field_152480_c = new NetworkStatistics.Tracker();
		private NetworkStatistics.Tracker field_152481_d = new NetworkStatistics.Tracker();
		

		public virtual void func_152469_a(int p_152469_1_, long p_152469_2_)
		{
			this.field_152480_c.func_152488_a(p_152469_1_, p_152469_2_);
		}

		public virtual void func_152464_b(int p_152464_1_, long p_152464_2_)
		{
			this.field_152481_d.func_152488_a(p_152464_1_, p_152464_2_);
		}

		public virtual long func_152465_a()
		{
			return this.field_152480_c.func_152485_a();
		}

		public virtual long func_152471_b()
		{
			return this.field_152481_d.func_152485_a();
		}

		public virtual long func_152472_c()
		{
			return this.field_152480_c.func_152489_b();
		}

		public virtual long func_152473_d()
		{
			return this.field_152481_d.func_152489_b();
		}

		public virtual NetworkStatistics.PacketStat func_152477_e()
		{
			return this.field_152480_c.func_152484_c();
		}

		public virtual NetworkStatistics.PacketStat func_152467_f()
		{
			return this.field_152480_c.func_152486_d();
		}

		public virtual NetworkStatistics.PacketStat func_152475_g()
		{
			return this.field_152481_d.func_152484_c();
		}

		public virtual NetworkStatistics.PacketStat func_152470_h()
		{
			return this.field_152481_d.func_152486_d();
		}

		public virtual NetworkStatistics.PacketStat func_152466_a(int p_152466_1_)
		{
			return this.field_152480_c.func_152487_a(p_152466_1_);
		}

		public virtual NetworkStatistics.PacketStat func_152468_b(int p_152468_1_)
		{
			return this.field_152481_d.func_152487_a(p_152468_1_);
		}

		public class PacketStat
		{
			private readonly int field_152482_a;
			private readonly NetworkStatistics.PacketStatData field_152483_b;
			

			public PacketStat(int p_i1188_1_, NetworkStatistics.PacketStatData p_i1188_2_)
			{
				this.field_152482_a = p_i1188_1_;
				this.field_152483_b = p_i1188_2_;
			}

			public override string ToString()
			{
				return "PacketStat(" + this.field_152482_a + ")" + this.field_152483_b;
			}
		}

		internal class PacketStatData
		{
			private readonly long field_152496_a;
			private readonly int field_152497_b;
			private readonly double field_152498_c;
			

			private PacketStatData(long p_i1184_1_, int p_i1184_3_, double p_i1184_4_)
			{
				this.field_152496_a = p_i1184_1_;
				this.field_152497_b = p_i1184_3_;
				this.field_152498_c = p_i1184_4_;
			}

			public virtual NetworkStatistics.PacketStatData func_152494_a(long p_152494_1_)
			{
				return new NetworkStatistics.PacketStatData(p_152494_1_ + this.field_152496_a, this.field_152497_b + 1, (double)((p_152494_1_ + this.field_152496_a) / (long)(this.field_152497_b + 1)));
			}

			public virtual long func_152493_a()
			{
				return this.field_152496_a;
			}

			public virtual int func_152495_b()
			{
				return this.field_152497_b;
			}

			public override string ToString()
			{
				return "{totalBytes=" + this.field_152496_a + ", count=" + this.field_152497_b + ", averageBytes=" + this.field_152498_c + '}';
			}

			internal PacketStatData(long p_i1185_1_, int p_i1185_3_, double p_i1185_4_, object p_i1185_6_) : this(p_i1185_1_, p_i1185_3_, p_i1185_4_)
			{
			}
		}

		internal class Tracker
		{
			private AtomicReference[] field_152490_a = new AtomicReference[100];
			

			public Tracker()
			{
				for (int var1 = 0; var1 < 100; ++var1)
				{
					this.field_152490_a[var1] = new AtomicReference(new NetworkStatistics.PacketStatData(0L, 0, 0.0D, null));
				}
			}

			public virtual void func_152488_a(int p_152488_1_, long p_152488_2_)
			{
				try
				{
					if (p_152488_1_ < 0 || p_152488_1_ >= 100)
					{
						return;
					}

					NetworkStatistics.PacketStatData var4;
					NetworkStatistics.PacketStatData var5;

					do
					{
						var4 = (NetworkStatistics.PacketStatData)this.field_152490_a[p_152488_1_].get();
						var5 = var4.func_152494_a(p_152488_2_);
					}
					while (!this.field_152490_a[p_152488_1_].compareAndSet(var4, var5));
				}
				catch (Exception var6)
				{
					if (NetworkStatistics.field_152478_a.DebugEnabled)
					{
						NetworkStatistics.field_152478_a.debug(NetworkStatistics.field_152479_b, "NetStat failed with packetId: " + p_152488_1_, var6);
					}
				}
			}

			public virtual long func_152485_a()
			{
				long var1 = 0L;

				for (int var3 = 0; var3 < 100; ++var3)
				{
					var1 += ((NetworkStatistics.PacketStatData)this.field_152490_a[var3].get()).func_152493_a();
				}

				return var1;
			}

			public virtual long func_152489_b()
			{
				long var1 = 0L;

				for (int var3 = 0; var3 < 100; ++var3)
				{
					var1 += (long)((NetworkStatistics.PacketStatData)this.field_152490_a[var3].get()).func_152495_b();
				}

				return var1;
			}

			public virtual NetworkStatistics.PacketStat func_152484_c()
			{
				int var1 = -1;
				NetworkStatistics.PacketStatData var2 = new NetworkStatistics.PacketStatData(-1L, -1, 0.0D, null);

				for (int var3 = 0; var3 < 100; ++var3)
				{
					NetworkStatistics.PacketStatData var4 = (NetworkStatistics.PacketStatData)this.field_152490_a[var3].get();

					if (var4.field_152496_a > var2.field_152496_a)
					{
						var1 = var3;
						var2 = var4;
					}
				}

				return new NetworkStatistics.PacketStat(var1, var2);
			}

			public virtual NetworkStatistics.PacketStat func_152486_d()
			{
				int var1 = -1;
				NetworkStatistics.PacketStatData var2 = new NetworkStatistics.PacketStatData(-1L, -1, 0.0D, null);

				for (int var3 = 0; var3 < 100; ++var3)
				{
					NetworkStatistics.PacketStatData var4 = (NetworkStatistics.PacketStatData)this.field_152490_a[var3].get();

					if (var4.field_152497_b > var2.field_152497_b)
					{
						var1 = var3;
						var2 = var4;
					}
				}

				return new NetworkStatistics.PacketStat(var1, var2);
			}

			public virtual NetworkStatistics.PacketStat func_152487_a(int p_152487_1_)
			{
				return p_152487_1_ >= 0 && p_152487_1_ < 100 ? new NetworkStatistics.PacketStat(p_152487_1_, (NetworkStatistics.PacketStatData)this.field_152490_a[p_152487_1_].get()) : null;
			}
		}
	}

}