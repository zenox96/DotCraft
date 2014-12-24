using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Text;

namespace DotCraftCore.nUtil
{
	public class ChatComponentTranslation : ChatComponentStyle
	{
		private readonly string key;
		private readonly object[] formatArgs;
		private readonly object syncLock = new object();
		private long lastTranslationUpdateTimeInMilliseconds = -1L;

///    
///     <summary> * The discrete elements that make up this component.  For example, this would be ["Prefix, ", "FirstArg",
///     * "SecondArg", " again ", "SecondArg", " and ", "FirstArg", " lastly ", "ThirdArg", " and also ", "FirstArg", "
///     * again!"] for "translation.test.complex" (see en-US.lang) </summary>
///     
		internal IList children = Lists.newArrayList();
		public static readonly Pattern stringVariablePattern = Pattern.compile("%(?:(\\d+)\\$)?([A-Za-z%]|$)");
		

		public ChatComponentTranslation(string p_i45160_1_, params object[] p_i45160_2_)
		{
			this.key = p_i45160_1_;
			this.formatArgs = p_i45160_2_;
			object[] var3 = p_i45160_2_;
			int var4 = p_i45160_2_.length;

			for(int var5 = 0; var5 < var4; ++var5)
			{
				object var6 = var3[var5];

				if(var6 is IChatComponent)
				{
					((IChatComponent)var6).ChatStyle.ParentStyle = this.ChatStyle;
				}
			}
		}

///    
///     <summary> * ensures that our children are initialized from the most recent string translation mapping. </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal virtual void ensureInitialized()
		{
			object var1 = this.syncLock;

			lock (this.syncLock)
			{
				long var2 = StatCollector.LastTranslationUpdateTimeInMilliseconds;

				if(var2 == this.lastTranslationUpdateTimeInMilliseconds)
				{
					return;
				}

				this.lastTranslationUpdateTimeInMilliseconds = var2;
				this.children.Clear();
			}

			try
			{
				this.initializeFromFormat(StatCollector.translateToLocal(this.key));
			}
			catch (ChatComponentTranslationFormatException var6)
			{
				this.children.Clear();

				try
				{
					this.initializeFromFormat(StatCollector.translateToFallback(this.key));
				}
				catch (ChatComponentTranslationFormatException var5)
				{
					throw var6;
				}
			}
		}

///    
///     <summary> * initializes our children from a format string, using the format args to fill in the placeholder variables. </summary>
///     
		protected internal virtual void initializeFromFormat(string p_150269_1_)
		{
			bool var2 = false;
			Matcher var3 = stringVariablePattern.matcher(p_150269_1_);
			int var4 = 0;
			int var5 = 0;

			try
			{
				int var7;

				for(; var3.find(var5); var5 = var7)
				{
					int var6 = var3.start();
					var7 = var3.end();

					if(var6 > var5)
					{
						ChatComponentText var8 = new ChatComponentText(string.format(p_150269_1_.Substring(var5, var6 - var5), new object[0]));
						var8.ChatStyle.ParentStyle = this.ChatStyle;
						this.children.Add(var8);
					}

					string var14 = var3.group(2);
					string var9 = p_150269_1_.Substring(var6, var7 - var6);

					if("%".Equals(var14) && "%%".Equals(var9))
					{
						ChatComponentText var15 = new ChatComponentText("%");
						var15.ChatStyle.ParentStyle = this.ChatStyle;
						this.children.Add(var15);
					}
					else
					{
						if(!"s".Equals(var14))
						{
							throw new ChatComponentTranslationFormatException(this, "Unsupported format: \'" + var9 + "\'");
						}

						string var10 = var3.group(1);
						int var11 = var10 != null ? Convert.ToInt32(var10) - 1 : var4++;
						this.children.Add(this.getFormatArgumentAsComponent(var11));
					}
				}

				if(var5 < p_150269_1_.Length)
				{
					ChatComponentText var13 = new ChatComponentText(string.format(p_150269_1_.Substring(var5), new object[0]));
					var13.ChatStyle.ParentStyle = this.ChatStyle;
					this.children.Add(var13);
				}
			}
			catch (IllegalFormatException var12)
			{
				throw new ChatComponentTranslationFormatException(this, var12);
			}
		}

		private IChatComponent getFormatArgumentAsComponent(int p_150272_1_)
		{
			if(p_150272_1_ >= this.formatArgs.Length)
			{
				throw new ChatComponentTranslationFormatException(this, p_150272_1_);
			}
			else
			{
				object var2 = this.formatArgs[p_150272_1_];
				object var3;

				if(var2 is IChatComponent)
				{
					var3 = (IChatComponent)var2;
				}
				else
				{
					var3 = new ChatComponentText(var2 == null ? "null" : var2.ToString());
					((IChatComponent)var3).ChatStyle.ParentStyle = this.ChatStyle;
				}

				return(IChatComponent)var3;
			}
		}

		public override IChatComponent ChatStyle
		{
			set
			{
				base.ChatStyle = value;
				object[] var2 = this.formatArgs;
				int var3 = var2.Length;
	
				for(int var4 = 0; var4 < var3; ++var4)
				{
					object var5 = var2[var4];
	
					if(var5 is IChatComponent)
					{
						((IChatComponent)var5).ChatStyle.ParentStyle = this.ChatStyle;
					}
				}
	
				if(this.lastTranslationUpdateTimeInMilliseconds > -1L)
				{
					IEnumerator var6 = this.children.GetEnumerator();
	
					while(var6.MoveNext())
					{
						IChatComponent var7 = (IChatComponent)var6.Current;
						var7.ChatStyle.ParentStyle = value;
					}
				}
	
				return this;
			}
		}

		public override IEnumerator iterator()
		{
			this.ensureInitialized();
			return Iterators.concat(createDeepCopyIterator(this.children), createDeepCopyIterator(this.siblings));
		}

///    
///     <summary> * Gets the text of this component, without any special formatting codes added, for chat.  TODO: why is this two
///     * different methods? </summary>
///     
		public virtual string UnformattedTextForChat
		{
			get
			{
				this.ensureInitialized();
				StringBuilder var1 = new StringBuilder();
				IEnumerator var2 = this.children.GetEnumerator();
	
				while(var2.MoveNext())
				{
					IChatComponent var3 = (IChatComponent)var2.Current;
					var1.Append(var3.UnformattedTextForChat);
				}
	
				return var1.ToString();
			}
		}

///    
///     <summary> * Creates a copy of this component.  Almost a deep copy, except the style is shallow-copied. </summary>
///     
		public virtual ChatComponentTranslation createCopy()
		{
			object[] var1 = new object[this.formatArgs.Length];

			for(int var2 = 0; var2 < this.formatArgs.Length; ++var2)
			{
				if(this.formatArgs[var2] is IChatComponent)
				{
					var1[var2] = ((IChatComponent)this.formatArgs[var2]).createCopy();
				}
				else
				{
					var1[var2] = this.formatArgs[var2];
				}
			}

			ChatComponentTranslation var5 = new ChatComponentTranslation(this.key, var1);
			var5.ChatStyle = this.ChatStyle.createShallowCopy();
			IEnumerator var3 = this.Siblings.GetEnumerator();

			while(var3.MoveNext())
			{
				IChatComponent var4 = (IChatComponent)var3.Current;
				var5.appendSibling(var4.createCopy());
			}

			return var5;
		}

		public override bool Equals(object p_equals_1_)
		{
			if(this == p_equals_1_)
			{
				return true;
			}
			else if(!(p_equals_1_ is ChatComponentTranslation))
			{
				return false;
			}
			else
			{
				ChatComponentTranslation var2 = (ChatComponentTranslation)p_equals_1_;
				return Array.Equals(this.formatArgs, var2.formatArgs) && this.key.Equals(var2.key) && base.Equals(p_equals_1_);
			}
		}

		public override int GetHashCode()
		{
			int var1 = base.GetHashCode();
			var1 = 31 * var1 + this.key.GetHashCode();
			var1 = 31 * var1 + Arrays.GetHashCode(this.formatArgs);
			return var1;
		}

		public override string ToString()
		{
			return "TranslatableComponent{key=\'" + this.key + '\'' + ", args=" + Arrays.ToString(this.formatArgs) + ", siblings=" + this.siblings + ", style=" + this.ChatStyle + '}';
		}

		public virtual string Key
		{
			get
			{
				return this.key;
			}
		}

		public virtual object[] FormatArgs
		{
			get
			{
				return this.formatArgs;
			}
		}
	}

}