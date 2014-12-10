using System;

namespace DotCraftCore.Entity.Passive
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IEntityOwnable = DotCraftCore.Entity.IEntityOwnable;
	using EntityAISit = DotCraftCore.Entity.AI.EntityAISit;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using Team = DotCraftCore.scoreboard.Team;
	using PreYggdrasilConverter = DotCraftCore.server.management.PreYggdrasilConverter;
	using World = DotCraftCore.world.World;

	public abstract class EntityTameable : EntityAnimal, IEntityOwnable
	{
		protected internal EntityAISit aiSit = new EntityAISit(this);
		private const string __OBFID = "CL_00001561";

		public EntityTameable(World p_i1604_1_) : base(p_i1604_1_)
		{
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, Convert.ToByte((sbyte)0));
			this.dataWatcher.addObject(17, "");
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);

			if (this.func_152113_b() == null)
			{
				p_70014_1_.setString("OwnerUUID", "");
			}
			else
			{
				p_70014_1_.setString("OwnerUUID", this.func_152113_b());
			}

			p_70014_1_.setBoolean("Sitting", this.Sitting);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			string var2 = "";

			if (p_70037_1_.func_150297_b("OwnerUUID", 8))
			{
				var2 = p_70037_1_.getString("OwnerUUID");
			}
			else
			{
				string var3 = p_70037_1_.getString("Owner");
				var2 = PreYggdrasilConverter.func_152719_a(var3);
			}

			if (var2.Length > 0)
			{
				this.func_152115_b(var2);
				this.Tamed = true;
			}

			this.aiSit.Sitting = p_70037_1_.getBoolean("Sitting");
			this.Sitting = p_70037_1_.getBoolean("Sitting");
		}

///    
///     <summary> * Play the taming effect, will either be hearts or smoke depending on status </summary>
///     
		protected internal virtual void playTameEffect(bool p_70908_1_)
		{
			string var2 = "heart";

			if (!p_70908_1_)
			{
				var2 = "smoke";
			}

			for (int var3 = 0; var3 < 7; ++var3)
			{
				double var4 = this.rand.nextGaussian() * 0.02D;
				double var6 = this.rand.nextGaussian() * 0.02D;
				double var8 = this.rand.nextGaussian() * 0.02D;
				this.worldObj.spawnParticle(var2, this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, var4, var6, var8);
			}
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 7)
			{
				this.playTameEffect(true);
			}
			else if (p_70103_1_ == 6)
			{
				this.playTameEffect(false);
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

		public virtual bool isTamed()
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 4) != 0;
			}
			set
			{
				sbyte var2 = this.dataWatcher.getWatchableObjectByte(16);
	
				if (value)
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 | 4)));
				}
				else
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 & -5)));
				}
			}
		}


		public virtual bool isSitting()
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
			}
			set
			{
				sbyte var2 = this.dataWatcher.getWatchableObjectByte(16);
	
				if (value)
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 | 1)));
				}
				else
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 & -2)));
				}
			}
		}


		public virtual string func_152113_b()
		{
			return this.dataWatcher.getWatchableObjectString(17);
		}

		public virtual void func_152115_b(string p_152115_1_)
		{
			this.dataWatcher.updateObject(17, p_152115_1_);
		}

		public virtual EntityLivingBase Owner
		{
			get
			{
				try
				{
					UUID var1 = UUID.fromString(this.func_152113_b());
					return var1 == null ? null : this.worldObj.func_152378_a(var1);
				}
				catch (System.ArgumentException var2)
				{
					return null;
				}
			}
		}

		public virtual bool func_152114_e(EntityLivingBase p_152114_1_)
		{
			return p_152114_1_ == this.Owner;
		}

		public virtual EntityAISit func_70907_r()
		{
			return this.aiSit;
		}

		public virtual bool func_142018_a(EntityLivingBase p_142018_1_, EntityLivingBase p_142018_2_)
		{
			return true;
		}

		public override Team Team
		{
			get
			{
				if (this.Tamed)
				{
					EntityLivingBase var1 = this.Owner;
	
					if (var1 != null)
					{
						return var1.Team;
					}
				}
	
				return base.Team;
			}
		}

		public override bool isOnSameTeam(EntityLivingBase p_142014_1_)
		{
			if (this.Tamed)
			{
				EntityLivingBase var2 = this.Owner;

				if (p_142014_1_ == var2)
				{
					return true;
				}

				if (var2 != null)
				{
					return var2.isOnSameTeam(p_142014_1_);
				}
			}

			return base.isOnSameTeam(p_142014_1_);
		}
	}

}