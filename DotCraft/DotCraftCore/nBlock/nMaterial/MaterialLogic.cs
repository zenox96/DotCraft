namespace DotCraftCore.nBlock.nMaterial
{

	public class MaterialLogic : Material
	{
		public MaterialLogic(MapColor p_i2112_1_) : base(p_i2112_1_)
		{
			this.setAdventureModeExempt();
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
		public override bool BlocksMovement()
		{
			return false;
		}
	}

}