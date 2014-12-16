using System;

namespace DotCraftCore.Network.Play.Client
{

	using PlayerCapabilities = DotCraftCore.entity.player.PlayerCapabilities;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.Network.Play.INetHandlerPlayServer;

	public class C13PacketPlayerAbilities : Packet
	{
		private bool field_149500_a;
		private bool field_149498_b;
		private bool field_149499_c;
		private bool field_149496_d;
		private float field_149497_e;
		private float field_149495_f;
		

		public C13PacketPlayerAbilities()
		{
		}

		public C13PacketPlayerAbilities(PlayerCapabilities p_i45257_1_)
		{
			this.func_149490_a(p_i45257_1_.disableDamage);
			this.func_149483_b(p_i45257_1_.isFlying);
			this.func_149491_c(p_i45257_1_.allowFlying);
			this.func_149493_d(p_i45257_1_.isCreativeMode);
			this.func_149485_a(p_i45257_1_.FlySpeed);
			this.func_149492_b(p_i45257_1_.WalkSpeed);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			sbyte var2 = p_148837_1_.readByte();
			this.func_149490_a((var2 & 1) > 0);
			this.func_149483_b((var2 & 2) > 0);
			this.func_149491_c((var2 & 4) > 0);
			this.func_149493_d((var2 & 8) > 0);
			this.func_149485_a(p_148837_1_.readFloat());
			this.func_149492_b(p_148837_1_.readFloat());
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			sbyte var2 = 0;

			if (this.func_149494_c())
			{
				var2 = (sbyte)(var2 | 1);
			}

			if (this.func_149488_d())
			{
				var2 = (sbyte)(var2 | 2);
			}

			if (this.func_149486_e())
			{
				var2 = (sbyte)(var2 | 4);
			}

			if (this.func_149484_f())
			{
				var2 = (sbyte)(var2 | 8);
			}

			p_148840_1_.writeByte(var2);
			p_148840_1_.writeFloat(this.field_149497_e);
			p_148840_1_.writeFloat(this.field_149495_f);
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processPlayerAbilities(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: return String.format("invuln=%b, flying=%b, canfly=%b, instabuild=%b, flyspeed=%.4f, walkspped=%.4f", new Object[] {Boolean.valueOf(this.func_149494_c()), Boolean.valueOf(this.func_149488_d()), Boolean.valueOf(this.func_149486_e()), Boolean.valueOf(this.func_149484_f()), Float.valueOf(this.func_149482_g()), Float.valueOf(this.func_149489_h())});
			return string.Format("invuln=%b, flying=%b, canfly=%b, instabuild=%b, flyspeed=%.4f, walkspped=%.4f", new object[] {Convert.ToBoolean(this.func_149494_c()), Convert.ToBoolean(this.func_149488_d()), Convert.ToBoolean(this.func_149486_e()), Convert.ToBoolean(this.func_149484_f()), Convert.ToSingle(this.func_149482_g()), Convert.ToSingle(this.func_149489_h())});
		}

		public virtual bool func_149494_c()
		{
			return this.field_149500_a;
		}

		public virtual void func_149490_a(bool p_149490_1_)
		{
			this.field_149500_a = p_149490_1_;
		}

		public virtual bool func_149488_d()
		{
			return this.field_149498_b;
		}

		public virtual void func_149483_b(bool p_149483_1_)
		{
			this.field_149498_b = p_149483_1_;
		}

		public virtual bool func_149486_e()
		{
			return this.field_149499_c;
		}

		public virtual void func_149491_c(bool p_149491_1_)
		{
			this.field_149499_c = p_149491_1_;
		}

		public virtual bool func_149484_f()
		{
			return this.field_149496_d;
		}

		public virtual void func_149493_d(bool p_149493_1_)
		{
			this.field_149496_d = p_149493_1_;
		}

		public virtual float func_149482_g()
		{
			return this.field_149497_e;
		}

		public virtual void func_149485_a(float p_149485_1_)
		{
			this.field_149497_e = p_149485_1_;
		}

		public virtual float func_149489_h()
		{
			return this.field_149495_f;
		}

		public virtual void func_149492_b(float p_149492_1_)
		{
			this.field_149495_f = p_149492_1_;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}