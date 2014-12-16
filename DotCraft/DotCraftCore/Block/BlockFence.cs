using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using ItemLead = DotCraftCore.Item.ItemLead;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockFence : Block
	{
		private readonly string field_149827_a;
		

		public BlockFence(string p_i45406_1_, Material p_i45406_2_) : base(p_i45406_2_)
		{
			this.field_149827_a = p_i45406_1_;
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			bool var8 = this.func_149826_e(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_ - 1);
			bool var9 = this.func_149826_e(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_ + 1);
			bool var10 = this.func_149826_e(p_149743_1_, p_149743_2_ - 1, p_149743_3_, p_149743_4_);
			bool var11 = this.func_149826_e(p_149743_1_, p_149743_2_ + 1, p_149743_3_, p_149743_4_);
			float var12 = 0.375F;
			float var13 = 0.625F;
			float var14 = 0.375F;
			float var15 = 0.625F;

			if (var8)
			{
				var14 = 0.0F;
			}

			if (var9)
			{
				var15 = 1.0F;
			}

			if (var8 || var9)
			{
				this.setBlockBounds(var12, 0.0F, var14, var13, 1.5F, var15);
				base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			}

			var14 = 0.375F;
			var15 = 0.625F;

			if (var10)
			{
				var12 = 0.0F;
			}

			if (var11)
			{
				var13 = 1.0F;
			}

			if (var10 || var11 || !var8 && !var9)
			{
				this.setBlockBounds(var12, 0.0F, var14, var13, 1.5F, var15);
				base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			}

			if (var8)
			{
				var14 = 0.0F;
			}

			if (var9)
			{
				var15 = 1.0F;
			}

			this.setBlockBounds(var12, 0.0F, var14, var13, 1.0F, var15);
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			bool var5 = this.func_149826_e(p_149719_1_, p_149719_2_, p_149719_3_, p_149719_4_ - 1);
			bool var6 = this.func_149826_e(p_149719_1_, p_149719_2_, p_149719_3_, p_149719_4_ + 1);
			bool var7 = this.func_149826_e(p_149719_1_, p_149719_2_ - 1, p_149719_3_, p_149719_4_);
			bool var8 = this.func_149826_e(p_149719_1_, p_149719_2_ + 1, p_149719_3_, p_149719_4_);
			float var9 = 0.375F;
			float var10 = 0.625F;
			float var11 = 0.375F;
			float var12 = 0.625F;

			if (var5)
			{
				var11 = 0.0F;
			}

			if (var6)
			{
				var12 = 1.0F;
			}

			if (var7)
			{
				var9 = 0.0F;
			}

			if (var8)
			{
				var10 = 1.0F;
			}

			this.setBlockBounds(var9, 0.0F, var11, var10, 1.0F, var12);
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

		public virtual bool getBlocksMovement(IBlockAccess p_149655_1_, int p_149655_2_, int p_149655_3_, int p_149655_4_)
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
				return 11;
			}
		}

		public virtual bool func_149826_e(IBlockAccess p_149826_1_, int p_149826_2_, int p_149826_3_, int p_149826_4_)
		{
			Block var5 = p_149826_1_.getBlock(p_149826_2_, p_149826_3_, p_149826_4_);
			return var5 != this && var5 != Blocks.fence_gate ? (var5.blockMaterial.Opaque && var5.renderAsNormalBlock() ? var5.blockMaterial != Material.field_151572_C : false) : true;
		}

		public static bool func_149825_a(Block p_149825_0_)
		{
			return p_149825_0_ == Blocks.fence || p_149825_0_ == Blocks.nether_brick_fence;
		}

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return true;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.field_149827_a);
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			return p_149727_1_.isClient ? true : ItemLead.func_150909_a(p_149727_5_, p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_);
		}
	}

}