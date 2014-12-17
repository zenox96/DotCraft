using System;

namespace DotCraftCore.nBlock
{

	
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;

	public class BlockGlass : BlockBreakable
	{
		

		public BlockGlass(Material p_i45408_1_, bool p_i45408_2_) : base("glass", p_i45408_1_, p_i45408_2_)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

///    
///     <summary> * Returns which pass should this block be rendered on. 0 for solids and 1 for alpha </summary>
///     
		public virtual int RenderBlockPass
		{
			get
			{
				return 0;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

		protected internal virtual bool canSilkHarvest()
		{
			return true;
		}
	}

}