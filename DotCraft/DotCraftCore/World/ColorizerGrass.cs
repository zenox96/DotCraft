namespace DotCraftCore.World
{

	public class ColorizerGrass
	{
	/// <summary> Color buffer for grass  </summary>
		private static int[] grassBuffer = new int[65536];
		

		public static int[] GrassBiomeColorizer
		{
			set
			{
				grassBuffer = value;
			}
		}

///    
///     <summary> * Gets grass color from temperature and humidity. Args: temperature, humidity </summary>
///     
		public static int getGrassColor(double p_77480_0_, double p_77480_2_)
		{
			p_77480_2_ *= p_77480_0_;
			int var4 = (int)((1.0D - p_77480_0_) * 255.0D);
			int var5 = (int)((1.0D - p_77480_2_) * 255.0D);
			return grassBuffer[var5 << 8 | var4];
		}
	}

}