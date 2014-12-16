namespace DotCraftCore.Entity.Monster
{

	using Block = DotCraftCore.block.Block;
	using BlockSilverfish = DotCraftCore.block.BlockSilverfish;
	using Entity = DotCraftCore.Entity.Entity;
	using EnumCreatureAttribute = DotCraftCore.Entity.EnumCreatureAttribute;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Item = DotCraftCore.Item.Item;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using EntityDamageSource = DotCraftCore.Util.EntityDamageSource;
	using Facing = DotCraftCore.Util.Facing;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;
	using ImmutablePair = org.apache.commons.lang3.tuple.ImmutablePair;

	public class EntitySilverfish : EntityMob
	{
///    
///     <summary> * A cooldown before this entity will search for another Silverfish to join them in battle. </summary>
///     
		private int allySummonCooldown;
		

		public EntitySilverfish(World p_i1740_1_) : base(p_i1740_1_)
		{
			this.setSize(0.3F, 0.7F);
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 8.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.6000000238418579D;
			this.getEntityAttribute(SharedMonsterAttributes.attackDamage).BaseValue = 1.0D;
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return false;
		}

///    
///     <summary> * Finds the closest player within 16 blocks to attack, or null if this Entity isn't interested in attacking
///     * (Animals, Spiders at day, peaceful PigZombies). </summary>
///     
		protected internal override Entity findPlayerToAttack()
		{
			double var1 = 8.0D;
			return this.worldObj.getClosestVulnerablePlayerToEntity(this, var1);
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.silverfish.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.silverfish.hit";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.silverfish.kill";
			}
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
				if (this.allySummonCooldown <= 0 && (p_70097_1_ is EntityDamageSource || p_70097_1_ == DamageSource.magic))
				{
					this.allySummonCooldown = 20;
				}

				return base.attackEntityFrom(p_70097_1_, p_70097_2_);
			}
		}

///    
///     <summary> * Basic mob attack. Default to touch of death in EntityCreature. Overridden by each mob to define their attack. </summary>
///     
		protected internal override void attackEntity(Entity p_70785_1_, float p_70785_2_)
		{
			if (this.attackTime <= 0 && p_70785_2_ < 1.2F && p_70785_1_.boundingBox.maxY > this.boundingBox.minY && p_70785_1_.boundingBox.minY < this.boundingBox.maxY)
			{
				this.attackTime = 20;
				this.attackEntityAsMob(p_70785_1_);
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.silverfish.step", 0.15F, 1.0F);
		}

		protected internal override Item func_146068_u()
		{
			return Item.getItemById(0);
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			this.renderYawOffset = this.rotationYaw;
			base.onUpdate();
		}

		protected internal override void updateEntityActionState()
		{
			base.updateEntityActionState();

			if (!this.worldObj.isClient)
			{
				int var1;
				int var2;
				int var3;
				int var6;

				if (this.allySummonCooldown > 0)
				{
					--this.allySummonCooldown;

					if (this.allySummonCooldown == 0)
					{
						var1 = MathHelper.floor_double(this.posX);
						var2 = MathHelper.floor_double(this.posY);
						var3 = MathHelper.floor_double(this.posZ);
						bool var4 = false;

						for (int var5 = 0; !var4 && var5 <= 5 && var5 >= -5; var5 = var5 <= 0 ? 1 - var5 : 0 - var5)
						{
							for (var6 = 0; !var4 && var6 <= 10 && var6 >= -10; var6 = var6 <= 0 ? 1 - var6 : 0 - var6)
							{
								for (int var7 = 0; !var4 && var7 <= 10 && var7 >= -10; var7 = var7 <= 0 ? 1 - var7 : 0 - var7)
								{
									if (this.worldObj.getBlock(var1 + var6, var2 + var5, var3 + var7) == Blocks.monster_egg)
									{
										if (!this.worldObj.GameRules.getGameRuleBooleanValue("mobGriefing"))
										{
											int var8 = this.worldObj.getBlockMetadata(var1 + var6, var2 + var5, var3 + var7);
											ImmutablePair var9 = BlockSilverfish.func_150197_b(var8);
											this.worldObj.setBlock(var1 + var6, var2 + var5, var3 + var7, (Block)var9.Left, (int)((int?)var9.Right), 3);
										}
										else
										{
											this.worldObj.func_147480_a(var1 + var6, var2 + var5, var3 + var7, false);
										}

										Blocks.monster_egg.onBlockDestroyedByPlayer(this.worldObj, var1 + var6, var2 + var5, var3 + var7, 0);

										if (this.rand.nextBoolean())
										{
											var4 = true;
											break;
										}
									}
								}
							}
						}
					}
				}

				if (this.entityToAttack == null && !this.hasPath())
				{
					var1 = MathHelper.floor_double(this.posX);
					var2 = MathHelper.floor_double(this.posY + 0.5D);
					var3 = MathHelper.floor_double(this.posZ);
					int var10 = this.rand.Next(6);
					Block var11 = this.worldObj.getBlock(var1 + Facing.offsetsXForSide[var10], var2 + Facing.offsetsYForSide[var10], var3 + Facing.offsetsZForSide[var10]);
					var6 = this.worldObj.getBlockMetadata(var1 + Facing.offsetsXForSide[var10], var2 + Facing.offsetsYForSide[var10], var3 + Facing.offsetsZForSide[var10]);

					if (BlockSilverfish.func_150196_a(var11))
					{
						this.worldObj.setBlock(var1 + Facing.offsetsXForSide[var10], var2 + Facing.offsetsYForSide[var10], var3 + Facing.offsetsZForSide[var10], Blocks.monster_egg, BlockSilverfish.func_150195_a(var11, var6), 3);
						this.spawnExplosionParticle();
						this.setDead();
					}
					else
					{
						this.updateWanderPath();
					}
				}
				else if (this.entityToAttack != null && !this.hasPath())
				{
					this.entityToAttack = null;
				}
			}
		}

///    
///     <summary> * Takes a coordinate in and returns a weight to determine how likely this creature will try to path to the block.
///     * Args: x, y, z </summary>
///     
		public override float getBlockPathWeight(int p_70783_1_, int p_70783_2_, int p_70783_3_)
		{
			return this.worldObj.getBlock(p_70783_1_, p_70783_2_ - 1, p_70783_3_) == Blocks.stone ? 10.0F : base.getBlockPathWeight(p_70783_1_, p_70783_2_, p_70783_3_);
		}

///    
///     <summary> * Checks to make sure the light is not too bright where the mob is spawning </summary>
///     
		protected internal override bool isValidLightLevel()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public override bool CanSpawnHere
		{
			get
			{
				if (base.CanSpawnHere)
				{
					EntityPlayer var1 = this.worldObj.getClosestPlayerToEntity(this, 5.0D);
					return var1 == null;
				}
				else
				{
					return false;
				}
			}
		}

///    
///     <summary> * Get this Entity's EnumCreatureAttribute </summary>
///     
		public override EnumCreatureAttribute CreatureAttribute
		{
			get
			{
				return EnumCreatureAttribute.ARTHROPOD;
			}
		}
	}

}