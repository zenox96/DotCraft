using System.Collections;

namespace DotCraftCore.Entity
{

	using AttributeModifier = DotCraftCore.Entity.AI.Attributes.AttributeModifier;
	using BaseAttributeMap = DotCraftCore.Entity.AI.Attributes.BaseAttributeMap;
	using IAttribute = DotCraftCore.Entity.AI.Attributes.IAttribute;
	using IAttributeInstance = DotCraftCore.Entity.AI.Attributes.IAttributeInstance;
	using RangedAttribute = DotCraftCore.Entity.AI.Attributes.RangedAttribute;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class SharedMonsterAttributes
	{
		private static readonly Logger logger = LogManager.Logger;
		public static readonly IAttribute maxHealth = (new RangedAttribute("generic.maxHealth", 20.0D, 0.0D, double.MAX_VALUE)).setDescription("Max Health").setShouldWatch(true);
		public static readonly IAttribute followRange = (new RangedAttribute("generic.followRange", 32.0D, 0.0D, 2048.0D)).Description = "Follow Range";
		public static readonly IAttribute knockbackResistance = (new RangedAttribute("generic.knockbackResistance", 0.0D, 0.0D, 1.0D)).Description = "Knockback Resistance";
		public static readonly IAttribute movementSpeed = (new RangedAttribute("generic.movementSpeed", 0.699999988079071D, 0.0D, double.MAX_VALUE)).setDescription("Movement Speed").setShouldWatch(true);
		public static readonly IAttribute attackDamage = new RangedAttribute("generic.attackDamage", 2.0D, 0.0D, double.MAX_VALUE);
		private const string __OBFID = "CL_00001695";

///    
///     <summary> * Creates an NBTTagList from a BaseAttributeMap, including all its AttributeInstances </summary>
///     
		public static NBTTagList writeBaseAttributeMapToNBT(BaseAttributeMap p_111257_0_)
		{
			NBTTagList var1 = new NBTTagList();
			IEnumerator var2 = p_111257_0_.AllAttributes.GetEnumerator();

			while (var2.MoveNext())
			{
				IAttributeInstance var3 = (IAttributeInstance)var2.Current;
				var1.appendTag(writeAttributeInstanceToNBT(var3));
			}

			return var1;
		}

///    
///     <summary> * Creates an NBTTagCompound from an AttributeInstance, including its AttributeModifiers </summary>
///     
		private static NBTTagCompound writeAttributeInstanceToNBT(IAttributeInstance p_111261_0_)
		{
			NBTTagCompound var1 = new NBTTagCompound();
			IAttribute var2 = p_111261_0_.Attribute;
			var1.setString("Name", var2.AttributeUnlocalizedName);
			var1.setDouble("Base", p_111261_0_.BaseValue);
			ICollection var3 = p_111261_0_.func_111122_c();

			if (var3 != null && !var3.Empty)
			{
				NBTTagList var4 = new NBTTagList();
				IEnumerator var5 = var3.GetEnumerator();

				while (var5.MoveNext())
				{
					AttributeModifier var6 = (AttributeModifier)var5.Current;

					if (var6.Saved)
					{
						var4.appendTag(writeAttributeModifierToNBT(var6));
					}
				}

				var1.setTag("Modifiers", var4);
			}

			return var1;
		}

///    
///     <summary> * Creates an NBTTagCompound from an AttributeModifier </summary>
///     
		private static NBTTagCompound writeAttributeModifierToNBT(AttributeModifier p_111262_0_)
		{
			NBTTagCompound var1 = new NBTTagCompound();
			var1.setString("Name", p_111262_0_.Name);
			var1.setDouble("Amount", p_111262_0_.Amount);
			var1.setInteger("Operation", p_111262_0_.Operation);
			var1.setLong("UUIDMost", p_111262_0_.ID.MostSignificantBits);
			var1.setLong("UUIDLeast", p_111262_0_.ID.LeastSignificantBits);
			return var1;
		}

		public static void func_151475_a(BaseAttributeMap p_151475_0_, NBTTagList p_151475_1_)
		{
			for (int var2 = 0; var2 < p_151475_1_.tagCount(); ++var2)
			{
				NBTTagCompound var3 = p_151475_1_.getCompoundTagAt(var2);
				IAttributeInstance var4 = p_151475_0_.getAttributeInstanceByName(var3.getString("Name"));

				if (var4 != null)
				{
					applyModifiersToAttributeInstance(var4, var3);
				}
				else
				{
					logger.warn("Ignoring unknown attribute \'" + var3.getString("Name") + "\'");
				}
			}
		}

		private static void applyModifiersToAttributeInstance(IAttributeInstance p_111258_0_, NBTTagCompound p_111258_1_)
		{
			p_111258_0_.BaseValue = p_111258_1_.getDouble("Base");

			if (p_111258_1_.func_150297_b("Modifiers", 9))
			{
				NBTTagList var2 = p_111258_1_.getTagList("Modifiers", 10);

				for (int var3 = 0; var3 < var2.tagCount(); ++var3)
				{
					AttributeModifier var4 = readAttributeModifierFromNBT(var2.getCompoundTagAt(var3));
					AttributeModifier var5 = p_111258_0_.getModifier(var4.ID);

					if (var5 != null)
					{
						p_111258_0_.removeModifier(var5);
					}

					p_111258_0_.applyModifier(var4);
				}
			}
		}

///    
///     <summary> * Creates an AttributeModifier from an NBTTagCompound </summary>
///     
		public static AttributeModifier readAttributeModifierFromNBT(NBTTagCompound p_111259_0_)
		{
			UUID var1 = new UUID(p_111259_0_.getLong("UUIDMost"), p_111259_0_.getLong("UUIDLeast"));
			return new AttributeModifier(var1, p_111259_0_.getString("Name"), p_111259_0_.getDouble("Amount"), p_111259_0_.getInteger("Operation"));
		}
	}

}