using System.Collections;

namespace DotCraftCore.nUtil
{

	using ForwardingSet = com.google.common.collect.ForwardingSet;
	using Sets = com.google.common.collect.Sets;
	using JsonArray = com.google.gson.JsonArray;
	using JsonElement = com.google.gson.JsonElement;
	using JsonPrimitive = com.google.gson.JsonPrimitive;

	public class JsonSerializableSet : ForwardingSet, IJsonSerializable
	{
	/// <summary> The set for this ForwardingSet to forward methods to.  </summary>
		private readonly Set underlyingSet = Sets.newHashSet();
		

		public virtual void func_152753_a(JsonElement p_152753_1_)
		{
			if(p_152753_1_.JsonArray)
			{
				IEnumerator var2 = p_152753_1_.AsJsonArray.GetEnumerator();

				while(var2.MoveNext())
				{
					JsonElement var3 = (JsonElement)var2.Current;
					this.add(var3.AsString);
				}
			}
		}

///    
///     <summary> * Gets the JsonElement that can be serialized. </summary>
///     
		public virtual JsonElement SerializableElement
		{
			get
			{
				JsonArray var1 = new JsonArray();
				IEnumerator var2 = this.GetEnumerator();
	
				while(var2.MoveNext())
				{
					string var3 = (string)var2.Current;
					var1.add(new JsonPrimitive(var3));
				}
	
				return var1;
			}
		}

		protected internal virtual Set @delegate()
		{
			return this.underlyingSet;
		}
	}

}