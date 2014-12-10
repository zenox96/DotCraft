namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using IIcon = DotCraftCore.Util.IIcon;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockFenceGate : BlockDirectional
	{
		private const string __OBFID = "CL_00000243";

		public BlockFenceGate() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabRedstone;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return Blocks.planks.getBlockTextureFromSide(p_149691_1_);
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return !p_149742_1_.getBlock(p_149742_2_, p_149742_3_ - 1, p_149742_4_).Material.Solid ? false : base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			int var5 = p_149668_1_.getBlockMetadata(p_149668_2_, p_149668_3_, p_149668_4_);
			return isFenceGateOpen(var5) ? null : (var5 != 2 && var5 != 0 ? AxisAlignedBB.getBoundingBox((double)((float)p_149668_2_ + 0.375F), (double)p_149668_3_, (double)p_149668_4_, (double)((float)p_149668_2_ + 0.625F), (double)((float)p_149668_3_ + 1.5F), (double)(p_149668_4_ + 1)) : AxisAlignedBB.getBoundingBox((double)p_149668_2_, (double)p_149668_3_, (double)((float)p_149668_4_ + 0.375F), (double)(p_149668_2_ + 1), (double)((float)p_149668_3_ + 1.5F), (double)((float)p_149668_4_ + 0.625F)));
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			int var5 = func_149895_l(p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_));

			if (var5 != 2 && var5 != 0)
			{
				this.setBlockBounds(0.375F, 0.0F, 0.0F, 0.625F, 1.0F, 1.0F);
			}
			else
			{
				this.setBlockBounds(0.0F, 0.0F, 0.375F, 1.0F, 1.0F, 0.625F);
			}
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

		public virtual bool getBlocksMovement(IBlockAccess p_149655_1_, int p_149655_2_, int p_149655_3_, int p_149655_4_)
		{
			return isFenceGateOpen(p_149655_1_.getBlockMetadata(p_149655_2_, p_149655_3_, p_149655_4_));
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 21;
			}
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			int var7 = (MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3) % 4;
			p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, var7, 2);
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			int var10 = p_149727_1_.getBlockMetadata(p_149727_2_, p_149727_3_, p_149727_4_);

			if (isFenceGateOpen(var10))
			{
				p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_, p_149727_4_, var10 & -5, 2);
			}
			else
			{
				int var11 = (MathHelper.floor_double((double)(p_149727_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3) % 4;
				int var12 = func_149895_l(var10);

				if (var12 == (var11 + 2) % 4)
				{
					var10 = var11;
				}

				p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_, p_149727_4_, var10 | 4, 2);
			}

			p_149727_1_.playAuxSFXAtEntity(p_149727_5_, 1003, p_149727_2_, p_149727_3_, p_149727_4_, 0);
			return true;
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!p_149695_1_.isClient)
			{
				int var6 = p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_);
				bool var7 = p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_);

				if (var7 || p_149695_5_.canProvidePower())
				{
					if (var7 && !isFenceGateOpen(var6))
					{
						p_149695_1_.setBlockMetadataWithNotify(p_149695_2_, p_149695_3_, p_149695_4_, var6 | 4, 2);
						p_149695_1_.playAuxSFXAtEntity((EntityPlayer)null, 1003, p_149695_2_, p_149695_3_, p_149695_4_, 0);
					}
					else if (!var7 && isFenceGateOpen(var6))
					{
						p_149695_1_.setBlockMetadataWithNotify(p_149695_2_, p_149695_3_, p_149695_4_, var6 & -5, 2);
						p_149695_1_.playAuxSFXAtEntity((EntityPlayer)null, 1003, p_149695_2_, p_149695_3_, p_149695_4_, 0);
					}
				}
			}
		}

///    
///     <summary> * Returns if the fence gate is open according to its metadata. </summary>
///     
		public static bool isFenceGateOpen(int p_149896_0_)
		{
			return (p_149896_0_ & 4) != 0;
		}

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return true;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
		}
	}

}