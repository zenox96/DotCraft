using System.Collections;

namespace DotCraftCore.Scoreboard
{


	public class Score
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		public static final Comparator field_96658_a = new Comparator()
//	{
//		private static final String __OBFID = "CL_00000618";
//		public int compare(Score p_compare_1_, Score p_compare_2_)
//		{
//			return p_compare_1_.getScorePoints() > p_compare_2_.getScorePoints() ? 1 : (p_compare_1_.getScorePoints() < p_compare_2_.getScorePoints() ? -1 : 0);
//		}
//		public int compare(Object p_compare_1_, Object p_compare_2_)
//		{
//			return this.compare((Score)p_compare_1_, (Score)p_compare_2_);
//		}
//	};
		private readonly Scoreboard theScoreboard;
		private readonly ScoreObjective theScoreObjective;
		private readonly string field_96654_d;
		private int field_96655_e;
		private const string __OBFID = "CL_00000617";

		public Score(Scoreboard p_i2309_1_, ScoreObjective p_i2309_2_, string p_i2309_3_)
		{
			this.theScoreboard = p_i2309_1_;
			this.theScoreObjective = p_i2309_2_;
			this.field_96654_d = p_i2309_3_;
		}

		public virtual void func_96649_a(int p_96649_1_)
		{
			if(this.theScoreObjective.Criteria.ReadOnly)
			{
				throw new IllegalStateException("Cannot modify read-only score");
			}
			else
			{
				this.func_96647_c(this.ScorePoints + p_96649_1_);
			}
		}

		public virtual void func_96646_b(int p_96646_1_)
		{
			if(this.theScoreObjective.Criteria.ReadOnly)
			{
				throw new IllegalStateException("Cannot modify read-only score");
			}
			else
			{
				this.func_96647_c(this.ScorePoints - p_96646_1_);
			}
		}

		public virtual void func_96648_a()
		{
			if(this.theScoreObjective.Criteria.ReadOnly)
			{
				throw new IllegalStateException("Cannot modify read-only score");
			}
			else
			{
				this.func_96649_a(1);
			}
		}

		public virtual int ScorePoints
		{
			get
			{
				return this.field_96655_e;
			}
		}

		public virtual void func_96647_c(int p_96647_1_)
		{
			int var2 = this.field_96655_e;
			this.field_96655_e = p_96647_1_;

			if(var2 != p_96647_1_)
			{
				this.func_96650_f().func_96536_a(this);
			}
		}

		public virtual ScoreObjective func_96645_d()
		{
			return this.theScoreObjective;
		}

		public virtual string PlayerName
		{
			get
			{
				return this.field_96654_d;
			}
		}

		public virtual Scoreboard func_96650_f()
		{
			return this.theScoreboard;
		}

		public virtual void func_96651_a(IList p_96651_1_)
		{
			this.func_96647_c(this.theScoreObjective.Criteria.func_96635_a(p_96651_1_));
		}
	}

}