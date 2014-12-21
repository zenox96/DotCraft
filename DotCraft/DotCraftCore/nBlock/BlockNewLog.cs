using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockNewLog : BlockLog
	{
		public static readonly string[] field_150169_M = new string[] {"acacia", "big_oak"};

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
		}
	}
}