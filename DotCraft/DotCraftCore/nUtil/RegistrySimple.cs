using System.Collections.Generic;

namespace DotCraftCore.nUtil
{
	public class RegistrySimple<T,K> : IRegistry<T,K> // IS only a base class
	{
        protected internal readonly IDictionary<T, K> registryObjects = new Dictionary<T,K>();
		
		public virtual K GetObject(T key)
		{
			return this.registryObjects[key];
		}
		
        public virtual void putObject(T key, K value)
		{
			if(this.registryObjects.ContainsKey(key))
			{
				//logger.debug("Adding duplicate key \'" + p_82595_1_ + "\' to registry");
			}

			this.registryObjects.Add(key, value);
		}
		
        public virtual ICollection<T> Keys
		{
			get
			{
                return registryObjects.Keys;
			}
		}

		public virtual bool containsKey(T key)
		{
			return this.registryObjects.ContainsKey(key);
		}
	}
}