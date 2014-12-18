namespace DotCraftCore.nCommand
{

	public class PlayerNotFoundException : CommandException
	{
		

		public PlayerNotFoundException() : this("commands.generic.player.notFound", new object[0])
		{
		}

		public PlayerNotFoundException(string p_i1362_1_, params object[] p_i1362_2_) : base(p_i1362_1_, p_i1362_2_)
		{
		}
	}

}