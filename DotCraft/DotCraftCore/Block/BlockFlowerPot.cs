using System;

namespace DotCraftCore.Block
{

	
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemBlock = DotCraftCore.item.ItemBlock;
	using ItemStack = DotCraftCore.item.ItemStack;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using TileEntityFlowerPot = DotCraftCore.tileentity.TileEntityFlowerPot;
	using World = DotCraftCore.world.World;

	public class BlockFlowerPot : BlockContainer
	{
		private const string __OBFID = "CL_00000247";

		public BlockFlowerPot() : base(Material.circuits)
		{
			this.setBlockBoundsForItemRender();
		}

///    
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public virtual void setBlockBoundsForItemRender()
		{
			float var1 = 0.375F;
			float var2 = var1 / 2.0F;
			this.setBlockBounds(0.5F - var2, 0.0F, 0.5F - var2, 0.5F + var2, var1, 0.5F + var2);
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
				return 33;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			ItemStack var10 = p_149727_5_.inventory.CurrentItem;

			if (var10 != null && var10.Item is ItemBlock)
			{
				TileEntityFlowerPot var11 = this.func_149929_e(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_);

				if (var11 != null)
				{
					if (var11.func_145965_a() != null)
					{
						return false;
					}
					else
					{
						Block var12 = Block.getBlockFromItem(var10.Item);

						if (!this.func_149928_a(var12, var10.ItemDamage))
						{
							return false;
						}
						else
						{
							var11.func_145964_a(var10.Item, var10.ItemDamage);
							var11.onInventoryChanged();

							if (!p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_, p_149727_4_, var10.ItemDamage, 2))
							{
								p_149727_1_.func_147471_g(p_149727_2_, p_149727_3_, p_149727_4_);
							}

							if (!p_149727_5_.capabilities.isCreativeMode && --var10.stackSize <= 0)
							{
								p_149727_5_.inventory.setInventorySlotContents(p_149727_5_.inventory.currentItem, (ItemStack)null);
							}

							return true;
						}
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private bool func_149928_a(Block p_149928_1_, int p_149928_2_)
		{
			return p_149928_1_ != Blocks.yellow_flower && p_149928_1_ != Blocks.red_flower && p_149928_1_ != Blocks.cactus && p_149928_1_ != Blocks.brown_mushroom && p_149928_1_ != Blocks.red_mushroom && p_149928_1_ != Blocks.sapling && p_149928_1_ != Blocks.deadbush ? p_149928_1_ == Blocks.tallgrass && p_149928_2_ == 2 : true;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			TileEntityFlowerPot var5 = this.func_149929_e(p_149694_1_, p_149694_2_, p_149694_3_, p_149694_4_);
			return var5 != null && var5.func_145965_a() != null ? var5.func_145965_a() : Items.flower_pot;
		}

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public virtual int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			TileEntityFlowerPot var5 = this.func_149929_e(p_149643_1_, p_149643_2_, p_149643_3_, p_149643_4_);
			return var5 != null && var5.func_145965_a() != null ? var5.func_145966_b() : 0;
		}

///    
///     <summary> * Returns true only if block is flowerPot </summary>
///     
		public virtual bool isFlowerPot()
		{
			get
			{
				return true;
			}
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_) && World.doesBlockHaveSolidTopSurface(p_149742_1_, p_149742_2_, p_149742_3_ - 1, p_149742_4_);
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!World.doesBlockHaveSolidTopSurface(p_149695_1_, p_149695_2_, p_149695_3_ - 1, p_149695_4_))
			{
				this.dropBlockAsItem(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_), 0);
				p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
			}
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			TileEntityFlowerPot var7 = this.func_149929_e(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_);

			if (var7 != null && var7.func_145965_a() != null)
			{
				this.dropBlockAsItem_do(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, new ItemStack(var7.func_145965_a(), 1, var7.func_145966_b()));
			}

			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
		}

///    
///     <summary> * Called when the block is attempted to be harvested </summary>
///     
		public virtual void onBlockHarvested(World p_149681_1_, int p_149681_2_, int p_149681_3_, int p_149681_4_, int p_149681_5_, EntityPlayer p_149681_6_)
		{
			base.onBlockHarvested(p_149681_1_, p_149681_2_, p_149681_3_, p_149681_4_, p_149681_5_, p_149681_6_);

			if (p_149681_6_.capabilities.isCreativeMode)
			{
				TileEntityFlowerPot var7 = this.func_149929_e(p_149681_1_, p_149681_2_, p_149681_3_, p_149681_4_);

				if (var7 != null)
				{
					var7.func_145964_a(Item.getItemById(0), 0);
				}
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.flower_pot;
		}

		private TileEntityFlowerPot func_149929_e(World p_149929_1_, int p_149929_2_, int p_149929_3_, int p_149929_4_)
		{
			TileEntity var5 = p_149929_1_.getTileEntity(p_149929_2_, p_149929_3_, p_149929_4_);
			return var5 != null && var5 is TileEntityFlowerPot ? (TileEntityFlowerPot)var5 : null;
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			object var3 = null;
			sbyte var4 = 0;

			switch (p_149915_2_)
			{
				case 1:
					var3 = Blocks.red_flower;
					var4 = 0;
					break;

				case 2:
					var3 = Blocks.yellow_flower;
					break;

				case 3:
					var3 = Blocks.sapling;
					var4 = 0;
					break;

				case 4:
					var3 = Blocks.sapling;
					var4 = 1;
					break;

				case 5:
					var3 = Blocks.sapling;
					var4 = 2;
					break;

				case 6:
					var3 = Blocks.sapling;
					var4 = 3;
					break;

				case 7:
					var3 = Blocks.red_mushroom;
					break;

				case 8:
					var3 = Blocks.brown_mushroom;
					break;

				case 9:
					var3 = Blocks.cactus;
					break;

				case 10:
					var3 = Blocks.deadbush;
					break;

				case 11:
					var3 = Blocks.tallgrass;
					var4 = 2;
					break;

				case 12:
					var3 = Blocks.sapling;
					var4 = 4;
					break;

				case 13:
					var3 = Blocks.sapling;
					var4 = 5;
				break;
			}

			return new TileEntityFlowerPot(Item.getItemFromBlock((Block)var3), var4);
		}
	}

}