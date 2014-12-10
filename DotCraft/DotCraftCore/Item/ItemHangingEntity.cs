using System;

namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityHanging = DotCraftCore.entity.EntityHanging;
	using EntityItemFrame = DotCraftCore.entity.item.EntityItemFrame;
	using EntityPainting = DotCraftCore.entity.item.EntityPainting;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Direction = DotCraftCore.Util.Direction;
	using World = DotCraftCore.World.World;

	public class ItemHangingEntity : Item
	{
		private readonly Type hangingEntityClass;
		private const string __OBFID = "CL_00000038";

		public ItemHangingEntity(Type p_i45342_1_)
		{
			this.hangingEntityClass = p_i45342_1_;
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
			else if (p_77648_7_ == 1)
			{
				return false;
			}
			else
			{
				int var11 = Direction.facingToDirection[p_77648_7_];
				EntityHanging var12 = this.createHangingEntity(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_, var11);

				if (!p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_))
				{
					return false;
				}
				else
				{
					if (var12 != null && var12.onValidSurface())
					{
						if (!p_77648_3_.isClient)
						{
							p_77648_3_.spawnEntityInWorld(var12);
						}

						--p_77648_1_.stackSize;
					}

					return true;
				}
			}
		}

///    
///     <summary> * Create the hanging entity associated to this item. </summary>
///     
		private EntityHanging createHangingEntity(World p_82810_1_, int p_82810_2_, int p_82810_3_, int p_82810_4_, int p_82810_5_)
		{
			return (EntityHanging)(this.hangingEntityClass == typeof(EntityPainting) ? new EntityPainting(p_82810_1_, p_82810_2_, p_82810_3_, p_82810_4_, p_82810_5_) : (this.hangingEntityClass == typeof(EntityItemFrame) ? new EntityItemFrame(p_82810_1_, p_82810_2_, p_82810_3_, p_82810_4_, p_82810_5_) : null));
		}
	}

}