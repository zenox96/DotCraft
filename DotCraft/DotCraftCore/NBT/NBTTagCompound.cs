using System;
using System.Collections;

namespace DotCraftCore.NBT
{

	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class NBTTagCompound : NBTBase
	{
		private static readonly Logger logger = LogManager.Logger;

///    
///     <summary> * The key-value pairs for the tag. Each key is a UTF string, each value is a tag. </summary>
///     
		private IDictionary tagMap = new Hashtable();
		private const string __OBFID = "CL_00001215";

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal override void write(DataOutput p_74734_1_)
		{
			IEnumerator var2 = this.tagMap.Keys.GetEnumerator();

			while (var2.MoveNext())
			{
				string var3 = (string)var2.Current;
				NBTBase var4 = (NBTBase)this.tagMap.get(var3);
				func_150298_a(var3, var4, p_74734_1_);
			}

			p_74734_1_.writeByte(0);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException
		internal override void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_)
		{
			if (p_152446_2_ > 512)
			{
				throw new Exception("Tried to read NBT tag with too high complexity, depth > 512");
			}
			else
			{
				this.tagMap.Clear();
				sbyte var4;

				while ((var4 = func_152447_a(p_152446_1_, p_152446_3_)) != 0)
				{
					string var5 = func_152448_b(p_152446_1_, p_152446_3_);
					p_152446_3_.func_152450_a((long)(16 * var5.Length));
					NBTBase var6 = func_152449_a(var4, var5, p_152446_1_, p_152446_2_ + 1, p_152446_3_);
					this.tagMap.Add(var5, var6);
				}
			}
		}

		public virtual Set func_150296_c()
		{
			return this.tagMap.Keys;
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public override sbyte Id
		{
			get
			{
				return (sbyte)10;
			}
		}

///    
///     <summary> * Stores the given tag into the map with the given string key. This is mostly used to store tag lists. </summary>
///     
		public virtual void setTag(string p_74782_1_, NBTBase p_74782_2_)
		{
			this.tagMap.Add(p_74782_1_, p_74782_2_);
		}

///    
///     <summary> * Stores a new NBTTagByte with the given byte value into the map with the given string key. </summary>
///     
		public virtual void setByte(string p_74774_1_, sbyte p_74774_2_)
		{
			this.tagMap.Add(p_74774_1_, new NBTTagByte(p_74774_2_));
		}

///    
///     <summary> * Stores a new NBTTagShort with the given short value into the map with the given string key. </summary>
///     
		public virtual void setShort(string p_74777_1_, short p_74777_2_)
		{
			this.tagMap.Add(p_74777_1_, new NBTTagShort(p_74777_2_));
		}

///    
///     <summary> * Stores a new NBTTagInt with the given integer value into the map with the given string key. </summary>
///     
		public virtual void setInteger(string p_74768_1_, int p_74768_2_)
		{
			this.tagMap.Add(p_74768_1_, new NBTTagInt(p_74768_2_));
		}

///    
///     <summary> * Stores a new NBTTagLong with the given long value into the map with the given string key. </summary>
///     
		public virtual void setLong(string p_74772_1_, long p_74772_2_)
		{
			this.tagMap.Add(p_74772_1_, new NBTTagLong(p_74772_2_));
		}

///    
///     <summary> * Stores a new NBTTagFloat with the given float value into the map with the given string key. </summary>
///     
		public virtual void setFloat(string p_74776_1_, float p_74776_2_)
		{
			this.tagMap.Add(p_74776_1_, new NBTTagFloat(p_74776_2_));
		}

///    
///     <summary> * Stores a new NBTTagDouble with the given double value into the map with the given string key. </summary>
///     
		public virtual void setDouble(string p_74780_1_, double p_74780_2_)
		{
			this.tagMap.Add(p_74780_1_, new NBTTagDouble(p_74780_2_));
		}

///    
///     <summary> * Stores a new NBTTagString with the given string value into the map with the given string key. </summary>
///     
		public virtual void setString(string p_74778_1_, string p_74778_2_)
		{
			this.tagMap.Add(p_74778_1_, new NBTTagString(p_74778_2_));
		}

///    
///     <summary> * Stores a new NBTTagByteArray with the given array as data into the map with the given string key. </summary>
///     
		public virtual void setByteArray(string p_74773_1_, sbyte[] p_74773_2_)
		{
			this.tagMap.Add(p_74773_1_, new NBTTagByteArray(p_74773_2_));
		}

///    
///     <summary> * Stores a new NBTTagIntArray with the given array as data into the map with the given string key. </summary>
///     
		public virtual void setIntArray(string p_74783_1_, int[] p_74783_2_)
		{
			this.tagMap.Add(p_74783_1_, new NBTTagIntArray(p_74783_2_));
		}

///    
///     <summary> * Stores the given boolean value as a NBTTagByte, storing 1 for true and 0 for false, using the given string key. </summary>
///     
		public virtual void setBoolean(string p_74757_1_, bool p_74757_2_)
		{
			this.setByte(p_74757_1_, (sbyte)(p_74757_2_ ? 1 : 0));
		}

///    
///     <summary> * gets a generic tag with the specified name </summary>
///     
		public virtual NBTBase getTag(string p_74781_1_)
		{
			return (NBTBase)this.tagMap.get(p_74781_1_);
		}

		public virtual sbyte func_150299_b(string p_150299_1_)
		{
			NBTBase var2 = (NBTBase)this.tagMap.get(p_150299_1_);
			return var2 != null ? var2.Id : 0;
		}

///    
///     <summary> * Returns whether the given string has been previously stored as a key in the map. </summary>
///     
		public virtual bool hasKey(string p_74764_1_)
		{
			return this.tagMap.ContainsKey(p_74764_1_);
		}

		public virtual bool func_150297_b(string p_150297_1_, int p_150297_2_)
		{
			sbyte var3 = this.func_150299_b(p_150297_1_);
			return var3 == p_150297_2_ ? true : (p_150297_2_ != 99 ? false : var3 == 1 || var3 == 2 || var3 == 3 || var3 == 4 || var3 == 5 || var3 == 6);
		}

///    
///     <summary> * Retrieves a byte value using the specified key, or 0 if no such key was stored. </summary>
///     
		public virtual sbyte getByte(string p_74771_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74771_1_) ? 0 : ((NBTBase.NBTPrimitive)this.tagMap.get(p_74771_1_)).func_150290_f();
			}
			catch (ClassCastException var3)
			{
				return (sbyte)0;
			}
		}

///    
///     <summary> * Retrieves a short value using the specified key, or 0 if no such key was stored. </summary>
///     
		public virtual short getShort(string p_74765_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74765_1_) ? 0 : ((NBTBase.NBTPrimitive)this.tagMap.get(p_74765_1_)).func_150289_e();
			}
			catch (ClassCastException var3)
			{
				return (short)0;
			}
		}

///    
///     <summary> * Retrieves an integer value using the specified key, or 0 if no such key was stored. </summary>
///     
		public virtual int getInteger(string p_74762_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74762_1_) ? 0 : ((NBTBase.NBTPrimitive)this.tagMap.get(p_74762_1_)).func_150287_d();
			}
			catch (ClassCastException var3)
			{
				return 0;
			}
		}

///    
///     <summary> * Retrieves a long value using the specified key, or 0 if no such key was stored. </summary>
///     
		public virtual long getLong(string p_74763_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74763_1_) ? 0L : ((NBTBase.NBTPrimitive)this.tagMap.get(p_74763_1_)).func_150291_c();
			}
			catch (ClassCastException var3)
			{
				return 0L;
			}
		}

///    
///     <summary> * Retrieves a float value using the specified key, or 0 if no such key was stored. </summary>
///     
		public virtual float getFloat(string p_74760_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74760_1_) ? 0.0F : ((NBTBase.NBTPrimitive)this.tagMap.get(p_74760_1_)).func_150288_h();
			}
			catch (ClassCastException var3)
			{
				return 0.0F;
			}
		}

///    
///     <summary> * Retrieves a double value using the specified key, or 0 if no such key was stored. </summary>
///     
		public virtual double getDouble(string p_74769_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74769_1_) ? 0.0D : ((NBTBase.NBTPrimitive)this.tagMap.get(p_74769_1_)).func_150286_g();
			}
			catch (ClassCastException var3)
			{
				return 0.0D;
			}
		}

///    
///     <summary> * Retrieves a string value using the specified key, or an empty string if no such key was stored. </summary>
///     
		public virtual string getString(string p_74779_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74779_1_) ? "" : ((NBTBase)this.tagMap.get(p_74779_1_)).func_150285_a_();
			}
			catch (ClassCastException var3)
			{
				return "";
			}
		}

///    
///     <summary> * Retrieves a byte array using the specified key, or a zero-length array if no such key was stored. </summary>
///     
		public virtual sbyte[] getByteArray(string p_74770_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74770_1_) ? new sbyte[0] : ((NBTTagByteArray)this.tagMap.get(p_74770_1_)).func_150292_c();
			}
			catch (ClassCastException var3)
			{
				throw new ReportedException(this.createCrashReport(p_74770_1_, 7, var3));
			}
		}

///    
///     <summary> * Retrieves an int array using the specified key, or a zero-length array if no such key was stored. </summary>
///     
		public virtual int[] getIntArray(string p_74759_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74759_1_) ? new int[0] : ((NBTTagIntArray)this.tagMap.get(p_74759_1_)).func_150302_c();
			}
			catch (ClassCastException var3)
			{
				throw new ReportedException(this.createCrashReport(p_74759_1_, 11, var3));
			}
		}

///    
///     <summary> * Retrieves a NBTTagCompound subtag matching the specified key, or a new empty NBTTagCompound if no such key was
///     * stored. </summary>
///     
		public virtual NBTTagCompound getCompoundTag(string p_74775_1_)
		{
			try
			{
				return !this.tagMap.ContainsKey(p_74775_1_) ? new NBTTagCompound() : (NBTTagCompound)this.tagMap.get(p_74775_1_);
			}
			catch (ClassCastException var3)
			{
				throw new ReportedException(this.createCrashReport(p_74775_1_, 10, var3));
			}
		}

///    
///     <summary> * Gets the NBTTagList object with the given name. Args: name, NBTBase type </summary>
///     
		public virtual NBTTagList getTagList(string p_150295_1_, int p_150295_2_)
		{
			try
			{
				if (this.func_150299_b(p_150295_1_) != 9)
				{
					return new NBTTagList();
				}
				else
				{
					NBTTagList var3 = (NBTTagList)this.tagMap.get(p_150295_1_);
					return var3.tagCount() > 0 && var3.func_150303_d() != p_150295_2_ ? new NBTTagList() : var3;
				}
			}
			catch (ClassCastException var4)
			{
				throw new ReportedException(this.createCrashReport(p_150295_1_, 9, var4));
			}
		}

///    
///     <summary> * Retrieves a boolean value using the specified key, or false if no such key was stored. This uses the getByte
///     * method. </summary>
///     
		public virtual bool getBoolean(string p_74767_1_)
		{
			return this.getByte(p_74767_1_) != 0;
		}

///    
///     <summary> * Remove the specified tag. </summary>
///     
		public virtual void removeTag(string p_82580_1_)
		{
			this.tagMap.Remove(p_82580_1_);
		}

		public override string ToString()
		{
			string var1 = "{";
			string var3;

			for (IEnumerator var2 = this.tagMap.Keys.GetEnumerator(); var2.MoveNext(); var1 = var1 + var3 + ':' + this.tagMap[var3] + ',')
			{
				var3 = (string)var2.Current;
			}

			return var1 + "}";
		}

///    
///     <summary> * Return whether this compound has no tags. </summary>
///     
		public virtual bool hasNoTags()
		{
			return this.tagMap.Count == 0;
		}

///    
///     <summary> * Create a crash report which indicates a NBT read error. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: private CrashReport createCrashReport(final String p_82581_1_, final int p_82581_2_, ClassCastException p_82581_3_)
		private CrashReport createCrashReport(string p_82581_1_, int p_82581_2_, ClassCastException p_82581_3_)
		{
			CrashReport var4 = CrashReport.makeCrashReport(p_82581_3_, "Reading NBT data");
			CrashReportCategory var5 = var4.makeCategoryDepth("Corrupt NBT tag", 1);
			var5.addCrashSectionCallable("Tag type found", new Callable() { private static final string __OBFID = "CL_00001216"; public string call() { return NBTBase.NBTTypes[((NBTBase)NBTTagCompound.tagMap.get(p_82581_1_)).Id]; } });
			var5.addCrashSectionCallable("Tag type expected", new Callable() { private static final string __OBFID = "CL_00001217"; public string call() { return NBTBase.NBTTypes[p_82581_2_]; } });
			var5.addCrashSection("Tag name", p_82581_1_);
			return var4;
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public override NBTBase copy()
		{
			NBTTagCompound var1 = new NBTTagCompound();
			IEnumerator var2 = this.tagMap.Keys.GetEnumerator();

			while (var2.MoveNext())
			{
				string var3 = (string)var2.Current;
				var1.setTag(var3, ((NBTBase)this.tagMap.get(var3)).copy());
			}

			return var1;
		}

		public override bool Equals(object p_equals_1_)
		{
			if (base.Equals(p_equals_1_))
			{
				NBTTagCompound var2 = (NBTTagCompound)p_equals_1_;
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'entrySet' method:
				return this.tagMap.entrySet().Equals(var2.tagMap.entrySet());
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.tagMap.GetHashCode();
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static void func_150298_a(String p_150298_0_, NBTBase p_150298_1_, DataOutput p_150298_2_) throws IOException
		private static void func_150298_a(string p_150298_0_, NBTBase p_150298_1_, DataOutput p_150298_2_)
		{
			p_150298_2_.writeByte(p_150298_1_.Id);

			if (p_150298_1_.Id != 0)
			{
				p_150298_2_.writeUTF(p_150298_0_);
				p_150298_1_.write(p_150298_2_);
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static byte func_152447_a(DataInput p_152447_0_, NBTSizeTracker p_152447_1_) throws IOException
		private static sbyte func_152447_a(DataInput p_152447_0_, NBTSizeTracker p_152447_1_)
		{
			return p_152447_0_.readByte();
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static String func_152448_b(DataInput p_152448_0_, NBTSizeTracker p_152448_1_) throws IOException
		private static string func_152448_b(DataInput p_152448_0_, NBTSizeTracker p_152448_1_)
		{
			return p_152448_0_.readUTF();
		}

		internal static NBTBase func_152449_a(sbyte p_152449_0_, string p_152449_1_, DataInput p_152449_2_, int p_152449_3_, NBTSizeTracker p_152449_4_)
		{
			NBTBase var5 = NBTBase.func_150284_a(p_152449_0_);

			try
			{
				var5.func_152446_a(p_152449_2_, p_152449_3_, p_152449_4_);
				return var5;
			}
			catch (IOException var9)
			{
				CrashReport var7 = CrashReport.makeCrashReport(var9, "Loading NBT data");
				CrashReportCategory var8 = var7.makeCategory("NBT Tag");
				var8.addCrashSection("Tag name", p_152449_1_);
				var8.addCrashSection("Tag type", Convert.ToByte(p_152449_0_));
				throw new ReportedException(var7);
			}
		}
	}

}