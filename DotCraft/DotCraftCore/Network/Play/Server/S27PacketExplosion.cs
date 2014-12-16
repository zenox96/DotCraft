using System.Collections;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using Vec3 = DotCraftCore.Util.Vec3;
	using ChunkPosition = DotCraftCore.World.ChunkPosition;

	public class S27PacketExplosion : Packet
	{
		private double field_149158_a;
		private double field_149156_b;
		private double field_149157_c;
		private float field_149154_d;
		private IList field_149155_e;
		private float field_149152_f;
		private float field_149153_g;
		private float field_149159_h;
		

		public S27PacketExplosion()
		{
		}

		public S27PacketExplosion(double p_i45193_1_, double p_i45193_3_, double p_i45193_5_, float p_i45193_7_, IList p_i45193_8_, Vec3 p_i45193_9_)
		{
			this.field_149158_a = p_i45193_1_;
			this.field_149156_b = p_i45193_3_;
			this.field_149157_c = p_i45193_5_;
			this.field_149154_d = p_i45193_7_;
			this.field_149155_e = new ArrayList(p_i45193_8_);

			if (p_i45193_9_ != null)
			{
				this.field_149152_f = (float)p_i45193_9_.xCoord;
				this.field_149153_g = (float)p_i45193_9_.yCoord;
				this.field_149159_h = (float)p_i45193_9_.zCoord;
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149158_a = (double)p_148837_1_.readFloat();
			this.field_149156_b = (double)p_148837_1_.readFloat();
			this.field_149157_c = (double)p_148837_1_.readFloat();
			this.field_149154_d = p_148837_1_.readFloat();
			int var2 = p_148837_1_.readInt();
			this.field_149155_e = new ArrayList(var2);
			int var3 = (int)this.field_149158_a;
			int var4 = (int)this.field_149156_b;
			int var5 = (int)this.field_149157_c;

			for (int var6 = 0; var6 < var2; ++var6)
			{
				int var7 = p_148837_1_.readByte() + var3;
				int var8 = p_148837_1_.readByte() + var4;
				int var9 = p_148837_1_.readByte() + var5;
				this.field_149155_e.Add(new ChunkPosition(var7, var8, var9));
			}

			this.field_149152_f = p_148837_1_.readFloat();
			this.field_149153_g = p_148837_1_.readFloat();
			this.field_149159_h = p_148837_1_.readFloat();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeFloat((float)this.field_149158_a);
			p_148840_1_.writeFloat((float)this.field_149156_b);
			p_148840_1_.writeFloat((float)this.field_149157_c);
			p_148840_1_.writeFloat(this.field_149154_d);
			p_148840_1_.writeInt(this.field_149155_e.Count);
			int var2 = (int)this.field_149158_a;
			int var3 = (int)this.field_149156_b;
			int var4 = (int)this.field_149157_c;
			IEnumerator var5 = this.field_149155_e.GetEnumerator();

			while (var5.MoveNext())
			{
				ChunkPosition var6 = (ChunkPosition)var5.Current;
				int var7 = var6.field_151329_a - var2;
				int var8 = var6.field_151327_b - var3;
				int var9 = var6.field_151328_c - var4;
				p_148840_1_.writeByte(var7);
				p_148840_1_.writeByte(var8);
				p_148840_1_.writeByte(var9);
			}

			p_148840_1_.writeFloat(this.field_149152_f);
			p_148840_1_.writeFloat(this.field_149153_g);
			p_148840_1_.writeFloat(this.field_149159_h);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleExplosion(this);
		}

		public virtual float func_149149_c()
		{
			return this.field_149152_f;
		}

		public virtual float func_149144_d()
		{
			return this.field_149153_g;
		}

		public virtual float func_149147_e()
		{
			return this.field_149159_h;
		}

		public virtual double func_149148_f()
		{
			return this.field_149158_a;
		}

		public virtual double func_149143_g()
		{
			return this.field_149156_b;
		}

		public virtual double func_149145_h()
		{
			return this.field_149157_c;
		}

		public virtual float func_149146_i()
		{
			return this.field_149154_d;
		}

		public virtual IList func_149150_j()
		{
			return this.field_149155_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}