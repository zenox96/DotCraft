namespace DotCraftCore.nStats
{

	using IChatComponent = DotCraftCore.nUtil.IChatComponent;

	public class StatBasic : StatBase
	{
		

		public StatBasic(string p_i45303_1_, IChatComponent p_i45303_2_, IStatType p_i45303_3_) : base(p_i45303_1_, p_i45303_2_, p_i45303_3_)
		{
		}

		public StatBasic(string p_i45304_1_, IChatComponent p_i45304_2_) : base(p_i45304_1_, p_i45304_2_)
		{
		}

///    
///     <summary> * Register the stat into StatList. </summary>
///     
		public override StatBase registerStat()
		{
			base.registerStat();
			StatList.generalStats.Add(this);
			return this;
		}
	}

}