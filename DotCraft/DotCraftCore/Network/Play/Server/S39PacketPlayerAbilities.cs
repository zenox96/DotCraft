using System;

namespace DotCraftCore.Network.Play.Server
{

	using PlayerCapabilities = DotCraftCore.entity.player.PlayerCapabilities;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S39PacketPlayerAbilities : Packet
	{
		private bool field_149119_a;
		private bool field_149117_b;
		private bool field_149118_c;
		private bool field_149115_d;
		private float field_149116_e;
		private float field_149114_f;
		

		public S39PacketPlayerAbilities()
		{
		}

		public S39PacketPlayerAbilities(PlayerCapabilities p_i45208_1_)
		{
			this.func_149108_a(p_i45208_1_.disableDamage);
			this.func_149102_b(p_i45208_1_.isFlying);
			this.func_149109_c(p_i45208_1_.allowFlying);
			this.func_149111_d(p_i45208_1_.isCreativeMode);
			this.func_149104_a(p_i45208_1_.FlySpeed);
			this.func_149110_b(p_i45208_1_.WalkSpeed);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			sbyte var2 = p_148837_1_.readByte();
			this.func_149108_a((var2 & 1) > 0);
			this.func_149102_b((var2 & 2) > 0);
			this.func_149109_c((var2 & 4) > 0);
			this.func_149111_d((var2 & 8) > 0);
			this.func_149104_a(p_148837_1_.readFloat());
			this.func_149110_b(p_148837_1_.readFloat());
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			sbyte var2 = 0;

			if (this.func_149112_c())
			{
				var2 = (sbyte)(var2 | 1);
			}

			if (this.func_149106_d())
			{
				var2 = (sbyte)(var2 | 2);
			}

			if (this.func_149105_e())
			{
				var2 = (sbyte)(var2 | 4);
			}

			if (this.func_149103_f())
			{
				var2 = (sbyte)(var2 | 8);
			}

			p_148840_1_.writeByte(var2);
			p_148840_1_.writeFloat(this.field_149116_e);
			p_148840_1_.writeFloat(this.field_149114_f);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handlePlayerAbilities(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: return String.format("invuln=%b, flying=%b, canfly=%b, instabuild=%b, flyspeed=%.4f, walkspped=%.4f", new Object[] {Boolean.valueOf(this.func_149112_c()), Boolean.valueOf(this.func_149106_d()), Boolean.valueOf(this.func_149105_e()), Boolean.valueOf(this.func_149103_f()), Float.valueOf(this.func_149101_g()), Float.valueOf(this.func_149107_h())});
			return string.Format("invuln=%b, flying=%b, canfly=%b, instabuild=%b, flyspeed=%.4f, walkspped=%.4f", new object[] {Convert.ToBoolean(this.func_149112_c()), Convert.ToBoolean(this.func_149106_d()), Convert.ToBoolean(this.func_149105_e()), Convert.ToBoolean(this.func_149103_f()), Convert.ToSingle(this.func_149101_g()), Convert.ToSingle(this.func_149107_h())});
		}

		public virtual bool func_149112_c()
		{
			return this.field_149119_a;
		}

		public virtual void func_149108_a(bool p_149108_1_)
		{
			this.field_149119_a = p_149108_1_;
		}

		public virtual bool func_149106_d()
		{
			return this.field_149117_b;
		}

		public virtual void func_149102_b(bool p_149102_1_)
		{
			this.field_149117_b = p_149102_1_;
		}

		public virtual bool func_149105_e()
		{
			return this.field_149118_c;
		}

		public virtual void func_149109_c(bool p_149109_1_)
		{
			this.field_149118_c = p_149109_1_;
		}

		public virtual bool func_149103_f()
		{
			return this.field_149115_d;
		}

		public virtual void func_149111_d(bool p_149111_1_)
		{
			this.field_149115_d = p_149111_1_;
		}

		public virtual float func_149101_g()
		{
			return this.field_149116_e;
		}

		public virtual void func_149104_a(float p_149104_1_)
		{
			this.field_149116_e = p_149104_1_;
		}

		public virtual float func_149107_h()
		{
			return this.field_149114_f;
		}

		public virtual void func_149110_b(float p_149110_1_)
		{
			this.field_149114_f = p_149110_1_;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}