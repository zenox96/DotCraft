using System;
using System.Collections;

namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Item = DotCraftCore.nItem.Item;
	using ItemDye = DotCraftCore.nItem.ItemDye;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using IIcon = DotCraftCore.nUtil.IIcon;

	public class BlockStainedGlass : BlockBreakable
	{
		private static readonly IIcon[] field_149998_a = new IIcon[16];
		

		public BlockStainedGlass(Material p_i45427_1_) : base("glass", p_i45427_1_, false)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return field_149998_a[p_149691_2_ % field_149998_a.Length];
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public static int func_149997_b(int p_149997_0_)
		{
			return ~p_149997_0_ & 15;
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 0; var4 < field_149998_a.Length; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

///    
///     <summary> * Returns which pass should this block be rendered on. 0 for solids and 1 for alpha </summary>
///     
		public virtual int RenderBlockPass
		{
			get
			{
				return 1;
			}
		}

		public override void registerBlockIcons(IIconRegister p_149651_1_)
		{
			for (int var2 = 0; var2 < field_149998_a.Length; ++var2)
			{
				field_149998_a[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + ItemDye.field_150921_b[func_149997_b(var2)]);
			}
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

		protected internal virtual bool canSilkHarvest()
		{
			return true;
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}
	}

}