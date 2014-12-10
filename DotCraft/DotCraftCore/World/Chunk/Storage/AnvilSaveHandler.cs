namespace DotCraftCore.World.Chunk.Storage
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using WorldProvider = DotCraftCore.World.WorldProvider;
	using WorldProviderEnd = DotCraftCore.World.WorldProviderEnd;
	using WorldProviderHell = DotCraftCore.World.WorldProviderHell;
	using SaveHandler = DotCraftCore.World.Storage.SaveHandler;
	using ThreadedFileIOBase = DotCraftCore.World.Storage.ThreadedFileIOBase;
	using WorldInfo = DotCraftCore.World.Storage.WorldInfo;

	public class AnvilSaveHandler : SaveHandler
	{
		private const string __OBFID = "CL_00000581";

		public AnvilSaveHandler(File p_i2142_1_, string p_i2142_2_, bool p_i2142_3_) : base(p_i2142_1_, p_i2142_2_, p_i2142_3_)
		{
		}

///    
///     <summary> * Returns the chunk loader with the provided world provider </summary>
///     
		public override IChunkLoader getChunkLoader(WorldProvider p_75763_1_)
		{
			File var2 = this.WorldDirectory;
			File var3;

			if (p_75763_1_ is WorldProviderHell)
			{
				var3 = new File(var2, "DIM-1");
				var3.mkdirs();
				return new AnvilChunkLoader(var3);
			}
			else if (p_75763_1_ is WorldProviderEnd)
			{
				var3 = new File(var2, "DIM1");
				var3.mkdirs();
				return new AnvilChunkLoader(var3);
			}
			else
			{
				return new AnvilChunkLoader(var2);
			}
		}

///    
///     <summary> * Saves the given World Info with the given NBTTagCompound as the Player. </summary>
///     
		public override void saveWorldInfoWithPlayer(WorldInfo p_75755_1_, NBTTagCompound p_75755_2_)
		{
			p_75755_1_.SaveVersion = 19133;
			base.saveWorldInfoWithPlayer(p_75755_1_, p_75755_2_);
		}

///    
///     <summary> * Called to flush all changes to disk, waiting for them to complete. </summary>
///     
		public override void flush()
		{
			try
			{
				ThreadedFileIOBase.threadedIOInstance.waitForFinish();
			}
			catch (InterruptedException var2)
			{
				var2.printStackTrace();
			}

			RegionFileCache.clearRegionFileReferences();
		}
	}

}