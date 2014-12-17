namespace DotCraftCore.nRealms
{

	using Minecraft = DotCraftCore.client.Minecraft;
	using GuiButton = DotCraftCore.client.gui.GuiButton;
	using GuiButtonRealmsProxy = DotCraftCore.client.gui.GuiButtonRealmsProxy;

	public class RealmsButton
	{
		private GuiButtonRealmsProxy proxy;
		

		public RealmsButton(int p_i1177_1_, int p_i1177_2_, int p_i1177_3_, string p_i1177_4_)
		{
			this.proxy = new GuiButtonRealmsProxy(this, p_i1177_1_, p_i1177_2_, p_i1177_3_, p_i1177_4_);
		}

		public RealmsButton(int p_i1178_1_, int p_i1178_2_, int p_i1178_3_, int p_i1178_4_, int p_i1178_5_, string p_i1178_6_)
		{
			this.proxy = new GuiButtonRealmsProxy(this, p_i1178_1_, p_i1178_2_, p_i1178_3_, p_i1178_6_, p_i1178_4_, p_i1178_5_);
		}

		public virtual GuiButton Proxy
		{
			get
			{
				return this.proxy;
			}
		}

		public virtual int id()
		{
			return this.proxy.func_154314_d();
		}

		public virtual bool active()
		{
			return this.proxy.func_154315_e();
		}

		public virtual void active(bool p_active_1_)
		{
			this.proxy.func_154313_b(p_active_1_);
		}

		public virtual void msg(string p_msg_1_)
		{
			this.proxy.func_154311_a(p_msg_1_);
		}

		public virtual int Width
		{
			get
			{
				return this.proxy.func_146117_b();
			}
		}

		public virtual int Height
		{
			get
			{
				return this.proxy.func_154310_c();
			}
		}

		public virtual int y()
		{
			return this.proxy.func_154316_f();
		}

		public virtual void render(int p_render_1_, int p_render_2_)
		{
			this.proxy.drawButton(Minecraft.Minecraft, p_render_1_, p_render_2_);
		}

		public virtual void clicked(int p_clicked_1_, int p_clicked_2_)
		{
		}

		public virtual void released(int p_released_1_, int p_released_2_)
		{
		}

		public virtual void blit(int p_blit_1_, int p_blit_2_, int p_blit_3_, int p_blit_4_, int p_blit_5_, int p_blit_6_)
		{
			this.proxy.drawTexturedModalRect(p_blit_1_, p_blit_2_, p_blit_3_, p_blit_4_, p_blit_5_, p_blit_6_);
		}

		public virtual void renderBg(int p_renderBg_1_, int p_renderBg_2_)
		{
		}

		public virtual int getYImage(bool p_getYImage_1_)
		{
			return this.proxy.func_154312_c(p_getYImage_1_);
		}
	}

}