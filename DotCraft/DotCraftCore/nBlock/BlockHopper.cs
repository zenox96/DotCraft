using System;
using System.Collections;

namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Entity = DotCraftCore.nEntity.Entity;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EntityItem = DotCraftCore.nEntity.nItem.EntityItem;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Container = DotCraftCore.nInventory.Container;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using TileEntity = DotCraftCore.nTileEntity.TileEntity;
	using TileEntityHopper = DotCraftCore.nTileEntity.TileEntityHopper;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using Facing = DotCraftCore.nUtil.Facing;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using IBlockAccess = DotCraftCore.nWorld.IBlockAccess;
	using World = DotCraftCore.nWorld.World;

	public class BlockHopper : BlockContainer
	{
		private readonly Random field_149922_a = new Random();
		private IIcon field_149921_b;
		private IIcon field_149923_M;
		private IIcon field_149924_N;
		

		public BlockHopper() : base(Material.iron)
		{
			this.CreativeTab = CreativeTabs.tabRedstone;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.625F, 1.0F);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			float var8 = 0.125F;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, var8, 1.0F, 1.0F);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, var8);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			this.setBlockBounds(1.0F - var8, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			this.setBlockBounds(0.0F, 0.0F, 1.0F - var8, 1.0F, 1.0F, 1.0F);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public virtual int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
		{
			int var10 = Facing.oppositeSide[p_149660_5_];

			if (var10 == 1)
			{
				var10 = 0;
			}

			return var10;
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityHopper();
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			base.onBlockPlacedBy(p_149689_1_, p_149689_2_, p_149689_3_, p_149689_4_, p_149689_5_, p_149689_6_);

			if (p_149689_6_.hasDisplayName())
			{
				TileEntityHopper var7 = func_149920_e(p_149689_1_, p_149689_2_, p_149689_3_, p_149689_4_);
				var7.func_145886_a(p_149689_6_.DisplayName);
			}
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
			this.func_149919_e(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (p_149727_1_.isClient)
			{
				return true;
			}
			else
			{
				TileEntityHopper var10 = func_149920_e(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_);

				if (var10 != null)
				{
					p_149727_5_.func_146093_a(var10);
				}

				return true;
			}
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			this.func_149919_e(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_);
		}

		private void func_149919_e(World p_149919_1_, int p_149919_2_, int p_149919_3_, int p_149919_4_)
		{
			int var5 = p_149919_1_.getBlockMetadata(p_149919_2_, p_149919_3_, p_149919_4_);
			int var6 = func_149918_b(var5);
			bool var7 = !p_149919_1_.isBlockIndirectlyGettingPowered(p_149919_2_, p_149919_3_, p_149919_4_);
			bool var8 = func_149917_c(var5);

			if (var7 != var8)
			{
				p_149919_1_.setBlockMetadataWithNotify(p_149919_2_, p_149919_3_, p_149919_4_, var6 | (var7 ? 0 : 8), 4);
			}
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			TileEntityHopper var7 = (TileEntityHopper)p_149749_1_.getTileEntity(p_149749_2_, p_149749_3_, p_149749_4_);

			if (var7 != null)
			{
				for (int var8 = 0; var8 < var7.SizeInventory; ++var8)
				{
					ItemStack var9 = var7.getStackInSlot(var8);

					if (var9 != null)
					{
						float var10 = this.field_149922_a.nextFloat() * 0.8F + 0.1F;
						float var11 = this.field_149922_a.nextFloat() * 0.8F + 0.1F;
						float var12 = this.field_149922_a.nextFloat() * 0.8F + 0.1F;

						while (var9.stackSize > 0)
						{
							int var13 = this.field_149922_a.Next(21) + 10;

							if (var13 > var9.stackSize)
							{
								var13 = var9.stackSize;
							}

							var9.stackSize -= var13;
							EntityItem var14 = new EntityItem(p_149749_1_, (double)((float)p_149749_2_ + var10), (double)((float)p_149749_3_ + var11), (double)((float)p_149749_4_ + var12), new ItemStack(var9.Item, var13, var9.ItemDamage));

							if (var9.hasTagCompound())
							{
								var14.EntityItem.TagCompound = (NBTTagCompound)var9.TagCompound.copy();
							}

							float var15 = 0.05F;
							var14.motionX = (double)((float)this.field_149922_a.nextGaussian() * var15);
							var14.motionY = (double)((float)this.field_149922_a.nextGaussian() * var15 + 0.2F);
							var14.motionZ = (double)((float)this.field_149922_a.nextGaussian() * var15);
							p_149749_1_.spawnEntityInWorld(var14);
						}
					}
				}

				p_149749_1_.func_147453_f(p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_);
			}

			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 38;
			}
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

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return true;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 1 ? this.field_149923_M : this.field_149921_b;
		}

		public static int func_149918_b(int p_149918_0_)
		{
			return p_149918_0_ & 7;
		}

		public static bool func_149917_c(int p_149917_0_)
		{
			return (p_149917_0_ & 8) != 8;
		}

		public virtual bool hasComparatorInputOverride()
		{
			return true;
		}

		public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			return Container.calcRedstoneFromInventory(func_149920_e(p_149736_1_, p_149736_2_, p_149736_3_, p_149736_4_));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149921_b = p_149651_1_.registerIcon("hopper_outside");
			this.field_149923_M = p_149651_1_.registerIcon("hopper_top");
			this.field_149924_N = p_149651_1_.registerIcon("hopper_inside");
		}

		public static IIcon func_149916_e(string p_149916_0_)
		{
			return p_149916_0_.Equals("hopper_outside") ? Blocks.hopper.field_149921_b : (p_149916_0_.Equals("hopper_inside") ? Blocks.hopper.field_149924_N : null);
		}

///    
///     <summary> * Gets the icon name of the ItemBlock corresponding to this block. Used by hoppers. </summary>
///     
		public virtual string ItemIconName
		{
			get
			{
				return "hopper";
			}
		}

		public static TileEntityHopper func_149920_e(IBlockAccess p_149920_0_, int p_149920_1_, int p_149920_2_, int p_149920_3_)
		{
			return (TileEntityHopper)p_149920_0_.getTileEntity(p_149920_1_, p_149920_2_, p_149920_3_);
		}
	}

}