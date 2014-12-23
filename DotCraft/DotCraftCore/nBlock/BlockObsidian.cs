using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nItem;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockObsidian : BlockStone
	{

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 1;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.obsidian);
		}

		public override MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151654_J;
		}
	}

}