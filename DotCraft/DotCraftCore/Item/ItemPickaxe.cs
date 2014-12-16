namespace DotCraftCore.Item
{

	using Sets = com.google.common.collect.Sets;
	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Blocks = DotCraftCore.init.Blocks;

	public class ItemPickaxe : ItemTool
	{
		private static readonly Set field_150915_c = Sets.newHashSet(new Block[] {Blocks.cobblestone, Blocks.double_stone_slab, Blocks.stone_slab, Blocks.stone, Blocks.sandstone, Blocks.mossy_cobblestone, Blocks.iron_ore, Blocks.iron_block, Blocks.coal_ore, Blocks.gold_block, Blocks.gold_ore, Blocks.diamond_ore, Blocks.diamond_block, Blocks.ice, Blocks.netherrack, Blocks.lapis_ore, Blocks.lapis_block, Blocks.redstone_ore, Blocks.lit_redstone_ore, Blocks.rail, Blocks.detector_rail, Blocks.golden_rail, Blocks.activator_rail});
		

		protected internal ItemPickaxe(Item.ToolMaterial p_i45347_1_) : base(2.0F, p_i45347_1_, field_150915_c)
		{
		}

		public virtual bool func_150897_b(Block p_150897_1_)
		{
			return p_150897_1_ == Blocks.obsidian ? this.toolMaterial.HarvestLevel == 3 : (p_150897_1_ != Blocks.diamond_block && p_150897_1_ != Blocks.diamond_ore ? (p_150897_1_ != Blocks.emerald_ore && p_150897_1_ != Blocks.emerald_block ? (p_150897_1_ != Blocks.gold_block && p_150897_1_ != Blocks.gold_ore ? (p_150897_1_ != Blocks.iron_block && p_150897_1_ != Blocks.iron_ore ? (p_150897_1_ != Blocks.lapis_block && p_150897_1_ != Blocks.lapis_ore ? (p_150897_1_ != Blocks.redstone_ore && p_150897_1_ != Blocks.lit_redstone_ore ? (p_150897_1_.Material == Material.rock ? true : (p_150897_1_.Material == Material.iron ? true : p_150897_1_.Material == Material.anvil)) : this.toolMaterial.HarvestLevel >= 2) : this.toolMaterial.HarvestLevel >= 1) : this.toolMaterial.HarvestLevel >= 1) : this.toolMaterial.HarvestLevel >= 2) : this.toolMaterial.HarvestLevel >= 2) : this.toolMaterial.HarvestLevel >= 2);
		}

		public override float func_150893_a(ItemStack p_150893_1_, Block p_150893_2_)
		{
			return p_150893_2_.Material != Material.iron && p_150893_2_.Material != Material.anvil && p_150893_2_.Material != Material.rock ? base.func_150893_a(p_150893_1_, p_150893_2_) : this.efficiencyOnProperMaterial;
		}
	}

}