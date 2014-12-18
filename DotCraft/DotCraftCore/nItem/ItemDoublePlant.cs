namespace DotCraftCore.nItem
{

	using Block = DotCraftCore.nBlock.Block;
	using BlockDoublePlant = DotCraftCore.nBlock.BlockDoublePlant;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using ColorizerGrass = DotCraftCore.nWorld.ColorizerGrass;

	public class ItemDoublePlant : ItemMultiTexture
	{
		

		public ItemDoublePlant(Block p_i45335_1_, BlockDoublePlant p_i45335_2_, string[] p_i45335_3_) : base(p_i45335_1_, p_i45335_2_, p_i45335_3_)
		{
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public override IIcon getIconFromDamage(int p_77617_1_)
		{
			return BlockDoublePlant.func_149890_d(p_77617_1_) == 0 ? ((BlockDoublePlant)this.field_150941_b).field_149891_b[0] : ((BlockDoublePlant)this.field_150941_b).func_149888_a(true, p_77617_1_);
		}

		public virtual int getColorFromItemStack(ItemStack p_82790_1_, int p_82790_2_)
		{
			int var3 = BlockDoublePlant.func_149890_d(p_82790_1_.ItemDamage);
			return var3 != 2 && var3 != 3 ? base.getColorFromItemStack(p_82790_1_, p_82790_2_) : ColorizerGrass.getGrassColor(0.5D, 1.0D);
		}
	}

}