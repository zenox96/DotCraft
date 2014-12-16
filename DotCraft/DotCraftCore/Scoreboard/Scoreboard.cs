using System.Collections;

namespace DotCraftCore.Scoreboard
{


	public class Scoreboard
	{
	/// <summary> Map of objective names to ScoreObjective objects.  </summary>
		private readonly IDictionary scoreObjectives = new Hashtable();
		private readonly IDictionary scoreObjectiveCriterias = new Hashtable();
		private readonly IDictionary field_96544_c = new Hashtable();
		private readonly ScoreObjective[] field_96541_d = new ScoreObjective[3];

	/// <summary> Map of teamnames to ScorePlayerTeam instances  </summary>
		private readonly IDictionary teams = new Hashtable();

	/// <summary> Map of usernames to ScorePlayerTeam objects.  </summary>
		private readonly IDictionary teamMemberships = new Hashtable();
		

///    
///     <summary> * Returns a ScoreObjective for the objective name </summary>
///     
		public virtual ScoreObjective getObjective(string p_96518_1_)
		{
			return(ScoreObjective)this.scoreObjectives.get(p_96518_1_);
		}

		public virtual ScoreObjective addScoreObjective(string p_96535_1_, IScoreObjectiveCriteria p_96535_2_)
		{
			ScoreObjective var3 = this.getObjective(p_96535_1_);

			if(var3 != null)
			{
				throw new System.ArgumentException("An objective with the name \'" + p_96535_1_ + "\' already exists!");
			}
			else
			{
				var3 = new ScoreObjective(this, p_96535_1_, p_96535_2_);
				object var4 = (IList)this.scoreObjectiveCriterias.get(p_96535_2_);

				if(var4 == null)
				{
					var4 = new ArrayList();
					this.scoreObjectiveCriterias.Add(p_96535_2_, var4);
				}

				((IList)var4).add(var3);
				this.scoreObjectives.Add(p_96535_1_, var3);
				this.func_96522_a(var3);
				return var3;
			}
		}

		public virtual ICollection func_96520_a(IScoreObjectiveCriteria p_96520_1_)
		{
			ICollection var2 = (ICollection)this.scoreObjectiveCriterias.get(p_96520_1_);
			return var2 == null ? new ArrayList() : new ArrayList(var2);
		}

		public virtual Score func_96529_a(string p_96529_1_, ScoreObjective p_96529_2_)
		{
			object var3 = (IDictionary)this.field_96544_c.get(p_96529_1_);

			if(var3 == null)
			{
				var3 = new Hashtable();
				this.field_96544_c.Add(p_96529_1_, var3);
			}

			Score var4 = (Score)((IDictionary)var3).get(p_96529_2_);

			if(var4 == null)
			{
				var4 = new Score(this, p_96529_2_, p_96529_1_);
				((IDictionary)var3).put(p_96529_2_, var4);
			}

			return var4;
		}

		public virtual ICollection func_96534_i(ScoreObjective p_96534_1_)
		{
			ArrayList var2 = new ArrayList();
			IEnumerator var3 = this.field_96544_c.Values.GetEnumerator();

			while(var3.MoveNext())
			{
				IDictionary var4 = (IDictionary)var3.Current;
				Score var5 = (Score)var4[p_96534_1_];

				if(var5 != null)
				{
					var2.Add(var5);
				}
			}

			Collections.sort(var2, Score.field_96658_a);
			return var2;
		}

		public virtual ICollection ScoreObjectives
		{
			get
			{
				return this.scoreObjectives.Values;
			}
		}

		public virtual ICollection ObjectiveNames
		{
			get
			{
				return this.field_96544_c.Keys;
			}
		}

		public virtual void func_96515_c(string p_96515_1_)
		{
			IDictionary var2 = (IDictionary)this.field_96544_c.remove(p_96515_1_);

			if(var2 != null)
			{
				this.func_96516_a(p_96515_1_);
			}
		}

		public virtual ICollection func_96528_e()
		{
			ICollection var1 = this.field_96544_c.Values;
			ArrayList var2 = new ArrayList();
			IEnumerator var3 = var1.GetEnumerator();

			while(var3.MoveNext())
			{
				IDictionary var4 = (IDictionary)var3.Current;
				var2.AddRange(var4.Values);
			}

			return var2;
		}

		public virtual IDictionary func_96510_d(string p_96510_1_)
		{
			object var2 = (IDictionary)this.field_96544_c.get(p_96510_1_);

			if(var2 == null)
			{
				var2 = new Hashtable();
			}

			return(IDictionary)var2;
		}

		public virtual void func_96519_k(ScoreObjective p_96519_1_)
		{
			this.scoreObjectives.Remove(p_96519_1_.Name);

			for(int var2 = 0; var2 < 3; ++var2)
			{
				if(this.func_96539_a(var2) == p_96519_1_)
				{
					this.func_96530_a(var2, (ScoreObjective)null);
				}
			}

			IList var5 = (IList)this.scoreObjectiveCriterias.get(p_96519_1_.Criteria);

			if(var5 != null)
			{
				var5.Remove(p_96519_1_);
			}

			IEnumerator var3 = this.field_96544_c.Values.GetEnumerator();

			while(var3.MoveNext())
			{
				IDictionary var4 = (IDictionary)var3.Current;
				var4.Remove(p_96519_1_);
			}

			this.func_96533_c(p_96519_1_);
		}

		public virtual void func_96530_a(int p_96530_1_, ScoreObjective p_96530_2_)
		{
			this.field_96541_d[p_96530_1_] = p_96530_2_;
		}

		public virtual ScoreObjective func_96539_a(int p_96539_1_)
		{
			return this.field_96541_d[p_96539_1_];
		}

///    
///     <summary> * Retrieve the ScorePlayerTeam instance identified by the passed team name </summary>
///     
		public virtual ScorePlayerTeam getTeam(string p_96508_1_)
		{
			return(ScorePlayerTeam)this.teams.get(p_96508_1_);
		}

///    
///     <summary> * Verifies that the given name doesn't already refer to an existing team, creates it otherwise and broadcasts the
///     * addition to all players </summary>
///     
		public virtual ScorePlayerTeam createTeam(string p_96527_1_)
		{
			ScorePlayerTeam var2 = this.getTeam(p_96527_1_);

			if(var2 != null)
			{
				throw new System.ArgumentException("A team with the name \'" + p_96527_1_ + "\' already exists!");
			}
			else
			{
				var2 = new ScorePlayerTeam(this, p_96527_1_);
				this.teams.Add(p_96527_1_, var2);
				this.func_96523_a(var2);
				return var2;
			}
		}

///    
///     <summary> * Removes the team from the scoreboard, updates all player memberships and broadcasts the deletion to all players </summary>
///     
		public virtual void removeTeam(ScorePlayerTeam p_96511_1_)
		{
			this.teams.Remove(p_96511_1_.RegisteredName);
			IEnumerator var2 = p_96511_1_.MembershipCollection.GetEnumerator();

			while(var2.MoveNext())
			{
				string var3 = (string)var2.Current;
				this.teamMemberships.Remove(var3);
			}

			this.func_96513_c(p_96511_1_);
		}

		public virtual bool func_151392_a(string p_151392_1_, string p_151392_2_)
		{
			if(!this.teams.ContainsKey(p_151392_2_))
			{
				return false;
			}
			else
			{
				ScorePlayerTeam var3 = this.getTeam(p_151392_2_);

				if(this.getPlayersTeam(p_151392_1_) != null)
				{
					this.func_96524_g(p_151392_1_);
				}

				this.teamMemberships.Add(p_151392_1_, var3);
				var3.MembershipCollection.add(p_151392_1_);
				return true;
			}
		}

		public virtual bool func_96524_g(string p_96524_1_)
		{
			ScorePlayerTeam var2 = this.getPlayersTeam(p_96524_1_);

			if(var2 != null)
			{
				this.removePlayerFromTeam(p_96524_1_, var2);
				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Removes the given username from the given ScorePlayerTeam. If the player is not on the team then an
///     * IllegalStateException is thrown. </summary>
///     
		public virtual void removePlayerFromTeam(string p_96512_1_, ScorePlayerTeam p_96512_2_)
		{
			if(this.getPlayersTeam(p_96512_1_) != p_96512_2_)
			{
				throw new IllegalStateException("Player is either on another team or not on any team. Cannot remove from team \'" + p_96512_2_.RegisteredName + "\'.");
			}
			else
			{
				this.teamMemberships.Remove(p_96512_1_);
				p_96512_2_.MembershipCollection.remove(p_96512_1_);
			}
		}

///    
///     <summary> * Retrieve all registered ScorePlayerTeam names </summary>
///     
		public virtual ICollection TeamNames
		{
			get
			{
				return this.teams.Keys;
			}
		}

///    
///     <summary> * Retrieve all registered ScorePlayerTeam instances </summary>
///     
		public virtual ICollection Teams
		{
			get
			{
				return this.teams.Values;
			}
		}

///    
///     <summary> * Gets the ScorePlayerTeam object for the given username. </summary>
///     
		public virtual ScorePlayerTeam getPlayersTeam(string p_96509_1_)
		{
			return(ScorePlayerTeam)this.teamMemberships.get(p_96509_1_);
		}

		public virtual void func_96522_a(ScoreObjective p_96522_1_)
		{
		}

		public virtual void func_96532_b(ScoreObjective p_96532_1_)
		{
		}

		public virtual void func_96533_c(ScoreObjective p_96533_1_)
		{
		}

		public virtual void func_96536_a(Score p_96536_1_)
		{
		}

		public virtual void func_96516_a(string p_96516_1_)
		{
		}

		public virtual void func_96523_a(ScorePlayerTeam p_96523_1_)
		{
		}

		public virtual void func_96538_b(ScorePlayerTeam p_96538_1_)
		{
		}

		public virtual void func_96513_c(ScorePlayerTeam p_96513_1_)
		{
		}

///    
///     <summary> * Returns 'list' for 0, 'sidebar' for 1, 'belowName for 2, otherwise null. </summary>
///     
		public static string getObjectiveDisplaySlot(int p_96517_0_)
		{
			switch (p_96517_0_)
			{
				case 0:
					return "list";

				case 1:
					return "sidebar";

				case 2:
					return "belowName";

				default:
					return null;
			}
		}

///    
///     <summary> * Returns 0 for (case-insensitive) 'list', 1 for 'sidebar', 2 for 'belowName', otherwise -1. </summary>
///     
		public static int getObjectiveDisplaySlotNumber(string p_96537_0_)
		{
			return p_96537_0_.equalsIgnoreCase("list") ? 0 : (p_96537_0_.equalsIgnoreCase("sidebar") ? 1 : (p_96537_0_.equalsIgnoreCase("belowName") ? 2 : -1));
		}
	}

}