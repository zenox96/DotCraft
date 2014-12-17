namespace DotCraftCore.nEntity.nMonster
{

	using IEntitySelector = DotCraftCore.nCommand.IEntitySelector;
	using Entity = DotCraftCore.nEntity.Entity;
	using IAnimals = DotCraftCore.nEntity.nPassive.IAnimals;

	public interface IMob : IAnimals
	{
	/// <summary> Entity selector for IMob types.  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		IEntitySelector mobSelector = new IEntitySelector()
//	{
//		
//		public boolean isEntityApplicable(Entity p_82704_1_)
//		{
//			return p_82704_1_ instanceof IMob;
//		}
//	};
	}

}