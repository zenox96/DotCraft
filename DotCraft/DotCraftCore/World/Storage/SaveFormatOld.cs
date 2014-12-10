using System;
using System.Collections;
using System.Threading;

namespace DotCraftCore.World.Storage
{

	using AnvilConverterException = DotCraftCore.client.AnvilConverterException;
	using CompressedStreamTools = DotCraftCore.nbt.CompressedStreamTools;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class SaveFormatOld : ISaveFormat
	{
		private static readonly Logger logger = LogManager.Logger;

///    
///     <summary> * Reference to the File object representing the directory for the world saves </summary>
///     
		protected internal readonly File savesDirectory;
		private const string __OBFID = "CL_00000586";

		public SaveFormatOld(File p_i2147_1_)
		{
			if (!p_i2147_1_.exists())
			{
				p_i2147_1_.mkdirs();
			}

			this.savesDirectory = p_i2147_1_;
		}

		public virtual string func_154333_a()
		{
			return "Old Format";
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public List getSaveList() throws AnvilConverterException
		public virtual IList SaveList
		{
			get
			{
				ArrayList var1 = new ArrayList();
	
				for (int var2 = 0; var2 < 5; ++var2)
				{
					string var3 = "World" + (var2 + 1);
					WorldInfo var4 = this.getWorldInfo(var3);
	
					if (var4 != null)
					{
						var1.Add(new SaveFormatComparator(var3, "", var4.LastTimePlayed, var4.SizeOnDisk, var4.GameType, false, var4.HardcoreModeEnabled, var4.areCommandsAllowed()));
					}
				}
	
				return var1;
			}
		}

		public virtual void flushCache()
		{
		}

///    
///     <summary> * gets the world info </summary>
///     
		public virtual WorldInfo getWorldInfo(string p_75803_1_)
		{
			File var2 = new File(this.savesDirectory, p_75803_1_);

			if (!var2.exists())
			{
				return null;
			}
			else
			{
				File var3 = new File(var2, "level.dat");
				NBTTagCompound var4;
				NBTTagCompound var5;

				if (var3.exists())
				{
					try
					{
						var4 = CompressedStreamTools.readCompressed(new FileInputStream(var3));
						var5 = var4.getCompoundTag("Data");
						return new WorldInfo(var5);
					}
					catch (Exception var7)
					{
						logger.error("Exception reading " + var3, var7);
					}
				}

				var3 = new File(var2, "level.dat_old");

				if (var3.exists())
				{
					try
					{
						var4 = CompressedStreamTools.readCompressed(new FileInputStream(var3));
						var5 = var4.getCompoundTag("Data");
						return new WorldInfo(var5);
					}
					catch (Exception var6)
					{
						logger.error("Exception reading " + var3, var6);
					}
				}

				return null;
			}
		}

///    
///     <summary> * @args: Takes two arguments - first the name of the directory containing the world and second the new name for
///     * that world. @desc: Renames the world by storing the new name in level.dat. It does *not* rename the directory
///     * containing the world data. </summary>
///     
		public virtual void renameWorld(string p_75806_1_, string p_75806_2_)
		{
			File var3 = new File(this.savesDirectory, p_75806_1_);

			if (var3.exists())
			{
				File var4 = new File(var3, "level.dat");

				if (var4.exists())
				{
					try
					{
						NBTTagCompound var5 = CompressedStreamTools.readCompressed(new FileInputStream(var4));
						NBTTagCompound var6 = var5.getCompoundTag("Data");
						var6.setString("LevelName", p_75806_2_);
						CompressedStreamTools.writeCompressed(var5, new FileOutputStream(var4));
					}
					catch (Exception var7)
					{
						var7.printStackTrace();
					}
				}
			}
		}

		public virtual bool func_154335_d(string p_154335_1_)
		{
			File var2 = new File(this.savesDirectory, p_154335_1_);

			if (var2.exists())
			{
				return false;
			}
			else
			{
				try
				{
					var2.mkdir();
					var2.delete();
					return true;
				}
				catch (Exception var4)
				{
					logger.warn("Couldn\'t make new level", var4);
					return false;
				}
			}
		}

///    
///     <summary> * @args: Takes one argument - the name of the directory of the world to delete. @desc: Delete the world by deleting
///     * the associated directory recursively. </summary>
///     
		public virtual bool deleteWorldDirectory(string p_75802_1_)
		{
			File var2 = new File(this.savesDirectory, p_75802_1_);

			if (!var2.exists())
			{
				return true;
			}
			else
			{
				logger.info("Deleting level " + p_75802_1_);

				for (int var3 = 1; var3 <= 5; ++var3)
				{
					logger.info("Attempt " + var3 + "...");

					if (deleteFiles(var2.listFiles()))
					{
						break;
					}

					logger.warn("Unsuccessful in deleting contents.");

					if (var3 < 5)
					{
						try
						{
							Thread.Sleep(500L);
						}
						catch (InterruptedException var5)
						{
							;
						}
					}
				}

				return var2.delete();
			}
		}

///    
///     <summary> * @args: Takes one argument - the list of files and directories to delete. @desc: Deletes the files and directory
///     * listed in the list recursively. </summary>
///     
		protected internal static bool deleteFiles(File[] p_75807_0_)
		{
			for (int var1 = 0; var1 < p_75807_0_.Length; ++var1)
			{
				File var2 = p_75807_0_[var1];
				logger.debug("Deleting " + var2);

				if (var2.Directory && !deleteFiles(var2.listFiles()))
				{
					logger.warn("Couldn\'t delete directory " + var2);
					return false;
				}

				if (!var2.delete())
				{
					logger.warn("Couldn\'t delete file " + var2);
					return false;
				}
			}

			return true;
		}

///    
///     <summary> * Returns back a loader for the specified save directory </summary>
///     
		public virtual ISaveHandler getSaveLoader(string p_75804_1_, bool p_75804_2_)
		{
			return new SaveHandler(this.savesDirectory, p_75804_1_, p_75804_2_);
		}

		public virtual bool func_154334_a(string p_154334_1_)
		{
			return false;
		}

///    
///     <summary> * Checks if the save directory uses the old map format </summary>
///     
		public virtual bool isOldMapFormat(string p_75801_1_)
		{
			return false;
		}

///    
///     <summary> * Converts the specified map to the new map format. Args: worldName, loadingScreen </summary>
///     
		public virtual bool convertMapFormat(string p_75805_1_, IProgressUpdate p_75805_2_)
		{
			return false;
		}

///    
///     <summary> * Return whether the given world can be loaded. </summary>
///     
		public virtual bool canLoadWorld(string p_90033_1_)
		{
			File var2 = new File(this.savesDirectory, p_90033_1_);
			return var2.Directory;
		}
	}

}