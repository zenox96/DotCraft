using System;

namespace DotCraftCore.nEntity.nPassive
{

	using Block = DotCraftCore.nBlock.Block;
	using EntityAgeable = DotCraftCore.nEntity.EntityAgeable;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using EntityAIFollowParent = DotCraftCore.nEntity.nAI.EntityAIFollowParent;
	using EntityAILookIdle = DotCraftCore.nEntity.nAI.EntityAILookIdle;
	using EntityAIMate = DotCraftCore.nEntity.nAI.EntityAIMate;
	using EntityAIPanic = DotCraftCore.nEntity.nAI.EntityAIPanic;
	using EntityAISwimming = DotCraftCore.nEntity.nAI.EntityAISwimming;
	using EntityAITempt = DotCraftCore.nEntity.nAI.EntityAITempt;
	using EntityAIWander = DotCraftCore.nEntity.nAI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.nEntity.nAI.EntityAIWatchClosest;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemSeeds = DotCraftCore.nItem.ItemSeeds;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class EntityChicken : EntityAnimal
	{
		public float field_70886_e;
		public float destPos;
		public float field_70884_g;
		public float field_70888_h;
		public float field_70889_i = 1.0F;

	/// <summary> The time until the next egg is spawned.  </summary>
		public int timeUntilNextEgg;
		public bool field_152118_bv;
		

		public EntityChicken(World p_i1682_1_) : base(p_i1682_1_)
		{
			this.setSize(0.3F, 0.7F);
			this.timeUntilNextEgg = this.rand.Next(6000) + 6000;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIPanic(this, 1.4D));
			this.tasks.addTask(2, new EntityAIMate(this, 1.0D));
			this.tasks.addTask(3, new EntityAITempt(this, 1.0D, Items.wheat_seeds, false));
			this.tasks.addTask(4, new EntityAIFollowParent(this, 1.1D));
			this.tasks.addTask(5, new EntityAIWander(this, 1.0D));
			this.tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(7, new EntityAILookIdle(this));
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		public override bool isAIEnabled()
		{
			get
			{
				return true;
			}
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 4.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.25D;
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			this.field_70888_h = this.field_70886_e;
			this.field_70884_g = this.destPos;
			this.destPos = (float)((double)this.destPos + (double)(this.onGround ? -1 : 4) * 0.3D);

			if (this.destPos < 0.0F)
			{
				this.destPos = 0.0F;
			}

			if (this.destPos > 1.0F)
			{
				this.destPos = 1.0F;
			}

			if (!this.onGround && this.field_70889_i < 1.0F)
			{
				this.field_70889_i = 1.0F;
			}

			this.field_70889_i = (float)((double)this.field_70889_i * 0.9D);

			if (!this.onGround && this.motionY < 0.0D)
			{
				this.motionY *= 0.6D;
			}

			this.field_70886_e += this.field_70889_i * 2.0F;

			if (!this.worldObj.isClient && !this.Child && !this.func_152116_bZ() && --this.timeUntilNextEgg <= 0)
			{
				this.playSound("mob.chicken.plop", 1.0F, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
				this.func_145779_a(Items.egg, 1);
				this.timeUntilNextEgg = this.rand.Next(6000) + 6000;
			}
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.chicken.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.chicken.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.chicken.hurt";
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.chicken.step", 0.15F, 1.0F);
		}

		protected internal override Item func_146068_u()
		{
			return Items.feather;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(3) + this.rand.Next(1 + p_70628_2_);

			for (int var4 = 0; var4 < var3; ++var4)
			{
				this.func_145779_a(Items.feather, 1);
			}

			if (this.Burning)
			{
				this.func_145779_a(Items.cooked_chicken, 1);
			}
			else
			{
				this.func_145779_a(Items.chicken, 1);
			}
		}

		public override EntityChicken createChild(EntityAgeable p_90011_1_)
		{
			return new EntityChicken(this.worldObj);
		}

///    
///     <summary> * Checks if the parameter is an item which this animal can be fed to breed it (wheat, carrots or seeds depending on
///     * the animal type) </summary>
///     
		public override bool isBreedingItem(ItemStack p_70877_1_)
		{
			return p_70877_1_ != null && p_70877_1_.Item is ItemSeeds;
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.field_152118_bv = p_70037_1_.getBoolean("IsChickenJockey");
		}

///    
///     <summary> * Get the experience points the entity currently has. </summary>
///     
		protected internal override int getExperiencePoints(EntityPlayer p_70693_1_)
		{
			return this.func_152116_bZ() ? 10 : base.getExperiencePoints(p_70693_1_);
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setBoolean("IsChickenJockey", this.field_152118_bv);
		}

///    
///     <summary> * Determines if an entity can be despawned, used on idle far away entities </summary>
///     
		protected internal override bool canDespawn()
		{
			return this.func_152116_bZ() && this.riddenByEntity == null;
		}

		public override void updateRiderPosition()
		{
			base.updateRiderPosition();
			float var1 = MathHelper.sin(this.renderYawOffset * (float)Math.PI / 180.0F);
			float var2 = MathHelper.cos(this.renderYawOffset * (float)Math.PI / 180.0F);
			float var3 = 0.1F;
			float var4 = 0.0F;
			this.riddenByEntity.setPosition(this.posX + (double)(var3 * var1), this.posY + (double)(this.height * 0.5F) + this.riddenByEntity.YOffset + (double)var4, this.posZ - (double)(var3 * var2));

			if (this.riddenByEntity is EntityLivingBase)
			{
				((EntityLivingBase)this.riddenByEntity).renderYawOffset = this.renderYawOffset;
			}
		}

		public virtual bool func_152116_bZ()
		{
			return this.field_152118_bv;
		}

		public virtual void func_152117_i(bool p_152117_1_)
		{
			this.field_152118_bv = p_152117_1_;
		}
	}

}