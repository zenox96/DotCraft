using System;
using System.Collections;

namespace DotCraftCore.nEntity.nAI.nAttributes
{

	using Maps = com.google.common.collect.Maps;

	public class ModifiableAttributeInstance : IAttributeInstance
	{
	/// <summary> The BaseAttributeMap this attributeInstance can be found in  </summary>
		private readonly BaseAttributeMap attributeMap;

	/// <summary> The Attribute this is an instance of  </summary>
		private readonly IAttribute genericAttribute;
		private readonly IDictionary mapByOperation = Maps.newHashMap();
		private readonly IDictionary mapByName = Maps.newHashMap();
		private readonly IDictionary mapByUUID = Maps.newHashMap();
		private double baseValue;
		private bool needsUpdate = true;
		private double cachedValue;
		

		public ModifiableAttributeInstance(BaseAttributeMap p_i1608_1_, IAttribute p_i1608_2_)
		{
			this.attributeMap = p_i1608_1_;
			this.genericAttribute = p_i1608_2_;
			this.baseValue = p_i1608_2_.DefaultValue;

			for (int var3 = 0; var3 < 3; ++var3)
			{
				this.mapByOperation.Add(Convert.ToInt32(var3), new HashSet());
			}
		}

///    
///     <summary> * Get the Attribute this is an instance of </summary>
///     
		public virtual IAttribute Attribute
		{
			get
			{
				return this.genericAttribute;
			}
		}

		public virtual double BaseValue
		{
			get
			{
				return this.baseValue;
			}
			set
			{
				if (value != this.BaseValue)
				{
					this.baseValue = value;
					this.flagForUpdate();
				}
			}
		}


		public virtual ICollection getModifiersByOperation(int p_111130_1_)
		{
			return (ICollection)this.mapByOperation.get(Convert.ToInt32(p_111130_1_));
		}

		public virtual ICollection func_111122_c()
		{
			HashSet var1 = new HashSet();

			for (int var2 = 0; var2 < 3; ++var2)
			{
				var1.addAll(this.getModifiersByOperation(var2));
			}

			return var1;
		}

///    
///     <summary> * Returns attribute modifier, if any, by the given UUID </summary>
///     
		public virtual AttributeModifier getModifier(UUID p_111127_1_)
		{
			return (AttributeModifier)this.mapByUUID.get(p_111127_1_);
		}

		public virtual void applyModifier(AttributeModifier p_111121_1_)
		{
			if (this.getModifier(p_111121_1_.ID) != null)
			{
				throw new System.ArgumentException("Modifier is already applied on this attribute!");
			}
			else
			{
				object var2 = (Set)this.mapByName.get(p_111121_1_.Name);

				if (var2 == null)
				{
					var2 = new HashSet();
					this.mapByName.Add(p_111121_1_.Name, var2);
				}

				((Set)this.mapByOperation.get(Convert.ToInt32(p_111121_1_.Operation))).add(p_111121_1_);
				((Set)var2).add(p_111121_1_);
				this.mapByUUID.Add(p_111121_1_.ID, p_111121_1_);
				this.flagForUpdate();
			}
		}

		private void flagForUpdate()
		{
			this.needsUpdate = true;
			this.attributeMap.addAttributeInstance(this);
		}

		public virtual void removeModifier(AttributeModifier p_111124_1_)
		{
			for (int var2 = 0; var2 < 3; ++var2)
			{
				Set var3 = (Set)this.mapByOperation.get(Convert.ToInt32(var2));
				var3.remove(p_111124_1_);
			}

			Set var4 = (Set)this.mapByName.get(p_111124_1_.Name);

			if (var4 != null)
			{
				var4.remove(p_111124_1_);

				if (var4.Empty)
				{
					this.mapByName.Remove(p_111124_1_.Name);
				}
			}

			this.mapByUUID.Remove(p_111124_1_.ID);
			this.flagForUpdate();
		}

		public virtual void removeAllModifiers()
		{
			ICollection var1 = this.func_111122_c();

			if (var1 != null)
			{
				ArrayList var4 = new ArrayList(var1);
				IEnumerator var2 = var4.GetEnumerator();

				while (var2.MoveNext())
				{
					AttributeModifier var3 = (AttributeModifier)var2.Current;
					this.removeModifier(var3);
				}
			}
		}

		public virtual double AttributeValue
		{
			get
			{
				if (this.needsUpdate)
				{
					this.cachedValue = this.computeValue();
					this.needsUpdate = false;
				}
	
				return this.cachedValue;
			}
		}

		private double computeValue()
		{
			double var1 = this.BaseValue;
			AttributeModifier var4;

			for (IEnumerator var3 = this.getModifiersByOperation(0).GetEnumerator(); var3.MoveNext(); var1 += var4.Amount)
			{
				var4 = (AttributeModifier)var3.Current;
			}

			double var7 = var1;
			IEnumerator var5;
			AttributeModifier var6;

			for (var5 = this.getModifiersByOperation(1).GetEnumerator(); var5.MoveNext(); var7 += var1 * var6.Amount)
			{
				var6 = (AttributeModifier)var5.Current;
			}

			for (var5 = this.getModifiersByOperation(2).GetEnumerator(); var5.MoveNext(); var7 *= 1.0D + var6.Amount)
			{
				var6 = (AttributeModifier)var5.Current;
			}

			return this.genericAttribute.clampValue(var7);
		}
	}

}