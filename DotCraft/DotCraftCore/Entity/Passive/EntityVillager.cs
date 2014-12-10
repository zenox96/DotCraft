using System;
using System.Collections;

namespace DotCraftCore.Entity.Passive
{

	using Enchantment = DotCraftCore.Enchantment.Enchantment;
	using EnchantmentData = DotCraftCore.Enchantment.EnchantmentData;
	using EnchantmentHelper = DotCraftCore.Enchantment.EnchantmentHelper;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityAgeable = DotCraftCore.Entity.EntityAgeable;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IEntityLivingData = DotCraftCore.Entity.IEntityLivingData;
	using IMerchant = DotCraftCore.Entity.IMerchant;
	using INpc = DotCraftCore.Entity.INpc;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIAvoidEntity = DotCraftCore.Entity.AI.EntityAIAvoidEntity;
	using EntityAIFollowGolem = DotCraftCore.Entity.AI.EntityAIFollowGolem;
	using EntityAILookAtTradePlayer = DotCraftCore.Entity.AI.EntityAILookAtTradePlayer;
	using EntityAIMoveIndoors = DotCraftCore.Entity.AI.EntityAIMoveIndoors;
	using EntityAIMoveTowardsRestriction = DotCraftCore.Entity.AI.EntityAIMoveTowardsRestriction;
	using EntityAIOpenDoor = DotCraftCore.Entity.AI.EntityAIOpenDoor;
	using EntityAIPlay = DotCraftCore.Entity.AI.EntityAIPlay;
	using EntityAIRestrictOpenDoor = DotCraftCore.Entity.AI.EntityAIRestrictOpenDoor;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAITradePlayer = DotCraftCore.Entity.AI.EntityAITradePlayer;
	using EntityAIVillagerMate = DotCraftCore.Entity.AI.EntityAIVillagerMate;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using EntityAIWatchClosest2 = DotCraftCore.Entity.AI.EntityAIWatchClosest2;
	using EntityZombie = DotCraftCore.Entity.Monster.EntityZombie;
	using IMob = DotCraftCore.Entity.Monster.IMob;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using Potion = DotCraftCore.Potion.Potion;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Tuple = DotCraftCore.Util.Tuple;
	using MerchantRecipe = DotCraftCore.Village.MerchantRecipe;
	using MerchantRecipeList = DotCraftCore.Village.MerchantRecipeList;
	using Village = DotCraftCore.Village.Village;
	using World = DotCraftCore.World.World;

	public class EntityVillager : EntityAgeable, IMerchant, INpc
	{
		private int randomTickDivider;
		private bool isMating;
		private bool isPlaying;
		internal Village villageObj;

	/// <summary> This villager's current customer.  </summary>
		private EntityPlayer buyingPlayer;

	/// <summary> Initialises the MerchantRecipeList.java  </summary>
		private MerchantRecipeList buyingList;
		private int timeUntilReset;

	/// <summary> addDefaultEquipmentAndRecipies is called if this is true  </summary>
		private bool needsInitilization;
		private int wealth;

	/// <summary> Last player to trade with this villager, used for aggressivity.  </summary>
		private string lastBuyingPlayer;
		private bool isLookingForHome;
		private float field_82191_bN;

	/// <summary> Selling list of Villagers items.  </summary>
		private static readonly IDictionary villagersSellingList = new Hashtable();

	/// <summary> Selling list of Blacksmith items.  </summary>
		private static readonly IDictionary blacksmithSellingList = new Hashtable();
		private const string __OBFID = "CL_00001707";

		public EntityVillager(World p_i1747_1_) : this(p_i1747_1_, 0)
		{
		}

		public EntityVillager(World p_i1748_1_, int p_i1748_2_) : base(p_i1748_1_)
		{
			this.Profession = p_i1748_2_;
			this.setSize(0.6F, 1.8F);
			this.Navigator.BreakDoors = true;
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIAvoidEntity(this, typeof(EntityZombie), 8.0F, 0.6D, 0.6D));
			this.tasks.addTask(1, new EntityAITradePlayer(this));
			this.tasks.addTask(1, new EntityAILookAtTradePlayer(this));
			this.tasks.addTask(2, new EntityAIMoveIndoors(this));
			this.tasks.addTask(3, new EntityAIRestrictOpenDoor(this));
			this.tasks.addTask(4, new EntityAIOpenDoor(this, true));
			this.tasks.addTask(5, new EntityAIMoveTowardsRestriction(this, 0.6D));
			this.tasks.addTask(6, new EntityAIVillagerMate(this));
			this.tasks.addTask(7, new EntityAIFollowGolem(this));
			this.tasks.addTask(8, new EntityAIPlay(this, 0.32D));
			this.tasks.addTask(9, new EntityAIWatchClosest2(this, typeof(EntityPlayer), 3.0F, 1.0F));
			this.tasks.addTask(9, new EntityAIWatchClosest2(this, typeof(EntityVillager), 5.0F, 0.02F));
			this.tasks.addTask(9, new EntityAIWander(this, 0.6D));
			this.tasks.addTask(10, new EntityAIWatchClosest(this, typeof(EntityLiving), 8.0F));
		}

		protected internal virtual void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.5D;
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		public virtual bool isAIEnabled()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * main AI tick function, replaces updateEntityActionState </summary>
///     
		protected internal virtual void updateAITick()
		{
			if (--this.randomTickDivider <= 0)
			{
				this.worldObj.villageCollectionObj.addVillagerPosition(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ));
				this.randomTickDivider = 70 + this.rand.Next(50);
				this.villageObj = this.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ), 32);

				if (this.villageObj == null)
				{
					this.detachHome();
				}
				else
				{
					ChunkCoordinates var1 = this.villageObj.Center;
					this.setHomeArea(var1.posX, var1.posY, var1.posZ, (int)((float)this.villageObj.VillageRadius * 0.6F));

					if (this.isLookingForHome)
					{
						this.isLookingForHome = false;
						this.villageObj.DefaultPlayerReputation = 5;
					}
				}
			}

			if (!this.Trading && this.timeUntilReset > 0)
			{
				--this.timeUntilReset;

				if (this.timeUntilReset <= 0)
				{
					if (this.needsInitilization)
					{
						if (this.buyingList.size() > 1)
						{
							IEnumerator var3 = this.buyingList.GetEnumerator();

							while (var3.MoveNext())
							{
								MerchantRecipe var2 = (MerchantRecipe)var3.Current;

								if (var2.RecipeDisabled)
								{
									var2.func_82783_a(this.rand.Next(6) + this.rand.Next(6) + 2);
								}
							}
						}

						this.addDefaultEquipmentAndRecipies(1);
						this.needsInitilization = false;

						if (this.villageObj != null && this.lastBuyingPlayer != null)
						{
							this.worldObj.setEntityState(this, (sbyte)14);
							this.villageObj.setReputationForPlayer(this.lastBuyingPlayer, 1);
						}
					}

					this.addPotionEffect(new PotionEffect(Potion.regeneration.id, 200, 0));
				}
			}

			base.updateAITick();
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;
			bool var3 = var2 != null && var2.Item == Items.spawn_egg;

			if (!var3 && this.EntityAlive && !this.Trading && !this.Child)
			{
				if (!this.worldObj.isClient)
				{
					this.Customer = p_70085_1_;
					p_70085_1_.displayGUIMerchant(this, this.CustomNameTag);
				}

				return true;
			}
			else
			{
				return base.interact(p_70085_1_);
			}
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, Convert.ToInt32(0));
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("Profession", this.Profession);
			p_70014_1_.setInteger("Riches", this.wealth);

			if (this.buyingList != null)
			{
				p_70014_1_.setTag("Offers", this.buyingList.RecipiesAsTags);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.Profession = p_70037_1_.getInteger("Profession");
			this.wealth = p_70037_1_.getInteger("Riches");

			if (p_70037_1_.func_150297_b("Offers", 10))
			{
				NBTTagCompound var2 = p_70037_1_.getCompoundTag("Offers");
				this.buyingList = new MerchantRecipeList(var2);
			}
		}

///    
///     <summary> * Determines if an entity can be despawned, used on idle far away entities </summary>
///     
		protected internal virtual bool canDespawn()
		{
			return false;
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal virtual string LivingSound
		{
			get
			{
				return this.Trading ? "mob.villager.haggle" : "mob.villager.idle";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal virtual string HurtSound
		{
			get
			{
				return "mob.villager.hit";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal virtual string DeathSound
		{
			get
			{
				return "mob.villager.death";
			}
		}

		public virtual int Profession
		{
			set
			{
				this.dataWatcher.updateObject(16, Convert.ToInt32(value));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(16);
			}
		}


		public virtual bool isMating()
		{
			get
			{
				return this.isMating;
			}
			set
			{
				this.isMating = value;
			}
		}


		public virtual bool Playing
		{
			set
			{
				this.isPlaying = value;
			}
			get
			{
				return this.isPlaying;
			}
		}


		public virtual EntityLivingBase RevengeTarget
		{
			set
			{
				base.RevengeTarget = value;
	
				if (this.villageObj != null && value != null)
				{
					this.villageObj.addOrRenewAgressor(value);
	
					if (value is EntityPlayer)
					{
						sbyte var2 = -1;
	
						if (this.Child)
						{
							var2 = -3;
						}
	
						this.villageObj.setReputationForPlayer(value.CommandSenderName, var2);
	
						if (this.EntityAlive)
						{
							this.worldObj.setEntityState(this, (sbyte)13);
						}
					}
				}
			}
		}

///    
///     <summary> * Called when the mob's health reaches 0. </summary>
///     
		public virtual void onDeath(DamageSource p_70645_1_)
		{
			if (this.villageObj != null)
			{
				Entity var2 = p_70645_1_.Entity;

				if (var2 != null)
				{
					if (var2 is EntityPlayer)
					{
						this.villageObj.setReputationForPlayer(var2.CommandSenderName, -2);
					}
					else if (var2 is IMob)
					{
						this.villageObj.endMatingSeason();
					}
				}
				else if (var2 == null)
				{
					EntityPlayer var3 = this.worldObj.getClosestPlayerToEntity(this, 16.0D);

					if (var3 != null)
					{
						this.villageObj.endMatingSeason();
					}
				}
			}

			base.onDeath(p_70645_1_);
		}

		public virtual EntityPlayer Customer
		{
			set
			{
				this.buyingPlayer = value;
			}
			get
			{
				return this.buyingPlayer;
			}
		}


		public virtual bool isTrading()
		{
			get
			{
				return this.buyingPlayer != null;
			}
		}

		public virtual void useRecipe(MerchantRecipe p_70933_1_)
		{
			p_70933_1_.incrementToolUses();
			this.livingSoundTime = -this.TalkInterval;
			this.playSound("mob.villager.yes", this.SoundVolume, this.SoundPitch);

			if (p_70933_1_.hasSameIDsAs((MerchantRecipe)this.buyingList.get(this.buyingList.size() - 1)))
			{
				this.timeUntilReset = 40;
				this.needsInitilization = true;

				if (this.buyingPlayer != null)
				{
					this.lastBuyingPlayer = this.buyingPlayer.CommandSenderName;
				}
				else
				{
					this.lastBuyingPlayer = null;
				}
			}

			if (p_70933_1_.ItemToBuy.Item == Items.emerald)
			{
				this.wealth += p_70933_1_.ItemToBuy.stackSize;
			}
		}

		public virtual void func_110297_a_(ItemStack p_110297_1_)
		{
			if (!this.worldObj.isClient && this.livingSoundTime > -this.TalkInterval + 20)
			{
				this.livingSoundTime = -this.TalkInterval;

				if (p_110297_1_ != null)
				{
					this.playSound("mob.villager.yes", this.SoundVolume, this.SoundPitch);
				}
				else
				{
					this.playSound("mob.villager.no", this.SoundVolume, this.SoundPitch);
				}
			}
		}

		public virtual MerchantRecipeList getRecipes(EntityPlayer p_70934_1_)
		{
			if (this.buyingList == null)
			{
				this.addDefaultEquipmentAndRecipies(1);
			}

			return this.buyingList;
		}

///    
///     <summary> * Adjusts the probability of obtaining a given recipe being offered by a villager </summary>
///     
		private float adjustProbability(float p_82188_1_)
		{
			float var2 = p_82188_1_ + this.field_82191_bN;
			return var2 > 0.9F ? 0.9F - (var2 - 0.9F) : var2;
		}

///    
///     <summary> * based on the villagers profession add items, equipment, and recipies adds par1 random items to the list of things
///     * that the villager wants to buy. (at most 1 of each wanted type is added) </summary>
///     
		private void addDefaultEquipmentAndRecipies(int p_70950_1_)
		{
			if (this.buyingList != null)
			{
				this.field_82191_bN = MathHelper.sqrt_float((float)this.buyingList.size()) * 0.2F;
			}
			else
			{
				this.field_82191_bN = 0.0F;
			}

			MerchantRecipeList var2;
			var2 = new MerchantRecipeList();
			int var6;
			label50:

			switch (this.Profession)
			{
				case 0:
					func_146091_a(var2, Items.wheat, this.rand, this.adjustProbability(0.9F));
					func_146091_a(var2, Item.getItemFromBlock(Blocks.wool), this.rand, this.adjustProbability(0.5F));
					func_146091_a(var2, Items.chicken, this.rand, this.adjustProbability(0.5F));
					func_146091_a(var2, Items.cooked_fished, this.rand, this.adjustProbability(0.4F));
					func_146089_b(var2, Items.bread, this.rand, this.adjustProbability(0.9F));
					func_146089_b(var2, Items.melon, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.apple, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.cookie, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.shears, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.flint_and_steel, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.cooked_chicken, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.arrow, this.rand, this.adjustProbability(0.5F));

					if (this.rand.nextFloat() < this.adjustProbability(0.5F))
					{
						var2.add(new MerchantRecipe(new ItemStack(Blocks.gravel, 10), new ItemStack(Items.emerald), new ItemStack(Items.flint, 4 + this.rand.Next(2), 0)));
					}

					break;

				case 1:
					func_146091_a(var2, Items.paper, this.rand, this.adjustProbability(0.8F));
					func_146091_a(var2, Items.book, this.rand, this.adjustProbability(0.8F));
					func_146091_a(var2, Items.written_book, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Item.getItemFromBlock(Blocks.bookshelf), this.rand, this.adjustProbability(0.8F));
					func_146089_b(var2, Item.getItemFromBlock(Blocks.glass), this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.compass, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.clock, this.rand, this.adjustProbability(0.2F));

					if (this.rand.nextFloat() < this.adjustProbability(0.07F))
					{
						Enchantment var8 = Enchantment.enchantmentsBookList[this.rand.Next(Enchantment.enchantmentsBookList.length)];
						int var10 = MathHelper.getRandomIntegerInRange(this.rand, var8.MinLevel, var8.MaxLevel);
						ItemStack var11 = Items.enchanted_book.getEnchantedItemStack(new EnchantmentData(var8, var10));
						var6 = 2 + this.rand.Next(5 + var10 * 10) + 3 * var10;
						var2.add(new MerchantRecipe(new ItemStack(Items.book), new ItemStack(Items.emerald, var6), var11));
					}

					break;

				case 2:
					func_146089_b(var2, Items.ender_eye, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.experience_bottle, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.redstone, this.rand, this.adjustProbability(0.4F));
					func_146089_b(var2, Item.getItemFromBlock(Blocks.glowstone), this.rand, this.adjustProbability(0.3F));
					Item[] var3 = new Item[] {Items.iron_sword, Items.diamond_sword, Items.iron_chestplate, Items.diamond_chestplate, Items.iron_axe, Items.diamond_axe, Items.iron_pickaxe, Items.diamond_pickaxe};
					Item[] var4 = var3;
					int var5 = var3.Length;
					var6 = 0;

					while (true)
					{
						if (var6 >= var5)
						{
							goto label50;
						}

						Item var7 = var4[var6];

						if (this.rand.nextFloat() < this.adjustProbability(0.05F))
						{
							var2.add(new MerchantRecipe(new ItemStack(var7, 1, 0), new ItemStack(Items.emerald, 2 + this.rand.Next(3), 0), EnchantmentHelper.addRandomEnchantment(this.rand, new ItemStack(var7, 1, 0), 5 + this.rand.Next(15))));
						}

						++var6;
					}

					goto case 3;
				case 3:
					func_146091_a(var2, Items.coal, this.rand, this.adjustProbability(0.7F));
					func_146091_a(var2, Items.iron_ingot, this.rand, this.adjustProbability(0.5F));
					func_146091_a(var2, Items.gold_ingot, this.rand, this.adjustProbability(0.5F));
					func_146091_a(var2, Items.diamond, this.rand, this.adjustProbability(0.5F));
					func_146089_b(var2, Items.iron_sword, this.rand, this.adjustProbability(0.5F));
					func_146089_b(var2, Items.diamond_sword, this.rand, this.adjustProbability(0.5F));
					func_146089_b(var2, Items.iron_axe, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.diamond_axe, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.iron_pickaxe, this.rand, this.adjustProbability(0.5F));
					func_146089_b(var2, Items.diamond_pickaxe, this.rand, this.adjustProbability(0.5F));
					func_146089_b(var2, Items.iron_shovel, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.diamond_shovel, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.iron_hoe, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.diamond_hoe, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.iron_boots, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.diamond_boots, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.iron_helmet, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.diamond_helmet, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.iron_chestplate, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.diamond_chestplate, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.iron_leggings, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.diamond_leggings, this.rand, this.adjustProbability(0.2F));
					func_146089_b(var2, Items.chainmail_boots, this.rand, this.adjustProbability(0.1F));
					func_146089_b(var2, Items.chainmail_helmet, this.rand, this.adjustProbability(0.1F));
					func_146089_b(var2, Items.chainmail_chestplate, this.rand, this.adjustProbability(0.1F));
					func_146089_b(var2, Items.chainmail_leggings, this.rand, this.adjustProbability(0.1F));
					break;

				case 4:
					func_146091_a(var2, Items.coal, this.rand, this.adjustProbability(0.7F));
					func_146091_a(var2, Items.porkchop, this.rand, this.adjustProbability(0.5F));
					func_146091_a(var2, Items.beef, this.rand, this.adjustProbability(0.5F));
					func_146089_b(var2, Items.saddle, this.rand, this.adjustProbability(0.1F));
					func_146089_b(var2, Items.leather_chestplate, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.leather_boots, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.leather_helmet, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.leather_leggings, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.cooked_porkchop, this.rand, this.adjustProbability(0.3F));
					func_146089_b(var2, Items.cooked_beef, this.rand, this.adjustProbability(0.3F));
				break;
			}

			if (var2.Empty)
			{
				func_146091_a(var2, Items.gold_ingot, this.rand, 1.0F);
			}

			Collections.shuffle(var2);

			if (this.buyingList == null)
			{
				this.buyingList = new MerchantRecipeList();
			}

			for (int var9 = 0; var9 < p_70950_1_ && var9 < var2.size(); ++var9)
			{
				this.buyingList.addToListWithCheck((MerchantRecipe)var2.get(var9));
			}
		}

		public virtual MerchantRecipeList Recipes
		{
			set
			{
			}
		}

		private static void func_146091_a(MerchantRecipeList p_146091_0_, Item p_146091_1_, Random p_146091_2_, float p_146091_3_)
		{
			if (p_146091_2_.nextFloat() < p_146091_3_)
			{
				p_146091_0_.add(new MerchantRecipe(func_146088_a(p_146091_1_, p_146091_2_), Items.emerald));
			}
		}

		private static ItemStack func_146088_a(Item p_146088_0_, Random p_146088_1_)
		{
			return new ItemStack(p_146088_0_, func_146092_b(p_146088_0_, p_146088_1_), 0);
		}

		private static int func_146092_b(Item p_146092_0_, Random p_146092_1_)
		{
			Tuple var2 = (Tuple)villagersSellingList[p_146092_0_];
			return var2 == null ? 1 : ((int)((int?)var2.First) >= (int)((int?)var2.Second) ? (int)((int?)var2.First) : (int)((int?)var2.First) + p_146092_1_.Next((int)((int?)var2.Second) - (int)((int?)var2.First)));
		}

		private static void func_146089_b(MerchantRecipeList p_146089_0_, Item p_146089_1_, Random p_146089_2_, float p_146089_3_)
		{
			if (p_146089_2_.nextFloat() < p_146089_3_)
			{
				int var4 = func_146090_c(p_146089_1_, p_146089_2_);
				ItemStack var5;
				ItemStack var6;

				if (var4 < 0)
				{
					var5 = new ItemStack(Items.emerald, 1, 0);
					var6 = new ItemStack(p_146089_1_, -var4, 0);
				}
				else
				{
					var5 = new ItemStack(Items.emerald, var4, 0);
					var6 = new ItemStack(p_146089_1_, 1, 0);
				}

				p_146089_0_.add(new MerchantRecipe(var5, var6));
			}
		}

		private static int func_146090_c(Item p_146090_0_, Random p_146090_1_)
		{
			Tuple var2 = (Tuple)blacksmithSellingList[p_146090_0_];
			return var2 == null ? 1 : ((int)((int?)var2.First) >= (int)((int?)var2.Second) ? (int)((int?)var2.First) : (int)((int?)var2.First) + p_146090_1_.Next((int)((int?)var2.Second) - (int)((int?)var2.First)));
		}

		public virtual void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 12)
			{
				this.generateRandomParticles("heart");
			}
			else if (p_70103_1_ == 13)
			{
				this.generateRandomParticles("angryVillager");
			}
			else if (p_70103_1_ == 14)
			{
				this.generateRandomParticles("happyVillager");
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

///    
///     <summary> * par1 is the particleName </summary>
///     
		private void generateRandomParticles(string p_70942_1_)
		{
			for (int var2 = 0; var2 < 5; ++var2)
			{
				double var3 = this.rand.nextGaussian() * 0.02D;
				double var5 = this.rand.nextGaussian() * 0.02D;
				double var7 = this.rand.nextGaussian() * 0.02D;
				this.worldObj.spawnParticle(p_70942_1_, this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + 1.0D + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, var3, var5, var7);
			}
		}

		public virtual IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			p_110161_1_ = base.onSpawnWithEgg(p_110161_1_);
			this.Profession = this.worldObj.rand.Next(5);
			return p_110161_1_;
		}

		public virtual void setLookingForHome()
		{
			this.isLookingForHome = true;
		}

		public override EntityVillager createChild(EntityAgeable p_90011_1_)
		{
			EntityVillager var2 = new EntityVillager(this.worldObj);
			var2.onSpawnWithEgg((IEntityLivingData)null);
			return var2;
		}

		public virtual bool allowLeashing()
		{
			return false;
		}

		static EntityVillager()
		{
			villagersSellingList.Add(Items.coal, new Tuple(Convert.ToInt32(16), Convert.ToInt32(24)));
			villagersSellingList.Add(Items.iron_ingot, new Tuple(Convert.ToInt32(8), Convert.ToInt32(10)));
			villagersSellingList.Add(Items.gold_ingot, new Tuple(Convert.ToInt32(8), Convert.ToInt32(10)));
			villagersSellingList.Add(Items.diamond, new Tuple(Convert.ToInt32(4), Convert.ToInt32(6)));
			villagersSellingList.Add(Items.paper, new Tuple(Convert.ToInt32(24), Convert.ToInt32(36)));
			villagersSellingList.Add(Items.book, new Tuple(Convert.ToInt32(11), Convert.ToInt32(13)));
			villagersSellingList.Add(Items.written_book, new Tuple(Convert.ToInt32(1), Convert.ToInt32(1)));
			villagersSellingList.Add(Items.ender_pearl, new Tuple(Convert.ToInt32(3), Convert.ToInt32(4)));
			villagersSellingList.Add(Items.ender_eye, new Tuple(Convert.ToInt32(2), Convert.ToInt32(3)));
			villagersSellingList.Add(Items.porkchop, new Tuple(Convert.ToInt32(14), Convert.ToInt32(18)));
			villagersSellingList.Add(Items.beef, new Tuple(Convert.ToInt32(14), Convert.ToInt32(18)));
			villagersSellingList.Add(Items.chicken, new Tuple(Convert.ToInt32(14), Convert.ToInt32(18)));
			villagersSellingList.Add(Items.cooked_fished, new Tuple(Convert.ToInt32(9), Convert.ToInt32(13)));
			villagersSellingList.Add(Items.wheat_seeds, new Tuple(Convert.ToInt32(34), Convert.ToInt32(48)));
			villagersSellingList.Add(Items.melon_seeds, new Tuple(Convert.ToInt32(30), Convert.ToInt32(38)));
			villagersSellingList.Add(Items.pumpkin_seeds, new Tuple(Convert.ToInt32(30), Convert.ToInt32(38)));
			villagersSellingList.Add(Items.wheat, new Tuple(Convert.ToInt32(18), Convert.ToInt32(22)));
			villagersSellingList.Add(Item.getItemFromBlock(Blocks.wool), new Tuple(Convert.ToInt32(14), Convert.ToInt32(22)));
			villagersSellingList.Add(Items.rotten_flesh, new Tuple(Convert.ToInt32(36), Convert.ToInt32(64)));
			blacksmithSellingList.Add(Items.flint_and_steel, new Tuple(Convert.ToInt32(3), Convert.ToInt32(4)));
			blacksmithSellingList.Add(Items.shears, new Tuple(Convert.ToInt32(3), Convert.ToInt32(4)));
			blacksmithSellingList.Add(Items.iron_sword, new Tuple(Convert.ToInt32(7), Convert.ToInt32(11)));
			blacksmithSellingList.Add(Items.diamond_sword, new Tuple(Convert.ToInt32(12), Convert.ToInt32(14)));
			blacksmithSellingList.Add(Items.iron_axe, new Tuple(Convert.ToInt32(6), Convert.ToInt32(8)));
			blacksmithSellingList.Add(Items.diamond_axe, new Tuple(Convert.ToInt32(9), Convert.ToInt32(12)));
			blacksmithSellingList.Add(Items.iron_pickaxe, new Tuple(Convert.ToInt32(7), Convert.ToInt32(9)));
			blacksmithSellingList.Add(Items.diamond_pickaxe, new Tuple(Convert.ToInt32(10), Convert.ToInt32(12)));
			blacksmithSellingList.Add(Items.iron_shovel, new Tuple(Convert.ToInt32(4), Convert.ToInt32(6)));
			blacksmithSellingList.Add(Items.diamond_shovel, new Tuple(Convert.ToInt32(7), Convert.ToInt32(8)));
			blacksmithSellingList.Add(Items.iron_hoe, new Tuple(Convert.ToInt32(4), Convert.ToInt32(6)));
			blacksmithSellingList.Add(Items.diamond_hoe, new Tuple(Convert.ToInt32(7), Convert.ToInt32(8)));
			blacksmithSellingList.Add(Items.iron_boots, new Tuple(Convert.ToInt32(4), Convert.ToInt32(6)));
			blacksmithSellingList.Add(Items.diamond_boots, new Tuple(Convert.ToInt32(7), Convert.ToInt32(8)));
			blacksmithSellingList.Add(Items.iron_helmet, new Tuple(Convert.ToInt32(4), Convert.ToInt32(6)));
			blacksmithSellingList.Add(Items.diamond_helmet, new Tuple(Convert.ToInt32(7), Convert.ToInt32(8)));
			blacksmithSellingList.Add(Items.iron_chestplate, new Tuple(Convert.ToInt32(10), Convert.ToInt32(14)));
			blacksmithSellingList.Add(Items.diamond_chestplate, new Tuple(Convert.ToInt32(16), Convert.ToInt32(19)));
			blacksmithSellingList.Add(Items.iron_leggings, new Tuple(Convert.ToInt32(8), Convert.ToInt32(10)));
			blacksmithSellingList.Add(Items.diamond_leggings, new Tuple(Convert.ToInt32(11), Convert.ToInt32(14)));
			blacksmithSellingList.Add(Items.chainmail_boots, new Tuple(Convert.ToInt32(5), Convert.ToInt32(7)));
			blacksmithSellingList.Add(Items.chainmail_helmet, new Tuple(Convert.ToInt32(5), Convert.ToInt32(7)));
			blacksmithSellingList.Add(Items.chainmail_chestplate, new Tuple(Convert.ToInt32(11), Convert.ToInt32(15)));
			blacksmithSellingList.Add(Items.chainmail_leggings, new Tuple(Convert.ToInt32(9), Convert.ToInt32(11)));
			blacksmithSellingList.Add(Items.bread, new Tuple(Convert.ToInt32(-4), Convert.ToInt32(-2)));
			blacksmithSellingList.Add(Items.melon, new Tuple(Convert.ToInt32(-8), Convert.ToInt32(-4)));
			blacksmithSellingList.Add(Items.apple, new Tuple(Convert.ToInt32(-8), Convert.ToInt32(-4)));
			blacksmithSellingList.Add(Items.cookie, new Tuple(Convert.ToInt32(-10), Convert.ToInt32(-7)));
			blacksmithSellingList.Add(Item.getItemFromBlock(Blocks.glass), new Tuple(Convert.ToInt32(-5), Convert.ToInt32(-3)));
			blacksmithSellingList.Add(Item.getItemFromBlock(Blocks.bookshelf), new Tuple(Convert.ToInt32(3), Convert.ToInt32(4)));
			blacksmithSellingList.Add(Items.leather_chestplate, new Tuple(Convert.ToInt32(4), Convert.ToInt32(5)));
			blacksmithSellingList.Add(Items.leather_boots, new Tuple(Convert.ToInt32(2), Convert.ToInt32(4)));
			blacksmithSellingList.Add(Items.leather_helmet, new Tuple(Convert.ToInt32(2), Convert.ToInt32(4)));
			blacksmithSellingList.Add(Items.leather_leggings, new Tuple(Convert.ToInt32(2), Convert.ToInt32(4)));
			blacksmithSellingList.Add(Items.saddle, new Tuple(Convert.ToInt32(6), Convert.ToInt32(8)));
			blacksmithSellingList.Add(Items.experience_bottle, new Tuple(Convert.ToInt32(-4), Convert.ToInt32(-1)));
			blacksmithSellingList.Add(Items.redstone, new Tuple(Convert.ToInt32(-4), Convert.ToInt32(-1)));
			blacksmithSellingList.Add(Items.compass, new Tuple(Convert.ToInt32(10), Convert.ToInt32(12)));
			blacksmithSellingList.Add(Items.clock, new Tuple(Convert.ToInt32(10), Convert.ToInt32(12)));
			blacksmithSellingList.Add(Item.getItemFromBlock(Blocks.glowstone), new Tuple(Convert.ToInt32(-3), Convert.ToInt32(-1)));
			blacksmithSellingList.Add(Items.cooked_porkchop, new Tuple(Convert.ToInt32(-7), Convert.ToInt32(-5)));
			blacksmithSellingList.Add(Items.cooked_beef, new Tuple(Convert.ToInt32(-7), Convert.ToInt32(-5)));
			blacksmithSellingList.Add(Items.cooked_chicken, new Tuple(Convert.ToInt32(-8), Convert.ToInt32(-6)));
			blacksmithSellingList.Add(Items.ender_eye, new Tuple(Convert.ToInt32(7), Convert.ToInt32(11)));
			blacksmithSellingList.Add(Items.arrow, new Tuple(Convert.ToInt32(-12), Convert.ToInt32(-8)));
		}
	}

}