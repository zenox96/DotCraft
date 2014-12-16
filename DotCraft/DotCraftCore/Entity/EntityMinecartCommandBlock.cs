using System;

namespace DotCraftCore.Entity
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using Block = DotCraftCore.block.Block;
	using CommandBlockLogic = DotCraftCore.command.server.CommandBlockLogic;
	using EntityMinecart = DotCraftCore.Entity.Item.EntityMinecart;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class EntityMinecartCommandBlock : EntityMinecart
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private final CommandBlockLogic field_145824_a = new CommandBlockLogic()
//	{
//		
//		public void func_145756_e()
//		{
//			EntityMinecartCommandBlock.getDataWatcher().updateObject(23, this.func_145753_i());
//			EntityMinecartCommandBlock.getDataWatcher().updateObject(24, IChatComponent.Serializer.func_150696_a(this.func_145749_h()));
//		}
//		public int func_145751_f()
//		{
//			return 1;
//		}
//		public void func_145757_a(ByteBuf p_145757_1_)
//		{
//			p_145757_1_.writeInt(EntityMinecartCommandBlock.getEntityId());
//		}
//		public ChunkCoordinates getPlayerCoordinates()
//		{
//			return new ChunkCoordinates(MathHelper.floor_double(EntityMinecartCommandBlock.posX), MathHelper.floor_double(EntityMinecartCommandBlock.posY + 0.5D), MathHelper.floor_double(EntityMinecartCommandBlock.posZ));
//		}
//		public World getEntityWorld()
//		{
//			return EntityMinecartCommandBlock.worldObj;
//		}
//	};
		private int field_145823_b = 0;
		

		public EntityMinecartCommandBlock(World p_i45321_1_) : base(p_i45321_1_)
		{
		}

		public EntityMinecartCommandBlock(World p_i45322_1_, double p_i45322_2_, double p_i45322_4_, double p_i45322_6_) : base(p_i45322_1_, p_i45322_2_, p_i45322_4_, p_i45322_6_)
		{
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.DataWatcher.addObject(23, "");
			this.DataWatcher.addObject(24, "");
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.field_145824_a.func_145759_b(p_70037_1_);
			this.DataWatcher.updateObject(23, this.func_145822_e().func_145753_i());
			this.DataWatcher.updateObject(24, IChatComponent.Serializer.func_150696_a(this.func_145822_e().func_145749_h()));
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			this.field_145824_a.func_145758_a(p_70014_1_);
		}

		public override int MinecartType
		{
			get
			{
				return 6;
			}
		}

		public override Block func_145817_o()
		{
			return Blocks.command_block;
		}

		public virtual CommandBlockLogic func_145822_e()
		{
			return this.field_145824_a;
		}

///    
///     <summary> * Called every tick the minecart is on an activator rail. Args: x, y, z, is the rail receiving power </summary>
///     
		public override void onActivatorRailPass(int p_96095_1_, int p_96095_2_, int p_96095_3_, bool p_96095_4_)
		{
			if (p_96095_4_ && this.ticksExisted - this.field_145823_b >= 4)
			{
				this.func_145822_e().func_145755_a(this.worldObj);
				this.field_145823_b = this.ticksExisted;
			}
		}

///    
///     <summary> * First layer of player interaction </summary>
///     
		public override bool interactFirst(EntityPlayer p_130002_1_)
		{
			if (this.worldObj.isClient)
			{
				p_130002_1_.func_146095_a(this.func_145822_e());
			}

			return base.interactFirst(p_130002_1_);
		}

		public override void func_145781_i(int p_145781_1_)
		{
			base.func_145781_i(p_145781_1_);

			if (p_145781_1_ == 24)
			{
				try
				{
					this.field_145824_a.func_145750_b(IChatComponent.Serializer.func_150699_a(this.DataWatcher.getWatchableObjectString(24)));
				}
				catch (Exception var3)
				{
					;
				}
			}
			else if (p_145781_1_ == 23)
			{
				this.field_145824_a.func_145752_a(this.DataWatcher.getWatchableObjectString(23));
			}
		}
	}

}