namespace DotCraftCore.nProfiler
{

	public interface IPlayerUsage
	{
		void addServerStatsToSnooper(PlayerUsageSnooper p_70000_1_);

		void addServerTypeToSnooper(PlayerUsageSnooper p_70001_1_);

///    
///     <summary> * Returns whether snooping is enabled or not. </summary>
///     
		bool isSnooperEnabled() {get;}
	}

}