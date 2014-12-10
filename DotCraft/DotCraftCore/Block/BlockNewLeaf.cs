using System.Collections;

namespace DotCraftCore.Block
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using IIcon = DotCraftCore.Util.IIcon;
	using World = DotCraftCore.World.World;

	public class BlockNewLeaf : BlockLeaves
	{
		public static readonly string[][] field_150132_N = new string[][] {{"leaves_acacia", "leaves_big_oak"}, {"leaves_acacia_opaque", "leaves_big_oak_opaque"}};
		public static readonly string[] field_150133_O = new string[] {"acacia", "big_oak"};
		private const string __OBFID = "CL_00000276";

		protected internal override void func_150124_c(World p_150124_1_, int p_150124_2_, int p_150124_3_, int p_150124_4_, int p_150124_5_, int p_150124_6_)
		{
			if ((p_150124_5_ & 3) == 1 && p_150124_1_.rand.Next(p_150124_6_) == 0)
			{
				this.dropBlockAsItem_do(p_150124_1_, p_150124_2_, p_150124_3_, p_150124_4_, new ItemStack(Items.apple, 1, 0));
			}
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public override int damageDropped(int p_149692_1_)
		{
			return base.damageDropped(p_149692_1_) + 4;
		}

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public virtual int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			return p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_, p_149643_4_) & 3;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public override IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return (p_149691_2_ & 3) == 1 ? this.field_150129_M[this.field_150127_b][1] : this.field_150129_M[this.field_150127_b][0];
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			for (int var2 = 0; var2 < field_150132_N.Length; ++var2)
			{
				this.field_150129_M[var2] = new IIcon[field_150132_N[var2].Length];

				for (int var3 = 0; var3 < field_150132_N[var2].Length; ++var3)
				{
					this.field_150129_M[var2][var3] = p_149651_1_.registerIcon(field_150132_N[var2][var3]);
				}
			}
		}

		public override string[] func_150125_e()
		{
			return field_150133_O;
		}
	}

}