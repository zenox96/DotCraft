using System;

namespace DotCraftCore.Block
{

	
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using StatList = DotCraftCore.stats.StatList;
	using World = DotCraftCore.world.World;

	public class BlockDeadBush : BlockBush
	{
		private const string __OBFID = "CL_00000224";

		protected internal BlockDeadBush() : base(Material.vine)
		{
			float var1 = 0.4F;
			this.setBlockBounds(0.5F - var1, 0.0F, 0.5F - var1, 0.5F + var1, 0.8F, 0.5F + var1);
		}

		protected internal override bool func_149854_a(Block p_149854_1_)
		{
			return p_149854_1_ == Blocks.sand || p_149854_1_ == Blocks.hardened_clay || p_149854_1_ == Blocks.stained_hardened_clay || p_149854_1_ == Blocks.dirt;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return null;
		}

		public virtual void harvestBlock(World p_149636_1_, EntityPlayer p_149636_2_, int p_149636_3_, int p_149636_4_, int p_149636_5_, int p_149636_6_)
		{
			if (!p_149636_1_.isClient && p_149636_2_.CurrentEquippedItem != null && p_149636_2_.CurrentEquippedItem.Item == Items.shears)
			{
				p_149636_2_.addStat(StatList.mineBlockStatArray[Block.getIdFromBlock(this)], 1);
				this.dropBlockAsItem_do(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, new ItemStack(Blocks.deadbush, 1, p_149636_6_));
			}
			else
			{
				base.harvestBlock(p_149636_1_, p_149636_2_, p_149636_3_, p_149636_4_, p_149636_5_, p_149636_6_);
			}
		}
	}

}