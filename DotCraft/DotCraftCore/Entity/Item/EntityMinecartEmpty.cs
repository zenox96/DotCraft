namespace DotCraftCore.Entity.Item
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using World = DotCraftCore.world.World;

	public class EntityMinecartEmpty : EntityMinecart
	{
		private const string __OBFID = "CL_00001677";

		public EntityMinecartEmpty(World p_i1722_1_) : base(p_i1722_1_)
		{
		}

		public EntityMinecartEmpty(World p_i1723_1_, double p_i1723_2_, double p_i1723_4_, double p_i1723_6_) : base(p_i1723_1_, p_i1723_2_, p_i1723_4_, p_i1723_6_)
		{
		}

///    
///     <summary> * First layer of player interaction </summary>
///     
		public override bool interactFirst(EntityPlayer p_130002_1_)
		{
			if (this.riddenByEntity != null && this.riddenByEntity is EntityPlayer && this.riddenByEntity != p_130002_1_)
			{
				return true;
			}
			else if (this.riddenByEntity != null && this.riddenByEntity != p_130002_1_)
			{
				return false;
			}
			else
			{
				if (!this.worldObj.isClient)
				{
					p_130002_1_.mountEntity(this);
				}

				return true;
			}
		}

		public override int MinecartType
		{
			get
			{
				return 0;
			}
		}
	}

}