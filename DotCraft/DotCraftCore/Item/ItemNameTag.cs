namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityLiving = DotCraftCore.entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;

	public class ItemNameTag : Item
	{
		private const string __OBFID = "CL_00000052";

		public ItemNameTag()
		{
			this.CreativeTab = CreativeTabs.tabTools;
		}

///    
///     <summary> * Returns true if the item can be used on the given entity, e.g. shears on sheep. </summary>
///     
		public virtual bool itemInteractionForEntity(ItemStack p_111207_1_, EntityPlayer p_111207_2_, EntityLivingBase p_111207_3_)
		{
			if (!p_111207_1_.hasDisplayName())
			{
				return false;
			}
			else if (p_111207_3_ is EntityLiving)
			{
				EntityLiving var4 = (EntityLiving)p_111207_3_;
				var4.CustomNameTag = p_111207_1_.DisplayName;
				var4.func_110163_bv();
				--p_111207_1_.stackSize;
				return true;
			}
			else
			{
				return base.itemInteractionForEntity(p_111207_1_, p_111207_2_, p_111207_3_);
			}
		}
	}

}