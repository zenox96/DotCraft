using System;

namespace DotCraftCore.Network.Play.Server
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using World = DotCraftCore.World.World;

	public class S14PacketEntity : Packet
	{
		protected internal int field_149074_a;
		protected internal sbyte field_149072_b;
		protected internal sbyte field_149073_c;
		protected internal sbyte field_149070_d;
		protected internal sbyte field_149071_e;
		protected internal sbyte field_149068_f;
		protected internal bool field_149069_g;
		

		public S14PacketEntity()
		{
		}

		public S14PacketEntity(int p_i45206_1_)
		{
			this.field_149074_a = p_i45206_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149074_a = p_148837_1_.readInt();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149074_a);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityMovement(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}", new object[] {Convert.ToInt32(this.field_149074_a)});
		}

		public override string ToString()
		{
			return "Entity_" + base.ToString();
		}

		public virtual Entity func_149065_a(World p_149065_1_)
		{
			return p_149065_1_.getEntityByID(this.field_149074_a);
		}

		public virtual sbyte func_149062_c()
		{
			return this.field_149072_b;
		}

		public virtual sbyte func_149061_d()
		{
			return this.field_149073_c;
		}

		public virtual sbyte func_149064_e()
		{
			return this.field_149070_d;
		}

		public virtual sbyte func_149066_f()
		{
			return this.field_149071_e;
		}

		public virtual sbyte func_149063_g()
		{
			return this.field_149068_f;
		}

		public virtual bool func_149060_h()
		{
			return this.field_149069_g;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}

		public class S15PacketEntityRelMove : S14PacketEntity
		{
			

			public S15PacketEntityRelMove()
			{
			}

			public S15PacketEntityRelMove(int p_i45203_1_, sbyte p_i45203_2_, sbyte p_i45203_3_, sbyte p_i45203_4_) : base(p_i45203_1_)
			{
				this.field_149072_b = p_i45203_2_;
				this.field_149073_c = p_i45203_3_;
				this.field_149070_d = p_i45203_4_;
			}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
			public override void readPacketData(PacketBuffer p_148837_1_)
			{
				base.readPacketData(p_148837_1_);
				this.field_149072_b = p_148837_1_.readByte();
				this.field_149073_c = p_148837_1_.readByte();
				this.field_149070_d = p_148837_1_.readByte();
			}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
			public override void writePacketData(PacketBuffer p_148840_1_)
			{
				base.writePacketData(p_148840_1_);
				p_148840_1_.writeByte(this.field_149072_b);
				p_148840_1_.writeByte(this.field_149073_c);
				p_148840_1_.writeByte(this.field_149070_d);
			}

			public override string serialize()
			{
				return base.serialize() + string.Format(", xa={0:D}, ya={1:D}, za={2:D}", new object[] {Convert.ToByte(this.field_149072_b), Convert.ToByte(this.field_149073_c), Convert.ToByte(this.field_149070_d)});
			}

			public override void processPacket(INetHandler p_148833_1_)
			{
				base.processPacket((INetHandlerPlayClient)p_148833_1_);
			}
		}

		public class S16PacketEntityLook : S14PacketEntity
		{
			

			public S16PacketEntityLook()
			{
				this.field_149069_g = true;
			}

			public S16PacketEntityLook(int p_i45205_1_, sbyte p_i45205_2_, sbyte p_i45205_3_) : base(p_i45205_1_)
			{
				this.field_149071_e = p_i45205_2_;
				this.field_149068_f = p_i45205_3_;
				this.field_149069_g = true;
			}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
			public override void readPacketData(PacketBuffer p_148837_1_)
			{
				base.readPacketData(p_148837_1_);
				this.field_149071_e = p_148837_1_.readByte();
				this.field_149068_f = p_148837_1_.readByte();
			}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
			public override void writePacketData(PacketBuffer p_148840_1_)
			{
				base.writePacketData(p_148840_1_);
				p_148840_1_.writeByte(this.field_149071_e);
				p_148840_1_.writeByte(this.field_149068_f);
			}

			public override string serialize()
			{
				return base.serialize() + string.Format(", yRot={0:D}, xRot={1:D}", new object[] {Convert.ToByte(this.field_149071_e), Convert.ToByte(this.field_149068_f)});
			}

			public override void processPacket(INetHandler p_148833_1_)
			{
				base.processPacket((INetHandlerPlayClient)p_148833_1_);
			}
		}

		public class S17PacketEntityLookMove : S14PacketEntity
		{
			

			public S17PacketEntityLookMove()
			{
				this.field_149069_g = true;
			}

			public S17PacketEntityLookMove(int p_i45204_1_, sbyte p_i45204_2_, sbyte p_i45204_3_, sbyte p_i45204_4_, sbyte p_i45204_5_, sbyte p_i45204_6_) : base(p_i45204_1_)
			{
				this.field_149072_b = p_i45204_2_;
				this.field_149073_c = p_i45204_3_;
				this.field_149070_d = p_i45204_4_;
				this.field_149071_e = p_i45204_5_;
				this.field_149068_f = p_i45204_6_;
				this.field_149069_g = true;
			}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
			public override void readPacketData(PacketBuffer p_148837_1_)
			{
				base.readPacketData(p_148837_1_);
				this.field_149072_b = p_148837_1_.readByte();
				this.field_149073_c = p_148837_1_.readByte();
				this.field_149070_d = p_148837_1_.readByte();
				this.field_149071_e = p_148837_1_.readByte();
				this.field_149068_f = p_148837_1_.readByte();
			}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
			public override void writePacketData(PacketBuffer p_148840_1_)
			{
				base.writePacketData(p_148840_1_);
				p_148840_1_.writeByte(this.field_149072_b);
				p_148840_1_.writeByte(this.field_149073_c);
				p_148840_1_.writeByte(this.field_149070_d);
				p_148840_1_.writeByte(this.field_149071_e);
				p_148840_1_.writeByte(this.field_149068_f);
			}

			public override string serialize()
			{
				return base.serialize() + string.Format(", xa={0:D}, ya={1:D}, za={2:D}, yRot={3:D}, xRot={4:D}", new object[] {Convert.ToByte(this.field_149072_b), Convert.ToByte(this.field_149073_c), Convert.ToByte(this.field_149070_d), Convert.ToByte(this.field_149071_e), Convert.ToByte(this.field_149068_f)});
			}

			public override void processPacket(INetHandler p_148833_1_)
			{
				base.processPacket((INetHandlerPlayClient)p_148833_1_);
			}
		}
	}

}