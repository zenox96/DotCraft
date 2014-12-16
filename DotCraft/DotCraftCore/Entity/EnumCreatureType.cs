using System;

namespace DotCraftCore.Entity
{

	using Material = DotCraftCore.block.material.Material;
	using IMob = DotCraftCore.Entity.Monster.IMob;
	using EntityAmbientCreature = DotCraftCore.Entity.Passive.EntityAmbientCreature;
	using EntityAnimal = DotCraftCore.Entity.Passive.EntityAnimal;
	using EntityWaterMob = DotCraftCore.Entity.Passive.EntityWaterMob;

	public enum EnumCreatureType
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		monster(IMob.class, 70, Material.air, false, false),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		creature(EntityAnimal.class, 10, Material.air, true, true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		ambient(EntityAmbientCreature.class, 15, Material.air, true, false),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		waterCreature(EntityWaterMob.class, 5, Material.water, true, false);

///    
///     <summary> * The root class of creatures associated with this EnumCreatureType (IMobs for aggressive creatures, EntityAnimals
///     * for friendly ones) </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final Class creatureClass;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final int maxNumberOfCreature;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final Material creatureMaterial;

	/// <summary> A flag indicating whether this creature type is peaceful.  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final boolean isPeacefulCreature;

	/// <summary> Whether this creature type is an animal.  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final boolean isAnimal;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		

//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		private EnumCreatureType(Class p_i1596_3_, int p_i1596_4_, Material p_i1596_5_, boolean p_i1596_6_, boolean p_i1596_7_)
//	{
//		this.creatureClass = p_i1596_3_;
//		this.maxNumberOfCreature = p_i1596_4_;
//		this.creatureMaterial = p_i1596_5_;
//		this.isPeacefulCreature = p_i1596_6_;
//		this.isAnimal = p_i1596_7_;
//	}




///    
///     <summary> * Gets whether or not this creature type is peaceful. </summary>
///     

///    
///     <summary> * Return whether this creature type is an animal. </summary>
///     
	}
	public static partial class EnumExtensionMethods
	{
			public Type getCreatureClass(this EnumCreatureType instanceJavaToDotNetTempPropertyGetCreatureClass)
		{
			return instance.creatureClass;
		}
			public int getMaxNumberOfCreature(this EnumCreatureType instanceJavaToDotNetTempPropertyGetMaxNumberOfCreature)
		{
			return instance.maxNumberOfCreature;
		}
			public Material getCreatureMaterial(this EnumCreatureType instanceJavaToDotNetTempPropertyGetCreatureMaterial)
		{
			return instance.creatureMaterial;
		}
			public bool getPeacefulCreature(this EnumCreatureType instanceJavaToDotNetTempPropertyGetPeacefulCreature)
		{
			return instance.isPeacefulCreature;
		}
			public bool getAnimal(this EnumCreatureType instanceJavaToDotNetTempPropertyGetAnimal)
		{
			return instance.isAnimal;
		}
	}

}