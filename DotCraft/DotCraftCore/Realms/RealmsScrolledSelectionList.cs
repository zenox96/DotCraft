namespace DotCraftCore.Realms
{

	using GuiSlotRealmsProxy = DotCraftCore.client.gui.GuiSlotRealmsProxy;

	public class RealmsScrolledSelectionList
	{
		private readonly GuiSlotRealmsProxy proxy;
		private const string __OBFID = "CL_00001863";

		public RealmsScrolledSelectionList(int p_i1119_1_, int p_i1119_2_, int p_i1119_3_, int p_i1119_4_, int p_i1119_5_)
		{
			this.proxy = new GuiSlotRealmsProxy(this, p_i1119_1_, p_i1119_2_, p_i1119_3_, p_i1119_4_, p_i1119_5_);
		}

		public virtual void render(int p_render_1_, int p_render_2_, float p_render_3_)
		{
			this.proxy.func_148128_a(p_render_1_, p_render_2_, p_render_3_);
		}

		public virtual int width()
		{
			return this.proxy.func_154338_k();
		}

		public virtual int ym()
		{
			return this.proxy.func_154339_l();
		}

		public virtual int xm()
		{
			return this.proxy.func_154337_m();
		}

		protected internal virtual void renderItem(int p_renderItem_1_, int p_renderItem_2_, int p_renderItem_3_, int p_renderItem_4_, Tezzelator p_renderItem_5_, int p_renderItem_6_, int p_renderItem_7_)
		{
		}

		public virtual void renderItem(int p_renderItem_1_, int p_renderItem_2_, int p_renderItem_3_, int p_renderItem_4_, int p_renderItem_5_, int p_renderItem_6_)
		{
			this.renderItem(p_renderItem_1_, p_renderItem_2_, p_renderItem_3_, p_renderItem_4_, Tezzelator.instance, p_renderItem_5_, p_renderItem_6_);
		}

		public virtual int ItemCount
		{
			get
			{
				return 0;
			}
		}

		public virtual void selectItem(int p_selectItem_1_, bool p_selectItem_2_, int p_selectItem_3_, int p_selectItem_4_)
		{
		}

		public virtual bool isSelectedItem(int p_isSelectedItem_1_)
		{
			return false;
		}

		public virtual void renderBackground()
		{
		}

		public virtual int MaxPosition
		{
			get
			{
				return 0;
			}
		}

		public virtual int ScrollbarPosition
		{
			get
			{
				return this.proxy.func_154338_k() / 2 + 124;
			}
		}
	}

}