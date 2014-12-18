using System.Collections;

namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Item = DotCraftCore.nItem.Item;
	using ItemDye = DotCraftCore.nItem.ItemDye;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using IIcon = DotCraftCore.nUtil.IIcon;

	public class BlockStainedGlassPane : BlockPane
	{
		private static readonly IIcon[] field_150106_a = new IIcon[16];
		private static readonly IIcon[] field_150105_b = new IIcon[16];
		

		public BlockStainedGlassPane() : base("glass", "glass_pane_top", Material.glass, false)
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

		public virtual IIcon func_149735_b(int p_149735_1_, int p_149735_2_)
		{
			return field_150106_a[p_149735_2_ % field_150106_a.Length];
		}

		public virtual IIcon func_150104_b(int p_150104_1_)
		{
			return field_150105_b[~p_150104_1_ & 15];
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return this.func_149735_b(p_149691_1_, ~p_149691_2_ & 15);
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

		public override void registerBlockIcons(IIconRegister p_149651_1_)
		{
			base.registerBlockIcons(p_149651_1_);

			for (int var2 = 0; var2 < field_150106_a.Length; ++var2)
			{
				field_150106_a[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + ItemDye.field_150921_b[func_150103_c(var2)]);
				field_150105_b[var2] = p_149651_1_.registerIcon(this.TextureName + "_pane_top_" + ItemDye.field_150921_b[func_150103_c(var2)]);
			}
		}
	}

}