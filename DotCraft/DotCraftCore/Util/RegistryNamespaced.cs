using System.Collections;

namespace DotCraftCore.Util
{

	using BiMap = com.google.common.collect.BiMap;
	using HashBiMap = com.google.common.collect.HashBiMap;

	public class RegistryNamespaced : RegistrySimple, IObjectIntIterable
	{
	/// <summary> The backing store that maps Integers to objects.  </summary>
		protected internal readonly ObjectIntIdentityMap underlyingIntegerMap = new ObjectIntIdentityMap();
		protected internal readonly IDictionary field_148758_b;
		private const string __OBFID = "CL_00001206";

		public RegistryNamespaced()
		{
			this.field_148758_b = ((BiMap)this.registryObjects).inverse();
		}

///    
///     <summary> * Adds a new object to this registry, keyed by both the given integer ID and the given string. </summary>
///     
		public virtual void addObject(int p_148756_1_, string p_148756_2_, object p_148756_3_)
		{
			this.underlyingIntegerMap.func_148746_a(p_148756_3_, p_148756_1_);
			this.putObject(ensureNamespaced(p_148756_2_), p_148756_3_);
		}

///    
///     <summary> * Creates the Map we will use to map keys to their registered values. </summary>
///     
		protected internal override IDictionary createUnderlyingMap()
		{
			return HashBiMap.create();
		}

		public virtual object getObject(string p_82594_1_)
		{
			return base.getObject(ensureNamespaced(p_82594_1_));
		}

///    
///     <summary> * Gets the name we use to identify the given object. </summary>
///     
		public virtual string getNameForObject(object p_148750_1_)
		{
			return(string)this.field_148758_b.get(p_148750_1_);
		}

///    
///     <summary> * Does this registry contain an entry for the given key? </summary>
///     
		public virtual bool containsKey(string p_148741_1_)
		{
			return base.containsKey(ensureNamespaced(p_148741_1_));
		}

///    
///     <summary> * Gets the integer ID we use to identify the given object. </summary>
///     
		public virtual int getIDForObject(object p_148757_1_)
		{
			return this.underlyingIntegerMap.func_148747_b(p_148757_1_);
		}

///    
///     <summary> * Gets the object identified by the given ID. </summary>
///     
		public virtual object getObjectForID(int p_148754_1_)
		{
			return this.underlyingIntegerMap.func_148745_a(p_148754_1_);
		}

		public virtual IEnumerator iterator()
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
		private static string ensureNamespaced(string p_148755_0_)
		{
			return p_148755_0_.IndexOf(58) == -1 ? "minecraft:" + p_148755_0_ : p_148755_0_;
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