using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nWorld;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockPressurePlate : BlockBasePressurePlate
	{
		private BlockPressurePlate.Sensitivity field_150069_a;
		

		protected internal BlockPressurePlate(string p_i45418_1_, Material p_i45418_2_, BlockPressurePlate.Sensitivity p_i45418_3_) : base(p_i45418_1_, p_i45418_2_)
		{
			this.field_150069_a = p_i45418_3_;
		}

		protected internal override int func_150066_d(int p_150066_1_)
		{
			return p_150066_1_ > 0 ? 1 : 0;
		}

		protected internal override int func_150060_c(int p_150060_1_)
		{
			return p_150060_1_ == 1 ? 15 : 0;
		}

		protected internal override int func_150065_e(World p_150065_1_, int p_150065_2_, int p_150065_3_, int p_150065_4_)
		{
			IList var5 = null;

			if (this.field_150069_a == BlockPressurePlate.Sensitivity.everything)
			{
				var5 = p_150065_1_.getEntitiesWithinAABBExcludingEntity((Entity)null, this.func_150061_a(p_150065_2_, p_150065_3_, p_150065_4_));
			}

			if (this.field_150069_a == BlockPressurePlate.Sensitivity.mobs)
			{
				var5 = p_150065_1_.getEntitiesWithinAABB(typeof(EntityLivingBase), this.func_150061_a(p_150065_2_, p_150065_3_, p_150065_4_));
			}

			if (this.field_150069_a == BlockPressurePlate.Sensitivity.players)
			{
				var5 = p_150065_1_.getEntitiesWithinAABB(typeof(EntityPlayer), this.func_150061_a(p_150065_2_, p_150065_3_, p_150065_4_));
			}

			if (var5 != null && var5.Count != 0)
			{
				IEnumerator var6 = var5.GetEnumerator();

				while (var6.MoveNext())
				{
					Entity var7 = (Entity)var6.Current;

					if (!var7.doesEntityNotTriggerPressurePlate())
					{
						return 15;
					}
				}
			}

			return 0;
		}

		public enum Sensitivity
		{
			everything = 0,
			mobs = 1,
			players = 2
		}
	}
}