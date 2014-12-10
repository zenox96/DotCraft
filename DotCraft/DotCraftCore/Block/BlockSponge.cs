namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;

	public class BlockSponge : Block
	{
		private const string __OBFID = "CL_00000311";

		protected internal BlockSponge() : base(Material.sponge)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}
	}

}