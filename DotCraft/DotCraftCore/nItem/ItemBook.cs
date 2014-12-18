namespace DotCraftCore.nItem
{

	public class ItemBook : Item
	{
		

///    
///     <summary> * Checks isDamagable and if it cannot be stacked </summary>
///     
		public virtual bool isItemTool(ItemStack p_77616_1_)
		{
			return p_77616_1_.stackSize == 1;
		}

///    
///     <summary> * Return the enchantability factor of the item, most of the time is based on material. </summary>
///     
		public virtual int ItemEnchantability
		{
			get
			{
				return 1;
			}
		}
	}

}