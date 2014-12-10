namespace DotCraftCore.TileEntity
{

	using BlockDaylightDetector = DotCraftCore.block.BlockDaylightDetector;

	public class TileEntityDaylightDetector : TileEntity
	{
		private const string __OBFID = "CL_00000350";

		public override void updateEntity()
		{
			if(this.worldObj != null && !this.worldObj.isClient && this.worldObj.TotalWorldTime % 20L == 0L)
			{
				this.blockType = this.BlockType;

				if(this.blockType is BlockDaylightDetector)
				{
					((BlockDaylightDetector)this.blockType).func_149957_e(this.worldObj, this.field_145851_c, this.field_145848_d, this.field_145849_e);
				}
			}
		}
	}

}