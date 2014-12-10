namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class ItemDoor : Item
	{
		private Material doorMaterial;
		private const string __OBFID = "CL_00000020";

		public ItemDoor(Material p_i45334_1_)
		{
			this.doorMaterial = p_i45334_1_;
			this.maxStackSize = 1;
			this.CreativeTab = CreativeTabs.tabRedstone;
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
			else
			{
				++p_77648_5_;
				Block var11;

				if (this.doorMaterial == Material.wood)
				{
					var11 = Blocks.wooden_door;
				}
				else
				{
					var11 = Blocks.iron_door;
				}

				if (p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_) && p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_ + 1, p_77648_6_, p_77648_7_, p_77648_1_))
				{
					if (!var11.canPlaceBlockAt(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_))
					{
						return false;
					}
					else
					{
						int var12 = MathHelper.floor_double((double)((p_77648_2_.rotationYaw + 180.0F) * 4.0F / 360.0F) - 0.5D) & 3;
						func_150924_a(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_, var12, var11);
						--p_77648_1_.stackSize;
						return true;
					}
				}
				else
				{
					return false;
				}
			}
		}

		public static void func_150924_a(World p_150924_0_, int p_150924_1_, int p_150924_2_, int p_150924_3_, int p_150924_4_, Block p_150924_5_)
		{
			sbyte var6 = 0;
			sbyte var7 = 0;

			if (p_150924_4_ == 0)
			{
				var7 = 1;
			}

			if (p_150924_4_ == 1)
			{
				var6 = -1;
			}

			if (p_150924_4_ == 2)
			{
				var7 = -1;
			}

			if (p_150924_4_ == 3)
			{
				var6 = 1;
			}

			int var8 = (p_150924_0_.getBlock(p_150924_1_ - var6, p_150924_2_, p_150924_3_ - var7).NormalCube ? 1 : 0) + (p_150924_0_.getBlock(p_150924_1_ - var6, p_150924_2_ + 1, p_150924_3_ - var7).NormalCube ? 1 : 0);
			int var9 = (p_150924_0_.getBlock(p_150924_1_ + var6, p_150924_2_, p_150924_3_ + var7).NormalCube ? 1 : 0) + (p_150924_0_.getBlock(p_150924_1_ + var6, p_150924_2_ + 1, p_150924_3_ + var7).NormalCube ? 1 : 0);
			bool var10 = p_150924_0_.getBlock(p_150924_1_ - var6, p_150924_2_, p_150924_3_ - var7) == p_150924_5_ || p_150924_0_.getBlock(p_150924_1_ - var6, p_150924_2_ + 1, p_150924_3_ - var7) == p_150924_5_;
			bool var11 = p_150924_0_.getBlock(p_150924_1_ + var6, p_150924_2_, p_150924_3_ + var7) == p_150924_5_ || p_150924_0_.getBlock(p_150924_1_ + var6, p_150924_2_ + 1, p_150924_3_ + var7) == p_150924_5_;
			bool var12 = false;

			if (var10 && !var11)
			{
				var12 = true;
			}
			else if (var9 > var8)
			{
				var12 = true;
			}

			p_150924_0_.setBlock(p_150924_1_, p_150924_2_, p_150924_3_, p_150924_5_, p_150924_4_, 2);
			p_150924_0_.setBlock(p_150924_1_, p_150924_2_ + 1, p_150924_3_, p_150924_5_, 8 | (var12 ? 1 : 0), 2);
			p_150924_0_.notifyBlocksOfNeighborChange(p_150924_1_, p_150924_2_, p_150924_3_, p_150924_5_);
			p_150924_0_.notifyBlocksOfNeighborChange(p_150924_1_, p_150924_2_ + 1, p_150924_3_, p_150924_5_);
		}
	}

}