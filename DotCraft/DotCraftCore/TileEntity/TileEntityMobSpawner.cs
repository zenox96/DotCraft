namespace DotCraftCore.TileEntity
{

	using Blocks = DotCraftCore.init.Blocks;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using Packet = DotCraftCore.network.Packet;
	using S35PacketUpdateTileEntity = DotCraftCore.network.play.server.S35PacketUpdateTileEntity;
	using World = DotCraftCore.World.World;

	public class TileEntityMobSpawner : TileEntity
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private final MobSpawnerBaseLogic field_145882_a = new MobSpawnerBaseLogic()
//	{
//		
//		public void func_98267_a(int p_98267_1_)
//		{
//			TileEntityMobSpawner.worldObj.func_147452_c(TileEntityMobSpawner.field_145851_c, TileEntityMobSpawner.field_145848_d, TileEntityMobSpawner.field_145849_e, Blocks.mob_spawner, p_98267_1_, 0);
//		}
//		public World getSpawnerWorld()
//		{
//			return TileEntityMobSpawner.worldObj;
//		}
//		public int getSpawnerX()
//		{
//			return TileEntityMobSpawner.field_145851_c;
//		}
//		public int getSpawnerY()
//		{
//			return TileEntityMobSpawner.field_145848_d;
//		}
//		public int getSpawnerZ()
//		{
//			return TileEntityMobSpawner.field_145849_e;
//		}
//		public void setRandomMinecart(MobSpawnerBaseLogic.WeightedRandomMinecart p_98277_1_)
//		{
//			base.setRandomMinecart(p_98277_1_);
//
//			if (this.getSpawnerWorld() != null)
//			{
//				this.getSpawnerWorld().func_147471_g(TileEntityMobSpawner.field_145851_c, TileEntityMobSpawner.field_145848_d, TileEntityMobSpawner.field_145849_e);
//			}
//		}
//	};
		

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			this.field_145882_a.readFromNBT(p_145839_1_);
		}

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			this.field_145882_a.writeToNBT(p_145841_1_);
		}

		public override void updateEntity()
		{
			this.field_145882_a.updateSpawner();
			base.updateEntity();
		}

///    
///     <summary> * Overriden in a sign to provide the text. </summary>
///     
		public override Packet DescriptionPacket
		{
			get
			{
				NBTTagCompound var1 = new NBTTagCompound();
				this.writeToNBT(var1);
				var1.removeTag("SpawnPotentials");
				return new S35PacketUpdateTileEntity(this.field_145851_c, this.field_145848_d, this.field_145849_e, 1, var1);
			}
		}

		public override bool receiveClientEvent(int p_145842_1_, int p_145842_2_)
		{
			return this.field_145882_a.setDelayToMin(p_145842_1_) ? true : base.receiveClientEvent(p_145842_1_, p_145842_2_);
		}

		public virtual MobSpawnerBaseLogic func_145881_a()
		{
			return this.field_145882_a;
		}
	}

}