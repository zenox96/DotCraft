using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nItem;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nNBT;
using DotCraftCore.nTileEntity;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using DotCraftUtil;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockFurnace : BlockContainer
	{
		private readonly Random field_149933_a = new Random();
		private readonly bool field_149932_b;
		private static bool field_149934_M;

		protected internal BlockFurnace(bool p_i45407_1_) : base(Material.rock)
		{
			this.field_149932_b = p_i45407_1_;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.furnace);
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
			this.func_149930_e(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
		}

		private void func_149930_e(World p_149930_1_, int p_149930_2_, int p_149930_3_, int p_149930_4_)
		{
			if (!p_149930_1_.isClient)
			{
				Block var5 = p_149930_1_.getBlock(p_149930_2_, p_149930_3_, p_149930_4_ - 1);
				Block var6 = p_149930_1_.getBlock(p_149930_2_, p_149930_3_, p_149930_4_ + 1);
				Block var7 = p_149930_1_.getBlock(p_149930_2_ - 1, p_149930_3_, p_149930_4_);
				Block var8 = p_149930_1_.getBlock(p_149930_2_ + 1, p_149930_3_, p_149930_4_);
				sbyte var9 = 3;

				if (var5.OpaqueCube && !var6.OpaqueCube)
				{
					var9 = 3;
				}

				if (var6.OpaqueCube && !var5.OpaqueCube)
				{
					var9 = 2;
				}

				if (var7.OpaqueCube && !var8.OpaqueCube)
				{
					var9 = 5;
				}

				if (var8.OpaqueCube && !var7.OpaqueCube)
				{
					var9 = 4;
				}

				p_149930_1_.setBlockMetadataWithNotify(p_149930_2_, p_149930_3_, p_149930_4_, var9, 2);
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public override void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			if (this.field_149932_b)
			{
				int var6 = p_149734_1_.getBlockMetadata(p_149734_2_, p_149734_3_, p_149734_4_);
				float var7 = (float)p_149734_2_ + 0.5F;
				float var8 = (float)p_149734_3_ + 0.0F + p_149734_5_.NextFloat() * 6.0F / 16.0F;
				float var9 = (float)p_149734_4_ + 0.5F;
				float var10 = 0.52F;
				float var11 = p_149734_5_.NextFloat() * 0.6F - 0.3F;

				if (var6 == 4)
				{
					p_149734_1_.spawnParticle("smoke", (double)(var7 - var10), (double)var8, (double)(var9 + var11), 0.0D, 0.0D, 0.0D);
					p_149734_1_.spawnParticle("flame", (double)(var7 - var10), (double)var8, (double)(var9 + var11), 0.0D, 0.0D, 0.0D);
				}
				else if (var6 == 5)
				{
					p_149734_1_.spawnParticle("smoke", (double)(var7 + var10), (double)var8, (double)(var9 + var11), 0.0D, 0.0D, 0.0D);
					p_149734_1_.spawnParticle("flame", (double)(var7 + var10), (double)var8, (double)(var9 + var11), 0.0D, 0.0D, 0.0D);
				}
				else if (var6 == 2)
				{
					p_149734_1_.spawnParticle("smoke", (double)(var7 + var11), (double)var8, (double)(var9 - var10), 0.0D, 0.0D, 0.0D);
					p_149734_1_.spawnParticle("flame", (double)(var7 + var11), (double)var8, (double)(var9 - var10), 0.0D, 0.0D, 0.0D);
				}
				else if (var6 == 3)
				{
					p_149734_1_.spawnParticle("smoke", (double)(var7 + var11), (double)var8, (double)(var9 + var10), 0.0D, 0.0D, 0.0D);
					p_149734_1_.spawnParticle("flame", (double)(var7 + var11), (double)var8, (double)(var9 + var10), 0.0D, 0.0D, 0.0D);
				}
			}
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public override bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (p_149727_1_.isClient)
			{
				return true;
			}
			else
			{
				TileEntityFurnace var10 = (TileEntityFurnace)p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_);

				if (var10 != null)
				{
					p_149727_5_.func_146101_a(var10);
				}

				return true;
			}
		}

		public static void func_149931_a(bool p_149931_0_, World p_149931_1_, int p_149931_2_, int p_149931_3_, int p_149931_4_)
		{
			int var5 = p_149931_1_.getBlockMetadata(p_149931_2_, p_149931_3_, p_149931_4_);
			TileEntity var6 = p_149931_1_.getTileEntity(p_149931_2_, p_149931_3_, p_149931_4_);
			field_149934_M = true;

			if (p_149931_0_)
			{
				p_149931_1_.setBlock(p_149931_2_, p_149931_3_, p_149931_4_, Blocks.lit_furnace);
			}
			else
			{
				p_149931_1_.setBlock(p_149931_2_, p_149931_3_, p_149931_4_, Blocks.furnace);
			}

			field_149934_M = false;
			p_149931_1_.setBlockMetadataWithNotify(p_149931_2_, p_149931_3_, p_149931_4_, var5, 2);

			if (var6 != null)
			{
				var6.validate();
				p_149931_1_.setTileEntity(p_149931_2_, p_149931_3_, p_149931_4_, var6);
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public override TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityFurnace();
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public override void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			int var7 = MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3;

			if (var7 == 0)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 2, 2);
			}

			if (var7 == 1)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 5, 2);
			}

			if (var7 == 2)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 3, 2);
			}

			if (var7 == 3)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 4, 2);
			}

			if (p_149689_6_.hasDisplayName())
			{
				((TileEntityFurnace)p_149689_1_.getTileEntity(p_149689_2_, p_149689_3_, p_149689_4_)).func_145951_a(p_149689_6_.DisplayName);
			}
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			if (!field_149934_M)
			{
				TileEntityFurnace var7 = (TileEntityFurnace)p_149749_1_.getTileEntity(p_149749_2_, p_149749_3_, p_149749_4_);

				if (var7 != null)
				{
					for (int var8 = 0; var8 < var7.SizeInventory; ++var8)
					{
						ItemStack var9 = var7.getStackInSlot(var8);

						if (var9 != null)
						{
							float var10 = this.field_149933_a.NextFloat() * 0.8F + 0.1F;
							float var11 = this.field_149933_a.NextFloat() * 0.8F + 0.1F;
							float var12 = this.field_149933_a.NextFloat() * 0.8F + 0.1F;

							while (var9.stackSize > 0)
							{
								int var13 = this.field_149933_a.Next(21) + 10;

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

								double var15 = 0.05D;
								var14.motionX = this.field_149933_a.NextGaussian() * var15;
								var14.motionY = this.field_149933_a.NextGaussian() * var15 + 0.2D;
								var14.motionZ = this.field_149933_a.NextGaussian() * var15;
								p_149749_1_.spawnEntityInWorld(var14);
							}
						}
					}

					p_149749_1_.func_147453_f(p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_);
				}
			}

			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
		}

		public override bool hasComparatorInputOverride()
		{
			return true;
		}

		public override int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			return Container.calcRedstoneFromInventory((IInventory)p_149736_1_.getTileEntity(p_149736_2_, p_149736_3_, p_149736_4_));
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public override Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemFromBlock(Blocks.furnace);
		}
	}

}