namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;

	public class BlockSponge : Block
	{
		

		protected internal BlockSponge() : base(Material.sponge)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}
	}

}