using System.Collections;

namespace DotCraftCore.Scoreboard
{


	public class ScorePlayerTeam : Team
	{
		private readonly Scoreboard theScoreboard;
		private readonly string field_96675_b;

	/// <summary> A set of all team member usernames.  </summary>
		private readonly Set membershipSet = new HashSet();
		private string teamNameSPT;
		private string namePrefixSPT = "";
		private string colorSuffix = "";
		private bool allowFriendlyFire = true;
		private bool canSeeFriendlyInvisibles = true;
		private const string __OBFID = "CL_00000616";

		public ScorePlayerTeam(Scoreboard p_i2308_1_, string p_i2308_2_)
		{
			this.theScoreboard = p_i2308_1_;
			this.field_96675_b = p_i2308_2_;
			this.teamNameSPT = p_i2308_2_;
		}

///    
///     <summary> * Retrieve the name by which this team is registered in the scoreboard </summary>
///     
		public override string RegisteredName
		{
			get
			{
				return this.field_96675_b;
			}
		}

		public virtual string func_96669_c()
		{
			return this.teamNameSPT;
		}

		public virtual string TeamName
		{
			set
			{
				if(value == null)
				{
					throw new System.ArgumentException("Name cannot be null");
				}
				else
				{
					this.teamNameSPT = value;
					this.theScoreboard.func_96538_b(this);
				}
			}
		}

		public virtual ICollection MembershipCollection
		{
			get
			{
				return this.membershipSet;
			}
		}

///    
///     <summary> * Returns the color prefix for the player's team name </summary>
///     
		public virtual string ColorPrefix
		{
			get
			{
				return this.namePrefixSPT;
			}
		}

		public virtual string NamePrefix
		{
			set
			{
				if(value == null)
				{
					throw new System.ArgumentException("Prefix cannot be null");
				}
				else
				{
					this.namePrefixSPT = value;
					this.theScoreboard.func_96538_b(this);
				}
			}
		}

///    
///     <summary> * Returns the color suffix for the player's team name </summary>
///     
		public virtual string ColorSuffix
		{
			get
			{
				return this.colorSuffix;
			}
		}

		public virtual string NameSuffix
		{
			set
			{
				if(value == null)
				{
					throw new System.ArgumentException("Suffix cannot be null");
				}
				else
				{
					this.colorSuffix = value;
					this.theScoreboard.func_96538_b(this);
				}
			}
		}

		public override string func_142053_d(string p_142053_1_)
		{
			return this.ColorPrefix + p_142053_1_ + this.ColorSuffix;
		}

///    
///     <summary> * Returns the player name including the color prefixes and suffixes </summary>
///     
		public static string formatPlayerName(Team p_96667_0_, string p_96667_1_)
		{
			return p_96667_0_ == null ? p_96667_1_ : p_96667_0_.func_142053_d(p_96667_1_);
		}

		public override bool AllowFriendlyFire
		{
			get
			{
				return this.allowFriendlyFire;
			}
			set
			{
				this.allowFriendlyFire = value;
				this.theScoreboard.func_96538_b(this);
			}
		}


		public override bool func_98297_h()
		{
			return this.canSeeFriendlyInvisibles;
		}

		public virtual bool SeeFriendlyInvisiblesEnabled
		{
			set
			{
				this.canSeeFriendlyInvisibles = value;
				this.theScoreboard.func_96538_b(this);
			}
		}

		public virtual int func_98299_i()
		{
			int var1 = 0;

			if(this.AllowFriendlyFire)
			{
				var1 |= 1;
			}

			if(this.func_98297_h())
			{
				var1 |= 2;
			}

			return var1;
		}

		public virtual void func_98298_a(int p_98298_1_)
		{
			this.AllowFriendlyFire = (p_98298_1_ & 1) > 0;
			this.SeeFriendlyInvisiblesEnabled = (p_98298_1_ & 2) > 0;
		}
	}

}