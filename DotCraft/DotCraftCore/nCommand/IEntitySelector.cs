namespace DotCraftCore.nCommand
{

	using Entity = DotCraftCore.nEntity.Entity;
	using EntityLiving = DotCraftCore.nEntity.EntityLiving;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using IInventory = DotCraftCore.nInventory.IInventory;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public interface IEntitySelector
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		IEntitySelector selectAnything = new IEntitySelector()
//	{
//		
//		public boolean isEntityApplicable(Entity p_82704_1_)
//		{
//			return p_82704_1_.isEntityAlive();
//		}
//	};
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		IEntitySelector field_152785_b = new IEntitySelector()
//	{
//		
//		public boolean isEntityApplicable(Entity p_82704_1_)
//		{
//			return p_82704_1_.isEntityAlive() && p_82704_1_.riddenByEntity == null && p_82704_1_.ridingEntity == null;
//		}
//	};
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		IEntitySelector selectInventories = new IEntitySelector()
//	{
//		
//		public boolean isEntityApplicable(Entity p_82704_1_)
//		{
//			return p_82704_1_ instanceof IInventory && p_82704_1_.isEntityAlive();
//		}
//	};

///    
///     <summary> * Return whether the specified entity is applicable to this filter. </summary>
///     
		bool isEntityApplicable(Entity p_82704_1_);

//JAVA TO VB & C# CONVERTER TODO TASK: Interfaces cannot contain types in .NET:
//		public static class ArmoredMob implements IEntitySelector
//	{
//		private final ItemStack field_96567_c;
//		
//
//		public ArmoredMob(ItemStack p_i1584_1_)
//		{
//			this.field_96567_c = p_i1584_1_;
//		}
//
//		public boolean isEntityApplicable(Entity p_82704_1_)
//		{
//			if (!p_82704_1_.isEntityAlive())
//			{
//				return false;
//			}
//			else if (!(p_82704_1_ instanceof EntityLivingBase))
//			{
//				return false;
//			}
//			else
//			{
//				EntityLivingBase var2 = (EntityLivingBase)p_82704_1_;
//				return var2.getEquipmentInSlot(EntityLiving.getArmorPosition(this.field_96567_c)) != null ? false : (var2 instanceof EntityLiving ? ((EntityLiving)var2).canPickUpLoot() : var2 instanceof EntityPlayer);
//			}
//		}
//	}
	}

}