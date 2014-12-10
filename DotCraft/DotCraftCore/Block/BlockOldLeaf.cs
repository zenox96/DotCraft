using System.Collections;

namespace DotCraftCore.Block
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using IIcon = DotCraftCore.Util.IIcon;
	using ColorizerFoliage = DotCraftCore.World.ColorizerFoliage;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockOldLeaf : BlockLeaves
	{
		public static readonly string[][] field_150130_N = new string[][] {{"leaves_oak", "leaves_spruce", "leaves_birch", "leaves_jungle"}, {"leaves_oak_opaque", "leaves_spruce_opaque", "leaves_birch_opaque", "leaves_jungle_opaque"}};
		public static readonly string[] field_150131_O = new string[] {"oak", "spruce", "birch", "jungle"};
		private const string __OBFID = "CL_00000280";

///    
///     <summary> * Returns the color this block should be rendered. Used by leaves. </summary>
///     
		public override int getRenderColor(int p_149741_1_)
		{
			return (p_149741_1_ & 3) == 1 ? ColorizerFoliage.FoliageColorPine : ((p_149741_1_ & 3) == 2 ? ColorizerFoliage.FoliageColorBirch : base.getRenderColor(p_149741_1_));
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public override int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			int var5 = p_149720_1_.getBlockMetadata(p_149720_2_, p_149720_3_, p_149720_4_);
			return (var5 & 3) == 1 ? ColorizerFoliage.FoliageColorPine : ((var5 & 3) == 2 ? ColorizerFoliage.FoliageColorBirch : base.colorMultiplier(p_149720_1_, p_149720_2_, p_149720_3_, p_149720_4_));
		}

		protected internal override void func_150124_c(World p_150124_1_, int p_150124_2_, int p_150124_3_, int p_150124_4_, int p_150124_5_, int p_150124_6_)
		{
			if ((p_150124_5_ & 3) == 0 && p_150124_1_.rand.Next(p_150124_6_) == 0)
			{
				this.dropBlockAsItem_do(p_150124_1_, p_150124_2_, p_150124_3_, p_150124_4_, new ItemStack(Items.apple, 1, 0));
			}
		}

		protected internal override int func_150123_b(int p_150123_1_)
		{
			int var2 = base.func_150123_b(p_150123_1_);

			if ((p_150123_1_ & 3) == 3)
			{
				var2 = 40;
			}

			return var2;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public override IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return (p_149691_2_ & 3) == 1 ? this.field_150129_M[this.field_150127_b][1] : ((p_149691_2_ & 3) == 3 ? this.field_150129_M[this.field_150127_b][3] : ((p_149691_2_ & 3) == 2 ? this.field_150129_M[this.field_150127_b][2] : this.field_150129_M[this.field_150127_b][0]));
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 2));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 3));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			for (int var2 = 0; var2 < field_150130_N.Length; ++var2)
			{
				this.field_150129_M[var2] = new IIcon[field_150130_N[var2].Length];

				for (int var3 = 0; var3 < field_150130_N[var2].Length; ++var3)
				{
					this.field_150129_M[var2][var3] = p_149651_1_.registerIcon(field_150130_N[var2][var3]);
				}
			}
		}

		public override string[] func_150125_e()
		{
			return field_150131_O;
		}
	}

}