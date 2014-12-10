namespace DotCraftCore.Block
{

	public class MaterialTransparent : Material
	{
		public MaterialTransparent(MapColor p_i2113_1_) : base(p_i2113_1_)
		{
			this.setReplaceable();
		}

		public override bool Solid
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Will prevent grass from growing on dirt underneath and kill any grass below it if it returns true </summary>
///     
		public override bool CanBlockGrass
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Returns if this material is considered solid or not </summary>
///     
		public override bool blocksMovement()
		{
			return false;
		}
	}

}