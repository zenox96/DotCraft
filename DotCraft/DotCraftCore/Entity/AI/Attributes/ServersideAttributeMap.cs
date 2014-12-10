using System.Collections;

namespace DotCraftCore.Entity.AI.Attributes
{

	using Sets = com.google.common.collect.Sets;
	using LowerStringMap = DotCraftCore.server.management.LowerStringMap;

	public class ServersideAttributeMap : BaseAttributeMap
	{
		private readonly Set attributeInstanceSet = Sets.newHashSet();
		protected internal readonly IDictionary descriptionToAttributeInstanceMap = new LowerStringMap();
		private const string __OBFID = "CL_00001569";

		public override ModifiableAttributeInstance getAttributeInstance(IAttribute p_111151_1_)
		{
			return (ModifiableAttributeInstance)base.getAttributeInstance(p_111151_1_);
		}

		public override ModifiableAttributeInstance getAttributeInstanceByName(string p_111152_1_)
		{
			IAttributeInstance var2 = base.getAttributeInstanceByName(p_111152_1_);

			if (var2 == null)
			{
				var2 = (IAttributeInstance)this.descriptionToAttributeInstanceMap.get(p_111152_1_);
			}

			return (ModifiableAttributeInstance)var2;
		}

///    
///     <summary> * Registers an attribute with this AttributeMap, returns a modifiable AttributeInstance associated with this map </summary>
///     
		public override IAttributeInstance registerAttribute(IAttribute p_111150_1_)
		{
			if (this.attributesByName.ContainsKey(p_111150_1_.AttributeUnlocalizedName))
			{
				throw new System.ArgumentException("Attribute is already registered!");
			}
			else
			{
				ModifiableAttributeInstance var2 = new ModifiableAttributeInstance(this, p_111150_1_);
				this.attributesByName.Add(p_111150_1_.AttributeUnlocalizedName, var2);

				if (p_111150_1_ is RangedAttribute && ((RangedAttribute)p_111150_1_).Description != null)
				{
					this.descriptionToAttributeInstanceMap.Add(((RangedAttribute)p_111150_1_).Description, var2);
				}

				this.attributes.Add(p_111150_1_, var2);
				return var2;
			}
		}

		public override void addAttributeInstance(ModifiableAttributeInstance p_111149_1_)
		{
			if (p_111149_1_.Attribute.ShouldWatch)
			{
				this.attributeInstanceSet.add(p_111149_1_);
			}
		}

		public virtual Set AttributeInstanceSet
		{
			get
			{
				return this.attributeInstanceSet;
			}
		}

		public virtual ICollection WatchedAttributes
		{
			get
			{
				HashSet var1 = Sets.newHashSet();
				IEnumerator var2 = this.AllAttributes.GetEnumerator();
	
				while (var2.MoveNext())
				{
					IAttributeInstance var3 = (IAttributeInstance)var2.Current;
	
					if (var3.Attribute.ShouldWatch)
					{
						var1.Add(var3);
					}
				}
	
				return var1;
			}
		}
	}

}