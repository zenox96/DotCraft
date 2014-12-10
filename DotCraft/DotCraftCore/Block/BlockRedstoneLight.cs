using System;

namespace DotCraftCore.Block
{

	
	using Blocks = DotCraftCore.Init.Blocks;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using World = DotCraftCore.world.World;

	public class BlockRedstoneLight : Block
	{
		private readonly bool field_150171_a;
		private const string __OBFID = "CL_00000297";

		public BlockRedstoneLight(bool p_i45421_1_) : base(Material.redstoneLight)
		{
			this.field_150171_a = p_i45421_1_;

			if (p_i45421_1_)
			{
				this.LightLevel = 1.0F;
			}
		}

		public virtual void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			if (!p_149726_1_.isClient)
			{
				if (this.field_150171_a && !p_149726_1_.isBlockIndirectlyGettingPowered(p_149726_2_, p_149726_3_, p_149726_4_))
				{
					p_149726_1_.scheduleBlockUpdate(p_149726_2_, p_149726_3_, p_149726_4_, this, 4);
				}
				else if (!this.field_150171_a && p_149726_1_.isBlockIndirectlyGettingPowered(p_149726_2_, p_149726_3_, p_149726_4_))
				{
					p_149726_1_.setBlock(p_149726_2_, p_149726_3_, p_149726_4_, Blocks.lit_redstone_lamp, 0, 2);
				}
			}
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!p_149695_1_.isClient)
			{
				if (this.field_150171_a && !p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_))
				{
					p_149695_1_.scheduleBlockUpdate(p_149695_2_, p_149695_3_, p_149695_4_, this, 4);
				}
				else if (!this.field_150171_a && p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_))
				{
					p_149695_1_.setBlock(p_149695_2_, p_149695_3_, p_149695_4_, Blocks.lit_redstone_lamp, 0, 2);
				}
			}
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!p_149674_1_.isClient && this.field_150171_a && !p_149674_1_.isBlockIndirectlyGettingPowered(p_149674_2_, p_149674_3_, p_149674_4_))
			{
				p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, Blocks.redstone_lamp, 0, 2);
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.redstone_lamp);
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemFromBlock(Blocks.redstone_lamp);
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
		{
			return new ItemStack(Blocks.redstone_lamp);
		}
	}

}