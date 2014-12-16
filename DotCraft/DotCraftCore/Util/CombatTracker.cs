using System.Collections;

namespace DotCraftCore.Util
{

	using Block = DotCraftCore.block.Block;
	using Entity = DotCraftCore.entity.Entity;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using ItemStack = DotCraftCore.item.ItemStack;

	public class CombatTracker
	{
	/// <summary> The CombatEntry objects that we've tracked so far.  </summary>
		private readonly IList combatEntries = new ArrayList();

	/// <summary> The entity tracked.  </summary>
		private readonly EntityLivingBase fighter;
		private int field_94555_c;
		private int field_152775_d;
		private int field_152776_e;
		private bool field_94552_d;
		private bool field_94553_e;
		private string field_94551_f;
		

		public CombatTracker(EntityLivingBase p_i1565_1_)
		{
			this.fighter = p_i1565_1_;
		}

		public virtual void func_94545_a()
		{
			this.func_94542_g();

			if(this.fighter.OnLadder)
			{
				Block var1 = this.fighter.worldObj.getBlock(MathHelper.floor_double(this.fighter.posX), MathHelper.floor_double(this.fighter.boundingBox.minY), MathHelper.floor_double(this.fighter.posZ));

				if(var1 == Blocks.ladder)
				{
					this.field_94551_f = "ladder";
				}
				else if(var1 == Blocks.vine)
				{
					this.field_94551_f = "vines";
				}
			}
			else if(this.fighter.InWater)
			{
				this.field_94551_f = "water";
			}
		}

		public virtual void func_94547_a(DamageSource p_94547_1_, float p_94547_2_, float p_94547_3_)
		{
			this.func_94549_h();
			this.func_94545_a();
			CombatEntry var4 = new CombatEntry(p_94547_1_, this.fighter.ticksExisted, p_94547_2_, p_94547_3_, this.field_94551_f, this.fighter.fallDistance);
			this.combatEntries.Add(var4);
			this.field_94555_c = this.fighter.ticksExisted;
			this.field_94553_e = true;

			if(var4.func_94559_f() && !this.field_94552_d && this.fighter.EntityAlive)
			{
				this.field_94552_d = true;
				this.field_152775_d = this.fighter.ticksExisted;
				this.field_152776_e = this.field_152775_d;
				this.fighter.func_152111_bt();
			}
		}

		public virtual IChatComponent func_151521_b()
		{
			if(this.combatEntries.Count == 0)
			{
				return new ChatComponentTranslation("death.attack.generic", new object[] {this.fighter.func_145748_c_()});
			}
			else
			{
				CombatEntry var1 = this.func_94544_f();
				CombatEntry var2 = (CombatEntry)this.combatEntries.get(this.combatEntries.Count - 1);
				IChatComponent var4 = var2.func_151522_h();
				Entity var5 = var2.DamageSrc.Entity;
				object var3;

				if(var1 != null && var2.DamageSrc == DamageSource.fall)
				{
					IChatComponent var6 = var1.func_151522_h();

					if(var1.DamageSrc != DamageSource.fall && var1.DamageSrc != DamageSource.outOfWorld)
					{
						if(var6 != null && (var4 == null || !var6.Equals(var4)))
						{
							Entity var9 = var1.DamageSrc.Entity;
							ItemStack var8 = var9 is EntityLivingBase ? ((EntityLivingBase)var9).HeldItem : null;

							if(var8 != null && var8.hasDisplayName())
							{
								var3 = new ChatComponentTranslation("death.fell.assist.item", new object[] {this.fighter.func_145748_c_(), var6, var8.func_151000_E()});
							}
							else
							{
								var3 = new ChatComponentTranslation("death.fell.assist", new object[] {this.fighter.func_145748_c_(), var6});
							}
						}
						else if(var4 != null)
						{
							ItemStack var7 = var5 is EntityLivingBase ? ((EntityLivingBase)var5).HeldItem : null;

							if(var7 != null && var7.hasDisplayName())
							{
								var3 = new ChatComponentTranslation("death.fell.finish.item", new object[] {this.fighter.func_145748_c_(), var4, var7.func_151000_E()});
							}
							else
							{
								var3 = new ChatComponentTranslation("death.fell.finish", new object[] {this.fighter.func_145748_c_(), var4});
							}
						}
						else
						{
							var3 = new ChatComponentTranslation("death.fell.killer", new object[] {this.fighter.func_145748_c_()});
						}
					}
					else
					{
						var3 = new ChatComponentTranslation("death.fell.accident." + this.func_94548_b(var1), new object[] {this.fighter.func_145748_c_()});
					}
				}
				else
				{
					var3 = var2.DamageSrc.func_151519_b(this.fighter);
				}

				return(IChatComponent)var3;
			}
		}

		public virtual EntityLivingBase func_94550_c()
		{
			EntityLivingBase var1 = null;
			EntityPlayer var2 = null;
			float var3 = 0.0F;
			float var4 = 0.0F;
			IEnumerator var5 = this.combatEntries.GetEnumerator();

			while(var5.MoveNext())
			{
				CombatEntry var6 = (CombatEntry)var5.Current;

				if(var6.DamageSrc.Entity is EntityPlayer && (var2 == null || var6.func_94563_c() > var4))
				{
					var4 = var6.func_94563_c();
					var2 = (EntityPlayer)var6.DamageSrc.Entity;
				}

				if(var6.DamageSrc.Entity is EntityLivingBase && (var1 == null || var6.func_94563_c() > var3))
				{
					var3 = var6.func_94563_c();
					var1 = (EntityLivingBase)var6.DamageSrc.Entity;
				}
			}

			if(var2 != null && var4 >= var3 / 3.0F)
			{
				return var2;
			}
			else
			{
				return var1;
			}
		}

		private CombatEntry func_94544_f()
		{
			CombatEntry var1 = null;
			CombatEntry var2 = null;
			sbyte var3 = 0;
			float var4 = 0.0F;

			for(int var5 = 0; var5 < this.combatEntries.Count; ++var5)
			{
				CombatEntry var6 = (CombatEntry)this.combatEntries.get(var5);
				CombatEntry var7 = var5 > 0 ? (CombatEntry)this.combatEntries.get(var5 - 1) : null;

				if((var6.DamageSrc == DamageSource.fall || var6.DamageSrc == DamageSource.outOfWorld) && var6.func_94561_i() > 0.0F && (var1 == null || var6.func_94561_i() > var4))
				{
					if(var5 > 0)
					{
						var1 = var7;
					}
					else
					{
						var1 = var6;
					}

					var4 = var6.func_94561_i();
				}

				if(var6.func_94562_g() != null && (var2 == null || var6.func_94563_c() > (float)var3))
				{
					var2 = var6;
				}
			}

			if(var4 > 5.0F && var1 != null)
			{
				return var1;
			}
			else if(var3 > 5 && var2 != null)
			{
				return var2;
			}
			else
			{
				return null;
			}
		}

		private string func_94548_b(CombatEntry p_94548_1_)
		{
			return p_94548_1_.func_94562_g() == null ? "generic" : p_94548_1_.func_94562_g();
		}

		private void func_94542_g()
		{
			this.field_94551_f = null;
		}

		public virtual void func_94549_h()
		{
			int var1 = this.field_94552_d ? 300 : 100;

			if(this.field_94553_e && (!this.fighter.EntityAlive || this.fighter.ticksExisted - this.field_94555_c > var1))
			{
				bool var2 = this.field_94552_d;
				this.field_94553_e = false;
				this.field_94552_d = false;
				this.field_152776_e = this.fighter.ticksExisted;

				if(var2)
				{
					this.fighter.func_152112_bu();
				}

				this.combatEntries.Clear();
			}
		}
	}

}