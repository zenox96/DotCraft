using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockStainedGlassPane : BlockPane
	{
		public BlockStainedGlassPane() : base("glass", "glass_pane_top", Material.glass, false)
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
        public override int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}
	}
}