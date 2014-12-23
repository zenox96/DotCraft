using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockPistonExtension : Block
	{
		public BlockPistonExtension() : base(Material.piston)
		{
			this.StepSound = soundTypePiston;
			this.Hardness = 0.5F;
		}

		public override void func_150087_e()
		{
			this.field_150088_a = null;
		}

///    
///     <summary> * Called when the block is attempted to be harvested </summary>
///     
		public override void onBlockHarvested(World p_149681_1_, int p_149681_2_, int p_149681_3_, int p_149681_4_, int p_149681_5_, EntityPlayer p_149681_6_)
		{
			if (p_149681_6_.capabilities.isCreativeMode)
			{
				int var7 = func_150085_b(p_149681_5_);
				Block var8 = p_149681_1_.getBlock(p_149681_2_ - Facing.offsetsXForSide[var7], p_149681_3_ - Facing.offsetsYForSide[var7], p_149681_4_ - Facing.offsetsZForSide[var7]);

				if (var8 == Blocks.piston || var8 == Blocks.sticky_piston)
				{
					p_149681_1_.setBlockToAir(p_149681_2_ - Facing.offsetsXForSide[var7], p_149681_3_ - Facing.offsetsYForSide[var7], p_149681_4_ - Facing.offsetsZForSide[var7]);
				}
			}

			base.onBlockHarvested(p_149681_1_, p_149681_2_, p_149681_3_, p_149681_4_, p_149681_5_, p_149681_6_);
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
			int var7 = Facing.oppositeSide[func_150085_b(p_149749_6_)];
			p_149749_2_ += Facing.offsetsXForSide[var7];
			p_149749_3_ += Facing.offsetsYForSide[var7];
			p_149749_4_ += Facing.offsetsZForSide[var7];
			Block var8 = p_149749_1_.getBlock(p_149749_2_, p_149749_3_, p_149749_4_);

			if (var8 == Blocks.piston || var8 == Blocks.sticky_piston)
			{
				p_149749_6_ = p_149749_1_.getBlockMetadata(p_149749_2_, p_149749_3_, p_149749_4_);

				if (BlockPistonBase.func_150075_c(p_149749_6_))
				{
					var8.dropBlockAsItem(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_6_, 0);
					p_149749_1_.setBlockToAir(p_149749_2_, p_149749_3_, p_149749_4_);
				}
			}
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return 17;
			}
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return false;
		}

///    
///     <summary> * checks to see if you can place this block can be placed on that side of a block: BlockLever overrides </summary>
///     
		public override bool canPlaceBlockOnSide(World p_149707_1_, int p_149707_2_, int p_149707_3_, int p_149707_4_, int p_149707_5_)
		{
			return false;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

		public override void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			int var8 = p_149743_1_.getBlockMetadata(p_149743_2_, p_149743_3_, p_149743_4_);
			float var9 = 0.25F;
			float var10 = 0.375F;
			float var11 = 0.625F;
			float var12 = 0.25F;
			float var13 = 0.75F;

			switch (func_150085_b(var8))
			{
				case 0:
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.25F, 1.0F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					this.setBlockBounds(0.375F, 0.25F, 0.375F, 0.625F, 1.0F, 0.625F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					break;

				case 1:
					this.setBlockBounds(0.0F, 0.75F, 0.0F, 1.0F, 1.0F, 1.0F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					this.setBlockBounds(0.375F, 0.0F, 0.375F, 0.625F, 0.75F, 0.625F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					break;

				case 2:
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.25F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					this.setBlockBounds(0.25F, 0.375F, 0.25F, 0.75F, 0.625F, 1.0F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					break;

				case 3:
					this.setBlockBounds(0.0F, 0.0F, 0.75F, 1.0F, 1.0F, 1.0F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					this.setBlockBounds(0.25F, 0.375F, 0.0F, 0.75F, 0.625F, 0.75F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					break;

				case 4:
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 0.25F, 1.0F, 1.0F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					this.setBlockBounds(0.375F, 0.25F, 0.25F, 0.625F, 0.75F, 1.0F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					break;

				case 5:
					this.setBlockBounds(0.75F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
					this.setBlockBounds(0.0F, 0.375F, 0.25F, 0.75F, 0.625F, 0.75F);
					base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
				break;
			}

			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			int var5 = p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_);
			float var6 = 0.25F;

			switch (func_150085_b(var5))
			{
				case 0:
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.25F, 1.0F);
					break;

				case 1:
					this.setBlockBounds(0.0F, 0.75F, 0.0F, 1.0F, 1.0F, 1.0F);
					break;

				case 2:
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.25F);
					break;

				case 3:
					this.setBlockBounds(0.0F, 0.0F, 0.75F, 1.0F, 1.0F, 1.0F);
					break;

				case 4:
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 0.25F, 1.0F, 1.0F);
					break;

				case 5:
					this.setBlockBounds(0.75F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
				break;
			}
		}

		public override void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			int var6 = func_150085_b(p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_));
			Block var7 = p_149695_1_.getBlock(p_149695_2_ - Facing.offsetsXForSide[var6], p_149695_3_ - Facing.offsetsYForSide[var6], p_149695_4_ - Facing.offsetsZForSide[var6]);

			if (var7 != Blocks.piston && var7 != Blocks.sticky_piston)
			{
				p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
			}
			else
			{
				var7.onNeighborBlockChange(p_149695_1_, p_149695_2_ - Facing.offsetsXForSide[var6], p_149695_3_ - Facing.offsetsYForSide[var6], p_149695_4_ - Facing.offsetsZForSide[var6], p_149695_5_);
			}
		}

		public static int func_150085_b(int p_150085_0_)
		{
			return MathHelper.clamp_int(p_150085_0_ & 7, 0, Facing.offsetsXForSide.Length - 1);
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public override Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			int var5 = p_149694_1_.getBlockMetadata(p_149694_2_, p_149694_3_, p_149694_4_);
			return (var5 & 8) != 0 ? Item.getItemFromBlock(Blocks.sticky_piston) : Item.getItemFromBlock(Blocks.piston);
		}
	}

}