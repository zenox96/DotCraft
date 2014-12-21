using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockFlower : BlockBush
	{
		private static readonly string[,] field_149860_M = new string[,] {{"flower_dandelion"}, {"flower_rose", "flower_blue_orchid", "flower_allium", "flower_houstonia", "flower_tulip_red", "flower_tulip_orange", "flower_tulip_white", "flower_tulip_pink", "flower_oxeye_daisy"}};
		public static readonly string[] field_149859_a = new string[] {"poppy", "blueOrchid", "allium", "houstonia", "tulipRed", "tulipOrange", "tulipWhite", "tulipPink", "oxeyeDaisy"};
		public static readonly string[] field_149858_b = new string[] {"dandelion"};
		private int field_149862_O;
		

		protected internal BlockFlower(int p_i2173_1_) : base(Material.plants)
		{
			this.field_149862_O = p_i2173_1_;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 0; var4 < this.field_149861_N.Length; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

		public static BlockFlower func_149857_e(string p_149857_0_)
		{
			string[] var1 = field_149858_b;
			int var2 = var1.Length;
			int var3;
			string var4;

			for (var3 = 0; var3 < var2; ++var3)
			{
				var4 = var1[var3];

				if (var4.Equals(p_149857_0_))
				{
					return Blocks.yellow_flower;
				}
			}

			var1 = field_149859_a;
			var2 = var1.Length;

			for (var3 = 0; var3 < var2; ++var3)
			{
				var4 = var1[var3];

				if (var4.Equals(p_149857_0_))
				{
					return Blocks.red_flower;
				}
			}

			return null;
		}

		public static int func_149856_f(string p_149856_0_)
		{
			int var1;

			for (var1 = 0; var1 < field_149858_b.Length; ++var1)
			{
				if (field_149858_b[var1].Equals(p_149856_0_))
				{
					return var1;
				}
			}

			for (var1 = 0; var1 < field_149859_a.Length; ++var1)
			{
				if (field_149859_a[var1].Equals(p_149856_0_))
				{
					return var1;
				}
			}

			return 0;
		}
	}

}