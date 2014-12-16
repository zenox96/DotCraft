using System;
using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using StatList = DotCraftCore.Stats.StatList;
	using IIcon = DotCraftCore.Util.IIcon;
	using ColorizerGrass = DotCraftCore.World.ColorizerGrass;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockTallGrass : BlockBush, IGrowable
	{
		private static readonly string[] field_149871_a = new string[] {"deadbush", "tallgrass", "fern"};
		private IIcon[] field_149870_b;
		

		protected internal BlockTallGrass() : base(Material.vine)
		{
			float var1 = 0.4F;
			this.setBlockBounds(0.5F - var1, 0.0F, 0.5F - var1, 0.5F + var1, 0.8F, 0.5F + var1);
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			if (p_149691_2_ >= this.field_149870_b.Length)
			{
				p_149691_2_ = 0;
			}

			return this.field_149870_b[p_149691_2_];
		}

		public virtual int BlockColor
		{
			get
			{
				double var1 = 0.5D;
				double var3 = 1.0D;
				return ColorizerGrass.getGrassColor(var1, var3);
			}
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public override bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			return this.func_149854_a(p_149718_1_.getBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_));
		}

///    
///     <summary> * Returns the color this block should be rendered. Used by leaves. </summary>
///     
		public virtual int getRenderColor(int p_149741_1_)
		{
			return p_149741_1_ == 0 ? 16777215 : ColorizerGrass.getGrassColor(0.5D, 1.0D);
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public virtual int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			int var5 = p_149720_1_.getBlockMetadata(p_149720_2_, p_149720_3_, p_149720_4_);
			return var5 == 0 ? 16777215 : p_149720_1_.getBiomeGenForCoords(p_149720_2_, p_149720_4_).getBiomeGrassColor(p_149720_2_, p_149720_3_, p_149720_4_);
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return p_149650_2_.Next(8) == 0 ? Items.wheat_seeds : null;
		}

///    
///     <summary> * Returns the usual quantity dropped by the block plus a bonus of 1 to 'i' (inclusive). </summary>
///     
		public virtual int quantityDroppedWithBonus(int p_149679_1_, Random p_149679_2_)
		{
			return 1 + p_149679_2_.Next(p_149679_1_ * 2 + 1);
		}

		public virtual void harvestBlock(World p_149636_1_, EntityPlayer p_149636_2_, int p_149636_3_, int p_149636_4_, int p_149636_5_, int p_149636_6_)
		{
			if (!p_149636_1_.isClient && p_149636_2_.CurrentEquippedItem != null && p_149636_2_.CurrentEquippedItem.Item == Items.shears)
			{
				p_149636_2_.addStat(StatList.mineBlockStatArray[Block.getIdFromBlock(this)], 1);
				this.dropBlockAsItem_do(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, new ItemStack(Blocks.tallgrass, 1, p_149636_6_));
			}
			else
			{
				base.harvestBlock(p_149636_1_, p_149636_2_, p_149636_3_, p_149636_4_, p_149636_5_, p_149636_6_);
			}
		}

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public virtual int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			return p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_, p_149643_4_);
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 1; var4 < 3; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149870_b = new IIcon[field_149871_a.Length];

			for (int var2 = 0; var2 < this.field_149870_b.Length; ++var2)
			{
				this.field_149870_b[var2] = p_149651_1_.registerIcon(field_149871_a[var2]);
			}
		}

		public virtual bool func_149851_a(World p_149851_1_, int p_149851_2_, int p_149851_3_, int p_149851_4_, bool p_149851_5_)
		{
			int var6 = p_149851_1_.getBlockMetadata(p_149851_2_, p_149851_3_, p_149851_4_);
			return var6 != 0;
		}

		public virtual bool func_149852_a(World p_149852_1_, Random p_149852_2_, int p_149852_3_, int p_149852_4_, int p_149852_5_)
		{
			return true;
		}

		public virtual void func_149853_b(World p_149853_1_, Random p_149853_2_, int p_149853_3_, int p_149853_4_, int p_149853_5_)
		{
			int var6 = p_149853_1_.getBlockMetadata(p_149853_3_, p_149853_4_, p_149853_5_);
			sbyte var7 = 2;

			if (var6 == 2)
			{
				var7 = 3;
			}

			if (Blocks.double_plant.canPlaceBlockAt(p_149853_1_, p_149853_3_, p_149853_4_, p_149853_5_))
			{
				Blocks.double_plant.func_149889_c(p_149853_1_, p_149853_3_, p_149853_4_, p_149853_5_, var7, 2);
			}
		}
	}

}