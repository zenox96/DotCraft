using System.Collections;

namespace DotCraftCore.Util
{

	using Maps = com.google.common.collect.Maps;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class RegistrySimple : IRegistry
	{
		private static readonly Logger logger = LogManager.Logger;

	/// <summary> Objects registered on this registry.  </summary>
		protected internal readonly IDictionary registryObjects = this.createUnderlyingMap();
		private const string __OBFID = "CL_00001210";

///    
///     <summary> * Creates the Map we will use to map keys to their registered values. </summary>
///     
		protected internal virtual IDictionary createUnderlyingMap()
		{
			return Maps.newHashMap();
		}

		public virtual object getObject(object p_82594_1_)
		{
			return this.registryObjects[p_82594_1_];
		}

///    
///     <summary> * Register an object on this registry. </summary>
///     
		public virtual void putObject(object p_82595_1_, object p_82595_2_)
		{
			if(this.registryObjects.ContainsKey(p_82595_1_))
			{
				logger.debug("Adding duplicate key \'" + p_82595_1_ + "\' to registry");
			}

			this.registryObjects.Add(p_82595_1_, p_82595_2_);
		}

///    
///     <summary> * Gets all the keys recognized by this registry. </summary>
///     
		public virtual Set Keys
		{
			get
			{
				return Collections.unmodifiableSet(this.registryObjects.Keys);
			}
		}

///    
///     <summary> * Does this registry contain an entry for the given key? </summary>
///     
		public virtual bool containsKey(object p_148741_1_)
		{
			return this.registryObjects.ContainsKey(p_148741_1_);
		}
	}

}