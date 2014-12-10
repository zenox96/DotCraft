using System;

namespace DotCraftCore.Block
{

	
	using Entity = DotCraftCore.entity.Entity;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public class BlockPressurePlateWeighted : BlockBasePressurePlate
	{
		private readonly int field_150068_a;
		private const string __OBFID = "CL_00000334";

		protected internal BlockPressurePlateWeighted(string p_i45436_1_, Material p_i45436_2_, int p_i45436_3_) : base(p_i45436_1_, p_i45436_2_)
		{
			this.field_150068_a = p_i45436_3_;
		}

		protected internal override int func_150065_e(World p_150065_1_, int p_150065_2_, int p_150065_3_, int p_150065_4_)
		{
			int var5 = Math.Min(p_150065_1_.getEntitiesWithinAABB(typeof(Entity), this.func_150061_a(p_150065_2_, p_150065_3_, p_150065_4_)).size(), this.field_150068_a);

			if (var5 <= 0)
			{
				return 0;
			}
			else
			{
				float var6 = (float)Math.Min(this.field_150068_a, var5) / (float)this.field_150068_a;
				return MathHelper.ceiling_float_int(var6 * 15.0F);
			}
		}

		protected internal override int func_150060_c(int p_150060_1_)
		{
			return p_150060_1_;
		}

		protected internal override int func_150066_d(int p_150066_1_)
		{
			return p_150066_1_;
		}

		public override int func_149738_a(World p_149738_1_)
		{
			return 10;
		}
	}

}