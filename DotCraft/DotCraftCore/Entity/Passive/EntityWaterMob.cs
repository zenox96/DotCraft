namespace DotCraftCore.nEntity.nPassive
{

	using EntityCreature = DotCraftCore.nEntity.EntityCreature;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using World = DotCraftCore.nWorld.World;

	public abstract class EntityWaterMob : EntityCreature, IAnimals
	{
		

		public EntityWaterMob(World p_i1695_1_) : base(p_i1695_1_)
		{
		}

		public virtual bool canBreatheUnderwater()
		{
			return true;
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public override bool CanSpawnHere
		{
			get
			{
				return this.worldObj.checkNoEntityCollision(this.boundingBox);
			}
		}

///    
///     <summary> * Get number of ticks, at least during which the living entity will be silent. </summary>
///     
		public virtual int TalkInterval
		{
			get
			{
				return 120;
			}
		}

///    
///     <summary> * Determines if an entity can be despawned, used on idle far away entities </summary>
///     
		protected internal virtual bool canDespawn()
		{
			return true;
		}

///    
///     <summary> * Get the experience points the entity currently has. </summary>
///     
		protected internal virtual int getExperiencePoints(EntityPlayer p_70693_1_)
		{
			return 1 + this.worldObj.rand.Next(3);
		}

///    
///     <summary> * Gets called every tick from main Entity class </summary>
///     
		public virtual void onEntityUpdate()
		{
			int var1 = this.Air;
			base.onEntityUpdate();

			if (this.EntityAlive && !this.InWater)
			{
				--var1;
				this.Air = var1;

				if (this.Air == -20)
				{
					this.Air = 0;
					this.attackEntityFrom(DamageSource.drown, 2.0F);
				}
			}
			else
			{
				this.Air = 300;
			}
		}
	}

}