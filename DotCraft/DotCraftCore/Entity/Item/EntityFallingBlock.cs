using System;
using System.Collections;

namespace DotCraftCore.Entity.Item
{

	using Block = DotCraftCore.block.Block;
	using BlockFalling = DotCraftCore.block.BlockFalling;
	using ITileEntityProvider = DotCraftCore.block.ITileEntityProvider;
	using Material = DotCraftCore.block.material.Material;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using Entity = DotCraftCore.Entity.Entity;
	using Blocks = DotCraftCore.Init.Blocks;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTBase = DotCraftCore.NBT.NBTBase;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class EntityFallingBlock : Entity
	{
		private Block field_145811_e;
		public int field_145814_a;
		public int field_145812_b;
		public bool field_145813_c;
		private bool field_145808_f;
		private bool field_145809_g;
		private int field_145815_h;
		private float field_145816_i;
		public NBTTagCompound field_145810_d;
		private const string __OBFID = "CL_00001668";

		public EntityFallingBlock(World p_i1706_1_) : base(p_i1706_1_)
		{
			this.field_145813_c = true;
			this.field_145815_h = 40;
			this.field_145816_i = 2.0F;
		}

		public EntityFallingBlock(World p_i45318_1_, double p_i45318_2_, double p_i45318_4_, double p_i45318_6_, Block p_i45318_8_) : this(p_i45318_1_, p_i45318_2_, p_i45318_4_, p_i45318_6_, p_i45318_8_, 0)
		{
		}

		public EntityFallingBlock(World p_i45319_1_, double p_i45319_2_, double p_i45319_4_, double p_i45319_6_, Block p_i45319_8_, int p_i45319_9_) : base(p_i45319_1_)
		{
			this.field_145813_c = true;
			this.field_145815_h = 40;
			this.field_145816_i = 2.0F;
			this.field_145811_e = p_i45319_8_;
			this.field_145814_a = p_i45319_9_;
			this.preventEntitySpawning = true;
			this.setSize(0.98F, 0.98F);
			this.yOffset = this.height / 2.0F;
			this.setPosition(p_i45319_2_, p_i45319_4_, p_i45319_6_);
			this.motionX = 0.0D;
			this.motionY = 0.0D;
			this.motionZ = 0.0D;
			this.prevPosX = p_i45319_2_;
			this.prevPosY = p_i45319_4_;
			this.prevPosZ = p_i45319_6_;
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		protected internal override void entityInit()
		{
		}

///    
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public override bool canBeCollidedWith()
		{
			return !this.isDead;
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			if (this.field_145811_e.Material == Material.air)
			{
				this.setDead();
			}
			else
			{
				this.prevPosX = this.posX;
				this.prevPosY = this.posY;
				this.prevPosZ = this.posZ;
				++this.field_145812_b;
				this.motionY -= 0.03999999910593033D;
				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				this.motionX *= 0.9800000190734863D;
				this.motionY *= 0.9800000190734863D;
				this.motionZ *= 0.9800000190734863D;

				if (!this.worldObj.isClient)
				{
					int var1 = MathHelper.floor_double(this.posX);
					int var2 = MathHelper.floor_double(this.posY);
					int var3 = MathHelper.floor_double(this.posZ);

					if (this.field_145812_b == 1)
					{
						if (this.worldObj.getBlock(var1, var2, var3) != this.field_145811_e)
						{
							this.setDead();
							return;
						}

						this.worldObj.setBlockToAir(var1, var2, var3);
					}

					if (this.onGround)
					{
						this.motionX *= 0.699999988079071D;
						this.motionZ *= 0.699999988079071D;
						this.motionY *= -0.5D;

						if (this.worldObj.getBlock(var1, var2, var3) != Blocks.piston_extension)
						{
							this.setDead();

							if (!this.field_145808_f && this.worldObj.canPlaceEntityOnSide(this.field_145811_e, var1, var2, var3, true, 1, (Entity)null, (ItemStack)null) && !BlockFalling.func_149831_e(this.worldObj, var1, var2 - 1, var3) && this.worldObj.setBlock(var1, var2, var3, this.field_145811_e, this.field_145814_a, 3))
							{
								if (this.field_145811_e is BlockFalling)
								{
									((BlockFalling)this.field_145811_e).func_149828_a(this.worldObj, var1, var2, var3, this.field_145814_a);
								}

								if (this.field_145810_d != null && this.field_145811_e is ITileEntityProvider)
								{
									TileEntity var4 = this.worldObj.getTileEntity(var1, var2, var3);

									if (var4 != null)
									{
										NBTTagCompound var5 = new NBTTagCompound();
										var4.writeToNBT(var5);
										IEnumerator var6 = this.field_145810_d.func_150296_c().GetEnumerator();

										while (var6.MoveNext())
										{
											string var7 = (string)var6.Current;
											NBTBase var8 = this.field_145810_d.getTag(var7);

											if (!var7.Equals("x") && !var7.Equals("y") && !var7.Equals("z"))
											{
												var5.setTag(var7, var8.copy());
											}
										}

										var4.readFromNBT(var5);
										var4.onInventoryChanged();
									}
								}
							}
							else if (this.field_145813_c && !this.field_145808_f)
							{
								this.entityDropItem(new ItemStack(this.field_145811_e, 1, this.field_145811_e.damageDropped(this.field_145814_a)), 0.0F);
							}
						}
					}
					else if (this.field_145812_b > 100 && !this.worldObj.isClient && (var2 < 1 || var2 > 256) || this.field_145812_b > 600)
					{
						if (this.field_145813_c)
						{
							this.entityDropItem(new ItemStack(this.field_145811_e, 1, this.field_145811_e.damageDropped(this.field_145814_a)), 0.0F);
						}

						this.setDead();
					}
				}
			}
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
			if (this.field_145809_g)
			{
				int var2 = MathHelper.ceiling_float_int(p_70069_1_ - 1.0F);

				if (var2 > 0)
				{
					ArrayList var3 = new ArrayList(this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox));
					bool var4 = this.field_145811_e == Blocks.anvil;
					DamageSource var5 = var4 ? DamageSource.anvil : DamageSource.fallingBlock;
					IEnumerator var6 = var3.GetEnumerator();

					while (var6.MoveNext())
					{
						Entity var7 = (Entity)var6.Current;
						var7.attackEntityFrom(var5, (float)Math.Min(MathHelper.floor_float((float)var2 * this.field_145816_i), this.field_145815_h));
					}

					if (var4 && (double)this.rand.nextFloat() < 0.05000000074505806D + (double)var2 * 0.05D)
					{
						int var8 = this.field_145814_a >> 2;
						int var9 = this.field_145814_a & 3;
						++var8;

						if (var8 > 2)
						{
							this.field_145808_f = true;
						}
						else
						{
							this.field_145814_a = var9 | var8 << 2;
						}
					}
				}
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setByte("Tile", (sbyte)Block.getIdFromBlock(this.field_145811_e));
			p_70014_1_.setInteger("TileID", Block.getIdFromBlock(this.field_145811_e));
			p_70014_1_.setByte("Data", (sbyte)this.field_145814_a);
			p_70014_1_.setByte("Time", (sbyte)this.field_145812_b);
			p_70014_1_.setBoolean("DropItem", this.field_145813_c);
			p_70014_1_.setBoolean("HurtEntities", this.field_145809_g);
			p_70014_1_.setFloat("FallHurtAmount", this.field_145816_i);
			p_70014_1_.setInteger("FallHurtMax", this.field_145815_h);

			if (this.field_145810_d != null)
			{
				p_70014_1_.setTag("TileEntityData", this.field_145810_d);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			if (p_70037_1_.func_150297_b("TileID", 99))
			{
				this.field_145811_e = Block.getBlockById(p_70037_1_.getInteger("TileID"));
			}
			else
			{
				this.field_145811_e = Block.getBlockById(p_70037_1_.getByte("Tile") & 255);
			}

			this.field_145814_a = p_70037_1_.getByte("Data") & 255;
			this.field_145812_b = p_70037_1_.getByte("Time") & 255;

			if (p_70037_1_.func_150297_b("HurtEntities", 99))
			{
				this.field_145809_g = p_70037_1_.getBoolean("HurtEntities");
				this.field_145816_i = p_70037_1_.getFloat("FallHurtAmount");
				this.field_145815_h = p_70037_1_.getInteger("FallHurtMax");
			}
			else if (this.field_145811_e == Blocks.anvil)
			{
				this.field_145809_g = true;
			}

			if (p_70037_1_.func_150297_b("DropItem", 99))
			{
				this.field_145813_c = p_70037_1_.getBoolean("DropItem");
			}

			if (p_70037_1_.func_150297_b("TileEntityData", 10))
			{
				this.field_145810_d = p_70037_1_.getCompoundTag("TileEntityData");
			}

			if (this.field_145811_e.Material == Material.air)
			{
				this.field_145811_e = Blocks.sand;
			}
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}

		public virtual World func_145807_e()
		{
			return this.worldObj;
		}

		public virtual void func_145806_a(bool p_145806_1_)
		{
			this.field_145809_g = p_145806_1_;
		}

///    
///     <summary> * Return whether this entity should be rendered as on fire. </summary>
///     
		public override bool canRenderOnFire()
		{
			return false;
		}

		public override void addEntityCrashInfo(CrashReportCategory p_85029_1_)
		{
			base.addEntityCrashInfo(p_85029_1_);
			p_85029_1_.addCrashSection("Immitating block ID", Convert.ToInt32(Block.getIdFromBlock(this.field_145811_e)));
			p_85029_1_.addCrashSection("Immitating block data", Convert.ToInt32(this.field_145814_a));
		}

		public virtual Block func_145805_f()
		{
			return this.field_145811_e;
		}
	}

}