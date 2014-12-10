using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Blocks = DotCraftCore.Init.Blocks;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using IIcon = DotCraftCore.Util.IIcon;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockDirt : Block
	{
		public static readonly string[] field_150009_a = new string[] {"default", "default", "podzol"};
		private IIcon field_150008_b;
		private IIcon field_150010_M;
		private const string __OBFID = "CL_00000228";

		protected internal BlockDirt() : base(Material.ground)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			if (p_149691_2_ == 2)
			{
				if (p_149691_1_ == 1)
				{
					return this.field_150008_b;
				}

				if (p_149691_1_ != 0)
				{
					return this.field_150010_M;
				}
			}

			return this.blockIcon;
		}

		public virtual IIcon getIcon(IBlockAccess p_149673_1_, int p_149673_2_, int p_149673_3_, int p_149673_4_, int p_149673_5_)
		{
			int var6 = p_149673_1_.getBlockMetadata(p_149673_2_, p_149673_3_, p_149673_4_);

			if (var6 == 2)
			{
				if (p_149673_5_ == 1)
				{
					return this.field_150008_b;
				}

				if (p_149673_5_ != 0)
				{
					Material var7 = p_149673_1_.getBlock(p_149673_2_, p_149673_3_ + 1, p_149673_4_).Material;

					if (var7 == Material.field_151597_y || var7 == Material.craftedSnow)
					{
						return Blocks.grass.getIcon(p_149673_1_, p_149673_2_, p_149673_3_, p_149673_4_, p_149673_5_);
					}

					Block var8 = p_149673_1_.getBlock(p_149673_2_, p_149673_3_ + 1, p_149673_4_);

					if (var8 != Blocks.dirt && var8 != Blocks.grass)
					{
						return this.field_150010_M;
					}
				}
			}

			return this.blockIcon;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return 0;
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
		{
			if (p_149644_1_ == 1)
			{
				p_149644_1_ = 0;
			}

			return base.createStackedBlock(p_149644_1_);
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(this, 1, 0));
			p_149666_3_.Add(new ItemStack(this, 1, 2));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			base.registerBlockIcons(p_149651_1_);
			this.field_150008_b = p_149651_1_.registerIcon(this.TextureName + "_" + "podzol_top");
			this.field_150010_M = p_149651_1_.registerIcon(this.TextureName + "_" + "podzol_side");
		}

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public virtual int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			int var5 = p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_, p_149643_4_);

			if (var5 == 1)
			{
				var5 = 0;
			}

			return var5;
		}
	}

}