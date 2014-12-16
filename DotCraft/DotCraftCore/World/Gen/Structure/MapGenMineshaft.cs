using System;
using System.Collections;

namespace DotCraftCore.World.Gen.Structure
{

	using MathHelper = DotCraftCore.Util.MathHelper;

	public class MapGenMineshaft : MapGenStructure
	{
		private double field_82673_e = 0.004D;
		

		public MapGenMineshaft()
		{
		}

		public override string func_143025_a()
		{
			return "Mineshaft";
		}

		public MapGenMineshaft(IDictionary p_i2034_1_)
		{
			IEnumerator var2 = p_i2034_1_.GetEnumerator();

			while (var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;

				if (((string)var3.Key).Equals("chance"))
				{
					this.field_82673_e = MathHelper.parseDoubleWithDefault((string)var3.Value, this.field_82673_e);
				}
			}
		}

		protected internal override bool canSpawnStructureAtCoords(int p_75047_1_, int p_75047_2_)
		{
			return this.rand.NextDouble() < this.field_82673_e && this.rand.Next(80) < Math.Max(Math.Abs(p_75047_1_), Math.Abs(p_75047_2_));
		}

		protected internal override StructureStart getStructureStart(int p_75049_1_, int p_75049_2_)
		{
			return new StructureMineshaftStart(this.worldObj, this.rand, p_75049_1_, p_75049_2_);
		}
	}

}