using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nItem;
using DotCraftCore.nTileEntity;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockRedstoneComparator : BlockRedstoneDiode, ITileEntityProvider
	{
		

		public BlockRedstoneComparator(bool p_i45399_1_) : base(p_i45399_1_)
		{
			this.isBlockContainer = true;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.comparator;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Items.comparator;
		}

		protected internal override int func_149901_b(int p_149901_1_)
		{
			return 2;
		}

		protected internal override BlockRedstoneDiode func_149906_e()
		{
			return Blocks.powered_comparator;
		}

		protected internal override BlockRedstoneDiode func_149898_i()
		{
			return Blocks.unpowered_comparator;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return 37;
			}
		}

		protected internal override bool func_149905_c(int p_149905_1_)
		{
			return this.field_149914_a || (p_149905_1_ & 8) != 0;
		}

		protected internal override int func_149904_f(IBlockAccess p_149904_1_, int p_149904_2_, int p_149904_3_, int p_149904_4_, int p_149904_5_)
		{
			return this.func_149971_e(p_149904_1_, p_149904_2_, p_149904_3_, p_149904_4_).func_145996_a();
		}

		private int func_149970_j(World p_149970_1_, int p_149970_2_, int p_149970_3_, int p_149970_4_, int p_149970_5_)
		{
			return !this.func_149969_d(p_149970_5_) ? this.func_149903_h(p_149970_1_, p_149970_2_, p_149970_3_, p_149970_4_, p_149970_5_) : Math.Max(this.func_149903_h(p_149970_1_, p_149970_2_, p_149970_3_, p_149970_4_, p_149970_5_) - this.func_149902_h(p_149970_1_, p_149970_2_, p_149970_3_, p_149970_4_, p_149970_5_), 0);
		}

		public virtual bool func_149969_d(int p_149969_1_)
		{
			return (p_149969_1_ & 4) == 4;
		}

		protected internal override bool func_149900_a(World p_149900_1_, int p_149900_2_, int p_149900_3_, int p_149900_4_, int p_149900_5_)
		{
			int var6 = this.func_149903_h(p_149900_1_, p_149900_2_, p_149900_3_, p_149900_4_, p_149900_5_);

			if (var6 >= 15)
			{
				return true;
			}
			else if (var6 == 0)
			{
				return false;
			}
			else
			{
				int var7 = this.func_149902_h(p_149900_1_, p_149900_2_, p_149900_3_, p_149900_4_, p_149900_5_);
				return var7 == 0 ? true : var6 >= var7;
			}
		}

		protected internal override int func_149903_h(World p_149903_1_, int p_149903_2_, int p_149903_3_, int p_149903_4_, int p_149903_5_)
		{
			int var6 = base.func_149903_h(p_149903_1_, p_149903_2_, p_149903_3_, p_149903_4_, p_149903_5_);
			int var7 = func_149895_l(p_149903_5_);
			int var8 = p_149903_2_ + Direction.offsetX[var7];
			int var9 = p_149903_4_ + Direction.offsetZ[var7];
			Block var10 = p_149903_1_.getBlock(var8, p_149903_3_, var9);

			if (var10.hasComparatorInputOverride())
			{
				var6 = var10.getComparatorInputOverride(p_149903_1_, var8, p_149903_3_, var9, Direction.rotateOpposite[var7]);
			}
			else if (var6 < 15 && var10.isBlockNormalCube())
			{
				var8 += Direction.offsetX[var7];
				var9 += Direction.offsetZ[var7];
				var10 = p_149903_1_.getBlock(var8, p_149903_3_, var9);

				if (var10.hasComparatorInputOverride())
				{
					var6 = var10.getComparatorInputOverride(p_149903_1_, var8, p_149903_3_, var9, Direction.rotateOpposite[var7]);
				}
			}

			return var6;
		}

		public virtual TileEntityComparator func_149971_e(IBlockAccess p_149971_1_, int p_149971_2_, int p_149971_3_, int p_149971_4_)
		{
			return (TileEntityComparator)p_149971_1_.getTileEntity(p_149971_2_, p_149971_3_, p_149971_4_);
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			int var10 = p_149727_1_.getBlockMetadata(p_149727_2_, p_149727_3_, p_149727_4_);
			bool var11 = this.field_149914_a | (var10 & 8) != 0;
			bool var12 = !this.func_149969_d(var10);
			int var13 = var12 ? 4 : 0;
			var13 |= var11 ? 8 : 0;
			p_149727_1_.playSoundEffect((double)p_149727_2_ + 0.5D, (double)p_149727_3_ + 0.5D, (double)p_149727_4_ + 0.5D, "random.click", 0.3F, var12 ? 0.55F : 0.5F);
			p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_, p_149727_4_, var13 | var10 & 3, 2);
			this.func_149972_c(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, p_149727_1_.rand);
			return true;
		}

		protected internal override void func_149897_b(World p_149897_1_, int p_149897_2_, int p_149897_3_, int p_149897_4_, Block p_149897_5_)
		{
			if (!p_149897_1_.func_147477_a(p_149897_2_, p_149897_3_, p_149897_4_, this))
			{
				int var6 = p_149897_1_.getBlockMetadata(p_149897_2_, p_149897_3_, p_149897_4_);
				int var7 = this.func_149970_j(p_149897_1_, p_149897_2_, p_149897_3_, p_149897_4_, var6);
				int var8 = this.func_149971_e(p_149897_1_, p_149897_2_, p_149897_3_, p_149897_4_).func_145996_a();

				if (var7 != var8 || this.func_149905_c(var6) != this.func_149900_a(p_149897_1_, p_149897_2_, p_149897_3_, p_149897_4_, var6))
				{
					if (this.func_149912_i(p_149897_1_, p_149897_2_, p_149897_3_, p_149897_4_, var6))
					{
						p_149897_1_.func_147454_a(p_149897_2_, p_149897_3_, p_149897_4_, this, this.func_149901_b(0), -1);
					}
					else
					{
						p_149897_1_.func_147454_a(p_149897_2_, p_149897_3_, p_149897_4_, this, this.func_149901_b(0), 0);
					}
				}
			}
		}

		private void func_149972_c(World p_149972_1_, int p_149972_2_, int p_149972_3_, int p_149972_4_, Random p_149972_5_)
		{
			int var6 = p_149972_1_.getBlockMetadata(p_149972_2_, p_149972_3_, p_149972_4_);
			int var7 = this.func_149970_j(p_149972_1_, p_149972_2_, p_149972_3_, p_149972_4_, var6);
			int var8 = this.func_149971_e(p_149972_1_, p_149972_2_, p_149972_3_, p_149972_4_).func_145996_a();
			this.func_149971_e(p_149972_1_, p_149972_2_, p_149972_3_, p_149972_4_).func_145995_a(var7);

			if (var8 != var7 || !this.func_149969_d(var6))
			{
				bool var9 = this.func_149900_a(p_149972_1_, p_149972_2_, p_149972_3_, p_149972_4_, var6);
				bool var10 = this.field_149914_a || (var6 & 8) != 0;

				if (var10 && !var9)
				{
					p_149972_1_.setBlockMetadataWithNotify(p_149972_2_, p_149972_3_, p_149972_4_, var6 & -9, 2);
				}
				else if (!var10 && var9)
				{
					p_149972_1_.setBlockMetadataWithNotify(p_149972_2_, p_149972_3_, p_149972_4_, var6 | 8, 2);
				}

				this.func_149911_e(p_149972_1_, p_149972_2_, p_149972_3_, p_149972_4_);
			}
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (this.field_149914_a)
			{
				int var6 = p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_);
				p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, this.func_149898_i(), var6 | 8, 4);
			}

			this.func_149972_c(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_5_);
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
			p_149726_1_.setTileEntity(p_149726_2_, p_149726_3_, p_149726_4_, this.createNewTileEntity(p_149726_1_, 0));
		}

		public virtual void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
			p_149749_1_.removeTileEntity(p_149749_2_, p_149749_3_, p_149749_4_);
			this.func_149911_e(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_);
		}

		public virtual bool onBlockEventReceived(World p_149696_1_, int p_149696_2_, int p_149696_3_, int p_149696_4_, int p_149696_5_, int p_149696_6_)
		{
			base.onBlockEventReceived(p_149696_1_, p_149696_2_, p_149696_3_, p_149696_4_, p_149696_5_, p_149696_6_);
			TileEntity var7 = p_149696_1_.getTileEntity(p_149696_2_, p_149696_3_, p_149696_4_);
			return var7 != null ? var7.receiveClientEvent(p_149696_5_, p_149696_6_) : false;
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityComparator();
		}
	}

}