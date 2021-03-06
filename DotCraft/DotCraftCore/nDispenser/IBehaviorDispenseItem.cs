namespace DotCraftCore.nDispenser
{

	using ItemStack = DotCraftCore.nItem.ItemStack;

	public interface IBehaviorDispenseItem
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		IBehaviorDispenseItem itemDispenseBehaviorProvider = new IBehaviorDispenseItem()
//	{
//		
//		public ItemStack dispense(IBlockSource p_82482_1_, ItemStack p_82482_2_)
//		{
//			return p_82482_2_;
//		}
//	};

///    
///     <summary> * Dispenses the specified ItemStack from a dispenser. </summary>
///     
		ItemStack dispense(IBlockSource p_82482_1_, ItemStack p_82482_2_);
	}

}