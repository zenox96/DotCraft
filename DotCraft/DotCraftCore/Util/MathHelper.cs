using System;

namespace DotCraftCore.Util
{


	public class MathHelper
	{
///    
///     <summary> * A table of sin values computed from 0 (inclusive) to 2*pi (exclusive), with steps of 2*PI / 65536. </summary>
///     
		private static float[] SIN_TABLE = new float[65536];

///    
///     <summary> * Though it looks like an array, this is really more like a mapping.  Key (index of this array) is the upper 5 bits
///     * of the result of multiplying a 32-bit unsigned integer by the B(2, 5) De Bruijn sequence 0x077CB531.  Value
///     * (value stored in the array) is the unique index (from the right) of the leftmost one-bit in a 32-bit unsigned
///     * integer that can cause the upper 5 bits to get that value.  Used for highly optimized "find the log-base-2 of
///     * this number" calculations. </summary>
///     
		private static readonly int[] multiplyDeBruijnBitPosition;
		private const string __OBFID = "CL_00001496";

///    
///     <summary> * sin looked up in a table </summary>
///     
		public static float sin(float p_76126_0_)
		{
			return SIN_TABLE[(int)(p_76126_0_ * 10430.378F) & 65535];
		}

///    
///     <summary> * cos looked up in the sin table with the appropriate offset </summary>
///     
		public static float cos(float p_76134_0_)
		{
			return SIN_TABLE[(int)(p_76134_0_ * 10430.378F + 16384.0F) & 65535];
		}

		public static float sqrt_float(float p_76129_0_)
		{
			return(float)Math.Sqrt((double)p_76129_0_);
		}

		public static float sqrt_double(double p_76133_0_)
		{
			return(float)Math.Sqrt(p_76133_0_);
		}

///    
///     <summary> * Returns the greatest integer less than or equal to the float argument </summary>
///     
		public static int floor_float(float p_76141_0_)
		{
			int var1 = (int)p_76141_0_;
			return p_76141_0_ < (float)var1 ? var1 - 1 : var1;
		}

///    
///     <summary> * returns par0 cast as an int, and no greater than Integer.MAX_VALUE-1024 </summary>
///     
		public static int truncateDoubleToInt(double p_76140_0_)
		{
			return(int)(p_76140_0_ + 1024.0D) - 1024;
		}

///    
///     <summary> * Returns the greatest integer less than or equal to the double argument </summary>
///     
		public static int floor_double(double p_76128_0_)
		{
			int var2 = (int)p_76128_0_;
			return p_76128_0_ < (double)var2 ? var2 - 1 : var2;
		}

///    
///     <summary> * Long version of floor_double </summary>
///     
		public static long floor_double_long(double p_76124_0_)
		{
			long var2 = (long)p_76124_0_;
			return p_76124_0_ < (double)var2 ? var2 - 1L : var2;
		}

		public static int func_154353_e(double p_154353_0_)
		{
			return(int)(p_154353_0_ >= 0.0D ? p_154353_0_ : -p_154353_0_ + 1.0D);
		}

		public static float abs(float p_76135_0_)
		{
			return p_76135_0_ >= 0.0F ? p_76135_0_ : -p_76135_0_;
		}

///    
///     <summary> * Returns the unsigned value of an int. </summary>
///     
		public static int abs_int(int p_76130_0_)
		{
			return p_76130_0_ >= 0 ? p_76130_0_ : -p_76130_0_;
		}

		public static int ceiling_float_int(float p_76123_0_)
		{
			int var1 = (int)p_76123_0_;
			return p_76123_0_ > (float)var1 ? var1 + 1 : var1;
		}

		public static int ceiling_double_int(double p_76143_0_)
		{
			int var2 = (int)p_76143_0_;
			return p_76143_0_ > (double)var2 ? var2 + 1 : var2;
		}

///    
///     <summary> * Returns the value of the first parameter, clamped to be within the lower and upper limits given by the second and
///     * third parameters. </summary>
///     
		public static int clamp_int(int p_76125_0_, int p_76125_1_, int p_76125_2_)
		{
			return p_76125_0_ < p_76125_1_ ? p_76125_1_ : (p_76125_0_ > p_76125_2_ ? p_76125_2_ : p_76125_0_);
		}

///    
///     <summary> * Returns the value of the first parameter, clamped to be within the lower and upper limits given by the second and
///     * third parameters </summary>
///     
		public static float clamp_float(float p_76131_0_, float p_76131_1_, float p_76131_2_)
		{
			return p_76131_0_ < p_76131_1_ ? p_76131_1_ : (p_76131_0_ > p_76131_2_ ? p_76131_2_ : p_76131_0_);
		}

		public static double clamp_double(double p_151237_0_, double p_151237_2_, double p_151237_4_)
		{
			return p_151237_0_ < p_151237_2_ ? p_151237_2_ : (p_151237_0_ > p_151237_4_ ? p_151237_4_ : p_151237_0_);
		}

		public static double denormalizeClamp(double p_151238_0_, double p_151238_2_, double p_151238_4_)
		{
			return p_151238_4_ < 0.0D ? p_151238_0_ : (p_151238_4_ > 1.0D ? p_151238_2_ : p_151238_0_ + (p_151238_2_ - p_151238_0_) * p_151238_4_);
		}

///    
///     <summary> * Maximum of the absolute value of two numbers. </summary>
///     
		public static double abs_max(double p_76132_0_, double p_76132_2_)
		{
			if(p_76132_0_ < 0.0D)
			{
				p_76132_0_ = -p_76132_0_;
			}

			if(p_76132_2_ < 0.0D)
			{
				p_76132_2_ = -p_76132_2_;
			}

			return p_76132_0_ > p_76132_2_ ? p_76132_0_ : p_76132_2_;
		}

///    
///     <summary> * Buckets an integer with specifed bucket sizes.  Args: i, bucketSize </summary>
///     
		public static int bucketInt(int p_76137_0_, int p_76137_1_)
		{
			return p_76137_0_ < 0 ? -((-p_76137_0_ - 1) / p_76137_1_) - 1 : p_76137_0_ / p_76137_1_;
		}

///    
///     <summary> * Tests if a string is null or of length zero </summary>
///     
		public static bool stringNullOrLengthZero(string p_76139_0_)
		{
			return p_76139_0_ == null || p_76139_0_.Length == 0;
		}

		public static int getRandomIntegerInRange(Random p_76136_0_, int p_76136_1_, int p_76136_2_)
		{
			return p_76136_1_ >= p_76136_2_ ? p_76136_1_ : p_76136_0_.Next(p_76136_2_ - p_76136_1_ + 1) + p_76136_1_;
		}

		public static float randomFloatClamp(Random p_151240_0_, float p_151240_1_, float p_151240_2_)
		{
			return p_151240_1_ >= p_151240_2_ ? p_151240_1_ : p_151240_0_.nextFloat() * (p_151240_2_ - p_151240_1_) + p_151240_1_;
		}

		public static double getRandomDoubleInRange(Random p_82716_0_, double p_82716_1_, double p_82716_3_)
		{
			return p_82716_1_ >= p_82716_3_ ? p_82716_1_ : p_82716_0_.NextDouble() * (p_82716_3_ - p_82716_1_) + p_82716_1_;
		}

		public static double average(long[] p_76127_0_)
		{
			long var1 = 0L;
			long[] var3 = p_76127_0_;
			int var4 = p_76127_0_.Length;

			for(int var5 = 0; var5 < var4; ++var5)
			{
				long var6 = var3[var5];
				var1 += var6;
			}

			return(double)var1 / (double)p_76127_0_.Length;
		}

///    
///     <summary> * the angle is reduced to an angle between -180 and +180 by mod, and a 360 check </summary>
///     
		public static float wrapAngleTo180_float(float p_76142_0_)
		{
			p_76142_0_ %= 360.0F;

			if(p_76142_0_ >= 180.0F)
			{
				p_76142_0_ -= 360.0F;
			}

			if(p_76142_0_ < -180.0F)
			{
				p_76142_0_ += 360.0F;
			}

			return p_76142_0_;
		}

///    
///     <summary> * the angle is reduced to an angle between -180 and +180 by mod, and a 360 check </summary>
///     
		public static double wrapAngleTo180_double(double p_76138_0_)
		{
			p_76138_0_ %= 360.0D;

			if(p_76138_0_ >= 180.0D)
			{
				p_76138_0_ -= 360.0D;
			}

			if(p_76138_0_ < -180.0D)
			{
				p_76138_0_ += 360.0D;
			}

			return p_76138_0_;
		}

///    
///     <summary> * parses the string as integer or returns the second parameter if it fails </summary>
///     
		public static int parseIntWithDefault(string p_82715_0_, int p_82715_1_)
		{
			int var2 = p_82715_1_;

			try
			{
				var2 = Convert.ToInt32(p_82715_0_);
			}
			catch (Exception var4)
			{
				;
			}

			return var2;
		}

///    
///     <summary> * parses the string as integer or returns the second parameter if it fails. this value is capped to par2 </summary>
///     
		public static int parseIntWithDefaultAndMax(string p_82714_0_, int p_82714_1_, int p_82714_2_)
		{
			int var3 = p_82714_1_;

			try
			{
				var3 = Convert.ToInt32(p_82714_0_);
			}
			catch (Exception var5)
			{
				;
			}

			if(var3 < p_82714_2_)
			{
				var3 = p_82714_2_;
			}

			return var3;
		}

///    
///     <summary> * parses the string as double or returns the second parameter if it fails. </summary>
///     
		public static double parseDoubleWithDefault(string p_82712_0_, double p_82712_1_)
		{
			double var3 = p_82712_1_;

			try
			{
				var3 = Convert.ToDouble(p_82712_0_);
			}
			catch (Exception var6)
			{
				;
			}

			return var3;
		}

		public static double parseDoubleWithDefaultAndMax(string p_82713_0_, double p_82713_1_, double p_82713_3_)
		{
			double var5 = p_82713_1_;

			try
			{
				var5 = Convert.ToDouble(p_82713_0_);
			}
			catch (Exception var8)
			{
				;
			}

			if(var5 < p_82713_3_)
			{
				var5 = p_82713_3_;
			}

			return var5;
		}

///    
///     <summary> * Returns the input value rounded up to the next highest power of two. </summary>
///     
		public static int roundUpToPowerOfTwo(int p_151236_0_)
		{
			int var1 = p_151236_0_ - 1;
			var1 |= var1 >> 1;
			var1 |= var1 >> 2;
			var1 |= var1 >> 4;
			var1 |= var1 >> 8;
			var1 |= var1 >> 16;
			return var1 + 1;
		}

///    
///     <summary> * Is the given value a power of two?  (1, 2, 4, 8, 16, ...) </summary>
///     
		private static bool isPowerOfTwo(int p_151235_0_)
		{
			return p_151235_0_ != 0 && (p_151235_0_ & p_151235_0_ - 1) == 0;
		}

///    
///     <summary> * Uses a B(2, 5) De Bruijn sequence and a lookup table to efficiently calculate the log-base-two of the given
///     * value.  Optimized for cases where the input value is a power-of-two.  If the input value is not a power-of-two,
///     * then subtract 1 from the return value. </summary>
///     
		private static int calculateLogBaseTwoDeBruijn(int p_151241_0_)
		{
			p_151241_0_ = isPowerOfTwo(p_151241_0_) ? p_151241_0_ : roundUpToPowerOfTwo(p_151241_0_);
			return multiplyDeBruijnBitPosition[(int)((long)p_151241_0_ * 125613361L >> 27) & 31];
		}

///    
///     <summary> * Efficiently calculates the floor of the base-2 log of an integer value.  This is effectively the index of the
///     * highest bit that is set.  For example, if the number in binary is 0...100101, this will return 5. </summary>
///     
		public static int calculateLogBaseTwo(int p_151239_0_)
		{
			return calculateLogBaseTwoDeBruijn(p_151239_0_) - (isPowerOfTwo(p_151239_0_) ? 0 : 1);
		}

		public static int func_154354_b(int p_154354_0_, int p_154354_1_)
		{
			if(p_154354_1_ == 0)
			{
				return 0;
			}
			else
			{
				if(p_154354_0_ < 0)
				{
					p_154354_1_ *= -1;
				}

				int var2 = p_154354_0_ % p_154354_1_;
				return var2 == 0 ? p_154354_0_ : p_154354_0_ + p_154354_1_ - var2;
			}
		}

		static MathHelper()
		{
			for(int var0 = 0; var0 < 65536; ++var0)
			{
				SIN_TABLE[var0] = (float)Math.Sin((double)var0 * Math.PI * 2.0D / 65536.0D);
			}

			multiplyDeBruijnBitPosition = new int[] {0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9};
		}
	}

}