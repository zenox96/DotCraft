using System.Collections;
using System.Text;

namespace DotCraftCore.Util
{

	using Function = com.google.common.base.Function;
	using Iterators = com.google.common.collect.Iterators;
	using Lists = com.google.common.collect.Lists;

	public abstract class ChatComponentStyle : IChatComponent
	{
///    
///     <summary> * The later siblings of this component.  If this component turns the text bold, that will apply to all the siblings
///     * until a later sibling turns the text something else. </summary>
///     
		protected internal IList siblings = Lists.newArrayList();
		private ChatStyle style;
		

///    
///     <summary> * Appends the given component to the end of this one. </summary>
///     
		public virtual IChatComponent appendSibling(IChatComponent p_150257_1_)
		{
			p_150257_1_.ChatStyle.ParentStyle = this.ChatStyle;
			this.siblings.Add(p_150257_1_);
			return this;
		}

///    
///     <summary> * Gets the sibling components of this one. </summary>
///     
		public virtual IList Siblings
		{
			get
			{
				return this.siblings;
			}
		}

///    
///     <summary> * Appends the given text to the end of this component. </summary>
///     
		public virtual IChatComponent appendText(string p_150258_1_)
		{
			return this.appendSibling(new ChatComponentText(p_150258_1_));
		}

		public virtual IChatComponent ChatStyle
		{
			set
			{
				this.style = value;
				IEnumerator var2 = this.siblings.GetEnumerator();
	
				while(var2.MoveNext())
				{
					IChatComponent var3 = (IChatComponent)var2.Current;
					var3.ChatStyle.ParentStyle = this.ChatStyle;
				}
	
				return this;
			}
			get
			{
				if(this.style == null)
				{
					this.style = new ChatStyle();
					IEnumerator var1 = this.siblings.GetEnumerator();
	
					while(var1.MoveNext())
					{
						IChatComponent var2 = (IChatComponent)var1.Current;
						var2.ChatStyle.ParentStyle = this.style;
					}
				}
	
				return this.style;
			}
		}


		public virtual IEnumerator iterator()
		{
			return Iterators.concat(Iterators.forArray(new ChatComponentStyle[] {this}), createDeepCopyIterator(this.siblings));
		}

///    
///     <summary> * Gets the text of this component, without any special formatting codes added.  TODO: why is this two different
///     * methods? </summary>
///     
		public string UnformattedText
		{
			get
			{
				StringBuilder var1 = new StringBuilder();
				IEnumerator var2 = this.GetEnumerator();
	
				while(var2.MoveNext())
				{
					IChatComponent var3 = (IChatComponent)var2.Current;
					var1.Append(var3.UnformattedTextForChat);
				}
	
				return var1.ToString();
			}
		}

///    
///     <summary> * Gets the text of this component, with formatting codes added for rendering. </summary>
///     
		public string FormattedText
		{
			get
			{
				StringBuilder var1 = new StringBuilder();
				IEnumerator var2 = this.GetEnumerator();
	
				while(var2.MoveNext())
				{
					IChatComponent var3 = (IChatComponent)var2.Current;
					var1.Append(var3.ChatStyle.FormattingCode);
					var1.Append(var3.UnformattedTextForChat);
					var1.Append(EnumChatFormatting.RESET);
				}
	
				return var1.ToString();
			}
		}

///    
///     <summary> * Creates an iterator that iterates over the given components, returning deep copies of each component in turn so
///     * that the properties of the returned objects will remain externally consistent after being returned. </summary>
///     
		public static IEnumerator createDeepCopyIterator(IEnumerable p_150262_0_)
		{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: Iterator var1 = Iterators.concat(Iterators.transform(p_150262_0_.iterator(), new Function() {  public Iterator apply(IChatComponent p_apply_1_) { return p_apply_1_.iterator(); } public Object apply(Object p_apply_1_) { return this.apply((IChatComponent)p_apply_1_); } }));
			IEnumerator var1 = Iterators.concat(Iterators.transform(p_150262_0_.GetEnumerator(), new Function() {  public IEnumerator apply(IChatComponent p_apply_1_) { return p_apply_1_.GetEnumerator(); } public object apply(object p_apply_1_) { return this.apply((IChatComponent)p_apply_1_); } }));
			var1 = Iterators.transform(var1, new Function() {  public IChatComponent apply(IChatComponent p_apply_1_) { IChatComponent var2 = p_apply_1_.createCopy(); var2.setChatStyle(var2.ChatStyle.createDeepCopy()); return var2; } public object apply(object p_apply_1_) { return this.apply((IChatComponent)p_apply_1_); } });
			return var1;
		}

		public override bool Equals(object p_equals_1_)
		{
			if(this == p_equals_1_)
			{
				return true;
			}
			else if(!(p_equals_1_ is ChatComponentStyle))
			{
				return false;
			}
			else
			{
				ChatComponentStyle var2 = (ChatComponentStyle)p_equals_1_;
				return this.siblings.Equals(var2.siblings) && this.ChatStyle.Equals(var2.ChatStyle);
			}
		}

		public override int GetHashCode()
		{
			return 31 * this.style.GetHashCode() + this.siblings.GetHashCode();
		}

		public override string ToString()
		{
			return "BaseComponent{style=" + this.style + ", siblings=" + this.siblings + '}';
		}
	}

}