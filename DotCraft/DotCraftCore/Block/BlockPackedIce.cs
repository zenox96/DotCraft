using System;

namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;

	public class BlockPackedIce : Block
	{
		

		public BlockPackedIce() : base(Material.field_151598_x)
		{
			this.slipperiness = 0.98F;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}
	}

}