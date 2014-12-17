namespace DotCraftCore.nItem
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.nNBT.NBTTagList;
	using World = DotCraftCore.nWorld.World;

	public class ItemWritableBook : Item
	{
		

		public ItemWritableBook()
		{
			this.MaxStackSize = 1;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			p_77659_3_.displayGUIBook(p_77659_1_);
			return p_77659_1_;
		}

///    
///     <summary> * If this function returns true (or the item is damageable), the ItemStack's NBT tag will be sent to the client. </summary>
///     
		public virtual bool ShareTag
		{
			get
			{
				return true;
			}
		}

		public static bool func_150930_a(NBTTagCompound p_150930_0_)
		{
			if (p_150930_0_ == null)
			{
				return false;
			}
			else if (!p_150930_0_.func_150297_b("pages", 9))
			{
				return false;
			}
			else
			{
				NBTTagList var1 = p_150930_0_.getTagList("pages", 8);

				for (int var2 = 0; var2 < var1.tagCount(); ++var2)
				{
					string var3 = var1.getStringTagAt(var2);

					if (var3 == null)
					{
						return false;
					}

					if (var3.Length > 256)
					{
						return false;
					}
				}

				return true;
			}
		}
	}

}