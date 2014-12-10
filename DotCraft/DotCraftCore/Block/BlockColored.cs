using System.Collections;

namespace DotCraftCore.Block
{

	using MapColor = DotCraftCore.Block.material.MapColor;
	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Item = DotCraftCore.item.Item;
	using ItemDye = DotCraftCore.item.ItemDye;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;

	public class BlockColored : Block
	{
		private IIcon[] field_150033_a;
		private const string __OBFID = "CL_00000217";

		public BlockColored(Material p_i45398_1_) : base(p_i45398_1_)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return this.field_150033_a[p_149691_2_ % this.field_150033_a.Length];
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public static int func_150032_b(int p_150032_0_)
		{
			return func_150031_c(p_150032_0_);
		}

		public static int func_150031_c(int p_150031_0_)
		{
			return ~p_150031_0_ & 15;
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 0; var4 < 16; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_150033_a = new IIcon[16];

			for (int var2 = 0; var2 < this.field_150033_a.Length; ++var2)
			{
				this.field_150033_a[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + ItemDye.field_150921_b[func_150031_c(var2)]);
			}
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.func_151644_a(p_149728_1_);
		}
	}

}