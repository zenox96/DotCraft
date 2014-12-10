using System.Collections;

namespace DotCraftCore.Entity.Monster
{

	using Entity = DotCraftCore.Entity.Entity;
	using IEntityLivingData = DotCraftCore.Entity.IEntityLivingData;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using AttributeModifier = DotCraftCore.Entity.AI.Attributes.AttributeModifier;
	using IAttributeInstance = DotCraftCore.Entity.AI.Attributes.IAttributeInstance;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using DamageSource = DotCraftCore.util.DamageSource;
	using EnumDifficulty = DotCraftCore.world.EnumDifficulty;
	using World = DotCraftCore.world.World;

	public class EntityPigZombie : EntityZombie
	{
		private static readonly UUID field_110189_bq = UUID.fromString("49455A49-7EC5-45BA-B886-3B90B23A1718");
		private static readonly AttributeModifier field_110190_br = (new AttributeModifier(field_110189_bq, "Attacking speed boost", 0.45D, 0)).Saved = false;

	/// <summary> Above zero if this PigZombie is Angry.  </summary>
		private int angerLevel;

	/// <summary> A random delay until this PigZombie next makes a sound.  </summary>
		private int randomSoundDelay;
		private Entity field_110191_bu;
		private const string __OBFID = "CL_00001693";

		public EntityPigZombie(World p_i1739_1_) : base(p_i1739_1_)
		{
			this.isImmuneToFire = true;
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(field_110186_bp).BaseValue = 0.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.5D;
			this.getEntityAttribute(SharedMonsterAttributes.attackDamage).BaseValue = 5.0D;
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		protected internal override bool isAIEnabled()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			if (this.field_110191_bu != this.entityToAttack && !this.worldObj.isClient)
			{
				IAttributeInstance var1 = this.getEntityAttribute(SharedMonsterAttributes.movementSpeed);
				var1.removeModifier(field_110190_br);

				if (this.entityToAttack != null)
				{
					var1.applyModifier(field_110190_br);
				}
			}

			this.field_110191_bu = this.entityToAttack;

			if (this.randomSoundDelay > 0 && --this.randomSoundDelay == 0)
			{
				this.playSound("mob.zombiepig.zpigangry", this.SoundVolume * 2.0F, ((this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F) * 1.8F);
			}

			base.onUpdate();
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
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setShort("Anger", (short)this.angerLevel);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.angerLevel = p_70037_1_.getShort("Anger");
		}

///    
///     <summary> * Finds the closest player within 16 blocks to attack, or null if this Entity isn't interested in attacking
///     * (Animals, Spiders at day, peaceful PigZombies). </summary>
///     
		protected internal override Entity findPlayerToAttack()
		{
			return this.angerLevel == 0 ? null : base.findPlayerToAttack();
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			if (this.EntityInvulnerable)
			{
				return false;
			}
			else
			{
				Entity var3 = p_70097_1_.Entity;

				if (var3 is EntityPlayer)
				{
					IList var4 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.expand(32.0D, 32.0D, 32.0D));

					for (int var5 = 0; var5 < var4.Count; ++var5)
					{
						Entity var6 = (Entity)var4[var5];

						if (var6 is EntityPigZombie)
						{
							EntityPigZombie var7 = (EntityPigZombie)var6;
							var7.becomeAngryAt(var3);
						}
					}

					this.becomeAngryAt(var3);
				}

				return base.attackEntityFrom(p_70097_1_, p_70097_2_);
			}
		}

///    
///     <summary> * Causes this PigZombie to become angry at the supplied Entity (which will be a player). </summary>
///     
		private void becomeAngryAt(Entity p_70835_1_)
		{
			this.entityToAttack = p_70835_1_;
			this.angerLevel = 400 + this.rand.Next(400);
			this.randomSoundDelay = this.rand.Next(40);
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.zombiepig.zpig";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.zombiepig.zpighurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.zombiepig.zpigdeath";
			}
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(2 + p_70628_2_);
			int var4;

			for (var4 = 0; var4 < var3; ++var4)
			{
				this.func_145779_a(Items.rotten_flesh, 1);
			}

			var3 = this.rand.Next(2 + p_70628_2_);

			for (var4 = 0; var4 < var3; ++var4)
			{
				this.func_145779_a(Items.gold_nugget, 1);
			}
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			return false;
		}

		protected internal override void dropRareDrop(int p_70600_1_)
		{
			this.func_145779_a(Items.gold_ingot, 1);
		}

///    
///     <summary> * Makes entity wear random armor based on difficulty </summary>
///     
		protected internal override void addRandomArmor()
		{
			this.setCurrentItemOrArmor(0, new ItemStack(Items.golden_sword));
		}

		public override IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			base.onSpawnWithEgg(p_110161_1_);
			this.Villager = false;
			return p_110161_1_;
		}
	}

}