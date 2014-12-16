namespace DotCraftCore.Stats
{

	using ScoreDummyCriteria = DotCraftCore.Scoreboard.ScoreDummyCriteria;

	public class ObjectiveStat : ScoreDummyCriteria
	{
		private readonly StatBase field_151459_g;
		

		public ObjectiveStat(StatBase p_i45483_1_) : base(p_i45483_1_.statId)
		{
			this.field_151459_g = p_i45483_1_;
		}
	}

}