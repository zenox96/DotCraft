using System;

namespace DotCraftCore.nNetwork.nPlay.nClient
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.nNetwork.nPlay.INetHandlerPlayServer;
	using EnumDifficulty = DotCraftCore.nWorld.EnumDifficulty;

	public class C15PacketClientSettings : Packet
	{
		private string field_149530_a;
		private int field_149528_b;
		private EntityPlayer.EnumChatVisibility field_149529_c;
		private bool field_149526_d;
		private EnumDifficulty field_149527_e;
		private bool field_149525_f;
		

		public C15PacketClientSettings()
		{
		}

		public C15PacketClientSettings(string p_i45243_1_, int p_i45243_2_, EntityPlayer.EnumChatVisibility p_i45243_3_, bool p_i45243_4_, EnumDifficulty p_i45243_5_, bool p_i45243_6_)
		{
			this.field_149530_a = p_i45243_1_;
			this.field_149528_b = p_i45243_2_;
			this.field_149529_c = p_i45243_3_;
			this.field_149526_d = p_i45243_4_;
			this.field_149527_e = p_i45243_5_;
			this.field_149525_f = p_i45243_6_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149530_a = p_148837_1_.readStringFromBuffer(7);
			this.field_149528_b = p_148837_1_.readByte();
			this.field_149529_c = EntityPlayer.EnumChatVisibility.getEnumChatVisibility(p_148837_1_.readByte());
			this.field_149526_d = p_148837_1_.readBoolean();
			this.field_149527_e = EnumDifficulty.getDifficultyEnum(p_148837_1_.readByte());
			this.field_149525_f = p_148837_1_.readBoolean();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149530_a);
			p_148840_1_.writeByte(this.field_149528_b);
			p_148840_1_.writeByte(this.field_149529_c.ChatVisibility);
			p_148840_1_.writeBoolean(this.field_149526_d);
			p_148840_1_.writeByte(this.field_149527_e.DifficultyId);
			p_148840_1_.writeBoolean(this.field_149525_f);
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processClientSettings(this);
		}

		public virtual string func_149524_c()
		{
			return this.field_149530_a;
		}

		public virtual int func_149521_d()
		{
			return this.field_149528_b;
		}

		public virtual EntityPlayer.EnumChatVisibility func_149523_e()
		{
			return this.field_149529_c;
		}

		public virtual bool func_149520_f()
		{
			return this.field_149526_d;
		}

		public virtual EnumDifficulty func_149518_g()
		{
			return this.field_149527_e;
		}

		public virtual bool func_149519_h()
		{
			return this.field_149525_f;
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: return String.format("lang=\'%s\', view=%d, chat=%s, col=%b, difficulty=%s, cape=%b", new Object[] {this.field_149530_a, Integer.valueOf(this.field_149528_b), this.field_149529_c, Boolean.valueOf(this.field_149526_d), this.field_149527_e, Boolean.valueOf(this.field_149525_f)});
			return string.Format("lang=\'%s\', view=%d, chat=%s, col=%b, difficulty=%s, cape=%b", new object[] {this.field_149530_a, Convert.ToInt32(this.field_149528_b), this.field_149529_c, Convert.ToBoolean(this.field_149526_d), this.field_149527_e, Convert.ToBoolean(this.field_149525_f)});
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}