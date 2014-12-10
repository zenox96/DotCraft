namespace DotCraftCore.Command
{

	public class CommandException : Exception
	{
		private object[] errorObjects;
		private const string __OBFID = "CL_00001187";

		public CommandException(string p_i1359_1_, params object[] p_i1359_2_) : base(p_i1359_1_)
		{
			this.errorObjects = p_i1359_2_;
		}

		public virtual object[] ErrorOjbects
		{
			get
			{
				return this.errorObjects;
			}
		}
	}

}