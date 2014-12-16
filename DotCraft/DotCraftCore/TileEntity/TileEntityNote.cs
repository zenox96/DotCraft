namespace DotCraftCore.TileEntity
{

	using Material = DotCraftCore.block.material.Material;
	using Blocks = DotCraftCore.init.Blocks;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using World = DotCraftCore.World.World;

	public class TileEntityNote : TileEntity
	{
		public sbyte field_145879_a;
		public bool field_145880_i;
		

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			p_145841_1_.setByte("note", this.field_145879_a);
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			this.field_145879_a = p_145839_1_.getByte("note");

			if(this.field_145879_a < 0)
			{
				this.field_145879_a = 0;
			}

			if(this.field_145879_a > 24)
			{
				this.field_145879_a = 24;
			}
		}

		public virtual void func_145877_a()
		{
			this.field_145879_a = (sbyte)((this.field_145879_a + 1) % 25);
			this.onInventoryChanged();
		}

		public virtual void func_145878_a(World p_145878_1_, int p_145878_2_, int p_145878_3_, int p_145878_4_)
		{
			if(p_145878_1_.getBlock(p_145878_2_, p_145878_3_ + 1, p_145878_4_).Material == Material.air)
			{
				Material var5 = p_145878_1_.getBlock(p_145878_2_, p_145878_3_ - 1, p_145878_4_).Material;
				sbyte var6 = 0;

				if(var5 == Material.rock)
				{
					var6 = 1;
				}

				if(var5 == Material.sand)
				{
					var6 = 2;
				}

				if(var5 == Material.glass)
				{
					var6 = 3;
				}

				if(var5 == Material.wood)
				{
					var6 = 4;
				}

				p_145878_1_.func_147452_c(p_145878_2_, p_145878_3_, p_145878_4_, Blocks.noteblock, var6, this.field_145879_a);
			}
		}
	}

}