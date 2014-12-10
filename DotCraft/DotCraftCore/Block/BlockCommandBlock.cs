using System;

namespace DotCraftCore.Block
{
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using TileEntityCommandBlock = DotCraftCore.TileEntity.TileEntityCommandBlock;
	using World = DotCraftCore.World.World;

	public class BlockCommandBlock : BlockContainer
	{
		private const string __OBFID = "CL_00000219";

		public BlockCommandBlock() : base(Material.iron)
		{
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityCommandBlock();
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!p_149695_1_.isClient)
			{
				bool var6 = p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_);
				int var7 = p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_);
				bool var8 = (var7 & 1) != 0;

				if (var6 && !var8)
				{
					p_149695_1_.setBlockMetadataWithNotify(p_149695_2_, p_149695_3_, p_149695_4_, var7 | 1, 4);
					p_149695_1_.scheduleBlockUpdate(p_149695_2_, p_149695_3_, p_149695_4_, this, this.func_149738_a(p_149695_1_));
				}
				else if (!var6 && var8)
				{
					p_149695_1_.setBlockMetadataWithNotify(p_149695_2_, p_149695_3_, p_149695_4_, var7 & -2, 4);
				}
			}
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			TileEntity var6 = p_149674_1_.getTileEntity(p_149674_2_, p_149674_3_, p_149674_4_);

			if (var6 != null && var6 is TileEntityCommandBlock)
			{
				CommandBlockLogic var7 = ((TileEntityCommandBlock)var6).func_145993_a();
				var7.func_145755_a(p_149674_1_);
				p_149674_1_.func_147453_f(p_149674_2_, p_149674_3_, p_149674_4_, this);
			}
		}

		public virtual int func_149738_a(World p_149738_1_)
		{
			return 1;
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			TileEntityCommandBlock var10 = (TileEntityCommandBlock)p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_);

			if (var10 != null)
			{
				p_149727_5_.func_146100_a(var10);
			}

			return true;
		}

		public virtual bool hasComparatorInputOverride()
		{
			return true;
		}

		public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			TileEntity var6 = p_149736_1_.getTileEntity(p_149736_2_, p_149736_3_, p_149736_4_);
			return var6 != null && var6 is TileEntityCommandBlock ? ((TileEntityCommandBlock)var6).func_145993_a().func_145760_g() : 0;
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			TileEntityCommandBlock var7 = (TileEntityCommandBlock)p_149689_1_.getTileEntity(p_149689_2_, p_149689_3_, p_149689_4_);

			if (p_149689_6_.hasDisplayName())
			{
				var7.func_145993_a().func_145754_b(p_149689_6_.DisplayName);
			}
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}
	}

}