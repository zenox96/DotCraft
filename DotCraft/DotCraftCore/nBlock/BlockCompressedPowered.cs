using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nWorld;

namespace DotCraftCore.nBlock
{
	public class BlockCompressedPowered : BlockCompressed
	{
		public BlockCompressedPowered(MapColor p_i45416_1_) : base(p_i45416_1_)
		{
			this.CreativeTab = CreativeTabs.tabRedstone;
		}

///    
///     <summary> * Can this block provide power. Only wire currently seems to have this change based on its state. </summary>
///     
		public virtual bool canProvidePower()
		{
			return true;
		}

		public virtual int isProvidingWeakPower(IBlockAccess p_149709_1_, int p_149709_2_, int p_149709_3_, int p_149709_4_, int p_149709_5_)
		{
			return 15;
		}
	}

}