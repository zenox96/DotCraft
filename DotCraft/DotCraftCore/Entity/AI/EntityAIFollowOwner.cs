namespace DotCraftCore.Entity.AI
{

	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityTameable = DotCraftCore.Entity.Passive.EntityTameable;
	using PathNavigate = DotCraftCore.Pathfinding.PathNavigate;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class EntityAIFollowOwner : EntityAIBase
	{
		private EntityTameable thePet;
		private EntityLivingBase theOwner;
		internal World theWorld;
		private double field_75336_f;
		private PathNavigate petPathfinder;
		private int field_75343_h;
		internal float maxDist;
		internal float minDist;
		private bool field_75344_i;
		private const string __OBFID = "CL_00001585";

		public EntityAIFollowOwner(EntityTameable p_i1625_1_, double p_i1625_2_, float p_i1625_4_, float p_i1625_5_)
		{
			this.thePet = p_i1625_1_;
			this.theWorld = p_i1625_1_.worldObj;
			this.field_75336_f = p_i1625_2_;
			this.petPathfinder = p_i1625_1_.Navigator;
			this.minDist = p_i1625_4_;
			this.maxDist = p_i1625_5_;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			EntityLivingBase var1 = this.thePet.Owner;

			if (var1 == null)
			{
				return false;
			}
			else if (this.thePet.Sitting)
			{
				return false;
			}
			else if (this.thePet.getDistanceSqToEntity(var1) < (double)(this.minDist * this.minDist))
			{
				return false;
			}
			else
			{
				this.theOwner = var1;
				return true;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.petPathfinder.noPath() && this.thePet.getDistanceSqToEntity(this.theOwner) > (double)(this.maxDist * this.maxDist) && !this.thePet.Sitting;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.field_75343_h = 0;
			this.field_75344_i = this.thePet.Navigator.AvoidsWater;
			this.thePet.Navigator.AvoidsWater = false;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.theOwner = null;
			this.petPathfinder.clearPathEntity();
			this.thePet.Navigator.AvoidsWater = this.field_75344_i;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.thePet.LookHelper.setLookPositionWithEntity(this.theOwner, 10.0F, (float)this.thePet.VerticalFaceSpeed);

			if (!this.thePet.Sitting)
			{
				if (--this.field_75343_h <= 0)
				{
					this.field_75343_h = 10;

					if (!this.petPathfinder.tryMoveToEntityLiving(this.theOwner, this.field_75336_f))
					{
						if (!this.thePet.Leashed)
						{
							if (this.thePet.getDistanceSqToEntity(this.theOwner) >= 144.0D)
							{
								int var1 = MathHelper.floor_double(this.theOwner.posX) - 2;
								int var2 = MathHelper.floor_double(this.theOwner.posZ) - 2;
								int var3 = MathHelper.floor_double(this.theOwner.boundingBox.minY);

								for (int var4 = 0; var4 <= 4; ++var4)
								{
									for (int var5 = 0; var5 <= 4; ++var5)
									{
										if ((var4 < 1 || var5 < 1 || var4 > 3 || var5 > 3) && World.doesBlockHaveSolidTopSurface(this.theWorld, var1 + var4, var3 - 1, var2 + var5) && !this.theWorld.getBlock(var1 + var4, var3, var2 + var5).NormalCube && !this.theWorld.getBlock(var1 + var4, var3 + 1, var2 + var5).NormalCube)
										{
											this.thePet.setLocationAndAngles((double)((float)(var1 + var4) + 0.5F), (double)var3, (double)((float)(var2 + var5) + 0.5F), this.thePet.rotationYaw, this.thePet.rotationPitch);
											this.petPathfinder.clearPathEntity();
											return;
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

}