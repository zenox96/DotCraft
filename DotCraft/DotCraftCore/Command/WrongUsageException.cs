namespace DotCraftCore.Command
{

	public class WrongUsageException : SyntaxErrorException
	{
		private const string __OBFID = "CL_00001192";

		public WrongUsageException(string p_i1364_1_, params object[] p_i1364_2_) : base(p_i1364_1_, p_i1364_2_)
		{
		}
	}

}