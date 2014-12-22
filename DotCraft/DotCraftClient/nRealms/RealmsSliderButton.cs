using System;

namespace DotCraftServer.nRealms
{
	public class RealmsSliderButton : RealmsButton
	{
		public float value;
		public bool sliding;
		private readonly float minValue;
		private readonly float maxValue;
		private int steps;
		

		public RealmsSliderButton(int p_i1056_1_, int p_i1056_2_, int p_i1056_3_, int p_i1056_4_, int p_i1056_5_, int p_i1056_6_) : this(p_i1056_1_, p_i1056_2_, p_i1056_3_, p_i1056_4_, p_i1056_6_, 0, 1.0F, (float)p_i1056_5_)
		{
		}

		public RealmsSliderButton(int p_i1057_1_, int p_i1057_2_, int p_i1057_3_, int p_i1057_4_, int p_i1057_5_, int p_i1057_6_, float p_i1057_7_, float p_i1057_8_) : base(p_i1057_1_, p_i1057_2_, p_i1057_3_, p_i1057_4_, 20, "")
		{
			this.value = 1.0F;
			this.minValue = p_i1057_7_;
			this.maxValue = p_i1057_8_;
			this.value = this.toPct((float)p_i1057_6_);
			this.Proxy.displayString = this.Message;
		}

		public virtual string Message
		{
			get
			{
				return "";
			}
		}

		public virtual float toPct(float p_toPct_1_)
		{
			return MathHelper.clamp_float((this.clamp(p_toPct_1_) - this.minValue) / (this.maxValue - this.minValue), 0.0F, 1.0F);
		}

		public virtual float toValue(float p_toValue_1_)
		{
			return this.clamp(this.minValue + (this.maxValue - this.minValue) * MathHelper.clamp_float(p_toValue_1_, 0.0F, 1.0F));
		}

		public virtual float clamp(float p_clamp_1_)
		{
			p_clamp_1_ = this.clampSteps(p_clamp_1_);
			return MathHelper.clamp_float(p_clamp_1_, this.minValue, this.maxValue);
		}

		protected internal virtual float clampSteps(float p_clampSteps_1_)
		{
			if (this.steps > 0)
			{
				p_clampSteps_1_ = (float)(this.steps * Math.Round(p_clampSteps_1_ / (float)this.steps));
			}

			return p_clampSteps_1_;
		}

		public override int getYImage(bool p_getYImage_1_)
		{
			return 0;
		}

		public override void renderBg(int p_renderBg_1_, int p_renderBg_2_)
		{
			if (this.Proxy.field_146125_m)
			{
				if (this.sliding)
				{
					this.value = (float)(p_renderBg_1_ - (this.Proxy.field_146128_h + 4)) / (float)(this.Proxy.func_146117_b() - 8);

					if (this.value < 0.0F)
					{
						this.value = 0.0F;
					}

					if (this.value > 1.0F)
					{
						this.value = 1.0F;
					}

					float var3 = this.toValue(this.value);
					this.clicked(var3);
					this.value = this.toPct(var3);
					this.Proxy.displayString = this.Message;
				}

				GL11.glColor4f(1.0F, 1.0F, 1.0F, 1.0F);
				this.blit(this.Proxy.field_146128_h + (int)(this.value * (float)(this.Proxy.func_146117_b() - 8)), this.Proxy.field_146129_i, 0, 66, 4, 20);
				this.blit(this.Proxy.field_146128_h + (int)(this.value * (float)(this.Proxy.func_146117_b() - 8)) + 4, this.Proxy.field_146129_i, 196, 66, 4, 20);
			}
		}

		public override void clicked(int p_clicked_1_, int p_clicked_2_)
		{
			this.value = (float)(p_clicked_1_ - (this.Proxy.field_146128_h + 4)) / (float)(this.Proxy.func_146117_b() - 8);

			if (this.value < 0.0F)
			{
				this.value = 0.0F;
			}

			if (this.value > 1.0F)
			{
				this.value = 1.0F;
			}

			this.clicked(this.toValue(this.value));
			this.Proxy.displayString = this.Message;
			this.sliding = true;
		}

		public virtual void clicked(float p_clicked_1_)
		{
		}

		public override void released(int p_released_1_, int p_released_2_)
		{
			this.sliding = false;
		}
	}

}