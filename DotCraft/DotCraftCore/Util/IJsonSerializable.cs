namespace DotCraftCore.Util
{

	using JsonElement = com.google.gson.JsonElement;

	public interface IJsonSerializable
	{
		void func_152753_a(JsonElement p_152753_1_);

///    
///     <summary> * Gets the JsonElement that can be serialized. </summary>
///     
		JsonElement SerializableElement {get;}
	}

}