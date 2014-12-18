using System.Collections;

namespace DotCraftCore.nRealms
{

	using IChatComponent = DotCraftCore.nUtil.IChatComponent;

	public class DisconnectedOnlineScreen : RealmsScreen
	{
		private string title;
		private IChatComponent reason;
		private IList lines;
		private readonly RealmsScreen parent;
		

		public DisconnectedOnlineScreen(RealmsScreen p_i1000_1_, string p_i1000_2_, IChatComponent p_i1000_3_)
		{
			this.parent = p_i1000_1_;
			this.title = getLocalizedString(p_i1000_2_);
			this.reason = p_i1000_3_;
		}

		public override void init()
		{
			this.buttonsClear();
			this.buttonsAdd(newButton(0, this.width() / 2 - 100, this.height() / 4 + 120 + 12, getLocalizedString("gui.back")));
			this.lines = this.fontSplit(this.reason.FormattedText, this.width() - 50);
		}

		public override void keyPressed(char p_keyPressed_1_, int p_keyPressed_2_)
		{
			if (p_keyPressed_2_ == 1)
			{
				Realms.Screen = this.parent;
			}
		}

		public override void buttonClicked(RealmsButton p_buttonClicked_1_)
		{
			if (p_buttonClicked_1_.id() == 0)
			{
				Realms.Screen = this.parent;
			}
		}

		public override void render(int p_render_1_, int p_render_2_, float p_render_3_)
		{
			this.renderBackground();
			this.drawCenteredString(this.title, this.width() / 2, this.height() / 2 - 50, 11184810);
			int var4 = this.height() / 2 - 30;

			if (this.lines != null)
			{
				for (IEnumerator var5 = this.lines.GetEnumerator(); var5.MoveNext(); var4 += this.fontLineHeight())
				{
					string var6 = (string)var5.Current;
					this.drawCenteredString(var6, this.width() / 2, var4, 16777215);
				}
			}

			base.render(p_render_1_, p_render_2_, p_render_3_);
		}
	}

}