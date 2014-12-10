using System;

namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EnchantmentHelper = DotCraftCore.enchantment.EnchantmentHelper;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using ItemStack = DotCraftCore.item.ItemStack;
	using StatList = DotCraftCore.stats.StatList;
	using EnumSkyBlock = DotCraftCore.world.EnumSkyBlock;
	using IBlockAccess = DotCraftCore.world.IBlockAccess;
	using World = DotCraftCore.world.World;

	public class BlockIce : BlockBreakable
	{
		private const string __OBFID = "CL_00000259";

		public BlockIce() : base("ice", Material.ice, false)
		{
			this.slipperiness = 0.98F;
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Returns which pass should this block be rendered on. 0 for solids and 1 for alpha </summary>
///     
		public virtual int RenderBlockPass
		{
			get
			{
				return 1;
			}
		}

		public override bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, 1 - p_149646_5_);
		}

		public virtual void harvestBlock(World p_149636_1_, EntityPlayer p_149636_2_, int p_149636_3_, int p_149636_4_, int p_149636_5_, int p_149636_6_)
		{
			p_149636_2_.addStat(StatList.mineBlockStatArray[Block.getIdFromBlock(this)], 1);
			p_149636_2_.addExhaustion(0.025F);

			if (this.canSilkHarvest() && EnchantmentHelper.getSilkTouchModifier(p_149636_2_))
			{
				ItemStack var9 = this.createStackedBlock(p_149636_6_);

				if (var9 != null)
				{
					this.dropBlockAsItem_do(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, var9);
				}
			}
			else
			{
				if (p_149636_1_.provider.isHellWorld)
				{
					p_149636_1_.setBlockToAir(p_149636_3_, p_149636_4_, p_149636_5_);
					return;
				}

				int var7 = EnchantmentHelper.getFortuneModifier(p_149636_2_);
				this.dropBlockAsItem(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, p_149636_6_, var7);
				Material var8 = p_149636_1_.getBlock(p_149636_3_, p_149636_4_ - 1, p_149636_5_).Material;

				if (var8.blocksMovement() || var8.Liquid)
				{
					p_149636_1_.setBlock(p_149636_3_, p_149636_4_, p_149636_5_, Blocks.flowing_water);
				}
			}
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (p_149674_1_.getSavedLightValue(EnumSkyBlock.Block, p_149674_2_, p_149674_3_, p_149674_4_) > 11 - this.LightOpacity)
			{
				if (p_149674_1_.provider.isHellWorld)
				{
					p_149674_1_.setBlockToAir(p_149674_2_, p_149674_3_, p_149674_4_);
					return;
				}

				this.dropBlockAsItem(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_), 0);
				p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, Blocks.water);
			}
		}

		public virtual int MobilityFlag
		{
			get
			{
				return 0;
			}
		}
	}

}