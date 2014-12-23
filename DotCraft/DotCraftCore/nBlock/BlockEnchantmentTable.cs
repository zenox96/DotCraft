using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nTileEntity;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockEnchantmentTable : BlockContainer
	{
		protected internal BlockEnchantmentTable() : base(Material.rock)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.75F, 1.0F);
			this.LightOpacity = 0;
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public override void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			base.randomDisplayTick(p_149734_1_, p_149734_2_, p_149734_3_, p_149734_4_, p_149734_5_);

			for (int var6 = p_149734_2_ - 2; var6 <= p_149734_2_ + 2; ++var6)
			{
				for (int var7 = p_149734_4_ - 2; var7 <= p_149734_4_ + 2; ++var7)
				{
					if (var6 > p_149734_2_ - 2 && var6 < p_149734_2_ + 2 && var7 == p_149734_4_ - 1)
					{
						var7 = p_149734_4_ + 2;
					}

					if (p_149734_5_.Next(16) == 0)
					{
						for (int var8 = p_149734_3_; var8 <= p_149734_3_ + 1; ++var8)
						{
							if (p_149734_1_.getBlock(var6, var8, var7) == Blocks.bookshelf)
							{
								if (!p_149734_1_.isAirBlock((var6 - p_149734_2_) / 2 + p_149734_2_, var8, (var7 - p_149734_4_) / 2 + p_149734_4_))
								{
									break;
								}

								p_149734_1_.spawnParticle("enchantmenttable", (double)p_149734_2_ + 0.5D, (double)p_149734_3_ + 2.0D, (double)p_149734_4_ + 0.5D, (double)(var6 - p_149734_2_) + p_149734_5_.NextDouble() - 0.5D, (double)(var8 - p_149734_3_) - p_149734_5_.NextDouble() - 1.0D, (double)(var7 - p_149734_4_) + p_149734_5_.NextDouble() - 0.5D);
							}
						}
					}
				}
			}
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public override TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityEnchantmentTable();
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
				TileEntityEnchantmentTable var10 = (TileEntityEnchantmentTable)p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_);
				p_149727_5_.displayGUIEnchantment(p_149727_2_, p_149727_3_, p_149727_4_, var10.func_145921_b() ? var10.func_145919_a() : null);
				return true;
			}
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public override void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			base.onBlockPlacedBy(p_149689_1_, p_149689_2_, p_149689_3_, p_149689_4_, p_149689_5_, p_149689_6_);

			if (p_149689_6_.hasDisplayName())
			{
				((TileEntityEnchantmentTable)p_149689_1_.getTileEntity(p_149689_2_, p_149689_3_, p_149689_4_)).func_145920_a(p_149689_6_.DisplayName);
			}
		}
	}
}