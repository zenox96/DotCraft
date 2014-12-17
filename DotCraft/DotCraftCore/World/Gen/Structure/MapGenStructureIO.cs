using System;
using System.Collections;

namespace DotCraftCore.nWorld.nGen.nStructure
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using World = DotCraftCore.nWorld.World;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class MapGenStructureIO
	{
		private static readonly Logger logger = LogManager.Logger;
		private static IDictionary field_143040_a = new Hashtable();
		private static IDictionary field_143038_b = new Hashtable();
		private static IDictionary field_143039_c = new Hashtable();
		private static IDictionary field_143037_d = new Hashtable();
		

		private static void func_143034_b(Type p_143034_0_, string p_143034_1_)
		{
			field_143040_a.Add(p_143034_1_, p_143034_0_);
			field_143038_b.Add(p_143034_0_, p_143034_1_);
		}

		internal static void func_143031_a(Type p_143031_0_, string p_143031_1_)
		{
			field_143039_c.Add(p_143031_1_, p_143031_0_);
			field_143037_d.Add(p_143031_0_, p_143031_1_);
		}

		public static string func_143033_a(StructureStart p_143033_0_)
		{
			return (string)field_143038_b[p_143033_0_.GetType()];
		}

		public static string func_143036_a(StructureComponent p_143036_0_)
		{
			return (string)field_143037_d[p_143036_0_.GetType()];
		}

		public static StructureStart func_143035_a(NBTTagCompound p_143035_0_, World p_143035_1_)
		{
			StructureStart var2 = null;

			try
			{
				Type var3 = (Class)field_143040_a[p_143035_0_.getString("id")];

				if (var3 != null)
				{
					var2 = (StructureStart)var3.newInstance();
				}
			}
			catch (Exception var4)
			{
				logger.warn("Failed Start with id " + p_143035_0_.getString("id"));
				var4.printStackTrace();
			}

			if (var2 != null)
			{
				var2.func_143020_a(p_143035_1_, p_143035_0_);
			}
			else
			{
				logger.warn("Skipping Structure with id " + p_143035_0_.getString("id"));
			}

			return var2;
		}

		public static StructureComponent func_143032_b(NBTTagCompound p_143032_0_, World p_143032_1_)
		{
			StructureComponent var2 = null;

			try
			{
				Type var3 = (Class)field_143039_c[p_143032_0_.getString("id")];

				if (var3 != null)
				{
					var2 = (StructureComponent)var3.newInstance();
				}
			}
			catch (Exception var4)
			{
				logger.warn("Failed Piece with id " + p_143032_0_.getString("id"));
				var4.printStackTrace();
			}

			if (var2 != null)
			{
				var2.func_143009_a(p_143032_1_, p_143032_0_);
			}
			else
			{
				logger.warn("Skipping Piece with id " + p_143032_0_.getString("id"));
			}

			return var2;
		}

		static MapGenStructureIO()
		{
			func_143034_b(typeof(StructureMineshaftStart), "Mineshaft");
			func_143034_b(typeof(MapGenVillage.Start), "Village");
			func_143034_b(typeof(MapGenNetherBridge.Start), "Fortress");
			func_143034_b(typeof(MapGenStronghold.Start), "Stronghold");
			func_143034_b(typeof(MapGenScatteredFeature.Start), "Temple");
			StructureMineshaftPieces.func_143048_a();
			StructureVillagePieces.func_143016_a();
			StructureNetherBridgePieces.func_143049_a();
			StructureStrongholdPieces.func_143046_a();
			ComponentScatteredFeaturePieces.func_143045_a();
		}
	}

}