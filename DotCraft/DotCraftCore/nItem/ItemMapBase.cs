namespace DotCraftCore.nItem
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Packet = DotCraftCore.nNetwork.Packet;
	using World = DotCraftCore.nWorld.World;

	public class ItemMapBase : Item
	{
		

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