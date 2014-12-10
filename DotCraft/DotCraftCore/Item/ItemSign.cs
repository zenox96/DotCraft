namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using TileEntitySign = DotCraftCore.TileEntity.TileEntitySign;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class ItemSign : Item
	{
		private const string __OBFID = "CL_00000064";

		public ItemSign()
		{
			this.maxStackSize = 16;
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (p_77648_7_ == 0)
			{
				return false;
			}
			else if (!p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_).Material.Solid)
			{
				return false;
			}
			else
			{
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
				else if (!Blocks.standing_sign.canPlaceBlockAt(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_))
				{
					return false;
				}
				else if (p_77648_3_.isClient)
				{
					return true;
				}
				else
				{
					if (p_77648_7_ == 1)
					{
						int var11 = MathHelper.floor_double((double)((p_77648_2_.rotationYaw + 180.0F) * 16.0F / 360.0F) + 0.5D) & 15;
						p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, Blocks.standing_sign, var11, 3);
					}
					else
					{
						p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, Blocks.wall_sign, p_77648_7_, 3);
					}

					--p_77648_1_.stackSize;
					TileEntitySign var12 = (TileEntitySign)p_77648_3_.getTileEntity(p_77648_4_, p_77648_5_, p_77648_6_);

					if (var12 != null)
					{
						p_77648_2_.func_146100_a(var12);
					}

					return true;
				}
			}
		}
	}

}