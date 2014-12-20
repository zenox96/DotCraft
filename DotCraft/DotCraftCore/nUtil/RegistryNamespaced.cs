using System.Collections;

namespace DotCraftCore.nUtil
{

	public class RegistryNamespaced<T,K> : RegistrySimple<T,K>
	{
	/// <summary> The backing store that maps Integers to objects.  </summary>
		protected internal readonly ObjectIntIdentityMap<K> underlyingIntegerMap = new ObjectIntIdentityMap<K>();
		protected internal readonly IDictionary bimapDict;
		

		public RegistryNamespaced()
		{
			this.bimapDict = ((BiMap)this.registryObjects).inverse();
		}

///    
///     <summary> * Adds a new object to this registry, keyed by both the given integer ID and the given string. </summary>
///     
		public virtual void addObject(int intKey, string stringKey, K value)
		{
			this.underlyingIntegerMap.func_148746_a(value, intKey);
			this.putObject(ensureNamespaced(stringKey), value);
		}

///    
///     <summary> * Creates the Map we will use to map keys to their registered values. </summary>
///     
		protected internal override IDictionary createUnderlyingMap()
		{
			return HashBiMap.create();
		}

		public virtual K getObject(string stringKey)
		{
			return base.getObject(ensureNamespaced(stringKey));
		}

///    
///     <summary> * Gets the name we use to identify the given object. </summary>
///     
		public virtual string getNameForObject(K value)
		{
			return(string)this.bimapDict[value];
		}

///    
///     <summary> * Does this registry contain an entry for the given key? </summary>
///     
		public virtual bool containsKey(string stringKey)
		{
			return base.containsKey(ensureNamespaced(stringKey));
		}

///    
///     <summary> * Gets the integer ID we use to identify the given object. </summary>
///     
		public virtual int getIDForObject(K value)
		{
			return this.underlyingIntegerMap.func_148747_b(value);
		}

///    
///     <summary> * Gets the object identified by the given ID. </summary>
///     
		public virtual K getObjectForID(int intKey)
		{
			return this.underlyingIntegerMap.func_148745_a(intKey);
		}

        public virtual IEnumerator GetEnumerator( )
		{
			return this.underlyingIntegerMap.GetEnumerator();
		}

///    
///     <summary> * Gets a value indicating whether this registry contains an object that can be identified by the given integer
///     * value </summary>
///     
		public virtual bool containsID(int p_148753_1_)
		{
			return this.underlyingIntegerMap.func_148744_b(p_148753_1_);
		}

///    
///     <summary> * Ensures that the given name is indicated by a colon-delimited namespace, prepending "minecraft:" if it is not
///     * already. </summary>
///     
		private static string ensureNamespaced(string param1)
		{
			return param1.IndexOf(':') == -1 ? "minecraft:" + param1 : param1;
		}

///    
///     <summary> * Does this registry contain an entry for the given key? </summary>
///     
		public override bool containsKey(object p_148741_1_)
		{
			return this.containsKey((string)p_148741_1_);
		}

		public override object getObject(object p_82594_1_)
		{
			return this.getObject((string)p_82594_1_);
		}
	}

}