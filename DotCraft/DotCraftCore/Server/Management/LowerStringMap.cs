using System.Collections;

namespace DotCraftCore.Server.Management
{


	public class LowerStringMap : IDictionary
	{
		private readonly IDictionary internalMap = new LinkedHashMap();
		private const string __OBFID = "CL_00001488";

		public virtual int size()
		{
			return this.internalMap.Count;
		}

		public virtual bool isEmpty()
		{
			get
			{
				return this.internalMap.Count == 0;
			}
		}

		public virtual bool containsKey(object p_containsKey_1_)
		{
			return this.internalMap.ContainsKey(p_containsKey_1_.ToString().ToLower());
		}

		public virtual bool containsValue(object p_containsValue_1_)
		{
			return this.internalMap.ContainsKey(p_containsValue_1_);
		}

		public virtual object get(object p_get_1_)
		{
			return this.internalMap[p_get_1_.ToString().ToLower()];
		}

		public virtual object put(string p_put_1_, object p_put_2_)
		{
			return this.internalMap.Add(p_put_1_.ToLower(), p_put_2_);
		}

		public virtual object remove(object p_remove_1_)
		{
			return this.internalMap.Remove(p_remove_1_.ToString().ToLower());
		}

		public virtual void putAll(IDictionary p_putAll_1_)
		{
			IEnumerator var2 = p_putAll_1_.GetEnumerator();

			while(var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;
				this.put((string)var3.Key, var3.Value);
			}
		}

		public virtual void clear()
		{
			this.internalMap.Clear();
		}

		public virtual Set keySet()
		{
			return this.internalMap.Keys;
		}

		public virtual ICollection values()
		{
			return this.internalMap.Values;
		}

		public virtual Set entrySet()
		{
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'entrySet' method:
//			return this.internalMap.entrySet();
		}

		public virtual object put(object p_put_1_, object p_put_2_)
		{
			return this.put((string)p_put_1_, p_put_2_);
		}
	}

}