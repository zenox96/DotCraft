namespace DotCraftCore.Util
{

	public class RegistryDefaulted<T,K> : RegistrySimple<T,K>
	{
///    
///     <summary> * Default object for this registry, returned when an object is not found. </summary>
///     
		private readonly K defaultObject;
		

		public RegistryDefaulted(K defualt)
		{
			this.defaultObject = defualt;
		}

		public override K getObject(T p_82594_1_)
		{
			K var2 = base.getObject(p_82594_1_);
			return var2 == null ? this.defaultObject : var2;
		}
	}

}