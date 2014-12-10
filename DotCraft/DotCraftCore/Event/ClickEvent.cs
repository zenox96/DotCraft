namespace DotCraftCore.Event
{

	using Maps = com.google.common.collect.Maps;

	public class ClickEvent
	{
		private readonly ClickEvent.Action action;
		private readonly string value;
		private const string __OBFID = "CL_00001260";

		public ClickEvent(ClickEvent.Action p_i45156_1_, string p_i45156_2_)
		{
			this.action = p_i45156_1_;
			this.value = p_i45156_2_;
		}

///    
///     <summary> * Gets the action to perform when this event is raised. </summary>
///     
		public virtual ClickEvent.Action Action
		{
			get
			{
				return this.action;
			}
		}

///    
///     <summary> * Gets the value to perform the action on when this event is raised.  For example, if the action is "open URL",
///     * this would be the URL to open. </summary>
///     
		public virtual string Value
		{
			get
			{
				return this.value;
			}
		}

		public override bool Equals(object p_equals_1_)
		{
			if (this == p_equals_1_)
			{
				return true;
			}
			else if (p_equals_1_ != null && this.GetType() == p_equals_1_.GetType())
			{
				ClickEvent var2 = (ClickEvent)p_equals_1_;

				if (this.action != var2.action)
				{
					return false;
				}
				else
				{
					if (this.value != null)
					{
						if (!this.value.Equals(var2.value))
						{
							return false;
						}
					}
					else if (var2.value != null)
					{
						return false;
					}

					return true;
				}
			}
			else
			{
				return false;
			}
		}

		public override string ToString()
		{
			return "ClickEvent{action=" + this.action + ", value=\'" + this.value + '\'' + '}';
		}

		public override int GetHashCode()
		{
			int var1 = this.action.GetHashCode();
			var1 = 31 * var1 + (this.value != null ? this.value.GetHashCode() : 0);
			return var1;
		}

		public enum Action
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			OPEN_URL("OPEN_URL", 0, "open_url", true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			OPEN_FILE("OPEN_FILE", 1, "open_file", false),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			RUN_COMMAND("RUN_COMMAND", 2, "run_command", true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			TWITCH_USER_INFO("TWITCH_USER_INFO", 3, "twitch_user_info", false),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SUGGEST_COMMAND("SUGGEST_COMMAND", 4, "suggest_command", true);
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private static final Map nameMapping = Maps.newHashMap();
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final boolean allowedInChat;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final String canonicalName;

			@private static final ClickEvent.Action[] $VALUES = new ClickEvent.Action[]{OPEN_URL, OPEN_FILE, RUN_COMMAND, TWITCH_USER_INFO, SUGGEST_COMMAND
		}
			private const string __OBFID = "CL_00001261";

			private Action(string p_i45155_1_, int p_i45155_2_, string p_i45155_3_, bool p_i45155_4_)
			{
				this.canonicalName = p_i45155_3_;
				this.allowedInChat = p_i45155_4_;
			}

			public virtual bool shouldAllowInChat()
			{
				return this.allowedInChat;
			}

			public virtual string CanonicalName
			{
				get
				{
					return this.canonicalName;
				}
			}

			public static ClickEvent.Action getValueByCanonicalName(string p_150672_0_)
			{
				return (ClickEvent.Action)nameMapping.get(p_150672_0_);
			}

			static ClickEvent()
			{
				ClickEvent.Action[] var0 = values();
				int var1 = var0.Length;

				for (int var2 = 0; var2 < var1; ++var2)
				{
					ClickEvent.Action var3 = var0[var2];
					nameMapping.put(var3.CanonicalName, var3);
				}
			}
		}
	}

}