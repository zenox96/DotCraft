namespace DotCraftCore.Entity.AI
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityHorse = DotCraftCore.Entity.Passive.EntityHorse;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Vec3 = DotCraftCore.Util.Vec3;

	public class EntityAIRunAroundLikeCrazy : EntityAIBase
	{
		private EntityHorse horseHost;
		private double field_111178_b;
		private double field_111179_c;
		private double field_111176_d;
		private double field_111177_e;
		

		public EntityAIRunAroundLikeCrazy(EntityHorse p_i1653_1_, double p_i1653_2_)
		{
			this.horseHost = p_i1653_1_;
			this.field_111178_b = p_i1653_2_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (!this.horseHost.Tame && this.horseHost.riddenByEntity != null)
			{
				Vec3 var1 = RandomPositionGenerator.findRandomTarget(this.horseHost, 5, 4);

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.field_111179_c = var1.xCoord;
					this.field_111176_d = var1.yCoord;
					this.field_111177_e = var1.zCoord;
					return true;
				}
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.horseHost.Navigator.tryMoveToXYZ(this.field_111179_c, this.field_111176_d, this.field_111177_e, this.field_111178_b);
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.horseHost.Navigator.noPath() && this.horseHost.riddenByEntity != null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			if (this.horseHost.RNG.Next(50) == 0)
			{
				if (this.horseHost.riddenByEntity is EntityPlayer)
				{
					int var1 = this.horseHost.Temper;
					int var2 = this.horseHost.MaxTemper;

					if (var2 > 0 && this.horseHost.RNG.Next(var2) < var1)
					{
						this.horseHost.TamedBy = (EntityPlayer)this.horseHost.riddenByEntity;
						this.horseHost.worldObj.setEntityState(this.horseHost, (sbyte)7);
						return;
					}

					this.horseHost.increaseTemper(5);
				}

				this.horseHost.riddenByEntity.mountEntity((Entity)null);
				this.horseHost.riddenByEntity = null;
				this.horseHost.makeHorseRearWithSound();
				this.horseHost.worldObj.setEntityState(this.horseHost, (sbyte)6);
			}
		}
	}

}