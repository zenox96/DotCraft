using System;
using System.Collections;

namespace DotCraftCore.Util
{


	public class WeightedRandom
	{
		

///    
///     <summary> * Returns the total weight of all items in a collection. </summary>
///     
		public static int getTotalWeight(ICollection p_76272_0_)
		{
			int var1 = 0;
			WeightedRandom.Item var3;

			for(IEnumerator var2 = p_76272_0_.GetEnumerator(); var2.MoveNext(); var1 += var3.itemWeight)
			{
				var3 = (WeightedRandom.Item)var2.Current;
			}

			return var1;
		}

///    
///     <summary> * Returns a random choice from the input items, with a total weight value. </summary>
///     
		public static WeightedRandom.Item getRandomItem(Random p_76273_0_, ICollection p_76273_1_, int p_76273_2_)
		{
			if(p_76273_2_ <= 0)
			{
				throw new System.ArgumentException();
			}
			else
			{
				int var3 = p_76273_0_.Next(p_76273_2_);
				IEnumerator var4 = p_76273_1_.GetEnumerator();
				WeightedRandom.Item var5;

				do
				{
					if(!var4.MoveNext())
					{
						return null;
					}

					var5 = (WeightedRandom.Item)var4.Current;
					var3 -= var5.itemWeight;
				}
				while(var3 >= 0);

				return var5;
			}
		}

///    
///     <summary> * Returns a random choice from the input items. </summary>
///     
		public static WeightedRandom.Item getRandomItem(Random p_76271_0_, ICollection p_76271_1_)
		{
			return getRandomItem(p_76271_0_, p_76271_1_, getTotalWeight(p_76271_1_));
		}

///    
///     <summary> * Returns the total weight of all items in a array. </summary>
///     
		public static int getTotalWeight(WeightedRandom.Item[] p_76270_0_)
		{
			int var1 = 0;
			WeightedRandom.Item[] var2 = p_76270_0_;
			int var3 = p_76270_0_.Length;

			for(int var4 = 0; var4 < var3; ++var4)
			{
				WeightedRandom.Item var5 = var2[var4];
				var1 += var5.itemWeight;
			}

			return var1;
		}

///    
///     <summary> * Returns a random choice from the input array of items, with a total weight value. </summary>
///     
		public static WeightedRandom.Item getRandomItem(Random p_76269_0_, WeightedRandom.Item[] p_76269_1_, int p_76269_2_)
		{
			if(p_76269_2_ <= 0)
			{
				throw new System.ArgumentException();
			}
			else
			{
				int var3 = p_76269_0_.Next(p_76269_2_);
				WeightedRandom.Item[] var4 = p_76269_1_;
				int var5 = p_76269_1_.Length;

				for(int var6 = 0; var6 < var5; ++var6)
				{
					WeightedRandom.Item var7 = var4[var6];
					var3 -= var7.itemWeight;

					if(var3 < 0)
					{
						return var7;
					}
				}

				return null;
			}
		}

///    
///     <summary> * Returns a random choice from the input items. </summary>
///     
		public static WeightedRandom.Item getRandomItem(Random p_76274_0_, WeightedRandom.Item[] p_76274_1_)
		{
			return getRandomItem(p_76274_0_, p_76274_1_, getTotalWeight(p_76274_1_));
		}

		public class Item
		{
			protected internal int itemWeight;
			

			public Item(int p_i1556_1_)
			{
				this.itemWeight = p_i1556_1_;
			}
		}
	}

}