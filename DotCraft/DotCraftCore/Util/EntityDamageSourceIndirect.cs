namespace DotCraftCore.nUtil
{

	using Entity = DotCraftCore.entity.Entity;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using ItemStack = DotCraftCore.item.ItemStack;

	public class EntityDamageSourceIndirect : EntityDamageSource
	{
		private Entity indirectEntity;
		

		public EntityDamageSourceIndirect(string p_i1568_1_, Entity p_i1568_2_, Entity p_i1568_3_) : base(p_i1568_1_, p_i1568_2_)
		{
			this.indirectEntity = p_i1568_3_;
		}

		public override Entity SourceOfDamage
		{
			get
			{
				return this.damageSourceEntity;
			}
		}

		public override Entity Entity
		{
			get
			{
				return this.indirectEntity;
			}
		}

		public override IChatComponent func_151519_b(EntityLivingBase p_151519_1_)
		{
			IChatComponent var2 = this.indirectEntity == null ? this.damageSourceEntity.func_145748_c_() : this.indirectEntity.func_145748_c_();
			ItemStack var3 = this.indirectEntity is EntityLivingBase ? ((EntityLivingBase)this.indirectEntity).HeldItem : null;
			string var4 = "death.attack." + this.damageType;
			string var5 = var4 + ".item";
			return var3 != null && var3.hasDisplayName() && StatCollector.canTranslate(var5) ? new ChatComponentTranslation(var5, new object[] {p_151519_1_.func_145748_c_(), var2, var3.func_151000_E()}): new ChatComponentTranslation(var4, new object[] {p_151519_1_.func_145748_c_(), var2});
		}
	}

}