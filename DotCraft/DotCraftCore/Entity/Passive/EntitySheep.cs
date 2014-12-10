using System;

namespace DotCraftCore.Entity.Passive
{

	using Block = DotCraftCore.block.Block;
	using EntityAgeable = DotCraftCore.Entity.EntityAgeable;
	using IEntityLivingData = DotCraftCore.Entity.IEntityLivingData;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIEatGrass = DotCraftCore.Entity.AI.EntityAIEatGrass;
	using EntityAIFollowParent = DotCraftCore.Entity.AI.EntityAIFollowParent;
	using EntityAILookIdle = DotCraftCore.Entity.AI.EntityAILookIdle;
	using EntityAIMate = DotCraftCore.Entity.AI.EntityAIMate;
	using EntityAIPanic = DotCraftCore.Entity.AI.EntityAIPanic;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAITempt = DotCraftCore.Entity.AI.EntityAITempt;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using EntityItem = DotCraftCore.Entity.Item.EntityItem;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Container = DotCraftCore.Inventory.Container;
	using InventoryCrafting = DotCraftCore.Inventory.InventoryCrafting;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using CraftingManager = DotCraftCore.Item.Crafting.CraftingManager;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class EntitySheep : EntityAnimal
	{
		private final InventoryCrafting;

///    
///     <summary> * Holds the RGB table of the sheep colors - in OpenGL glColor3f values - used to render the sheep colored fleece. </summary>
///     
		public static readonly float[][] fleeceColorTable = new float[][] {{1.0F, 1.0F, 1.0F}, {0.85F, 0.5F, 0.2F}, {0.7F, 0.3F, 0.85F}, {0.4F, 0.6F, 0.85F}, {0.9F, 0.9F, 0.2F}, {0.5F, 0.8F, 0.1F}, {0.95F, 0.5F, 0.65F}, {0.3F, 0.3F, 0.3F}, {0.6F, 0.6F, 0.6F}, {0.3F, 0.5F, 0.6F}, {0.5F, 0.25F, 0.7F}, {0.2F, 0.3F, 0.7F}, {0.4F, 0.3F, 0.2F}, {0.4F, 0.5F, 0.2F}, {0.6F, 0.2F, 0.2F}, {0.1F, 0.1F, 0.1F}};

///    
///     <summary> * Used to control movement as well as wool regrowth. Set to 40 on handleHealthUpdate and counts down with each
///     * tick. </summary>
///     
		private int sheepTimer;
		private EntityAIEatGrass field_146087_bs = new EntityAIEatGrass(this);
		private const string __OBFID = "CL_00001648";

		public EntitySheep(World p_i1691_1_) : base(p_i1691_1_)
		{
			this.setSize(0.9F, 1.3F);
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIPanic(this, 1.25D));
			this.tasks.addTask(2, new EntityAIMate(this, 1.0D));
			this.tasks.addTask(3, new EntityAITempt(this, 1.1D, Items.wheat, false));
			this.tasks.addTask(4, new EntityAIFollowParent(this, 1.1D));
			this.tasks.addTask(5, this.field_146087_bs);
			this.tasks.addTask(6, new EntityAIWander(this, 1.0D));
			this.tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(8, new EntityAILookIdle(this));
			this.field_90016_e.setInventorySlotContents(0, new ItemStack(Items.dye, 1, 0));
			this.field_90016_e.setInventorySlotContents(1, new ItemStack(Items.dye, 1, 0));
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		protected internal override bool isAIEnabled()
		{
			get
			{
				return true;
			}
		}

		protected internal override void updateAITasks()
		{
			this.sheepTimer = this.field_146087_bs.func_151499_f();
			base.updateAITasks();
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			if (this.worldObj.isClient)
			{
				this.sheepTimer = Math.Max(0, this.sheepTimer - 1);
			}

			base.onLivingUpdate();
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 8.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.23000000417232513D;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, new sbyte?((sbyte)0));
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			if (!this.Sheared)
			{
				this.entityDropItem(new ItemStack(Item.getItemFromBlock(Blocks.wool), 1, this.FleeceColor), 0.0F);
			}
		}

		protected internal override Item func_146068_u()
		{
			return Item.getItemFromBlock(Blocks.wool);
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 10)
			{
				this.sheepTimer = 40;
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

		public virtual float func_70894_j(float p_70894_1_)
		{
			return this.sheepTimer <= 0 ? 0.0F : (this.sheepTimer >= 4 && this.sheepTimer <= 36 ? 1.0F : (this.sheepTimer < 4 ? ((float)this.sheepTimer - p_70894_1_) / 4.0F : -((float)(this.sheepTimer - 40) - p_70894_1_) / 4.0F));
		}

		public virtual float func_70890_k(float p_70890_1_)
		{
			if (this.sheepTimer > 4 && this.sheepTimer <= 36)
			{
				float var2 = ((float)(this.sheepTimer - 4) - p_70890_1_) / 32.0F;
				return ((float)Math.PI / 5F) + ((float)Math.PI * 7F / 100F) * MathHelper.sin(var2 * 28.7F);
			}
			else
			{
				return this.sheepTimer > 0 ? ((float)Math.PI / 5F) : this.rotationPitch / (180F / (float)Math.PI);
			}
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (var2 != null && var2.Item == Items.shears && !this.Sheared && !this.Child)
			{
				if (!this.worldObj.isClient)
				{
					this.Sheared = true;
					int var3 = 1 + this.rand.Next(3);

					for (int var4 = 0; var4 < var3; ++var4)
					{
						EntityItem var5 = this.entityDropItem(new ItemStack(Item.getItemFromBlock(Blocks.wool), 1, this.FleeceColor), 1.0F);
						var5.motionY += (double)(this.rand.nextFloat() * 0.05F);
						var5.motionX += (double)((this.rand.nextFloat() - this.rand.nextFloat()) * 0.1F);
						var5.motionZ += (double)((this.rand.nextFloat() - this.rand.nextFloat()) * 0.1F);
					}
				}

				var2.damageItem(1, p_70085_1_);
				this.playSound("mob.sheep.shear", 1.0F, 1.0F);
			}

			return base.interact(p_70085_1_);
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setBoolean("Sheared", this.Sheared);
			p_70014_1_.setByte("Color", (sbyte)this.FleeceColor);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.Sheared = p_70037_1_.getBoolean("Sheared");
			this.FleeceColor = p_70037_1_.getByte("Color");
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.sheep.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.sheep.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.sheep.say";
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.sheep.step", 0.15F, 1.0F);
		}

		public virtual int FleeceColor
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(16) & 15;
			}
			set
			{
				sbyte var2 = this.dataWatcher.getWatchableObjectByte(16);
				this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 & 240 | value & 15)));
			}
		}


///    
///     <summary> * returns true if a sheeps wool has been sheared </summary>
///     
		public virtual bool Sheared
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 16) != 0;
			}
			set
			{
				sbyte var2 = this.dataWatcher.getWatchableObjectByte(16);
	
				if (value)
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 | 16)));
				}
				else
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 & -17)));
				}
			}
		}

///    
///     <summary> * make a sheep sheared if set to true </summary>
///     

///    
///     <summary> * This method is called when a sheep spawns in the world to select the color of sheep fleece. </summary>
///     
		public static int getRandomFleeceColor(Random p_70895_0_)
		{
			int var1 = p_70895_0_.Next(100);
			return var1 < 5 ? 15 : (var1 < 10 ? 7 : (var1 < 15 ? 8 : (var1 < 18 ? 12 : (p_70895_0_.Next(500) == 0 ? 6 : 0))));
		}

		public override EntitySheep createChild(EntityAgeable p_90011_1_)
		{
			EntitySheep var2 = (EntitySheep)p_90011_1_;
			EntitySheep var3 = new EntitySheep(this.worldObj);
			int var4 = this.func_90014_a(this, var2);
			var3.FleeceColor = 15 - var4;
			return var3;
		}

///    
///     <summary> * This function applies the benefits of growing back wool and faster growing up to the acting entity. (This
///     * function is used in the AIEatGrass) </summary>
///     
		public override void eatGrassBonus()
		{
			this.Sheared = false;

			if (this.Child)
			{
				this.addGrowth(60);
			}
		}

		public override IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			p_110161_1_ = base.onSpawnWithEgg(p_110161_1_);
			this.FleeceColor = getRandomFleeceColor(this.worldObj.rand);
			return p_110161_1_;
		}

		private int func_90014_a(EntityAnimal p_90014_1_, EntityAnimal p_90014_2_)
		{
			int var3 = this.func_90013_b(p_90014_1_);
			int var4 = this.func_90013_b(p_90014_2_);
			this.field_90016_e.getStackInSlot(0).ItemDamage = var3;
			this.field_90016_e.getStackInSlot(1).ItemDamage = var4;
			ItemStack var5 = CraftingManager.Instance.findMatchingRecipe(this.field_90016_e, ((EntitySheep)p_90014_1_).worldObj);
			int var6;

			if (var5 != null && var5.Item == Items.dye)
			{
				var6 = var5.ItemDamage;
			}
			else
			{
				var6 = this.worldObj.rand.nextBoolean() ? var3 : var4;
			}

			return var6;
		}

		private int func_90013_b(EntityAnimal p_90013_1_)
		{
			return 15 - ((EntitySheep)p_90013_1_).FleeceColor;
		}
	}

}