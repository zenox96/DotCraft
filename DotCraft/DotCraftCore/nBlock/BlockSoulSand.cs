using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nInventory;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
namespace DotCraftCore.nBlock
{
	public class BlockSoulSand : Block
	{
		public BlockSoulSand() : base(Material.sand)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			float var5 = 0.125F;
			return AxisAlignedBB.getBoundingBox((double)p_149668_2_, (double)p_149668_3_, (double)p_149668_4_, (double)(p_149668_2_ + 1), (double)((float)(p_149668_3_ + 1) - var5), (double)(p_149668_4_ + 1));
		}

		public override void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
		{
			p_149670_5_.motionX *= 0.4D;
			p_149670_5_.motionZ *= 0.4D;
		}
	}

}