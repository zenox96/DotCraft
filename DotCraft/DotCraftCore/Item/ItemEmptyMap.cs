using System;

namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using World = DotCraftCore.World.World;
	using MapData = DotCraftCore.World.Storage.MapData;

	public class ItemEmptyMap : ItemMapBase
	{
		private const string __OBFID = "CL_00000024";

		protected internal ItemEmptyMap()
		{
			this.CreativeTab = CreativeTabs.tabMisc;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			ItemStack var4 = new ItemStack(Items.filled_map, 1, p_77659_2_.getUniqueDataId("map"));
			string var5 = "map_" + var4.ItemDamage;
			MapData var6 = new MapData(var5);
			p_77659_2_.setItemData(var5, var6);
			var6.scale = 0;
			int var7 = 128 * (1 << var6.scale);
			var6.xCenter = (int)(Math.Round(p_77659_3_.posX / (double)var7) * (long)var7);
			var6.zCenter = (int)(Math.Round(p_77659_3_.posZ / (double)var7) * (long)var7);
			var6.dimension = (sbyte)p_77659_2_.provider.dimensionId;
			var6.markDirty();
			--p_77659_1_.stackSize;

			if (p_77659_1_.stackSize <= 0)
			{
				return var4;
			}
			else
			{
				if (!p_77659_3_.inventory.addItemStackToInventory(var4.copy()))
				{
					p_77659_3_.dropPlayerItemWithRandomChoice(var4, false);
				}

				return p_77659_1_;
			}
		}
	}

}