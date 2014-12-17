namespace DotCraftCore.nCommand
{

	public class NumberInvalidException : CommandException
	{
		

		public NumberInvalidException() : this("commands.generic.num.invalid", new object[0])
		{
		}

		public NumberInvalidException(string p_i1360_1_, params object[] p_i1360_2_) : base(p_i1360_1_, p_i1360_2_)
		{
		}
	}

}