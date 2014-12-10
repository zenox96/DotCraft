namespace DotCraftCore.Item
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Packet = DotCraftCore.Network.Packet;
	using World = DotCraftCore.World.World;

	public class ItemMapBase : Item
	{
		private const string __OBFID = "CL_00000004";

///    
///     <summary> * false for all Items except sub-classes of ItemMapBase </summary>
///     
		public virtual bool isMap()
		{
			get
			{
				return true;
			}
		}

		public virtual Packet func_150911_c(ItemStack p_150911_1_, World p_150911_2_, EntityPlayer p_150911_3_)
		{
			return null;
		}
	}

}