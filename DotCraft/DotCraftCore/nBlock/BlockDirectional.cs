using DotCraftCore.nBlock.nMaterial;
namespace DotCraftCore.nBlock
{
	public abstract class BlockDirectional : Block
	{
		protected internal BlockDirectional(Material p_i45401_1_) : base(p_i45401_1_)
		{
		}

		public static int func_149895_l(int p_149895_0_)
		{
			return p_149895_0_ & 3;
		}
	}

}