using System;

namespace DotCraftCore.Entity.AI
{

	using Block = DotCraftCore.block.Block;
	using BlockSlab = DotCraftCore.block.BlockSlab;
	using Material = DotCraftCore.block.material.Material;
	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using PathFinder = DotCraftCore.Pathfinding.PathFinder;
	using PathPoint = DotCraftCore.Pathfinding.PathPoint;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class EntityAIControlledByPlayer : EntityAIBase
	{
		private readonly EntityLiving thisEntity;
		private readonly float maxSpeed;
		private float currentSpeed;

	/// <summary> Whether the entity's speed is boosted.  </summary>
		private bool speedBoosted;

///    
///     <summary> * Counter for speed boosting, upon reaching maxSpeedBoostTime the speed boost will be disabled </summary>
///     
		private int speedBoostTime;

	/// <summary> Maximum time the entity's speed should be boosted for.  </summary>
		private int maxSpeedBoostTime;
		

		public EntityAIControlledByPlayer(EntityLiving p_i1620_1_, float p_i1620_2_)
		{
			this.thisEntity = p_i1620_1_;
			this.maxSpeed = p_i1620_2_;
			this.MutexBits = 7;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.currentSpeed = 0.0F;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.speedBoosted = false;
			this.currentSpeed = 0.0F;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			return this.thisEntity.EntityAlive && this.thisEntity.riddenByEntity != null && this.thisEntity.riddenByEntity is EntityPlayer && (this.speedBoosted || this.thisEntity.canBeSteered());
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			EntityPlayer var1 = (EntityPlayer)this.thisEntity.riddenByEntity;
			EntityCreature var2 = (EntityCreature)this.thisEntity;
			float var3 = MathHelper.wrapAngleTo180_float(var1.rotationYaw - this.thisEntity.rotationYaw) * 0.5F;

			if (var3 > 5.0F)
			{
				var3 = 5.0F;
			}

			if (var3 < -5.0F)
			{
				var3 = -5.0F;
			}

			this.thisEntity.rotationYaw = MathHelper.wrapAngleTo180_float(this.thisEntity.rotationYaw + var3);

			if (this.currentSpeed < this.maxSpeed)
			{
				this.currentSpeed += (this.maxSpeed - this.currentSpeed) * 0.01F;
			}

			if (this.currentSpeed > this.maxSpeed)
			{
				this.currentSpeed = this.maxSpeed;
			}

			int var4 = MathHelper.floor_double(this.thisEntity.posX);
			int var5 = MathHelper.floor_double(this.thisEntity.posY);
			int var6 = MathHelper.floor_double(this.thisEntity.posZ);
			float var7 = this.currentSpeed;

			if (this.speedBoosted)
			{
				if (this.speedBoostTime++ > this.maxSpeedBoostTime)
				{
					this.speedBoosted = false;
				}

				var7 += var7 * 1.15F * MathHelper.sin((float)this.speedBoostTime / (float)this.maxSpeedBoostTime * (float)Math.PI);
			}

			float var8 = 0.91F;

			if (this.thisEntity.onGround)
			{
				var8 = this.thisEntity.worldObj.getBlock(MathHelper.floor_float((float)var4), MathHelper.floor_float((float)var5) - 1, MathHelper.floor_float((float)var6)).slipperiness * 0.91F;
			}

			float var9 = 0.16277136F / (var8 * var8 * var8);
			float var10 = MathHelper.sin(var2.rotationYaw * (float)Math.PI / 180.0F);
			float var11 = MathHelper.cos(var2.rotationYaw * (float)Math.PI / 180.0F);
			float var12 = var2.AIMoveSpeed * var9;
			float var13 = Math.Max(var7, 1.0F);
			var13 = var12 / var13;
			float var14 = var7 * var13;
			float var15 = -(var14 * var10);
			float var16 = var14 * var11;

			if (MathHelper.abs(var15) > MathHelper.abs(var16))
			{
				if (var15 < 0.0F)
				{
					var15 -= this.thisEntity.width / 2.0F;
				}

				if (var15 > 0.0F)
				{
					var15 += this.thisEntity.width / 2.0F;
				}

				var16 = 0.0F;
			}
			else
			{
				var15 = 0.0F;

				if (var16 < 0.0F)
				{
					var16 -= this.thisEntity.width / 2.0F;
				}

				if (var16 > 0.0F)
				{
					var16 += this.thisEntity.width / 2.0F;
				}
			}

			int var17 = MathHelper.floor_double(this.thisEntity.posX + (double)var15);
			int var18 = MathHelper.floor_double(this.thisEntity.posZ + (double)var16);
			PathPoint var19 = new PathPoint(MathHelper.floor_float(this.thisEntity.width + 1.0F), MathHelper.floor_float(this.thisEntity.height + var1.height + 1.0F), MathHelper.floor_float(this.thisEntity.width + 1.0F));

			if (var4 != var17 || var6 != var18)
			{
				Block var20 = this.thisEntity.worldObj.getBlock(var4, var5, var6);
				bool var21 = !this.func_151498_a(var20) && (var20.Material != Material.air || !this.func_151498_a(this.thisEntity.worldObj.getBlock(var4, var5 - 1, var6)));

				if (var21 && PathFinder.func_82565_a(this.thisEntity, var17, var5, var18, var19, false, false, true) == 0 && PathFinder.func_82565_a(this.thisEntity, var4, var5 + 1, var6, var19, false, false, true) == 1 && PathFinder.func_82565_a(this.thisEntity, var17, var5 + 1, var18, var19, false, false, true) == 1)
				{
					var2.JumpHelper.setJumping();
				}
			}

			if (!var1.capabilities.isCreativeMode && this.currentSpeed >= this.maxSpeed * 0.5F && this.thisEntity.RNG.nextFloat() < 0.006F && !this.speedBoosted)
			{
				ItemStack var22 = var1.HeldItem;

				if (var22 != null && var22.Item == Items.carrot_on_a_stick)
				{
					var22.damageItem(1, var1);

					if (var22.stackSize == 0)
					{
						ItemStack var23 = new ItemStack(Items.fishing_rod);
						var23.TagCompound = var22.stackTagCompound;
						var1.inventory.mainInventory[var1.inventory.currentItem] = var23;
					}
				}
			}

			this.thisEntity.moveEntityWithHeading(0.0F, var7);
		}

		private bool func_151498_a(Block p_151498_1_)
		{
			return p_151498_1_.RenderType == 10 || p_151498_1_ is BlockSlab;
		}

///    
///     <summary> * Return whether the entity's speed is boosted. </summary>
///     
		public virtual bool isSpeedBoosted()
		{
			get
			{
				return this.speedBoosted;
			}
		}

///    
///     <summary> * Boost the entity's movement speed. </summary>
///     
		public virtual void boostSpeed()
		{
			this.speedBoosted = true;
			this.speedBoostTime = 0;
			this.maxSpeedBoostTime = this.thisEntity.RNG.Next(841) + 140;
		}

///    
///     <summary> * Return whether the entity is being controlled by a player. </summary>
///     
		public virtual bool isControlledByPlayer()
		{
			get
			{
				return !this.SpeedBoosted && this.currentSpeed > this.maxSpeed * 0.3F;
			}
		}
	}

}