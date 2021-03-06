using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nItem;
using DotCraftCore.nTileEntity;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockMobSpawner : BlockContainer
	{
		protected internal BlockMobSpawner() : base(Material.rock)
		{
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public override TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityMobSpawner();
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return null;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public override void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			base.dropBlockAsItemWithChance(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, p_149690_5_, p_149690_6_, p_149690_7_);
			int var8 = 15 + p_149690_1_.rand.Next(15) + p_149690_1_.rand.Next(15);
			this.dropXpOnBlockBreak(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, var8);
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public override Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemById(0);
		}
	}

}