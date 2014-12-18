using System;

namespace DotCraftCore.nWorld.nGen.nFeature
{

	using Block = DotCraftCore.nBlock.Block;
	using World = DotCraftCore.nWorld.World;

	public abstract class WorldGenerator
	{
///    
///     <summary> * Sets wither or not the generator should notify blocks of blocks it changes. When the world is first generated,
///     * this is false, when saplings grow, this is true. </summary>
///     
		private readonly bool doBlockNotify;
		

		public WorldGenerator()
		{
			this.doBlockNotify = false;
		}

		public WorldGenerator(bool p_i2013_1_)
		{
			this.doBlockNotify = p_i2013_1_;
		}

		public abstract bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_);

///    
///     <summary> * Rescales the generator settings, only used in WorldGenBigTree </summary>
///     
		public virtual void setScale(double p_76487_1_, double p_76487_3_, double p_76487_5_)
		{
		}

		protected internal virtual void func_150515_a(World p_150515_1_, int p_150515_2_, int p_150515_3_, int p_150515_4_, Block p_150515_5_)
		{
			this.func_150516_a(p_150515_1_, p_150515_2_, p_150515_3_, p_150515_4_, p_150515_5_, 0);
		}

		protected internal virtual void func_150516_a(World p_150516_1_, int p_150516_2_, int p_150516_3_, int p_150516_4_, Block p_150516_5_, int p_150516_6_)
		{
			if (this.doBlockNotify)
			{
				p_150516_1_.setBlock(p_150516_2_, p_150516_3_, p_150516_4_, p_150516_5_, p_150516_6_, 3);
			}
			else
			{
				p_150516_1_.setBlock(p_150516_2_, p_150516_3_, p_150516_4_, p_150516_5_, p_150516_6_, 2);
			}
		}
	}

}