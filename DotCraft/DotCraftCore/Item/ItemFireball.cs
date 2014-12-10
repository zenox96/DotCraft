namespace DotCraftCore.Item
{

	using Material = DotCraftCore.block.material.Material;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;

	public class ItemFireball : Item
	{
		private const string __OBFID = "CL_00000029";

		public ItemFireball()
		{
			this.CreativeTab = CreativeTabs.tabMisc;
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (p_77648_3_.isClient)
			{
				return true;
			}
			else
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

				if (!p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_))
				{
					return false;
				}
				else
				{
					if (p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_).Material == Material.air)
					{
						p_77648_3_.playSoundEffect((double)p_77648_4_ + 0.5D, (double)p_77648_5_ + 0.5D, (double)p_77648_6_ + 0.5D, "fire.ignite", 1.0F, itemRand.nextFloat() * 0.4F + 0.8F);
						p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, Blocks.fire);
					}

					if (!p_77648_2_.capabilities.isCreativeMode)
					{
						--p_77648_1_.stackSize;
					}

					return true;
				}
			}
		}
	}

}