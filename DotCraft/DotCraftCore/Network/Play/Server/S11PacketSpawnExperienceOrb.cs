using System;

namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using EntityXPOrb = DotCraftCore.entity.item.EntityXPOrb;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;
	using MathHelper = DotCraftCore.nUtil.MathHelper;

	public class S11PacketSpawnExperienceOrb : Packet
	{
		private int field_148992_a;
		private int field_148990_b;
		private int field_148991_c;
		private int field_148988_d;
		private int field_148989_e;
		

		public S11PacketSpawnExperienceOrb()
		{
		}

		public S11PacketSpawnExperienceOrb(EntityXPOrb p_i45167_1_)
		{
			this.field_148992_a = p_i45167_1_.EntityId;
			this.field_148990_b = MathHelper.floor_double(p_i45167_1_.posX * 32.0D);
			this.field_148991_c = MathHelper.floor_double(p_i45167_1_.posY * 32.0D);
			this.field_148988_d = MathHelper.floor_double(p_i45167_1_.posZ * 32.0D);
			this.field_148989_e = p_i45167_1_.XpValue;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148992_a = p_148837_1_.readVarIntFromBuffer();
			this.field_148990_b = p_148837_1_.readInt();
			this.field_148991_c = p_148837_1_.readInt();
			this.field_148988_d = p_148837_1_.readInt();
			this.field_148989_e = p_148837_1_.readShort();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_148992_a);
			p_148840_1_.writeInt(this.field_148990_b);
			p_148840_1_.writeInt(this.field_148991_c);
			p_148840_1_.writeInt(this.field_148988_d);
			p_148840_1_.writeShort(this.field_148989_e);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSpawnExperienceOrb(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, value={1:D}, x={2:F2}, y={3:F2}, z={4:F2}", new object[] {Convert.ToInt32(this.field_148992_a), Convert.ToInt32(this.field_148989_e), Convert.ToSingle((float)this.field_148990_b / 32.0F), Convert.ToSingle((float)this.field_148991_c / 32.0F), Convert.ToSingle((float)this.field_148988_d / 32.0F)});
		}

		public virtual int func_148985_c()
		{
			return this.field_148992_a;
		}

		public virtual int func_148984_d()
		{
			return this.field_148990_b;
		}

		public virtual int func_148983_e()
		{
			return this.field_148991_c;
		}

		public virtual int func_148982_f()
		{
			return this.field_148988_d;
		}

		public virtual int func_148986_g()
		{
			return this.field_148989_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}