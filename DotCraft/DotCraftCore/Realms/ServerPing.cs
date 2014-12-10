namespace DotCraftCore.Realms
{

	public class ServerPing
	{
		public volatile string nrOfPlayers = "0";
		public volatile long lastPingSnapshot = 0L;
		private const string __OBFID = "CL_00001860";
	}

}