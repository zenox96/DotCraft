using System;

namespace DotCraftCore.Network.Play.Server
{

	using EntityPainting = DotCraftCore.entity.item.EntityPainting;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S10PacketSpawnPainting : Packet
	{
		private int field_148973_a;
		private int field_148971_b;
		private int field_148972_c;
		private int field_148969_d;
		private int field_148970_e;
		private string field_148968_f;
		

		public S10PacketSpawnPainting()
		{
		}

		public S10PacketSpawnPainting(EntityPainting p_i45170_1_)
		{
			this.field_148973_a = p_i45170_1_.EntityId;
			this.field_148971_b = p_i45170_1_.field_146063_b;
			this.field_148972_c = p_i45170_1_.field_146064_c;
			this.field_148969_d = p_i45170_1_.field_146062_d;
			this.field_148970_e = p_i45170_1_.hangingDirection;
			this.field_148968_f = p_i45170_1_.art.title;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148973_a = p_148837_1_.readVarIntFromBuffer();
			this.field_148968_f = p_148837_1_.readStringFromBuffer(EntityPainting.EnumArt.maxArtTitleLength);
			this.field_148971_b = p_148837_1_.readInt();
			this.field_148972_c = p_148837_1_.readInt();
			this.field_148969_d = p_148837_1_.readInt();
			this.field_148970_e = p_148837_1_.readInt();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_148973_a);
			p_148840_1_.writeStringToBuffer(this.field_148968_f);
			p_148840_1_.writeInt(this.field_148971_b);
			p_148840_1_.writeInt(this.field_148972_c);
			p_148840_1_.writeInt(this.field_148969_d);
			p_148840_1_.writeInt(this.field_148970_e);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSpawnPainting(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, type={1}, x={2:D}, y={3:D}, z={4:D}", new object[] {Convert.ToInt32(this.field_148973_a), this.field_148968_f, Convert.ToInt32(this.field_148971_b), Convert.ToInt32(this.field_148972_c), Convert.ToInt32(this.field_148969_d)});
		}

		public virtual int func_148965_c()
		{
			return this.field_148973_a;
		}

		public virtual int func_148964_d()
		{
			return this.field_148971_b;
		}

		public virtual int func_148963_e()
		{
			return this.field_148972_c;
		}

		public virtual int func_148962_f()
		{
			return this.field_148969_d;
		}

		public virtual int func_148966_g()
		{
			return this.field_148970_e;
		}

		public virtual string func_148961_h()
		{
			return this.field_148968_f;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}