using System.Runtime.CompilerServices;
using System.Collections;

namespace DotCraftCore.nWorld.nChunk.nStorage
{


	public class RegionFileCache
	{
	/// <summary> A map containing Files as keys and RegionFiles as values  </summary>
		private static readonly IDictionary regionsByFilename = new Hashtable();
		

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static RegionFile createOrLoadRegionFile(File p_76550_0_, int p_76550_1_, int p_76550_2_)
		{
			File var3 = new File(p_76550_0_, "region");
			File var4 = new File(var3, "r." + (p_76550_1_ >> 5) + "." + (p_76550_2_ >> 5) + ".mca");
			RegionFile var5 = (RegionFile)regionsByFilename[var4];

			if (var5 != null)
			{
				return var5;
			}
			else
			{
				if (!var3.exists())
				{
					var3.mkdirs();
				}

				if (regionsByFilename.Count >= 256)
				{
					clearRegionFileReferences();
				}

				RegionFile var6 = new RegionFile(var4);
				regionsByFilename.Add(var4, var6);
				return var6;
			}
		}

///    
///     <summary> * Saves the current Chunk Map Cache </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void clearRegionFileReferences()
		{
			IEnumerator var0 = regionsByFilename.Values.GetEnumerator();

			while (var0.MoveNext())
			{
				RegionFile var1 = (RegionFile)var0.Current;

				try
				{
					if (var1 != null)
					{
						var1.close();
					}
				}
				catch (IOException var3)
				{
					var3.printStackTrace();
				}
			}

			regionsByFilename.Clear();
		}

///    
///     <summary> * Returns an input stream for the specified chunk. Args: worldDir, chunkX, chunkZ </summary>
///     
		public static DataInputStream getChunkInputStream(File p_76549_0_, int p_76549_1_, int p_76549_2_)
		{
			RegionFile var3 = createOrLoadRegionFile(p_76549_0_, p_76549_1_, p_76549_2_);
			return var3.getChunkDataInputStream(p_76549_1_ & 31, p_76549_2_ & 31);
		}

///    
///     <summary> * Returns an output stream for the specified chunk. Args: worldDir, chunkX, chunkZ </summary>
///     
		public static DataOutputStream getChunkOutputStream(File p_76552_0_, int p_76552_1_, int p_76552_2_)
		{
			RegionFile var3 = createOrLoadRegionFile(p_76552_0_, p_76552_1_, p_76552_2_);
			return var3.getChunkDataOutputStream(p_76552_1_ & 31, p_76552_2_ & 31);
		}
	}

}