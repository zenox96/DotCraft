using System;

namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using StatList = DotCraftCore.nStats.StatList;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using EnumSkyBlock = DotCraftCore.nWorld.EnumSkyBlock;
	using IBlockAccess = DotCraftCore.nWorld.IBlockAccess;
	using World = DotCraftCore.nWorld.World;

	public class BlockSnow : Block
	{
		

		protected internal BlockSnow() : base(Material.field_151597_y)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.125F, 1.0F);
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabDecorations;
			this.func_150154_b(0);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon("snow");
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			int var5 = p_149668_1_.getBlockMetadata(p_149668_2_, p_149668_3_, p_149668_4_) & 7;
			float var6 = 0.125F;
			return AxisAlignedBB.getBoundingBox((double)p_149668_2_ + this.field_149759_B, (double)p_149668_3_ + this.field_149760_C, (double)p_149668_4_ + this.field_149754_D, (double)p_149668_2_ + this.field_149755_E, (double)((float)p_149668_3_ + (float)var5 * var6), (double)p_149668_4_ + this.field_149757_G);
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
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public virtual void setBlockBoundsForItemRender()
		{
			this.func_150154_b(0);
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			this.func_150154_b(p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_));
		}

		protected internal virtual void func_150154_b(int p_150154_1_)
		{
			int var2 = p_150154_1_ & 7;
			float var3 = (float)(2 * (1 + var2)) / 16.0F;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, var3, 1.0F);
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			Block var5 = p_149742_1_.getBlock(p_149742_2_, p_149742_3_ - 1, p_149742_4_);
			return var5 != Blocks.ice && var5 != Blocks.packed_ice ? (var5.Material == Material.leaves ? true : (var5 == this && (p_149742_1_.getBlockMetadata(p_149742_2_, p_149742_3_ - 1, p_149742_4_) & 7) == 7 ? true : var5.OpaqueCube && var5.blockMaterial.blocksMovement())) : false;
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			this.func_150155_m(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_);
		}

		private bool func_150155_m(World p_150155_1_, int p_150155_2_, int p_150155_3_, int p_150155_4_)
		{
			if (!this.canPlaceBlockAt(p_150155_1_, p_150155_2_, p_150155_3_, p_150155_4_))
			{
				this.dropBlockAsItem(p_150155_1_, p_150155_2_, p_150155_3_, p_150155_4_, p_150155_1_.getBlockMetadata(p_150155_2_, p_150155_3_, p_150155_4_), 0);
				p_150155_1_.setBlockToAir(p_150155_2_, p_150155_3_, p_150155_4_);
				return false;
			}
			else
			{
				return true;
			}
		}

		public virtual void harvestBlock(World p_149636_1_, EntityPlayer p_149636_2_, int p_149636_3_, int p_149636_4_, int p_149636_5_, int p_149636_6_)
		{
			int var7 = p_149636_6_ & 7;
			this.dropBlockAsItem_do(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, new ItemStack(Items.snowball, var7 + 1, 0));
			p_149636_1_.setBlockToAir(p_149636_3_, p_149636_4_, p_149636_5_);
			p_149636_2_.addStat(StatList.mineBlockStatArray[Block.getIdFromBlock(this)], 1);
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.snowball;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (p_149674_1_.getSavedLightValue(EnumSkyBlock.Block, p_149674_2_, p_149674_3_, p_149674_4_) > 11)
			{
				this.dropBlockAsItem(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_), 0);
				p_149674_1_.setBlockToAir(p_149674_2_, p_149674_3_, p_149674_4_);
			}
		}

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return p_149646_5_ == 1 ? true : base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_);
		}
	}

}