using System.Collections;

namespace DotCraftCore.Scoreboard
{


	public class ScoreDummyCriteria : IScoreObjectiveCriteria
	{
		private readonly string field_96644_g;
		

		public ScoreDummyCriteria(string p_i2311_1_)
		{
			this.field_96644_g = p_i2311_1_;
			IScoreObjectiveCriteria.field_96643_a.put(p_i2311_1_, this);
		}

		public virtual string func_96636_a()
		{
			return this.field_96644_g;
		}

		public virtual int func_96635_a(IList p_96635_1_)
		{
			return 0;
		}

		public virtual bool isReadOnly()
		{
			get
			{
				return false;
			}
		}
	}

}