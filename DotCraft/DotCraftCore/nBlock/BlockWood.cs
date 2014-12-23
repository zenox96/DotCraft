using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockWood : Block
	{
		public static readonly string[] field_150096_a = new string[] {"oak", "spruce", "birch", "jungle", "acacia", "big_oak"};

		public BlockWood() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public override int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public override void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 2));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 3));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 4));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 5));
		}
	}
}