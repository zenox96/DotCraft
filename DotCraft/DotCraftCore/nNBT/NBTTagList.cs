using System;
using System.Collections;

namespace DotCraftCore.nNBT
{


	public class NBTTagList : NBTBase
	{
	/// <summary> The array list containing the tags encapsulated in this list.  </summary>
		private IList tagList = new ArrayList();

///    
///     <summary> * The type byte for the tags in the list - they must all be of the same type. </summary>
///     
		private sbyte tagType = 0;
		

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal override void write(DataOutput p_74734_1_)
		{
			if (!this.tagList.Count == 0)
			{
				this.tagType = ((NBTBase)this.tagList.get(0)).Id;
			}
			else
			{
				this.tagType = 0;
			}

			p_74734_1_.writeByte(this.tagType);
			p_74734_1_.writeInt(this.tagList.Count);

			for (int var2 = 0; var2 < this.tagList.Count; ++var2)
			{
				((NBTBase)this.tagList.get(var2)).write(p_74734_1_);
			}
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
				p_152446_3_.func_152450_a(8L);
				this.tagType = p_152446_1_.readByte();
				int var4 = p_152446_1_.readInt();
				this.tagList = new ArrayList();

				for (int var5 = 0; var5 < var4; ++var5)
				{
					NBTBase var6 = NBTBase.func_150284_a(this.tagType);
					var6.func_152446_a(p_152446_1_, p_152446_2_ + 1, p_152446_3_);
					this.tagList.Add(var6);
				}
			}
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public override sbyte Id
		{
			get
			{
				return (sbyte)9;
			}
		}

		public override string ToString()
		{
			string var1 = "[";
			int var2 = 0;

			for (IEnumerator var3 = this.tagList.GetEnumerator(); var3.MoveNext(); ++var2)
			{
				NBTBase var4 = (NBTBase)var3.Current;
				var1 = var1 + "" + var2 + ':' + var4 + ',';
			}

			return var1 + "]";
		}

///    
///     <summary> * Adds the provided tag to the end of the list. There is no check to verify this tag is of the same type as any
///     * previous tag. </summary>
///     
		public virtual void appendTag(NBTBase p_74742_1_)
		{
			if (this.tagType == 0)
			{
				this.tagType = p_74742_1_.Id;
			}
			else if (this.tagType != p_74742_1_.Id)
			{
				System.err.println("WARNING: Adding mismatching tag types to tag list");
				return;
			}

			this.tagList.Add(p_74742_1_);
		}

		public virtual void func_150304_a(int p_150304_1_, NBTBase p_150304_2_)
		{
			if (p_150304_1_ >= 0 && p_150304_1_ < this.tagList.Count)
			{
				if (this.tagType == 0)
				{
					this.tagType = p_150304_2_.Id;
				}
				else if (this.tagType != p_150304_2_.Id)
				{
					System.err.println("WARNING: Adding mismatching tag types to tag list");
					return;
				}

				this.tagList[p_150304_1_] = p_150304_2_;
			}
			else
			{
				System.err.println("WARNING: index out of bounds to set tag in tag list");
			}
		}

///    
///     <summary> * Removes a tag at the given index. </summary>
///     
		public virtual NBTBase removeTag(int p_74744_1_)
		{
			return (NBTBase)this.tagList.remove(p_74744_1_);
		}

///    
///     <summary> * Retrieves the NBTTagCompound at the specified index in the list </summary>
///     
		public virtual NBTTagCompound getCompoundTagAt(int p_150305_1_)
		{
			if (p_150305_1_ >= 0 && p_150305_1_ < this.tagList.Count)
			{
				NBTBase var2 = (NBTBase)this.tagList.get(p_150305_1_);
				return var2.Id == 10 ? (NBTTagCompound)var2 : new NBTTagCompound();
			}
			else
			{
				return new NBTTagCompound();
			}
		}

		public virtual int[] func_150306_c(int p_150306_1_)
		{
			if (p_150306_1_ >= 0 && p_150306_1_ < this.tagList.Count)
			{
				NBTBase var2 = (NBTBase)this.tagList.get(p_150306_1_);
				return var2.Id == 11 ? ((NBTTagIntArray)var2).func_150302_c() : new int[0];
			}
			else
			{
				return new int[0];
			}
		}

		public virtual double func_150309_d(int p_150309_1_)
		{
			if (p_150309_1_ >= 0 && p_150309_1_ < this.tagList.Count)
			{
				NBTBase var2 = (NBTBase)this.tagList.get(p_150309_1_);
				return var2.Id == 6 ? ((NBTTagDouble)var2).func_150286_g() : 0.0D;
			}
			else
			{
				return 0.0D;
			}
		}

		public virtual float func_150308_e(int p_150308_1_)
		{
			if (p_150308_1_ >= 0 && p_150308_1_ < this.tagList.Count)
			{
				NBTBase var2 = (NBTBase)this.tagList.get(p_150308_1_);
				return var2.Id == 5 ? ((NBTTagFloat)var2).func_150288_h() : 0.0F;
			}
			else
			{
				return 0.0F;
			}
		}

///    
///     <summary> * Retrieves the tag String value at the specified index in the list </summary>
///     
		public virtual string getStringTagAt(int p_150307_1_)
		{
			if (p_150307_1_ >= 0 && p_150307_1_ < this.tagList.Count)
			{
				NBTBase var2 = (NBTBase)this.tagList.get(p_150307_1_);
				return var2.Id == 8 ? var2.func_150285_a_() : var2.ToString();
			}
			else
			{
				return "";
			}
		}

///    
///     <summary> * Returns the number of tags in the list. </summary>
///     
		public virtual int tagCount()
		{
			return this.tagList.Count;
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public override NBTBase copy()
		{
			NBTTagList var1 = new NBTTagList();
			var1.tagType = this.tagType;
			IEnumerator var2 = this.tagList.GetEnumerator();

			while (var2.MoveNext())
			{
				NBTBase var3 = (NBTBase)var2.Current;
				NBTBase var4 = var3.copy();
				var1.tagList.Add(var4);
			}

			return var1;
		}

		public override bool Equals(object p_equals_1_)
		{
			if (base.Equals(p_equals_1_))
			{
				NBTTagList var2 = (NBTTagList)p_equals_1_;

				if (this.tagType == var2.tagType)
				{
					return this.tagList.Equals(var2.tagList);
				}
			}

			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.tagList.GetHashCode();
		}

		public virtual int func_150303_d()
		{
			return this.tagType;
		}
	}

}