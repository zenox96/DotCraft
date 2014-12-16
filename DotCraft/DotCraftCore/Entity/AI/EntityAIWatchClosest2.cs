using System;

namespace DotCraftCore.Entity.AI
{

	using EntityLiving = DotCraftCore.Entity.EntityLiving;

	public class EntityAIWatchClosest2 : EntityAIWatchClosest
	{
		

		public EntityAIWatchClosest2(EntityLiving p_i1629_1_, Type p_i1629_2_, float p_i1629_3_, float p_i1629_4_) : base(p_i1629_1_, p_i1629_2_, p_i1629_3_, p_i1629_4_)
		{
			this.MutexBits = 3;
		}
	}

}