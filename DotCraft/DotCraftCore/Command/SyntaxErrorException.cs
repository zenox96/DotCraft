namespace DotCraftCore.Command
{

	public class SyntaxErrorException : CommandException
	{
		

		public SyntaxErrorException() : this("commands.generic.snytax", new object[0])
		{
		}

		public SyntaxErrorException(string p_i1361_1_, params object[] p_i1361_2_) : base(p_i1361_1_, p_i1361_2_)
		{
		}
	}

}