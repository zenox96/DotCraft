using DotCraftCore.nWorld;
namespace DotCraftCore.nBlock
{
	public class BlockRail : BlockRailBase
	{
		protected internal BlockRail() : base(false)
		{
		}

		protected internal override void func_150048_a(World p_150048_1_, int p_150048_2_, int p_150048_3_, int p_150048_4_, int p_150048_5_, int p_150048_6_, Block p_150048_7_)
		{
			if (p_150048_7_.canProvidePower() && (new BlockRailBase.Rail(p_150048_1_, p_150048_2_, p_150048_3_, p_150048_4_)).func_150650_a() == 3)
			{
				this.func_150052_a(p_150048_1_, p_150048_2_, p_150048_3_, p_150048_4_, false);
			}
		}
	}

}