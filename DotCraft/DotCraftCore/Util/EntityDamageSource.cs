namespace DotCraftCore.Util
{

	using Entity = DotCraftCore.entity.Entity;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using ItemStack = DotCraftCore.item.ItemStack;

	public class EntityDamageSource : DamageSource
	{
		protected internal Entity damageSourceEntity;
		

		public EntityDamageSource(string p_i1567_1_, Entity p_i1567_2_) : base(p_i1567_1_)
		{
			this.damageSourceEntity = p_i1567_2_;
		}

		public override Entity Entity
		{
			get
			{
				return this.damageSourceEntity;
			}
		}

		public override IChatComponent func_151519_b(EntityLivingBase p_151519_1_)
		{
			ItemStack var2 = this.damageSourceEntity is EntityLivingBase ? ((EntityLivingBase)this.damageSourceEntity).HeldItem : null;
			string var3 = "death.attack." + this.damageType;
			string var4 = var3 + ".item";
			return var2 != null && var2.hasDisplayName() && StatCollector.canTranslate(var4) ? new ChatComponentTranslation(var4, new object[] {p_151519_1_.func_145748_c_(), this.damageSourceEntity.func_145748_c_(), var2.func_151000_E()}): new ChatComponentTranslation(var3, new object[] {p_151519_1_.func_145748_c_(), this.damageSourceEntity.func_145748_c_()});
		}

///    
///     <summary> * Return whether this damage source will have its damage amount scaled based on the current difficulty. </summary>
///     
		public override bool isDifficultyScaled()
		{
			get
			{
				return this.damageSourceEntity != null && this.damageSourceEntity is EntityLivingBase && !(this.damageSourceEntity is EntityPlayer);
			}
		}
	}

}