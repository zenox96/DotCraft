using System.Collections;

namespace DotCraftCore.Scoreboard
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class ScoreHealthCriteria : ScoreDummyCriteria
	{
		private const string __OBFID = "CL_00000623";

		public ScoreHealthCriteria(string p_i2312_1_) : base(p_i2312_1_)
		{
		}

		public override int func_96635_a(IList p_96635_1_)
		{
			float var2 = 0.0F;
			EntityPlayer var4;

			for(IEnumerator var3 = p_96635_1_.GetEnumerator(); var3.MoveNext(); var2 += var4.Health + var4.AbsorptionAmount)
			{
				var4 = (EntityPlayer)var3.Current;
			}

			if(p_96635_1_.Count > 0)
			{
				var2 /= (float)p_96635_1_.Count;
			}

			return MathHelper.ceiling_float_int(var2);
		}

		public override bool isReadOnly()
		{
			get
			{
				return true;
			}
		}
	}

}