using System.Collections;

namespace DotCraftCore.Block
{

	using MapColor = DotCraftCore.Block.material.MapColor;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;

	public class BlockSand : BlockFalling
	{
		public static readonly string[] field_149838_a = new string[] {"default", "red"};
		private static IIcon field_149837_b;
		private static IIcon field_149839_N;
		private const string __OBFID = "CL_00000303";

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_2_ == 1 ? field_149839_N : field_149837_b;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			field_149837_b = p_149651_1_.registerIcon("sand");
			field_149839_N = p_149651_1_.registerIcon("red_sand");
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return p_149728_1_ == 1 ? MapColor.field_151664_l : MapColor.field_151658_d;
		}
	}

}