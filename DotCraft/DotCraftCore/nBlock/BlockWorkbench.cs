using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInventory;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;

namespace DotCraftCore.nBlock
{
	public class BlockWorkbench : Block
	{
		protected internal BlockWorkbench() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
		}
///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public override bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (p_149727_1_.isClient)
			{
				return true;
			}
			else
			{
				p_149727_5_.displayGUIWorkbench(p_149727_2_, p_149727_3_, p_149727_4_);
				return true;
			}
		}
	}

}