namespace DotCraftCore.nPotion
{

	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;

	public class PotionEffect
	{
	/// <summary> ID value of the potion this effect matches.  </summary>
		private int potionID;

	/// <summary> The duration of the potion effect  </summary>
		private int duration;

	/// <summary> The amplifier of the potion effect  </summary>
		private int amplifier;

	/// <summary> Whether the potion is a splash potion  </summary>
		private bool isSplashPotion;

	/// <summary> Whether the potion effect came from a beacon  </summary>
		private bool isAmbient;

	/// <summary> True if potion effect duration is at maximum, false otherwise.  </summary>
		private bool isPotionDurationMax;
		

		public PotionEffect(int p_i1574_1_, int p_i1574_2_) : this(p_i1574_1_, p_i1574_2_, 0)
		{
		}

		public PotionEffect(int p_i1575_1_, int p_i1575_2_, int p_i1575_3_) : this(p_i1575_1_, p_i1575_2_, p_i1575_3_, false)
		{
		}

		public PotionEffect(int p_i1576_1_, int p_i1576_2_, int p_i1576_3_, bool p_i1576_4_)
		{
			this.potionID = p_i1576_1_;
			this.duration = p_i1576_2_;
			this.amplifier = p_i1576_3_;
			this.isAmbient = p_i1576_4_;
		}

		public PotionEffect(PotionEffect p_i1577_1_)
		{
			this.potionID = p_i1577_1_.potionID;
			this.duration = p_i1577_1_.duration;
			this.amplifier = p_i1577_1_.amplifier;
		}

///    
///     <summary> * merges the input PotionEffect into this one if this.amplifier <= tomerge.amplifier. The duration in the supplied
///     * potion effect is assumed to be greater. </summary>
///     
		public virtual void combine(PotionEffect p_76452_1_)
		{
			if (this.potionID != p_76452_1_.potionID)
			{
				System.err.println("This method should only be called for matching effects!");
			}

			if (p_76452_1_.amplifier > this.amplifier)
			{
				this.amplifier = p_76452_1_.amplifier;
				this.duration = p_76452_1_.duration;
			}
			else if (p_76452_1_.amplifier == this.amplifier && this.duration < p_76452_1_.duration)
			{
				this.duration = p_76452_1_.duration;
			}
			else if (!p_76452_1_.isAmbient && this.isAmbient)
			{
				this.isAmbient = p_76452_1_.isAmbient;
			}
		}

///    
///     <summary> * Retrieve the ID of the potion this effect matches. </summary>
///     
		public virtual int PotionID
		{
			get
			{
				return this.potionID;
			}
		}

		public virtual int Duration
		{
			get
			{
				return this.duration;
			}
		}

		public virtual int Amplifier
		{
			get
			{
				return this.amplifier;
			}
		}

///    
///     <summary> * Set whether this potion is a splash potion. </summary>
///     
		public virtual bool SplashPotion
		{
			set
			{
				this.isSplashPotion = value;
			}
		}

///    
///     <summary> * Gets whether this potion effect originated from a beacon </summary>
///     
		public virtual bool IsAmbient
		{
			get
			{
				return this.isAmbient;
			}
		}

		public virtual bool onUpdate(EntityLivingBase p_76455_1_)
		{
			if (this.duration > 0)
			{
				if (Potion.potionTypes[this.potionID].isReady(this.duration, this.amplifier))
				{
					this.performEffect(p_76455_1_);
				}

				this.deincrementDuration();
			}

			return this.duration > 0;
		}

		private int deincrementDuration()
		{
			return --this.duration;
		}

		public virtual void performEffect(EntityLivingBase p_76457_1_)
		{
			if (this.duration > 0)
			{
				Potion.potionTypes[this.potionID].performEffect(p_76457_1_, this.amplifier);
			}
		}

		public virtual string EffectName
		{
			get
			{
				return Potion.potionTypes[this.potionID].Name;
			}
		}

		public override int GetHashCode()
		{
			return this.potionID;
		}

		public override string ToString()
		{
			string var1 = "";

			if (this.Amplifier > 0)
			{
				var1 = this.EffectName + " x " + (this.Amplifier + 1) + ", Duration: " + this.Duration;
			}
			else
			{
				var1 = this.EffectName + ", Duration: " + this.Duration;
			}

			if (this.isSplashPotion)
			{
				var1 = var1 + ", Splash: true";
			}

			return Potion.potionTypes[this.potionID].Usable ? "(" + var1 + ")" : var1;
		}

		public override bool Equals(object p_equals_1_)
		{
			if (!(p_equals_1_ is PotionEffect))
			{
				return false;
			}
			else
			{
				PotionEffect var2 = (PotionEffect)p_equals_1_;
				return this.potionID == var2.potionID && this.amplifier == var2.amplifier && this.duration == var2.duration && this.isSplashPotion == var2.isSplashPotion && this.isAmbient == var2.isAmbient;
			}
		}

///    
///     <summary> * Write a custom potion effect to a potion item's NBT data. </summary>
///     
		public virtual NBTTagCompound writeCustomPotionEffectToNBT(NBTTagCompound p_82719_1_)
		{
			p_82719_1_.setByte("Id", (sbyte)this.PotionID);
			p_82719_1_.setByte("Amplifier", (sbyte)this.Amplifier);
			p_82719_1_.setInteger("Duration", this.Duration);
			p_82719_1_.setBoolean("Ambient", this.IsAmbient);
			return p_82719_1_;
		}

///    
///     <summary> * Read a custom potion effect from a potion item's NBT data. </summary>
///     
		public static PotionEffect readCustomPotionEffectFromNBT(NBTTagCompound p_82722_0_)
		{
			sbyte var1 = p_82722_0_.getByte("Id");

			if (var1 >= 0 && var1 < Potion.potionTypes.Length && Potion.potionTypes[var1] != null)
			{
				sbyte var2 = p_82722_0_.getByte("Amplifier");
				int var3 = p_82722_0_.getInteger("Duration");
				bool var4 = p_82722_0_.getBoolean("Ambient");
				return new PotionEffect(var1, var3, var2, var4);
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Toggle the isPotionDurationMax field. </summary>
///     
		public virtual bool PotionDurationMax
		{
			set
			{
				this.isPotionDurationMax = value;
			}
		}

		public virtual bool IsPotionDurationMax
		{
			get
			{
				return this.isPotionDurationMax;
			}
		}
	}

}