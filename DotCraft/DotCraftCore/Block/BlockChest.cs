using System;
using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityItem = DotCraftCore.Entity.Item.EntityItem;
	using EntityOcelot = DotCraftCore.Entity.Passive.EntityOcelot;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Container = DotCraftCore.Inventory.Container;
	using IInventory = DotCraftCore.Inventory.IInventory;
	using InventoryLargeChest = DotCraftCore.Inventory.InventoryLargeChest;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using TileEntityChest = DotCraftCore.TileEntity.TileEntityChest;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockChest : BlockContainer
	{
		private readonly Random field_149955_b = new Random();
		public readonly int field_149956_a;
		private const string __OBFID = "CL_00000214";

		protected internal BlockChest(int p_i45397_1_) : base(Material.wood)
		{
			this.field_149956_a = p_i45397_1_;
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

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			if (p_149719_1_.getBlock(p_149719_2_, p_149719_3_, p_149719_4_ - 1) == this)
			{
				this.setBlockBounds(0.0625F, 0.0F, 0.0F, 0.9375F, 0.875F, 0.9375F);
			}
			else if (p_149719_1_.getBlock(p_149719_2_, p_149719_3_, p_149719_4_ + 1) == this)
			{
				this.setBlockBounds(0.0625F, 0.0F, 0.0625F, 0.9375F, 0.875F, 1.0F);
			}
			else if (p_149719_1_.getBlock(p_149719_2_ - 1, p_149719_3_, p_149719_4_) == this)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0625F, 0.9375F, 0.875F, 0.9375F);
			}
			else if (p_149719_1_.getBlock(p_149719_2_ + 1, p_149719_3_, p_149719_4_) == this)
			{
				this.setBlockBounds(0.0625F, 0.0F, 0.0625F, 1.0F, 0.875F, 0.9375F);
			}
			else
			{
				this.setBlockBounds(0.0625F, 0.0F, 0.0625F, 0.9375F, 0.875F, 0.9375F);
			}
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
			this.func_149954_e(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
			Block var5 = p_149726_1_.getBlock(p_149726_2_, p_149726_3_, p_149726_4_ - 1);
			Block var6 = p_149726_1_.getBlock(p_149726_2_, p_149726_3_, p_149726_4_ + 1);
			Block var7 = p_149726_1_.getBlock(p_149726_2_ - 1, p_149726_3_, p_149726_4_);
			Block var8 = p_149726_1_.getBlock(p_149726_2_ + 1, p_149726_3_, p_149726_4_);

			if (var5 == this)
			{
				this.func_149954_e(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_ - 1);
			}

			if (var6 == this)
			{
				this.func_149954_e(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_ + 1);
			}

			if (var7 == this)
			{
				this.func_149954_e(p_149726_1_, p_149726_2_ - 1, p_149726_3_, p_149726_4_);
			}

			if (var8 == this)
			{
				this.func_149954_e(p_149726_1_, p_149726_2_ + 1, p_149726_3_, p_149726_4_);
			}
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			Block var7 = p_149689_1_.getBlock(p_149689_2_, p_149689_3_, p_149689_4_ - 1);
			Block var8 = p_149689_1_.getBlock(p_149689_2_, p_149689_3_, p_149689_4_ + 1);
			Block var9 = p_149689_1_.getBlock(p_149689_2_ - 1, p_149689_3_, p_149689_4_);
			Block var10 = p_149689_1_.getBlock(p_149689_2_ + 1, p_149689_3_, p_149689_4_);
			sbyte var11 = 0;
			int var12 = MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3;

			if (var12 == 0)
			{
				var11 = 2;
			}

			if (var12 == 1)
			{
				var11 = 5;
			}

			if (var12 == 2)
			{
				var11 = 3;
			}

			if (var12 == 3)
			{
				var11 = 4;
			}

			if (var7 != this && var8 != this && var9 != this && var10 != this)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, var11, 3);
			}
			else
			{
				if ((var7 == this || var8 == this) && (var11 == 4 || var11 == 5))
				{
					if (var7 == this)
					{
						p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_ - 1, var11, 3);
					}
					else
					{
						p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_ + 1, var11, 3);
					}

					p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, var11, 3);
				}

				if ((var9 == this || var10 == this) && (var11 == 2 || var11 == 3))
				{
					if (var9 == this)
					{
						p_149689_1_.setBlockMetadataWithNotify(p_149689_2_ - 1, p_149689_3_, p_149689_4_, var11, 3);
					}
					else
					{
						p_149689_1_.setBlockMetadataWithNotify(p_149689_2_ + 1, p_149689_3_, p_149689_4_, var11, 3);
					}

					p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, var11, 3);
				}
			}

			if (p_149689_6_.hasDisplayName())
			{
				((TileEntityChest)p_149689_1_.getTileEntity(p_149689_2_, p_149689_3_, p_149689_4_)).func_145976_a(p_149689_6_.DisplayName);
			}
		}

		public virtual void func_149954_e(World p_149954_1_, int p_149954_2_, int p_149954_3_, int p_149954_4_)
		{
			if (!p_149954_1_.isClient)
			{
				Block var5 = p_149954_1_.getBlock(p_149954_2_, p_149954_3_, p_149954_4_ - 1);
				Block var6 = p_149954_1_.getBlock(p_149954_2_, p_149954_3_, p_149954_4_ + 1);
				Block var7 = p_149954_1_.getBlock(p_149954_2_ - 1, p_149954_3_, p_149954_4_);
				Block var8 = p_149954_1_.getBlock(p_149954_2_ + 1, p_149954_3_, p_149954_4_);
				bool var9 = true;
				int var10;
				Block var11;
				int var12;
				Block var13;
				bool var14;
				sbyte var15;
				int var16;

				if (var5 != this && var6 != this)
				{
					if (var7 != this && var8 != this)
					{
						var15 = 3;

						if (var5.func_149730_j() && !var6.func_149730_j())
						{
							var15 = 3;
						}

						if (var6.func_149730_j() && !var5.func_149730_j())
						{
							var15 = 2;
						}

						if (var7.func_149730_j() && !var8.func_149730_j())
						{
							var15 = 5;
						}

						if (var8.func_149730_j() && !var7.func_149730_j())
						{
							var15 = 4;
						}
					}
					else
					{
						var10 = var7 == this ? p_149954_2_ - 1 : p_149954_2_ + 1;
						var11 = p_149954_1_.getBlock(var10, p_149954_3_, p_149954_4_ - 1);
						var12 = var7 == this ? p_149954_2_ - 1 : p_149954_2_ + 1;
						var13 = p_149954_1_.getBlock(var12, p_149954_3_, p_149954_4_ + 1);
						var15 = 3;
						var14 = true;

						if (var7 == this)
						{
							var16 = p_149954_1_.getBlockMetadata(p_149954_2_ - 1, p_149954_3_, p_149954_4_);
						}
						else
						{
							var16 = p_149954_1_.getBlockMetadata(p_149954_2_ + 1, p_149954_3_, p_149954_4_);
						}

						if (var16 == 2)
						{
							var15 = 2;
						}

						if ((var5.func_149730_j() || var11.func_149730_j()) && !var6.func_149730_j() && !var13.func_149730_j())
						{
							var15 = 3;
						}

						if ((var6.func_149730_j() || var13.func_149730_j()) && !var5.func_149730_j() && !var11.func_149730_j())
						{
							var15 = 2;
						}
					}
				}
				else
				{
					var10 = var5 == this ? p_149954_4_ - 1 : p_149954_4_ + 1;
					var11 = p_149954_1_.getBlock(p_149954_2_ - 1, p_149954_3_, var10);
					var12 = var5 == this ? p_149954_4_ - 1 : p_149954_4_ + 1;
					var13 = p_149954_1_.getBlock(p_149954_2_ + 1, p_149954_3_, var12);
					var15 = 5;
					var14 = true;

					if (var5 == this)
					{
						var16 = p_149954_1_.getBlockMetadata(p_149954_2_, p_149954_3_, p_149954_4_ - 1);
					}
					else
					{
						var16 = p_149954_1_.getBlockMetadata(p_149954_2_, p_149954_3_, p_149954_4_ + 1);
					}

					if (var16 == 4)
					{
						var15 = 4;
					}

					if ((var7.func_149730_j() || var11.func_149730_j()) && !var8.func_149730_j() && !var13.func_149730_j())
					{
						var15 = 5;
					}

					if ((var8.func_149730_j() || var13.func_149730_j()) && !var7.func_149730_j() && !var11.func_149730_j())
					{
						var15 = 4;
					}
				}

				p_149954_1_.setBlockMetadataWithNotify(p_149954_2_, p_149954_3_, p_149954_4_, var15, 3);
			}
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			int var5 = 0;

			if (p_149742_1_.getBlock(p_149742_2_ - 1, p_149742_3_, p_149742_4_) == this)
			{
				++var5;
			}

			if (p_149742_1_.getBlock(p_149742_2_ + 1, p_149742_3_, p_149742_4_) == this)
			{
				++var5;
			}

			if (p_149742_1_.getBlock(p_149742_2_, p_149742_3_, p_149742_4_ - 1) == this)
			{
				++var5;
			}

			if (p_149742_1_.getBlock(p_149742_2_, p_149742_3_, p_149742_4_ + 1) == this)
			{
				++var5;
			}

			return var5 > 1 ? false : (this.func_149952_n(p_149742_1_, p_149742_2_ - 1, p_149742_3_, p_149742_4_) ? false : (this.func_149952_n(p_149742_1_, p_149742_2_ + 1, p_149742_3_, p_149742_4_) ? false : (this.func_149952_n(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_ - 1) ? false : !this.func_149952_n(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_ + 1))));
		}

		private bool func_149952_n(World p_149952_1_, int p_149952_2_, int p_149952_3_, int p_149952_4_)
		{
			return p_149952_1_.getBlock(p_149952_2_, p_149952_3_, p_149952_4_) != this ? false : (p_149952_1_.getBlock(p_149952_2_ - 1, p_149952_3_, p_149952_4_) == this ? true : (p_149952_1_.getBlock(p_149952_2_ + 1, p_149952_3_, p_149952_4_) == this ? true : (p_149952_1_.getBlock(p_149952_2_, p_149952_3_, p_149952_4_ - 1) == this ? true : p_149952_1_.getBlock(p_149952_2_, p_149952_3_, p_149952_4_ + 1) == this)));
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			base.onNeighborBlockChange(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_5_);
			TileEntityChest var6 = (TileEntityChest)p_149695_1_.getTileEntity(p_149695_2_, p_149695_3_, p_149695_4_);

			if (var6 != null)
			{
				var6.updateContainingBlockInfo();
			}
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			TileEntityChest var7 = (TileEntityChest)p_149749_1_.getTileEntity(p_149749_2_, p_149749_3_, p_149749_4_);

			if (var7 != null)
			{
				for (int var8 = 0; var8 < var7.SizeInventory; ++var8)
				{
					ItemStack var9 = var7.getStackInSlot(var8);

					if (var9 != null)
					{
						float var10 = this.field_149955_b.nextFloat() * 0.8F + 0.1F;
						float var11 = this.field_149955_b.nextFloat() * 0.8F + 0.1F;
						EntityItem var14;

						for (float var12 = this.field_149955_b.nextFloat() * 0.8F + 0.1F; var9.stackSize > 0; p_149749_1_.spawnEntityInWorld(var14))
						{
							int var13 = this.field_149955_b.Next(21) + 10;

							if (var13 > var9.stackSize)
							{
								var13 = var9.stackSize;
							}

							var9.stackSize -= var13;
							var14 = new EntityItem(p_149749_1_, (double)((float)p_149749_2_ + var10), (double)((float)p_149749_3_ + var11), (double)((float)p_149749_4_ + var12), new ItemStack(var9.Item, var13, var9.ItemDamage));
							float var15 = 0.05F;
							var14.motionX = (double)((float)this.field_149955_b.nextGaussian() * var15);
							var14.motionY = (double)((float)this.field_149955_b.nextGaussian() * var15 + 0.2F);
							var14.motionZ = (double)((float)this.field_149955_b.nextGaussian() * var15);

							if (var9.hasTagCompound())
							{
								var14.EntityItem.TagCompound = (NBTTagCompound)var9.TagCompound.copy();
							}
						}
					}
				}

				p_149749_1_.func_147453_f(p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_);
			}

			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
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
				IInventory var10 = this.func_149951_m(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_);

				if (var10 != null)
				{
					p_149727_5_.displayGUIChest(var10);
				}

				return true;
			}
		}

		public virtual IInventory func_149951_m(World p_149951_1_, int p_149951_2_, int p_149951_3_, int p_149951_4_)
		{
			object var5 = (TileEntityChest)p_149951_1_.getTileEntity(p_149951_2_, p_149951_3_, p_149951_4_);

			if (var5 == null)
			{
				return null;
			}
			else if (p_149951_1_.getBlock(p_149951_2_, p_149951_3_ + 1, p_149951_4_).NormalCube)
			{
				return null;
			}
			else if (func_149953_o(p_149951_1_, p_149951_2_, p_149951_3_, p_149951_4_))
			{
				return null;
			}
			else if (p_149951_1_.getBlock(p_149951_2_ - 1, p_149951_3_, p_149951_4_) == this && (p_149951_1_.getBlock(p_149951_2_ - 1, p_149951_3_ + 1, p_149951_4_).NormalCube || func_149953_o(p_149951_1_, p_149951_2_ - 1, p_149951_3_, p_149951_4_)))
			{
				return null;
			}
			else if (p_149951_1_.getBlock(p_149951_2_ + 1, p_149951_3_, p_149951_4_) == this && (p_149951_1_.getBlock(p_149951_2_ + 1, p_149951_3_ + 1, p_149951_4_).NormalCube || func_149953_o(p_149951_1_, p_149951_2_ + 1, p_149951_3_, p_149951_4_)))
			{
				return null;
			}
			else if (p_149951_1_.getBlock(p_149951_2_, p_149951_3_, p_149951_4_ - 1) == this && (p_149951_1_.getBlock(p_149951_2_, p_149951_3_ + 1, p_149951_4_ - 1).NormalCube || func_149953_o(p_149951_1_, p_149951_2_, p_149951_3_, p_149951_4_ - 1)))
			{
				return null;
			}
			else if (p_149951_1_.getBlock(p_149951_2_, p_149951_3_, p_149951_4_ + 1) == this && (p_149951_1_.getBlock(p_149951_2_, p_149951_3_ + 1, p_149951_4_ + 1).NormalCube || func_149953_o(p_149951_1_, p_149951_2_, p_149951_3_, p_149951_4_ + 1)))
			{
				return null;
			}
			else
			{
				if (p_149951_1_.getBlock(p_149951_2_ - 1, p_149951_3_, p_149951_4_) == this)
				{
					var5 = new InventoryLargeChest("container.chestDouble", (TileEntityChest)p_149951_1_.getTileEntity(p_149951_2_ - 1, p_149951_3_, p_149951_4_), (IInventory)var5);
				}

				if (p_149951_1_.getBlock(p_149951_2_ + 1, p_149951_3_, p_149951_4_) == this)
				{
					var5 = new InventoryLargeChest("container.chestDouble", (IInventory)var5, (TileEntityChest)p_149951_1_.getTileEntity(p_149951_2_ + 1, p_149951_3_, p_149951_4_));
				}

				if (p_149951_1_.getBlock(p_149951_2_, p_149951_3_, p_149951_4_ - 1) == this)
				{
					var5 = new InventoryLargeChest("container.chestDouble", (TileEntityChest)p_149951_1_.getTileEntity(p_149951_2_, p_149951_3_, p_149951_4_ - 1), (IInventory)var5);
				}

				if (p_149951_1_.getBlock(p_149951_2_, p_149951_3_, p_149951_4_ + 1) == this)
				{
					var5 = new InventoryLargeChest("container.chestDouble", (IInventory)var5, (TileEntityChest)p_149951_1_.getTileEntity(p_149951_2_, p_149951_3_, p_149951_4_ + 1));
				}

				return (IInventory)var5;
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			TileEntityChest var3 = new TileEntityChest();
			return var3;
		}

///    
///     <summary> * Can this block provide power. Only wire currently seems to have this change based on its state. </summary>
///     
		public virtual bool canProvidePower()
		{
			return this.field_149956_a == 1;
		}

		public virtual int isProvidingWeakPower(IBlockAccess p_149709_1_, int p_149709_2_, int p_149709_3_, int p_149709_4_, int p_149709_5_)
		{
			if (!this.canProvidePower())
			{
				return 0;
			}
			else
			{
				int var6 = ((TileEntityChest)p_149709_1_.getTileEntity(p_149709_2_, p_149709_3_, p_149709_4_)).field_145987_o;
				return MathHelper.clamp_int(var6, 0, 15);
			}
		}

		public virtual int isProvidingStrongPower(IBlockAccess p_149748_1_, int p_149748_2_, int p_149748_3_, int p_149748_4_, int p_149748_5_)
		{
			return p_149748_5_ == 1 ? this.isProvidingWeakPower(p_149748_1_, p_149748_2_, p_149748_3_, p_149748_4_, p_149748_5_) : 0;
		}

		private static bool func_149953_o(World p_149953_0_, int p_149953_1_, int p_149953_2_, int p_149953_3_)
		{
			IEnumerator var4 = p_149953_0_.getEntitiesWithinAABB(typeof(EntityOcelot), AxisAlignedBB.getBoundingBox((double)p_149953_1_, (double)(p_149953_2_ + 1), (double)p_149953_3_, (double)(p_149953_1_ + 1), (double)(p_149953_2_ + 2), (double)(p_149953_3_ + 1))).GetEnumerator();
			EntityOcelot var6;

			do
			{
				if (!var4.MoveNext())
				{
					return false;
				}

				Entity var5 = (Entity)var4.Current;
				var6 = (EntityOcelot)var5;
			}
			while (!var6.Sitting);

			return true;
		}

		public virtual bool hasComparatorInputOverride()
		{
			return true;
		}

		public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			return Container.calcRedstoneFromInventory(this.func_149951_m(p_149736_1_, p_149736_2_, p_149736_3_, p_149736_4_));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon("planks_oak");
		}
	}

}