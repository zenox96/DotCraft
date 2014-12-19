namespace DotCraftCore.nWorld.nBiome
{

	using EntityDragon = DotCraftCore.entity.boss.EntityDragon;
	using Blocks = DotCraftCore.init.Blocks;
	using WorldGenSpikes = DotCraftCore.nWorld.nGen.nFeature.WorldGenSpikes;
	using WorldGenerator = DotCraftCore.nWorld.nGen.nFeature.WorldGenerator;

	public class BiomeEndDecorator : BiomeDecorator
	{
		protected internal WorldGenerator spikeGen;
		

		public BiomeEndDecorator()
		{
			this.spikeGen = new WorldGenSpikes(Blocks.end_stone);
		}

		protected internal override void func_150513_a(BiomeGenBase p_150513_1_)
		{
			this.generateOres();

			if (this.randomGenerator.Next(5) == 0)
			{
				int var2 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				int var3 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				int var4 = this.currentWorld.getTopSolidOrLiquidBlock(var2, var3);
				this.spikeGen.generate(this.currentWorld, this.randomGenerator, var2, var4, var3);
			}

			if (this.chunk_X == 0 && this.chunk_Z == 0)
			{
				EntityDragon var5 = new EntityDragon(this.currentWorld);
				var5.setLocationAndAngles(0.0D, 128.0D, 0.0D, this.randomGenerator.nextFloat() * 360.0F, 0.0F);
				this.currentWorld.spawnEntityInWorld(var5);
			}
		}
	}

}