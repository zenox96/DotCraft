namespace DotCraftCore.nEntity.nAI
{

	using Block = DotCraftCore.nBlock.Block;
	using EntityMinecart = DotCraftCore.nEntity.nItem.EntityMinecart;
	using Blocks = DotCraftCore.nInit.Blocks;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using MobSpawnerBaseLogic = DotCraftCore.nTileEntity.MobSpawnerBaseLogic;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class EntityMinecartMobSpawner : EntityMinecart
	{
	/// <summary> Mob spawner logic for this spawner minecart.  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private final MobSpawnerBaseLogic mobSpawnerLogic = new MobSpawnerBaseLogic()
//	{
//		
//		public void func_98267_a(int p_98267_1_)
//		{
//			EntityMinecartMobSpawner.worldObj.setEntityState(EntityMinecartMobSpawner.this, (byte)p_98267_1_);
//		}
//		public World getSpawnerWorld()
//		{
//			return EntityMinecartMobSpawner.worldObj;
//		}
//		public int getSpawnerX()
//		{
//			return MathHelper.floor_double(EntityMinecartMobSpawner.posX);
//		}
//		public int getSpawnerY()
//		{
//			return MathHelper.floor_double(EntityMinecartMobSpawner.posY);
//		}
//		public int getSpawnerZ()
//		{
//			return MathHelper.floor_double(EntityMinecartMobSpawner.posZ);
//		}
//	};
		

		public EntityMinecartMobSpawner(World p_i1725_1_) : base(p_i1725_1_)
		{
		}

		public EntityMinecartMobSpawner(World p_i1726_1_, double p_i1726_2_, double p_i1726_4_, double p_i1726_6_) : base(p_i1726_1_, p_i1726_2_, p_i1726_4_, p_i1726_6_)
		{
		}

		public override int MinecartType
		{
			get
			{
				return 4;
			}
		}

		public override Block func_145817_o()
		{
			return Blocks.mob_spawner;
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.mobSpawnerLogic.readFromNBT(p_70037_1_);
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			this.mobSpawnerLogic.writeToNBT(p_70014_1_);
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			this.mobSpawnerLogic.DelayToMin = p_70103_1_;
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();
			this.mobSpawnerLogic.updateSpawner();
		}

		public virtual MobSpawnerBaseLogic func_98039_d()
		{
			return this.mobSpawnerLogic;
		}
	}

}