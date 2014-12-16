namespace DotCraftCore.Scoreboard
{

	public class ScoreObjective
	{
		private readonly Scoreboard theScoreboard;
		private readonly string name;

	/// <summary> The ScoreObjectiveCriteria for this objetive  </summary>
		private readonly IScoreObjectiveCriteria objectiveCriteria;
		private string displayName;
		

		public ScoreObjective(Scoreboard p_i2307_1_, string p_i2307_2_, IScoreObjectiveCriteria p_i2307_3_)
		{
			this.theScoreboard = p_i2307_1_;
			this.name = p_i2307_2_;
			this.objectiveCriteria = p_i2307_3_;
			this.displayName = p_i2307_2_;
		}

		public virtual Scoreboard Scoreboard
		{
			get
			{
				return this.theScoreboard;
			}
		}

		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		public virtual IScoreObjectiveCriteria Criteria
		{
			get
			{
				return this.objectiveCriteria;
			}
		}

		public virtual string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
				this.theScoreboard.func_96532_b(this);
			}
		}

	}

}