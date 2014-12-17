using System.Collections;

namespace DotCraftCore.nItem
{

	using EntityFireworkRocket = DotCraftCore.entity.item.EntityFireworkRocket;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.nNBT.NBTTagList;
	using StatCollector = DotCraftCore.nUtil.StatCollector;
	using World = DotCraftCore.nWorld.World;

	public class ItemFirework : Item
	{
		

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (!p_77648_3_.isClient)
			{
				EntityFireworkRocket var11 = new EntityFireworkRocket(p_77648_3_, (double)((float)p_77648_4_ + p_77648_8_), (double)((float)p_77648_5_ + p_77648_9_), (double)((float)p_77648_6_ + p_77648_10_), p_77648_1_);
				p_77648_3_.spawnEntityInWorld(var11);

				if (!p_77648_2_.capabilities.isCreativeMode)
				{
					--p_77648_1_.stackSize;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * allows items to add custom lines of information to the mouseover description </summary>
///     
		public virtual void addInformation(ItemStack p_77624_1_, EntityPlayer p_77624_2_, IList p_77624_3_, bool p_77624_4_)
		{
			if (p_77624_1_.hasTagCompound())
			{
				NBTTagCompound var5 = p_77624_1_.TagCompound.getCompoundTag("Fireworks");

				if (var5 != null)
				{
					if (var5.func_150297_b("Flight", 99))
					{
						p_77624_3_.Add(StatCollector.translateToLocal("item.fireworks.flight") + " " + var5.getByte("Flight"));
					}

					NBTTagList var6 = var5.getTagList("Explosions", 10);

					if (var6 != null && var6.tagCount() > 0)
					{
						for (int var7 = 0; var7 < var6.tagCount(); ++var7)
						{
							NBTTagCompound var8 = var6.getCompoundTagAt(var7);
							ArrayList var9 = new ArrayList();
							ItemFireworkCharge.func_150902_a(var8, var9);

							if (var9.Count > 0)
							{
								for (int var10 = 1; var10 < var9.Count; ++var10)
								{
									var9[var10] = "  " + (string)var9[var10];
								}

								p_77624_3_.AddRange(var9);
							}
						}
					}
				}
			}
		}
	}

}