namespace DotCraftCore.Block.Material
{

	public class MaterialLiquid : Material
	{
		public MaterialLiquid(MapColor p_i2114_1_) : base(p_i2114_1_)
		{
			this.setReplaceable();
			this.setNoPushMobility();
		}

///    
///     <summary> * Returns if blocks of these materials are liquids. </summary>
///     
		public override bool Liquid
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Returns if this material is considered solid or not </summary>
///     
		public override bool blocksMovement()
		{
			return false;
		}

		public override bool Solid
		{
			get
			{
				return false;
			}
		}
	}

}