using System;

namespace DotCraftCore.World.Gen.Structure
{

	using World = DotCraftCore.World.World;

	public class StructureMineshaftStart : StructureStart
	{
		

		public StructureMineshaftStart()
		{
		}

		public StructureMineshaftStart(World p_i2039_1_, Random p_i2039_2_, int p_i2039_3_, int p_i2039_4_) : base(p_i2039_3_, p_i2039_4_)
		{
			StructureMineshaftPieces.Room var5 = new StructureMineshaftPieces.Room(0, p_i2039_2_, (p_i2039_3_ << 4) + 2, (p_i2039_4_ << 4) + 2);
			this.components.add(var5);
			var5.buildComponent(var5, this.components, p_i2039_2_);
			this.updateBoundingBox();
			this.markAvailableHeight(p_i2039_1_, p_i2039_2_, 10);
		}
	}

}