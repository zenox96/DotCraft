namespace DotCraftCore.TileEntity
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using CommandBlockLogic = DotCraftCore.command.server.CommandBlockLogic;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using Packet = DotCraftCore.network.Packet;
	using S35PacketUpdateTileEntity = DotCraftCore.network.play.server.S35PacketUpdateTileEntity;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using World = DotCraftCore.World.World;

	public class TileEntityCommandBlock : TileEntity
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private final CommandBlockLogic field_145994_a = new CommandBlockLogic()
//	{
//		
//		public ChunkCoordinates getPlayerCoordinates()
//		{
//			return new ChunkCoordinates(TileEntityCommandBlock.field_145851_c, TileEntityCommandBlock.field_145848_d, TileEntityCommandBlock.field_145849_e);
//		}
//		public World getEntityWorld()
//		{
//			return TileEntityCommandBlock.getWorldObj();
//		}
//		public void func_145752_a(String p_145752_1_)
//		{
//			base.func_145752_a(p_145752_1_);
//			TileEntityCommandBlock.onInventoryChanged();
//		}
//		public void func_145756_e()
//		{
//			TileEntityCommandBlock.getWorldObj().func_147471_g(TileEntityCommandBlock.field_145851_c, TileEntityCommandBlock.field_145848_d, TileEntityCommandBlock.field_145849_e);
//		}
//		public int func_145751_f()
//		{
//			return 0;
//		}
//		public void func_145757_a(ByteBuf p_145757_1_)
//		{
//			p_145757_1_.writeInt(TileEntityCommandBlock.field_145851_c);
//			p_145757_1_.writeInt(TileEntityCommandBlock.field_145848_d);
//			p_145757_1_.writeInt(TileEntityCommandBlock.field_145849_e);
//		}
//	};
		

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			this.field_145994_a.func_145758_a(p_145841_1_);
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			this.field_145994_a.func_145759_b(p_145839_1_);
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
				return new S35PacketUpdateTileEntity(this.field_145851_c, this.field_145848_d, this.field_145849_e, 2, var1);
			}
		}

		public virtual CommandBlockLogic func_145993_a()
		{
			return this.field_145994_a;
		}
	}

}