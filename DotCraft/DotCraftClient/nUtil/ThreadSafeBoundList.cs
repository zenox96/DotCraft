using System;

namespace DotCraftServer.nUtil
{
	public class ThreadSafeBoundList
	{
		public ThreadSafeBoundList(Type p_i1126_1_, int p_i1126_2_)
		{
			this.field_152760_b = p_i1126_1_;
			this.field_152759_a = (object[])((object[])Array.newInstance(p_i1126_1_, p_i1126_2_));
		}

		public virtual object func_152757_a(object p_152757_1_)
		{
			this.field_152761_c.writeLock().lock();
			this.field_152759_a[this.field_152763_e] = p_152757_1_;
			this.field_152763_e = (this.field_152763_e + 1) % this.func_152758_b();

			if(this.field_152762_d < this.func_152758_b())
			{
				++this.field_152762_d;
			}

			this.field_152761_c.writeLock().unlock();
			return p_152757_1_;
		}

		public virtual int func_152758_b()
		{
			this.field_152761_c.readLock().lock();
			int var1 = this.field_152759_a.Length;
			this.field_152761_c.readLock().unlock();
			return var1;
		}

		public virtual object[] func_152756_c()
		{
			object[] var1 = (object[])((object[])Array.newInstance(this.field_152760_b, this.field_152762_d));
			this.field_152761_c.readLock().lock();

			for(int var2 = 0; var2 < this.field_152762_d; ++var2)
			{
				int var3 = (this.field_152763_e - this.field_152762_d + var2) % this.func_152758_b();

				if(var3 < 0)
				{
					var3 += this.func_152758_b();
				}

				var1[var2] = this.field_152759_a[var3];
			}

			this.field_152761_c.readLock().unlock();
			return var1;
		}
	}

}