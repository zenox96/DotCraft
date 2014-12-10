namespace DotCraftCore.Enchantment
{

	using Item = DotCraftCore.Item.Item;
	using ItemArmor = DotCraftCore.Item.ItemArmor;
	using ItemBow = DotCraftCore.Item.ItemBow;
	using ItemFishingRod = DotCraftCore.Item.ItemFishingRod;
	using ItemSword = DotCraftCore.Item.ItemSword;
	using ItemTool = DotCraftCore.Item.ItemTool;

	public enum EnumEnchantmentType
	{
		all,
		armor,
		armor_feet,
		armor_legs,
		armor_torso,
		armor_head,
		weapon,
		digger,
		fishing_rod,
		breakable,
		bow
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private static final String __OBFID = "CL_00000106";

///    
///     <summary> * Return true if the item passed can be enchanted by a enchantment of this type. </summary>
///     
	}
	public static partial class EnumExtensionMethods
	{
			public bool canEnchantItem(this EnumEnchantmentType instance, Item p_77557_1_)
		{
			if (instance == all)
			{
				return true;
			}
			else if (instance == breakable && p_77557_1_.Damageable)
			{
				return true;
			}
			else if (p_77557_1_ is ItemArmor)
			{
				if (instance == armor)
				{
					return true;
				}
				else
				{
					ItemArmor var2 = (ItemArmor)p_77557_1_;
					return var2.armorType == 0 ? instance == armor_head : (var2.armorType == 2 ? instance == armor_legs : (var2.armorType == 1 ? instance == armor_torso : (var2.armorType == 3 ? instance == armor_feet : false)));
				}
			}
			else
			{
				return p_77557_1_ is ItemSword ? instance == weapon : (p_77557_1_ is ItemTool ? instance == digger : (p_77557_1_ is ItemBow ? instance == bow : (p_77557_1_ is ItemFishingRod ? instance == fishing_rod : false)));
			}
		}
	}

}