using System;

namespace DotCraftCore.nBlock
{

	
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using EnumSkyBlock = DotCraftCore.nWorld.EnumSkyBlock;
	using World = DotCraftCore.nWorld.World;

	public class BlockSnowBlock : Block
	{
		

		protected internal BlockSnowBlock() : base(Material.craftedSnow)
		{
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.snowball;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 4;
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (p_149674_1_.getSavedLightValue(EnumSkyBlock.Block, p_149674_2_, p_149674_3_, p_149674_4_) > 11)
			{
				this.dropBlockAsItem(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_), 0);
				p_149674_1_.setBlockToAir(p_149674_2_, p_149674_3_, p_149674_4_);
			}
		}
	}

}