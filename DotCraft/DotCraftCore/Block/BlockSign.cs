using System;

namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using TileEntity = DotCraftCore.nTileEntity.TileEntity;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using IBlockAccess = DotCraftCore.nWorld.IBlockAccess;
	using World = DotCraftCore.nWorld.World;

	public class BlockSign : BlockContainer
	{
		private Type field_149968_a;
		private bool field_149967_b;
		

		protected internal BlockSign(Type p_i45426_1_, bool p_i45426_2_) : base(Material.wood)
		{
			this.field_149967_b = p_i45426_2_;
			this.field_149968_a = p_i45426_1_;
			float var3 = 0.25F;
			float var4 = 1.0F;
			this.setBlockBounds(0.5F - var3, 0.0F, 0.5F - var3, 0.5F + var3, var4, 0.5F + var3);
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return Blocks.planks.getBlockTextureFromSide(p_149691_1_);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			return null;
		}

///    
///     <summary> * Returns the bounding box of the wired rectangular prism to render. </summary>
///     
		public virtual AxisAlignedBB getSelectedBoundingBoxFromPool(World p_149633_1_, int p_149633_2_, int p_149633_3_, int p_149633_4_)
		{
			this.setBlockBoundsBasedOnState(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
			return base.getSelectedBoundingBoxFromPool(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			if (!this.field_149967_b)
			{
				int var5 = p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_);
				float var6 = 0.28125F;
				float var7 = 0.78125F;
				float var8 = 0.0F;
				float var9 = 1.0F;
				float var10 = 0.125F;
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);

				if (var5 == 2)
				{
					this.setBlockBounds(var8, var6, 1.0F - var10, var9, var7, 1.0F);
				}

				if (var5 == 3)
				{
					this.setBlockBounds(var8, var6, 0.0F, var9, var7, var10);
				}

				if (var5 == 4)
				{
					this.setBlockBounds(1.0F - var10, var6, var8, 1.0F, var7, var9);
				}

				if (var5 == 5)
				{
					this.setBlockBounds(0.0F, var6, var8, var10, var7, var9);
				}
			}
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return -1;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

		public virtual bool getBlocksMovement(IBlockAccess p_149655_1_, int p_149655_2_, int p_149655_3_, int p_149655_4_)
		{
			return true;
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			try
			{
				return (TileEntity)this.field_149968_a.newInstance();
			}
			catch (Exception var4)
			{
				throw new Exception(var4);
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.sign;
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			bool var6 = false;

			if (this.field_149967_b)
			{
				if (!p_149695_1_.getBlock(p_149695_2_, p_149695_3_ - 1, p_149695_4_).Material.Solid)
				{
					var6 = true;
				}
			}
			else
			{
				int var7 = p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_);
				var6 = true;

				if (var7 == 2 && p_149695_1_.getBlock(p_149695_2_, p_149695_3_, p_149695_4_ + 1).Material.Solid)
				{
					var6 = false;
				}

				if (var7 == 3 && p_149695_1_.getBlock(p_149695_2_, p_149695_3_, p_149695_4_ - 1).Material.Solid)
				{
					var6 = false;
				}

				if (var7 == 4 && p_149695_1_.getBlock(p_149695_2_ + 1, p_149695_3_, p_149695_4_).Material.Solid)
				{
					var6 = false;
				}

				if (var7 == 5 && p_149695_1_.getBlock(p_149695_2_ - 1, p_149695_3_, p_149695_4_).Material.Solid)
				{
					var6 = false;
				}
			}

			if (var6)
			{
				this.dropBlockAsItem(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_), 0);
				p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
			}

			base.onNeighborBlockChange(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_5_);
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Items.sign;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
		}
	}

}