using System.Collections;

namespace DotCraftCore.Stats
{

	using Maps = com.google.common.collect.Maps;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using IJsonSerializable = DotCraftCore.Util.IJsonSerializable;
	using TupleIntJsonSerializable = DotCraftCore.Util.TupleIntJsonSerializable;

	public class StatFileWriter
	{
		protected internal readonly IDictionary field_150875_a = Maps.newConcurrentMap();
		

///    
///     <summary> * Returns true if the achievement has been unlocked. </summary>
///     
		public virtual bool hasAchievementUnlocked(Achievement p_77443_1_)
		{
			return this.writeStat(p_77443_1_) > 0;
		}

///    
///     <summary> * Returns true if the parent has been unlocked, or there is no parent </summary>
///     
		public virtual bool canUnlockAchievement(Achievement p_77442_1_)
		{
			return p_77442_1_.parentAchievement == null || this.hasAchievementUnlocked(p_77442_1_.parentAchievement);
		}

		public virtual int func_150874_c(Achievement p_150874_1_)
		{
			if(this.hasAchievementUnlocked(p_150874_1_))
			{
				return 0;
			}
			else
			{
				int var2 = 0;

				for(Achievement var3 = p_150874_1_.parentAchievement; var3 != null && !this.hasAchievementUnlocked(var3); ++var2)
				{
					var3 = var3.parentAchievement;
				}

				return var2;
			}
		}

		public virtual void func_150871_b(EntityPlayer p_150871_1_, StatBase p_150871_2_, int p_150871_3_)
		{
			if(!p_150871_2_.Achievement || this.canUnlockAchievement((Achievement)p_150871_2_))
			{
				this.func_150873_a(p_150871_1_, p_150871_2_, this.writeStat(p_150871_2_) + p_150871_3_);
			}
		}

		public virtual void func_150873_a(EntityPlayer p_150873_1_, StatBase p_150873_2_, int p_150873_3_)
		{
			TupleIntJsonSerializable var4 = (TupleIntJsonSerializable)this.field_150875_a.get(p_150873_2_);

			if(var4 == null)
			{
				var4 = new TupleIntJsonSerializable();
				this.field_150875_a.Add(p_150873_2_, var4);
			}

			var4.IntegerValue = p_150873_3_;
		}

		public virtual int writeStat(StatBase p_77444_1_)
		{
			TupleIntJsonSerializable var2 = (TupleIntJsonSerializable)this.field_150875_a.get(p_77444_1_);
			return var2 == null ? 0 : var2.IntegerValue;
		}

		public virtual IJsonSerializable func_150870_b(StatBase p_150870_1_)
		{
			TupleIntJsonSerializable var2 = (TupleIntJsonSerializable)this.field_150875_a.get(p_150870_1_);
			return var2 != null ? var2.JsonSerializableValue : null;
		}

		public virtual IJsonSerializable func_150872_a(StatBase p_150872_1_, IJsonSerializable p_150872_2_)
		{
			TupleIntJsonSerializable var3 = (TupleIntJsonSerializable)this.field_150875_a.get(p_150872_1_);

			if(var3 == null)
			{
				var3 = new TupleIntJsonSerializable();
				this.field_150875_a.Add(p_150872_1_, var3);
			}

			var3.JsonSerializableValue = p_150872_2_;
			return p_150872_2_;
		}
	}

}