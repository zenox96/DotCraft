using System;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using BehaviorDefaultDispenseItem = DotCraftCore.dispenser.BehaviorDefaultDispenseItem;
	using IBehaviorDispenseItem = DotCraftCore.dispenser.IBehaviorDispenseItem;
	using IBlockSource = DotCraftCore.dispenser.IBlockSource;
	using IPosition = DotCraftCore.dispenser.IPosition;
	using PositionImpl = DotCraftCore.dispenser.PositionImpl;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityItem = DotCraftCore.entity.item.EntityItem;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Container = DotCraftCore.inventory.Container;
	using IInventory = DotCraftCore.inventory.IInventory;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using TileEntityDispenser = DotCraftCore.tileentity.TileEntityDispenser;
	using EnumFacing = DotCraftCore.util.EnumFacing;
	using IIcon = DotCraftCore.util.IIcon;
	using IRegistry = DotCraftCore.util.IRegistry;
	using RegistryDefaulted = DotCraftCore.util.RegistryDefaulted;
	using World = DotCraftCore.world.World;

	public class BlockDispenser : BlockContainer
	{
		public static readonly IRegistry dispenseBehaviorRegistry = new RegistryDefaulted(new BehaviorDefaultDispenseItem());
		protected internal Random field_149942_b = new Random();
		protected internal IIcon field_149944_M;
		protected internal IIcon field_149945_N;
		protected internal IIcon field_149946_O;
		private const string __OBFID = "CL_00000229";

		protected internal BlockDispenser() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabRedstone;
		}

		public virtual int func_149738_a(World p_149738_1_)
		{
			return 4;
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
			this.func_149938_m(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
		}

		private void func_149938_m(World p_149938_1_, int p_149938_2_, int p_149938_3_, int p_149938_4_)
		{
			if (!p_149938_1_.isClient)
			{
				Block var5 = p_149938_1_.getBlock(p_149938_2_, p_149938_3_, p_149938_4_ - 1);
				Block var6 = p_149938_1_.getBlock(p_149938_2_, p_149938_3_, p_149938_4_ + 1);
				Block var7 = p_149938_1_.getBlock(p_149938_2_ - 1, p_149938_3_, p_149938_4_);
				Block var8 = p_149938_1_.getBlock(p_149938_2_ + 1, p_149938_3_, p_149938_4_);
				sbyte var9 = 3;

				if (var5.func_149730_j() && !var6.func_149730_j())
				{
					var9 = 3;
				}

				if (var6.func_149730_j() && !var5.func_149730_j())
				{
					var9 = 2;
				}

				if (var7.func_149730_j() && !var8.func_149730_j())
				{
					var9 = 5;
				}

				if (var8.func_149730_j() && !var7.func_149730_j())
				{
					var9 = 4;
				}

				p_149938_1_.setBlockMetadataWithNotify(p_149938_2_, p_149938_3_, p_149938_4_, var9, 2);
			}
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			int var3 = p_149691_2_ & 7;
			return p_149691_1_ == var3 ? (var3 != 1 && var3 != 0 ? this.field_149945_N : this.field_149946_O) : (var3 != 1 && var3 != 0 ? (p_149691_1_ != 1 && p_149691_1_ != 0 ? this.blockIcon : this.field_149944_M) : this.field_149944_M);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon("furnace_side");
			this.field_149944_M = p_149651_1_.registerIcon("furnace_top");
			this.field_149945_N = p_149651_1_.registerIcon(this.TextureName + "_front_horizontal");
			this.field_149946_O = p_149651_1_.registerIcon(this.TextureName + "_front_vertical");
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
				TileEntityDispenser var10 = (TileEntityDispenser)p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_);

				if (var10 != null)
				{
					p_149727_5_.func_146102_a(var10);
				}

				return true;
			}
		}

		protected internal virtual void func_149941_e(World p_149941_1_, int p_149941_2_, int p_149941_3_, int p_149941_4_)
		{
			BlockSourceImpl var5 = new BlockSourceImpl(p_149941_1_, p_149941_2_, p_149941_3_, p_149941_4_);
			TileEntityDispenser var6 = (TileEntityDispenser)var5.BlockTileEntity;

			if (var6 != null)
			{
				int var7 = var6.func_146017_i();

				if (var7 < 0)
				{
					p_149941_1_.playAuxSFX(1001, p_149941_2_, p_149941_3_, p_149941_4_, 0);
				}
				else
				{
					ItemStack var8 = var6.getStackInSlot(var7);
					IBehaviorDispenseItem var9 = this.func_149940_a(var8);

					if (var9 != IBehaviorDispenseItem.itemDispenseBehaviorProvider)
					{
						ItemStack var10 = var9.dispense(var5, var8);
						var6.setInventorySlotContents(var7, var10.stackSize == 0 ? null : var10);
					}
				}
			}
		}

		protected internal virtual IBehaviorDispenseItem func_149940_a(ItemStack p_149940_1_)
		{
			return (IBehaviorDispenseItem)dispenseBehaviorRegistry.getObject(p_149940_1_.Item);
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			bool var6 = p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_) || p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_ + 1, p_149695_4_);
			int var7 = p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_);
			bool var8 = (var7 & 8) != 0;

			if (var6 && !var8)
			{
				p_149695_1_.scheduleBlockUpdate(p_149695_2_, p_149695_3_, p_149695_4_, this, this.func_149738_a(p_149695_1_));
				p_149695_1_.setBlockMetadataWithNotify(p_149695_2_, p_149695_3_, p_149695_4_, var7 | 8, 4);
			}
			else if (!var6 && var8)
			{
				p_149695_1_.setBlockMetadataWithNotify(p_149695_2_, p_149695_3_, p_149695_4_, var7 & -9, 4);
			}
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!p_149674_1_.isClient)
			{
				this.func_149941_e(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_);
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityDispenser();
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			int var7 = BlockPistonBase.func_150071_a(p_149689_1_, p_149689_2_, p_149689_3_, p_149689_4_, p_149689_5_);
			p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, var7, 2);

			if (p_149689_6_.hasDisplayName())
			{
				((TileEntityDispenser)p_149689_1_.getTileEntity(p_149689_2_, p_149689_3_, p_149689_4_)).func_146018_a(p_149689_6_.DisplayName);
			}
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			TileEntityDispenser var7 = (TileEntityDispenser)p_149749_1_.getTileEntity(p_149749_2_, p_149749_3_, p_149749_4_);

			if (var7 != null)
			{
				for (int var8 = 0; var8 < var7.SizeInventory; ++var8)
				{
					ItemStack var9 = var7.getStackInSlot(var8);

					if (var9 != null)
					{
						float var10 = this.field_149942_b.nextFloat() * 0.8F + 0.1F;
						float var11 = this.field_149942_b.nextFloat() * 0.8F + 0.1F;
						float var12 = this.field_149942_b.nextFloat() * 0.8F + 0.1F;

						while (var9.stackSize > 0)
						{
							int var13 = this.field_149942_b.Next(21) + 10;

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
							var14.motionX = (double)((float)this.field_149942_b.nextGaussian() * var15);
							var14.motionY = (double)((float)this.field_149942_b.nextGaussian() * var15 + 0.2F);
							var14.motionZ = (double)((float)this.field_149942_b.nextGaussian() * var15);
							p_149749_1_.spawnEntityInWorld(var14);
						}
					}
				}

				p_149749_1_.func_147453_f(p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_);
			}

			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
		}

		public static IPosition func_149939_a(IBlockSource p_149939_0_)
		{
			EnumFacing var1 = func_149937_b(p_149939_0_.BlockMetadata);
			double var2 = p_149939_0_.X + 0.7D * (double)var1.FrontOffsetX;
			double var4 = p_149939_0_.Y + 0.7D * (double)var1.FrontOffsetY;
			double var6 = p_149939_0_.Z + 0.7D * (double)var1.FrontOffsetZ;
			return new PositionImpl(var2, var4, var6);
		}

		public static EnumFacing func_149937_b(int p_149937_0_)
		{
			return EnumFacing.getFront(p_149937_0_ & 7);
		}

		public virtual bool hasComparatorInputOverride()
		{
			return true;
		}

		public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			return Container.calcRedstoneFromInventory((IInventory)p_149736_1_.getTileEntity(p_149736_2_, p_149736_3_, p_149736_4_));
		}
	}

}