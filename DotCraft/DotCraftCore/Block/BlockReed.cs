using System;

namespace DotCraftCore.Block
{

	
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockReed : Block
	{
		private const string __OBFID = "CL_00000300";

		protected internal BlockReed() : base(Material.plants)
		{
			float var1 = 0.375F;
			this.setBlockBounds(0.5F - var1, 0.0F, 0.5F - var1, 0.5F + var1, 1.0F, 0.5F + var1);
			this.TickRandomly = true;
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (p_149674_1_.getBlock(p_149674_2_, p_149674_3_ - 1, p_149674_4_) == Blocks.reeds || this.func_150170_e(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_))
			{
				if (p_149674_1_.isAirBlock(p_149674_2_, p_149674_3_ + 1, p_149674_4_))
				{
					int var6;

					for (var6 = 1; p_149674_1_.getBlock(p_149674_2_, p_149674_3_ - var6, p_149674_4_) == this; ++var6)
					{
						;
					}

					if (var6 < 3)
					{
						int var7 = p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_);

						if (var7 == 15)
						{
							p_149674_1_.setBlock(p_149674_2_, p_149674_3_ + 1, p_149674_4_, this);
							p_149674_1_.setBlockMetadataWithNotify(p_149674_2_, p_149674_3_, p_149674_4_, 0, 4);
						}
						else
						{
							p_149674_1_.setBlockMetadataWithNotify(p_149674_2_, p_149674_3_, p_149674_4_, var7 + 1, 4);
						}
					}
				}
			}
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			Block var5 = p_149742_1_.getBlock(p_149742_2_, p_149742_3_ - 1, p_149742_4_);
			return var5 == this ? true : (var5 != Blocks.grass && var5 != Blocks.dirt && var5 != Blocks.sand ? false : (p_149742_1_.getBlock(p_149742_2_ - 1, p_149742_3_ - 1, p_149742_4_).Material == Material.water ? true : (p_149742_1_.getBlock(p_149742_2_ + 1, p_149742_3_ - 1, p_149742_4_).Material == Material.water ? true : (p_149742_1_.getBlock(p_149742_2_, p_149742_3_ - 1, p_149742_4_ - 1).Material == Material.water ? true : p_149742_1_.getBlock(p_149742_2_, p_149742_3_ - 1, p_149742_4_ + 1).Material == Material.water))));
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			this.func_150170_e(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_);
		}

		protected internal bool func_150170_e(World p_150170_1_, int p_150170_2_, int p_150170_3_, int p_150170_4_)
		{
			if (!this.canBlockStay(p_150170_1_, p_150170_2_, p_150170_3_, p_150170_4_))
			{
				this.dropBlockAsItem(p_150170_1_, p_150170_2_, p_150170_3_, p_150170_4_, p_150170_1_.getBlockMetadata(p_150170_2_, p_150170_3_, p_150170_4_), 0);
				p_150170_1_.setBlockToAir(p_150170_2_, p_150170_3_, p_150170_4_);
				return false;
			}
			else
			{
				return true;
			}
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public virtual bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			return this.canPlaceBlockAt(p_149718_1_, p_149718_2_, p_149718_3_, p_149718_4_);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			return null;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.reeds;
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 1;
			}
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Items.reeds;
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public virtual int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			return p_149720_1_.getBiomeGenForCoords(p_149720_2_, p_149720_4_).getBiomeGrassColor(p_149720_2_, p_149720_3_, p_149720_4_);
		}
	}

}