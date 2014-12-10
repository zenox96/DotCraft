using System;

namespace DotCraftCore.Util
{

	public class LongHashMap
	{
	/// <summary> the array of all elements in the hash  </summary>
		[NonSerialized]
		private LongHashMap.Entry[] hashArray = new LongHashMap.Entry[16];

	/// <summary> the number of elements in the hash array  </summary>
		[NonSerialized]
		private int numHashElements;

///    
///     <summary> * the maximum amount of elements in the hash (probably 3/4 the size due to meh hashing function) </summary>
///     
		private int capacity = 12;

///    
///     <summary> * percent of the hasharray that can be used without hash colliding probably </summary>
///     
		private readonly float percentUseable = 0.75F;

	/// <summary> count of times elements have been added/removed  </summary>
		[NonSerialized]
		private volatile int modCount;
		private const string __OBFID = "CL_00001492";

///    
///     <summary> * returns the hashed key given the original key </summary>
///     
		private static int getHashedKey(long p_76155_0_)
		{
			return hash((int)(p_76155_0_ ^ (long)((ulong)p_76155_0_ >> 32)));
		}

///    
///     <summary> * the hash function </summary>
///     
		private static int hash(int p_76157_0_)
		{
			p_76157_0_ ^= (int)((uint)p_76157_0_ >> 20 ^ p_76157_0_ >)>> 12;
			return p_76157_0_ ^ (int)((uint)p_76157_0_ >> 7 ^ p_76157_0_ >)>> 4;
		}

///    
///     <summary> * gets the index in the hash given the array length and the hashed key </summary>
///     
		private static int getHashIndex(int p_76158_0_, int p_76158_1_)
		{
			return p_76158_0_ & p_76158_1_ - 1;
		}

		public virtual int NumHashElements
		{
			get
			{
				return this.numHashElements;
			}
		}

///    
///     <summary> * get the value from the map given the key </summary>
///     
		public virtual object getValueByKey(long p_76164_1_)
		{
			int var3 = getHashedKey(p_76164_1_);

			for(LongHashMap.Entry var4 = this.hashArray[getHashIndex(var3, this.hashArray.Length)]; var4 != null; var4 = var4.nextEntry)
			{
				if(var4.key == p_76164_1_)
				{
					return var4.value;
				}
			}

			return null;
		}

		public virtual bool containsItem(long p_76161_1_)
		{
			return this.getEntry(p_76161_1_) != null;
		}

		internal LongHashMap.Entry getEntry(long p_76160_1_)
		{
			int var3 = getHashedKey(p_76160_1_);

			for(LongHashMap.Entry var4 = this.hashArray[getHashIndex(var3, this.hashArray.Length)]; var4 != null; var4 = var4.nextEntry)
			{
				if(var4.key == p_76160_1_)
				{
					return var4;
				}
			}

			return null;
		}

///    
///     <summary> * Add a key-value pair. </summary>
///     
		public virtual void add(long p_76163_1_, object p_76163_3_)
		{
			int var4 = getHashedKey(p_76163_1_);
			int var5 = getHashIndex(var4, this.hashArray.Length);

			for(LongHashMap.Entry var6 = this.hashArray[var5]; var6 != null; var6 = var6.nextEntry)
			{
				if(var6.key == p_76163_1_)
				{
					var6.value = p_76163_3_;
					return;
				}
			}

			++this.modCount;
			this.createKey(var4, p_76163_1_, p_76163_3_, var5);
		}

///    
///     <summary> * resizes the table </summary>
///     
		private void resizeTable(int p_76153_1_)
		{
			LongHashMap.Entry[] var2 = this.hashArray;
			int var3 = var2.Length;

			if(var3 == 1073741824)
			{
				this.capacity = int.MaxValue;
			}
			else
			{
				LongHashMap.Entry[] var4 = new LongHashMap.Entry[p_76153_1_];
				this.copyHashTableTo(var4);
				this.hashArray = var4;
				this.capacity = (int)((float)p_76153_1_ * this.percentUseable);
			}
		}

///    
///     <summary> * copies the hash table to the specified array </summary>
///     
		private void copyHashTableTo(LongHashMap.Entry[] p_76154_1_)
		{
			LongHashMap.Entry[] var2 = this.hashArray;
			int var3 = p_76154_1_.Length;

			for(int var4 = 0; var4 < var2.Length; ++var4)
			{
				LongHashMap.Entry var5 = var2[var4];

				if(var5 != null)
				{
					var2[var4] = null;
					LongHashMap.Entry var6;

					do
					{
						var6 = var5.nextEntry;
						int var7 = getHashIndex(var5.hash, var3);
						var5.nextEntry = p_76154_1_[var7];
						p_76154_1_[var7] = var5;
						var5 = var6;
					}
					while(var6 != null);
				}
			}
		}

///    
///     <summary> * calls the removeKey method and returns removed object </summary>
///     
		public virtual object remove(long p_76159_1_)
		{
			LongHashMap.Entry var3 = this.removeKey(p_76159_1_);
			return var3 == null ? null : var3.value;
		}

///    
///     <summary> * removes the key from the hash linked list </summary>
///     
		internal LongHashMap.Entry removeKey(long p_76152_1_)
		{
			int var3 = getHashedKey(p_76152_1_);
			int var4 = getHashIndex(var3, this.hashArray.Length);
			LongHashMap.Entry var5 = this.hashArray[var4];
			LongHashMap.Entry var6;
			LongHashMap.Entry var7;

			for(var6 = var5; var6 != null; var6 = var7)
			{
				var7 = var6.nextEntry;

				if(var6.key == p_76152_1_)
				{
					++this.modCount;
					--this.numHashElements;

					if(var5 == var6)
					{
						this.hashArray[var4] = var7;
					}
					else
					{
						var5.nextEntry = var7;
					}

					return var6;
				}

				var5 = var6;
			}

			return var6;
		}

///    
///     <summary> * creates the key in the hash table </summary>
///     
		private void createKey(int p_76156_1_, long p_76156_2_, object p_76156_4_, int p_76156_5_)
		{
			LongHashMap.Entry var6 = this.hashArray[p_76156_5_];
			this.hashArray[p_76156_5_] = new LongHashMap.Entry(p_76156_1_, p_76156_2_, p_76156_4_, var6);

			if(this.numHashElements++ >= this.capacity)
			{
				this.resizeTable(2 * this.hashArray.Length);
			}
		}

		internal class Entry
		{
			internal readonly long key;
			internal object value;
			internal LongHashMap.Entry nextEntry;
			internal readonly int hash;
			private const string __OBFID = "CL_00001493";

			internal Entry(int p_i1553_1_, long p_i1553_2_, object p_i1553_4_, LongHashMap.Entry p_i1553_5_)
			{
				this.value = p_i1553_4_;
				this.nextEntry = p_i1553_5_;
				this.key = p_i1553_2_;
				this.hash = p_i1553_1_;
			}

			public long Key
			{
				get
				{
					return this.key;
				}
			}

			public object Value
			{
				get
				{
					return this.value;
				}
			}

			public sealed override bool Equals(object p_equals_1_)
			{
				if(!(p_equals_1_ is LongHashMap.Entry))
				{
					return false;
				}
				else
				{
					LongHashMap.Entry var2 = (LongHashMap.Entry)p_equals_1_;
					long? var3 = Convert.ToInt64(this.Key);
					long? var4 = Convert.ToInt64(var2.Key);

					if(var3 == var4 || var3 != null && var3.Equals(var4))
					{
						object var5 = this.Value;
						object var6 = var2.Value;

						if(var5 == var6 || var5 != null && var5.Equals(var6))
						{
							return true;
						}
					}

					return false;
				}
			}

			public sealed override int GetHashCode()
			{
				return LongHashMap.getHashedKey(this.key);
			}

			public sealed override string ToString()
			{
				return this.Key + "=" + this.Value;
			}
		}
	}

}