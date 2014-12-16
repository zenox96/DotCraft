namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityPig = DotCraftCore.entity.passive.EntityPig;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;

	public class ItemSaddle : Item
	{
		

		public ItemSaddle()
		{
			this.maxStackSize = 1;
			this.CreativeTab = CreativeTabs.tabTransport;
		}

///    
///     <summary> * Returns true if the item can be used on the given entity, e.g. shears on sheep. </summary>
///     
		public virtual bool itemInteractionForEntity(ItemStack p_111207_1_, EntityPlayer p_111207_2_, EntityLivingBase p_111207_3_)
		{
			if (p_111207_3_ is EntityPig)
			{
				EntityPig var4 = (EntityPig)p_111207_3_;

				if (!var4.Saddled && !var4.Child)
				{
					var4.Saddled = true;
					var4.worldObj.playSoundAtEntity(var4, "mob.horse.leather", 0.5F, 1.0F);
					--p_111207_1_.stackSize;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Current implementations of this method in child classes do not use the entry argument beside ev. They just raise
///     * the damage on the stack. </summary>
///     
		public virtual bool hitEntity(ItemStack p_77644_1_, EntityLivingBase p_77644_2_, EntityLivingBase p_77644_3_)
		{
			this.itemInteractionForEntity(p_77644_1_, (EntityPlayer)null, p_77644_2_);
			return true;
		}
	}

}