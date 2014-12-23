using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public abstract class BlockLog : BlockRotatedPillar
	{
		public BlockLog() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
			this.Hardness = 2.0F;
			this.StepSound = soundTypeWood;
		}

		public static int func_150165_c(int p_150165_0_)
		{
			return p_150165_0_ & 3;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 1;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(this);
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			sbyte var7 = 4;
			int var8 = var7 + 1;

			if (p_149749_1_.checkChunksExist(p_149749_2_ - var8, p_149749_3_ - var8, p_149749_4_ - var8, p_149749_2_ + var8, p_149749_3_ + var8, p_149749_4_ + var8))
			{
				for (int var9 = -var7; var9 <= var7; ++var9)
				{
					for (int var10 = -var7; var10 <= var7; ++var10)
					{
						for (int var11 = -var7; var11 <= var7; ++var11)
						{
							if (p_149749_1_.getBlock(p_149749_2_ + var9, p_149749_3_ + var10, p_149749_4_ + var11).BlockMaterial == Material.leaves)
							{
								int var12 = p_149749_1_.getBlockMetadata(p_149749_2_ + var9, p_149749_3_ + var10, p_149749_4_ + var11);

								if ((var12 & 8) == 0)
								{
									p_149749_1_.setBlockMetadataWithNotify(p_149749_2_ + var9, p_149749_3_ + var10, p_149749_4_ + var11, var12 | 8, 4);
								}
							}
						}
					}
				}
			}
		}
	}
}