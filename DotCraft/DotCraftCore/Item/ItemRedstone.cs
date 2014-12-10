namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;

	public class ItemRedstone : Item
	{
		private const string __OBFID = "CL_00000058";

		public ItemRedstone()
		{
			this.CreativeTab = CreativeTabs.tabRedstone;
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_) != Blocks.snow_layer)
			{
				if (p_77648_7_ == 0)
				{
					--p_77648_5_;
				}

				if (p_77648_7_ == 1)
				{
					++p_77648_5_;
				}

				if (p_77648_7_ == 2)
				{
					--p_77648_6_;
				}

				if (p_77648_7_ == 3)
				{
					++p_77648_6_;
				}

				if (p_77648_7_ == 4)
				{
					--p_77648_4_;
				}

				if (p_77648_7_ == 5)
				{
					++p_77648_4_;
				}

				if (!p_77648_3_.isAirBlock(p_77648_4_, p_77648_5_, p_77648_6_))
				{
					return false;
				}
			}

			if (!p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_))
			{
				return false;
			}
			else
			{
				if (Blocks.redstone_wire.canPlaceBlockAt(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_))
				{
					--p_77648_1_.stackSize;
					p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, Blocks.redstone_wire);
				}

				return true;
			}
		}
	}

}