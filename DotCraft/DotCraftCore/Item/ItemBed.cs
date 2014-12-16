namespace DotCraftCore.Item
{

	using BlockBed = DotCraftCore.block.BlockBed;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class ItemBed : Item
	{
		

		public ItemBed()
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
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
			else if (p_77648_7_ != 1)
			{
				return false;
			}
			else
			{
				++p_77648_5_;
				BlockBed var11 = (BlockBed)Blocks.bed;
				int var12 = MathHelper.floor_double((double)(p_77648_2_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3;
				sbyte var13 = 0;
				sbyte var14 = 0;

				if (var12 == 0)
				{
					var14 = 1;
				}

				if (var12 == 1)
				{
					var13 = -1;
				}

				if (var12 == 2)
				{
					var14 = -1;
				}

				if (var12 == 3)
				{
					var13 = 1;
				}

				if (p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_) && p_77648_2_.canPlayerEdit(p_77648_4_ + var13, p_77648_5_, p_77648_6_ + var14, p_77648_7_, p_77648_1_))
				{
					if (p_77648_3_.isAirBlock(p_77648_4_, p_77648_5_, p_77648_6_) && p_77648_3_.isAirBlock(p_77648_4_ + var13, p_77648_5_, p_77648_6_ + var14) && World.doesBlockHaveSolidTopSurface(p_77648_3_, p_77648_4_, p_77648_5_ - 1, p_77648_6_) && World.doesBlockHaveSolidTopSurface(p_77648_3_, p_77648_4_ + var13, p_77648_5_ - 1, p_77648_6_ + var14))
					{
						p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, var11, var12, 3);

						if (p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_) == var11)
						{
							p_77648_3_.setBlock(p_77648_4_ + var13, p_77648_5_, p_77648_6_ + var14, var11, var12 + 8, 3);
						}

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

}