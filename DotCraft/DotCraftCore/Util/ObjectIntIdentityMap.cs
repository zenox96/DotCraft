using System;
using System.Collections;
using System.Collections.Generic;

namespace DotCraftCore.Util
{
	public class ObjectIntIdentityMap<T>
	{
		private Dictionary<T, int> map = new Dictionary<T, int>(512);
		private List<T> list = new List<T>();
		

		public virtual void func_148746_a(T key, int value)
		{
			this.map.Add(key, value);

            if (list.Capacity<value) list.Capacity = value;

			this.list[value] = key;
		}

		public virtual int getValue(T key)
		{
			int var2 = this.map[key];
			return var2 == null ? -1 : var2;
		}

		public virtual T? getObject(int index)
		{
            if (index >= 0 && index < this.list.Count)
			    return this.list[index];
            else
                throw new ArgumentOutOfRangeException("" + index);
		}

		public virtual IEnumerator iterator()
		{
            return list.GetEnumerator();
		}

		public virtual bool isObjectNotNull(int p_148744_1_)
		{
			return this.getObject(p_148744_1_) != null;
		}
	}

}