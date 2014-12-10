using System;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using InventoryEnderChest = DotCraftCore.inventory.InventoryEnderChest;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using TileEntityEnderChest = DotCraftCore.tileentity.TileEntityEnderChest;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public class BlockEnderChest : BlockContainer
	{
		private const string __OBFID = "CL_00000238";

		protected internal BlockEnderChest() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
			this.setBlockBounds(0.0625F, 0.0F, 0.0625F, 0.9375F, 0.875F, 0.9375F);
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
				return 22;
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.obsidian);
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 8;
		}

		protected internal virtual bool canSilkHarvest()
		{
			return true;
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			sbyte var7 = 0;
			int var8 = MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3;

			if (var8 == 0)
			{
				var7 = 2;
			}

			if (var8 == 1)
			{
				var7 = 5;
			}

			if (var8 == 2)
			{
				var7 = 3;
			}

			if (var8 == 3)
			{
				var7 = 4;
			}

			p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, var7, 2);
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			InventoryEnderChest var10 = p_149727_5_.InventoryEnderChest;
			TileEntityEnderChest var11 = (TileEntityEnderChest)p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_);

			if (var10 != null && var11 != null)
			{
				if (p_149727_1_.getBlock(p_149727_2_, p_149727_3_ + 1, p_149727_4_).NormalCube)
				{
					return true;
				}
				else if (p_149727_1_.isClient)
				{
					return true;
				}
				else
				{
					var10.func_146031_a(var11);
					p_149727_5_.displayGUIChest(var10);
					return true;
				}
			}
			else
			{
				return true;
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityEnderChest();
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public virtual void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			for (int var6 = 0; var6 < 3; ++var6)
			{
				double var10000 = (double)((float)p_149734_2_ + p_149734_5_.nextFloat());
				double var9 = (double)((float)p_149734_3_ + p_149734_5_.nextFloat());
				var10000 = (double)((float)p_149734_4_ + p_149734_5_.nextFloat());
				double var13 = 0.0D;
				double var15 = 0.0D;
				double var17 = 0.0D;
				int var19 = p_149734_5_.Next(2) * 2 - 1;
				int var20 = p_149734_5_.Next(2) * 2 - 1;
				var13 = ((double)p_149734_5_.nextFloat() - 0.5D) * 0.125D;
				var15 = ((double)p_149734_5_.nextFloat() - 0.5D) * 0.125D;
				var17 = ((double)p_149734_5_.nextFloat() - 0.5D) * 0.125D;
				double var11 = (double)p_149734_4_ + 0.5D + 0.25D * (double)var20;
				var17 = (double)(p_149734_5_.nextFloat() * 1.0F * (float)var20);
				double var7 = (double)p_149734_2_ + 0.5D + 0.25D * (double)var19;
				var13 = (double)(p_149734_5_.nextFloat() * 1.0F * (float)var19);
				p_149734_1_.spawnParticle("portal", var7, var9, var11, var13, var15, var17);
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon("obsidian");
		}
	}

}