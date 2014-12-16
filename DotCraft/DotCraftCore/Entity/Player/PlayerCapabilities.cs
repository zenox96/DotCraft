namespace DotCraftCore.Entity.Player
{

	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;

	public class PlayerCapabilities
	{
	/// <summary> Disables player damage.  </summary>
		public bool disableDamage;

	/// <summary> Sets/indicates whether the player is flying.  </summary>
		public bool isFlying;

	/// <summary> whether or not to allow the player to fly when they double jump.  </summary>
		public bool allowFlying;

///    
///     <summary> * Used to determine if creative mode is enabled, and therefore if items should be depleted on usage </summary>
///     
		public bool isCreativeMode;

	/// <summary> Indicates whether the player is allowed to modify the surroundings  </summary>
		public bool allowEdit = true;
		private float flySpeed = 0.05F;
		private float walkSpeed = 0.1F;
		

		public virtual void writeCapabilitiesToNBT(NBTTagCompound p_75091_1_)
		{
			NBTTagCompound var2 = new NBTTagCompound();
			var2.setBoolean("invulnerable", this.disableDamage);
			var2.setBoolean("flying", this.isFlying);
			var2.setBoolean("mayfly", this.allowFlying);
			var2.setBoolean("instabuild", this.isCreativeMode);
			var2.setBoolean("mayBuild", this.allowEdit);
			var2.setFloat("flySpeed", this.flySpeed);
			var2.setFloat("walkSpeed", this.walkSpeed);
			p_75091_1_.setTag("abilities", var2);
		}

		public virtual void readCapabilitiesFromNBT(NBTTagCompound p_75095_1_)
		{
			if (p_75095_1_.func_150297_b("abilities", 10))
			{
				NBTTagCompound var2 = p_75095_1_.getCompoundTag("abilities");
				this.disableDamage = var2.getBoolean("invulnerable");
				this.isFlying = var2.getBoolean("flying");
				this.allowFlying = var2.getBoolean("mayfly");
				this.isCreativeMode = var2.getBoolean("instabuild");

				if (var2.func_150297_b("flySpeed", 99))
				{
					this.flySpeed = var2.getFloat("flySpeed");
					this.walkSpeed = var2.getFloat("walkSpeed");
				}

				if (var2.func_150297_b("mayBuild", 1))
				{
					this.allowEdit = var2.getBoolean("mayBuild");
				}
			}
		}

		public virtual float FlySpeed
		{
			get
			{
				return this.flySpeed;
			}
			set
			{
				this.flySpeed = value;
			}
		}


		public virtual float WalkSpeed
		{
			get
			{
				return this.walkSpeed;
			}
		}

		public virtual float PlayerWalkSpeed
		{
			set
			{
				this.walkSpeed = value;
			}
		}
	}

}