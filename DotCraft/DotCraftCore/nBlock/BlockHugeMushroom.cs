using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockHugeMushroom : Block
	{
		private static readonly string[] field_149793_a = new string[] {"skin_brown", "skin_red"};
		private readonly int field_149792_b;

		public BlockHugeMushroom(Material p_i45412_1_, int p_i45412_2_) : base(p_i45412_1_)
		{
			this.field_149792_b = p_i45412_2_;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			int var2 = p_149745_1_.Next(10) - 7;

			if (var2 < 0)
			{
				var2 = 0;
			}

			return var2;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemById(Block.getIdFromBlock(Blocks.brown_mushroom) + this.field_149792_b);
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public override Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemById(Block.getIdFromBlock(Blocks.brown_mushroom) + this.field_149792_b);
		}
	}
}