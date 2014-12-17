using System;
using System.Collections;
using System.Text;

namespace DotCraftCore.nWorld.nGen
{

	using Block = DotCraftCore.nBlock.Block;
	using Blocks = DotCraftCore.init.Blocks;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using BiomeGenBase = DotCraftCore.nWorld.nBiome.BiomeGenBase;

	public class FlatGeneratorInfo
	{
	/// <summary> List of layers on this preset.  </summary>
		private readonly IList flatLayers = new ArrayList();

	/// <summary> List of world features enabled on this preset.  </summary>
		private readonly IDictionary worldFeatures = new Hashtable();
		private int biomeToUse;
		

///    
///     <summary> * Return the biome used on this preset. </summary>
///     
		public virtual int Biome
		{
			get
			{
				return this.biomeToUse;
			}
			set
			{
				this.biomeToUse = value;
			}
		}

///    
///     <summary> * Set the biome used on this preset. </summary>
///     

///    
///     <summary> * Return the list of world features enabled on this preset. </summary>
///     
		public virtual IDictionary WorldFeatures
		{
			get
			{
				return this.worldFeatures;
			}
		}

///    
///     <summary> * Return the list of layers on this preset. </summary>
///     
		public virtual IList FlatLayers
		{
			get
			{
				return this.flatLayers;
			}
		}

		public virtual void func_82645_d()
		{
			int var1 = 0;
			FlatLayerInfo var3;

			for (IEnumerator var2 = this.flatLayers.GetEnumerator(); var2.MoveNext(); var1 += var3.LayerCount)
			{
				var3 = (FlatLayerInfo)var2.Current;
				var3.MinY = var1;
			}
		}

		public override string ToString()
		{
			StringBuilder var1 = new StringBuilder();
			var1.Append(2);
			var1.Append(";");
			int var2;

			for (var2 = 0; var2 < this.flatLayers.Count; ++var2)
			{
				if (var2 > 0)
				{
					var1.Append(",");
				}

				var1.Append(((FlatLayerInfo)this.flatLayers.get(var2)).ToString());
			}

			var1.Append(";");
			var1.Append(this.biomeToUse);

			if (!this.worldFeatures.Count == 0)
			{
				var1.Append(";");
				var2 = 0;
				IEnumerator var3 = this.worldFeatures.GetEnumerator();

				while (var3.MoveNext())
				{
					Entry var4 = (Entry)var3.Current;

					if (var2++ > 0)
					{
						var1.Append(",");
					}

					var1.Append(((string)var4.Key).ToLower());
					IDictionary var5 = (IDictionary)var4.Value;

					if (!var5.Count == 0)
					{
						var1.Append("(");
						int var6 = 0;
						IEnumerator var7 = var5.GetEnumerator();

						while (var7.MoveNext())
						{
							Entry var8 = (Entry)var7.Current;

							if (var6++ > 0)
							{
								var1.Append(" ");
							}

							var1.Append((string)var8.Key);
							var1.Append("=");
							var1.Append((string)var8.Value);
						}

						var1.Append(")");
					}
				}
			}
			else
			{
				var1.Append(";");
			}

			return var1.ToString();
		}

		private static FlatLayerInfo func_82646_a(string p_82646_0_, int p_82646_1_)
		{
			string[] var2 = p_82646_0_.Split("x", 2);
			int var3 = 1;
			int var5 = 0;

			if (var2.Length == 2)
			{
				try
				{
					var3 = Convert.ToInt32(var2[0]);

					if (p_82646_1_ + var3 >= 256)
					{
						var3 = 256 - p_82646_1_;
					}

					if (var3 < 0)
					{
						var3 = 0;
					}
				}
				catch (Exception var7)
				{
					return null;
				}
			}

			int var4;

			try
			{
				string var6 = var2[var2.Length - 1];
				var2 = var6.Split(":", 2);
				var4 = Convert.ToInt32(var2[0]);

				if (var2.Length > 1)
				{
					var5 = Convert.ToInt32(var2[1]);
				}

				if (Block.getBlockById(var4) == Blocks.air)
				{
					var4 = 0;
					var5 = 0;
				}

				if (var5 < 0 || var5 > 15)
				{
					var5 = 0;
				}
			}
			catch (Exception var8)
			{
				return null;
			}

			FlatLayerInfo var9 = new FlatLayerInfo(var3, Block.getBlockById(var4), var5);
			var9.MinY = p_82646_1_;
			return var9;
		}

		private static IList func_82652_b(string p_82652_0_)
		{
			if (p_82652_0_ != null && p_82652_0_.Length >= 1)
			{
				ArrayList var1 = new ArrayList();
				string[] var2 = StringHelperClass.StringSplit(p_82652_0_, ",", true);
				int var3 = 0;
				string[] var4 = var2;
				int var5 = var2.Length;

				for (int var6 = 0; var6 < var5; ++var6)
				{
					string var7 = var4[var6];
					FlatLayerInfo var8 = func_82646_a(var7, var3);

					if (var8 == null)
					{
						return null;
					}

					var1.Add(var8);
					var3 += var8.LayerCount;
				}

				return var1;
			}
			else
			{
				return null;
			}
		}

		public static FlatGeneratorInfo createFlatGeneratorFromString(string p_82651_0_)
		{
			if (p_82651_0_ == null)
			{
				return DefaultFlatGenerator;
			}
			else
			{
				string[] var1 = StringHelperClass.StringSplit(p_82651_0_, ";", false);
				int var2 = var1.Length == 1 ? 0 : MathHelper.parseIntWithDefault(var1[0], 0);

				if (var2 >= 0 && var2 <= 2)
				{
					FlatGeneratorInfo var3 = new FlatGeneratorInfo();
					int var4 = var1.Length == 1 ? 0 : 1;
					IList var5 = func_82652_b(var1[var4++]);

					if (var5 != null && !var5.Count == 0)
					{
						var3.FlatLayers.AddRange(var5);
						var3.func_82645_d();
						int var6 = BiomeGenBase.plains.biomeID;

						if (var2 > 0 && var1.Length > var4)
						{
							var6 = MathHelper.parseIntWithDefault(var1[var4++], var6);
						}

						var3.Biome = var6;

						if (var2 > 0 && var1.Length > var4)
						{
							string[] var7 = var1[var4++].ToLower().Split(",");
							string[] var8 = var7;
							int var9 = var7.Length;

							for (int var10 = 0; var10 < var9; ++var10)
							{
								string var11 = var8[var10];
								string[] var12 = var11.Split("\\(", 2);
								Hashtable var13 = new Hashtable();

								if (var12[0].Length > 0)
								{
									var3.WorldFeatures.Add(var12[0], var13);

									if (var12.Length > 1 && var12[1].EndsWith(")") && var12[1].Length > 1)
									{
										string[] var14 = var12[1].Substring(0, var12[1].Length - 1).Split(" ");

										for (int var15 = 0; var15 < var14.Length; ++var15)
										{
											string[] var16 = var14[var15].Split("=", 2);

											if (var16.Length == 2)
											{
												var13.Add(var16[0], var16[1]);
											}
										}
									}
								}
							}
						}
						else
						{
							var3.WorldFeatures.Add("village", new Hashtable());
						}

						return var3;
					}
					else
					{
						return DefaultFlatGenerator;
					}
				}
				else
				{
					return DefaultFlatGenerator;
				}
			}
		}

		public static FlatGeneratorInfo DefaultFlatGenerator
		{
			get
			{
				FlatGeneratorInfo var0 = new FlatGeneratorInfo();
				var0.Biome = BiomeGenBase.plains.biomeID;
				var0.FlatLayers.Add(new FlatLayerInfo(1, Blocks.bedrock));
				var0.FlatLayers.Add(new FlatLayerInfo(2, Blocks.dirt));
				var0.FlatLayers.Add(new FlatLayerInfo(1, Blocks.grass));
				var0.func_82645_d();
				var0.WorldFeatures.Add("village", new Hashtable());
				return var0;
			}
		}
	}

}