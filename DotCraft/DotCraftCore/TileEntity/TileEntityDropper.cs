namespace DotCraftCore.TileEntity
{

	public class TileEntityDropper : TileEntityDispenser
	{
		private const string __OBFID = "CL_00000353";

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