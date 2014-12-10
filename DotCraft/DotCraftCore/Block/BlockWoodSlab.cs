using System;
using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Blocks = DotCraftCore.init.Blocks;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;

	public class BlockWoodSlab : BlockSlab
	{
		public static readonly string[] field_150005_b = new string[] {"oak", "spruce", "birch", "jungle", "acacia", "big_oak"};
		private const string __OBFID = "CL_00000337";

		public BlockWoodSlab(bool p_i45437_1_) : base(p_i45437_1_, Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return Blocks.planks.getIcon(p_149691_1_, p_149691_2_ & 7);
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.wooden_slab);
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
		{
			return new ItemStack(Item.getItemFromBlock(Blocks.wooden_slab), 2, p_149644_1_ & 7);
		}

		public override string func_150002_b(int p_150002_1_)
		{
			if (p_150002_1_ < 0 || p_150002_1_ >= field_150005_b.Length)
			{
				p_150002_1_ = 0;
			}

			return base.UnlocalizedName + "." + field_150005_b[p_150002_1_];
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			if (p_149666_1_ != Item.getItemFromBlock(Blocks.double_wooden_slab))
			{
				for (int var4 = 0; var4 < field_150005_b.Length; ++var4)
				{
					p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
				}
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
		}
	}

}