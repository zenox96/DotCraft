namespace DotCraftCore.nPotion
{

	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using BaseAttributeMap = DotCraftCore.entity.ai.attributes.BaseAttributeMap;

	public class PotionAbsoption : Potion
	{
		

		protected internal PotionAbsoption(int p_i1569_1_, bool p_i1569_2_, int p_i1569_3_) : base(p_i1569_1_, p_i1569_2_, p_i1569_3_)
		{
		}

		public override void removeAttributesModifiersFromEntity(EntityLivingBase p_111187_1_, BaseAttributeMap p_111187_2_, int p_111187_3_)
		{
			p_111187_1_.AbsorptionAmount = p_111187_1_.AbsorptionAmount - (float)(4 * (p_111187_3_ + 1));
			base.removeAttributesModifiersFromEntity(p_111187_1_, p_111187_2_, p_111187_3_);
		}

		public override void applyAttributesModifiersToEntity(EntityLivingBase p_111185_1_, BaseAttributeMap p_111185_2_, int p_111185_3_)
		{
			p_111185_1_.AbsorptionAmount = p_111185_1_.AbsorptionAmount + (float)(4 * (p_111185_3_ + 1));
			base.applyAttributesModifiersToEntity(p_111185_1_, p_111185_2_, p_111185_3_);
		}
	}

}