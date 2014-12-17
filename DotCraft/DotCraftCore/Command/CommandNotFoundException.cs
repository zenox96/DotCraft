namespace DotCraftCore.nCommand
{

	public class CommandNotFoundException : CommandException
	{
		

		public CommandNotFoundException() : this("commands.generic.notFound", new object[0])
		{
		}

		public CommandNotFoundException(string p_i1363_1_, params object[] p_i1363_2_) : base(p_i1363_1_, p_i1363_2_)
		{
		}
	}

}