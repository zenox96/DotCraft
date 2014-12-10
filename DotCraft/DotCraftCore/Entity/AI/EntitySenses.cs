using System.Collections;

namespace DotCraftCore.Entity.AI
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;

	public class EntitySenses
	{
		internal EntityLiving entityObj;

	/// <summary> Cache of entities which we can see  </summary>
		internal IList seenEntities = new ArrayList();

	/// <summary> Cache of entities which we cannot see  </summary>
		internal IList unseenEntities = new ArrayList();
		private const string __OBFID = "CL_00001628";

		public EntitySenses(EntityLiving p_i1672_1_)
		{
			this.entityObj = p_i1672_1_;
		}

///    
///     <summary> * Clears canSeeCachePositive and canSeeCacheNegative. </summary>
///     
		public virtual void clearSensingCache()
		{
			this.seenEntities.Clear();
			this.unseenEntities.Clear();
		}

///    
///     <summary> * Checks, whether 'our' entity can see the entity given as argument (true) or not (false), caching the result. </summary>
///     
		public virtual bool canSee(Entity p_75522_1_)
		{
			if (this.seenEntities.Contains(p_75522_1_))
			{
				return true;
			}
			else if (this.unseenEntities.Contains(p_75522_1_))
			{
				return false;
			}
			else
			{
				this.entityObj.worldObj.theProfiler.startSection("canSee");
				bool var2 = this.entityObj.canEntityBeSeen(p_75522_1_);
				this.entityObj.worldObj.theProfiler.endSection();

				if (var2)
				{
					this.seenEntities.Add(p_75522_1_);
				}
				else
				{
					this.unseenEntities.Add(p_75522_1_);
				}

				return var2;
			}
		}
	}

}