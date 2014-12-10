using System;
using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Entity = DotCraftCore.Entity.Entity;
	using Blocks = DotCraftCore.Init.Blocks;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using IIcon = DotCraftCore.Util.IIcon;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockPane : Block
	{
		private readonly string field_150100_a;
		private readonly bool field_150099_b;
		private readonly string field_150101_M;
		private IIcon field_150102_N;
		private const string __OBFID = "CL_00000322";

		protected internal BlockPane(string p_i45432_1_, string p_i45432_2_, Material p_i45432_3_, bool p_i45432_4_) : base(p_i45432_3_)
		{
			this.field_150100_a = p_i45432_2_;
			this.field_150099_b = p_i45432_4_;
			this.field_150101_M = p_i45432_1_;
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return !this.field_150099_b ? null : base.getItemDropped(p_149650_1_, p_149650_2_, p_149650_3_);
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return this.blockMaterial == Material.glass ? 41 : 18;
			}
		}

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return p_149646_1_.getBlock(p_149646_2_, p_149646_3_, p_149646_4_) == this ? false : base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_);
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			bool var8 = this.func_150098_a(p_149743_1_.getBlock(p_149743_2_, p_149743_3_, p_149743_4_ - 1));
			bool var9 = this.func_150098_a(p_149743_1_.getBlock(p_149743_2_, p_149743_3_, p_149743_4_ + 1));
			bool var10 = this.func_150098_a(p_149743_1_.getBlock(p_149743_2_ - 1, p_149743_3_, p_149743_4_));
			bool var11 = this.func_150098_a(p_149743_1_.getBlock(p_149743_2_ + 1, p_149743_3_, p_149743_4_));

			if ((!var10 || !var11) && (var10 || var11 || var8 || var9))
			{
				if (var10 && !var11)
				{
					this.setBlockBounds(0.0F, 0.0F, 0.4375F, 0.5F, 1.0F, 0.5625F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
				}
				else if (!var10 && var11)
				{
					this.setBlockBounds(0.5F, 0.0F, 0.4375F, 1.0F, 1.0F, 0.5625F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
				}
			}
			else
			{
				this.setBlockBounds(0.0F, 0.0F, 0.4375F, 1.0F, 1.0F, 0.5625F);
				base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			}

			if ((!var8 || !var9) && (var10 || var11 || var8 || var9))
			{
				if (var8 && !var9)
				{
					this.setBlockBounds(0.4375F, 0.0F, 0.0F, 0.5625F, 1.0F, 0.5F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
				}
				else if (!var8 && var9)
				{
					this.setBlockBounds(0.4375F, 0.0F, 0.5F, 0.5625F, 1.0F, 1.0F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
				}
			}
			else
			{
				this.setBlockBounds(0.4375F, 0.0F, 0.0F, 0.5625F, 1.0F, 1.0F);
				base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			}
		}

///    
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public virtual void setBlockBoundsForItemRender()
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			float var5 = 0.4375F;
			float var6 = 0.5625F;
			float var7 = 0.4375F;
			float var8 = 0.5625F;
			bool var9 = this.func_150098_a(p_149719_1_.getBlock(p_149719_2_, p_149719_3_, p_149719_4_ - 1));
			bool var10 = this.func_150098_a(p_149719_1_.getBlock(p_149719_2_, p_149719_3_, p_149719_4_ + 1));
			bool var11 = this.func_150098_a(p_149719_1_.getBlock(p_149719_2_ - 1, p_149719_3_, p_149719_4_));
			bool var12 = this.func_150098_a(p_149719_1_.getBlock(p_149719_2_ + 1, p_149719_3_, p_149719_4_));

			if ((!var11 || !var12) && (var11 || var12 || var9 || var10))
			{
				if (var11 && !var12)
				{
					var5 = 0.0F;
				}
				else if (!var11 && var12)
				{
					var6 = 1.0F;
				}
			}
			else
			{
				var5 = 0.0F;
				var6 = 1.0F;
			}

			if ((!var9 || !var10) && (var11 || var12 || var9 || var10))
			{
				if (var9 && !var10)
				{
					var7 = 0.0F;
				}
				else if (!var9 && var10)
				{
					var8 = 1.0F;
				}
			}
			else
			{
				var7 = 0.0F;
				var8 = 1.0F;
			}

			this.setBlockBounds(var5, 0.0F, var7, var6, 1.0F, var8);
		}

		public virtual IIcon func_150097_e()
		{
			return this.field_150102_N;
		}

		public bool func_150098_a(Block p_150098_1_)
		{
			return p_150098_1_.func_149730_j() || p_150098_1_ == this || p_150098_1_ == Blocks.glass || p_150098_1_ == Blocks.stained_glass || p_150098_1_ == Blocks.stained_glass_pane || p_150098_1_ is BlockPane;
		}

		protected internal virtual bool canSilkHarvest()
		{
			return true;
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
		{
			return new ItemStack(Item.getItemFromBlock(this), 1, p_149644_1_);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.field_150101_M);
			this.field_150102_N = p_149651_1_.registerIcon(this.field_150100_a);
		}
	}

}