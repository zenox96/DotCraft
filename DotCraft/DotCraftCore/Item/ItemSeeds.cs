namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using World = DotCraftCore.World.World;

	public class ItemSeeds : Item
	{
		private Block field_150925_a;

	/// <summary> BlockID of the block the seeds can be planted on.  </summary>
		private Block soilBlockID;
		

		public ItemSeeds(Block p_i45352_1_, Block p_i45352_2_)
		{
			this.field_150925_a = p_i45352_1_;
			this.soilBlockID = p_i45352_2_;
			this.CreativeTab = CreativeTabs.tabMaterials;
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (p_77648_7_ != 1)
			{
				return false;
			}
			else if (p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_) && p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_ + 1, p_77648_6_, p_77648_7_, p_77648_1_))
			{
				if (p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_) == this.soilBlockID && p_77648_3_.isAirBlock(p_77648_4_, p_77648_5_ + 1, p_77648_6_))
				{
					p_77648_3_.setBlock(p_77648_4_, p_77648_5_ + 1, p_77648_6_, this.field_150925_a);
					--p_77648_1_.stackSize;
					return true;
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
	}

}