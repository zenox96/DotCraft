namespace DotCraftCore.nEntity.nMonster
{

	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using EntityLiving = DotCraftCore.nEntity.EntityLiving;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using IRangedAttackMob = DotCraftCore.nEntity.IRangedAttackMob;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using EntityAIArrowAttack = DotCraftCore.nEntity.nAI.EntityAIArrowAttack;
	using EntityAILookIdle = DotCraftCore.nEntity.nAI.EntityAILookIdle;
	using EntityAINearestAttackableTarget = DotCraftCore.nEntity.nAI.EntityAINearestAttackableTarget;
	using EntityAIWander = DotCraftCore.nEntity.nAI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.nEntity.nAI.EntityAIWatchClosest;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using EntitySnowball = DotCraftCore.nEntity.nProjectile.EntitySnowball;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class EntitySnowman : EntityGolem, IRangedAttackMob
	{
		

		public EntitySnowman(World p_i1692_1_) : base(p_i1692_1_)
		{
			this.setSize(0.4F, 1.8F);
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(1, new EntityAIArrowAttack(this, 1.25D, 20, 10.0F));
			this.tasks.addTask(2, new EntityAIWander(this, 1.0D));
			this.tasks.addTask(3, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(4, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAINearestAttackableTarget(this, typeof(EntityLiving), 0, true, false, IMob.mobSelector));
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
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.20000000298023224D;
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			int var1 = MathHelper.floor_double(this.posX);
			int var2 = MathHelper.floor_double(this.posY);
			int var3 = MathHelper.floor_double(this.posZ);

			if (this.Wet)
			{
				this.attackEntityFrom(DamageSource.drown, 1.0F);
			}

			if (this.worldObj.getBiomeGenForCoords(var1, var3).getFloatTemperature(var1, var2, var3) > 1.0F)
			{
				this.attackEntityFrom(DamageSource.onFire, 1.0F);
			}

			for (int var4 = 0; var4 < 4; ++var4)
			{
				var1 = MathHelper.floor_double(this.posX + (double)((float)(var4 % 2 * 2 - 1) * 0.25F));
				var2 = MathHelper.floor_double(this.posY);
				var3 = MathHelper.floor_double(this.posZ + (double)((float)(var4 / 2 % 2 * 2 - 1) * 0.25F));

				if (this.worldObj.getBlock(var1, var2, var3).Material == Material.air && this.worldObj.getBiomeGenForCoords(var1, var3).getFloatTemperature(var1, var2, var3) < 0.8F && Blocks.snow_layer.canPlaceBlockAt(this.worldObj, var1, var2, var3))
				{
					this.worldObj.setBlock(var1, var2, var3, Blocks.snow_layer);
				}
			}
		}

		protected internal override Item func_146068_u()
		{
			return Items.snowball;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(16);

			for (int var4 = 0; var4 < var3; ++var4)
			{
				this.func_145779_a(Items.snowball, 1);
			}
		}

///    
///     <summary> * Attack the specified entity using a ranged attack. </summary>
///     
		public virtual void attackEntityWithRangedAttack(EntityLivingBase p_82196_1_, float p_82196_2_)
		{
			EntitySnowball var3 = new EntitySnowball(this.worldObj, this);
			double var4 = p_82196_1_.posX - this.posX;
			double var6 = p_82196_1_.posY + (double)p_82196_1_.EyeHeight - 1.100000023841858D - var3.posY;
			double var8 = p_82196_1_.posZ - this.posZ;
			float var10 = MathHelper.sqrt_double(var4 * var4 + var8 * var8) * 0.2F;
			var3.setThrowableHeading(var4, var6 + (double)var10, var8, 1.6F, 12.0F);
			this.playSound("random.bow", 1.0F, 1.0F / (this.RNG.nextFloat() * 0.4F + 0.8F));
			this.worldObj.spawnEntityInWorld(var3);
		}
	}

}