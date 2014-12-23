namespace DotCraftServer.nWorld
{

	public class ColorizerFoliage
	{
	/// <summary> Color buffer for foliage  </summary>
		private static int[] foliageBuffer = new int[65536];
		

		public static int[] FoliageBiomeColorizer
		{
			set
			{
				foliageBuffer = value;
			}
		}

///    
///     <summary> * Gets foliage color from temperature and humidity. Args: temperature, humidity </summary>
///     
		public static int getFoliageColor(double p_77470_0_, double p_77470_2_)
		{
			p_77470_2_ *= p_77470_0_;
			int var4 = (int)((1.0D - p_77470_0_) * 255.0D);
			int var5 = (int)((1.0D - p_77470_2_) * 255.0D);
			return foliageBuffer[var5 << 8 | var4];
		}

///    
///     <summary> * Gets the foliage color for pine type (metadata 1) trees </summary>
///     
		public static int FoliageColorPine
		{
			get
			{
				return 6396257;
			}
		}

///    
///     <summary> * Gets the foliage color for birch type (metadata 2) trees </summary>
///     
		public static int FoliageColorBirch
		{
			get
			{
				return 8431445;
			}
		}

		public static int FoliageColorBasic
		{
			get
			{
				return 4764952;
			}
		}
	}

}