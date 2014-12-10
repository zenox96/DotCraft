using System;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using IIcon = DotCraftCore.util.IIcon;

	public class BlockMelon : Block
	{
		private IIcon field_150201_a;
		private const string __OBFID = "CL_00000267";

		protected internal BlockMelon() : base(Material.field_151572_C)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ != 1 && p_149691_1_ != 0 ? this.blockIcon : this.field_150201_a;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.melon;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 3 + p_149745_1_.Next(5);
		}

///    
///     <summary> * Returns the usual quantity dropped by the block plus a bonus of 1 to 'i' (inclusive). </summary>
///     
		public virtual int quantityDroppedWithBonus(int p_149679_1_, Random p_149679_2_)
		{
			int var3 = this.quantityDropped(p_149679_2_) + p_149679_2_.Next(1 + p_149679_1_);

			if (var3 > 9)
			{
				var3 = 9;
			}

			return var3;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
			this.field_150201_a = p_149651_1_.registerIcon(this.TextureName + "_top");
		}
	}

}