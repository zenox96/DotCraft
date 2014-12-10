using System.Collections;

namespace DotCraftCore.Entity.AI.Attributes
{

	using Multimap = com.google.common.collect.Multimap;
	using LowerStringMap = DotCraftCore.Server.Management.LowerStringMap;

	public abstract class BaseAttributeMap
	{
		protected internal readonly IDictionary attributes = new Hashtable();
		protected internal readonly IDictionary attributesByName = new LowerStringMap();
		private const string __OBFID = "CL_00001566";

		public virtual IAttributeInstance getAttributeInstance(IAttribute p_111151_1_)
		{
			return (IAttributeInstance)this.attributes.get(p_111151_1_);
		}

		public virtual IAttributeInstance getAttributeInstanceByName(string p_111152_1_)
		{
			return (IAttributeInstance)this.attributesByName.get(p_111152_1_);
		}

///    
///     <summary> * Registers an attribute with this AttributeMap, returns a modifiable AttributeInstance associated with this map </summary>
///     
		public abstract IAttributeInstance registerAttribute(IAttribute p_111150_1_);

		public virtual ICollection AllAttributes
		{
			get
			{
				return this.attributesByName.Values;
			}
		}

		public virtual void addAttributeInstance(ModifiableAttributeInstance p_111149_1_)
		{
		}

		public virtual void removeAttributeModifiers(Multimap p_111148_1_)
		{
			IEnumerator var2 = p_111148_1_.entries().GetEnumerator();

			while (var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;
				IAttributeInstance var4 = this.getAttributeInstanceByName((string)var3.Key);

				if (var4 != null)
				{
					var4.removeModifier((AttributeModifier)var3.Value);
				}
			}
		}

		public virtual void applyAttributeModifiers(Multimap p_111147_1_)
		{
			IEnumerator var2 = p_111147_1_.entries().GetEnumerator();

			while (var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;
				IAttributeInstance var4 = this.getAttributeInstanceByName((string)var3.Key);

				if (var4 != null)
				{
					var4.removeModifier((AttributeModifier)var3.Value);
					var4.applyModifier((AttributeModifier)var3.Value);
				}
			}
		}
	}

}