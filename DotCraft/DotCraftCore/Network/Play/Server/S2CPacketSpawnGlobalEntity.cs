using System;

namespace DotCraftCore.Network.Play.Server
{

	using Entity = DotCraftCore.entity.Entity;
	using EntityLightningBolt = DotCraftCore.entity.effect.EntityLightningBolt;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class S2CPacketSpawnGlobalEntity : Packet
	{
		private int field_149059_a;
		private int field_149057_b;
		private int field_149058_c;
		private int field_149055_d;
		private int field_149056_e;
		

		public S2CPacketSpawnGlobalEntity()
		{
		}

		public S2CPacketSpawnGlobalEntity(Entity p_i45191_1_)
		{
			this.field_149059_a = p_i45191_1_.EntityId;
			this.field_149057_b = MathHelper.floor_double(p_i45191_1_.posX * 32.0D);
			this.field_149058_c = MathHelper.floor_double(p_i45191_1_.posY * 32.0D);
			this.field_149055_d = MathHelper.floor_double(p_i45191_1_.posZ * 32.0D);

			if (p_i45191_1_ is EntityLightningBolt)
			{
				this.field_149056_e = 1;
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149059_a = p_148837_1_.readVarIntFromBuffer();
			this.field_149056_e = p_148837_1_.readByte();
			this.field_149057_b = p_148837_1_.readInt();
			this.field_149058_c = p_148837_1_.readInt();
			this.field_149055_d = p_148837_1_.readInt();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_149059_a);
			p_148840_1_.writeByte(this.field_149056_e);
			p_148840_1_.writeInt(this.field_149057_b);
			p_148840_1_.writeInt(this.field_149058_c);
			p_148840_1_.writeInt(this.field_149055_d);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSpawnGlobalEntity(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, type={1:D}, x={2:F2}, y={3:F2}, z={4:F2}", new object[] {Convert.ToInt32(this.field_149059_a), Convert.ToInt32(this.field_149056_e), Convert.ToSingle((float)this.field_149057_b / 32.0F), Convert.ToSingle((float)this.field_149058_c / 32.0F), Convert.ToSingle((float)this.field_149055_d / 32.0F)});
		}

		public virtual int func_149052_c()
		{
			return this.field_149059_a;
		}

		public virtual int func_149051_d()
		{
			return this.field_149057_b;
		}

		public virtual int func_149050_e()
		{
			return this.field_149058_c;
		}

		public virtual int func_149049_f()
		{
			return this.field_149055_d;
		}

		public virtual int func_149053_g()
		{
			return this.field_149056_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}