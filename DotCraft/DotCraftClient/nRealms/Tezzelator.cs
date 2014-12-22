namespace DotCraftServer.nRealms
{
	public class Tezzelator
	{
		public static Tessellator t = Tessellator.instance;
		public static readonly Tezzelator instance = new Tezzelator();
		

		public virtual int end()
		{
			return t.draw();
		}

		public virtual void vertex(double p_vertex_1_, double p_vertex_3_, double p_vertex_5_)
		{
			t.addVertex(p_vertex_1_, p_vertex_3_, p_vertex_5_);
		}

		public virtual void color(float p_color_1_, float p_color_2_, float p_color_3_, float p_color_4_)
		{
			t.setColorRGBA_F(p_color_1_, p_color_2_, p_color_3_, p_color_4_);
		}

		public virtual void color(int p_color_1_, int p_color_2_, int p_color_3_)
		{
			t.setColorOpaque(p_color_1_, p_color_2_, p_color_3_);
		}

		public virtual void tex2(int p_tex2_1_)
		{
			t.Brightness = p_tex2_1_;
		}

		public virtual void normal(float p_normal_1_, float p_normal_2_, float p_normal_3_)
		{
			t.setNormal(p_normal_1_, p_normal_2_, p_normal_3_);
		}

		public virtual void noColor()
		{
			t.disableColor();
		}

		public virtual void color(int p_color_1_)
		{
			t.ColorOpaque_I = p_color_1_;
		}

		public virtual void color(float p_color_1_, float p_color_2_, float p_color_3_)
		{
			t.setColorOpaque_F(p_color_1_, p_color_2_, p_color_3_);
		}

		public virtual TesselatorVertexState sortQuads(float p_sortQuads_1_, float p_sortQuads_2_, float p_sortQuads_3_)
		{
			return t.getVertexState(p_sortQuads_1_, p_sortQuads_2_, p_sortQuads_3_);
		}

		public virtual void restoreState(TesselatorVertexState p_restoreState_1_)
		{
			t.VertexState = p_restoreState_1_;
		}

		public virtual void begin(int p_begin_1_)
		{
			t.startDrawing(p_begin_1_);
		}

		public virtual void begin()
		{
			t.startDrawingQuads();
		}

		public virtual void vertexUV(double p_vertexUV_1_, double p_vertexUV_3_, double p_vertexUV_5_, double p_vertexUV_7_, double p_vertexUV_9_)
		{
			t.addVertexWithUV(p_vertexUV_1_, p_vertexUV_3_, p_vertexUV_5_, p_vertexUV_7_, p_vertexUV_9_);
		}

		public virtual void color(int p_color_1_, int p_color_2_)
		{
			t.setColorRGBA_I(p_color_1_, p_color_2_);
		}

		public virtual void offset(double p_offset_1_, double p_offset_3_, double p_offset_5_)
		{
			t.setTranslation(p_offset_1_, p_offset_3_, p_offset_5_);
		}

		public virtual void color(int p_color_1_, int p_color_2_, int p_color_3_, int p_color_4_)
		{
			t.setColorRGBA(p_color_1_, p_color_2_, p_color_3_, p_color_4_);
		}

		public virtual void addOffset(float p_addOffset_1_, float p_addOffset_2_, float p_addOffset_3_)
		{
			t.addTranslation(p_addOffset_1_, p_addOffset_2_, p_addOffset_3_);
		}

		public virtual void tex(double p_tex_1_, double p_tex_3_)
		{
			t.setTextureUV(p_tex_1_, p_tex_3_);
		}

		public virtual void color(sbyte p_color_1_, sbyte p_color_2_, sbyte p_color_3_)
		{
			t.func_154352_a(p_color_1_, p_color_2_, p_color_3_);
		}
	}

}