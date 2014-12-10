using System;

namespace DotCraftCore.Util
{

	using JsonArray = com.google.gson.JsonArray;
	using JsonElement = com.google.gson.JsonElement;
	using JsonObject = com.google.gson.JsonObject;
	using JsonPrimitive = com.google.gson.JsonPrimitive;
	using JsonSyntaxException = com.google.gson.JsonSyntaxException;

	public class JsonUtils
	{
		private const string __OBFID = "CL_00001484";

///    
///     <summary> * Does the given JsonObject contain a string field with the given name? </summary>
///     
		public static bool jsonObjectFieldTypeIsString(JsonObject p_151205_0_, string p_151205_1_)
		{
			return !jsonObjectFieldTypeIsPrimitive(p_151205_0_, p_151205_1_) ? false : p_151205_0_.getAsJsonPrimitive(p_151205_1_).String;
		}

///    
///     <summary> * Is the given JsonElement a string? </summary>
///     
		public static bool jsonElementTypeIsString(JsonElement p_151211_0_)
		{
			return !p_151211_0_.JsonPrimitive ? false : p_151211_0_.AsJsonPrimitive.String;
		}

///    
///     <summary> * Does the given JsonObject contain an array field with the given name? </summary>
///     
		public static bool jsonObjectFieldTypeIsArray(JsonObject p_151202_0_, string p_151202_1_)
		{
			return !jsonObjectHasNamedField(p_151202_0_, p_151202_1_) ? false : p_151202_0_.get(p_151202_1_).JsonArray;
		}

///    
///     <summary> * Does the given JsonObject contain a field with the given name whose type is primitive (String, Java primitive, or
///     * Java primitive wrapper)? </summary>
///     
		public static bool jsonObjectFieldTypeIsPrimitive(JsonObject p_151201_0_, string p_151201_1_)
		{
			return !jsonObjectHasNamedField(p_151201_0_, p_151201_1_) ? false : p_151201_0_.get(p_151201_1_).JsonPrimitive;
		}

///    
///     <summary> * Does the given JsonObject contain a field with the given name? </summary>
///     
		public static bool jsonObjectHasNamedField(JsonObject p_151204_0_, string p_151204_1_)
		{
			return p_151204_0_ == null ? false : p_151204_0_.get(p_151204_1_) != null;
		}

///    
///     <summary> * Gets the string value of the given JsonElement.  Expects the second parameter to be the name of the element's
///     * field if an error message needs to be thrown. </summary>
///     
		public static string getJsonElementStringValue(JsonElement p_151206_0_, string p_151206_1_)
		{
			if(p_151206_0_.JsonPrimitive)
			{
				return p_151206_0_.AsString;
			}
			else
			{
				throw new JsonSyntaxException("Expected " + p_151206_1_ + " to be a string, was " + getJsonElementTypeDescription(p_151206_0_));
			}
		}

///    
///     <summary> * Gets the string value of the field on the JsonObject with the given name. </summary>
///     
		public static string getJsonObjectStringFieldValue(JsonObject p_151200_0_, string p_151200_1_)
		{
			if(p_151200_0_.has(p_151200_1_))
			{
				return getJsonElementStringValue(p_151200_0_.get(p_151200_1_), p_151200_1_);
			}
			else
			{
				throw new JsonSyntaxException("Missing " + p_151200_1_ + ", expected to find a string");
			}
		}

///    
///     <summary> * Gets the string value of the field on the JsonObject with the given name, or the given default value if the field
///     * is missing. </summary>
///     
		public static string getJsonObjectStringFieldValueOrDefault(JsonObject p_151219_0_, string p_151219_1_, string p_151219_2_)
		{
			return p_151219_0_.has(p_151219_1_) ? getJsonElementStringValue(p_151219_0_.get(p_151219_1_), p_151219_1_) : p_151219_2_;
		}

///    
///     <summary> * Gets the boolean value of the given JsonElement.  Expects the second parameter to be the name of the element's
///     * field if an error message needs to be thrown. </summary>
///     
		public static bool getJsonElementBooleanValue(JsonElement p_151216_0_, string p_151216_1_)
		{
			if(p_151216_0_.JsonPrimitive)
			{
				return p_151216_0_.AsBoolean;
			}
			else
			{
				throw new JsonSyntaxException("Expected " + p_151216_1_ + " to be a Boolean, was " + getJsonElementTypeDescription(p_151216_0_));
			}
		}

///    
///     <summary> * Gets the boolean value of the field on the JsonObject with the given name. </summary>
///     
		public static bool getJsonObjectBooleanFieldValue(JsonObject p_151212_0_, string p_151212_1_)
		{
			if(p_151212_0_.has(p_151212_1_))
			{
				return getJsonElementBooleanValue(p_151212_0_.get(p_151212_1_), p_151212_1_);
			}
			else
			{
				throw new JsonSyntaxException("Missing " + p_151212_1_ + ", expected to find a Boolean");
			}
		}

///    
///     <summary> * Gets the boolean value of the field on the JsonObject with the given name, or the given default value if the
///     * field is missing. </summary>
///     
		public static bool getJsonObjectBooleanFieldValueOrDefault(JsonObject p_151209_0_, string p_151209_1_, bool p_151209_2_)
		{
			return p_151209_0_.has(p_151209_1_) ? getJsonElementBooleanValue(p_151209_0_.get(p_151209_1_), p_151209_1_) : p_151209_2_;
		}

///    
///     <summary> * Gets the float value of the given JsonElement.  Expects the second parameter to be the name of the element's
///     * field if an error message needs to be thrown. </summary>
///     
		public static float getJsonElementFloatValue(JsonElement p_151220_0_, string p_151220_1_)
		{
			if(p_151220_0_.JsonPrimitive && p_151220_0_.AsJsonPrimitive.Number)
			{
				return p_151220_0_.AsFloat;
			}
			else
			{
				throw new JsonSyntaxException("Expected " + p_151220_1_ + " to be a Float, was " + getJsonElementTypeDescription(p_151220_0_));
			}
		}

///    
///     <summary> * Gets the float value of the field on the JsonObject with the given name. </summary>
///     
		public static float getJsonObjectFloatFieldValue(JsonObject p_151217_0_, string p_151217_1_)
		{
			if(p_151217_0_.has(p_151217_1_))
			{
				return getJsonElementFloatValue(p_151217_0_.get(p_151217_1_), p_151217_1_);
			}
			else
			{
				throw new JsonSyntaxException("Missing " + p_151217_1_ + ", expected to find a Float");
			}
		}

///    
///     <summary> * Gets the float value of the field on the JsonObject with the given name, or the given default value if the field
///     * is missing. </summary>
///     
		public static float getJsonObjectFloatFieldValueOrDefault(JsonObject p_151221_0_, string p_151221_1_, float p_151221_2_)
		{
			return p_151221_0_.has(p_151221_1_) ? getJsonElementFloatValue(p_151221_0_.get(p_151221_1_), p_151221_1_) : p_151221_2_;
		}

///    
///     <summary> * Gets the integer value of the given JsonElement.  Expects the second parameter to be the name of the element's
///     * field if an error message needs to be thrown. </summary>
///     
		public static int getJsonElementIntegerValue(JsonElement p_151215_0_, string p_151215_1_)
		{
			if(p_151215_0_.JsonPrimitive && p_151215_0_.AsJsonPrimitive.Number)
			{
				return p_151215_0_.AsInt;
			}
			else
			{
				throw new JsonSyntaxException("Expected " + p_151215_1_ + " to be a Int, was " + getJsonElementTypeDescription(p_151215_0_));
			}
		}

///    
///     <summary> * Gets the integer value of the field on the JsonObject with the given name. </summary>
///     
		public static int getJsonObjectIntegerFieldValue(JsonObject p_151203_0_, string p_151203_1_)
		{
			if(p_151203_0_.has(p_151203_1_))
			{
				return getJsonElementIntegerValue(p_151203_0_.get(p_151203_1_), p_151203_1_);
			}
			else
			{
				throw new JsonSyntaxException("Missing " + p_151203_1_ + ", expected to find a Int");
			}
		}

///    
///     <summary> * Gets the integer value of the field on the JsonObject with the given name, or the given default value if the
///     * field is missing. </summary>
///     
		public static int getJsonObjectIntegerFieldValueOrDefault(JsonObject p_151208_0_, string p_151208_1_, int p_151208_2_)
		{
			return p_151208_0_.has(p_151208_1_) ? getJsonElementIntegerValue(p_151208_0_.get(p_151208_1_), p_151208_1_) : p_151208_2_;
		}

///    
///     <summary> * Gets the given JsonElement as a JsonObject.  Expects the second parameter to be the name of the element's field
///     * if an error message needs to be thrown. </summary>
///     
		public static JsonObject getJsonElementAsJsonObject(JsonElement p_151210_0_, string p_151210_1_)
		{
			if(p_151210_0_.JsonObject)
			{
				return p_151210_0_.AsJsonObject;
			}
			else
			{
				throw new JsonSyntaxException("Expected " + p_151210_1_ + " to be a JsonObject, was " + getJsonElementTypeDescription(p_151210_0_));
			}
		}

		public static JsonObject func_152754_s(JsonObject p_152754_0_, string p_152754_1_)
		{
			if(p_152754_0_.has(p_152754_1_))
			{
				return getJsonElementAsJsonObject(p_152754_0_.get(p_152754_1_), p_152754_1_);
			}
			else
			{
				throw new JsonSyntaxException("Missing " + p_152754_1_ + ", expected to find a JsonObject");
			}
		}

///    
///     <summary> * Gets the JsonObject field on the JsonObject with the given name, or the given default value if the field is
///     * missing. </summary>
///     
		public static JsonObject getJsonObjectFieldOrDefault(JsonObject p_151218_0_, string p_151218_1_, JsonObject p_151218_2_)
		{
			return p_151218_0_.has(p_151218_1_) ? getJsonElementAsJsonObject(p_151218_0_.get(p_151218_1_), p_151218_1_) : p_151218_2_;
		}

///    
///     <summary> * Gets the given JsonElement as a JsonArray.  Expects the second parameter to be the name of the element's field if
///     * an error message needs to be thrown. </summary>
///     
		public static JsonArray getJsonElementAsJsonArray(JsonElement p_151207_0_, string p_151207_1_)
		{
			if(p_151207_0_.JsonArray)
			{
				return p_151207_0_.AsJsonArray;
			}
			else
			{
				throw new JsonSyntaxException("Expected " + p_151207_1_ + " to be a JsonArray, was " + getJsonElementTypeDescription(p_151207_0_));
			}
		}

///    
///     <summary> * Gets the JsonArray field on the JsonObject with the given name. </summary>
///     
		public static JsonArray getJsonObjectJsonArrayField(JsonObject p_151214_0_, string p_151214_1_)
		{
			if(p_151214_0_.has(p_151214_1_))
			{
				return getJsonElementAsJsonArray(p_151214_0_.get(p_151214_1_), p_151214_1_);
			}
			else
			{
				throw new JsonSyntaxException("Missing " + p_151214_1_ + ", expected to find a JsonArray");
			}
		}

///    
///     <summary> * Gets the JsonArray field on the JsonObject with the given name, or the given default value if the field is
///     * missing. </summary>
///     
		public static JsonArray getJsonObjectJsonArrayFieldOrDefault(JsonObject p_151213_0_, string p_151213_1_, JsonArray p_151213_2_)
		{
			return p_151213_0_.has(p_151213_1_) ? getJsonElementAsJsonArray(p_151213_0_.get(p_151213_1_), p_151213_1_) : p_151213_2_;
		}

///    
///     <summary> * Gets a human-readable description of the given JsonElement's type.  For example: "a number (4)" </summary>
///     
		public static string getJsonElementTypeDescription(JsonElement p_151222_0_)
		{
			string var1 = org.apache.commons.lang3.StringUtils.abbreviateMiddle(Convert.ToString(p_151222_0_), "...", 10);

			if(p_151222_0_ == null)
			{
				return "null (missing)";
			}
			else if(p_151222_0_.JsonNull)
			{
				return "null (json)";
			}
			else if(p_151222_0_.JsonArray)
			{
				return "an array (" + var1 + ")";
			}
			else if(p_151222_0_.JsonObject)
			{
				return "an object (" + var1 + ")";
			}
			else
			{
				if(p_151222_0_.JsonPrimitive)
				{
					JsonPrimitive var2 = p_151222_0_.AsJsonPrimitive;

					if(var2.Number)
					{
						return "a number (" + var1 + ")";
					}

					if(var2.Boolean)
					{
						return "a boolean (" + var1 + ")";
					}
				}

				return var1;
			}
		}
	}

}