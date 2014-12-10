namespace DotCraftCore.Entity.Boss
{

	using Entity = DotCraftCore.Entity.Entity;
	using IEntityMultiPart = DotCraftCore.Entity.IEntityMultiPart;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using DamageSource = DotCraftCore.Util.DamageSource;

	public class EntityDragonPart : Entity
	{
	/// <summary> The dragon entity this dragon part belongs to  </summary>
		public readonly IEntityMultiPart entityDragonObj;
		public readonly string field_146032_b;
		private const string __OBFID = "CL_00001657";

		public EntityDragonPart(IEntityMultiPart p_i1697_1_, string p_i1697_2_, float p_i1697_3_, float p_i1697_4_) : base(p_i1697_1_.func_82194_d())
		{
			this.setSize(p_i1697_3_, p_i1697_4_);
			this.entityDragonObj = p_i1697_1_;
			this.field_146032_b = p_i1697_2_;
		}

		protected internal override void entityInit()
		{
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
		}

///    
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public override bool canBeCollidedWith()
		{
			return true;
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			return this.EntityInvulnerable ? false : this.entityDragonObj.attackEntityFromPart(this, p_70097_1_, p_70097_2_);
		}

///    
///     <summary> * Returns true if Entity argument is equal to this Entity </summary>
///     
		public override bool isEntityEqual(Entity p_70028_1_)
		{
			return this == p_70028_1_ || this.entityDragonObj == p_70028_1_;
		}
	}

}