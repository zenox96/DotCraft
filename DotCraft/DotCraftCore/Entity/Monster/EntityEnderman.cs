using System;

namespace DotCraftCore.Entity.Monster
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Entity = DotCraftCore.Entity.Entity;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using AttributeModifier = DotCraftCore.Entity.AI.Attributes.AttributeModifier;
	using IAttributeInstance = DotCraftCore.Entity.AI.Attributes.IAttributeInstance;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using EntityDamageSource = DotCraftCore.Util.EntityDamageSource;
	using EntityDamageSourceIndirect = DotCraftCore.Util.EntityDamageSourceIndirect;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using World = DotCraftCore.World.World;

	public class EntityEnderman : EntityMob
	{
		private static readonly UUID attackingSpeedBoostModifierUUID = UUID.fromString("020E0DFB-87AE-4653-9556-831010E291A0");
		private static readonly AttributeModifier attackingSpeedBoostModifier = (new AttributeModifier(attackingSpeedBoostModifierUUID, "Attacking speed boost", 6.199999809265137D, 0)).Saved = false;
		private static bool[] carriableBlocks = new bool[256];

///    
///     <summary> * Counter to delay the teleportation of an enderman towards the currently attacked target </summary>
///     
		private int teleportDelay;

///    
///     <summary> * A player must stare at an enderman for 5 ticks before it becomes aggressive. This field counts those ticks. </summary>
///     
		private int stareTimer;
		private Entity lastEntityToAttack;
		private bool isAggressive;
		

		public EntityEnderman(World p_i1734_1_) : base(p_i1734_1_)
		{
			this.setSize(0.6F, 2.9F);
			this.stepHeight = 1.0F;
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 40.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.30000001192092896D;
			this.getEntityAttribute(SharedMonsterAttributes.attackDamage).BaseValue = 7.0D;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, new sbyte?((sbyte)0));
			this.dataWatcher.addObject(17, new sbyte?((sbyte)0));
			this.dataWatcher.addObject(18, new sbyte?((sbyte)0));
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setShort("carried", (short)Block.getIdFromBlock(this.func_146080_bZ()));
			p_70014_1_.setShort("carriedData", (short)this.CarryingData);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.func_146081_a(Block.getBlockById(p_70037_1_.getShort("carried")));
			this.CarryingData = p_70037_1_.getShort("carriedData");
		}

///    
///     <summary> * Finds the closest player within 16 blocks to attack, or null if this Entity isn't interested in attacking
///     * (Animals, Spiders at day, peaceful PigZombies). </summary>
///     
		protected internal override Entity findPlayerToAttack()
		{
			EntityPlayer var1 = this.worldObj.getClosestVulnerablePlayerToEntity(this, 64.0D);

			if (var1 != null)
			{
				if (this.shouldAttackPlayer(var1))
				{
					this.isAggressive = true;

					if (this.stareTimer == 0)
					{
						this.worldObj.playSoundEffect(var1.posX, var1.posY, var1.posZ, "mob.endermen.stare", 1.0F, 1.0F);
					}

					if (this.stareTimer++ == 5)
					{
						this.stareTimer = 0;
						this.Screaming = true;
						return var1;
					}
				}
				else
				{
					this.stareTimer = 0;
				}
			}

			return null;
		}

///    
///     <summary> * Checks to see if this enderman should be attacking this player </summary>
///     
		private bool shouldAttackPlayer(EntityPlayer p_70821_1_)
		{
			ItemStack var2 = p_70821_1_.inventory.armorInventory[3];

			if (var2 != null && var2.Item == Item.getItemFromBlock(Blocks.pumpkin))
			{
				return false;
			}
			else
			{
				Vec3 var3 = p_70821_1_.getLook(1.0F).normalize();
				Vec3 var4 = Vec3.createVectorHelper(this.posX - p_70821_1_.posX, this.boundingBox.minY + (double)(this.height / 2.0F) - (p_70821_1_.posY + (double)p_70821_1_.EyeHeight), this.posZ - p_70821_1_.posZ);
				double var5 = var4.lengthVector();
				var4 = var4.normalize();
				double var7 = var3.dotProduct(var4);
				return var7 > 1.0D - 0.025D / var5 && p_70821_1_.canEntityBeSeen(this);
			}
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			if (this.Wet)
			{
				this.attackEntityFrom(DamageSource.drown, 1.0F);
			}

			if (this.lastEntityToAttack != this.entityToAttack)
			{
				IAttributeInstance var1 = this.getEntityAttribute(SharedMonsterAttributes.movementSpeed);
				var1.removeModifier(attackingSpeedBoostModifier);

				if (this.entityToAttack != null)
				{
					var1.applyModifier(attackingSpeedBoostModifier);
				}
			}

			this.lastEntityToAttack = this.entityToAttack;
			int var6;

			if (!this.worldObj.isClient && this.worldObj.GameRules.getGameRuleBooleanValue("mobGriefing"))
			{
				int var2;
				int var3;
				Block var4;

				if (this.func_146080_bZ().Material == Material.air)
				{
					if (this.rand.Next(20) == 0)
					{
						var6 = MathHelper.floor_double(this.posX - 2.0D + this.rand.NextDouble() * 4.0D);
						var2 = MathHelper.floor_double(this.posY + this.rand.NextDouble() * 3.0D);
						var3 = MathHelper.floor_double(this.posZ - 2.0D + this.rand.NextDouble() * 4.0D);
						var4 = this.worldObj.getBlock(var6, var2, var3);

						if (carriableBlocks[Block.getIdFromBlock(var4)])
						{
							this.func_146081_a(var4);
							this.CarryingData = this.worldObj.getBlockMetadata(var6, var2, var3);
							this.worldObj.setBlock(var6, var2, var3, Blocks.air);
						}
					}
				}
				else if (this.rand.Next(2000) == 0)
				{
					var6 = MathHelper.floor_double(this.posX - 1.0D + this.rand.NextDouble() * 2.0D);
					var2 = MathHelper.floor_double(this.posY + this.rand.NextDouble() * 2.0D);
					var3 = MathHelper.floor_double(this.posZ - 1.0D + this.rand.NextDouble() * 2.0D);
					var4 = this.worldObj.getBlock(var6, var2, var3);
					Block var5 = this.worldObj.getBlock(var6, var2 - 1, var3);

					if (var4.Material == Material.air && var5.Material != Material.air && var5.renderAsNormalBlock())
					{
						this.worldObj.setBlock(var6, var2, var3, this.func_146080_bZ(), this.CarryingData, 3);
						this.func_146081_a(Blocks.air);
					}
				}
			}

			for (var6 = 0; var6 < 2; ++var6)
			{
				this.worldObj.spawnParticle("portal", this.posX + (this.rand.NextDouble() - 0.5D) * (double)this.width, this.posY + this.rand.NextDouble() * (double)this.height - 0.25D, this.posZ + (this.rand.NextDouble() - 0.5D) * (double)this.width, (this.rand.NextDouble() - 0.5D) * 2.0D, -this.rand.NextDouble(), (this.rand.NextDouble() - 0.5D) * 2.0D);
			}

			if (this.worldObj.Daytime && !this.worldObj.isClient)
			{
				float var7 = this.getBrightness(1.0F);

				if (var7 > 0.5F && this.worldObj.canBlockSeeTheSky(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)) && this.rand.nextFloat() * 30.0F < (var7 - 0.4F) * 2.0F)
				{
					this.entityToAttack = null;
					this.Screaming = false;
					this.isAggressive = false;
					this.teleportRandomly();
				}
			}

			if (this.Wet || this.Burning)
			{
				this.entityToAttack = null;
				this.Screaming = false;
				this.isAggressive = false;
				this.teleportRandomly();
			}

			if (this.Screaming && !this.isAggressive && this.rand.Next(100) == 0)
			{
				this.Screaming = false;
			}

			this.isJumping = false;

			if (this.entityToAttack != null)
			{
				this.faceEntity(this.entityToAttack, 100.0F, 100.0F);
			}

			if (!this.worldObj.isClient && this.EntityAlive)
			{
				if (this.entityToAttack != null)
				{
					if (this.entityToAttack is EntityPlayer && this.shouldAttackPlayer((EntityPlayer)this.entityToAttack))
					{
						if (this.entityToAttack.getDistanceSqToEntity(this) < 16.0D)
						{
							this.teleportRandomly();
						}

						this.teleportDelay = 0;
					}
					else if (this.entityToAttack.getDistanceSqToEntity(this) > 256.0D && this.teleportDelay++ >= 30 && this.teleportToEntity(this.entityToAttack))
					{
						this.teleportDelay = 0;
					}
				}
				else
				{
					this.Screaming = false;
					this.teleportDelay = 0;
				}
			}

			base.onLivingUpdate();
		}

///    
///     <summary> * Teleport the enderman to a random nearby position </summary>
///     
		protected internal virtual bool teleportRandomly()
		{
			double var1 = this.posX + (this.rand.NextDouble() - 0.5D) * 64.0D;
			double var3 = this.posY + (double)(this.rand.Next(64) - 32);
			double var5 = this.posZ + (this.rand.NextDouble() - 0.5D) * 64.0D;
			return this.teleportTo(var1, var3, var5);
		}

///    
///     <summary> * Teleport the enderman to another entity </summary>
///     
		protected internal virtual bool teleportToEntity(Entity p_70816_1_)
		{
			Vec3 var2 = Vec3.createVectorHelper(this.posX - p_70816_1_.posX, this.boundingBox.minY + (double)(this.height / 2.0F) - p_70816_1_.posY + (double)p_70816_1_.EyeHeight, this.posZ - p_70816_1_.posZ);
			var2 = var2.normalize();
			double var3 = 16.0D;
			double var5 = this.posX + (this.rand.NextDouble() - 0.5D) * 8.0D - var2.xCoord * var3;
			double var7 = this.posY + (double)(this.rand.Next(16) - 8) - var2.yCoord * var3;
			double var9 = this.posZ + (this.rand.NextDouble() - 0.5D) * 8.0D - var2.zCoord * var3;
			return this.teleportTo(var5, var7, var9);
		}

///    
///     <summary> * Teleport the enderman </summary>
///     
		protected internal virtual bool teleportTo(double p_70825_1_, double p_70825_3_, double p_70825_5_)
		{
			double var7 = this.posX;
			double var9 = this.posY;
			double var11 = this.posZ;
			this.posX = p_70825_1_;
			this.posY = p_70825_3_;
			this.posZ = p_70825_5_;
			bool var13 = false;
			int var14 = MathHelper.floor_double(this.posX);
			int var15 = MathHelper.floor_double(this.posY);
			int var16 = MathHelper.floor_double(this.posZ);

			if (this.worldObj.blockExists(var14, var15, var16))
			{
				bool var17 = false;

				while (!var17 && var15 > 0)
				{
					Block var18 = this.worldObj.getBlock(var14, var15 - 1, var16);

					if (var18.Material.blocksMovement())
					{
						var17 = true;
					}
					else
					{
						--this.posY;
						--var15;
					}
				}

				if (var17)
				{
					this.setPosition(this.posX, this.posY, this.posZ);

					if (this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Empty && !this.worldObj.isAnyLiquid(this.boundingBox))
					{
						var13 = true;
					}
				}
			}

			if (!var13)
			{
				this.setPosition(var7, var9, var11);
				return false;
			}
			else
			{
				short var30 = 128;

				for (int var31 = 0; var31 < var30; ++var31)
				{
					double var19 = (double)var31 / ((double)var30 - 1.0D);
					float var21 = (this.rand.nextFloat() - 0.5F) * 0.2F;
					float var22 = (this.rand.nextFloat() - 0.5F) * 0.2F;
					float var23 = (this.rand.nextFloat() - 0.5F) * 0.2F;
					double var24 = var7 + (this.posX - var7) * var19 + (this.rand.NextDouble() - 0.5D) * (double)this.width * 2.0D;
					double var26 = var9 + (this.posY - var9) * var19 + this.rand.NextDouble() * (double)this.height;
					double var28 = var11 + (this.posZ - var11) * var19 + (this.rand.NextDouble() - 0.5D) * (double)this.width * 2.0D;
					this.worldObj.spawnParticle("portal", var24, var26, var28, (double)var21, (double)var22, (double)var23);
				}

				this.worldObj.playSoundEffect(var7, var9, var11, "mob.endermen.portal", 1.0F, 1.0F);
				this.playSound("mob.endermen.portal", 1.0F, 1.0F);
				return true;
			}
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return this.Screaming ? "mob.endermen.scream" : "mob.endermen.idle";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.endermen.hit";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.endermen.death";
			}
		}

		protected internal override Item func_146068_u()
		{
			return Items.ender_pearl;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			Item var3 = this.func_146068_u();

			if (var3 != null)
			{
				int var4 = this.rand.Next(2 + p_70628_2_);

				for (int var5 = 0; var5 < var4; ++var5)
				{
					this.func_145779_a(var3, 1);
				}
			}
		}

		public virtual void func_146081_a(Block p_146081_1_)
		{
			this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(Block.getIdFromBlock(p_146081_1_) & 255)));
		}

		public virtual Block func_146080_bZ()
		{
			return Block.getBlockById(this.dataWatcher.getWatchableObjectByte(16));
		}

///    
///     <summary> * Set the metadata of the block an enderman carries </summary>
///     
		public virtual int CarryingData
		{
			set
			{
				this.dataWatcher.updateObject(17, Convert.ToByte((sbyte)(value & 255)));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectByte(17);
			}
		}

///    
///     <summary> * Get the metadata of the block an enderman carries </summary>
///     

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
				this.Screaming = true;

				if (p_70097_1_ is EntityDamageSource && p_70097_1_.Entity is EntityPlayer)
				{
					this.isAggressive = true;
				}

				if (p_70097_1_ is EntityDamageSourceIndirect)
				{
					this.isAggressive = false;

					for (int var3 = 0; var3 < 64; ++var3)
					{
						if (this.teleportRandomly())
						{
							return true;
						}
					}

					return false;
				}
				else
				{
					return base.attackEntityFrom(p_70097_1_, p_70097_2_);
				}
			}
		}

		public virtual bool isScreaming()
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(18) > 0;
			}
			set
			{
				this.dataWatcher.updateObject(18, Convert.ToByte((sbyte)(value ? 1 : 0)));
			}
		}


		static EntityEnderman()
		{
			carriableBlocks[Block.getIdFromBlock(Blocks.grass)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.dirt)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.sand)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.gravel)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.yellow_flower)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.red_flower)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.brown_mushroom)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.red_mushroom)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.tnt)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.cactus)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.clay)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.pumpkin)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.melon_block)] = true;
			carriableBlocks[Block.getIdFromBlock(Blocks.mycelium)] = true;
		}
	}

}