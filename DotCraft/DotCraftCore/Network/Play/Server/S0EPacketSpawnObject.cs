using System;

namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;
	using MathHelper = DotCraftCore.nUtil.MathHelper;

	public class S0EPacketSpawnObject : Packet
	{
		private int field_149018_a;
		private int field_149016_b;
		private int field_149017_c;
		private int field_149014_d;
		private int field_149015_e;
		private int field_149012_f;
		private int field_149013_g;
		private int field_149021_h;
		private int field_149022_i;
		private int field_149019_j;
		private int field_149020_k;
		

		public S0EPacketSpawnObject()
		{
		}

		public S0EPacketSpawnObject(Entity p_i45165_1_, int p_i45165_2_) : this(p_i45165_1_, p_i45165_2_, 0)
		{
		}

		public S0EPacketSpawnObject(Entity p_i45166_1_, int p_i45166_2_, int p_i45166_3_)
		{
			this.field_149018_a = p_i45166_1_.EntityId;
			this.field_149016_b = MathHelper.floor_double(p_i45166_1_.posX * 32.0D);
			this.field_149017_c = MathHelper.floor_double(p_i45166_1_.posY * 32.0D);
			this.field_149014_d = MathHelper.floor_double(p_i45166_1_.posZ * 32.0D);
			this.field_149021_h = MathHelper.floor_float(p_i45166_1_.rotationPitch * 256.0F / 360.0F);
			this.field_149022_i = MathHelper.floor_float(p_i45166_1_.rotationYaw * 256.0F / 360.0F);
			this.field_149019_j = p_i45166_2_;
			this.field_149020_k = p_i45166_3_;

			if (p_i45166_3_ > 0)
			{
				double var4 = p_i45166_1_.motionX;
				double var6 = p_i45166_1_.motionY;
				double var8 = p_i45166_1_.motionZ;
				double var10 = 3.9D;

				if (var4 < -var10)
				{
					var4 = -var10;
				}

				if (var6 < -var10)
				{
					var6 = -var10;
				}

				if (var8 < -var10)
				{
					var8 = -var10;
				}

				if (var4 > var10)
				{
					var4 = var10;
				}

				if (var6 > var10)
				{
					var6 = var10;
				}

				if (var8 > var10)
				{
					var8 = var10;
				}

				this.field_149015_e = (int)(var4 * 8000.0D);
				this.field_149012_f = (int)(var6 * 8000.0D);
				this.field_149013_g = (int)(var8 * 8000.0D);
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149018_a = p_148837_1_.readVarIntFromBuffer();
			this.field_149019_j = p_148837_1_.readByte();
			this.field_149016_b = p_148837_1_.readInt();
			this.field_149017_c = p_148837_1_.readInt();
			this.field_149014_d = p_148837_1_.readInt();
			this.field_149021_h = p_148837_1_.readByte();
			this.field_149022_i = p_148837_1_.readByte();
			this.field_149020_k = p_148837_1_.readInt();

			if (this.field_149020_k > 0)
			{
				this.field_149015_e = p_148837_1_.readShort();
				this.field_149012_f = p_148837_1_.readShort();
				this.field_149013_g = p_148837_1_.readShort();
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_149018_a);
			p_148840_1_.writeByte(this.field_149019_j);
			p_148840_1_.writeInt(this.field_149016_b);
			p_148840_1_.writeInt(this.field_149017_c);
			p_148840_1_.writeInt(this.field_149014_d);
			p_148840_1_.writeByte(this.field_149021_h);
			p_148840_1_.writeByte(this.field_149022_i);
			p_148840_1_.writeInt(this.field_149020_k);

			if (this.field_149020_k > 0)
			{
				p_148840_1_.writeShort(this.field_149015_e);
				p_148840_1_.writeShort(this.field_149012_f);
				p_148840_1_.writeShort(this.field_149013_g);
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSpawnObject(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, type={1:D}, x={2:F2}, y={3:F2}, z={4:F2}", new object[] {Convert.ToInt32(this.field_149018_a), Convert.ToInt32(this.field_149019_j), Convert.ToSingle((float)this.field_149016_b / 32.0F), Convert.ToSingle((float)this.field_149017_c / 32.0F), Convert.ToSingle((float)this.field_149014_d / 32.0F)});
		}

		public virtual int func_149001_c()
		{
			return this.field_149018_a;
		}

		public virtual int func_148997_d()
		{
			return this.field_149016_b;
		}

		public virtual int func_148998_e()
		{
			return this.field_149017_c;
		}

		public virtual int func_148994_f()
		{
			return this.field_149014_d;
		}

		public virtual int func_149010_g()
		{
			return this.field_149015_e;
		}

		public virtual int func_149004_h()
		{
			return this.field_149012_f;
		}

		public virtual int func_148999_i()
		{
			return this.field_149013_g;
		}

		public virtual int func_149008_j()
		{
			return this.field_149021_h;
		}

		public virtual int func_149006_k()
		{
			return this.field_149022_i;
		}

		public virtual int func_148993_l()
		{
			return this.field_149019_j;
		}

		public virtual int func_149009_m()
		{
			return this.field_149020_k;
		}

		public virtual void func_148996_a(int p_148996_1_)
		{
			this.field_149016_b = p_148996_1_;
		}

		public virtual void func_148995_b(int p_148995_1_)
		{
			this.field_149017_c = p_148995_1_;
		}

		public virtual void func_149005_c(int p_149005_1_)
		{
			this.field_149014_d = p_149005_1_;
		}

		public virtual void func_149003_d(int p_149003_1_)
		{
			this.field_149015_e = p_149003_1_;
		}

		public virtual void func_149000_e(int p_149000_1_)
		{
			this.field_149012_f = p_149000_1_;
		}

		public virtual void func_149007_f(int p_149007_1_)
		{
			this.field_149013_g = p_149007_1_;
		}

		public virtual void func_149002_g(int p_149002_1_)
		{
			this.field_149020_k = p_149002_1_;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}