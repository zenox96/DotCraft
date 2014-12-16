namespace DotCraftCore.Util
{

	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;

	public class CombatEntry
	{
		private readonly DamageSource damageSrc;
		private readonly int field_94567_b;
		private readonly float field_94568_c;
		private readonly float field_94565_d;
		private readonly string field_94566_e;
		private readonly float field_94564_f;
		

		public CombatEntry(DamageSource p_i1564_1_, int p_i1564_2_, float p_i1564_3_, float p_i1564_4_, string p_i1564_5_, float p_i1564_6_)
		{
			this.damageSrc = p_i1564_1_;
			this.field_94567_b = p_i1564_2_;
			this.field_94568_c = p_i1564_4_;
			this.field_94565_d = p_i1564_3_;
			this.field_94566_e = p_i1564_5_;
			this.field_94564_f = p_i1564_6_;
		}

///    
///     <summary> * Get the DamageSource of the CombatEntry instance. </summary>
///     
		public virtual DamageSource DamageSrc
		{
			get
			{
				return this.damageSrc;
			}
		}

		public virtual float func_94563_c()
		{
			return this.field_94568_c;
		}

		public virtual bool func_94559_f()
		{
			return this.damageSrc.Entity is EntityLivingBase;
		}

		public virtual string func_94562_g()
		{
			return this.field_94566_e;
		}

		public virtual IChatComponent func_151522_h()
		{
			return this.DamageSrc.Entity == null ? null : this.DamageSrc.Entity.func_145748_c_();
		}

		public virtual float func_94561_i()
		{
			return this.damageSrc == DamageSource.outOfWorld ? float.MaxValue : this.field_94564_f;
		}
	}

}