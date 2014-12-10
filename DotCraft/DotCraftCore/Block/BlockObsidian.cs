using System;

namespace DotCraftCore.Block
{

	using MapColor = DotCraftCore.Block.material.MapColor;
	using Blocks = DotCraftCore.init.Blocks;
	using Item = DotCraftCore.item.Item;

	public class BlockObsidian : BlockStone
	{
		private const string __OBFID = "CL_00000279";

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 1;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.obsidian);
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151654_J;
		}
	}

}