using System;

namespace DotCraftCore.TileEntity
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using Packet = DotCraftCore.network.Packet;
	using S33PacketUpdateSign = DotCraftCore.network.play.server.S33PacketUpdateSign;

	public class TileEntitySign : TileEntity
	{
		public string[] field_145915_a = new string[] {"", "", "", ""};
		public int field_145918_i = -1;
		private bool field_145916_j = true;
		private EntityPlayer field_145917_k;
		private const string __OBFID = "CL_00000363";

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			p_145841_1_.setString("Text1", this.field_145915_a[0]);
			p_145841_1_.setString("Text2", this.field_145915_a[1]);
			p_145841_1_.setString("Text3", this.field_145915_a[2]);
			p_145841_1_.setString("Text4", this.field_145915_a[3]);
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			this.field_145916_j = false;
			base.readFromNBT(p_145839_1_);

			for(int var2 = 0; var2 < 4; ++var2)
			{
				this.field_145915_a[var2] = p_145839_1_.getString("Text" + (var2 + 1));

				if(this.field_145915_a[var2].Length > 15)
				{
					this.field_145915_a[var2] = this.field_145915_a[var2].Substring(0, 15);
				}
			}
		}

///    
///     <summary> * Overriden in a sign to provide the text. </summary>
///     
		public override Packet DescriptionPacket
		{
			get
			{
				string[] var1 = new string[4];
				Array.Copy(this.field_145915_a, 0, var1, 0, 4);
				return new S33PacketUpdateSign(this.field_145851_c, this.field_145848_d, this.field_145849_e, var1);
			}
		}

		public virtual bool func_145914_a()
		{
			return this.field_145916_j;
		}

		public virtual void func_145913_a(bool p_145913_1_)
		{
			this.field_145916_j = p_145913_1_;

			if(!p_145913_1_)
			{
				this.field_145917_k = null;
			}
		}

		public virtual void func_145912_a(EntityPlayer p_145912_1_)
		{
			this.field_145917_k = p_145912_1_;
		}

		public virtual EntityPlayer func_145911_b()
		{
			return this.field_145917_k;
		}
	}

}