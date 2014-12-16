using System.Collections;

namespace DotCraftCore.Item
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using EnumChatFormatting = DotCraftCore.Util.EnumChatFormatting;
	using StatCollector = DotCraftCore.Util.StatCollector;
	using StringUtils = DotCraftCore.Util.StringUtils;
	using World = DotCraftCore.World.World;

	public class ItemEditableBook : Item
	{
		

		public ItemEditableBook()
		{
			this.MaxStackSize = 1;
		}

		public static bool validBookTagContents(NBTTagCompound p_77828_0_)
		{
			if (!ItemWritableBook.func_150930_a(p_77828_0_))
			{
				return false;
			}
			else if (!p_77828_0_.func_150297_b("title", 8))
			{
				return false;
			}
			else
			{
				string var1 = p_77828_0_.getString("title");
				return var1 != null && var1.Length <= 16 ? p_77828_0_.func_150297_b("author", 8) : false;
			}
		}

		public virtual string getItemStackDisplayName(ItemStack p_77653_1_)
		{
			if (p_77653_1_.hasTagCompound())
			{
				NBTTagCompound var2 = p_77653_1_.TagCompound;
				string var3 = var2.getString("title");

				if (!StringUtils.isNullOrEmpty(var3))
				{
					return var3;
				}
			}

			return base.getItemStackDisplayName(p_77653_1_);
		}

///    
///     <summary> * allows items to add custom lines of information to the mouseover description </summary>
///     
		public virtual void addInformation(ItemStack p_77624_1_, EntityPlayer p_77624_2_, IList p_77624_3_, bool p_77624_4_)
		{
			if (p_77624_1_.hasTagCompound())
			{
				NBTTagCompound var5 = p_77624_1_.TagCompound;
				string var6 = var5.getString("author");

				if (!StringUtils.isNullOrEmpty(var6))
				{
					p_77624_3_.Add(EnumChatFormatting.GRAY + StatCollector.translateToLocalFormatted("book.byAuthor", new object[] {var6}));
				}
			}
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

		public virtual bool hasEffect(ItemStack p_77636_1_)
		{
			return true;
		}
	}

}