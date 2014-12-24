using System;

namespace DotCraftCore.nCommand
{
	public class CommandException : Exception
	{
		public CommandException(string message, params object[] errorObjs) : base(message)
		{
            ErrorOjbects = errorObjs;
		}

		public readonly virtual object[] ErrorOjbects
		{
			get;
            protected set;
		}
	}
}