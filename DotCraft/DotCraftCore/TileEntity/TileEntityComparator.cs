namespace DotCraftCore.TileEntity
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;

	public class TileEntityComparator : TileEntity
	{
		private int field_145997_a;
		

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			p_145841_1_.setInteger("OutputSignal", this.field_145997_a);
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			this.field_145997_a = p_145839_1_.getInteger("OutputSignal");
		}

		public virtual int func_145996_a()
		{
			return this.field_145997_a;
		}

		public virtual void func_145995_a(int p_145995_1_)
		{
			this.field_145997_a = p_145995_1_;
		}
	}

}