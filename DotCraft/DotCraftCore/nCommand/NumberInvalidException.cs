namespace DotCraftCore.nCommand
{

	public class NumberInvalidException : CommandException
	{
		

		public NumberInvalidException() : this("commands.generic.num.invalid", new object[0])
		{
		}

		public NumberInvalidException(string unlocalizedMessage, params object[] errorObjs) : base(unlocalizedMessage, errorObjs)
		{
		}
	}

}