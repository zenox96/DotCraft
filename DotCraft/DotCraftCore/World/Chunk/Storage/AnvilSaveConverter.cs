using System;
using System.Collections;

namespace DotCraftCore.World.Chunk.Storage
{

	using AnvilConverterException = DotCraftCore.client.AnvilConverterException;
	using CompressedStreamTools = DotCraftCore.nbt.CompressedStreamTools;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using WorldType = DotCraftCore.World.WorldType;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;
	using WorldChunkManager = DotCraftCore.World.Biome.WorldChunkManager;
	using WorldChunkManagerHell = DotCraftCore.World.Biome.WorldChunkManagerHell;
	using ISaveHandler = DotCraftCore.World.Storage.ISaveHandler;
	using SaveFormatComparator = DotCraftCore.World.Storage.SaveFormatComparator;
	using SaveFormatOld = DotCraftCore.World.Storage.SaveFormatOld;
	using WorldInfo = DotCraftCore.World.Storage.WorldInfo;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class AnvilSaveConverter : SaveFormatOld
	{
		private static readonly Logger logger = LogManager.Logger;
		

		public AnvilSaveConverter(File p_i2144_1_) : base(p_i2144_1_)
		{
		}

		public override string func_154333_a()
		{
			return "Anvil";
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public List getSaveList() throws AnvilConverterException
		public override IList SaveList
		{
			get
			{
				if (this.savesDirectory != null && this.savesDirectory.exists() && this.savesDirectory.Directory)
				{
					ArrayList var1 = new ArrayList();
					File[] var2 = this.savesDirectory.listFiles();
					File[] var3 = var2;
					int var4 = var2.Length;
	
					for (int var5 = 0; var5 < var4; ++var5)
					{
						File var6 = var3[var5];
	
						if (var6.Directory)
						{
							string var7 = var6.Name;
							WorldInfo var8 = this.getWorldInfo(var7);
	
							if (var8 != null && (var8.SaveVersion == 19132 || var8.SaveVersion == 19133))
							{
								bool var9 = var8.SaveVersion != this.SaveVersion;
								string var10 = var8.WorldName;
	
								if (var10 == null || MathHelper.stringNullOrLengthZero(var10))
								{
									var10 = var7;
								}
	
								long var11 = 0L;
								var1.Add(new SaveFormatComparator(var7, var10, var8.LastTimePlayed, var11, var8.GameType, var9, var8.HardcoreModeEnabled, var8.areCommandsAllowed()));
							}
						}
					}
	
					return var1;
				}
				else
				{
					throw new AnvilConverterException("Unable to read or access folder where game worlds are saved!");
				}
			}
		}

		protected internal virtual int SaveVersion
		{
			get
			{
				return 19133;
			}
		}

		public override void flushCache()
		{
			RegionFileCache.clearRegionFileReferences();
		}

///    
///     <summary> * Returns back a loader for the specified save directory </summary>
///     
		public override ISaveHandler getSaveLoader(string p_75804_1_, bool p_75804_2_)
		{
			return new AnvilSaveHandler(this.savesDirectory, p_75804_1_, p_75804_2_);
		}

		public override bool func_154334_a(string p_154334_1_)
		{
			WorldInfo var2 = this.getWorldInfo(p_154334_1_);
			return var2 != null && var2.SaveVersion == 19132;
		}

///    
///     <summary> * Checks if the save directory uses the old map format </summary>
///     
		public override bool isOldMapFormat(string p_75801_1_)
		{
			WorldInfo var2 = this.getWorldInfo(p_75801_1_);
			return var2 != null && var2.SaveVersion != this.SaveVersion;
		}

///    
///     <summary> * Converts the specified map to the new map format. Args: worldName, loadingScreen </summary>
///     
		public override bool convertMapFormat(string p_75805_1_, IProgressUpdate p_75805_2_)
		{
			p_75805_2_.LoadingProgress = 0;
			ArrayList var3 = new ArrayList();
			ArrayList var4 = new ArrayList();
			ArrayList var5 = new ArrayList();
			File var6 = new File(this.savesDirectory, p_75805_1_);
			File var7 = new File(var6, "DIM-1");
			File var8 = new File(var6, "DIM1");
			logger.info("Scanning folders...");
			this.addRegionFilesToCollection(var6, var3);

			if (var7.exists())
			{
				this.addRegionFilesToCollection(var7, var4);
			}

			if (var8.exists())
			{
				this.addRegionFilesToCollection(var8, var5);
			}

			int var9 = var3.Count + var4.Count + var5.Count;
			logger.info("Total conversion count is " + var9);
			WorldInfo var10 = this.getWorldInfo(p_75805_1_);
			object var11 = null;

			if (var10.TerrainType == WorldType.FLAT)
			{
				var11 = new WorldChunkManagerHell(BiomeGenBase.plains, 0.5F);
			}
			else
			{
				var11 = new WorldChunkManager(var10.Seed, var10.TerrainType);
			}

			this.convertFile(new File(var6, "region"), var3, (WorldChunkManager)var11, 0, var9, p_75805_2_);
			this.convertFile(new File(var7, "region"), var4, new WorldChunkManagerHell(BiomeGenBase.hell, 0.0F), var3.Count, var9, p_75805_2_);
			this.convertFile(new File(var8, "region"), var5, new WorldChunkManagerHell(BiomeGenBase.sky, 0.0F), var3.Count + var4.Count, var9, p_75805_2_);
			var10.SaveVersion = 19133;

			if (var10.TerrainType == WorldType.DEFAULT_1_1)
			{
				var10.TerrainType = WorldType.DEFAULT;
			}

			this.createFile(p_75805_1_);
			ISaveHandler var12 = this.getSaveLoader(p_75805_1_, false);
			var12.saveWorldInfo(var10);
			return true;
		}

///    
///     <summary> * par: filename for the level.dat_mcr backup </summary>
///     
		private void createFile(string p_75809_1_)
		{
			File var2 = new File(this.savesDirectory, p_75809_1_);

			if (!var2.exists())
			{
				logger.warn("Unable to create level.dat_mcr backup");
			}
			else
			{
				File var3 = new File(var2, "level.dat");

				if (!var3.exists())
				{
					logger.warn("Unable to create level.dat_mcr backup");
				}
				else
				{
					File var4 = new File(var2, "level.dat_mcr");

					if (!var3.renameTo(var4))
					{
						logger.warn("Unable to create level.dat_mcr backup");
					}
				}
			}
		}

		private void convertFile(File p_75813_1_, IEnumerable p_75813_2_, WorldChunkManager p_75813_3_, int p_75813_4_, int p_75813_5_, IProgressUpdate p_75813_6_)
		{
			IEnumerator var7 = p_75813_2_.GetEnumerator();

			while (var7.MoveNext())
			{
				File var8 = (File)var7.Current;
				this.convertChunks(p_75813_1_, var8, p_75813_3_, p_75813_4_, p_75813_5_, p_75813_6_);
				++p_75813_4_;
				int var9 = (int)Math.Round(100.0D * (double)p_75813_4_ / (double)p_75813_5_);
				p_75813_6_.LoadingProgress = var9;
			}
		}

///    
///     <summary> * copies a 32x32 chunk set from par2File to par1File, via AnvilConverterData </summary>
///     
		private void convertChunks(File p_75811_1_, File p_75811_2_, WorldChunkManager p_75811_3_, int p_75811_4_, int p_75811_5_, IProgressUpdate p_75811_6_)
		{
			try
			{
				string var7 = p_75811_2_.Name;
				RegionFile var8 = new RegionFile(p_75811_2_);
				RegionFile var9 = new RegionFile(new File(p_75811_1_, var7.Substring(0, var7.Length - ".mcr".Length) + ".mca"));

				for (int var10 = 0; var10 < 32; ++var10)
				{
					int var11;

					for (var11 = 0; var11 < 32; ++var11)
					{
						if (var8.isChunkSaved(var10, var11) && !var9.isChunkSaved(var10, var11))
						{
							DataInputStream var12 = var8.getChunkDataInputStream(var10, var11);

							if (var12 == null)
							{
								logger.warn("Failed to fetch input stream");
							}
							else
							{
								NBTTagCompound var13 = CompressedStreamTools.read(var12);
								var12.close();
								NBTTagCompound var14 = var13.getCompoundTag("Level");
								ChunkLoader.AnvilConverterData var15 = ChunkLoader.load(var14);
								NBTTagCompound var16 = new NBTTagCompound();
								NBTTagCompound var17 = new NBTTagCompound();
								var16.setTag("Level", var17);
								ChunkLoader.convertToAnvilFormat(var15, var17, p_75811_3_);
								DataOutputStream var18 = var9.getChunkDataOutputStream(var10, var11);
								CompressedStreamTools.write(var16, var18);
								var18.close();
							}
						}
					}

					var11 = (int)Math.Round(100.0D * (double)(p_75811_4_ * 1024) / (double)(p_75811_5_ * 1024));
					int var20 = (int)Math.Round(100.0D * (double)((var10 + 1) * 32 + p_75811_4_ * 1024) / (double)(p_75811_5_ * 1024));

					if (var20 > var11)
					{
						p_75811_6_.LoadingProgress = var20;
					}
				}

				var8.close();
				var9.close();
			}
			catch (IOException var19)
			{
				var19.printStackTrace();
			}
		}

///    
///     <summary> * filters the files in the par1 directory, and adds them to the par2 collections </summary>
///     
		private void addRegionFilesToCollection(File p_75810_1_, ICollection p_75810_2_)
		{
			File var3 = new File(p_75810_1_, "region");
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: File[] var4 = var3.listFiles(new FilenameFilter() {  public boolean accept(File p_accept_1_, String p_accept_2_) { return p_accept_2_.endsWith(".mcr"); } });
			File[] var4 = var3.listFiles(new FilenameFilter() {  public bool accept(File p_accept_1_, string p_accept_2_) { return p_accept_2_.EndsWith(".mcr"); } });

			if (var4 != null)
			{
				Collections.addAll(p_75810_2_, var4);
			}
		}
	}

}