using System;
using System.Collections;

namespace DotCraftCore.Profiler
{

	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class Profiler
	{
		private static readonly Logger logger = LogManager.Logger;

	/// <summary> List of parent sections  </summary>
		private readonly IList sectionList = new ArrayList();

	/// <summary> List of timestamps (System.nanoTime)  </summary>
		private readonly IList timestampList = new ArrayList();

	/// <summary> Flag profiling enabled  </summary>
		public bool profilingEnabled;

	/// <summary> Current profiling section  </summary>
		private string profilingSection = "";

	/// <summary> Profiling map  </summary>
		private readonly IDictionary profilingMap = new Hashtable();
		

///    
///     <summary> * Clear profiling. </summary>
///     
		public virtual void clearProfiling()
		{
			this.profilingMap.Clear();
			this.profilingSection = "";
			this.sectionList.Clear();
		}

///    
///     <summary> * Start section </summary>
///     
		public virtual void startSection(string p_76320_1_)
		{
			if (this.profilingEnabled)
			{
				if (this.profilingSection.Length > 0)
				{
					this.profilingSection = this.profilingSection + ".";
				}

				this.profilingSection = this.profilingSection + p_76320_1_;
				this.sectionList.Add(this.profilingSection);
				this.timestampList.Add(Convert.ToInt64(System.nanoTime()));
			}
		}

///    
///     <summary> * End section </summary>
///     
		public virtual void endSection()
		{
			if (this.profilingEnabled)
			{
				long var1 = System.nanoTime();
				long var3 = (long)((long?)this.timestampList.remove(this.timestampList.Count - 1));
				this.sectionList.Remove(this.sectionList.Count - 1);
				long var5 = var1 - var3;

				if (this.profilingMap.ContainsKey(this.profilingSection))
				{
					this.profilingMap.Add(this.profilingSection, Convert.ToInt64((long)((long?)this.profilingMap.get(this.profilingSection)) + var5));
				}
				else
				{
					this.profilingMap.Add(this.profilingSection, Convert.ToInt64(var5));
				}

				if (var5 > 100000000L)
				{
					logger.warn("Something\'s taking too long! \'" + this.profilingSection + "\' took aprox " + (double)var5 / 1000000.0D + " ms");
				}

				this.profilingSection = !this.sectionList.Count == 0 ? (string)this.sectionList.get(this.sectionList.Count - 1) : "";
			}
		}

///    
///     <summary> * Get profiling data </summary>
///     
		public virtual IList getProfilingData(string p_76321_1_)
		{
			if (!this.profilingEnabled)
			{
				return null;
			}
			else
			{
				long var3 = this.profilingMap.ContainsKey("root") ? (long)((long?)this.profilingMap.get("root")) : 0L;
				long var5 = this.profilingMap.ContainsKey(p_76321_1_) ? (long)((long?)this.profilingMap.get(p_76321_1_)) : -1L;
				ArrayList var7 = new ArrayList();

				if (p_76321_1_.Length > 0)
				{
					p_76321_1_ = p_76321_1_ + ".";
				}

				long var8 = 0L;
				IEnumerator var10 = this.profilingMap.Keys.GetEnumerator();

				while (var10.MoveNext())
				{
					string var11 = (string)var10.Current;

					if (var11.Length > p_76321_1_.Length && var11.StartsWith(p_76321_1_) && var11.IndexOf(".", p_76321_1_.Length + 1) < 0)
					{
						var8 += (long)((long?)this.profilingMap.get(var11));
					}
				}

				float var20 = (float)var8;

				if (var8 < var5)
				{
					var8 = var5;
				}

				if (var3 < var8)
				{
					var3 = var8;
				}

				IEnumerator var21 = this.profilingMap.Keys.GetEnumerator();
				string var12;

				while (var21.MoveNext())
				{
					var12 = (string)var21.Current;

					if (var12.Length > p_76321_1_.Length && var12.StartsWith(p_76321_1_) && var12.IndexOf(".", p_76321_1_.Length + 1) < 0)
					{
						long var13 = (long)((long?)this.profilingMap.get(var12));
						double var15 = (double)var13 * 100.0D / (double)var8;
						double var17 = (double)var13 * 100.0D / (double)var3;
						string var19 = var12.Substring(p_76321_1_.Length);
						var7.Add(new Profiler.Result(var19, var15, var17));
					}
				}

				var21 = this.profilingMap.Keys.GetEnumerator();

				while (var21.MoveNext())
				{
					var12 = (string)var21.Current;
					this.profilingMap.Add(var12, Convert.ToInt64((long)((long?)this.profilingMap.get(var12)) * 999L / 1000L));
				}

				if ((float)var8 > var20)
				{
					var7.Add(new Profiler.Result("unspecified", (double)((float)var8 - var20) * 100.0D / (double)var8, (double)((float)var8 - var20) * 100.0D / (double)var3));
				}

				Collections.sort(var7);
				var7.Insert(0, new Profiler.Result(p_76321_1_, 100.0D, (double)var8 * 100.0D / (double)var3));
				return var7;
			}
		}

///    
///     <summary> * End current section and start a new section </summary>
///     
		public virtual void endStartSection(string p_76318_1_)
		{
			this.endSection();
			this.startSection(p_76318_1_);
		}

		public virtual string NameOfLastSection
		{
			get
			{
				return this.sectionList.Count == 0 ? "[UNKNOWN]" : (string)this.sectionList.get(this.sectionList.Count - 1);
			}
		}

		public sealed class Result : Comparable
		{
			public double field_76332_a;
			public double field_76330_b;
			public string field_76331_c;
			

			public Result(string p_i1554_1_, double p_i1554_2_, double p_i1554_4_)
			{
				this.field_76331_c = p_i1554_1_;
				this.field_76332_a = p_i1554_2_;
				this.field_76330_b = p_i1554_4_;
			}

			public int compareTo(Profiler.Result p_compareTo_1_)
			{
				return p_compareTo_1_.field_76332_a < this.field_76332_a ? -1 : (p_compareTo_1_.field_76332_a > this.field_76332_a ? 1 : p_compareTo_1_.field_76331_c.CompareTo(this.field_76331_c));
			}

			public int func_76329_a()
			{
				return (this.field_76331_c.GetHashCode() & 11184810) + 4473924;
			}

			public int compareTo(object p_compareTo_1_)
			{
				return this.CompareTo((Profiler.Result)p_compareTo_1_);
			}
		}
	}

}