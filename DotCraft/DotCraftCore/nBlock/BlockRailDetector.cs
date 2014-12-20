using DotCraftCore.nCommand;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nItem;
using DotCraftCore.nInventory;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockRailDetector : BlockRailBase
	{
		public BlockRailDetector() : base(true)
		{
			this.TickRandomly = true;
		}

		public virtual int func_149738_a(World p_149738_1_)
		{
			return 20;
		}

///    
///     <summary> * Can this block provide power. Only wire currently seems to have this change based on its state. </summary>
///     
		public virtual bool canProvidePower()
		{
			return true;
		}

		public virtual void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
		{
			if (!p_149670_1_.isClient)
			{
				int var6 = p_149670_1_.getBlockMetadata(p_149670_2_, p_149670_3_, p_149670_4_);

				if ((var6 & 8) == 0)
				{
					this.func_150054_a(p_149670_1_, p_149670_2_, p_149670_3_, p_149670_4_, var6);
				}
			}
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!p_149674_1_.isClient)
			{
				int var6 = p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_);

				if ((var6 & 8) != 0)
				{
					this.func_150054_a(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, var6);
				}
			}
		}

		public virtual int isProvidingWeakPower(IBlockAccess p_149709_1_, int p_149709_2_, int p_149709_3_, int p_149709_4_, int p_149709_5_)
		{
			return (p_149709_1_.getBlockMetadata(p_149709_2_, p_149709_3_, p_149709_4_) & 8) != 0 ? 15 : 0;
		}

		public virtual int isProvidingStrongPower(IBlockAccess p_149748_1_, int p_149748_2_, int p_149748_3_, int p_149748_4_, int p_149748_5_)
		{
			return (p_149748_1_.getBlockMetadata(p_149748_2_, p_149748_3_, p_149748_4_) & 8) == 0 ? 0 : (p_149748_5_ == 1 ? 15 : 0);
		}

		private void func_150054_a(World p_150054_1_, int p_150054_2_, int p_150054_3_, int p_150054_4_, int p_150054_5_)
		{
			bool var6 = (p_150054_5_ & 8) != 0;
			bool var7 = false;
			float var8 = 0.125F;
			IList var9 = p_150054_1_.getEntitiesWithinAABB(typeof(EntityMinecart), AxisAlignedBB.getBoundingBox((double)((float)p_150054_2_ + var8), (double)p_150054_3_, (double)((float)p_150054_4_ + var8), (double)((float)(p_150054_2_ + 1) - var8), (double)((float)(p_150054_3_ + 1) - var8), (double)((float)(p_150054_4_ + 1) - var8)));

			if (!var9.Count == 0)
			{
				var7 = true;
			}

			if (var7 && !var6)
			{
				p_150054_1_.setBlockMetadataWithNotify(p_150054_2_, p_150054_3_, p_150054_4_, p_150054_5_ | 8, 3);
				p_150054_1_.notifyBlocksOfNeighborChange(p_150054_2_, p_150054_3_, p_150054_4_, this);
				p_150054_1_.notifyBlocksOfNeighborChange(p_150054_2_, p_150054_3_ - 1, p_150054_4_, this);
				p_150054_1_.markBlockRangeForRenderUpdate(p_150054_2_, p_150054_3_, p_150054_4_, p_150054_2_, p_150054_3_, p_150054_4_);
			}

			if (!var7 && var6)
			{
				p_150054_1_.setBlockMetadataWithNotify(p_150054_2_, p_150054_3_, p_150054_4_, p_150054_5_ & 7, 3);
				p_150054_1_.notifyBlocksOfNeighborChange(p_150054_2_, p_150054_3_, p_150054_4_, this);
				p_150054_1_.notifyBlocksOfNeighborChange(p_150054_2_, p_150054_3_ - 1, p_150054_4_, this);
				p_150054_1_.markBlockRangeForRenderUpdate(p_150054_2_, p_150054_3_, p_150054_4_, p_150054_2_, p_150054_3_, p_150054_4_);
			}

			if (var7)
			{
				p_150054_1_.scheduleBlockUpdate(p_150054_2_, p_150054_3_, p_150054_4_, this, this.func_149738_a(p_150054_1_));
			}

			p_150054_1_.func_147453_f(p_150054_2_, p_150054_3_, p_150054_4_, this);
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
			this.func_150054_a(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_, p_149726_1_.getBlockMetadata(p_149726_2_, p_149726_3_, p_149726_4_));
		}

		public virtual bool hasComparatorInputOverride()
		{
			return true;
		}

		public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			if ((p_149736_1_.getBlockMetadata(p_149736_2_, p_149736_3_, p_149736_4_) & 8) > 0)
			{
				float var6 = 0.125F;
				IList var7 = p_149736_1_.getEntitiesWithinAABB(typeof(EntityMinecartCommandBlock), AxisAlignedBB.getBoundingBox((double)((float)p_149736_2_ + var6), (double)p_149736_3_, (double)((float)p_149736_4_ + var6), (double)((float)(p_149736_2_ + 1) - var6), (double)((float)(p_149736_3_ + 1) - var6), (double)((float)(p_149736_4_ + 1) - var6)));

				if (var7.Count > 0)
				{
					return ((EntityMinecartCommandBlock)var7[0]).func_145822_e().func_145760_g();
				}

				IList var8 = p_149736_1_.selectEntitiesWithinAABB(typeof(EntityMinecart), AxisAlignedBB.getBoundingBox((double)((float)p_149736_2_ + var6), (double)p_149736_3_, (double)((float)p_149736_4_ + var6), (double)((float)(p_149736_2_ + 1) - var6), (double)((float)(p_149736_3_ + 1) - var6), (double)((float)(p_149736_4_ + 1) - var6)), IEntitySelector.selectInventories);

				if (var8.Count > 0)
				{
					return Container.calcRedstoneFromInventory((IInventory)var8[0]);
				}
			}

			return 0;
		}
	}
}