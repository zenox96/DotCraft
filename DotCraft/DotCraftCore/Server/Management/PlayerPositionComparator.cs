namespace DotCraftCore.Server.Management
{

	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;

	public class PlayerPositionComparator : IComparer
	{
		private readonly ChunkCoordinates theChunkCoordinates;
		

		public PlayerPositionComparator(ChunkCoordinates p_i1499_1_)
		{
			this.theChunkCoordinates = p_i1499_1_;
		}

		public virtual int compare(EntityPlayerMP p_compare_1_, EntityPlayerMP p_compare_2_)
		{
			double var3 = p_compare_1_.getDistanceSq((double)this.theChunkCoordinates.posX, (double)this.theChunkCoordinates.posY, (double)this.theChunkCoordinates.posZ);
			double var5 = p_compare_2_.getDistanceSq((double)this.theChunkCoordinates.posX, (double)this.theChunkCoordinates.posY, (double)this.theChunkCoordinates.posZ);
			return var3 < var5 ? -1 : (var3 > var5 ? 1 : 0);
		}

		public virtual int compare(object p_compare_1_, object p_compare_2_)
		{
			return this.compare((EntityPlayerMP)p_compare_1_, (EntityPlayerMP)p_compare_2_);
		}
	}

}