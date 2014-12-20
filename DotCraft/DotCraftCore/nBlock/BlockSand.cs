using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockSand : BlockFalling
	{
		public static readonly string[] field_149838_a = new string[] {"default", "red"};
///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return p_149728_1_ == 1 ? MapColor.field_151664_l : MapColor.field_151658_d;
		}
	}

}