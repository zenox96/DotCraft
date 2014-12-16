namespace DotCraftCore.TileEntity
{

	public class TileEntityDropper : TileEntityDispenser
	{
		

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public override string InventoryName
		{
			get
			{
				return this.InventoryNameLocalized ? this.field_146020_a : "container.dropper";
			}
		}
	}

}