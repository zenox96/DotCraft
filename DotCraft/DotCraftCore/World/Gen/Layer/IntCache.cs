using System.Runtime.CompilerServices;
using System.Collections;

namespace DotCraftCore.World.Gen.Layer
{


	public class IntCache
	{
		private static int intCacheSize = 256;

///    
///     <summary> * A list of pre-allocated int[256] arrays that are currently unused and can be returned by getIntCache() </summary>
///     
		private static IList freeSmallArrays = new ArrayList();

///    
///     <summary> * A list of pre-allocated int[256] arrays that were previously returned by getIntCache() and which will not be re-
///     * used again until resetIntCache() is called. </summary>
///     
		private static IList inUseSmallArrays = new ArrayList();

///    
///     <summary> * A list of pre-allocated int[cacheSize] arrays that are currently unused and can be returned by getIntCache() </summary>
///     
		private static IList freeLargeArrays = new ArrayList();

///    
///     <summary> * A list of pre-allocated int[cacheSize] arrays that were previously returned by getIntCache() and which will not
///     * be re-used again until resetIntCache() is called. </summary>
///     
		private static IList inUseLargeArrays = new ArrayList();
		

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static int[] getIntCache(int p_76445_0_)
		{
			int[] var1;

			if (p_76445_0_ <= 256)
			{
				if (freeSmallArrays.Count == 0)
				{
					var1 = new int[256];
					inUseSmallArrays.Add(var1);
					return var1;
				}
				else
				{
					var1 = (int[])freeSmallArrays.Remove(freeSmallArrays.Count - 1);
					inUseSmallArrays.Add(var1);
					return var1;
				}
			}
			else if (p_76445_0_ > intCacheSize)
			{
				intCacheSize = p_76445_0_;
				freeLargeArrays.Clear();
				inUseLargeArrays.Clear();
				var1 = new int[intCacheSize];
				inUseLargeArrays.Add(var1);
				return var1;
			}
			else if (freeLargeArrays.Count == 0)
			{
				var1 = new int[intCacheSize];
				inUseLargeArrays.Add(var1);
				return var1;
			}
			else
			{
				var1 = (int[])freeLargeArrays.Remove(freeLargeArrays.Count - 1);
				inUseLargeArrays.Add(var1);
				return var1;
			}
		}

///    
///     <summary> * Mark all pre-allocated arrays as available for re-use by moving them to the appropriate free lists. </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void resetIntCache()
		{
			if (!freeLargeArrays.Count == 0)
			{
				freeLargeArrays.Remove(freeLargeArrays.Count - 1);
			}

			if (!freeSmallArrays.Count == 0)
			{
				freeSmallArrays.Remove(freeSmallArrays.Count - 1);
			}

			freeLargeArrays.AddRange(inUseLargeArrays);
			freeSmallArrays.AddRange(inUseSmallArrays);
			inUseLargeArrays.Clear();
			inUseSmallArrays.Clear();
		}

///    
///     <summary> * Gets a human-readable string that indicates the sizes of all the cache fields.  Basically a synchronized static
///     * toString. </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static string CacheSizes
		{
			get
			{
				return "cache: " + freeLargeArrays.Count + ", tcache: " + freeSmallArrays.Count + ", allocated: " + inUseLargeArrays.Count + ", tallocated: " + inUseSmallArrays.Count;
			}
		}
	}

}