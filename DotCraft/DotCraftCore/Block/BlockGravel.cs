using System;

namespace DotCraftCore.Block
{

	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;

	public class BlockGravel : BlockFalling
	{
		

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			if (p_149650_3_ > 3)
			{
				p_149650_3_ = 3;
			}

			return p_149650_2_.Next(10 - p_149650_3_ * 3) == 0 ? Items.flint : Item.getItemFromBlock(this);
		}
	}

}