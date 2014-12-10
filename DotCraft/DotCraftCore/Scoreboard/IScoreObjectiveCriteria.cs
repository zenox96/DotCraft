using System.Collections;

namespace DotCraftCore.Scoreboard
{


	public interface IScoreObjectiveCriteria
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Interfaces cannot contain fields in .NET:
//		Map field_96643_a = new HashMap();
//JAVA TO VB & C# CONVERTER TODO TASK: Interfaces cannot contain fields in .NET:
//		IScoreObjectiveCriteria field_96641_b = new ScoreDummyCriteria("dummy");
//JAVA TO VB & C# CONVERTER TODO TASK: Interfaces cannot contain fields in .NET:
//		IScoreObjectiveCriteria deathCount = new ScoreDummyCriteria("deathCount");
//JAVA TO VB & C# CONVERTER TODO TASK: Interfaces cannot contain fields in .NET:
//		IScoreObjectiveCriteria playerKillCount = new ScoreDummyCriteria("playerKillCount");
//JAVA TO VB & C# CONVERTER TODO TASK: Interfaces cannot contain fields in .NET:
//		IScoreObjectiveCriteria totalKillCount = new ScoreDummyCriteria("totalKillCount");
//JAVA TO VB & C# CONVERTER TODO TASK: Interfaces cannot contain fields in .NET:
//		IScoreObjectiveCriteria health = new ScoreHealthCriteria("health");

		string func_96636_a();

		int func_96635_a(IList p_96635_1_);

		bool isReadOnly() {get;}
	}

}