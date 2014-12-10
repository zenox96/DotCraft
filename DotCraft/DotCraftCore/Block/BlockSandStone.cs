using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;

	public class BlockSandStone : Block
	{
		public static readonly string[] field_150157_a = new string[] {"default", "chiseled", "smooth"};
		private static readonly string[] field_150156_b = new string[] {"normal", "carved", "smooth"};
		private IIcon[] field_150158_M;
		private IIcon field_150159_N;
		private IIcon field_150160_O;
		private const string __OBFID = "CL_00000304";

		public BlockSandStone() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			if (p_149691_1_ != 1 && (p_149691_1_ != 0 || p_149691_2_ != 1 && p_149691_2_ != 2))
			{
				if (p_149691_1_ == 0)
				{
					return this.field_150160_O;
				}
				else
				{
					if (p_149691_2_ < 0 || p_149691_2_ >= this.field_150158_M.Length)
					{
						p_149691_2_ = 0;
					}

					return this.field_150158_M[p_149691_2_];
				}
			}
			else
			{
				return this.field_150159_N;
			}
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
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_150158_M = new IIcon[field_150156_b.Length];

			for (int var2 = 0; var2 < this.field_150158_M.Length; ++var2)
			{
				this.field_150158_M[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + field_150156_b[var2]);
			}

			this.field_150159_N = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.field_150160_O = p_149651_1_.registerIcon(this.TextureName + "_bottom");
		}
	}

}