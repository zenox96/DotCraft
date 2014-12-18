namespace DotCraftCore.nItem
{

	using Block = DotCraftCore.nBlock.Block;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using MovingObjectPosition = DotCraftCore.nUtil.MovingObjectPosition;
	using World = DotCraftCore.nWorld.World;

	public class ItemLilyPad : ItemColored
	{
		

		public ItemLilyPad(Block p_i45357_1_) : base(p_i45357_1_, false)
		{
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			MovingObjectPosition var4 = this.getMovingObjectPositionFromPlayer(p_77659_2_, p_77659_3_, true);

			if (var4 == null)
			{
				return p_77659_1_;
			}
			else
			{
				if (var4.typeOfHit == MovingObjectPosition.MovingObjectType.BLOCK)
				{
					int var5 = var4.blockX;
					int var6 = var4.blockY;
					int var7 = var4.blockZ;

					if (!p_77659_2_.canMineBlock(p_77659_3_, var5, var6, var7))
					{
						return p_77659_1_;
					}

					if (!p_77659_3_.canPlayerEdit(var5, var6, var7, var4.sideHit, p_77659_1_))
					{
						return p_77659_1_;
					}

					if (p_77659_2_.getBlock(var5, var6, var7).Material == Material.water && p_77659_2_.getBlockMetadata(var5, var6, var7) == 0 && p_77659_2_.isAirBlock(var5, var6 + 1, var7))
					{
						p_77659_2_.setBlock(var5, var6 + 1, var7, Blocks.waterlily);

						if (!p_77659_3_.capabilities.isCreativeMode)
						{
							--p_77659_1_.stackSize;
						}
					}
				}

				return p_77659_1_;
			}
		}

		public override int getColorFromItemStack(ItemStack p_82790_1_, int p_82790_2_)
		{
			return Blocks.waterlily.getRenderColor(p_82790_1_.ItemDamage);
		}
	}

}