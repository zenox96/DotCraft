using DotCraftCore.nUtil;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotCraftCore.nEvent
{
	public class HoverEvent
	{
		private readonly HoverEvent.EnumAction action;
		private readonly IChatComponent value;


        public HoverEvent(HoverEvent.EnumAction p_i45158_1_, IChatComponent p_i45158_2_)
		{
			this.action = p_i45158_1_;
			this.value = p_i45158_2_;
		}

///    
///     <summary> * Gets the action to perform when this event is raised. </summary>
///     
        public virtual HoverEvent.EnumAction Action
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

		public enum EnumAction
		{
            SHOW_TEXT = 0,
            SHOW_ACHIEVEMENT = 1,
            SHOW_ITEM = 2
        }
        
        public static class EnumActionExtension
        {
            private static readonly bool[] allowedInChatMap;
            private static readonly string[] canonicalNameMap;
            private static readonly Dictionary<string, EnumAction> nameMapping;

            static EnumActionExtension() {
                allowedInChatMap = new bool[]{true, true, true};
                canonicalNameMap = new string[]{"show_text","show_achievement","show_item"};
                nameMapping = new Dictionary<string, EnumAction>();
                for (int i = 0; i < canonicalNameMap.Length; i++){
                    nameMapping.Add(canonicalNameMap[i], (EnumAction)i);
                }
            }

            public static bool shouldAllowInChat(this EnumAction e)
			{
				return EnumActionExtension.allowedInChatMap[(int)e];
			}

            public static string CanonicalName(this EnumAction e)
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