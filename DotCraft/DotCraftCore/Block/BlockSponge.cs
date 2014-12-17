namespace DotCraftCore.nBlock
{

	
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;

	public class BlockSponge : Block
	{
		

		protected internal BlockSponge() : base(Material.sponge)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}
	}

}