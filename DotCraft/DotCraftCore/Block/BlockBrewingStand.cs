using System;
using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using Entity = DotCraftCore.entity.Entity;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityItem = DotCraftCore.entity.item.EntityItem;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using Container = DotCraftCore.inventory.Container;
	using IInventory = DotCraftCore.inventory.IInventory;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using TileEntityBrewingStand = DotCraftCore.tileentity.TileEntityBrewingStand;
	using AxisAlignedBB = DotCraftCore.util.AxisAlignedBB;
	using IIcon = DotCraftCore.util.IIcon;
	using World = DotCraftCore.world.World;

	public class BlockBrewingStand : BlockContainer
	{
		private Random field_149961_a = new Random();
		private IIcon field_149960_b;
		private const string __OBFID = "CL_00000207";

		public BlockBrewingStand() : base(Material.iron)
		{
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
				return 25;
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityBrewingStand();
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			this.setBlockBounds(0.4375F, 0.0F, 0.4375F, 0.5625F, 0.875F, 0.5625F);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			this.setBlockBoundsForItemRender();
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
		}

///    
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public virtual void setBlockBoundsForItemRender()
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.125F, 1.0F);
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
				TileEntityBrewingStand var10 = (TileEntityBrewingStand)p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_);

				if (var10 != null)
				{
					p_149727_5_.func_146098_a(var10);
				}

				return true;
			}
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			if (p_149689_6_.hasDisplayName())
			{
				((TileEntityBrewingStand)p_149689_1_.getTileEntity(p_149689_2_, p_149689_3_, p_149689_4_)).func_145937_a(p_149689_6_.DisplayName);
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public virtual void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			double var6 = (double)((float)p_149734_2_ + 0.4F + p_149734_5_.nextFloat() * 0.2F);
			double var8 = (double)((float)p_149734_3_ + 0.7F + p_149734_5_.nextFloat() * 0.3F);
			double var10 = (double)((float)p_149734_4_ + 0.4F + p_149734_5_.nextFloat() * 0.2F);
			p_149734_1_.spawnParticle("smoke", var6, var8, var10, 0.0D, 0.0D, 0.0D);
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			TileEntity var7 = p_149749_1_.getTileEntity(p_149749_2_, p_149749_3_, p_149749_4_);

			if (var7 is TileEntityBrewingStand)
			{
				TileEntityBrewingStand var8 = (TileEntityBrewingStand)var7;

				for (int var9 = 0; var9 < var8.SizeInventory; ++var9)
				{
					ItemStack var10 = var8.getStackInSlot(var9);

					if (var10 != null)
					{
						float var11 = this.field_149961_a.nextFloat() * 0.8F + 0.1F;
						float var12 = this.field_149961_a.nextFloat() * 0.8F + 0.1F;
						float var13 = this.field_149961_a.nextFloat() * 0.8F + 0.1F;

						while (var10.stackSize > 0)
						{
							int var14 = this.field_149961_a.Next(21) + 10;

							if (var14 > var10.stackSize)
							{
								var14 = var10.stackSize;
							}

							var10.stackSize -= var14;
							EntityItem var15 = new EntityItem(p_149749_1_, (double)((float)p_149749_2_ + var11), (double)((float)p_149749_3_ + var12), (double)((float)p_149749_4_ + var13), new ItemStack(var10.Item, var14, var10.ItemDamage));
							float var16 = 0.05F;
							var15.motionX = (double)((float)this.field_149961_a.nextGaussian() * var16);
							var15.motionY = (double)((float)this.field_149961_a.nextGaussian() * var16 + 0.2F);
							var15.motionZ = (double)((float)this.field_149961_a.nextGaussian() * var16);
							p_149749_1_.spawnEntityInWorld(var15);
						}
					}
				}
			}

			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.brewing_stand;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Items.brewing_stand;
		}

		public virtual bool hasComparatorInputOverride()
		{
			return true;
		}

		public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			return Container.calcRedstoneFromInventory((IInventory)p_149736_1_.getTileEntity(p_149736_2_, p_149736_3_, p_149736_4_));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			base.registerBlockIcons(p_149651_1_);
			this.field_149960_b = p_149651_1_.registerIcon(this.TextureName + "_base");
		}

		public virtual IIcon func_149959_e()
		{
			return this.field_149960_b;
		}
	}

}