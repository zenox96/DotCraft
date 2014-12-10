namespace DotCraftCore.Entity
{

	using EntityDragonPart = DotCraftCore.Entity.Boss.EntityDragonPart;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using World = DotCraftCore.World.World;

	public interface IEntityMultiPart
	{
		World func_82194_d();

		bool attackEntityFromPart(EntityDragonPart p_70965_1_, DamageSource p_70965_2_, float p_70965_3_);
	}

}