namespace DotCraftCore.Entity.Passive
{

	using Block = DotCraftCore.block.Block;
	using EntityAgeable = DotCraftCore.Entity.EntityAgeable;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIFollowParent = DotCraftCore.Entity.AI.EntityAIFollowParent;
	using EntityAILookIdle = DotCraftCore.Entity.AI.EntityAILookIdle;
	using EntityAIMate = DotCraftCore.Entity.AI.EntityAIMate;
	using EntityAIPanic = DotCraftCore.Entity.AI.EntityAIPanic;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAITempt = DotCraftCore.Entity.AI.EntityAITempt;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using World = DotCraftCore.World.World;

	public class EntityCow : EntityAnimal
	{
		private const string __OBFID = "CL_00001640";

		public EntityCow(World p_i1683_1_) : base(p_i1683_1_)
		{
			this.setSize(0.9F, 1.3F);
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIPanic(this, 2.0D));
			this.tasks.addTask(2, new EntityAIMate(this, 1.0D));
			this.tasks.addTask(3, new EntityAITempt(this, 1.25D, Items.wheat, false));
			this.tasks.addTask(4, new EntityAIFollowParent(this, 1.25D));
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
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 10.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.20000000298023224D;
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.cow.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.cow.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.cow.hurt";
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.cow.step", 0.15F, 1.0F);
		}

///    
///     <summary> * Returns the volume for the sounds this mob makes. </summary>
///     
		protected internal override float SoundVolume
		{
			get
			{
				return 0.4F;
			}
		}

		protected internal override Item func_146068_u()
		{
			return Items.leather;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(3) + this.rand.Next(1 + p_70628_2_);
			int var4;

			for (var4 = 0; var4 < var3; ++var4)
			{
				this.func_145779_a(Items.leather, 1);
			}

			var3 = this.rand.Next(3) + 1 + this.rand.Next(1 + p_70628_2_);

			for (var4 = 0; var4 < var3; ++var4)
			{
				if (this.Burning)
				{
					this.func_145779_a(Items.cooked_beef, 1);
				}
				else
				{
					this.func_145779_a(Items.beef, 1);
				}
			}
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (var2 != null && var2.Item == Items.bucket && !p_70085_1_.capabilities.isCreativeMode)
			{
				if (var2.stackSize-- == 1)
				{
					p_70085_1_.inventory.setInventorySlotContents(p_70085_1_.inventory.currentItem, new ItemStack(Items.milk_bucket));
				}
				else if (!p_70085_1_.inventory.addItemStackToInventory(new ItemStack(Items.milk_bucket)))
				{
					p_70085_1_.dropPlayerItemWithRandomChoice(new ItemStack(Items.milk_bucket, 1, 0), false);
				}

				return true;
			}
			else
			{
				return base.interact(p_70085_1_);
			}
		}

		public override EntityCow createChild(EntityAgeable p_90011_1_)
		{
			return new EntityCow(this.worldObj);
		}
	}

}