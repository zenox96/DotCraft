using System;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using EnumDifficulty = DotCraftCore.World.EnumDifficulty;
	using WorldSettings = DotCraftCore.World.WorldSettings;
	using WorldType = DotCraftCore.World.WorldType;

	public class S01PacketJoinGame : Packet
	{
		private int field_149206_a;
		private bool field_149204_b;
		private WorldSettings.GameType field_149205_c;
		private int field_149202_d;
		private EnumDifficulty field_149203_e;
		private int field_149200_f;
		private WorldType field_149201_g;
		private const string __OBFID = "CL_00001310";

		public S01PacketJoinGame()
		{
		}

		public S01PacketJoinGame(int p_i45201_1_, WorldSettings.GameType p_i45201_2_, bool p_i45201_3_, int p_i45201_4_, EnumDifficulty p_i45201_5_, int p_i45201_6_, WorldType p_i45201_7_)
		{
			this.field_149206_a = p_i45201_1_;
			this.field_149202_d = p_i45201_4_;
			this.field_149203_e = p_i45201_5_;
			this.field_149205_c = p_i45201_2_;
			this.field_149200_f = p_i45201_6_;
			this.field_149204_b = p_i45201_3_;
			this.field_149201_g = p_i45201_7_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149206_a = p_148837_1_.readInt();
			short var2 = p_148837_1_.readUnsignedByte();
			this.field_149204_b = (var2 & 8) == 8;
			int var3 = var2 & -9;
			this.field_149205_c = WorldSettings.GameType.getByID(var3);
			this.field_149202_d = p_148837_1_.readByte();
			this.field_149203_e = EnumDifficulty.getDifficultyEnum(p_148837_1_.readUnsignedByte());
			this.field_149200_f = p_148837_1_.readUnsignedByte();
			this.field_149201_g = WorldType.parseWorldType(p_148837_1_.readStringFromBuffer(16));

			if (this.field_149201_g == null)
			{
				this.field_149201_g = WorldType.DEFAULT;
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149206_a);
			int var2 = this.field_149205_c.ID;

			if (this.field_149204_b)
			{
				var2 |= 8;
			}

			p_148840_1_.writeByte(var2);
			p_148840_1_.writeByte(this.field_149202_d);
			p_148840_1_.writeByte(this.field_149203_e.DifficultyId);
			p_148840_1_.writeByte(this.field_149200_f);
			p_148840_1_.writeStringToBuffer(this.field_149201_g.WorldTypeName);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleJoinGame(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: return String.format("eid=%d, gameType=%d, hardcore=%b, dimension=%d, difficulty=%s, maxplayers=%d", new Object[] {Integer.valueOf(this.field_149206_a), Integer.valueOf(this.field_149205_c.getID()), Boolean.valueOf(this.field_149204_b), Integer.valueOf(this.field_149202_d), this.field_149203_e, Integer.valueOf(this.field_149200_f)});
			return string.Format("eid=%d, gameType=%d, hardcore=%b, dimension=%d, difficulty=%s, maxplayers=%d", new object[] {Convert.ToInt32(this.field_149206_a), Convert.ToInt32(this.field_149205_c.ID), Convert.ToBoolean(this.field_149204_b), Convert.ToInt32(this.field_149202_d), this.field_149203_e, Convert.ToInt32(this.field_149200_f)});
		}

		public virtual int func_149197_c()
		{
			return this.field_149206_a;
		}

		public virtual bool func_149195_d()
		{
			return this.field_149204_b;
		}

		public virtual WorldSettings.GameType func_149198_e()
		{
			return this.field_149205_c;
		}

		public virtual int func_149194_f()
		{
			return this.field_149202_d;
		}

		public virtual EnumDifficulty func_149192_g()
		{
			return this.field_149203_e;
		}

		public virtual int func_149193_h()
		{
			return this.field_149200_f;
		}

		public virtual WorldType func_149196_i()
		{
			return this.field_149201_g;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}