namespace DotCraftCore.Scoreboard
{

	public abstract class Team
	{
		private const string __OBFID = "CL_00000621";

///    
///     <summary> * Same as == </summary>
///     
		public virtual bool isSameTeam(Team p_142054_1_)
		{
			return p_142054_1_ == null ? false : this == p_142054_1_;
		}

///    
///     <summary> * Retrieve the name by which this team is registered in the scoreboard </summary>
///     
		public abstract string RegisteredName {get;}

		public abstract string func_142053_d(string p_142053_1_);

		public abstract bool func_98297_h();

		public abstract bool AllowFriendlyFire {get;}
	}

}