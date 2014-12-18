namespace DotCraftCore.nEntity
{

	using EntityDragonPart = DotCraftCore.nEntity.nBoss.EntityDragonPart;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using World = DotCraftCore.nWorld.World;

	public interface IEntityMultiPart
	{
		World func_82194_d();

		bool attackEntityFromPart(EntityDragonPart p_70965_1_, DamageSource p_70965_2_, float p_70965_3_);
	}

}