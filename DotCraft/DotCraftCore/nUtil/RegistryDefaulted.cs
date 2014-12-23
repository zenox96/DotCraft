namespace DotCraftCore.nUtil
{
	public class RegistryDefaulted<T,K> : RegistrySimple<T,K>
	{
		private readonly K defaultObject;

		public RegistryDefaulted(K defualt)
		{
			this.defaultObject = defualt;
		}

		public override K GetObject(T p_82594_1_)
		{
			K var2 = base.GetObject(p_82594_1_);
			return var2 == null ? this.defaultObject : var2;
		}
	}
}