using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;

	public class BlockStoneBrick : Block
	{
		public static readonly string[] field_150142_a = new string[] {"default", "mossy", "cracked", "chiseled"};
		public static readonly string[] field_150141_b = new string[] {null, "mossy", "cracked", "carved"};
		private IIcon[] field_150143_M;
		private const string __OBFID = "CL_00000318";

		public BlockStoneBrick() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			if (p_149691_2_ < 0 || p_149691_2_ >= field_150141_b.Length)
			{
				p_149691_2_ = 0;
			}

			return this.field_150143_M[p_149691_2_];
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
			for (int var4 = 0; var4 < 4; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_150143_M = new IIcon[field_150141_b.Length];

			for (int var2 = 0; var2 < this.field_150143_M.Length; ++var2)
			{
				string var3 = this.TextureName;

				if (field_150141_b[var2] != null)
				{
					var3 = var3 + "_" + field_150141_b[var2];
				}

				this.field_150143_M[var2] = p_149651_1_.registerIcon(var3);
			}
		}
	}

}