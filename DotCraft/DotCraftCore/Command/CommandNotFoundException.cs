namespace DotCraftCore.Command
{

	public class CommandNotFoundException : CommandException
	{
		private const string __OBFID = "CL_00001191";

		public CommandNotFoundException() : this("commands.generic.notFound", new object[0])
		{
		}

		public CommandNotFoundException(string p_i1363_1_, params object[] p_i1363_2_) : base(p_i1363_1_, p_i1363_2_)
		{
		}
	}

}