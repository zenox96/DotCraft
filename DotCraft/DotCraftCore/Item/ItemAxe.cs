namespace DotCraftCore.Item
{

	using Sets = com.google.common.collect.Sets;
	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Blocks = DotCraftCore.init.Blocks;

	public class ItemAxe : ItemTool
	{
		private static readonly Set field_150917_c = Sets.newHashSet(new Block[] {Blocks.planks, Blocks.bookshelf, Blocks.log, Blocks.log2, Blocks.chest, Blocks.pumpkin, Blocks.lit_pumpkin});
		private const string __OBFID = "CL_00001770";

		protected internal ItemAxe(Item.ToolMaterial p_i45327_1_) : base(3.0F, p_i45327_1_, field_150917_c)
		{
		}

		public override float func_150893_a(ItemStack p_150893_1_, Block p_150893_2_)
		{
			return p_150893_2_.Material != Material.wood && p_150893_2_.Material != Material.plants && p_150893_2_.Material != Material.vine ? base.func_150893_a(p_150893_1_, p_150893_2_) : this.efficiencyOnProperMaterial;
		}
	}

}