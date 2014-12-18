namespace DotCraftCore.nWorld
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;

	public abstract class WorldSavedData
	{
	/// <summary> The name of the map data nbt  </summary>
		public readonly string mapName;

	/// <summary> Whether this MapDataBase needs saving to disk.  </summary>
		private bool dirty;
		

		public WorldSavedData(string p_i2141_1_)
		{
			this.mapName = p_i2141_1_;
		}

///    
///     <summary> * reads in data from the NBTTagCompound into this MapDataBase </summary>
///     
		public abstract void readFromNBT(NBTTagCompound p_76184_1_);

///    
///     <summary> * write data to NBTTagCompound from this MapDataBase, similar to Entities and TileEntities </summary>
///     
		public abstract void writeToNBT(NBTTagCompound p_76187_1_);

///    
///     <summary> * Marks this MapDataBase dirty, to be saved to disk when the level next saves. </summary>
///     
		public virtual void markDirty()
		{
			this.Dirty = true;
		}

///    
///     <summary> * Sets the dirty state of this MapDataBase, whether it needs saving to disk. </summary>
///     
		public virtual bool Dirty
		{
			set
			{
				this.dirty = value;
			}
			get
			{
				return this.dirty;
			}
		}

///    
///     <summary> * Whether this MapDataBase needs saving to disk. </summary>
///     
	}

}