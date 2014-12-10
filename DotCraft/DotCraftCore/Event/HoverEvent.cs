namespace DotCraftCore.Event
{

	using Maps = com.google.common.collect.Maps;
	using IChatComponent = DotCraftCore.util.IChatComponent;

	public class HoverEvent
	{
		private readonly HoverEvent.Action action;
		private readonly IChatComponent value;
		private const string __OBFID = "CL_00001264";

		public HoverEvent(HoverEvent.Action p_i45158_1_, IChatComponent p_i45158_2_)
		{
			this.action = p_i45158_1_;
			this.value = p_i45158_2_;
		}

///    
///     <summary> * Gets the action to perform when this event is raised. </summary>
///     
		public virtual HoverEvent.Action Action
		{
			get
			{
				return this.action;
			}
		}

///    
///     <summary> * Gets the value to perform the action on when this event is raised.  For example, if the action is "show item",
///     * this would be the item to show. </summary>
///     
		public virtual IChatComponent Value
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
				HoverEvent var2 = (HoverEvent)p_equals_1_;

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
			return "HoverEvent{action=" + this.action + ", value=\'" + this.value + '\'' + '}';
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
			SHOW_TEXT("SHOW_TEXT", 0, "show_text", true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SHOW_ACHIEVEMENT("SHOW_ACHIEVEMENT", 1, "show_achievement", true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SHOW_ITEM("SHOW_ITEM", 2, "show_item", true);
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private static final Map nameMapping = Maps.newHashMap();
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final boolean allowedInChat;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final String canonicalName;

			@private static final HoverEvent.Action[] $VALUES = new HoverEvent.Action[]{SHOW_TEXT, SHOW_ACHIEVEMENT, SHOW_ITEM
		}
			private const string __OBFID = "CL_00001265";

			private Action(string p_i45157_1_, int p_i45157_2_, string p_i45157_3_, bool p_i45157_4_)
			{
				this.canonicalName = p_i45157_3_;
				this.allowedInChat = p_i45157_4_;
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

			public static HoverEvent.Action getValueByCanonicalName(string p_150684_0_)
			{
				return (HoverEvent.Action)nameMapping.get(p_150684_0_);
			}

			static HoverEvent()
			{
				HoverEvent.Action[] var0 = values();
				int var1 = var0.Length;

				for (int var2 = 0; var2 < var1; ++var2)
				{
					HoverEvent.Action var3 = var0[var2];
					nameMapping.put(var3.CanonicalName, var3);
				}
			}
		}
	}

}