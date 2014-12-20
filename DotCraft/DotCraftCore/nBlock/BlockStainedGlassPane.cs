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
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public static int func_150103_c(int p_150103_0_)
		{
			return p_150103_0_ & 15;
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 0; var4 < field_150106_a.Length; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

///    
///     <summary> * Returns which pass should this block be rendered on. 0 for solids and 1 for alpha </summary>
///     
		public virtual int RenderBlockPass
		{
			get
			{
				return 1;
			}
		}
	}
}