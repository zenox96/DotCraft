using System;
using System.Collections;

namespace DotCraftCore.Entity.Item
{

	using Material = DotCraftCore.block.material.Material;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using AchievementList = DotCraftCore.stats.AchievementList;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MathHelper = DotCraftCore.util.MathHelper;
	using StatCollector = DotCraftCore.util.StatCollector;
	using World = DotCraftCore.world.World;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class EntityItem : Entity
	{
		private static readonly Logger logger = LogManager.Logger;

///    
///     <summary> * The age of this EntityItem (used to animate it up and down as well as expire it) </summary>
///     
		public int age;
		public int delayBeforeCanPickup;

	/// <summary> The health of this EntityItem. (For example, damage for tools)  </summary>
		private int health;
		private string field_145801_f;
		private string field_145802_g;

	/// <summary> The EntityItem's random initial float height.  </summary>
		public float hoverStart;
		private const string __OBFID = "CL_00001669";

		public EntityItem(World p_i1709_1_, double p_i1709_2_, double p_i1709_4_, double p_i1709_6_) : base(p_i1709_1_)
		{
			this.health = 5;
			this.hoverStart = (float)(new Random(1).NextDouble() * Math.PI * 2.0D);
			this.setSize(0.25F, 0.25F);
			this.yOffset = this.height / 2.0F;
			this.setPosition(p_i1709_2_, p_i1709_4_, p_i1709_6_);
			this.rotationYaw = (float)(new Random(2).NextDouble() * 360.0D);
			this.motionX = (double)((float)(new Random(3).NextDouble() * 0.20000000298023224D - 0.10000000149011612D));
			this.motionY = 0.20000000298023224D;
			this.motionZ = (double)((float)(new Random(4).NextDouble() * 0.20000000298023224D - 0.10000000149011612D));
		}

		public EntityItem(World p_i1710_1_, double p_i1710_2_, double p_i1710_4_, double p_i1710_6_, ItemStack p_i1710_8_) : this(p_i1710_1_, p_i1710_2_, p_i1710_4_, p_i1710_6_)
		{
			this.EntityItemStack = p_i1710_8_;
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		public EntityItem(World p_i1711_1_) : base(p_i1711_1_)
		{
			this.health = 5;
			this.hoverStart = (float)(new Random(1).NextDouble() * Math.PI * 2.0D);
			this.setSize(0.25F, 0.25F);
			this.yOffset = this.height / 2.0F;
		}

		protected internal override void entityInit()
		{
			this.DataWatcher.addObjectByDataType(10, 5);
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			if (this.EntityItem == null)
			{
				this.setDead();
			}
			else
			{
				base.onUpdate();

				if (this.delayBeforeCanPickup > 0)
				{
					--this.delayBeforeCanPickup;
				}

				this.prevPosX = this.posX;
				this.prevPosY = this.posY;
				this.prevPosZ = this.posZ;
				this.motionY -= 0.03999999910593033D;
				this.noClip = this.func_145771_j(this.posX, (this.boundingBox.minY + this.boundingBox.maxY) / 2.0D, this.posZ);
				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				bool var1 = (int)this.prevPosX != (int)this.posX || (int)this.prevPosY != (int)this.posY || (int)this.prevPosZ != (int)this.posZ;

				if (var1 || this.ticksExisted % 25 == 0)
				{
					if (this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)).Material == Material.lava)
					{
						this.motionY = 0.20000000298023224D;
						this.motionX = (double)((this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F);
						this.motionZ = (double)((this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F);
						this.playSound("random.fizz", 0.4F, 2.0F + this.rand.nextFloat() * 0.4F);
					}

					if (!this.worldObj.isClient)
					{
						this.searchForOtherItemsNearby();
					}
				}

				float var2 = 0.98F;

				if (this.onGround)
				{
					var2 = this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.boundingBox.minY) - 1, MathHelper.floor_double(this.posZ)).slipperiness * 0.98F;
				}

				this.motionX *= (double)var2;
				this.motionY *= 0.9800000190734863D;
				this.motionZ *= (double)var2;

				if (this.onGround)
				{
					this.motionY *= -0.5D;
				}

				++this.age;

				if (!this.worldObj.isClient && this.age >= 6000)
				{
					this.setDead();
				}
			}
		}

///    
///     <summary> * Looks for other itemstacks nearby and tries to stack them together </summary>
///     
		private void searchForOtherItemsNearby()
		{
			IEnumerator var1 = this.worldObj.getEntitiesWithinAABB(typeof(EntityItem), this.boundingBox.expand(0.5D, 0.0D, 0.5D)).GetEnumerator();

			while (var1.MoveNext())
			{
				EntityItem var2 = (EntityItem)var1.Current;
				this.combineItems(var2);
			}
		}

///    
///     <summary> * Tries to merge this item with the item passed as the parameter. Returns true if successful. Either this item or
///     * the other item will  be removed from the world. </summary>
///     
		public virtual bool combineItems(EntityItem p_70289_1_)
		{
			if (p_70289_1_ == this)
			{
				return false;
			}
			else if (p_70289_1_.EntityAlive && this.EntityAlive)
			{
				ItemStack var2 = this.EntityItem;
				ItemStack var3 = p_70289_1_.EntityItem;

				if (var3.Item != var2.Item)
				{
					return false;
				}
				else if (var3.hasTagCompound() ^ var2.hasTagCompound())
				{
					return false;
				}
				else if (var3.hasTagCompound() && !var3.TagCompound.Equals(var2.TagCompound))
				{
					return false;
				}
				else if (var3.Item == null)
				{
					return false;
				}
				else if (var3.Item.HasSubtypes && var3.ItemDamage != var2.ItemDamage)
				{
					return false;
				}
				else if (var3.stackSize < var2.stackSize)
				{
					return p_70289_1_.combineItems(this);
				}
				else if (var3.stackSize + var2.stackSize > var3.MaxStackSize)
				{
					return false;
				}
				else
				{
					var3.stackSize += var2.stackSize;
					p_70289_1_.delayBeforeCanPickup = Math.Max(p_70289_1_.delayBeforeCanPickup, this.delayBeforeCanPickup);
					p_70289_1_.age = Math.Min(p_70289_1_.age, this.age);
					p_70289_1_.EntityItemStack = var3;
					this.setDead();
					return true;
				}
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * sets the age of the item so that it'll despawn one minute after it has been dropped (instead of five). Used when
///     * items are dropped from players in creative mode </summary>
///     
		public virtual void setAgeToCreativeDespawnTime()
		{
			this.age = 4800;
		}

///    
///     <summary> * Returns if this entity is in water and will end up adding the waters velocity to the entity </summary>
///     
		public override bool handleWaterMovement()
		{
			return this.worldObj.handleMaterialAcceleration(this.boundingBox, Material.water, this);
		}

///    
///     <summary> * Will deal the specified amount of damage to the entity if the entity isn't immune to fire damage. Args:
///     * amountDamage </summary>
///     
		protected internal override void dealFireDamage(int p_70081_1_)
		{
			this.attackEntityFrom(DamageSource.inFire, (float)p_70081_1_);
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
			else if (this.EntityItem != null && this.EntityItem.Item == Items.nether_star && p_70097_1_.Explosion)
			{
				return false;
			}
			else
			{
				this.setBeenAttacked();
				this.health = (int)((float)this.health - p_70097_2_);

				if (this.health <= 0)
				{
					this.setDead();
				}

				return false;
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setShort("Health", (short)((sbyte)this.health));
			p_70014_1_.setShort("Age", (short)this.age);

			if (this.func_145800_j() != null)
			{
				p_70014_1_.setString("Thrower", this.field_145801_f);
			}

			if (this.func_145798_i() != null)
			{
				p_70014_1_.setString("Owner", this.field_145802_g);
			}

			if (this.EntityItem != null)
			{
				p_70014_1_.setTag("Item", this.EntityItem.writeToNBT(new NBTTagCompound()));
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			this.health = p_70037_1_.getShort("Health") & 255;
			this.age = p_70037_1_.getShort("Age");

			if (p_70037_1_.hasKey("Owner"))
			{
				this.field_145802_g = p_70037_1_.getString("Owner");
			}

			if (p_70037_1_.hasKey("Thrower"))
			{
				this.field_145801_f = p_70037_1_.getString("Thrower");
			}

			NBTTagCompound var2 = p_70037_1_.getCompoundTag("Item");
			this.EntityItemStack = ItemStack.loadItemStackFromNBT(var2);

			if (this.EntityItem == null)
			{
				this.setDead();
			}
		}

///    
///     <summary> * Called by a player entity when they collide with an entity </summary>
///     
		public override void onCollideWithPlayer(EntityPlayer p_70100_1_)
		{
			if (!this.worldObj.isClient)
			{
				ItemStack var2 = this.EntityItem;
				int var3 = var2.stackSize;

				if (this.delayBeforeCanPickup == 0 && (this.field_145802_g == null || 6000 - this.age <= 200 || this.field_145802_g.Equals(p_70100_1_.CommandSenderName)) && p_70100_1_.inventory.addItemStackToInventory(var2))
				{
					if (var2.Item == Item.getItemFromBlock(Blocks.log))
					{
						p_70100_1_.triggerAchievement(AchievementList.mineWood);
					}

					if (var2.Item == Item.getItemFromBlock(Blocks.log2))
					{
						p_70100_1_.triggerAchievement(AchievementList.mineWood);
					}

					if (var2.Item == Items.leather)
					{
						p_70100_1_.triggerAchievement(AchievementList.killCow);
					}

					if (var2.Item == Items.diamond)
					{
						p_70100_1_.triggerAchievement(AchievementList.diamonds);
					}

					if (var2.Item == Items.blaze_rod)
					{
						p_70100_1_.triggerAchievement(AchievementList.blazeRod);
					}

					if (var2.Item == Items.diamond && this.func_145800_j() != null)
					{
						EntityPlayer var4 = this.worldObj.getPlayerEntityByName(this.func_145800_j());

						if (var4 != null && var4 != p_70100_1_)
						{
							var4.triggerAchievement(AchievementList.field_150966_x);
						}
					}

					this.worldObj.playSoundAtEntity(p_70100_1_, "random.pop", 0.2F, ((this.rand.nextFloat() - this.rand.nextFloat()) * 0.7F + 1.0F) * 2.0F);
					p_70100_1_.onItemPickup(this, var3);

					if (var2.stackSize <= 0)
					{
						this.setDead();
					}
				}
			}
		}

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public override string CommandSenderName
		{
			get
			{
				return StatCollector.translateToLocal("item." + this.EntityItem.UnlocalizedName);
			}
		}

///    
///     <summary> * If returns false, the item will not inflict any damage against entities. </summary>
///     
		public override bool canAttackWithItem()
		{
			return false;
		}

///    
///     <summary> * Teleports the entity to another dimension. Params: Dimension number to teleport to </summary>
///     
		public override void travelToDimension(int p_71027_1_)
		{
			base.travelToDimension(p_71027_1_);

			if (!this.worldObj.isClient)
			{
				this.searchForOtherItemsNearby();
			}
		}

///    
///     <summary> * Returns the ItemStack corresponding to the Entity (Note: if no item exists, will log an error but still return an
///     * ItemStack containing Block.stone) </summary>
///     
		public virtual ItemStack EntityItem
		{
			get
			{
				ItemStack var1 = this.DataWatcher.getWatchableObjectItemStack(10);
				return var1 == null ? new ItemStack(Blocks.stone) : var1;
			}
		}

///    
///     <summary> * Sets the ItemStack for this entity </summary>
///     
		public virtual ItemStack EntityItemStack
		{
			set
			{
				this.DataWatcher.updateObject(10, value);
				this.DataWatcher.ObjectWatched = 10;
			}
		}

		public virtual string func_145798_i()
		{
			return this.field_145802_g;
		}

		public virtual void func_145797_a(string p_145797_1_)
		{
			this.field_145802_g = p_145797_1_;
		}

		public virtual string func_145800_j()
		{
			return this.field_145801_f;
		}

		public virtual void func_145799_b(string p_145799_1_)
		{
			this.field_145801_f = p_145799_1_;
		}
	}

}