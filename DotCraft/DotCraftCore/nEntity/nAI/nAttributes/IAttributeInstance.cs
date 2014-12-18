using System.Collections;

namespace DotCraftCore.nEntity.nAI.nAttributes
{


	public interface IAttributeInstance
	{
///    
///     <summary> * Get the Attribute this is an instance of </summary>
///     
		IAttribute Attribute {get;}

		double BaseValue {get;set;}


		ICollection func_111122_c();

///    
///     <summary> * Returns attribute modifier, if any, by the given UUID </summary>
///     
		AttributeModifier getModifier(UUID p_111127_1_);

		void applyModifier(AttributeModifier p_111121_1_);

		void removeModifier(AttributeModifier p_111124_1_);

		void removeAllModifiers();

		double AttributeValue {get;}
	}

}