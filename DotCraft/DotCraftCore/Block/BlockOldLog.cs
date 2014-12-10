using System.Collections;

namespace DotCraftCore.Block
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;

	public class BlockOldLog : BlockLog
	{
		public static readonly string[] field_150168_M = new string[] {"oak", "spruce", "birch", "jungle"};
		private const string __OBFID = "CL_00000281";

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 2));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 3));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_150167_a = new IIcon[field_150168_M.Length];
			this.field_150166_b = new IIcon[field_150168_M.Length];

			for (int var2 = 0; var2 < this.field_150167_a.Length; ++var2)
			{
				this.field_150167_a[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + field_150168_M[var2]);
				this.field_150166_b[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + field_150168_M[var2] + "_top");
			}
		}
	}

}