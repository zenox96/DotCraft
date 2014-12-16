using System;

namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using TileEntityNote = DotCraftCore.TileEntity.TileEntityNote;
	using World = DotCraftCore.World.World;

	public class BlockNote : BlockContainer
	{
		

		public BlockNote() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabRedstone;
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			bool var6 = p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_);
			TileEntityNote var7 = (TileEntityNote)p_149695_1_.getTileEntity(p_149695_2_, p_149695_3_, p_149695_4_);

			if (var7 != null && var7.field_145880_i != var6)
			{
				if (var6)
				{
					var7.func_145878_a(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_);
				}

				var7.field_145880_i = var6;
			}
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (p_149727_1_.isClient)
			{
				return true;
			}
			else
			{
				TileEntityNote var10 = (TileEntityNote)p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_);

				if (var10 != null)
				{
					var10.func_145877_a();
					var10.func_145878_a(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_);
				}

				return true;
			}
		}

///    
///     <summary> * Called when a player hits the block. Args: world, x, y, z, player </summary>
///     
		public virtual void onBlockClicked(World p_149699_1_, int p_149699_2_, int p_149699_3_, int p_149699_4_, EntityPlayer p_149699_5_)
		{
			if (!p_149699_1_.isClient)
			{
				TileEntityNote var6 = (TileEntityNote)p_149699_1_.getTileEntity(p_149699_2_, p_149699_3_, p_149699_4_);

				if (var6 != null)
				{
					var6.func_145878_a(p_149699_1_, p_149699_2_, p_149699_3_, p_149699_4_);
				}
			}
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityNote();
		}

		public override bool onBlockEventReceived(World p_149696_1_, int p_149696_2_, int p_149696_3_, int p_149696_4_, int p_149696_5_, int p_149696_6_)
		{
			float var7 = (float)Math.Pow(2.0D, (double)(p_149696_6_ - 12) / 12.0D);
			string var8 = "harp";

			if (p_149696_5_ == 1)
			{
				var8 = "bd";
			}

			if (p_149696_5_ == 2)
			{
				var8 = "snare";
			}

			if (p_149696_5_ == 3)
			{
				var8 = "hat";
			}

			if (p_149696_5_ == 4)
			{
				var8 = "bassattack";
			}

			p_149696_1_.playSoundEffect((double)p_149696_2_ + 0.5D, (double)p_149696_3_ + 0.5D, (double)p_149696_4_ + 0.5D, "note." + var8, 3.0F, var7);
			p_149696_1_.spawnParticle("note", (double)p_149696_2_ + 0.5D, (double)p_149696_3_ + 1.2D, (double)p_149696_4_ + 0.5D, (double)p_149696_6_ / 24.0D, 0.0D, 0.0D);
			return true;
		}
	}

}