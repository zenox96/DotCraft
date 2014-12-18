using System;

namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using Entity = DotCraftCore.nEntity.Entity;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Item = DotCraftCore.nItem.Item;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using World = DotCraftCore.nWorld.World;

	public class BlockFarmland : Block
	{
		private IIcon field_149824_a;
		private IIcon field_149823_b;
		

		protected internal BlockFarmland() : base(Material.ground)
		{
			this.TickRandomly = true;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.9375F, 1.0F);
			this.LightOpacity = 255;
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			return AxisAlignedBB.getBoundingBox((double)(p_149668_2_ + 0), (double)(p_149668_3_ + 0), (double)(p_149668_4_ + 0), (double)(p_149668_2_ + 1), (double)(p_149668_3_ + 1), (double)(p_149668_4_ + 1));
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
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 1 ? (p_149691_2_ > 0 ? this.field_149824_a : this.field_149823_b) : Blocks.dirt.getBlockTextureFromSide(p_149691_1_);
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!this.func_149821_m(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_) && !p_149674_1_.canLightningStrikeAt(p_149674_2_, p_149674_3_ + 1, p_149674_4_))
			{
				int var6 = p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_);

				if (var6 > 0)
				{
					p_149674_1_.setBlockMetadataWithNotify(p_149674_2_, p_149674_3_, p_149674_4_, var6 - 1, 2);
				}
				else if (!this.func_149822_e(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_))
				{
					p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, Blocks.dirt);
				}
			}
			else
			{
				p_149674_1_.setBlockMetadataWithNotify(p_149674_2_, p_149674_3_, p_149674_4_, 7, 2);
			}
		}

///    
///     <summary> * Block's chance to react to an entity falling on it. </summary>
///     
		public virtual void onFallenUpon(World p_149746_1_, int p_149746_2_, int p_149746_3_, int p_149746_4_, Entity p_149746_5_, float p_149746_6_)
		{
			if (!p_149746_1_.isClient && p_149746_1_.rand.nextFloat() < p_149746_6_ - 0.5F)
			{
				if (!(p_149746_5_ is EntityPlayer) && !p_149746_1_.GameRules.getGameRuleBooleanValue("mobGriefing"))
				{
					return;
				}

				p_149746_1_.setBlock(p_149746_2_, p_149746_3_, p_149746_4_, Blocks.dirt);
			}
		}

		private bool func_149822_e(World p_149822_1_, int p_149822_2_, int p_149822_3_, int p_149822_4_)
		{
			sbyte var5 = 0;

			for (int var6 = p_149822_2_ - var5; var6 <= p_149822_2_ + var5; ++var6)
			{
				for (int var7 = p_149822_4_ - var5; var7 <= p_149822_4_ + var5; ++var7)
				{
					Block var8 = p_149822_1_.getBlock(var6, p_149822_3_ + 1, var7);

					if (var8 == Blocks.wheat || var8 == Blocks.melon_stem || var8 == Blocks.pumpkin_stem || var8 == Blocks.potatoes || var8 == Blocks.carrots)
					{
						return true;
					}
				}
			}

			return false;
		}

		private bool func_149821_m(World p_149821_1_, int p_149821_2_, int p_149821_3_, int p_149821_4_)
		{
			for (int var5 = p_149821_2_ - 4; var5 <= p_149821_2_ + 4; ++var5)
			{
				for (int var6 = p_149821_3_; var6 <= p_149821_3_ + 1; ++var6)
				{
					for (int var7 = p_149821_4_ - 4; var7 <= p_149821_4_ + 4; ++var7)
					{
						if (p_149821_1_.getBlock(var5, var6, var7).Material == Material.water)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			base.onNeighborBlockChange(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_5_);
			Material var6 = p_149695_1_.getBlock(p_149695_2_, p_149695_3_ + 1, p_149695_4_).Material;

			if (var6.Solid)
			{
				p_149695_1_.setBlock(p_149695_2_, p_149695_3_, p_149695_4_, Blocks.dirt);
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Blocks.dirt.getItemDropped(0, p_149650_2_, p_149650_3_);
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemFromBlock(Blocks.dirt);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149824_a = p_149651_1_.registerIcon(this.TextureName + "_wet");
			this.field_149823_b = p_149651_1_.registerIcon(this.TextureName + "_dry");
		}
	}

}