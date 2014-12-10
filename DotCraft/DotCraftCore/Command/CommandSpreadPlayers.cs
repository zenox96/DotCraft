using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using Lists = com.google.common.collect.Lists;
	using Maps = com.google.common.collect.Maps;
	using Sets = com.google.common.collect.Sets;
	using Material = DotCraftCore.block.material.Material;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using Team = DotCraftCore.scoreboard.Team;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.util.ChatComponentTranslation;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public class CommandSpreadPlayers : CommandBase
	{
		private const string __OBFID = "CL_00001080";

		public virtual string CommandName
		{
			get
			{
				return "spreadplayers";
			}
		}

///    
///     <summary> * Return the required permission level for this command. </summary>
///     
		public override int RequiredPermissionLevel
		{
			get
			{
				return 2;
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.spreadplayers.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 6)
			{
				throw new WrongUsageException("commands.spreadplayers.usage", new object[0]);
			}
			else
			{
				sbyte var3 = 0;
				int var16 = var3 + 1;
				double var4 = func_110666_a(p_71515_1_, double.NaN, p_71515_2_[var3]);
				double var6 = func_110666_a(p_71515_1_, double.NaN, p_71515_2_[var16++]);
				double var8 = parseDoubleWithMin(p_71515_1_, p_71515_2_[var16++], 0.0D);
				double var10 = parseDoubleWithMin(p_71515_1_, p_71515_2_[var16++], var8 + 1.0D);
				bool var12 = parseBoolean(p_71515_1_, p_71515_2_[var16++]);
				ArrayList var13 = Lists.newArrayList();

				while (true)
				{
					while (var16 < p_71515_2_.Length)
					{
						string var14 = p_71515_2_[var16++];

						if(PlayerSelector.hasArguments(var14))
						{
							EntityPlayerMP[] var17 = PlayerSelector.matchPlayers(p_71515_1_, var14);

							if(var17 == null || var17.Length == 0)
							{
								throw new PlayerNotFoundException();
							}

							Collections.addAll(var13, var17);
						}
						else
						{
							EntityPlayerMP var15 = MinecraftServer.Server.ConfigurationManager.func_152612_a(var14);

							if(var15 == null)
							{
								throw new PlayerNotFoundException();
							}

							var13.Add(var15);
						}
					}

					if(var13.Count == 0)
					{
						throw new PlayerNotFoundException();
					}

					p_71515_1_.addChatMessage(new ChatComponentTranslation("commands.spreadplayers.spreading." + (var12 ? "teams" : "players"), new object[] {Convert.ToInt32(var13.Count), Convert.ToDouble(var10), Convert.ToDouble(var4), Convert.ToDouble(var6), Convert.ToDouble(var8)}));
					this.func_110669_a(p_71515_1_, var13, new CommandSpreadPlayers.Position(var4, var6), var8, var10, ((EntityLivingBase)var13[0]).worldObj, var12);
					return;
				}
			}
		}

		private void func_110669_a(ICommandSender p_110669_1_, IList p_110669_2_, CommandSpreadPlayers.Position p_110669_3_, double p_110669_4_, double p_110669_6_, World p_110669_8_, bool p_110669_9_)
		{
			Random var10 = new Random();
			double var11 = p_110669_3_.field_111101_a - p_110669_6_;
			double var13 = p_110669_3_.field_111100_b - p_110669_6_;
			double var15 = p_110669_3_.field_111101_a + p_110669_6_;
			double var17 = p_110669_3_.field_111100_b + p_110669_6_;
			CommandSpreadPlayers.Position[] var19 = this.func_110670_a(var10, p_110669_9_ ? this.func_110667_a(p_110669_2_) : p_110669_2_.Count, var11, var13, var15, var17);
			int var20 = this.func_110668_a(p_110669_3_, p_110669_4_, p_110669_8_, var10, var11, var13, var15, var17, var19, p_110669_9_);
			double var21 = this.func_110671_a(p_110669_2_, p_110669_8_, var19, p_110669_9_);
			func_152373_a(p_110669_1_, this, "commands.spreadplayers.success." + (p_110669_9_ ? "teams" : "players"), new object[] {Convert.ToInt32(var19.Length), Convert.ToDouble(p_110669_3_.field_111101_a), Convert.ToDouble(p_110669_3_.field_111100_b)});

			if(var19.Length > 1)
			{
				p_110669_1_.addChatMessage(new ChatComponentTranslation("commands.spreadplayers.info." + (p_110669_9_ ? "teams" : "players"), new object[] {string.Format("{0:F2}", new object[]{Convert.ToDouble(var21)}), Convert.ToInt32(var20)}));
			}
		}

		private int func_110667_a(IList p_110667_1_)
		{
			HashSet var2 = Sets.newHashSet();
			IEnumerator var3 = p_110667_1_.GetEnumerator();

			while (var3.MoveNext())
			{
				EntityLivingBase var4 = (EntityLivingBase)var3.Current;

				if(var4 is EntityPlayer)
				{
					var2.Add(var4.Team);
				}
				else
				{
					var2.Add((object)null);
				}
			}

			return var2.Count;
		}

		private int func_110668_a(CommandSpreadPlayers.Position p_110668_1_, double p_110668_2_, World p_110668_4_, Random p_110668_5_, double p_110668_6_, double p_110668_8_, double p_110668_10_, double p_110668_12_, CommandSpreadPlayers.Position[] p_110668_14_, bool p_110668_15_)
		{
			bool var16 = true;
			double var18 = 3.4028234663852886E38D;
			int var17;

			for (var17 = 0; var17 < 10000 && var16; ++var17)
			{
				var16 = false;
				var18 = 3.4028234663852886E38D;
				int var22;
				CommandSpreadPlayers.Position var23;

				for (int var20 = 0; var20 < p_110668_14_.Length; ++var20)
				{
					CommandSpreadPlayers.Position var21 = p_110668_14_[var20];
					var22 = 0;
					var23 = new CommandSpreadPlayers.Position();

					for (int var24 = 0; var24 < p_110668_14_.Length; ++var24)
					{
						if(var20 != var24)
						{
							CommandSpreadPlayers.Position var25 = p_110668_14_[var24];
							double var26 = var21.func_111099_a(var25);
							var18 = Math.Min(var26, var18);

							if(var26 < p_110668_2_)
							{
								++var22;
								var23.field_111101_a += var25.field_111101_a - var21.field_111101_a;
								var23.field_111100_b += var25.field_111100_b - var21.field_111100_b;
							}
						}
					}

					if(var22 > 0)
					{
						var23.field_111101_a /= (double)var22;
						var23.field_111100_b /= (double)var22;
						double var30 = (double)var23.func_111096_b();

						if(var30 > 0.0D)
						{
							var23.func_111095_a();
							var21.func_111094_b(var23);
						}
						else
						{
							var21.func_111097_a(p_110668_5_, p_110668_6_, p_110668_8_, p_110668_10_, p_110668_12_);
						}

						var16 = true;
					}

					if(var21.func_111093_a(p_110668_6_, p_110668_8_, p_110668_10_, p_110668_12_))
					{
						var16 = true;
					}
				}

				if(!var16)
				{
					CommandSpreadPlayers.Position[] var28 = p_110668_14_;
					int var29 = p_110668_14_.Length;

					for (var22 = 0; var22 < var29; ++var22)
					{
						var23 = var28[var22];

						if(!var23.func_111098_b(p_110668_4_))
						{
							var23.func_111097_a(p_110668_5_, p_110668_6_, p_110668_8_, p_110668_10_, p_110668_12_);
							var16 = true;
						}
					}
				}
			}

			if(var17 >= 10000)
			{
				throw new CommandException("commands.spreadplayers.failure." + (p_110668_15_ ? "teams" : "players"), new object[] {Convert.ToInt32(p_110668_14_.Length), Convert.ToDouble(p_110668_1_.field_111101_a), Convert.ToDouble(p_110668_1_.field_111100_b), string.Format("{0:F2}", new object[]{Convert.ToDouble(var18)})});
			}
			else
			{
				return var17;
			}
		}

		private double func_110671_a(IList p_110671_1_, World p_110671_2_, CommandSpreadPlayers.Position[] p_110671_3_, bool p_110671_4_)
		{
			double var5 = 0.0D;
			int var7 = 0;
			Hashtable var8 = Maps.newHashMap();

			for (int var9 = 0; var9 < p_110671_1_.Count; ++var9)
			{
				EntityLivingBase var10 = (EntityLivingBase)p_110671_1_[var9];
				CommandSpreadPlayers.Position var11;

				if(p_110671_4_)
				{
					Team var12 = var10 is EntityPlayer ? var10.Team : null;

					if(!var8.ContainsKey(var12))
					{
						var8.Add(var12, p_110671_3_[var7++]);
					}

					var11 = (CommandSpreadPlayers.Position)var8[var12];
				}
				else
				{
					var11 = p_110671_3_[var7++];
				}

				var10.setPositionAndUpdate((double)((float)MathHelper.floor_double(var11.field_111101_a) + 0.5F), (double)var11.func_111092_a(p_110671_2_), (double)MathHelper.floor_double(var11.field_111100_b) + 0.5D);
				double var17 = double.MaxValue;

				for (int var14 = 0; var14 < p_110671_3_.Length; ++var14)
				{
					if(var11 != p_110671_3_[var14])
					{
						double var15 = var11.func_111099_a(p_110671_3_[var14]);
						var17 = Math.Min(var15, var17);
					}
				}

				var5 += var17;
			}

			var5 /= (double)p_110671_1_.Count;
			return var5;
		}

		private CommandSpreadPlayers.Position[] func_110670_a(Random p_110670_1_, int p_110670_2_, double p_110670_3_, double p_110670_5_, double p_110670_7_, double p_110670_9_)
		{
			CommandSpreadPlayers.Position[] var11 = new CommandSpreadPlayers.Position[p_110670_2_];

			for (int var12 = 0; var12 < var11.Length; ++var12)
			{
				CommandSpreadPlayers.Position var13 = new CommandSpreadPlayers.Position();
				var13.func_111097_a(p_110670_1_, p_110670_3_, p_110670_5_, p_110670_7_, p_110670_9_);
				var11[var12] = var13;
			}

			return var11;
		}

		internal class Position
		{
			internal double field_111101_a;
			internal double field_111100_b;
			private const string __OBFID = "CL_00001105";

			internal Position()
			{
			}

			internal Position(double p_i1358_1_, double p_i1358_3_)
			{
				this.field_111101_a = p_i1358_1_;
				this.field_111100_b = p_i1358_3_;
			}

			internal virtual double func_111099_a(CommandSpreadPlayers.Position p_111099_1_)
			{
				double var2 = this.field_111101_a - p_111099_1_.field_111101_a;
				double var4 = this.field_111100_b - p_111099_1_.field_111100_b;
				return Math.Sqrt(var2 * var2 + var4 * var4);
			}

			internal virtual void func_111095_a()
			{
				double var1 = (double)this.func_111096_b();
				this.field_111101_a /= var1;
				this.field_111100_b /= var1;
			}

			internal virtual float func_111096_b()
			{
				return MathHelper.sqrt_double(this.field_111101_a * this.field_111101_a + this.field_111100_b * this.field_111100_b);
			}

			public virtual void func_111094_b(CommandSpreadPlayers.Position p_111094_1_)
			{
				this.field_111101_a -= p_111094_1_.field_111101_a;
				this.field_111100_b -= p_111094_1_.field_111100_b;
			}

			public virtual bool func_111093_a(double p_111093_1_, double p_111093_3_, double p_111093_5_, double p_111093_7_)
			{
				bool var9 = false;

				if(this.field_111101_a < p_111093_1_)
				{
					this.field_111101_a = p_111093_1_;
					var9 = true;
				}
				else if(this.field_111101_a > p_111093_5_)
				{
					this.field_111101_a = p_111093_5_;
					var9 = true;
				}

				if(this.field_111100_b < p_111093_3_)
				{
					this.field_111100_b = p_111093_3_;
					var9 = true;
				}
				else if(this.field_111100_b > p_111093_7_)
				{
					this.field_111100_b = p_111093_7_;
					var9 = true;
				}

				return var9;
			}

			public virtual int func_111092_a(World p_111092_1_)
			{
				int var2 = MathHelper.floor_double(this.field_111101_a);
				int var3 = MathHelper.floor_double(this.field_111100_b);

				for (int var4 = 256; var4 > 0; --var4)
				{
					if(p_111092_1_.getBlock(var2, var4, var3).Material != Material.air)
					{
						return var4 + 1;
					}
				}

				return 257;
			}

			public virtual bool func_111098_b(World p_111098_1_)
			{
				int var2 = MathHelper.floor_double(this.field_111101_a);
				int var3 = MathHelper.floor_double(this.field_111100_b);
				short var4 = 256;

				if(var4 <= 0)
				{
					return false;
				}
				else
				{
					Material var5 = p_111098_1_.getBlock(var2, var4, var3).Material;
					return !var5.Liquid && var5 != Material.fire;
				}
			}

			public virtual void func_111097_a(Random p_111097_1_, double p_111097_2_, double p_111097_4_, double p_111097_6_, double p_111097_8_)
			{
				this.field_111101_a = MathHelper.getRandomDoubleInRange(p_111097_1_, p_111097_2_, p_111097_6_);
				this.field_111100_b = MathHelper.getRandomDoubleInRange(p_111097_1_, p_111097_4_, p_111097_8_);
			}
		}
	}

}