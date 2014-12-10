using System.Collections;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using ScorePlayerTeam = DotCraftCore.Scoreboard.ScorePlayerTeam;

	public class S3EPacketTeams : Packet
	{
		private string field_149320_a = "";
		private string field_149318_b = "";
		private string field_149319_c = "";
		private string field_149316_d = "";
		private ICollection field_149317_e = new ArrayList();
		private int field_149314_f;
		private int field_149315_g;
		private const string __OBFID = "CL_00001334";

		public S3EPacketTeams()
		{
		}

		public S3EPacketTeams(ScorePlayerTeam p_i45225_1_, int p_i45225_2_)
		{
			this.field_149320_a = p_i45225_1_.RegisteredName;
			this.field_149314_f = p_i45225_2_;

			if (p_i45225_2_ == 0 || p_i45225_2_ == 2)
			{
				this.field_149318_b = p_i45225_1_.func_96669_c();
				this.field_149319_c = p_i45225_1_.ColorPrefix;
				this.field_149316_d = p_i45225_1_.ColorSuffix;
				this.field_149315_g = p_i45225_1_.func_98299_i();
			}

			if (p_i45225_2_ == 0)
			{
				this.field_149317_e.addAll(p_i45225_1_.MembershipCollection);
			}
		}

		public S3EPacketTeams(ScorePlayerTeam p_i45226_1_, ICollection p_i45226_2_, int p_i45226_3_)
		{
			if (p_i45226_3_ != 3 && p_i45226_3_ != 4)
			{
				throw new System.ArgumentException("Method must be join or leave for player constructor");
			}
			else if (p_i45226_2_ != null && !p_i45226_2_.Empty)
			{
				this.field_149314_f = p_i45226_3_;
				this.field_149320_a = p_i45226_1_.RegisteredName;
				this.field_149317_e.addAll(p_i45226_2_);
			}
			else
			{
				throw new System.ArgumentException("Players cannot be null/empty");
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149320_a = p_148837_1_.readStringFromBuffer(16);
			this.field_149314_f = p_148837_1_.readByte();

			if (this.field_149314_f == 0 || this.field_149314_f == 2)
			{
				this.field_149318_b = p_148837_1_.readStringFromBuffer(32);
				this.field_149319_c = p_148837_1_.readStringFromBuffer(16);
				this.field_149316_d = p_148837_1_.readStringFromBuffer(16);
				this.field_149315_g = p_148837_1_.readByte();
			}

			if (this.field_149314_f == 0 || this.field_149314_f == 3 || this.field_149314_f == 4)
			{
				short var2 = p_148837_1_.readShort();

				for (int var3 = 0; var3 < var2; ++var3)
				{
					this.field_149317_e.add(p_148837_1_.readStringFromBuffer(40));
				}
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149320_a);
			p_148840_1_.writeByte(this.field_149314_f);

			if (this.field_149314_f == 0 || this.field_149314_f == 2)
			{
				p_148840_1_.writeStringToBuffer(this.field_149318_b);
				p_148840_1_.writeStringToBuffer(this.field_149319_c);
				p_148840_1_.writeStringToBuffer(this.field_149316_d);
				p_148840_1_.writeByte(this.field_149315_g);
			}

			if (this.field_149314_f == 0 || this.field_149314_f == 3 || this.field_149314_f == 4)
			{
				p_148840_1_.writeShort(this.field_149317_e.size());
				IEnumerator var2 = this.field_149317_e.GetEnumerator();

				while (var2.MoveNext())
				{
					string var3 = (string)var2.Current;
					p_148840_1_.writeStringToBuffer(var3);
				}
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleTeams(this);
		}

		public virtual string func_149312_c()
		{
			return this.field_149320_a;
		}

		public virtual string func_149306_d()
		{
			return this.field_149318_b;
		}

		public virtual string func_149311_e()
		{
			return this.field_149319_c;
		}

		public virtual string func_149309_f()
		{
			return this.field_149316_d;
		}

		public virtual ICollection func_149310_g()
		{
			return this.field_149317_e;
		}

		public virtual int func_149307_h()
		{
			return this.field_149314_f;
		}

		public virtual int func_149308_i()
		{
			return this.field_149315_g;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}