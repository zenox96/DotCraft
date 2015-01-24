

using System.Collections.Generic;
namespace DotCraftCore.nEvent
{
	public class ClickEvent
	{
		private readonly ClickEvent.EnumAction action;
		private readonly string value;
		

		public ClickEvent(ClickEvent.EnumAction p_i45156_1_, string p_i45156_2_)
		{
			this.action = p_i45156_1_;
			this.value = p_i45156_2_;
		}

///    
///     <summary> * Gets the action to perform when this event is raised. </summary>
///     
		public virtual ClickEvent.EnumAction Action
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

		public enum EnumAction
		{
            OPEN_URL = 0,
            OPEN_FILE = 1,
            RUN_COMMAND = 2,
            TWITCH_USER_INFO = 3,
            SUGGEST_COMMAND = 4
        }

        public static class EnumActionExtension
        {
            private static readonly bool[] allowedInChatMap;
            private static readonly string[] canonicalNameMap;
            private static readonly Dictionary<string, EnumAction> nameMapping;

            static EnumActionExtension(){
                allowedInChatMap = new bool[]{true, false, true, false, true};
                canonicalNameMap = new string[]{"open_url","open_file","run_command","twitch_user_info","suggest_command"};
                nameMapping = new Dictionary<string, EnumAction>();
                for (int i = 0; i < canonicalNameMap.Length; i++){
                    nameMapping.Add(canonicalNameMap[i], (EnumAction)i);
                }
            }

            public bool shouldAllowInChat(this EnumAction e)
			{
				return EnumActionExtension.allowedInChatMap[(int)e];
			}

            public string CanonicalName(this EnumAction e)
            {
                return EnumActionExtension.canonicalNameMap[(int)e];
			}

            public static HoverEvent.EnumAction getValueByCanonicalName(this EnumAction e, string p_150684_0_)
			{
				return (HoverEvent.EnumAction)nameMapping[p_150684_0_];
			}
        }
	}
}