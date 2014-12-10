using System;

namespace DotCraftCore.Stats
{

	using HoverEvent = DotCraftCore.event.HoverEvent;
	using IScoreObjectiveCriteria = DotCraftCore.Scoreboard.IScoreObjectiveCriteria;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using EnumChatFormatting = DotCraftCore.Util.EnumChatFormatting;
	using IChatComponent = DotCraftCore.Util.IChatComponent;

	public class StatBase
	{
	/// <summary> The Stat ID  </summary>
		public readonly string statId;

	/// <summary> The Stat name  </summary>
		private readonly IChatComponent statName;
		public bool isIndependent;
		private readonly IStatType type;
		private readonly IScoreObjectiveCriteria field_150957_c;
		private Type field_150956_d;
		private static NumberFormat numberFormat = NumberFormat.getIntegerInstance(Locale.US);
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		public static IStatType simpleStatType = new IStatType()
//	{
//		private static final String __OBFID = "CL_00001473";
//		public String format(int p_75843_1_)
//		{
//			return StatBase.numberFormat.format((long)p_75843_1_);
//		}
//	};
		private static DecimalFormat decimalFormat = new DecimalFormat("########0.00");
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		public static IStatType timeStatType = new IStatType()
//	{
//		private static final String __OBFID = "CL_00001474";
//		public String format(int p_75843_1_)
//		{
//			double var2 = (double)p_75843_1_ / 20.0D;
//			double var4 = var2 / 60.0D;
//			double var6 = var4 / 60.0D;
//			double var8 = var6 / 24.0D;
//			double var10 = var8 / 365.0D;
//			return var10 > 0.5D ? StatBase.decimalFormat.format(var10) + " y" : (var8 > 0.5D ? StatBase.decimalFormat.format(var8) + " d" : (var6 > 0.5D ? StatBase.decimalFormat.format(var6) + " h" : (var4 > 0.5D ? StatBase.decimalFormat.format(var4) + " m" : var2 + " s")));
//		}
//	};
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		public static IStatType distanceStatType = new IStatType()
//	{
//		private static final String __OBFID = "CL_00001475";
//		public String format(int p_75843_1_)
//		{
//			double var2 = (double)p_75843_1_ / 100.0D;
//			double var4 = var2 / 1000.0D;
//			return var4 > 0.5D ? StatBase.decimalFormat.format(var4) + " km" : (var2 > 0.5D ? StatBase.decimalFormat.format(var2) + " m" : p_75843_1_ + " cm");
//		}
//	};
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		public static IStatType field_111202_k = new IStatType()
//	{
//		private static final String __OBFID = "CL_00001476";
//		public String format(int p_75843_1_)
//		{
//			return StatBase.decimalFormat.format((double)p_75843_1_ * 0.1D);
//		}
//	};
		private const string __OBFID = "CL_00001472";

		public StatBase(string p_i45307_1_, IChatComponent p_i45307_2_, IStatType p_i45307_3_)
		{
			this.statId = p_i45307_1_;
			this.statName = p_i45307_2_;
			this.type = p_i45307_3_;
			this.field_150957_c = new ObjectiveStat(this);
			IScoreObjectiveCriteria.field_96643_a.put(this.field_150957_c.func_96636_a(), this.field_150957_c);
		}

		public StatBase(string p_i45308_1_, IChatComponent p_i45308_2_) : this(p_i45308_1_, p_i45308_2_, simpleStatType)
		{
		}

///    
///     <summary> * Initializes the current stat as independent (i.e., lacking prerequisites for being updated) and returns the
///     * current instance. </summary>
///     
		public virtual StatBase initIndependentStat()
		{
			this.isIndependent = true;
			return this;
		}

///    
///     <summary> * Register the stat into StatList. </summary>
///     
		public virtual StatBase registerStat()
		{
			if(StatList.oneShotStats.ContainsKey(this.statId))
			{
				throw new Exception("Duplicate stat id: \"" + ((StatBase)StatList.oneShotStats[this.statId]).statName + "\" and \"" + this.statName + "\" at id " + this.statId);
			}
			else
			{
				StatList.allStats.Add(this);
				StatList.oneShotStats.Add(this.statId, this);
				return this;
			}
		}

///    
///     <summary> * Returns whether or not the StatBase-derived class is a statistic (running counter) or an achievement (one-shot). </summary>
///     
		public virtual bool isAchievement()
		{
			get
			{
				return false;
			}
		}

		public virtual string func_75968_a(int p_75968_1_)
		{
			return this.type.format(p_75968_1_);
		}

		public virtual IChatComponent func_150951_e()
		{
			IChatComponent var1 = this.statName.createCopy();
			var1.ChatStyle.Color = EnumChatFormatting.GRAY;
			var1.ChatStyle.ChatHoverEvent = new HoverEvent(HoverEvent.Action.SHOW_ACHIEVEMENT, new ChatComponentText(this.statId));
			return var1;
		}

		public virtual IChatComponent func_150955_j()
		{
			IChatComponent var1 = this.func_150951_e();
			IChatComponent var2 = (new ChatComponentText("[")).appendSibling(var1).appendText("]");
			var2.ChatStyle = var1.ChatStyle;
			return var2;
		}

		public override bool Equals(object p_equals_1_)
		{
			if(this == p_equals_1_)
			{
				return true;
			}
			else if(p_equals_1_ != null && this.GetType() == p_equals_1_.GetType())
			{
				StatBase var2 = (StatBase)p_equals_1_;
				return this.statId.Equals(var2.statId);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.statId.GetHashCode();
		}

		public override string ToString()
		{
			return "Stat{id=" + this.statId + ", nameId=" + this.statName + ", awardLocallyOnly=" + this.isIndependent + ", formatter=" + this.type + ", objectiveCriteria=" + this.field_150957_c + '}';
		}

		public virtual IScoreObjectiveCriteria func_150952_k()
		{
			return this.field_150957_c;
		}

		public virtual Type func_150954_l()
		{
			return this.field_150956_d;
		}

		public virtual StatBase func_150953_b(Type p_150953_1_)
		{
			this.field_150956_d = p_150953_1_;
			return this;
		}
	}

}