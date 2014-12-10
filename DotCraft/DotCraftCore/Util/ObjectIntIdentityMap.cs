using System;
using System.Collections;

namespace DotCraftCore.Util
{

	using Predicates = com.google.common.base.Predicates;
	using Iterators = com.google.common.collect.Iterators;
	using Lists = com.google.common.collect.Lists;

	public class ObjectIntIdentityMap : IObjectIntIterable
	{
		private IdentityHashMap field_148749_a = new IdentityHashMap(512);
		private IList field_148748_b = Lists.newArrayList();
		private const string __OBFID = "CL_00001203";

		public virtual void func_148746_a(object p_148746_1_, int p_148746_2_)
		{
			this.field_148749_a.put(p_148746_1_, Convert.ToInt32(p_148746_2_));

			while(this.field_148748_b.Count <= p_148746_2_)
			{
				this.field_148748_b.Add((object)null);
			}

			this.field_148748_b[p_148746_2_] = p_148746_1_;
		}

		public virtual int func_148747_b(object p_148747_1_)
		{
			int? var2 = (int?)this.field_148749_a.get(p_148747_1_);
			return var2 == null ? -1 : (int)var2;
		}

		public virtual object func_148745_a(int p_148745_1_)
		{
			return p_148745_1_ >= 0 && p_148745_1_ < this.field_148748_b.Count ? this.field_148748_b[p_148745_1_] : null;
		}

		public virtual IEnumerator iterator()
		{
			return Iterators.filter(this.field_148748_b.GetEnumerator(), Predicates.notNull());
		}

		public virtual bool func_148744_b(int p_148744_1_)
		{
			return this.func_148745_a(p_148744_1_) != null;
		}
	}

}