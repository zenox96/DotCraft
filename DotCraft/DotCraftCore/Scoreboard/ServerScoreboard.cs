using System.Collections;

namespace DotCraftCore.Scoreboard
{

	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using Packet = DotCraftCore.network.Packet;
	using S3BPacketScoreboardObjective = DotCraftCore.network.play.server.S3BPacketScoreboardObjective;
	using S3CPacketUpdateScore = DotCraftCore.network.play.server.S3CPacketUpdateScore;
	using S3DPacketDisplayScoreboard = DotCraftCore.network.play.server.S3DPacketDisplayScoreboard;
	using S3EPacketTeams = DotCraftCore.network.play.server.S3EPacketTeams;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;

	public class ServerScoreboard : Scoreboard
	{
		private readonly MinecraftServer scoreboardMCServer;
		private readonly Set field_96553_b = new HashSet();
		private ScoreboardSaveData field_96554_c;
		

		public ServerScoreboard(MinecraftServer p_i1501_1_)
		{
			this.scoreboardMCServer = p_i1501_1_;
		}

		public override void func_96536_a(Score p_96536_1_)
		{
			base.func_96536_a(p_96536_1_);

			if(this.field_96553_b.contains(p_96536_1_.func_96645_d()))
			{
				this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3CPacketUpdateScore(p_96536_1_, 0));
			}

			this.func_96551_b();
		}

		public override void func_96516_a(string p_96516_1_)
		{
			base.func_96516_a(p_96516_1_);
			this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3CPacketUpdateScore(p_96516_1_));
			this.func_96551_b();
		}

		public override void func_96530_a(int p_96530_1_, ScoreObjective p_96530_2_)
		{
			ScoreObjective var3 = this.func_96539_a(p_96530_1_);
			base.func_96530_a(p_96530_1_, p_96530_2_);

			if(var3 != p_96530_2_ && var3 != null)
			{
				if(this.func_96552_h(var3) > 0)
				{
					this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3DPacketDisplayScoreboard(p_96530_1_, p_96530_2_));
				}
				else
				{
					this.func_96546_g(var3);
				}
			}

			if(p_96530_2_ != null)
			{
				if(this.field_96553_b.contains(p_96530_2_))
				{
					this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3DPacketDisplayScoreboard(p_96530_1_, p_96530_2_));
				}
				else
				{
					this.func_96549_e(p_96530_2_);
				}
			}

			this.func_96551_b();
		}

		public override bool func_151392_a(string p_151392_1_, string p_151392_2_)
		{
			if(base.func_151392_a(p_151392_1_, p_151392_2_))
			{
				ScorePlayerTeam var3 = this.getTeam(p_151392_2_);
				this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3EPacketTeams(var3, new string[] {p_151392_1_}, 3));
				this.func_96551_b();
				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Removes the given username from the given ScorePlayerTeam. If the player is not on the team then an
///     * IllegalStateException is thrown. </summary>
///     
		public override void removePlayerFromTeam(string p_96512_1_, ScorePlayerTeam p_96512_2_)
		{
			base.removePlayerFromTeam(p_96512_1_, p_96512_2_);
			this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3EPacketTeams(p_96512_2_, new string[] {p_96512_1_}, 4));
			this.func_96551_b();
		}

		public override void func_96522_a(ScoreObjective p_96522_1_)
		{
			base.func_96522_a(p_96522_1_);
			this.func_96551_b();
		}

		public override void func_96532_b(ScoreObjective p_96532_1_)
		{
			base.func_96532_b(p_96532_1_);

			if(this.field_96553_b.contains(p_96532_1_))
			{
				this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3BPacketScoreboardObjective(p_96532_1_, 2));
			}

			this.func_96551_b();
		}

		public override void func_96533_c(ScoreObjective p_96533_1_)
		{
			base.func_96533_c(p_96533_1_);

			if(this.field_96553_b.contains(p_96533_1_))
			{
				this.func_96546_g(p_96533_1_);
			}

			this.func_96551_b();
		}

		public override void func_96523_a(ScorePlayerTeam p_96523_1_)
		{
			base.func_96523_a(p_96523_1_);
			this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3EPacketTeams(p_96523_1_, 0));
			this.func_96551_b();
		}

		public override void func_96538_b(ScorePlayerTeam p_96538_1_)
		{
			base.func_96538_b(p_96538_1_);
			this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3EPacketTeams(p_96538_1_, 2));
			this.func_96551_b();
		}

		public override void func_96513_c(ScorePlayerTeam p_96513_1_)
		{
			base.func_96513_c(p_96513_1_);
			this.scoreboardMCServer.ConfigurationManager.func_148540_a(new S3EPacketTeams(p_96513_1_, 1));
			this.func_96551_b();
		}

		public virtual void func_96547_a(ScoreboardSaveData p_96547_1_)
		{
			this.field_96554_c = p_96547_1_;
		}

		protected internal virtual void func_96551_b()
		{
			if(this.field_96554_c != null)
			{
				this.field_96554_c.markDirty();
			}
		}

		public virtual IList func_96550_d(ScoreObjective p_96550_1_)
		{
			ArrayList var2 = new ArrayList();
			var2.Add(new S3BPacketScoreboardObjective(p_96550_1_, 0));

			for(int var3 = 0; var3 < 3; ++var3)
			{
				if(this.func_96539_a(var3) == p_96550_1_)
				{
					var2.Add(new S3DPacketDisplayScoreboard(var3, p_96550_1_));
				}
			}

			IEnumerator var5 = this.func_96534_i(p_96550_1_).GetEnumerator();

			while(var5.MoveNext())
			{
				Score var4 = (Score)var5.Current;
				var2.Add(new S3CPacketUpdateScore(var4, 0));
			}

			return var2;
		}

		public virtual void func_96549_e(ScoreObjective p_96549_1_)
		{
			IList var2 = this.func_96550_d(p_96549_1_);
			IEnumerator var3 = this.scoreboardMCServer.ConfigurationManager.playerEntityList.GetEnumerator();

			while(var3.MoveNext())
			{
				EntityPlayerMP var4 = (EntityPlayerMP)var3.Current;
				IEnumerator var5 = var2.GetEnumerator();

				while(var5.MoveNext())
				{
					Packet var6 = (Packet)var5.Current;
					var4.playerNetServerHandler.sendPacket(var6);
				}
			}

			this.field_96553_b.add(p_96549_1_);
		}

		public virtual IList func_96548_f(ScoreObjective p_96548_1_)
		{
			ArrayList var2 = new ArrayList();
			var2.Add(new S3BPacketScoreboardObjective(p_96548_1_, 1));

			for(int var3 = 0; var3 < 3; ++var3)
			{
				if(this.func_96539_a(var3) == p_96548_1_)
				{
					var2.Add(new S3DPacketDisplayScoreboard(var3, p_96548_1_));
				}
			}

			return var2;
		}

		public virtual void func_96546_g(ScoreObjective p_96546_1_)
		{
			IList var2 = this.func_96548_f(p_96546_1_);
			IEnumerator var3 = this.scoreboardMCServer.ConfigurationManager.playerEntityList.GetEnumerator();

			while(var3.MoveNext())
			{
				EntityPlayerMP var4 = (EntityPlayerMP)var3.Current;
				IEnumerator var5 = var2.GetEnumerator();

				while(var5.MoveNext())
				{
					Packet var6 = (Packet)var5.Current;
					var4.playerNetServerHandler.sendPacket(var6);
				}
			}

			this.field_96553_b.remove(p_96546_1_);
		}

		public virtual int func_96552_h(ScoreObjective p_96552_1_)
		{
			int var2 = 0;

			for(int var3 = 0; var3 < 3; ++var3)
			{
				if(this.func_96539_a(var3) == p_96552_1_)
				{
					++var2;
				}
			}

			return var2;
		}
	}

}