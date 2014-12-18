namespace DotCraftCore.nEntity.nPassive
{

	using EntityLiving = DotCraftCore.nEntity.EntityLiving;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using World = DotCraftCore.nWorld.World;

	public abstract class EntityAmbientCreature : EntityLiving, IAnimals
	{
		

		public EntityAmbientCreature(World p_i1679_1_) : base(p_i1679_1_)
		{
		}

		public override bool allowLeashing()
		{
			return false;
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		protected internal override bool interact(EntityPlayer p_70085_1_)
		{
			return false;
		}
	}

}