using System.Collections;

namespace DotCraftCore.nUtil
{


	public class ChatComponentText : ChatComponentStyle
	{
		private readonly string text;
		

		public ChatComponentText(string p_i45159_1_)
		{
			this.text = p_i45159_1_;
		}

///    
///     <summary> * Gets the text value of this ChatComponentText.  TODO: what are getUnformattedText and getUnformattedTextForChat
///     * missing that made someone decide to create a third equivalent method that only ChatComponentText can implement? </summary>
///     
		public virtual string ChatComponentText_TextValue
		{
			get
			{
				return this.text;
			}
		}

///    
///     <summary> * Gets the text of this component, without any special formatting codes added, for chat.  TODO: why is this two
///     * different methods? </summary>
///     
		public virtual string UnformattedTextForChat
		{
			get
			{
				return this.text;
			}
		}

///    
///     <summary> * Creates a copy of this component.  Almost a deep copy, except the style is shallow-copied. </summary>
///     
		public virtual ChatComponentText createCopy()
		{
			ChatComponentText var1 = new ChatComponentText(this.text);
			var1.ChatStyle = this.ChatStyle.createShallowCopy();
			IEnumerator var2 = this.Siblings.GetEnumerator();

			while(var2.MoveNext())
			{
				IChatComponent var3 = (IChatComponent)var2.Current;
				var1.appendSibling(var3.createCopy());
			}

			return var1;
		}

		public override bool Equals(object p_equals_1_)
		{
			if(this == p_equals_1_)
			{
				return true;
			}
			else if(!(p_equals_1_ is ChatComponentText))
			{
				return false;
			}
			else
			{
				ChatComponentText var2 = (ChatComponentText)p_equals_1_;
				return this.text.Equals(var2.ChatComponentText_TextValue) && base.Equals(p_equals_1_);
			}
		}

		public override string ToString()
		{
			return "TextComponent{text=\'" + this.text + '\'' + ", siblings=" + this.siblings + ", style=" + this.ChatStyle + '}';
		}
	}

}