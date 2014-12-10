using System;
using System.Collections;

namespace DotCraftCore.World.Storage
{

	using CompressedStreamTools = DotCraftCore.nbt.CompressedStreamTools;
	using NBTBase = DotCraftCore.nbt.NBTBase;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagShort = DotCraftCore.nbt.NBTTagShort;
	using WorldSavedData = DotCraftCore.World.WorldSavedData;

	public class MapStorage
	{
		private ISaveHandler saveHandler;

	/// <summary> Map of item data String id to loaded MapDataBases  </summary>
		private IDictionary loadedDataMap = new Hashtable();

	/// <summary> List of loaded MapDataBases.  </summary>
		private IList loadedDataList = new ArrayList();

///    
///     <summary> * Map of MapDataBase id String prefixes ('map' etc) to max known unique Short id (the 0 part etc) for that prefix </summary>
///     
		private IDictionary idCounts = new Hashtable();
		private const string __OBFID = "CL_00000604";

		public MapStorage(ISaveHandler p_i2162_1_)
		{
			this.saveHandler = p_i2162_1_;
			this.loadIdCounts();
		}

///    
///     <summary> * Loads an existing MapDataBase corresponding to the given String id from disk, instantiating the given Class, or
///     * returns null if none such file exists. args: Class to instantiate, String dataid </summary>
///     
		public virtual WorldSavedData loadData(Type p_75742_1_, string p_75742_2_)
		{
			WorldSavedData var3 = (WorldSavedData)this.loadedDataMap.get(p_75742_2_);

			if (var3 != null)
			{
				return var3;
			}
			else
			{
				if (this.saveHandler != null)
				{
					try
					{
						File var4 = this.saveHandler.getMapFileFromName(p_75742_2_);

						if (var4 != null && var4.exists())
						{
							try
							{
								var3 = (WorldSavedData)p_75742_1_.GetConstructor(new Class[] {typeof(string)}).newInstance(new object[] {p_75742_2_});
							}
							catch (Exception var7)
							{
								throw new Exception("Failed to instantiate " + p_75742_1_.ToString(), var7);
							}

							FileInputStream var5 = new FileInputStream(var4);
							NBTTagCompound var6 = CompressedStreamTools.readCompressed(var5);
							var5.close();
							var3.readFromNBT(var6.getCompoundTag("data"));
						}
					}
					catch (Exception var8)
					{
						var8.printStackTrace();
					}
				}

				if (var3 != null)
				{
					this.loadedDataMap.Add(p_75742_2_, var3);
					this.loadedDataList.Add(var3);
				}

				return var3;
			}
		}

///    
///     <summary> * Assigns the given String id to the given MapDataBase, removing any existing ones of the same id. </summary>
///     
		public virtual void setData(string p_75745_1_, WorldSavedData p_75745_2_)
		{
			if (p_75745_2_ == null)
			{
				throw new Exception("Can\'t set null data");
			}
			else
			{
				if (this.loadedDataMap.ContainsKey(p_75745_1_))
				{
					this.loadedDataList.Remove(this.loadedDataMap.Remove(p_75745_1_));
				}

				this.loadedDataMap.Add(p_75745_1_, p_75745_2_);
				this.loadedDataList.Add(p_75745_2_);
			}
		}

///    
///     <summary> * Saves all dirty loaded MapDataBases to disk. </summary>
///     
		public virtual void saveAllData()
		{
			for (int var1 = 0; var1 < this.loadedDataList.Count; ++var1)
			{
				WorldSavedData var2 = (WorldSavedData)this.loadedDataList.get(var1);

				if (var2.Dirty)
				{
					this.saveData(var2);
					var2.Dirty = false;
				}
			}
		}

///    
///     <summary> * Saves the given MapDataBase to disk. </summary>
///     
		private void saveData(WorldSavedData p_75747_1_)
		{
			if (this.saveHandler != null)
			{
				try
				{
					File var2 = this.saveHandler.getMapFileFromName(p_75747_1_.mapName);

					if (var2 != null)
					{
						NBTTagCompound var3 = new NBTTagCompound();
						p_75747_1_.writeToNBT(var3);
						NBTTagCompound var4 = new NBTTagCompound();
						var4.setTag("data", var3);
						FileOutputStream var5 = new FileOutputStream(var2);
						CompressedStreamTools.writeCompressed(var4, var5);
						var5.close();
					}
				}
				catch (Exception var6)
				{
					var6.printStackTrace();
				}
			}
		}

///    
///     <summary> * Loads the idCounts Map from the 'idcounts' file. </summary>
///     
		private void loadIdCounts()
		{
			try
			{
				this.idCounts.Clear();

				if (this.saveHandler == null)
				{
					return;
				}

				File var1 = this.saveHandler.getMapFileFromName("idcounts");

				if (var1 != null && var1.exists())
				{
					DataInputStream var2 = new DataInputStream(new FileInputStream(var1));
					NBTTagCompound var3 = CompressedStreamTools.read(var2);
					var2.close();
					IEnumerator var4 = var3.func_150296_c().GetEnumerator();

					while (var4.MoveNext())
					{
						string var5 = (string)var4.Current;
						NBTBase var6 = var3.getTag(var5);

						if (var6 is NBTTagShort)
						{
							NBTTagShort var7 = (NBTTagShort)var6;
							short var9 = var7.func_150289_e();
							this.idCounts.Add(var5, Convert.ToInt16(var9));
						}
					}
				}
			}
			catch (Exception var10)
			{
				var10.printStackTrace();
			}
		}

///    
///     <summary> * Returns an unique new data id for the given prefix and saves the idCounts map to the 'idcounts' file. </summary>
///     
		public virtual int getUniqueDataId(string p_75743_1_)
		{
			short? var2 = (short?)this.idCounts.get(p_75743_1_);

			if (var2 == null)
			{
				var2 = Convert.ToInt16((short)0);
			}
			else
			{
				var2 = Convert.ToInt16((short)((short)var2 + 1));
			}

			this.idCounts.Add(p_75743_1_, var2);

			if (this.saveHandler == null)
			{
				return (short)var2;
			}
			else
			{
				try
				{
					File var3 = this.saveHandler.getMapFileFromName("idcounts");

					if (var3 != null)
					{
						NBTTagCompound var4 = new NBTTagCompound();
						IEnumerator var5 = this.idCounts.Keys.GetEnumerator();

						while (var5.MoveNext())
						{
							string var6 = (string)var5.Current;
							short var7 = (short)((short?)this.idCounts.get(var6));
							var4.setShort(var6, var7);
						}

						DataOutputStream var9 = new DataOutputStream(new FileOutputStream(var3));
						CompressedStreamTools.write(var4, var9);
						var9.close();
					}
				}
				catch (Exception var8)
				{
					var8.printStackTrace();
				}

				return (short)var2;
			}
		}
	}

}