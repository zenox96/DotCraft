using System.Collections;

namespace DotCraftCore.nItem
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Potion = DotCraftCore.nPotion.Potion;
	using PotionEffect = DotCraftCore.nPotion.PotionEffect;
	using World = DotCraftCore.nWorld.World;

	public class ItemAppleGold : ItemFood
	{
		

		public ItemAppleGold(int p_i45341_1_, float p_i45341_2_, bool p_i45341_3_) : base(p_i45341_1_, p_i45341_2_, p_i45341_3_)
		{
			this.HasSubtypes = true;
		}

		public virtual bool hasEffect(ItemStack p_77636_1_)
		{
			return p_77636_1_.ItemDamage > 0;
		}

///    
///     <summary> * Return an item rarity from EnumRarity </summary>
///     
		public virtual EnumRarity getRarity(ItemStack p_77613_1_)
		{
			return p_77613_1_.ItemDamage == 0 ? EnumRarity.rare : EnumRarity.epic;
		}

		protected internal override void onFoodEaten(ItemStack p_77849_1_, World p_77849_2_, EntityPlayer p_77849_3_)
		{
			if (!p_77849_2_.isClient)
			{
				p_77849_3_.addPotionEffect(new PotionEffect(Potion.field_76444_x.id, 2400, 0));
			}

			if (p_77849_1_.ItemDamage > 0)
			{
				if (!p_77849_2_.isClient)
				{
					p_77849_3_.addPotionEffect(new PotionEffect(Potion.regeneration.id, 600, 4));
					p_77849_3_.addPotionEffect(new PotionEffect(Potion.resistance.id, 6000, 0));
					p_77849_3_.addPotionEffect(new PotionEffect(Potion.fireResistance.id, 6000, 0));
				}
			}
			else
			{
				base.onFoodEaten(p_77849_1_, p_77849_2_, p_77849_3_);
			}
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
			p_150895_3_.Add(new ItemStack(p_150895_1_, 1, 0));
			p_150895_3_.Add(new ItemStack(p_150895_1_, 1, 1));
		}
	}

}