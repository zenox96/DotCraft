using System;

namespace DotCraftCore.World.Gen.Feature
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;

	public abstract class WorldGenAbstractTree : WorldGenerator
	{
		private const string __OBFID = "CL_00000399";

		public WorldGenAbstractTree(bool p_i45448_1_) : base(p_i45448_1_)
		{
		}

		protected internal virtual bool func_150523_a(Block p_150523_1_)
		{
			return p_150523_1_.Material == Material.air || p_150523_1_.Material == Material.leaves || p_150523_1_ == Blocks.grass || p_150523_1_ == Blocks.dirt || p_150523_1_ == Blocks.log || p_150523_1_ == Blocks.log2 || p_150523_1_ == Blocks.sapling || p_150523_1_ == Blocks.vine;
		}

		public virtual void func_150524_b(World p_150524_1_, Random p_150524_2_, int p_150524_3_, int p_150524_4_, int p_150524_5_)
		{
		}
	}

}