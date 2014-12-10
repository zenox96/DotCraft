namespace DotCraftCore.Entity.Passive
{

	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using World = DotCraftCore.World.World;

	public abstract class EntityAmbientCreature : EntityLiving, IAnimals
	{
		private const string __OBFID = "CL_00001636";

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