namespace DotCraftCore.Entity.AI.Attributes
{

	public interface IAttribute
	{
		string AttributeUnlocalizedName {get;}

		double clampValue(double p_111109_1_);

		double DefaultValue {get;}

		bool ShouldWatch {get;}
	}

}