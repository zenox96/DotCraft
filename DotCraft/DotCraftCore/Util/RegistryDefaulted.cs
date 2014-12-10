namespace DotCraftCore.Util
{

	public class RegistryDefaulted : RegistrySimple
	{
///    
///     <summary> * Default object for this registry, returned when an object is not found. </summary>
///     
		private readonly object defaultObject;
		private const string __OBFID = "CL_00001198";

		public RegistryDefaulted(object p_i1366_1_)
		{
			this.defaultObject = p_i1366_1_;
		}

		public override object getObject(object p_82594_1_)
		{
			object var2 = base.getObject(p_82594_1_);
			return var2 == null ? this.defaultObject : var2;
		}
	}

}