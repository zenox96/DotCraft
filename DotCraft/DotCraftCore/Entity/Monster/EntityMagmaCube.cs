namespace DotCraftCore.nEntity.nMonster
{

	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using EnumDifficulty = DotCraftCore.nWorld.EnumDifficulty;
	using World = DotCraftCore.nWorld.World;

	public class EntityMagmaCube : EntitySlime
	{
		

		public EntityMagmaCube(World p_i1737_1_) : base(p_i1737_1_)
		{
			this.isImmuneToFire = true;
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.20000000298023224D;
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public override bool CanSpawnHere
		{
			get
			{
				return this.worldObj.difficultySetting != EnumDifficulty.PEACEFUL && this.worldObj.checkNoEntityCollision(this.boundingBox) && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Empty && !this.worldObj.isAnyLiquid(this.boundingBox);
			}
		}

///    
///     <summary> * Returns the current armor value as determined by a call to InventoryPlayer.getTotalArmorValue </summary>
///     
		public override int TotalArmorValue
		{
			get
			{
				return this.SlimeSize * 3;
			}
		}

		public override int getBrightnessForRender(float p_70070_1_)
		{
			return 15728880;
		}

///    
///     <summary> * Gets how bright this entity is. </summary>
///     
		public override float getBrightness(float p_70013_1_)
		{
			return 1.0F;
		}

///    
///     <summary> * Returns the name of a particle effect that may be randomly created by EntitySlime.onUpdate() </summary>
///     
		protected internal override string SlimeParticle
		{
			get
			{
				return "flame";
			}
		}

		protected internal override EntitySlime createInstance()
		{
			return new EntityMagmaCube(this.worldObj);
		}

		protected internal override Item func_146068_u()
		{
			return Items.magma_cream;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			Item var3 = this.func_146068_u();

			if (var3 != null && this.SlimeSize > 1)
			{
				int var4 = this.rand.Next(4) - 2;

				if (p_70628_2_ > 0)
				{
					var4 += this.rand.Next(p_70628_2_ + 1);
				}

				for (int var5 = 0; var5 < var4; ++var5)
				{
					this.func_145779_a(var3, 1);
				}
			}
		}

///    
///     <summary> * Returns true if the entity is on fire. Used by render to add the fire effect on rendering. </summary>
///     
		public override bool isBurning()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Gets the amount of time the slime needs to wait between jumps. </summary>
///     
		protected internal override int JumpDelay
		{
			get
			{
				return base.JumpDelay * 4;
			}
		}

		protected internal override void alterSquishAmount()
		{
			this.squishAmount *= 0.9F;
		}

///    
///     <summary> * Causes this entity to do an upwards motion (jumping). </summary>
///     
		protected internal override void jump()
		{
			this.motionY = (double)(0.42F + (float)this.SlimeSize * 0.1F);
			this.isAirBorne = true;
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
		}

///    
///     <summary> * Indicates weather the slime is able to damage the player (based upon the slime's size) </summary>
///     
		protected internal override bool canDamagePlayer()
		{
			return true;
		}

///    
///     <summary> * Gets the amount of damage dealt to the player when "attacked" by the slime. </summary>
///     
		protected internal override int AttackStrength
		{
			get
			{
				return base.AttackStrength + 2;
			}
		}

///    
///     <summary> * Returns the name of the sound played when the slime jumps. </summary>
///     
		protected internal override string JumpSound
		{
			get
			{
				return this.SlimeSize > 1 ? "mob.magmacube.big" : "mob.magmacube.small";
			}
		}

///    
///     <summary> * Whether or not the current entity is in lava </summary>
///     
		public override bool handleLavaMovement()
		{
			return false;
		}

///    
///     <summary> * Returns true if the slime makes a sound when it lands after a jump (based upon the slime's size) </summary>
///     
		protected internal override bool makesSoundOnLand()
		{
			return true;
		}
	}

}