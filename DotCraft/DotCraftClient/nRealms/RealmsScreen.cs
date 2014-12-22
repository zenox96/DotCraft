using System.Collections;

namespace DotCraftServer.nRealms
{
	public class RealmsScreen
	{
		public const int SKIN_HEAD_U = 8;
		public const int SKIN_HEAD_V = 8;
		public const int SKIN_HEAD_WIDTH = 8;
		public const int SKIN_HEAD_HEIGHT = 8;
		public const int SKIN_TEX_WIDTH = 64;
		public const int SKIN_TEX_HEIGHT = 32;
		public const int SKIN_HAT_U = 40;
		public const int SKIN_HAT_V = 8;
		public const int SKIN_HAT_WIDTH = 8;
		public const int SKIN_HAT_HEIGHT = 8;
		protected internal Minecraft minecraft;
		public int width;
		public int height;
		private GuiScreenRealmsProxy proxy = new GuiScreenRealmsProxy(this);
		

		public virtual GuiScreenRealmsProxy Proxy
		{
			get
			{
				return this.proxy;
			}
		}

		public virtual void init()
		{
		}

		public virtual void init(Minecraft p_init_1_, int p_init_2_, int p_init_3_)
		{
		}

		public virtual void drawCenteredString(string p_drawCenteredString_1_, int p_drawCenteredString_2_, int p_drawCenteredString_3_, int p_drawCenteredString_4_)
		{
			this.proxy.func_154325_a(p_drawCenteredString_1_, p_drawCenteredString_2_, p_drawCenteredString_3_, p_drawCenteredString_4_);
		}

		public virtual void drawString(string p_drawString_1_, int p_drawString_2_, int p_drawString_3_, int p_drawString_4_)
		{
			this.proxy.func_154322_b(p_drawString_1_, p_drawString_2_, p_drawString_3_, p_drawString_4_);
		}

		public virtual void blit(int p_blit_1_, int p_blit_2_, int p_blit_3_, int p_blit_4_, int p_blit_5_, int p_blit_6_)
		{
			this.proxy.drawTexturedModalRect(p_blit_1_, p_blit_2_, p_blit_3_, p_blit_4_, p_blit_5_, p_blit_6_);
		}

		public static void blit(int p_blit_0_, int p_blit_1_, float p_blit_2_, float p_blit_3_, int p_blit_4_, int p_blit_5_, int p_blit_6_, int p_blit_7_, float p_blit_8_, float p_blit_9_)
		{
			Gui.func_152125_a(p_blit_0_, p_blit_1_, p_blit_2_, p_blit_3_, p_blit_4_, p_blit_5_, p_blit_6_, p_blit_7_, p_blit_8_, p_blit_9_);
		}

		public static void blit(int p_blit_0_, int p_blit_1_, float p_blit_2_, float p_blit_3_, int p_blit_4_, int p_blit_5_, float p_blit_6_, float p_blit_7_)
		{
			Gui.func_146110_a(p_blit_0_, p_blit_1_, p_blit_2_, p_blit_3_, p_blit_4_, p_blit_5_, p_blit_6_, p_blit_7_);
		}

		public virtual void fillGradient(int p_fillGradient_1_, int p_fillGradient_2_, int p_fillGradient_3_, int p_fillGradient_4_, int p_fillGradient_5_, int p_fillGradient_6_)
		{
			this.proxy.drawGradientRect(p_fillGradient_1_, p_fillGradient_2_, p_fillGradient_3_, p_fillGradient_4_, p_fillGradient_5_, p_fillGradient_6_);
		}

		public virtual void renderBackground()
		{
			this.proxy.drawDefaultBackground();
		}

		public virtual bool isPauseScreen()
		{
			get
			{
				return this.proxy.doesGuiPauseGame();
			}
		}

		public virtual void renderBackground(int p_renderBackground_1_)
		{
			this.proxy.func_146270_b(p_renderBackground_1_);
		}

		public virtual void render(int p_render_1_, int p_render_2_, float p_render_3_)
		{
			for (int var4 = 0; var4 < this.proxy.func_154320_j().size(); ++var4)
			{
				((RealmsButton)this.proxy.func_154320_j().get(var4)).render(p_render_1_, p_render_2_);
			}
		}

		public virtual void renderTooltip(ItemStack p_renderTooltip_1_, int p_renderTooltip_2_, int p_renderTooltip_3_)
		{
			this.proxy.func_146285_a(p_renderTooltip_1_, p_renderTooltip_2_, p_renderTooltip_3_);
		}

		public virtual void renderTooltip(string p_renderTooltip_1_, int p_renderTooltip_2_, int p_renderTooltip_3_)
		{
			this.proxy.func_146279_a(p_renderTooltip_1_, p_renderTooltip_2_, p_renderTooltip_3_);
		}

		public virtual void renderTooltip(IList p_renderTooltip_1_, int p_renderTooltip_2_, int p_renderTooltip_3_)
		{
			this.proxy.func_146283_a(p_renderTooltip_1_, p_renderTooltip_2_, p_renderTooltip_3_);
		}

		public static void bindFace(string p_bindFace_0_)
		{
			ResourceLocation var1 = AbstractClientPlayer.getLocationSkin(p_bindFace_0_);

			if (var1 == null)
			{
				var1 = AbstractClientPlayer.getLocationSkin("default");
			}

			AbstractClientPlayer.getDownloadImageSkin(var1, p_bindFace_0_);
			Minecraft.Minecraft.TextureManager.bindTexture(var1);
		}

		public static void bind(string p_bind_0_)
		{
			ResourceLocation var1 = new ResourceLocation(p_bind_0_);
			Minecraft.Minecraft.TextureManager.bindTexture(var1);
		}

		public virtual void tick()
		{
		}

		public virtual int width()
		{
			return this.proxy.width;
		}

		public virtual int height()
		{
			return this.proxy.height;
		}

		public virtual int fontLineHeight()
		{
			return this.proxy.func_154329_h();
		}

		public virtual int fontWidth(string p_fontWidth_1_)
		{
			return this.proxy.func_154326_c(p_fontWidth_1_);
		}

		public virtual void fontDrawShadow(string p_fontDrawShadow_1_, int p_fontDrawShadow_2_, int p_fontDrawShadow_3_, int p_fontDrawShadow_4_)
		{
			this.proxy.func_154319_c(p_fontDrawShadow_1_, p_fontDrawShadow_2_, p_fontDrawShadow_3_, p_fontDrawShadow_4_);
		}

		public virtual IList fontSplit(string p_fontSplit_1_, int p_fontSplit_2_)
		{
			return this.proxy.func_154323_a(p_fontSplit_1_, p_fontSplit_2_);
		}

		public virtual void buttonClicked(RealmsButton p_buttonClicked_1_)
		{
		}

		public static RealmsButton newButton(int p_newButton_0_, int p_newButton_1_, int p_newButton_2_, string p_newButton_3_)
		{
			return new RealmsButton(p_newButton_0_, p_newButton_1_, p_newButton_2_, p_newButton_3_);
		}

		public static RealmsButton newButton(int p_newButton_0_, int p_newButton_1_, int p_newButton_2_, int p_newButton_3_, int p_newButton_4_, string p_newButton_5_)
		{
			return new RealmsButton(p_newButton_0_, p_newButton_1_, p_newButton_2_, p_newButton_3_, p_newButton_4_, p_newButton_5_);
		}

		public virtual void buttonsClear()
		{
			this.proxy.func_154324_i();
		}

		public virtual void buttonsAdd(RealmsButton p_buttonsAdd_1_)
		{
			this.proxy.func_154327_a(p_buttonsAdd_1_);
		}

		public virtual IList buttons()
		{
			return this.proxy.func_154320_j();
		}

		public virtual void buttonsRemove(RealmsButton p_buttonsRemove_1_)
		{
			this.proxy.func_154328_b(p_buttonsRemove_1_);
		}

		public virtual RealmsEditBox newEditBox(int p_newEditBox_1_, int p_newEditBox_2_, int p_newEditBox_3_, int p_newEditBox_4_)
		{
			return new RealmsEditBox(p_newEditBox_1_, p_newEditBox_2_, p_newEditBox_3_, p_newEditBox_4_);
		}

		public virtual void mouseClicked(int p_mouseClicked_1_, int p_mouseClicked_2_, int p_mouseClicked_3_)
		{
		}

		public virtual void mouseEvent()
		{
		}

		public virtual void keyboardEvent()
		{
		}

		public virtual void mouseReleased(int p_mouseReleased_1_, int p_mouseReleased_2_, int p_mouseReleased_3_)
		{
		}

		public virtual void mouseDragged(int p_mouseDragged_1_, int p_mouseDragged_2_, int p_mouseDragged_3_, long p_mouseDragged_4_)
		{
		}

		public virtual void keyPressed(char p_keyPressed_1_, int p_keyPressed_2_)
		{
		}

		public virtual void confirmResult(bool p_confirmResult_1_, int p_confirmResult_2_)
		{
		}

		public static string getLocalizedString(string p_getLocalizedString_0_)
		{
			return I18n.format(p_getLocalizedString_0_, new object[0]);
		}

		public static string getLocalizedString(string p_getLocalizedString_0_, params object[] p_getLocalizedString_1_)
		{
			return I18n.format(p_getLocalizedString_0_, p_getLocalizedString_1_);
		}

		public virtual RealmsAnvilLevelStorageSource LevelStorageSource
		{
			get
			{
				return new RealmsAnvilLevelStorageSource(Minecraft.Minecraft.SaveLoader);
			}
		}

		public virtual void removed()
		{
		}
	}

}