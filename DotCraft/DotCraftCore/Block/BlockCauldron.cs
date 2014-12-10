using System;
using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using Entity = DotCraftCore.entity.Entity;
	using EntityItem = DotCraftCore.entity.item.EntityItem;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemArmor = DotCraftCore.item.ItemArmor;
	using ItemStack = DotCraftCore.item.ItemStack;
	using AxisAlignedBB = DotCraftCore.util.AxisAlignedBB;
	using IIcon = DotCraftCore.util.IIcon;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public class BlockCauldron : Block
	{
		private IIcon field_150029_a;
		private IIcon field_150028_b;
		private IIcon field_150030_M;
		private const string __OBFID = "CL_00000213";

		public BlockCauldron() : base(Material.iron)
		{
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 1 ? this.field_150028_b : (p_149691_1_ == 0 ? this.field_150030_M : this.blockIcon);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_150029_a = p_149651_1_.registerIcon(this.TextureName + "_" + "inner");
			this.field_150028_b = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.field_150030_M = p_149651_1_.registerIcon(this.TextureName + "_" + "bottom");
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
		}

		public static IIcon func_150026_e(string p_150026_0_)
		{
			return p_150026_0_.Equals("inner") ? Blocks.cauldron.field_150029_a : (p_150026_0_.Equals("bottom") ? Blocks.cauldron.field_150030_M : null);
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.3125F, 1.0F);
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
			this.setBlockBoundsForItemRender();
		}

///    
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public virtual void setBlockBoundsForItemRender()
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
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
				return 24;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

		public virtual void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
		{
			int var6 = func_150027_b(p_149670_1_.getBlockMetadata(p_149670_2_, p_149670_3_, p_149670_4_));
			float var7 = (float)p_149670_3_ + (6.0F + (float)(3 * var6)) / 16.0F;

			if (!p_149670_1_.isClient && p_149670_5_.Burning && var6 > 0 && p_149670_5_.boundingBox.minY <= (double)var7)
			{
				p_149670_5_.extinguish();
				this.func_150024_a(p_149670_1_, p_149670_2_, p_149670_3_, p_149670_4_, var6 - 1);
			}
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
				ItemStack var10 = p_149727_5_.inventory.CurrentItem;

				if (var10 == null)
				{
					return true;
				}
				else
				{
					int var11 = p_149727_1_.getBlockMetadata(p_149727_2_, p_149727_3_, p_149727_4_);
					int var12 = func_150027_b(var11);

					if (var10.Item == Items.water_bucket)
					{
						if (var12 < 3)
						{
							if (!p_149727_5_.capabilities.isCreativeMode)
							{
								p_149727_5_.inventory.setInventorySlotContents(p_149727_5_.inventory.currentItem, new ItemStack(Items.bucket));
							}

							this.func_150024_a(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, 3);
						}

						return true;
					}
					else
					{
						if (var10.Item == Items.glass_bottle)
						{
							if (var12 > 0)
							{
								if (!p_149727_5_.capabilities.isCreativeMode)
								{
									ItemStack var13 = new ItemStack(Items.potionitem, 1, 0);

									if (!p_149727_5_.inventory.addItemStackToInventory(var13))
									{
										p_149727_1_.spawnEntityInWorld(new EntityItem(p_149727_1_, (double)p_149727_2_ + 0.5D, (double)p_149727_3_ + 1.5D, (double)p_149727_4_ + 0.5D, var13));
									}
									else if (p_149727_5_ is EntityPlayerMP)
									{
										((EntityPlayerMP)p_149727_5_).sendContainerToPlayer(p_149727_5_.inventoryContainer);
									}

									--var10.stackSize;

									if (var10.stackSize <= 0)
									{
										p_149727_5_.inventory.setInventorySlotContents(p_149727_5_.inventory.currentItem, (ItemStack)null);
									}
								}

								this.func_150024_a(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, var12 - 1);
							}
						}
						else if (var12 > 0 && var10.Item is ItemArmor && ((ItemArmor)var10.Item).ArmorMaterial == ItemArmor.ArmorMaterial.CLOTH)
						{
							ItemArmor var14 = (ItemArmor)var10.Item;
							var14.removeColor(var10);
							this.func_150024_a(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, var12 - 1);
							return true;
						}

						return false;
					}
				}
			}
		}

		public virtual void func_150024_a(World p_150024_1_, int p_150024_2_, int p_150024_3_, int p_150024_4_, int p_150024_5_)
		{
			p_150024_1_.setBlockMetadataWithNotify(p_150024_2_, p_150024_3_, p_150024_4_, MathHelper.clamp_int(p_150024_5_, 0, 3), 2);
			p_150024_1_.func_147453_f(p_150024_2_, p_150024_3_, p_150024_4_, this);
		}

///    
///     <summary> * currently only used by BlockCauldron to incrament meta-data during rain </summary>
///     
		public virtual void fillWithRain(World p_149639_1_, int p_149639_2_, int p_149639_3_, int p_149639_4_)
		{
			if (p_149639_1_.rand.Next(20) == 1)
			{
				int var5 = p_149639_1_.getBlockMetadata(p_149639_2_, p_149639_3_, p_149639_4_);

				if (var5 < 3)
				{
					p_149639_1_.setBlockMetadataWithNotify(p_149639_2_, p_149639_3_, p_149639_4_, var5 + 1, 2);
				}
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.cauldron;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Items.cauldron;
		}

		public virtual bool hasComparatorInputOverride()
		{
			return true;
		}

		public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			int var6 = p_149736_1_.getBlockMetadata(p_149736_2_, p_149736_3_, p_149736_4_);
			return func_150027_b(var6);
		}

		public static int func_150027_b(int p_150027_0_)
		{
			return p_150027_0_;
		}

		public static float func_150025_c(int p_150025_0_)
		{
			int var1 = MathHelper.clamp_int(p_150025_0_, 0, 3);
			return (float)(6 + 3 * var1) / 16.0F;
		}
	}

}