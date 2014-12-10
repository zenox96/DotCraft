using System;

namespace DotCraftCore.Block
{

	
	using IconFlipped = DotCraftCore.client.renderer.IconFlipped;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.item.Item;
	using AxisAlignedBB = DotCraftCore.util.AxisAlignedBB;
	using IIcon = DotCraftCore.util.IIcon;
	using MovingObjectPosition = DotCraftCore.util.MovingObjectPosition;
	using Vec3 = DotCraftCore.util.Vec3;
	using IBlockAccess = DotCraftCore.world.IBlockAccess;
	using World = DotCraftCore.world.World;

	public class BlockDoor : Block
	{
		private IIcon[] field_150017_a;
		private IIcon[] field_150016_b;
		private const string __OBFID = "CL_00000230";

		protected internal BlockDoor(Material p_i45402_1_) : base(p_i45402_1_)
		{
			float var2 = 0.5F;
			float var3 = 1.0F;
			this.setBlockBounds(0.5F - var2, 0.0F, 0.5F - var2, 0.5F + var2, var3, 0.5F + var2);
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return this.field_150016_b[0];
		}

		public virtual IIcon getIcon(IBlockAccess p_149673_1_, int p_149673_2_, int p_149673_3_, int p_149673_4_, int p_149673_5_)
		{
			if (p_149673_5_ != 1 && p_149673_5_ != 0)
			{
				int var6 = this.func_150012_g(p_149673_1_, p_149673_2_, p_149673_3_, p_149673_4_);
				int var7 = var6 & 3;
				bool var8 = (var6 & 4) != 0;
				bool var9 = false;
				bool var10 = (var6 & 8) != 0;

				if (var8)
				{
					if (var7 == 0 && p_149673_5_ == 2)
					{
						var9 = !var9;
					}
					else if (var7 == 1 && p_149673_5_ == 5)
					{
						var9 = !var9;
					}
					else if (var7 == 2 && p_149673_5_ == 3)
					{
						var9 = !var9;
					}
					else if (var7 == 3 && p_149673_5_ == 4)
					{
						var9 = !var9;
					}
				}
				else
				{
					if (var7 == 0 && p_149673_5_ == 5)
					{
						var9 = !var9;
					}
					else if (var7 == 1 && p_149673_5_ == 3)
					{
						var9 = !var9;
					}
					else if (var7 == 2 && p_149673_5_ == 4)
					{
						var9 = !var9;
					}
					else if (var7 == 3 && p_149673_5_ == 2)
					{
						var9 = !var9;
					}

					if ((var6 & 16) != 0)
					{
						var9 = !var9;
					}
				}

				return var10 ? this.field_150017_a[var9 ? 1 : 0] : this.field_150016_b[var9 ? 1 : 0];
			}
			else
			{
				return this.field_150016_b[0];
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_150017_a = new IIcon[2];
			this.field_150016_b = new IIcon[2];
			this.field_150017_a[0] = p_149651_1_.registerIcon(this.TextureName + "_upper");
			this.field_150016_b[0] = p_149651_1_.registerIcon(this.TextureName + "_lower");
			this.field_150017_a[1] = new IconFlipped(this.field_150017_a[0], true, false);
			this.field_150016_b[1] = new IconFlipped(this.field_150016_b[0], true, false);
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

		public virtual bool getBlocksMovement(IBlockAccess p_149655_1_, int p_149655_2_, int p_149655_3_, int p_149655_4_)
		{
			int var5 = this.func_150012_g(p_149655_1_, p_149655_2_, p_149655_3_, p_149655_4_);
			return (var5 & 4) != 0;
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
				return 7;
			}
		}

///    
///     <summary> * Returns the bounding box of the wired rectangular prism to render. </summary>
///     
		public virtual AxisAlignedBB getSelectedBoundingBoxFromPool(World p_149633_1_, int p_149633_2_, int p_149633_3_, int p_149633_4_)
		{
			this.setBlockBoundsBasedOnState(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
			return base.getSelectedBoundingBoxFromPool(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			this.setBlockBoundsBasedOnState(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);
			return base.getCollisionBoundingBoxFromPool(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			this.func_150011_b(this.func_150012_g(p_149719_1_, p_149719_2_, p_149719_3_, p_149719_4_));
		}

		public virtual int func_150013_e(IBlockAccess p_150013_1_, int p_150013_2_, int p_150013_3_, int p_150013_4_)
		{
			return this.func_150012_g(p_150013_1_, p_150013_2_, p_150013_3_, p_150013_4_) & 3;
		}

		public virtual bool func_150015_f(IBlockAccess p_150015_1_, int p_150015_2_, int p_150015_3_, int p_150015_4_)
		{
			return (this.func_150012_g(p_150015_1_, p_150015_2_, p_150015_3_, p_150015_4_) & 4) != 0;
		}

		private void func_150011_b(int p_150011_1_)
		{
			float var2 = 0.1875F;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 2.0F, 1.0F);
			int var3 = p_150011_1_ & 3;
			bool var4 = (p_150011_1_ & 4) != 0;
			bool var5 = (p_150011_1_ & 16) != 0;

			if (var3 == 0)
			{
				if (var4)
				{
					if (!var5)
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, var2);
					}
					else
					{
						this.setBlockBounds(0.0F, 0.0F, 1.0F - var2, 1.0F, 1.0F, 1.0F);
					}
				}
				else
				{
					this.setBlockBounds(0.0F, 0.0F, 0.0F, var2, 1.0F, 1.0F);
				}
			}
			else if (var3 == 1)
			{
				if (var4)
				{
					if (!var5)
					{
						this.setBlockBounds(1.0F - var2, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
					}
					else
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, var2, 1.0F, 1.0F);
					}
				}
				else
				{
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, var2);
				}
			}
			else if (var3 == 2)
			{
				if (var4)
				{
					if (!var5)
					{
						this.setBlockBounds(0.0F, 0.0F, 1.0F - var2, 1.0F, 1.0F, 1.0F);
					}
					else
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, var2);
					}
				}
				else
				{
					this.setBlockBounds(1.0F - var2, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
				}
			}
			else if (var3 == 3)
			{
				if (var4)
				{
					if (!var5)
					{
						this.setBlockBounds(0.0F, 0.0F, 0.0F, var2, 1.0F, 1.0F);
					}
					else
					{
						this.setBlockBounds(1.0F - var2, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
					}
				}
				else
				{
					this.setBlockBounds(0.0F, 0.0F, 1.0F - var2, 1.0F, 1.0F, 1.0F);
				}
			}
		}

///    
///     <summary> * Called when a player hits the block. Args: world, x, y, z, player </summary>
///     
		public virtual void onBlockClicked(World p_149699_1_, int p_149699_2_, int p_149699_3_, int p_149699_4_, EntityPlayer p_149699_5_)
		{
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (this.blockMaterial == Material.iron)
			{
				return true;
			}
			else
			{
				int var10 = this.func_150012_g(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_);
				int var11 = var10 & 7;
				var11 ^= 4;

				if ((var10 & 8) == 0)
				{
					p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_, p_149727_4_, var11, 2);
					p_149727_1_.markBlockRangeForRenderUpdate(p_149727_2_, p_149727_3_, p_149727_4_, p_149727_2_, p_149727_3_, p_149727_4_);
				}
				else
				{
					p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_ - 1, p_149727_4_, var11, 2);
					p_149727_1_.markBlockRangeForRenderUpdate(p_149727_2_, p_149727_3_ - 1, p_149727_4_, p_149727_2_, p_149727_3_, p_149727_4_);
				}

				p_149727_1_.playAuxSFXAtEntity(p_149727_5_, 1003, p_149727_2_, p_149727_3_, p_149727_4_, 0);
				return true;
			}
		}

		public virtual void func_150014_a(World p_150014_1_, int p_150014_2_, int p_150014_3_, int p_150014_4_, bool p_150014_5_)
		{
			int var6 = this.func_150012_g(p_150014_1_, p_150014_2_, p_150014_3_, p_150014_4_);
			bool var7 = (var6 & 4) != 0;

			if (var7 != p_150014_5_)
			{
				int var8 = var6 & 7;
				var8 ^= 4;

				if ((var6 & 8) == 0)
				{
					p_150014_1_.setBlockMetadataWithNotify(p_150014_2_, p_150014_3_, p_150014_4_, var8, 2);
					p_150014_1_.markBlockRangeForRenderUpdate(p_150014_2_, p_150014_3_, p_150014_4_, p_150014_2_, p_150014_3_, p_150014_4_);
				}
				else
				{
					p_150014_1_.setBlockMetadataWithNotify(p_150014_2_, p_150014_3_ - 1, p_150014_4_, var8, 2);
					p_150014_1_.markBlockRangeForRenderUpdate(p_150014_2_, p_150014_3_ - 1, p_150014_4_, p_150014_2_, p_150014_3_, p_150014_4_);
				}

				p_150014_1_.playAuxSFXAtEntity((EntityPlayer)null, 1003, p_150014_2_, p_150014_3_, p_150014_4_, 0);
			}
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			int var6 = p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_);

			if ((var6 & 8) == 0)
			{
				bool var7 = false;

				if (p_149695_1_.getBlock(p_149695_2_, p_149695_3_ + 1, p_149695_4_) != this)
				{
					p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
					var7 = true;
				}

				if (!World.doesBlockHaveSolidTopSurface(p_149695_1_, p_149695_2_, p_149695_3_ - 1, p_149695_4_))
				{
					p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
					var7 = true;

					if (p_149695_1_.getBlock(p_149695_2_, p_149695_3_ + 1, p_149695_4_) == this)
					{
						p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_ + 1, p_149695_4_);
					}
				}

				if (var7)
				{
					if (!p_149695_1_.isClient)
					{
						this.dropBlockAsItem(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, var6, 0);
					}
				}
				else
				{
					bool var8 = p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_) || p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_ + 1, p_149695_4_);

					if ((var8 || p_149695_5_.canProvidePower()) && p_149695_5_ != this)
					{
						this.func_150014_a(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, var8);
					}
				}
			}
			else
			{
				if (p_149695_1_.getBlock(p_149695_2_, p_149695_3_ - 1, p_149695_4_) != this)
				{
					p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
				}

				if (p_149695_5_ != this)
				{
					this.onNeighborBlockChange(p_149695_1_, p_149695_2_, p_149695_3_ - 1, p_149695_4_, p_149695_5_);
				}
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return (p_149650_1_ & 8) != 0 ? null : (this.blockMaterial == Material.iron ? Items.iron_door : Items.wooden_door);
		}

		public virtual MovingObjectPosition collisionRayTrace(World p_149731_1_, int p_149731_2_, int p_149731_3_, int p_149731_4_, Vec3 p_149731_5_, Vec3 p_149731_6_)
		{
			this.setBlockBoundsBasedOnState(p_149731_1_, p_149731_2_, p_149731_3_, p_149731_4_);
			return base.collisionRayTrace(p_149731_1_, p_149731_2_, p_149731_3_, p_149731_4_, p_149731_5_, p_149731_6_);
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return p_149742_3_ >= 255 ? false : World.doesBlockHaveSolidTopSurface(p_149742_1_, p_149742_2_, p_149742_3_ - 1, p_149742_4_) && base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_) && base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_ + 1, p_149742_4_);
		}

		public virtual int MobilityFlag
		{
			get
			{
				return 1;
			}
		}

		public virtual int func_150012_g(IBlockAccess p_150012_1_, int p_150012_2_, int p_150012_3_, int p_150012_4_)
		{
			int var5 = p_150012_1_.getBlockMetadata(p_150012_2_, p_150012_3_, p_150012_4_);
			bool var6 = (var5 & 8) != 0;
			int var7;
			int var8;

			if (var6)
			{
				var7 = p_150012_1_.getBlockMetadata(p_150012_2_, p_150012_3_ - 1, p_150012_4_);
				var8 = var5;
			}
			else
			{
				var7 = var5;
				var8 = p_150012_1_.getBlockMetadata(p_150012_2_, p_150012_3_ + 1, p_150012_4_);
			}

			bool var9 = (var8 & 1) != 0;
			return var7 & 7 | (var6 ? 8 : 0) | (var9 ? 16 : 0);
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return this.blockMaterial == Material.iron ? Items.iron_door : Items.wooden_door;
		}

///    
///     <summary> * Called when the block is attempted to be harvested </summary>
///     
		public virtual void onBlockHarvested(World p_149681_1_, int p_149681_2_, int p_149681_3_, int p_149681_4_, int p_149681_5_, EntityPlayer p_149681_6_)
		{
			if (p_149681_6_.capabilities.isCreativeMode && (p_149681_5_ & 8) != 0 && p_149681_1_.getBlock(p_149681_2_, p_149681_3_ - 1, p_149681_4_) == this)
			{
				p_149681_1_.setBlockToAir(p_149681_2_, p_149681_3_ - 1, p_149681_4_);
			}
		}
	}

}