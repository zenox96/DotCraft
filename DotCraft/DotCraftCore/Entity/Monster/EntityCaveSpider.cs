namespace DotCraftCore.nEntity.nMonster
{

	using Entity = DotCraftCore.nEntity.Entity;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using IEntityLivingData = DotCraftCore.nEntity.IEntityLivingData;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using Potion = DotCraftCore.nPotion.Potion;
	using PotionEffect = DotCraftCore.nPotion.PotionEffect;
	using EnumDifficulty = DotCraftCore.nWorld.EnumDifficulty;
	using World = DotCraftCore.nWorld.World;

	public class EntityCaveSpider : EntitySpider
	{
		

		public EntityCaveSpider(World p_i1732_1_) : base(p_i1732_1_)
		{
			this.setSize(0.7F, 0.5F);
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 12.0D;
		}

		public override bool attackEntityAsMob(Entity p_70652_1_)
		{
			if (base.attackEntityAsMob(p_70652_1_))
			{
				if (p_70652_1_ is EntityLivingBase)
				{
					sbyte var2 = 0;

					if (this.worldObj.difficultySetting == EnumDifficulty.NORMAL)
					{
						var2 = 7;
					}
					else if (this.worldObj.difficultySetting == EnumDifficulty.HARD)
					{
						var2 = 15;
					}

					if (var2 > 0)
					{
						((EntityLivingBase)p_70652_1_).addPotionEffect(new PotionEffect(Potion.poison.id, var2 * 20, 0));
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		public override IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			return p_110161_1_;
		}
	}

}