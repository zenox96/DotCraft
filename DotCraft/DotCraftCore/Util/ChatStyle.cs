using System;
using System.Text;

namespace DotCraftCore.nUtil
{

	using JsonDeserializationContext = com.google.gson.JsonDeserializationContext;
	using JsonDeserializer = com.google.gson.JsonDeserializer;
	using JsonElement = com.google.gson.JsonElement;
	using JsonObject = com.google.gson.JsonObject;
	using JsonPrimitive = com.google.gson.JsonPrimitive;
	using JsonSerializationContext = com.google.gson.JsonSerializationContext;
	using JsonSerializer = com.google.gson.JsonSerializer;
	using ClickEvent = DotCraftCore.event.ClickEvent;
	using HoverEvent = DotCraftCore.event.HoverEvent;

	public class ChatStyle
	{
///    
///     <summary> * The parent of this ChatStyle.  Used for looking up values that this instance does not override. </summary>
///     
		private ChatStyle parentStyle;
		private EnumChatFormatting color;
		private bool? bold;
		private bool? italic;
		private bool? underlined;
		private bool? strikethrough;
		private bool? obfuscated;
		private ClickEvent chatClickEvent;
		private HoverEvent chatHoverEvent;

///    
///     <summary> * The base of the ChatStyle hierarchy.  All ChatStyle instances are implicitly children of this. </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private static final ChatStyle rootStyle = new ChatStyle()
//	{
//		
//		public EnumChatFormatting getColor()
//		{
//			return null;
//		}
//		public boolean getBold()
//		{
//			return false;
//		}
//		public boolean getItalic()
//		{
//			return false;
//		}
//		public boolean getStrikethrough()
//		{
//			return false;
//		}
//		public boolean getUnderlined()
//		{
//			return false;
//		}
//		public boolean getObfuscated()
//		{
//			return false;
//		}
//		public ClickEvent getChatClickEvent()
//		{
//			return null;
//		}
//		public HoverEvent getChatHoverEvent()
//		{
//			return null;
//		}
//		public ChatStyle setColor(EnumChatFormatting p_150238_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public ChatStyle setBold(Boolean p_150227_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public ChatStyle setItalic(Boolean p_150217_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public ChatStyle setStrikethrough(Boolean p_150225_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public ChatStyle setUnderlined(Boolean p_150228_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public ChatStyle setObfuscated(Boolean p_150237_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public ChatStyle setChatClickEvent(ClickEvent p_150241_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public ChatStyle setChatHoverEvent(HoverEvent p_150209_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public ChatStyle setParentStyle(ChatStyle p_150221_1_)
//		{
//			throw new UnsupportedOperationException();
//		}
//		public String toString()
//		{
//			return "Style.ROOT";
//		}
//		public ChatStyle createShallowCopy()
//		{
//			return this;
//		}
//		public ChatStyle createDeepCopy()
//		{
//			return this;
//		}
//		public String getFormattingCode()
//		{
//			return "";
//		}
//	};
		

///    
///     <summary> * Gets the effective color of this ChatStyle. </summary>
///     
		public virtual EnumChatFormatting Color
		{
			get
			{
				return this.color == null ? this.Parent.Color : this.color;
			}
			set
			{
				this.color = value;
				return this;
			}
		}

///    
///     <summary> * Whether or not text of this ChatStyle should be in bold. </summary>
///     
		public virtual bool Bold
		{
			get
			{
				return this.bold == null ? this.Parent.Bold : (bool)this.bold;
			}
			set
			{
				this.bold = value;
				return this;
			}
		}

///    
///     <summary> * Whether or not text of this ChatStyle should be italicized. </summary>
///     
		public virtual bool Italic
		{
			get
			{
				return this.italic == null ? this.Parent.Italic : (bool)this.italic;
			}
			set
			{
				this.italic = value;
				return this;
			}
		}

///    
///     <summary> * Whether or not to format text of this ChatStyle using strikethrough. </summary>
///     
		public virtual bool Strikethrough
		{
			get
			{
				return this.strikethrough == null ? this.Parent.Strikethrough : (bool)this.strikethrough;
			}
			set
			{
				this.strikethrough = value;
				return this;
			}
		}

///    
///     <summary> * Whether or not text of this ChatStyle should be underlined. </summary>
///     
		public virtual bool Underlined
		{
			get
			{
				return this.underlined == null ? this.Parent.Underlined : (bool)this.underlined;
			}
			set
			{
				this.underlined = value;
				return this;
			}
		}

///    
///     <summary> * Whether or not text of this ChatStyle should be obfuscated. </summary>
///     
		public virtual bool Obfuscated
		{
			get
			{
				return this.obfuscated == null ? this.Parent.Obfuscated : (bool)this.obfuscated;
			}
			set
			{
				this.obfuscated = value;
				return this;
			}
		}

///    
///     <summary> * Whether or not this style is empty (inherits everything from the parent). </summary>
///     
		public virtual bool isEmpty()
		{
			get
			{
				return this.bold == null && this.italic == null && this.strikethrough == null && this.underlined == null && this.obfuscated == null && this.color == null && this.chatClickEvent == null && this.chatHoverEvent == null;
			}
		}

///    
///     <summary> * The effective chat click event. </summary>
///     
		public virtual ClickEvent ChatClickEvent
		{
			get
			{
				return this.chatClickEvent == null ? this.Parent.ChatClickEvent : this.chatClickEvent;
			}
			set
			{
				this.chatClickEvent = value;
				return this;
			}
		}

///    
///     <summary> * The effective chat hover event. </summary>
///     
		public virtual HoverEvent ChatHoverEvent
		{
			get
			{
				return this.chatHoverEvent == null ? this.Parent.ChatHoverEvent : this.chatHoverEvent;
			}
			set
			{
				this.chatHoverEvent = value;
				return this;
			}
		}

///    
///     <summary> * Sets the color for this ChatStyle to the given value.  Only use color values for this; set other values using the
///     * specific methods. </summary>
///     

///    
///     <summary> * Sets whether or not text of this ChatStyle should be in bold.  Set to false if, e.g., the parent style is bold
///     * and you want text of this style to be unbolded. </summary>
///     

///    
///     <summary> * Sets whether or not text of this ChatStyle should be italicized.  Set to false if, e.g., the parent style is
///     * italicized and you want to override that for this style. </summary>
///     

///    
///     <summary> * Sets whether or not to format text of this ChatStyle using strikethrough.  Set to false if, e.g., the parent
///     * style uses strikethrough and you want to override that for this style. </summary>
///     

///    
///     <summary> * Sets whether or not text of this ChatStyle should be underlined.  Set to false if, e.g., the parent style is
///     * underlined and you want to override that for this style. </summary>
///     

///    
///     <summary> * Sets whether or not text of this ChatStyle should be obfuscated.  Set to false if, e.g., the parent style is
///     * obfuscated and you want to override that for this style. </summary>
///     

///    
///     <summary> * Sets the event that should be run when text of this ChatStyle is clicked on. </summary>
///     

///    
///     <summary> * Sets the event that should be run when text of this ChatStyle is hovered over. </summary>
///     

///    
///     <summary> * Sets the fallback ChatStyle to use if this ChatStyle does not override some value.  Without a parent, obvious
///     * defaults are used (bold: false, underlined: false, etc). </summary>
///     
		public virtual ChatStyle ParentStyle
		{
			set
			{
				this.parentStyle = value;
				return this;
			}
		}

///    
///     <summary> * Gets the equivalent text formatting code for this style, without the initial section sign (U+00A7) character. </summary>
///     
		public virtual string FormattingCode
		{
			get
			{
				if(this.Empty)
				{
					return this.parentStyle != null ? this.parentStyle.FormattingCode : "";
				}
				else
				{
					StringBuilder var1 = new StringBuilder();
	
					if(this.Color != null)
					{
						var1.Append(this.Color);
					}
	
					if(this.Bold)
					{
						var1.Append(EnumChatFormatting.BOLD);
					}
	
					if(this.Italic)
					{
						var1.Append(EnumChatFormatting.ITALIC);
					}
	
					if(this.Underlined)
					{
						var1.Append(EnumChatFormatting.UNDERLINE);
					}
	
					if(this.Obfuscated)
					{
						var1.Append(EnumChatFormatting.OBFUSCATED);
					}
	
					if(this.Strikethrough)
					{
						var1.Append(EnumChatFormatting.STRIKETHROUGH);
					}
	
					return var1.ToString();
				}
			}
		}

///    
///     <summary> * Gets the immediate parent of this ChatStyle. </summary>
///     
		private ChatStyle Parent
		{
			get
			{
				return this.parentStyle == null ? rootStyle : this.parentStyle;
			}
		}

		public override string ToString()
		{
			return "Style{hasParent=" + (this.parentStyle != null) + ", color=" + this.color + ", bold=" + this.bold + ", italic=" + this.italic + ", underlined=" + this.underlined + ", obfuscated=" + this.obfuscated + ", clickEvent=" + this.ChatClickEvent + ", hoverEvent=" + this.ChatHoverEvent + '}';
		}

		public override bool Equals(object p_equals_1_)
		{
			if(this == p_equals_1_)
			{
				return true;
			}
			else if(!(p_equals_1_ is ChatStyle))
			{
				return false;
			}
			else
			{
				ChatStyle var2 = (ChatStyle)p_equals_1_;
				bool var10000;

				if(this.Bold == var2.Bold && this.Color == var2.Color && this.Italic == var2.Italic && this.Obfuscated == var2.Obfuscated && this.Strikethrough == var2.Strikethrough && this.Underlined == var2.Underlined)
				{
					label56:
					{
						if(this.ChatClickEvent != null)
						{
							if(!this.ChatClickEvent.Equals(var2.ChatClickEvent))
							{
								goto label56;
							}
						}
						else if(var2.ChatClickEvent != null)
						{
							goto label56;
						}

						if(this.ChatHoverEvent != null)
						{
							if(!this.ChatHoverEvent.Equals(var2.ChatHoverEvent))
							{
								goto label56;
							}
						}
						else if(var2.ChatHoverEvent != null)
						{
							goto label56;
						}

						var10000 = true;
						return var10000;
					}
				}

				var10000 = false;
				return var10000;
			}
		}

		public override int GetHashCode()
		{
			int var1 = this.color.GetHashCode();
			var1 = 31 * var1 + this.bold.GetHashCode();
			var1 = 31 * var1 + this.italic.GetHashCode();
			var1 = 31 * var1 + this.underlined.GetHashCode();
			var1 = 31 * var1 + this.strikethrough.GetHashCode();
			var1 = 31 * var1 + this.obfuscated.GetHashCode();
			var1 = 31 * var1 + this.chatClickEvent.GetHashCode();
			var1 = 31 * var1 + this.chatHoverEvent.GetHashCode();
			return var1;
		}

///    
///     <summary> * Creates a shallow copy of this style.  Changes to this instance's values will not be reflected in the copy, but
///     * changes to the parent style's values WILL be reflected in both this instance and the copy, wherever either does
///     * not override a value. </summary>
///     
		public virtual ChatStyle createShallowCopy()
		{
			ChatStyle var1 = new ChatStyle();
			var1.bold = this.bold;
			var1.italic = this.italic;
			var1.strikethrough = this.strikethrough;
			var1.underlined = this.underlined;
			var1.obfuscated = this.obfuscated;
			var1.color = this.color;
			var1.chatClickEvent = this.chatClickEvent;
			var1.chatHoverEvent = this.chatHoverEvent;
			var1.parentStyle = this.parentStyle;
			return var1;
		}

///    
///     <summary> * Creates a deep copy of this style.  No changes to this instance or its parent style will be reflected in the
///     * copy. </summary>
///     
		public virtual ChatStyle createDeepCopy()
		{
			ChatStyle var1 = new ChatStyle();
			var1.Bold = Convert.ToBoolean(this.Bold);
			var1.Italic = Convert.ToBoolean(this.Italic);
			var1.Strikethrough = Convert.ToBoolean(this.Strikethrough);
			var1.Underlined = Convert.ToBoolean(this.Underlined);
			var1.Obfuscated = Convert.ToBoolean(this.Obfuscated);
			var1.Color = this.Color;
			var1.ChatClickEvent = this.ChatClickEvent;
			var1.ChatHoverEvent = this.ChatHoverEvent;
			return var1;
		}

		public class Serializer : JsonDeserializer, JsonSerializer
		{
			

			public virtual ChatStyle deserialize(JsonElement p_deserialize_1_, Type p_deserialize_2_, JsonDeserializationContext p_deserialize_3_)
			{
				if(p_deserialize_1_.JsonObject)
				{
					ChatStyle var4 = new ChatStyle();
					JsonObject var5 = p_deserialize_1_.AsJsonObject;

					if(var5 == null)
					{
						return null;
					}
					else
					{
						if(var5.has("bold"))
						{
							var4.bold = Convert.ToBoolean(var5.get("bold").AsBoolean);
						}

						if(var5.has("italic"))
						{
							var4.italic = Convert.ToBoolean(var5.get("italic").AsBoolean);
						}

						if(var5.has("underlined"))
						{
							var4.underlined = Convert.ToBoolean(var5.get("underlined").AsBoolean);
						}

						if(var5.has("strikethrough"))
						{
							var4.strikethrough = Convert.ToBoolean(var5.get("strikethrough").AsBoolean);
						}

						if(var5.has("obfuscated"))
						{
							var4.obfuscated = Convert.ToBoolean(var5.get("obfuscated").AsBoolean);
						}

						if(var5.has("color"))
						{
							var4.color = (EnumChatFormatting)p_deserialize_3_.deserialize(var5.get("color"), typeof(EnumChatFormatting));
						}

						JsonObject var6;
						JsonPrimitive var7;

						if(var5.has("clickEvent"))
						{
							var6 = var5.getAsJsonObject("clickEvent");

							if(var6 != null)
							{
								var7 = var6.getAsJsonPrimitive("action");
								ClickEvent.Action var8 = var7 == null ? null : ClickEvent.Action.getValueByCanonicalName(var7.AsString);
								JsonPrimitive var9 = var6.getAsJsonPrimitive("value");
								string var10 = var9 == null ? null : var9.AsString;

								if(var8 != null && var10 != null && var8.shouldAllowInChat())
								{
									var4.chatClickEvent = new ClickEvent(var8, var10);
								}
							}
						}

						if(var5.has("hoverEvent"))
						{
							var6 = var5.getAsJsonObject("hoverEvent");

							if(var6 != null)
							{
								var7 = var6.getAsJsonPrimitive("action");
								HoverEvent.Action var11 = var7 == null ? null : HoverEvent.Action.getValueByCanonicalName(var7.AsString);
								IChatComponent var12 = (IChatComponent)p_deserialize_3_.deserialize(var6.get("value"), typeof(IChatComponent));

								if(var11 != null && var12 != null && var11.shouldAllowInChat())
								{
									var4.chatHoverEvent = new HoverEvent(var11, var12);
								}
							}
						}

						return var4;
					}
				}
				else
				{
					return null;
				}
			}

			public virtual JsonElement serialize(ChatStyle p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
			{
				if(p_serialize_1_.Empty)
				{
					return null;
				}
				else
				{
					JsonObject var4 = new JsonObject();

					if(p_serialize_1_.bold != null)
					{
						var4.addProperty("bold", p_serialize_1_.bold);
					}

					if(p_serialize_1_.italic != null)
					{
						var4.addProperty("italic", p_serialize_1_.italic);
					}

					if(p_serialize_1_.underlined != null)
					{
						var4.addProperty("underlined", p_serialize_1_.underlined);
					}

					if(p_serialize_1_.strikethrough != null)
					{
						var4.addProperty("strikethrough", p_serialize_1_.strikethrough);
					}

					if(p_serialize_1_.obfuscated != null)
					{
						var4.addProperty("obfuscated", p_serialize_1_.obfuscated);
					}

					if(p_serialize_1_.color != null)
					{
						var4.add("color", p_serialize_3_.serialize(p_serialize_1_.color));
					}

					JsonObject var5;

					if(p_serialize_1_.chatClickEvent != null)
					{
						var5 = new JsonObject();
						var5.addProperty("action", p_serialize_1_.chatClickEvent.Action.CanonicalName);
						var5.addProperty("value", p_serialize_1_.chatClickEvent.Value);
						var4.add("clickEvent", var5);
					}

					if(p_serialize_1_.chatHoverEvent != null)
					{
						var5 = new JsonObject();
						var5.addProperty("action", p_serialize_1_.chatHoverEvent.Action.CanonicalName);
						var5.add("value", p_serialize_3_.serialize(p_serialize_1_.chatHoverEvent.Value));
						var4.add("hoverEvent", var5);
					}

					return var4;
				}
			}

			public virtual JsonElement serialize(object p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
			{
				return this.serialize((ChatStyle)p_serialize_1_, p_serialize_2_, p_serialize_3_);
			}
		}
	}

}