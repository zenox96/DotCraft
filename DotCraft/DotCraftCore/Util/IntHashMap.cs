using System;

namespace DotCraftCore.Util
{


	public class IntHashMap
	{
	/// <summary> An array of HashEntries representing the heads of hash slot lists  </summary>
		[NonSerialized]
		private IntHashMap.Entry[] slots = new IntHashMap.Entry[16];

	/// <summary> The number of items stored in this map  </summary>
		[NonSerialized]
		private int count;

	/// <summary> The grow threshold  </summary>
		private int threshold = 12;

	/// <summary> The scale factor used to determine when to grow the table  </summary>
		private readonly float growFactor = 0.75F;

	/// <summary> A serial stamp used to mark changes  </summary>
		[NonSerialized]
		private volatile int versionStamp;

	/// <summary> The set of all the keys stored in this MCHash object  </summary>
		private Set keySet = new HashSet();
		

///    
///     <summary> * Makes the passed in integer suitable for hashing by a number of shifts </summary>
///     
		private static int computeHash(int p_76044_0_)
		{
			p_76044_0_ ^= (int)((uint)p_76044_0_ >> 20 ^ p_76044_0_ >)>> 12;
			return p_76044_0_ ^ (int)((uint)p_76044_0_ >> 7 ^ p_76044_0_ >)>> 4;
		}

///    
///     <summary> * Computes the index of the slot for the hash and slot count passed in. </summary>
///     
		private static int getSlotIndex(int p_76043_0_, int p_76043_1_)
		{
			return p_76043_0_ & p_76043_1_ - 1;
		}

///    
///     <summary> * Returns the object associated to a key </summary>
///     
		public virtual object lookup(int p_76041_1_)
		{
			int var2 = computeHash(p_76041_1_);

			for(IntHashMap.Entry var3 = this.slots[getSlotIndex(var2, this.slots.Length)]; var3 != null; var3 = var3.nextEntry)
			{
				if(var3.hashEntry == p_76041_1_)
				{
					return var3.valueEntry;
				}
			}

			return null;
		}

///    
///     <summary> * Return true if an object is associated with the given key </summary>
///     
		public virtual bool containsItem(int p_76037_1_)
		{
			return this.lookupEntry(p_76037_1_) != null;
		}

///    
///     <summary> * Returns the key/object mapping for a given key as a MCHashEntry </summary>
///     
		internal IntHashMap.Entry lookupEntry(int p_76045_1_)
		{
			int var2 = computeHash(p_76045_1_);

			for(IntHashMap.Entry var3 = this.slots[getSlotIndex(var2, this.slots.Length)]; var3 != null; var3 = var3.nextEntry)
			{
				if(var3.hashEntry == p_76045_1_)
				{
					return var3;
				}
			}

			return null;
		}

///    
///     <summary> * Adds a key and associated value to this map </summary>
///     
		public virtual void addKey(int p_76038_1_, object p_76038_2_)
		{
			this.keySet.add(Convert.ToInt32(p_76038_1_));
			int var3 = computeHash(p_76038_1_);
			int var4 = getSlotIndex(var3, this.slots.Length);

			for(IntHashMap.Entry var5 = this.slots[var4]; var5 != null; var5 = var5.nextEntry)
			{
				if(var5.hashEntry == p_76038_1_)
				{
					var5.valueEntry = p_76038_2_;
					return;
				}
			}

			++this.versionStamp;
			this.insert(var3, p_76038_1_, p_76038_2_, var4);
		}

///    
///     <summary> * Increases the number of hash slots </summary>
///     
		private void grow(int p_76047_1_)
		{
			IntHashMap.Entry[] var2 = this.slots;
			int var3 = var2.Length;

			if(var3 == 1073741824)
			{
				this.threshold = int.MaxValue;
			}
			else
			{
				IntHashMap.Entry[] var4 = new IntHashMap.Entry[p_76047_1_];
				this.copyTo(var4);
				this.slots = var4;
				this.threshold = (int)((float)p_76047_1_ * this.growFactor);
			}
		}

///    
///     <summary> * Copies the hash slots to a new array </summary>
///     
		private void copyTo(IntHashMap.Entry[] p_76048_1_)
		{
			IntHashMap.Entry[] var2 = this.slots;
			int var3 = p_76048_1_.Length;

			for(int var4 = 0; var4 < var2.Length; ++var4)
			{
				IntHashMap.Entry var5 = var2[var4];

				if(var5 != null)
				{
					var2[var4] = null;
					IntHashMap.Entry var6;

					do
					{
						var6 = var5.nextEntry;
						int var7 = getSlotIndex(var5.slotHash, var3);
						var5.nextEntry = p_76048_1_[var7];
						p_76048_1_[var7] = var5;
						var5 = var6;
					}
					while(var6 != null);
				}
			}
		}

///    
///     <summary> * Removes the specified object from the map and returns it </summary>
///     
		public virtual object removeObject(int p_76049_1_)
		{
			this.keySet.remove(Convert.ToInt32(p_76049_1_));
			IntHashMap.Entry var2 = this.removeEntry(p_76049_1_);
			return var2 == null ? null : var2.valueEntry;
		}

///    
///     <summary> * Removes the specified entry from the map and returns it </summary>
///     
		internal IntHashMap.Entry removeEntry(int p_76036_1_)
		{
			int var2 = computeHash(p_76036_1_);
			int var3 = getSlotIndex(var2, this.slots.Length);
			IntHashMap.Entry var4 = this.slots[var3];
			IntHashMap.Entry var5;
			IntHashMap.Entry var6;

			for(var5 = var4; var5 != null; var5 = var6)
			{
				var6 = var5.nextEntry;

				if(var5.hashEntry == p_76036_1_)
				{
					++this.versionStamp;
					--this.count;

					if(var4 == var5)
					{
						this.slots[var3] = var6;
					}
					else
					{
						var4.nextEntry = var6;
					}

					return var5;
				}

				var4 = var5;
			}

			return var5;
		}

///    
///     <summary> * Removes all entries from the map </summary>
///     
		public virtual void clearMap()
		{
			++this.versionStamp;
			IntHashMap.Entry[] var1 = this.slots;

			for(int var2 = 0; var2 < var1.Length; ++var2)
			{
				var1[var2] = null;
			}

			this.count = 0;
		}

///    
///     <summary> * Adds an object to a slot </summary>
///     
		private void insert(int p_76040_1_, int p_76040_2_, object p_76040_3_, int p_76040_4_)
		{
			IntHashMap.Entry var5 = this.slots[p_76040_4_];
			this.slots[p_76040_4_] = new IntHashMap.Entry(p_76040_1_, p_76040_2_, p_76040_3_, var5);

			if(this.count++ >= this.threshold)
			{
				this.grow(2 * this.slots.Length);
			}
		}

		internal class Entry
		{
			internal readonly int hashEntry;
			internal object valueEntry;
			internal IntHashMap.Entry nextEntry;
			internal readonly int slotHash;
			

			internal Entry(int p_i1552_1_, int p_i1552_2_, object p_i1552_3_, IntHashMap.Entry p_i1552_4_)
			{
				this.valueEntry = p_i1552_3_;
				this.nextEntry = p_i1552_4_;
				this.hashEntry = p_i1552_2_;
				this.slotHash = p_i1552_1_;
			}

			public int Hash
			{
				get
				{
					return this.hashEntry;
				}
			}

			public object Value
			{
				get
				{
					return this.valueEntry;
				}
			}

			public sealed override bool Equals(object p_equals_1_)
			{
				if(!(p_equals_1_ is IntHashMap.Entry))
				{
					return false;
				}
				else
				{
					IntHashMap.Entry var2 = (IntHashMap.Entry)p_equals_1_;
					int? var3 = Convert.ToInt32(this.Hash);
					int? var4 = Convert.ToInt32(var2.Hash);

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
				return IntHashMap.computeHash(this.hashEntry);
			}

			public sealed override string ToString()
			{
				return this.Hash + "=" + this.Value;
			}
		}
	}

}