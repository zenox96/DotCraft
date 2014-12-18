namespace DotCraftCore.nItem
{

	using Sets = com.google.common.collect.Sets;
	using Block = DotCraftCore.nBlock.Block;
	using Blocks = DotCraftCore.init.Blocks;

	public class ItemSpade : ItemTool
	{
		private static readonly Set field_150916_c = Sets.newHashSet(new Block[] {Blocks.grass, Blocks.dirt, Blocks.sand, Blocks.gravel, Blocks.snow_layer, Blocks.snow, Blocks.clay, Blocks.farmland, Blocks.soul_sand, Blocks.mycelium});
		

		public ItemSpade(Item.ToolMaterial p_i45353_1_) : base(1.0F, p_i45353_1_, field_150916_c)
		{
		}

		public virtual bool func_150897_b(Block p_150897_1_)
		{
			return p_150897_1_ == Blocks.snow_layer ? true : p_150897_1_ == Blocks.snow;
		}
	}

}