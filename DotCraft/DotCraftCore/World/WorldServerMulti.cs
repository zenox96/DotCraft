namespace DotCraftCore.World
{

	using Profiler = DotCraftCore.profiler.Profiler;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using DerivedWorldInfo = DotCraftCore.World.Storage.DerivedWorldInfo;
	using ISaveHandler = DotCraftCore.World.Storage.ISaveHandler;

	public class WorldServerMulti : WorldServer
	{
		private const string __OBFID = "CL_00001430";

		public WorldServerMulti(MinecraftServer p_i45283_1_, ISaveHandler p_i45283_2_, string p_i45283_3_, int p_i45283_4_, WorldSettings p_i45283_5_, WorldServer p_i45283_6_, Profiler p_i45283_7_) : base(p_i45283_1_, p_i45283_2_, p_i45283_3_, p_i45283_4_, p_i45283_5_, p_i45283_7_)
		{
			this.mapStorage = p_i45283_6_.mapStorage;
			this.worldScoreboard = p_i45283_6_.Scoreboard;
			this.worldInfo = new DerivedWorldInfo(p_i45283_6_.WorldInfo);
		}

///    
///     <summary> * Saves the chunks to disk. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void saveLevel() throws MinecraftException
		protected internal override void saveLevel()
		{
		}
	}

}