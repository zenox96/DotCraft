using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockDirt : Block
	{
		public static readonly string[] field_150009_a = new string[] {"default", "default", "podzol"};

		protected internal BlockDirt() : base(Material.ground)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return 0;
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
		{
			if (p_149644_1_ == 1)
			{
				p_149644_1_ = 0;
			}

			return base.createStackedBlock(p_149644_1_);
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(this, 1, 0));
			p_149666_3_.Add(new ItemStack(this, 1, 2));
		}

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public virtual int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			int var5 = p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_, p_149643_4_);

			if (var5 == 1)
			{
				var5 = 0;
			}

			return var5;
		}
	}

}