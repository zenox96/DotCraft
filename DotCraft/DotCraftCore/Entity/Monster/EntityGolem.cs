namespace DotCraftCore.Entity.Monster
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using IAnimals = DotCraftCore.Entity.Passive.IAnimals;
	using World = DotCraftCore.world.World;

	public abstract class EntityGolem : EntityCreature, IAnimals
	{
		private const string __OBFID = "CL_00001644";

		public EntityGolem(World p_i1686_1_) : base(p_i1686_1_)
		{
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal virtual void fall(float p_70069_1_)
		{
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal virtual string LivingSound
		{
			get
			{
				return "none";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal virtual string HurtSound
		{
			get
			{
				return "none";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal virtual string DeathSound
		{
			get
			{
				return "none";
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
			return false;
		}
	}

}