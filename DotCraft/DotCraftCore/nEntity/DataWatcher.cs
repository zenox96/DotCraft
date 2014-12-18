using System;
using System.Collections;

namespace DotCraftCore.nEntity
{

	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using ChunkCoordinates = DotCraftCore.nUtil.ChunkCoordinates;
	using ReportedException = DotCraftCore.nUtil.ReportedException;
	using ObjectUtils = org.apache.commons.lang3.ObjectUtils;

	public class DataWatcher
	{
		private readonly Entity field_151511_a;

	/// <summary> When isBlank is true the DataWatcher is not watching any objects  </summary>
		private bool isBlank = true;
		private static readonly Hashtable dataTypes = new Hashtable();
		private readonly IDictionary watchedObjects = new Hashtable();

	/// <summary> true if one or more object was changed  </summary>
		private bool objectChanged;
		private ReadWriteLock @lock = new ReentrantReadWriteLock();
		

		public DataWatcher(Entity p_i45313_1_)
		{
			this.field_151511_a = p_i45313_1_;
		}

///    
///     <summary> * adds a new object to dataWatcher to watch, to update an already existing object see updateObject. Arguments: data
///     * Value Id, Object to add </summary>
///     
		public virtual void addObject(int p_75682_1_, object p_75682_2_)
		{
			int? var3 = (int?)dataTypes[p_75682_2_.GetType()];

			if (var3 == null)
			{
				throw new System.ArgumentException("Unknown data type: " + p_75682_2_.GetType());
			}
			else if (p_75682_1_ > 31)
			{
				throw new System.ArgumentException("Data value id is too big with " + p_75682_1_ + "! (Max is " + 31 + ")");
			}
			else if (this.watchedObjects.ContainsKey(Convert.ToInt32(p_75682_1_)))
			{
				throw new System.ArgumentException("Duplicate id value for " + p_75682_1_ + "!");
			}
			else
			{
				DataWatcher.WatchableObject var4 = new DataWatcher.WatchableObject((int)var3, p_75682_1_, p_75682_2_);
				this.@lock.writeLock().lock();
				this.watchedObjects.Add(Convert.ToInt32(p_75682_1_), var4);
				this.@lock.writeLock().unlock();
				this.isBlank = false;
			}
		}

///    
///     <summary> * Add a new object for the DataWatcher to watch, using the specified data type. </summary>
///     
		public virtual void addObjectByDataType(int p_82709_1_, int p_82709_2_)
		{
			DataWatcher.WatchableObject var3 = new DataWatcher.WatchableObject(p_82709_2_, p_82709_1_, (object)null);
			this.@lock.writeLock().lock();
			this.watchedObjects.Add(Convert.ToInt32(p_82709_1_), var3);
			this.@lock.writeLock().unlock();
			this.isBlank = false;
		}

///    
///     <summary> * gets the bytevalue of a watchable object </summary>
///     
		public virtual sbyte getWatchableObjectByte(int p_75683_1_)
		{
			return (sbyte)((sbyte?)this.getWatchedObject(p_75683_1_).Object);
		}

		public virtual short getWatchableObjectShort(int p_75693_1_)
		{
			return (short)((short?)this.getWatchedObject(p_75693_1_).Object);
		}

///    
///     <summary> * gets a watchable object and returns it as a Integer </summary>
///     
		public virtual int getWatchableObjectInt(int p_75679_1_)
		{
			return (int)((int?)this.getWatchedObject(p_75679_1_).Object);
		}

		public virtual float getWatchableObjectFloat(int p_111145_1_)
		{
			return (float)((float?)this.getWatchedObject(p_111145_1_).Object);
		}

///    
///     <summary> * gets a watchable object and returns it as a String </summary>
///     
		public virtual string getWatchableObjectString(int p_75681_1_)
		{
			return (string)this.getWatchedObject(p_75681_1_).Object;
		}

///    
///     <summary> * Get a watchable object as an ItemStack. </summary>
///     
		public virtual ItemStack getWatchableObjectItemStack(int p_82710_1_)
		{
			return (ItemStack)this.getWatchedObject(p_82710_1_).Object;
		}

///    
///     <summary> * is threadsafe, unless it throws an exception, then </summary>
///     
		private DataWatcher.WatchableObject getWatchedObject(int p_75691_1_)
		{
			this.@lock.readLock().lock();
			DataWatcher.WatchableObject var2;

			try
			{
				var2 = (DataWatcher.WatchableObject)this.watchedObjects.get(Convert.ToInt32(p_75691_1_));
			}
			catch (Exception var6)
			{
				CrashReport var4 = CrashReport.makeCrashReport(var6, "Getting synched entity data");
				CrashReportCategory var5 = var4.makeCategory("Synched entity data");
				var5.addCrashSection("Data ID", Convert.ToInt32(p_75691_1_));
				throw new ReportedException(var4);
			}

			this.@lock.readLock().unlock();
			return var2;
		}

///    
///     <summary> * updates an already existing object </summary>
///     
		public virtual void updateObject(int p_75692_1_, object p_75692_2_)
		{
			DataWatcher.WatchableObject var3 = this.getWatchedObject(p_75692_1_);

			if (ObjectUtils.notEqual(p_75692_2_, var3.Object))
			{
				var3.Object = p_75692_2_;
				this.field_151511_a.func_145781_i(p_75692_1_);
				var3.Watched = true;
				this.objectChanged = true;
			}
		}

		public virtual int ObjectWatched
		{
			set
			{
				this.getWatchedObject(value).watched = true;
				this.objectChanged = true;
			}
		}

		public virtual bool hasChanges()
		{
			return this.objectChanged;
		}

///    
///     <summary> * Writes the list of watched objects (entity attribute of type {byte, short, int, float, string, ItemStack,
///     * ChunkCoordinates}) to the specified PacketBuffer </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void writeWatchedListToPacketBuffer(List p_151507_0_, PacketBuffer p_151507_1_) throws IOException
		public static void writeWatchedListToPacketBuffer(IList p_151507_0_, PacketBuffer p_151507_1_)
		{
			if (p_151507_0_ != null)
			{
				IEnumerator var2 = p_151507_0_.GetEnumerator();

				while (var2.MoveNext())
				{
					DataWatcher.WatchableObject var3 = (DataWatcher.WatchableObject)var2.Current;
					writeWatchableObjectToPacketBuffer(p_151507_1_, var3);
				}
			}

			p_151507_1_.writeByte(127);
		}

		public virtual IList Changed
		{
			get
			{
				ArrayList var1 = null;
	
				if (this.objectChanged)
				{
					this.@lock.readLock().lock();
					IEnumerator var2 = this.watchedObjects.Values.GetEnumerator();
	
					while (var2.MoveNext())
					{
						DataWatcher.WatchableObject var3 = (DataWatcher.WatchableObject)var2.Current;
	
						if (var3.Watched)
						{
							var3.Watched = false;
	
							if (var1 == null)
							{
								var1 = new ArrayList();
							}
	
							var1.Add(var3);
						}
					}
	
					this.@lock.readLock().unlock();
				}
	
				this.objectChanged = false;
				return var1;
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void func_151509_a(PacketBuffer p_151509_1_) throws IOException
		public virtual void func_151509_a(PacketBuffer p_151509_1_)
		{
			this.@lock.readLock().lock();
			IEnumerator var2 = this.watchedObjects.Values.GetEnumerator();

			while (var2.MoveNext())
			{
				DataWatcher.WatchableObject var3 = (DataWatcher.WatchableObject)var2.Current;
				writeWatchableObjectToPacketBuffer(p_151509_1_, var3);
			}

			this.@lock.readLock().unlock();
			p_151509_1_.writeByte(127);
		}

		public virtual IList AllWatched
		{
			get
			{
				ArrayList var1 = null;
				this.@lock.readLock().lock();
				DataWatcher.WatchableObject var3;
	
				for (IEnumerator var2 = this.watchedObjects.Values.GetEnumerator(); var2.MoveNext(); var1.Add(var3))
				{
					var3 = (DataWatcher.WatchableObject)var2.Current;
	
					if (var1 == null)
					{
						var1 = new ArrayList();
					}
				}
	
				this.@lock.readLock().unlock();
				return var1;
			}
		}

///    
///     <summary> * Writes a watchable object (entity attribute of type {byte, short, int, float, string, ItemStack,
///     * ChunkCoordinates}) to the specified PacketBuffer </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static void writeWatchableObjectToPacketBuffer(PacketBuffer p_151510_0_, DataWatcher.WatchableObject p_151510_1_) throws IOException
		private static void writeWatchableObjectToPacketBuffer(PacketBuffer p_151510_0_, DataWatcher.WatchableObject p_151510_1_)
		{
			int var2 = (p_151510_1_.ObjectType << 5 | p_151510_1_.DataValueId & 31) & 255;
			p_151510_0_.writeByte(var2);

			switch (p_151510_1_.ObjectType)
			{
				case 0:
					p_151510_0_.writeByte((sbyte)((sbyte?)p_151510_1_.Object));
					break;

				case 1:
					p_151510_0_.writeShort((short)((short?)p_151510_1_.Object));
					break;

				case 2:
					p_151510_0_.writeInt((int)((int?)p_151510_1_.Object));
					break;

				case 3:
					p_151510_0_.writeFloat((float)((float?)p_151510_1_.Object));
					break;

				case 4:
					p_151510_0_.writeStringToBuffer((string)p_151510_1_.Object);
					break;

				case 5:
					ItemStack var4 = (ItemStack)p_151510_1_.Object;
					p_151510_0_.writeItemStackToBuffer(var4);
					break;

				case 6:
					ChunkCoordinates var3 = (ChunkCoordinates)p_151510_1_.Object;
					p_151510_0_.writeInt(var3.posX);
					p_151510_0_.writeInt(var3.posY);
					p_151510_0_.writeInt(var3.posZ);
				break;
			}
		}

///    
///     <summary> * Reads a list of watched objects (entity attribute of type {byte, short, int, float, string, ItemStack,
///     * ChunkCoordinates}) from the supplied PacketBuffer </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static List readWatchedListFromPacketBuffer(PacketBuffer p_151508_0_) throws IOException
		public static IList readWatchedListFromPacketBuffer(PacketBuffer p_151508_0_)
		{
			ArrayList var1 = null;

			for (sbyte var2 = p_151508_0_.readByte(); var2 != 127; var2 = p_151508_0_.readByte())
			{
				if (var1 == null)
				{
					var1 = new ArrayList();
				}

				int var3 = (var2 & 224) >> 5;
				int var4 = var2 & 31;
				DataWatcher.WatchableObject var5 = null;

				switch (var3)
				{
					case 0:
						var5 = new DataWatcher.WatchableObject(var3, var4, Convert.ToByte(p_151508_0_.readByte()));
						break;

					case 1:
						var5 = new DataWatcher.WatchableObject(var3, var4, Convert.ToInt16(p_151508_0_.readShort()));
						break;

					case 2:
						var5 = new DataWatcher.WatchableObject(var3, var4, Convert.ToInt32(p_151508_0_.readInt()));
						break;

					case 3:
						var5 = new DataWatcher.WatchableObject(var3, var4, Convert.ToSingle(p_151508_0_.readFloat()));
						break;

					case 4:
						var5 = new DataWatcher.WatchableObject(var3, var4, p_151508_0_.readStringFromBuffer(32767));
						break;

					case 5:
						var5 = new DataWatcher.WatchableObject(var3, var4, p_151508_0_.readItemStackFromBuffer());
						break;

					case 6:
						int var6 = p_151508_0_.readInt();
						int var7 = p_151508_0_.readInt();
						int var8 = p_151508_0_.readInt();
						var5 = new DataWatcher.WatchableObject(var3, var4, new ChunkCoordinates(var6, var7, var8));
					break;
				}

				var1.Add(var5);
			}

			return var1;
		}

		public virtual void updateWatchedObjectsFromList(IList p_75687_1_)
		{
			this.@lock.writeLock().lock();
			IEnumerator var2 = p_75687_1_.GetEnumerator();

			while (var2.MoveNext())
			{
				DataWatcher.WatchableObject var3 = (DataWatcher.WatchableObject)var2.Current;
				DataWatcher.WatchableObject var4 = (DataWatcher.WatchableObject)this.watchedObjects.get(Convert.ToInt32(var3.DataValueId));

				if (var4 != null)
				{
					var4.Object = var3.Object;
					this.field_151511_a.func_145781_i(var3.DataValueId);
				}
			}

			this.@lock.writeLock().unlock();
			this.objectChanged = true;
		}

		public virtual bool IsBlank
		{
			get
			{
				return this.isBlank;
			}
		}

		public virtual void func_111144_e()
		{
			this.objectChanged = false;
		}

		static DataWatcher()
		{
			dataTypes.Add(typeof(sbyte?), Convert.ToInt32(0));
			dataTypes.Add(typeof(short?), Convert.ToInt32(1));
			dataTypes.Add(typeof(int?), Convert.ToInt32(2));
			dataTypes.Add(typeof(float?), Convert.ToInt32(3));
			dataTypes.Add(typeof(string), Convert.ToInt32(4));
			dataTypes.Add(typeof(ItemStack), Convert.ToInt32(5));
			dataTypes.Add(typeof(ChunkCoordinates), Convert.ToInt32(6));
		}

		public class WatchableObject
		{
			private readonly int objectType;
			private readonly int dataValueId;
			private object watchedObject;
			private bool watched;
			

			public WatchableObject(int p_i1603_1_, int p_i1603_2_, object p_i1603_3_)
			{
				this.dataValueId = p_i1603_2_;
				this.watchedObject = p_i1603_3_;
				this.objectType = p_i1603_1_;
				this.watched = true;
			}

			public virtual int DataValueId
			{
				get
				{
					return this.dataValueId;
				}
			}

			public virtual object Object
			{
				set
				{
					this.watchedObject = value;
				}
				get
				{
					return this.watchedObject;
				}
			}


			public virtual int ObjectType
			{
				get
				{
					return this.objectType;
				}
			}

			public virtual bool isWatched()
			{
				get
				{
					return this.watched;
				}
				set
				{
					this.watched = value;
				}
			}

		}
	}

}