using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using IIcon = DotCraftCore.Util.IIcon;

	public class BlockWood : Block
	{
		public static readonly string[] field_150096_a = new string[] {"oak", "spruce", "birch", "jungle", "acacia", "big_oak"};
		private IIcon[] field_150095_b;
		private const string __OBFID = "CL_00000335";

		public BlockWood() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			if (p_149691_2_ < 0 || p_149691_2_ >= this.field_150095_b.Length)
			{
				p_149691_2_ = 0;
			}

			return this.field_150095_b[p_149691_2_];
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
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 2));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 3));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 4));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 5));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_150095_b = new IIcon[field_150096_a.Length];

			for (int var2 = 0; var2 < this.field_150095_b.Length; ++var2)
			{
				this.field_150095_b[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + field_150096_a[var2]);
			}
		}
	}

}