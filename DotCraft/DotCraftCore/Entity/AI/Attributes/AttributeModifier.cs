using System;

namespace DotCraftCore.nEntity.nAI.nAttributes
{

	using Validate = org.apache.commons.lang3.Validate;

	public class AttributeModifier
	{
		private readonly double amount;
		private readonly int operation;
		private readonly string name;
		private readonly UUID id;

///    
///     <summary> * If false, this modifier is not saved in NBT. Used for "natural" modifiers like speed boost from sprinting </summary>
///     
		private bool isSaved;
		

		public AttributeModifier(string p_i1605_1_, double p_i1605_2_, int p_i1605_4_) : this(UUID.randomUUID(), p_i1605_1_, p_i1605_2_, p_i1605_4_)
		{
		}

		public AttributeModifier(UUID p_i1606_1_, string p_i1606_2_, double p_i1606_3_, int p_i1606_5_)
		{
			this.isSaved = true;
			this.id = p_i1606_1_;
			this.name = p_i1606_2_;
			this.amount = p_i1606_3_;
			this.operation = p_i1606_5_;
			Validate.notEmpty(p_i1606_2_, "Modifier name cannot be empty", new object[0]);
			Validate.inclusiveBetween(Convert.ToInt32(0), Convert.ToInt32(2), Convert.ToInt32(p_i1606_5_), "Invalid operation", new object[0]);
		}

		public virtual UUID ID
		{
			get
			{
				return this.id;
			}
		}

		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		public virtual int Operation
		{
			get
			{
				return this.operation;
			}
		}

		public virtual double Amount
		{
			get
			{
				return this.amount;
			}
		}

///    
///     * <seealso cref= #isSaved </seealso>
///     
		public virtual bool isSaved()
		{
			get
			{
				return this.isSaved;
			}
			set
			{
				this.isSaved = value;
				return this;
			}
		}

///    
///     * <seealso cref= #isSaved </seealso>
///     

		public override bool Equals(object p_equals_1_)
		{
			if (this == p_equals_1_)
			{
				return true;
			}
			else if (p_equals_1_ != null && this.GetType() == p_equals_1_.GetType())
			{
				AttributeModifier var2 = (AttributeModifier)p_equals_1_;

				if (this.id != null)
				{
					if (!this.id.Equals(var2.id))
					{
						return false;
					}
				}
				else if (var2.id != null)
				{
					return false;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.id != null ? this.id.GetHashCode() : 0;
		}

		public override string ToString()
		{
			return "AttributeModifier{amount=" + this.amount + ", operation=" + this.operation + ", name=\'" + this.name + '\'' + ", id=" + this.id + ", serialize=" + this.isSaved + '}';
		}
	}

}