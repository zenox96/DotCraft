namespace DotCraftCore.nItem
{

	using Block = DotCraftCore.nBlock.Block;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.nWorld.World;

	public class ItemShears : Item
	{
		

		public ItemShears()
		{
			this.MaxStackSize = 1;
			this.MaxDamage = 238;
			this.CreativeTab = CreativeTabs.tabTools;
		}

		public virtual bool onBlockDestroyed(ItemStack p_150894_1_, World p_150894_2_, Block p_150894_3_, int p_150894_4_, int p_150894_5_, int p_150894_6_, EntityLivingBase p_150894_7_)
		{
			if (p_150894_3_.Material != Material.leaves && p_150894_3_ != Blocks.web && p_150894_3_ != Blocks.tallgrass && p_150894_3_ != Blocks.vine && p_150894_3_ != Blocks.tripwire)
			{
				return base.onBlockDestroyed(p_150894_1_, p_150894_2_, p_150894_3_, p_150894_4_, p_150894_5_, p_150894_6_, p_150894_7_);
			}
			else
			{
				p_150894_1_.damageItem(1, p_150894_7_);
				return true;
			}
		}

		public virtual bool func_150897_b(Block p_150897_1_)
		{
			return p_150897_1_ == Blocks.web || p_150897_1_ == Blocks.redstone_wire || p_150897_1_ == Blocks.tripwire;
		}

		public virtual float func_150893_a(ItemStack p_150893_1_, Block p_150893_2_)
		{
			return p_150893_2_ != Blocks.web && p_150893_2_.Material != Material.leaves ? (p_150893_2_ == Blocks.wool ? 5.0F : base.func_150893_a(p_150893_1_, p_150893_2_)) : 15.0F;
		}
	}

}