using System;

namespace DotCraftCore.nUtil
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using ItemFood = DotCraftCore.item.ItemFood;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using EnumDifficulty = DotCraftCore.nWorld.EnumDifficulty;

	public class FoodStats
	{
	/// <summary> The player's food level.  </summary>
		private int foodLevel = 20;

	/// <summary> The player's food saturation.  </summary>
		private float foodSaturationLevel = 5.0F;

	/// <summary> The player's food exhaustion.  </summary>
		private float foodExhaustionLevel;

	/// <summary> The player's food timer value.  </summary>
		private int foodTimer;
		private int prevFoodLevel = 20;
		

///    
///     <summary> * Args: int foodLevel, float foodSaturationModifier </summary>
///     
		public virtual void addStats(int p_75122_1_, float p_75122_2_)
		{
			this.foodLevel = Math.Min(p_75122_1_ + this.foodLevel, 20);
			this.foodSaturationLevel = Math.Min(this.foodSaturationLevel + (float)p_75122_1_ * p_75122_2_ * 2.0F, (float)this.foodLevel);
		}

		public virtual void func_151686_a(ItemFood p_151686_1_, ItemStack p_151686_2_)
		{
			this.addStats(p_151686_1_.func_150905_g(p_151686_2_), p_151686_1_.func_150906_h(p_151686_2_));
		}

///    
///     <summary> * Handles the food game logic. </summary>
///     
		public virtual void onUpdate(EntityPlayer p_75118_1_)
		{
			EnumDifficulty var2 = p_75118_1_.worldObj.difficultySetting;
			this.prevFoodLevel = this.foodLevel;

			if(this.foodExhaustionLevel > 4.0F)
			{
				this.foodExhaustionLevel -= 4.0F;

				if(this.foodSaturationLevel > 0.0F)
				{
					this.foodSaturationLevel = Math.Max(this.foodSaturationLevel - 1.0F, 0.0F);
				}
				else if(var2 != EnumDifficulty.PEACEFUL)
				{
					this.foodLevel = Math.Max(this.foodLevel - 1, 0);
				}
			}

			if(p_75118_1_.worldObj.GameRules.getGameRuleBooleanValue("naturalRegeneration") && this.foodLevel >= 18 && p_75118_1_.shouldHeal())
			{
				++this.foodTimer;

				if(this.foodTimer >= 80)
				{
					p_75118_1_.heal(1.0F);
					this.addExhaustion(3.0F);
					this.foodTimer = 0;
				}
			}
			else if(this.foodLevel <= 0)
			{
				++this.foodTimer;

				if(this.foodTimer >= 80)
				{
					if(p_75118_1_.Health > 10.0F || var2 == EnumDifficulty.HARD || p_75118_1_.Health > 1.0F && var2 == EnumDifficulty.NORMAL)
					{
						p_75118_1_.attackEntityFrom(DamageSource.starve, 1.0F);
					}

					this.foodTimer = 0;
				}
			}
			else
			{
				this.foodTimer = 0;
			}
		}

///    
///     <summary> * Reads food stats from an NBT object. </summary>
///     
		public virtual void readNBT(NBTTagCompound p_75112_1_)
		{
			if(p_75112_1_.func_150297_b("foodLevel", 99))
			{
				this.foodLevel = p_75112_1_.getInteger("foodLevel");
				this.foodTimer = p_75112_1_.getInteger("foodTickTimer");
				this.foodSaturationLevel = p_75112_1_.getFloat("foodSaturationLevel");
				this.foodExhaustionLevel = p_75112_1_.getFloat("foodExhaustionLevel");
			}
		}

///    
///     <summary> * Writes food stats to an NBT object. </summary>
///     
		public virtual void writeNBT(NBTTagCompound p_75117_1_)
		{
			p_75117_1_.setInteger("foodLevel", this.foodLevel);
			p_75117_1_.setInteger("foodTickTimer", this.foodTimer);
			p_75117_1_.setFloat("foodSaturationLevel", this.foodSaturationLevel);
			p_75117_1_.setFloat("foodExhaustionLevel", this.foodExhaustionLevel);
		}

///    
///     <summary> * Get the player's food level. </summary>
///     
		public virtual int FoodLevel
		{
			get
			{
				return this.foodLevel;
			}
			set
			{
				this.foodLevel = value;
			}
		}

		public virtual int PrevFoodLevel
		{
			get
			{
				return this.prevFoodLevel;
			}
		}

///    
///     <summary> * If foodLevel is not max. </summary>
///     
		public virtual bool needFood()
		{
			return this.foodLevel < 20;
		}

///    
///     <summary> * adds input to foodExhaustionLevel to a max of 40 </summary>
///     
		public virtual void addExhaustion(float p_75113_1_)
		{
			this.foodExhaustionLevel = Math.Min(this.foodExhaustionLevel + p_75113_1_, 40.0F);
		}

///    
///     <summary> * Get the player's food saturation level. </summary>
///     
		public virtual float SaturationLevel
		{
			get
			{
				return this.foodSaturationLevel;
			}
		}


		public virtual float FoodSaturationLevel
		{
			set
			{
				this.foodSaturationLevel = value;
			}
		}
	}

}