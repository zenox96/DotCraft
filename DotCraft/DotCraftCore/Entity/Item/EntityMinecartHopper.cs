using System.Collections;

namespace DotCraftCore.Entity.Item
{

	using Block = DotCraftCore.block.Block;
	using IEntitySelector = DotCraftCore.command.IEntitySelector;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Item = DotCraftCore.Item.Item;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using IHopper = DotCraftCore.TileEntity.IHopper;
	using TileEntityHopper = DotCraftCore.TileEntity.TileEntityHopper;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using World = DotCraftCore.World.World;

	public class EntityMinecartHopper : EntityMinecartContainer, IHopper
	{
	/// <summary> Whether this hopper minecart is being blocked by an activator rail.  </summary>
		private bool isBlocked = true;
		private int transferTicker = -1;
		private const string __OBFID = "CL_00001676";

		public EntityMinecartHopper(World p_i1720_1_) : base(p_i1720_1_)
		{
		}

		public EntityMinecartHopper(World p_i1721_1_, double p_i1721_2_, double p_i1721_4_, double p_i1721_6_) : base(p_i1721_1_, p_i1721_2_, p_i1721_4_, p_i1721_6_)
		{
		}

		public override int MinecartType
		{
			get
			{
				return 5;
			}
		}

		public override Block func_145817_o()
		{
			return Blocks.hopper;
		}

		public override int DefaultDisplayTileOffset
		{
			get
			{
				return 1;
			}
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return 5;
			}
		}

///    
///     <summary> * First layer of player interaction </summary>
///     
		public override bool interactFirst(EntityPlayer p_130002_1_)
		{
			if (!this.worldObj.isClient)
			{
				p_130002_1_.displayGUIHopperMinecart(this);
			}

			return true;
		}

///    
///     <summary> * Called every tick the minecart is on an activator rail. Args: x, y, z, is the rail receiving power </summary>
///     
		public override void onActivatorRailPass(int p_96095_1_, int p_96095_2_, int p_96095_3_, bool p_96095_4_)
		{
			bool var5 = !p_96095_4_;

			if (var5 != this.Blocked)
			{
				this.Blocked = var5;
			}
		}

///    
///     <summary> * Get whether this hopper minecart is being blocked by an activator rail. </summary>
///     
		public virtual bool Blocked
		{
			get
			{
				return this.isBlocked;
			}
			set
			{
				this.isBlocked = value;
			}
		}

///    
///     <summary> * Set whether this hopper minecart is being blocked by an activator rail. </summary>
///     

///    
///     <summary> * Returns the worldObj for this tileEntity. </summary>
///     
		public virtual World WorldObj
		{
			get
			{
				return this.worldObj;
			}
		}

///    
///     <summary> * Gets the world X position for this hopper entity. </summary>
///     
		public virtual double XPos
		{
			get
			{
				return this.posX;
			}
		}

///    
///     <summary> * Gets the world Y position for this hopper entity. </summary>
///     
		public virtual double YPos
		{
			get
			{
				return this.posY;
			}
		}

///    
///     <summary> * Gets the world Z position for this hopper entity. </summary>
///     
		public virtual double ZPos
		{
			get
			{
				return this.posZ;
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();

			if (!this.worldObj.isClient && this.EntityAlive && this.Blocked)
			{
				--this.transferTicker;

				if (!this.canTransfer())
				{
					this.TransferTicker = 0;

					if (this.func_96112_aD())
					{
						this.TransferTicker = 4;
						this.onInventoryChanged();
					}
				}
			}
		}

		public virtual bool func_96112_aD()
		{
			if (TileEntityHopper.func_145891_a(this))
			{
				return true;
			}
			else
			{
				IList var1 = this.worldObj.selectEntitiesWithinAABB(typeof(EntityItem), this.boundingBox.expand(0.25D, 0.0D, 0.25D), IEntitySelector.selectAnything);

				if (var1.Count > 0)
				{
					TileEntityHopper.func_145898_a(this, (EntityItem)var1[0]);
				}

				return false;
			}
		}

		public override void killMinecart(DamageSource p_94095_1_)
		{
			base.killMinecart(p_94095_1_);
			this.func_145778_a(Item.getItemFromBlock(Blocks.hopper), 1, 0.0F);
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("TransferCooldown", this.transferTicker);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.transferTicker = p_70037_1_.getInteger("TransferCooldown");
		}

///    
///     <summary> * Sets the transfer ticker, used to determine the delay between transfers. </summary>
///     
		public virtual int TransferTicker
		{
			set
			{
				this.transferTicker = value;
			}
		}

///    
///     <summary> * Returns whether the hopper cart can currently transfer an item. </summary>
///     
		public virtual bool canTransfer()
		{
			return this.transferTicker > 0;
		}
	}

}