using System.Collections;

namespace DotCraftCore.Entity
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using World = DotCraftCore.World.World;

	public class EntityLeashKnot : EntityHanging
	{
		

		public EntityLeashKnot(World p_i1592_1_) : base(p_i1592_1_)
		{
		}

		public EntityLeashKnot(World p_i1593_1_, int p_i1593_2_, int p_i1593_3_, int p_i1593_4_) : base(p_i1593_1_, p_i1593_2_, p_i1593_3_, p_i1593_4_, 0)
		{
			this.setPosition((double)p_i1593_2_ + 0.5D, (double)p_i1593_3_ + 0.5D, (double)p_i1593_4_ + 0.5D);
		}

		protected internal override void entityInit()
		{
			base.entityInit();
		}

		public override int Direction
		{
			set
			{
			}
		}

		public override int WidthPixels
		{
			get
			{
				return 9;
			}
		}

		public override int HeightPixels
		{
			get
			{
				return 9;
			}
		}

///    
///     <summary> * Checks if the entity is in range to render by using the past in distance and comparing it to its average edge
///     * length * 64 * renderDistanceWeight Args: distance </summary>
///     
		public override bool isInRangeToRenderDist(double p_70112_1_)
		{
			return p_70112_1_ < 1024.0D;
		}

///    
///     <summary> * Called when this entity is broken. Entity parameter may be null. </summary>
///     
		public override void onBroken(Entity p_110128_1_)
		{
		}

///    
///     <summary> * Either write this entity to the NBT tag given and return true, or return false without doing anything. If this
///     * returns false the entity is not saved on disk. Ridden entities return false here as they are saved with their
///     * rider. </summary>
///     
		public override bool writeToNBTOptional(NBTTagCompound p_70039_1_)
		{
			return false;
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
		}

///    
///     <summary> * First layer of player interaction </summary>
///     
		public override bool interactFirst(EntityPlayer p_130002_1_)
		{
			ItemStack var2 = p_130002_1_.HeldItem;
			bool var3 = false;
			double var4;
			IList var6;
			IEnumerator var7;
			EntityLiving var8;

			if (var2 != null && var2.Item == Items.lead && !this.worldObj.isClient)
			{
				var4 = 7.0D;
				var6 = this.worldObj.getEntitiesWithinAABB(typeof(EntityLiving), AxisAlignedBB.getBoundingBox(this.posX - var4, this.posY - var4, this.posZ - var4, this.posX + var4, this.posY + var4, this.posZ + var4));

				if (var6 != null)
				{
					var7 = var6.GetEnumerator();

					while (var7.MoveNext())
					{
						var8 = (EntityLiving)var7.Current;

						if (var8.Leashed && var8.LeashedToEntity == p_130002_1_)
						{
							var8.setLeashedToEntity(this, true);
							var3 = true;
						}
					}
				}
			}

			if (!this.worldObj.isClient && !var3)
			{
				this.setDead();

				if (p_130002_1_.capabilities.isCreativeMode)
				{
					var4 = 7.0D;
					var6 = this.worldObj.getEntitiesWithinAABB(typeof(EntityLiving), AxisAlignedBB.getBoundingBox(this.posX - var4, this.posY - var4, this.posZ - var4, this.posX + var4, this.posY + var4, this.posZ + var4));

					if (var6 != null)
					{
						var7 = var6.GetEnumerator();

						while (var7.MoveNext())
						{
							var8 = (EntityLiving)var7.Current;

							if (var8.Leashed && var8.LeashedToEntity == this)
							{
								var8.clearLeashed(true, false);
							}
						}
					}
				}
			}

			return true;
		}

///    
///     <summary> * checks to make sure painting can be placed there </summary>
///     
		public override bool onValidSurface()
		{
			return this.worldObj.getBlock(this.field_146063_b, this.field_146064_c, this.field_146062_d).RenderType == 11;
		}

		public static EntityLeashKnot func_110129_a(World p_110129_0_, int p_110129_1_, int p_110129_2_, int p_110129_3_)
		{
			EntityLeashKnot var4 = new EntityLeashKnot(p_110129_0_, p_110129_1_, p_110129_2_, p_110129_3_);
			var4.forceSpawn = true;
			p_110129_0_.spawnEntityInWorld(var4);
			return var4;
		}

		public static EntityLeashKnot getKnotForBlock(World p_110130_0_, int p_110130_1_, int p_110130_2_, int p_110130_3_)
		{
			IList var4 = p_110130_0_.getEntitiesWithinAABB(typeof(EntityLeashKnot), AxisAlignedBB.getBoundingBox((double)p_110130_1_ - 1.0D, (double)p_110130_2_ - 1.0D, (double)p_110130_3_ - 1.0D, (double)p_110130_1_ + 1.0D, (double)p_110130_2_ + 1.0D, (double)p_110130_3_ + 1.0D));

			if (var4 != null)
			{
				IEnumerator var5 = var4.GetEnumerator();

				while (var5.MoveNext())
				{
					EntityLeashKnot var6 = (EntityLeashKnot)var5.Current;

					if (var6.field_146063_b == p_110130_1_ && var6.field_146064_c == p_110130_2_ && var6.field_146062_d == p_110130_3_)
					{
						return var6;
					}
				}
			}

			return null;
		}
	}

}