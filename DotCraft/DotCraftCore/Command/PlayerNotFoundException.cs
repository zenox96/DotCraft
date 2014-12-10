namespace DotCraftCore.Command
{

	public class PlayerNotFoundException : CommandException
	{
		private const string __OBFID = "CL_00001190";

		public PlayerNotFoundException() : this("commands.generic.player.notFound", new object[0])
		{
		}

		public PlayerNotFoundException(string p_i1362_1_, params object[] p_i1362_2_) : base(p_i1362_1_, p_i1362_2_)
		{
		}
	}

}