using System;

namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.item.Item;

	public class BlockClay : Block
	{
		private const string __OBFID = "CL_00000215";

		public BlockClay() : base(Material.field_151571_B)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.clay_ball;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 4;
		}
	}

}