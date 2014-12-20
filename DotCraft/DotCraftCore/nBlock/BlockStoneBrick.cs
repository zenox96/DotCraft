using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockStoneBrick : Block
	{
		public static readonly string[] field_150142_a = new string[] {"default", "mossy", "cracked", "chiseled"};
		public static readonly string[] field_150141_b = new string[] {null, "mossy", "cracked", "carved"};

		public BlockStoneBrick() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 0; var4 < 4; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}
	}
}