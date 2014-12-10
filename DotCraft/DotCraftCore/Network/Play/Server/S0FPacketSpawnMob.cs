using System;
using System.Collections;

namespace DotCraftCore.Network.Play.Server
{

	using DataWatcher = DotCraftCore.entity.DataWatcher;
	using EntityList = DotCraftCore.entity.EntityList;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class S0FPacketSpawnMob : Packet
	{
		private int field_149042_a;
		private int field_149040_b;
		private int field_149041_c;
		private int field_149038_d;
		private int field_149039_e;
		private int field_149036_f;
		private int field_149037_g;
		private int field_149047_h;
		private sbyte field_149048_i;
		private sbyte field_149045_j;
		private sbyte field_149046_k;
		private DataWatcher field_149043_l;
		private IList field_149044_m;
		private const string __OBFID = "CL_00001279";

		public S0FPacketSpawnMob()
		{
		}

		public S0FPacketSpawnMob(EntityLivingBase p_i45192_1_)
		{
			this.field_149042_a = p_i45192_1_.EntityId;
			this.field_149040_b = (sbyte)EntityList.getEntityID(p_i45192_1_);
			this.field_149041_c = p_i45192_1_.myEntitySize.multiplyBy32AndRound(p_i45192_1_.posX);
			this.field_149038_d = MathHelper.floor_double(p_i45192_1_.posY * 32.0D);
			this.field_149039_e = p_i45192_1_.myEntitySize.multiplyBy32AndRound(p_i45192_1_.posZ);
			this.field_149048_i = (sbyte)((int)(p_i45192_1_.rotationYaw * 256.0F / 360.0F));
			this.field_149045_j = (sbyte)((int)(p_i45192_1_.rotationPitch * 256.0F / 360.0F));
			this.field_149046_k = (sbyte)((int)(p_i45192_1_.rotationYawHead * 256.0F / 360.0F));
			double var2 = 3.9D;
			double var4 = p_i45192_1_.motionX;
			double var6 = p_i45192_1_.motionY;
			double var8 = p_i45192_1_.motionZ;

			if (var4 < -var2)
			{
				var4 = -var2;
			}

			if (var6 < -var2)
			{
				var6 = -var2;
			}

			if (var8 < -var2)
			{
				var8 = -var2;
			}

			if (var4 > var2)
			{
				var4 = var2;
			}

			if (var6 > var2)
			{
				var6 = var2;
			}

			if (var8 > var2)
			{
				var8 = var2;
			}

			this.field_149036_f = (int)(var4 * 8000.0D);
			this.field_149037_g = (int)(var6 * 8000.0D);
			this.field_149047_h = (int)(var8 * 8000.0D);
			this.field_149043_l = p_i45192_1_.DataWatcher;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149042_a = p_148837_1_.readVarIntFromBuffer();
			this.field_149040_b = p_148837_1_.readByte() & 255;
			this.field_149041_c = p_148837_1_.readInt();
			this.field_149038_d = p_148837_1_.readInt();
			this.field_149039_e = p_148837_1_.readInt();
			this.field_149048_i = p_148837_1_.readByte();
			this.field_149045_j = p_148837_1_.readByte();
			this.field_149046_k = p_148837_1_.readByte();
			this.field_149036_f = p_148837_1_.readShort();
			this.field_149037_g = p_148837_1_.readShort();
			this.field_149047_h = p_148837_1_.readShort();
			this.field_149044_m = DataWatcher.readWatchedListFromPacketBuffer(p_148837_1_);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_149042_a);
			p_148840_1_.writeByte(this.field_149040_b & 255);
			p_148840_1_.writeInt(this.field_149041_c);
			p_148840_1_.writeInt(this.field_149038_d);
			p_148840_1_.writeInt(this.field_149039_e);
			p_148840_1_.writeByte(this.field_149048_i);
			p_148840_1_.writeByte(this.field_149045_j);
			p_148840_1_.writeByte(this.field_149046_k);
			p_148840_1_.writeShort(this.field_149036_f);
			p_148840_1_.writeShort(this.field_149037_g);
			p_148840_1_.writeShort(this.field_149047_h);
			this.field_149043_l.func_151509_a(p_148840_1_);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSpawnMob(this);
		}

		public virtual IList func_149027_c()
		{
			if (this.field_149044_m == null)
			{
				this.field_149044_m = this.field_149043_l.AllWatched;
			}

			return this.field_149044_m;
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, type={1:D}, x={2:F2}, y={3:F2}, z={4:F2}, xd={5:F2}, yd={6:F2}, zd={7:F2}", new object[] {Convert.ToInt32(this.field_149042_a), Convert.ToInt32(this.field_149040_b), Convert.ToSingle((float)this.field_149041_c / 32.0F), Convert.ToSingle((float)this.field_149038_d / 32.0F), Convert.ToSingle((float)this.field_149039_e / 32.0F), Convert.ToSingle((float)this.field_149036_f / 8000.0F), Convert.ToSingle((float)this.field_149037_g / 8000.0F), Convert.ToSingle((float)this.field_149047_h / 8000.0F)});
		}

		public virtual int func_149024_d()
		{
			return this.field_149042_a;
		}

		public virtual int func_149025_e()
		{
			return this.field_149040_b;
		}

		public virtual int func_149023_f()
		{
			return this.field_149041_c;
		}

		public virtual int func_149034_g()
		{
			return this.field_149038_d;
		}

		public virtual int func_149029_h()
		{
			return this.field_149039_e;
		}

		public virtual int func_149026_i()
		{
			return this.field_149036_f;
		}

		public virtual int func_149033_j()
		{
			return this.field_149037_g;
		}

		public virtual int func_149031_k()
		{
			return this.field_149047_h;
		}

		public virtual sbyte func_149028_l()
		{
			return this.field_149048_i;
		}

		public virtual sbyte func_149030_m()
		{
			return this.field_149045_j;
		}

		public virtual sbyte func_149032_n()
		{
			return this.field_149046_k;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}