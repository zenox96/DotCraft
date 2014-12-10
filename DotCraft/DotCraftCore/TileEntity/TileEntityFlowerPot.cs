namespace DotCraftCore.TileEntity
{

	using Item = DotCraftCore.item.Item;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using Packet = DotCraftCore.network.Packet;
	using S35PacketUpdateTileEntity = DotCraftCore.network.play.server.S35PacketUpdateTileEntity;

	public class TileEntityFlowerPot : TileEntity
	{
		private Item field_145967_a;
		private int field_145968_i;
		private const string __OBFID = "CL_00000356";

		public TileEntityFlowerPot()
		{
		}

		public TileEntityFlowerPot(Item p_i45442_1_, int p_i45442_2_)
		{
			this.field_145967_a = p_i45442_1_;
			this.field_145968_i = p_i45442_2_;
		}

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			p_145841_1_.setInteger("Item", Item.getIdFromItem(this.field_145967_a));
			p_145841_1_.setInteger("Data", this.field_145968_i);
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			this.field_145967_a = Item.getItemById(p_145839_1_.getInteger("Item"));
			this.field_145968_i = p_145839_1_.getInteger("Data");
		}

///    
///     <summary> * Overriden in a sign to provide the text. </summary>
///     
		public override Packet DescriptionPacket
		{
			get
			{
				NBTTagCompound var1 = new NBTTagCompound();
				this.writeToNBT(var1);
				return new S35PacketUpdateTileEntity(this.field_145851_c, this.field_145848_d, this.field_145849_e, 5, var1);
			}
		}

		public virtual void func_145964_a(Item p_145964_1_, int p_145964_2_)
		{
			this.field_145967_a = p_145964_1_;
			this.field_145968_i = p_145964_2_;
		}

		public virtual Item func_145965_a()
		{
			return this.field_145967_a;
		}

		public virtual int func_145966_b()
		{
			return this.field_145968_i;
		}
	}

}