using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity.nItem;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nNBT;
using DotCraftCore.nTileEntity;
using DotCraftCore.nWorld;
namespace DotCraftCore.nBlock
{
	public class BlockJukebox : BlockContainer
	{
		protected internal BlockJukebox() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public override bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (p_149727_1_.getBlockMetadata(p_149727_2_, p_149727_3_, p_149727_4_) == 0)
			{
				return false;
			}
			else
			{
				this.func_149925_e(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_);
				return true;
			}
		}

		public override void func_149926_b(World p_149926_1_, int p_149926_2_, int p_149926_3_, int p_149926_4_, ItemStack p_149926_5_)
		{
			if (!p_149926_1_.isClient)
			{
				BlockJukebox.TileEntityJukebox var6 = (BlockJukebox.TileEntityJukebox)p_149926_1_.getTileEntity(p_149926_2_, p_149926_3_, p_149926_4_);

				if (var6 != null)
				{
					var6.func_145857_a(p_149926_5_.copy());
					p_149926_1_.setBlockMetadataWithNotify(p_149926_2_, p_149926_3_, p_149926_4_, 1, 2);
				}
			}
		}

		public override void func_149925_e(World p_149925_1_, int p_149925_2_, int p_149925_3_, int p_149925_4_)
		{
			if (!p_149925_1_.isClient)
			{
				BlockJukebox.TileEntityJukebox var5 = (BlockJukebox.TileEntityJukebox)p_149925_1_.getTileEntity(p_149925_2_, p_149925_3_, p_149925_4_);

				if (var5 != null)
				{
					ItemStack var6 = var5.func_145856_a();

					if (var6 != null)
					{
						p_149925_1_.playAuxSFX(1005, p_149925_2_, p_149925_3_, p_149925_4_, 0);
						p_149925_1_.playRecord((string)null, p_149925_2_, p_149925_3_, p_149925_4_);
						var5.func_145857_a((ItemStack)null);
						p_149925_1_.setBlockMetadataWithNotify(p_149925_2_, p_149925_3_, p_149925_4_, 0, 2);
						float var7 = 0.7F;
						double var8 = (double)(p_149925_1_.rand.NextDouble() * var7) + (double)(1.0F - var7) * 0.5D;
						double var10 = (double)(p_149925_1_.rand.NextDouble() * var7) + (double)(1.0F - var7) * 0.2D + 0.6D;
						double var12 = (double)(p_149925_1_.rand.NextDouble() * var7) + (double)(1.0F - var7) * 0.5D;
						ItemStack var14 = var6.copy();
						EntityItem var15 = new EntityItem(p_149925_1_, (double)p_149925_2_ + var8, (double)p_149925_3_ + var10, (double)p_149925_4_ + var12, var14);
						var15.delayBeforeCanPickup = 10;
						p_149925_1_.spawnEntityInWorld(var15);
					}
				}
			}
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			this.func_149925_e(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_);
			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public override void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			if (!p_149690_1_.isClient)
			{
				base.dropBlockAsItemWithChance(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, p_149690_5_, p_149690_6_, 0);
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public override TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new BlockJukebox.TileEntityJukebox();
		}

		public override bool hasComparatorInputOverride()
		{
			return true;
		}

		public override int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			ItemStack var6 = ((BlockJukebox.TileEntityJukebox)p_149736_1_.getTileEntity(p_149736_2_, p_149736_3_, p_149736_4_)).func_145856_a();
			return var6 == null ? 0 : Item.getIdFromItem(var6.Item) + 1 - Item.getIdFromItem(Items.record_13);
		}

		public class TileEntityJukebox : TileEntity
		{
			private ItemStack field_145858_a;
			

			public override void readFromNBT(NBTTagCompound p_145839_1_)
			{
				base.readFromNBT(p_145839_1_);

				if (p_145839_1_.func_150297_b("RecordItem", 10))
				{
					this.func_145857_a(ItemStack.loadItemStackFromNBT(p_145839_1_.getCompoundTag("RecordItem")));
				}
				else if (p_145839_1_.getInteger("Record") > 0)
				{
					this.func_145857_a(new ItemStack(Item.getItemById(p_145839_1_.getInteger("Record")), 1, 0));
				}
			}

			public override void writeToNBT(NBTTagCompound p_145841_1_)
			{
				base.writeToNBT(p_145841_1_);

				if (this.func_145856_a() != null)
				{
					p_145841_1_.setTag("RecordItem", this.func_145856_a().writeToNBT(new NBTTagCompound()));
					p_145841_1_.setInteger("Record", Item.getIdFromItem(this.func_145856_a().Item));
				}
			}

			public override ItemStack func_145856_a()
			{
				return this.field_145858_a;
			}

			public override void func_145857_a(ItemStack p_145857_1_)
			{
				this.field_145858_a = p_145857_1_;
				this.onInventoryChanged();
			}
		}
	}

}