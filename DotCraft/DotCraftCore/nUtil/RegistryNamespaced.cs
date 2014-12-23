using System.Collections;

namespace DotCraftCore.nUtil
{
	public class RegistryNamespaced<K> : RegistrySimple<string,K>
	{
		protected internal readonly ObjectIntIdentityMap<K> underlyingIntegerMap = new ObjectIntIdentityMap<K>();
		protected internal readonly BiMap<K,string> bimapDict;
		
		public RegistryNamespaced()
		{
			this.bimapDict = ((BiMap<string,K>)this.registryObjects).Inverse();
		}

		public virtual void AddObject(int intKey, string stringKey, K value)
		{
			this.underlyingIntegerMap.Add(value, intKey);
			this.putObject(EnsureNamespaced(stringKey), value);
		}

		public virtual K GetObject(string stringKey)
		{
			return base.GetObject(EnsureNamespaced(stringKey));
		}

///    
///     <summary> * Gets the name we use to identify the given object. </summary>
///     
		public virtual string GetNameForObject(K value)
		{
			return(string)this.bimapDict[value];
		}

///    
///     <summary> * Does this registry contain an entry for the given key? </summary>
///     
		public virtual bool ContainsKey(string stringKey)
		{
			return base.containsKey(EnsureNamespaced(stringKey));
		}

///    
///     <summary> * Gets the integer ID we use to identify the given object. </summary>
///     
		public virtual int GetIDForObject(K value)
		{
			return this.underlyingIntegerMap.GetValue(value);
		}

///    
///     <summary> * Gets the object identified by the given ID. </summary>
///     
		public virtual K GetObjectForID(int intKey)
		{
			return this.underlyingIntegerMap.GetObject(intKey);
		}

        public virtual IEnumerator GetEnumerator( )
		{
			return this.underlyingIntegerMap.GetEnumerator();
		}

///    
///     <summary> * Gets a value indicating whether this registry contains an object that can be identified by the given integer
///     * value </summary>
///     
		public virtual bool ContainsID(int p_148753_1_)
		{
			return this.underlyingIntegerMap.IsObjectNotNull(p_148753_1_);
		}

///    
///     <summary> * Ensures that the given name is indicated by a colon-delimited namespace, prepending "minecraft:" if it is not
///     * already. </summary>
///     
		private static string EnsureNamespaced(string param1)
		{
			return param1.IndexOf(':') == -1 ? "minecraft:" + param1 : param1;
		}
	}
}