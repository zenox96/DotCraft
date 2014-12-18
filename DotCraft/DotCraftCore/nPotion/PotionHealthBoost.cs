namespace DotCraftCore.nPotion
{

	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using BaseAttributeMap = DotCraftCore.entity.ai.attributes.BaseAttributeMap;

	public class PotionHealthBoost : Potion
	{
		

		public PotionHealthBoost(int p_i1571_1_, bool p_i1571_2_, int p_i1571_3_) : base(p_i1571_1_, p_i1571_2_, p_i1571_3_)
		{
		}

		public override void removeAttributesModifiersFromEntity(EntityLivingBase p_111187_1_, BaseAttributeMap p_111187_2_, int p_111187_3_)
		{
			base.removeAttributesModifiersFromEntity(p_111187_1_, p_111187_2_, p_111187_3_);

			if (p_111187_1_.Health > p_111187_1_.MaxHealth)
			{
				p_111187_1_.Health = p_111187_1_.MaxHealth;
			}
		}
	}

}