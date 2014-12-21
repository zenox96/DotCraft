using DotCraftCore.nInit;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
namespace DotCraftCore.nBlock
{
	public class BlockPotato : BlockCrops
	{
		protected internal override Item func_149866_i()
		{
			return Items.potato;
		}

		protected internal override Item func_149865_P()
		{
			return Items.potato;
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public override void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			base.dropBlockAsItemWithChance(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, p_149690_5_, p_149690_6_, p_149690_7_);

			if (!p_149690_1_.isClient)
			{
				if (p_149690_5_ >= 7 && p_149690_1_.rand.Next(50) == 0)
				{
					this.dropBlockAsItem_do(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, new ItemStack(Items.poisonous_potato));
				}
			}
		}
	}
}