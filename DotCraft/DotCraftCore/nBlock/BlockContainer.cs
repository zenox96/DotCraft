namespace DotCraftCore.nBlock
{
    using DotCraftCore.nBlock.nMaterial;
    using DotCraftCore.nTileEntity;
    using DotCraftCore.nWorld;

	public abstract class BlockContainer : Block, ITileEntityProvider
	{
		protected internal BlockContainer(Material p_i45386_1_) : base(p_i45386_1_)
		{
			this.isBlockContainer = true;
		}

		public virtual void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
		}

		public virtual void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
			p_149749_1_.removeTileEntity(p_149749_2_, p_149749_3_, p_149749_4_);
		}

		public virtual bool onBlockEventReceived(World p_149696_1_, int p_149696_2_, int p_149696_3_, int p_149696_4_, int p_149696_5_, int p_149696_6_)
		{
			base.onBlockEventReceived(p_149696_1_, p_149696_2_, p_149696_3_, p_149696_4_, p_149696_5_, p_149696_6_);
			TileEntity var7 = p_149696_1_.getTileEntity(p_149696_2_, p_149696_3_, p_149696_4_);
			return var7 != null ? var7.receiveClientEvent(p_149696_5_, p_149696_6_) : false;
		}
	}

}