using System;

namespace DotCraftCore.Network.Play.Server
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S12PacketEntityVelocity : Packet
	{
		private int field_149417_a;
		private int field_149415_b;
		private int field_149416_c;
		private int field_149414_d;
		

		public S12PacketEntityVelocity()
		{
		}

		public S12PacketEntityVelocity(Entity p_i45219_1_) : this(p_i45219_1_.getEntityId(), p_i45219_1_.motionX, p_i45219_1_.motionY, p_i45219_1_.motionZ)
		{
		}

		public S12PacketEntityVelocity(int p_i45220_1_, double p_i45220_2_, double p_i45220_4_, double p_i45220_6_)
		{
			this.field_149417_a = p_i45220_1_;
			double var8 = 3.9D;

			if (p_i45220_2_ < -var8)
			{
				p_i45220_2_ = -var8;
			}

			if (p_i45220_4_ < -var8)
			{
				p_i45220_4_ = -var8;
			}

			if (p_i45220_6_ < -var8)
			{
				p_i45220_6_ = -var8;
			}

			if (p_i45220_2_ > var8)
			{
				p_i45220_2_ = var8;
			}

			if (p_i45220_4_ > var8)
			{
				p_i45220_4_ = var8;
			}

			if (p_i45220_6_ > var8)
			{
				p_i45220_6_ = var8;
			}

			this.field_149415_b = (int)(p_i45220_2_ * 8000.0D);
			this.field_149416_c = (int)(p_i45220_4_ * 8000.0D);
			this.field_149414_d = (int)(p_i45220_6_ * 8000.0D);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149417_a = p_148837_1_.readInt();
			this.field_149415_b = p_148837_1_.readShort();
			this.field_149416_c = p_148837_1_.readShort();
			this.field_149414_d = p_148837_1_.readShort();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149417_a);
			p_148840_1_.writeShort(this.field_149415_b);
			p_148840_1_.writeShort(this.field_149416_c);
			p_148840_1_.writeShort(this.field_149414_d);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityVelocity(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, x={1:F2}, y={2:F2}, z={3:F2}", new object[] {Convert.ToInt32(this.field_149417_a), Convert.ToSingle((float)this.field_149415_b / 8000.0F), Convert.ToSingle((float)this.field_149416_c / 8000.0F), Convert.ToSingle((float)this.field_149414_d / 8000.0F)});
		}

		public virtual int func_149412_c()
		{
			return this.field_149417_a;
		}

		public virtual int func_149411_d()
		{
			return this.field_149415_b;
		}

		public virtual int func_149410_e()
		{
			return this.field_149416_c;
		}

		public virtual int func_149409_f()
		{
			return this.field_149414_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}