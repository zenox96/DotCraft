namespace DotCraftCore.Util
{

	public class TupleIntJsonSerializable
	{
		private int integerValue;
		private IJsonSerializable jsonSerializableValue;
		private const string __OBFID = "CL_00001478";

///    
///     <summary> * Gets the integer value stored in this tuple. </summary>
///     
		public virtual int IntegerValue
		{
			get
			{
				return this.integerValue;
			}
			set
			{
				this.integerValue = value;
			}
		}

///    
///     <summary> * Sets this tuple's integer value to the given value. </summary>
///     

///    
///     <summary> * Gets the JsonSerializable value stored in this tuple. </summary>
///     
		public virtual IJsonSerializable JsonSerializableValue
		{
			get
			{
				return this.jsonSerializableValue;
			}
			set
			{
				this.jsonSerializableValue = value;
			}
		}

///    
///     <summary> * Sets this tuple's JsonSerializable value to the given value. </summary>
///     
	}

}