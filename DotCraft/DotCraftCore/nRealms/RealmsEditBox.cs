using System;

namespace DotCraftCore.nRealms
{

	using Minecraft = DotCraftCore.client.Minecraft;
	using FontRenderer = DotCraftCore.client.gui.FontRenderer;
	using Gui = DotCraftCore.client.gui.Gui;
	using GuiScreen = DotCraftCore.client.gui.GuiScreen;
	using Tessellator = DotCraftCore.client.renderer.Tessellator;
	using ChatAllowedCharacters = DotCraftCore.nUtil.ChatAllowedCharacters;
	using GL11 = org.lwjgl.opengl.GL11;

	public class RealmsEditBox
	{
		public const int BACKWARDS = -1;
		public const int FORWARDS = 1;
		private const int CURSOR_INSERT_WIDTH = 1;
		private const int CURSOR_INSERT_COLOR = -3092272;
		private const string CURSOR_APPEND_CHARACTER = "_";
		private readonly FontRenderer font;
		private readonly int x;
		private readonly int y;
		private readonly int width;
		private readonly int height;
		private string value;
		private int maxLength;
		private int frame;
		private bool bordered;
		private bool canLoseFocus;
		private bool inFocus;
		private bool isEditable;
		private int displayPos;
		private int cursorPos;
		private int highlightPos;
		private int textColor;
		private int textColorUneditable;
		private bool visible;
		

		public RealmsEditBox(int p_i1111_1_, int p_i1111_2_, int p_i1111_3_, int p_i1111_4_) : this(Minecraft.getMinecraft().fontRenderer, p_i1111_1_, p_i1111_2_, p_i1111_3_, p_i1111_4_)
		{
		}

		public RealmsEditBox(FontRenderer p_i1112_1_, int p_i1112_2_, int p_i1112_3_, int p_i1112_4_, int p_i1112_5_)
		{
			this.value = "";
			this.maxLength = 32;
			this.bordered = true;
			this.canLoseFocus = true;
			this.isEditable = true;
			this.textColor = 14737632;
			this.textColorUneditable = 7368816;
			this.visible = true;
			this.font = p_i1112_1_;
			this.x = p_i1112_2_;
			this.y = p_i1112_3_;
			this.width = p_i1112_4_;
			this.height = p_i1112_5_;
		}

		public virtual void tick()
		{
			++this.frame;
		}

		public virtual string Value
		{
			set
			{
				if (value.Length > this.maxLength)
				{
					this.value = value.Substring(0, this.maxLength);
				}
				else
				{
					this.value = value;
				}
	
				this.moveCursorToEnd();
			}
			get
			{
				return this.value;
			}
		}


		public virtual string Highlighted
		{
			get
			{
				int var1 = this.cursorPos < this.highlightPos ? this.cursorPos : this.highlightPos;
				int var2 = this.cursorPos < this.highlightPos ? this.highlightPos : this.cursorPos;
				return this.value.Substring(var1, var2 - var1);
			}
		}

		public virtual void insertText(string p_insertText_1_)
		{
			string var2 = "";
			string var3 = ChatAllowedCharacters.filerAllowedCharacters(p_insertText_1_);
			int var4 = this.cursorPos < this.highlightPos ? this.cursorPos : this.highlightPos;
			int var5 = this.cursorPos < this.highlightPos ? this.highlightPos : this.cursorPos;
			int var6 = this.maxLength - this.value.Length - (var4 - this.highlightPos);
			bool var7 = false;

			if (this.value.Length > 0)
			{
				var2 = var2 + this.value.Substring(0, var4);
			}

			int var8;

			if (var6 < var3.Length)
			{
				var2 = var2 + var3.Substring(0, var6);
				var8 = var6;
			}
			else
			{
				var2 = var2 + var3;
				var8 = var3.Length;
			}

			if (this.value.Length > 0 && var5 < this.value.Length)
			{
				var2 = var2 + this.value.Substring(var5);
			}

			this.value = var2;
			this.moveCursor(var4 - this.highlightPos + var8);
		}

		public virtual void deleteWords(int p_deleteWords_1_)
		{
			if (this.value.Length != 0)
			{
				if (this.highlightPos != this.cursorPos)
				{
					this.insertText("");
				}
				else
				{
					this.deleteChars(this.getWordPosition(p_deleteWords_1_) - this.cursorPos);
				}
			}
		}

		public virtual void deleteChars(int p_deleteChars_1_)
		{
			if (this.value.Length != 0)
			{
				if (this.highlightPos != this.cursorPos)
				{
					this.insertText("");
				}
				else
				{
					bool var2 = p_deleteChars_1_ < 0;
					int var3 = var2 ? this.cursorPos + p_deleteChars_1_ : this.cursorPos;
					int var4 = var2 ? this.cursorPos : this.cursorPos + p_deleteChars_1_;
					string var5 = "";

					if (var3 >= 0)
					{
						var5 = this.value.Substring(0, var3);
					}

					if (var4 < this.value.Length)
					{
						var5 = var5 + this.value.Substring(var4);
					}

					this.value = var5;

					if (var2)
					{
						this.moveCursor(p_deleteChars_1_);
					}
				}
			}
		}

		public virtual int getWordPosition(int p_getWordPosition_1_)
		{
			return this.getWordPosition(p_getWordPosition_1_, this.CursorPosition);
		}

		public virtual int getWordPosition(int p_getWordPosition_1_, int p_getWordPosition_2_)
		{
			return this.getWordPosition(p_getWordPosition_1_, this.CursorPosition, true);
		}

		public virtual int getWordPosition(int p_getWordPosition_1_, int p_getWordPosition_2_, bool p_getWordPosition_3_)
		{
			int var4 = p_getWordPosition_2_;
			bool var5 = p_getWordPosition_1_ < 0;
			int var6 = Math.Abs(p_getWordPosition_1_);

			for (int var7 = 0; var7 < var6; ++var7)
			{
				if (var5)
				{
					while (p_getWordPosition_3_ && var4 > 0 && this.value[var4 - 1] == 32)
					{
						--var4;
					}

					while (var4 > 0 && this.value[var4 - 1] != 32)
					{
						--var4;
					}
				}
				else
				{
					int var8 = this.value.Length;
					var4 = this.value.IndexOf(32, var4);

					if (var4 == -1)
					{
						var4 = var8;
					}
					else
					{
						while (p_getWordPosition_3_ && var4 < var8 && this.value[var4] == 32)
						{
							++var4;
						}
					}
				}
			}

			return var4;
		}

		public virtual void moveCursor(int p_moveCursor_1_)
		{
			this.moveCursorTo(this.highlightPos + p_moveCursor_1_);
		}

		public virtual void moveCursorTo(int p_moveCursorTo_1_)
		{
			this.cursorPos = p_moveCursorTo_1_;
			int var2 = this.value.Length;

			if (this.cursorPos < 0)
			{
				this.cursorPos = 0;
			}

			if (this.cursorPos > var2)
			{
				this.cursorPos = var2;
			}

			this.HighlightPos = this.cursorPos;
		}

		public virtual void moveCursorToStart()
		{
			this.moveCursorTo(0);
		}

		public virtual void moveCursorToEnd()
		{
			this.moveCursorTo(this.value.Length);
		}

		public virtual bool keyPressed(char p_keyPressed_1_, int p_keyPressed_2_)
		{
			if (!this.inFocus)
			{
				return false;
			}
			else
			{
				switch (p_keyPressed_1_)
				{
					case 1:
						this.moveCursorToEnd();
						this.HighlightPos = 0;
						return true;

					case 3:
						GuiScreen.ClipboardString = this.Highlighted;
						return true;

					case 22:
						if (this.isEditable)
						{
							this.insertText(GuiScreen.ClipboardString);
						}

						return true;

					case 24:
						GuiScreen.ClipboardString = this.Highlighted;

						if (this.isEditable)
						{
							this.insertText("");
						}

						return true;

					default:
						switch (p_keyPressed_2_)
						{
							case 14:
								if (GuiScreen.CtrlKeyDown)
								{
									if (this.isEditable)
									{
										this.deleteWords(-1);
									}
								}
								else if (this.isEditable)
								{
									this.deleteChars(-1);
								}

								return true;

							case 199:
								if (GuiScreen.ShiftKeyDown)
								{
									this.HighlightPos = 0;
								}
								else
								{
									this.moveCursorToStart();
								}

								return true;

							case 203:
								if (GuiScreen.ShiftKeyDown)
								{
									if (GuiScreen.CtrlKeyDown)
									{
										this.HighlightPos = this.getWordPosition(-1, this.HighlightPos);
									}
									else
									{
										this.HighlightPos = this.HighlightPos - 1;
									}
								}
								else if (GuiScreen.CtrlKeyDown)
								{
									this.moveCursorTo(this.getWordPosition(-1));
								}
								else
								{
									this.moveCursor(-1);
								}

								return true;

							case 205:
								if (GuiScreen.ShiftKeyDown)
								{
									if (GuiScreen.CtrlKeyDown)
									{
										this.HighlightPos = this.getWordPosition(1, this.HighlightPos);
									}
									else
									{
										this.HighlightPos = this.HighlightPos + 1;
									}
								}
								else if (GuiScreen.CtrlKeyDown)
								{
									this.moveCursorTo(this.getWordPosition(1));
								}
								else
								{
									this.moveCursor(1);
								}

								return true;

							case 207:
								if (GuiScreen.ShiftKeyDown)
								{
									this.HighlightPos = this.value.Length;
								}
								else
								{
									this.moveCursorToEnd();
								}

								return true;

							case 211:
								if (GuiScreen.CtrlKeyDown)
								{
									if (this.isEditable)
									{
										this.deleteWords(1);
									}
								}
								else if (this.isEditable)
								{
									this.deleteChars(1);
								}

								return true;

							default:
								if (ChatAllowedCharacters.isAllowedCharacter(p_keyPressed_1_))
								{
									if (this.isEditable)
									{
										this.insertText(char.ToString(p_keyPressed_1_));
									}

									return true;
								}
								else
								{
									return false;
								}
						}
					break;
				}
			}
		}

		public virtual void mouseClicked(int p_mouseClicked_1_, int p_mouseClicked_2_, int p_mouseClicked_3_)
		{
			bool var4 = p_mouseClicked_1_ >= this.x && p_mouseClicked_1_ < this.x + this.width && p_mouseClicked_2_ >= this.y && p_mouseClicked_2_ < this.y + this.height;

			if (this.canLoseFocus)
			{
				this.Focus = var4;
			}

			if (this.inFocus && p_mouseClicked_3_ == 0)
			{
				int var5 = p_mouseClicked_1_ - this.x;

				if (this.bordered)
				{
					var5 -= 4;
				}

				string var6 = this.font.trimStringToWidth(this.value.Substring(this.displayPos), this.InnerWidth);
				this.moveCursorTo(this.font.trimStringToWidth(var6, var5).Length + this.displayPos);
			}
		}

		public virtual void render()
		{
			if (this.Visible)
			{
				if (this.Bordered)
				{
					Gui.drawRect(this.x - 1, this.y - 1, this.x + this.width + 1, this.y + this.height + 1, -6250336);
					Gui.drawRect(this.x, this.y, this.x + this.width, this.y + this.height, -16777216);
				}

				int var1 = this.isEditable ? this.textColor : this.textColorUneditable;
				int var2 = this.cursorPos - this.displayPos;
				int var3 = this.highlightPos - this.displayPos;
				string var4 = this.font.trimStringToWidth(this.value.Substring(this.displayPos), this.InnerWidth);
				bool var5 = var2 >= 0 && var2 <= var4.Length;
				bool var6 = this.inFocus && this.frame / 6 % 2 == 0 && var5;
				int var7 = this.bordered ? this.x + 4 : this.x;
				int var8 = this.bordered ? this.y + (this.height - 8) / 2 : this.y;
				int var9 = var7;

				if (var3 > var4.Length)
				{
					var3 = var4.Length;
				}

				if (var4.Length > 0)
				{
					string var10 = var5 ? var4.Substring(0, var2) : var4;
					var9 = this.font.drawStringWithShadow(var10, var7, var8, var1);
				}

				bool var13 = this.cursorPos < this.value.Length || this.value.Length >= this.MaxLength;
				int var11 = var9;

				if (!var5)
				{
					var11 = var2 > 0 ? var7 + this.width : var7;
				}
				else if (var13)
				{
					var11 = var9 - 1;
					--var9;
				}

				if (var4.Length > 0 && var5 && var2 < var4.Length)
				{
					this.font.drawStringWithShadow(var4.Substring(var2), var9, var8, var1);
				}

				if (var6)
				{
					if (var13)
					{
						Gui.drawRect(var11, var8 - 1, var11 + 1, var8 + 1 + this.font.FONT_HEIGHT, -3092272);
					}
					else
					{
						this.font.drawStringWithShadow("_", var11, var8, var1);
					}
				}

				if (var3 != var2)
				{
					int var12 = var7 + this.font.getStringWidth(var4.Substring(0, var3));
					this.renderHighlight(var11, var8 - 1, var12 - 1, var8 + 1 + this.font.FONT_HEIGHT);
				}
			}
		}

		private void renderHighlight(int p_renderHighlight_1_, int p_renderHighlight_2_, int p_renderHighlight_3_, int p_renderHighlight_4_)
		{
			int var5;

			if (p_renderHighlight_1_ < p_renderHighlight_3_)
			{
				var5 = p_renderHighlight_1_;
				p_renderHighlight_1_ = p_renderHighlight_3_;
				p_renderHighlight_3_ = var5;
			}

			if (p_renderHighlight_2_ < p_renderHighlight_4_)
			{
				var5 = p_renderHighlight_2_;
				p_renderHighlight_2_ = p_renderHighlight_4_;
				p_renderHighlight_4_ = var5;
			}

			if (p_renderHighlight_3_ > this.x + this.width)
			{
				p_renderHighlight_3_ = this.x + this.width;
			}

			if (p_renderHighlight_1_ > this.x + this.width)
			{
				p_renderHighlight_1_ = this.x + this.width;
			}

			Tessellator var6 = Tessellator.instance;
			GL11.glColor4f(0.0F, 0.0F, 255.0F, 255.0F);
			GL11.glDisable(GL11.GL_TEXTURE_2D);
			GL11.glEnable(GL11.GL_COLOR_LOGIC_OP);
			GL11.glLogicOp(GL11.GL_OR_REVERSE);
			var6.startDrawingQuads();
			var6.addVertex((double)p_renderHighlight_1_, (double)p_renderHighlight_4_, 0.0D);
			var6.addVertex((double)p_renderHighlight_3_, (double)p_renderHighlight_4_, 0.0D);
			var6.addVertex((double)p_renderHighlight_3_, (double)p_renderHighlight_2_, 0.0D);
			var6.addVertex((double)p_renderHighlight_1_, (double)p_renderHighlight_2_, 0.0D);
			var6.draw();
			GL11.glDisable(GL11.GL_COLOR_LOGIC_OP);
			GL11.glEnable(GL11.GL_TEXTURE_2D);
		}

		public virtual int MaxLength
		{
			set
			{
				this.maxLength = value;
	
				if (this.value.Length > value)
				{
					this.value = this.value.Substring(0, value);
				}
			}
			get
			{
				return this.maxLength;
			}
		}


		public virtual int CursorPosition
		{
			get
			{
				return this.cursorPos;
			}
		}

		public virtual bool isBordered()
		{
			get
			{
				return this.bordered;
			}
			set
			{
				this.bordered = value;
			}
		}


		public virtual int TextColor
		{
			get
			{
				return this.textColor;
			}
			set
			{
				this.textColor = value;
			}
		}


		public virtual int TextColorUneditable
		{
			get
			{
				return this.textColorUneditable;
			}
			set
			{
				this.textColorUneditable = value;
			}
		}


		public virtual bool Focus
		{
			set
			{
				if (value && !this.inFocus)
				{
					this.frame = 0;
				}
	
				this.inFocus = value;
			}
		}

		public virtual bool isFocused()
		{
			get
			{
				return this.inFocus;
			}
		}

		public virtual bool isIsEditable()
		{
			get
			{
				return this.isEditable;
			}
			set
			{
				this.isEditable = value;
			}
		}


		public virtual int HighlightPos
		{
			get
			{
				return this.highlightPos;
			}
			set
			{
				int var2 = this.value.Length;
	
				if (value > var2)
				{
					value = var2;
				}
	
				if (value < 0)
				{
					value = 0;
				}
	
				this.highlightPos = value;
	
				if (this.font != null)
				{
					if (this.displayPos > var2)
					{
						this.displayPos = var2;
					}
	
					int var3 = this.InnerWidth;
					string var4 = this.font.trimStringToWidth(this.value.Substring(this.displayPos), var3);
					int var5 = var4.Length + this.displayPos;
	
					if (value == this.displayPos)
					{
						this.displayPos -= this.font.trimStringToWidth(this.value, var3, true).Length;
					}
	
					if (value > var5)
					{
						this.displayPos += value - var5;
					}
					else if (value <= this.displayPos)
					{
						this.displayPos -= this.displayPos - value;
					}
	
					if (this.displayPos < 0)
					{
						this.displayPos = 0;
					}
	
					if (this.displayPos > var2)
					{
						this.displayPos = var2;
					}
				}
			}
		}

		public virtual int InnerWidth
		{
			get
			{
				return this.Bordered ? this.width - 8 : this.width;
			}
		}


		public virtual bool isCanLoseFocus()
		{
			get
			{
				return this.canLoseFocus;
			}
			set
			{
				this.canLoseFocus = value;
			}
		}


		public virtual bool isVisible()
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
			}
		}

	}

}