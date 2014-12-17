namespace DotCraftCore.nWorld.nDemo
{

	using Profiler = DotCraftCore.profiler.Profiler;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using WorldServer = DotCraftCore.nWorld.WorldServer;
	using WorldSettings = DotCraftCore.nWorld.WorldSettings;
	using WorldType = DotCraftCore.nWorld.WorldType;
	using ISaveHandler = DotCraftCore.nWorld.nStorage.ISaveHandler;

	public class DemoWorldServer : WorldServer
	{
		private const long demoWorldSeed = (long)"North Carolina".GetHashCode();
		public static readonly WorldSettings demoWorldSettings = (new WorldSettings(demoWorldSeed, WorldSettings.GameType.SURVIVAL, true, false, WorldType.DEFAULT)).enableBonusChest();
		

		public DemoWorldServer(MinecraftServer p_i45282_1_, ISaveHandler p_i45282_2_, string p_i45282_3_, int p_i45282_4_, Profiler p_i45282_5_) : base(p_i45282_1_, p_i45282_2_, p_i45282_3_, p_i45282_4_, demoWorldSettings, p_i45282_5_)
		{
		}
	}

}