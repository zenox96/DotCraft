namespace DotCraftCore.nUtil
{

	using Validate = org.apache.commons.lang3.Validate;

	public class ResourceLocation
	{
		private readonly string resourceDomain;
		private readonly string resourcePath;
		

		public ResourceLocation(string p_i1292_1_, string p_i1292_2_)
		{
			Validate.notNull(p_i1292_2_);

			if(p_i1292_1_ != null && p_i1292_1_.Length != 0)
			{
				this.resourceDomain = p_i1292_1_;
			}
			else
			{
				this.resourceDomain = "minecraft";
			}

			this.resourcePath = p_i1292_2_;
		}

		public ResourceLocation(string p_i1293_1_)
		{
			string var2 = "minecraft";
			string var3 = p_i1293_1_;
			int var4 = p_i1293_1_.IndexOf(58);

			if(var4 >= 0)
			{
				var3 = p_i1293_1_.Substring(var4 + 1, p_i1293_1_.Length - (var4 + 1));

				if(var4 > 1)
				{
					var2 = p_i1293_1_.Substring(0, var4);
				}
			}

			this.resourceDomain = var2.ToLower();
			this.resourcePath = var3;
		}

		public virtual string ResourcePath
		{
			get
			{
				return this.resourcePath;
			}
		}

		public virtual string ResourceDomain
		{
			get
			{
				return this.resourceDomain;
			}
		}

		public override string ToString()
		{
			return this.resourceDomain + ":" + this.resourcePath;
		}

		public override bool Equals(object p_equals_1_)
		{
			if(this == p_equals_1_)
			{
				return true;
			}
			else if(!(p_equals_1_ is ResourceLocation))
			{
				return false;
			}
			else
			{
				ResourceLocation var2 = (ResourceLocation)p_equals_1_;
				return this.resourceDomain.Equals(var2.resourceDomain) && this.resourcePath.Equals(var2.resourcePath);
			}
		}

		public override int GetHashCode()
		{
			return 31 * this.resourceDomain.GetHashCode() + this.resourcePath.GetHashCode();
		}
	}

}