using System;

namespace DotCraftCore.nBlock
{

	
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Entity = DotCraftCore.nEntity.Entity;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using World = DotCraftCore.nWorld.World;

	public class BlockWeb : Block
	{
		

		public BlockWeb() : base(Material.field_151569_G)
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

		public virtual void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
		{
			p_149670_5_.setInWeb();
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			return null;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 1;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.string;
		}

		protected internal virtual bool canSilkHarvest()
		{
			return true;
		}
	}

}