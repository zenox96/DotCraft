using System.Collections.Generic;

namespace DotCraftCore.nUtil
{
	public class RegistrySimple<T,K> : IRegistry<T,K>
	{
		//private static readonly Logger logger = LogManager.Logger;

	/// <summary> Objects registered on this registry.  </summary>
        protected internal readonly IDictionary<T, K> registryObjects = new Dictionary<T,K>();
		
///    
///     <summary> * Creates the Map we will use to map keys to their registered values. </summary>
///     
		public virtual K getObject(T key)
		{
			return this.registryObjects[key];
		}

///    
///     <summary> * Register an object on this registry. </summary>
///     
		public virtual void putObject(T key, K value)
		{
			if(this.registryObjects.ContainsKey(key))
			{
				//logger.debug("Adding duplicate key \'" + p_82595_1_ + "\' to registry");
			}

			this.registryObjects.Add(key, value);
		}

///    
///     <summary> * Gets all the keys recognized by this registry. </summary>
///     
		public virtual ICollection<T> Keys
		{
			get
			{
                return registryObjects.Keys;
			}
		}

///    
///     <summary> * Does this registry contain an entry for the given key? </summary>
///     
		public virtual bool containsKey(T key)
		{
			return this.registryObjects.ContainsKey(key);
		}
	}

}