using System;

namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Entity = DotCraftCore.nEntity.Entity;
	using Blocks = DotCraftCore.nInit.Blocks;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using World = DotCraftCore.nWorld.World;

	public class BlockCactus : Block
	{
		private IIcon field_150041_a;
		private IIcon field_150040_b;
		

		protected internal BlockCactus() : base(Material.field_151570_A)
		{
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
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
						this.onNeighborBlockChange(p_149674_1_, p_149674_2_, p_149674_3_ + 1, p_149674_4_, this);
					}
					else
					{
						p_149674_1_.setBlockMetadataWithNotify(p_149674_2_, p_149674_3_, p_149674_4_, var7 + 1, 4);
					}
				}
			}
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			float var5 = 0.0625F;
			return AxisAlignedBB.getBoundingBox((double)((float)p_149668_2_ + var5), (double)p_149668_3_, (double)((float)p_149668_4_ + var5), (double)((float)(p_149668_2_ + 1) - var5), (double)((float)(p_149668_3_ + 1) - var5), (double)((float)(p_149668_4_ + 1) - var5));
		}

///    
///     <summary> * Returns the bounding box of the wired rectangular prism to render. </summary>
///     
		public virtual AxisAlignedBB getSelectedBoundingBoxFromPool(World p_149633_1_, int p_149633_2_, int p_149633_3_, int p_149633_4_)
		{
			float var5 = 0.0625F;
			return AxisAlignedBB.getBoundingBox((double)((float)p_149633_2_ + var5), (double)p_149633_3_, (double)((float)p_149633_4_ + var5), (double)((float)(p_149633_2_ + 1) - var5), (double)(p_149633_3_ + 1), (double)((float)(p_149633_4_ + 1) - var5));
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 1 ? this.field_150041_a : (p_149691_1_ == 0 ? this.field_150040_b : this.blockIcon);
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 13;
			}
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return !base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_) ? false : this.canBlockStay(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_);
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!this.canBlockStay(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_))
			{
				p_149695_1_.func_147480_a(p_149695_2_, p_149695_3_, p_149695_4_, true);
			}
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public virtual bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			if (p_149718_1_.getBlock(p_149718_2_ - 1, p_149718_3_, p_149718_4_).Material.Solid)
			{
				return false;
			}
			else if (p_149718_1_.getBlock(p_149718_2_ + 1, p_149718_3_, p_149718_4_).Material.Solid)
			{
				return false;
			}
			else if (p_149718_1_.getBlock(p_149718_2_, p_149718_3_, p_149718_4_ - 1).Material.Solid)
			{
				return false;
			}
			else if (p_149718_1_.getBlock(p_149718_2_, p_149718_3_, p_149718_4_ + 1).Material.Solid)
			{
				return false;
			}
			else
			{
				Block var5 = p_149718_1_.getBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_);
				return var5 == Blocks.cactus || var5 == Blocks.sand;
			}
		}

		public virtual void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
		{
			p_149670_5_.attackEntityFrom(DamageSource.cactus, 1.0F);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
			this.field_150041_a = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.field_150040_b = p_149651_1_.registerIcon(this.TextureName + "_bottom");
		}
	}

}