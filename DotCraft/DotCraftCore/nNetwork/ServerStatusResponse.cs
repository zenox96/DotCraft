using System;

namespace DotCraftCore.nNetwork
{

	using JsonArray = com.google.gson.JsonArray;
	using JsonDeserializationContext = com.google.gson.JsonDeserializationContext;
	using JsonDeserializer = com.google.gson.JsonDeserializer;
	using JsonElement = com.google.gson.JsonElement;
	using JsonObject = com.google.gson.JsonObject;
	using JsonSerializationContext = com.google.gson.JsonSerializationContext;
	using JsonSerializer = com.google.gson.JsonSerializer;
	using GameProfile = com.mojang.authlib.GameProfile;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;
	using JsonUtils = DotCraftCore.nUtil.JsonUtils;

	public class ServerStatusResponse
	{
		private IChatComponent field_151326_a;
		private ServerStatusResponse.PlayerCountData field_151324_b;
		private ServerStatusResponse.MinecraftProtocolVersionIdentifier field_151325_c;
		private string field_151323_d;
		

		public virtual IChatComponent func_151317_a()
		{
			return this.field_151326_a;
		}

		public virtual void func_151315_a(IChatComponent p_151315_1_)
		{
			this.field_151326_a = p_151315_1_;
		}

		public virtual ServerStatusResponse.PlayerCountData func_151318_b()
		{
			return this.field_151324_b;
		}

		public virtual void func_151319_a(ServerStatusResponse.PlayerCountData p_151319_1_)
		{
			this.field_151324_b = p_151319_1_;
		}

		public virtual ServerStatusResponse.MinecraftProtocolVersionIdentifier func_151322_c()
		{
			return this.field_151325_c;
		}

		public virtual void func_151321_a(ServerStatusResponse.MinecraftProtocolVersionIdentifier p_151321_1_)
		{
			this.field_151325_c = p_151321_1_;
		}

		public virtual void func_151320_a(string p_151320_1_)
		{
			this.field_151323_d = p_151320_1_;
		}

		public virtual string func_151316_d()
		{
			return this.field_151323_d;
		}

		public class MinecraftProtocolVersionIdentifier
		{
			private readonly string field_151306_a;
			private readonly int field_151305_b;
			

			public MinecraftProtocolVersionIdentifier(string p_i45275_1_, int p_i45275_2_)
			{
				this.field_151306_a = p_i45275_1_;
				this.field_151305_b = p_i45275_2_;
			}

			public virtual string func_151303_a()
			{
				return this.field_151306_a;
			}

			public virtual int func_151304_b()
			{
				return this.field_151305_b;
			}

			public class Serializer : JsonDeserializer, JsonSerializer
			{
				

				public virtual ServerStatusResponse.MinecraftProtocolVersionIdentifier deserialize(JsonElement p_deserialize_1_, Type p_deserialize_2_, JsonDeserializationContext p_deserialize_3_)
				{
					JsonObject var4 = JsonUtils.getJsonElementAsJsonObject(p_deserialize_1_, "version");
					return new ServerStatusResponse.MinecraftProtocolVersionIdentifier(JsonUtils.getJsonObjectStringFieldValue(var4, "name"), JsonUtils.getJsonObjectIntegerFieldValue(var4, "protocol"));
				}

				public virtual JsonElement serialize(ServerStatusResponse.MinecraftProtocolVersionIdentifier p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
				{
					JsonObject var4 = new JsonObject();
					var4.addProperty("name", p_serialize_1_.func_151303_a());
					var4.addProperty("protocol", Convert.ToInt32(p_serialize_1_.func_151304_b()));
					return var4;
				}

				public virtual JsonElement serialize(object p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
				{
					return this.serialize((ServerStatusResponse.MinecraftProtocolVersionIdentifier)p_serialize_1_, p_serialize_2_, p_serialize_3_);
				}
			}
		}

		public class PlayerCountData
		{
			private readonly int field_151336_a;
			private readonly int field_151334_b;
			private GameProfile[] field_151335_c;
			

			public PlayerCountData(int p_i45274_1_, int p_i45274_2_)
			{
				this.field_151336_a = p_i45274_1_;
				this.field_151334_b = p_i45274_2_;
			}

			public virtual int func_151332_a()
			{
				return this.field_151336_a;
			}

			public virtual int func_151333_b()
			{
				return this.field_151334_b;
			}

			public virtual GameProfile[] func_151331_c()
			{
				return this.field_151335_c;
			}

			public virtual void func_151330_a(GameProfile[] p_151330_1_)
			{
				this.field_151335_c = p_151330_1_;
			}

			public class Serializer : JsonDeserializer, JsonSerializer
			{
				

				public virtual ServerStatusResponse.PlayerCountData deserialize(JsonElement p_deserialize_1_, Type p_deserialize_2_, JsonDeserializationContext p_deserialize_3_)
				{
					JsonObject var4 = JsonUtils.getJsonElementAsJsonObject(p_deserialize_1_, "players");
					ServerStatusResponse.PlayerCountData var5 = new ServerStatusResponse.PlayerCountData(JsonUtils.getJsonObjectIntegerFieldValue(var4, "max"), JsonUtils.getJsonObjectIntegerFieldValue(var4, "online"));

					if (JsonUtils.jsonObjectFieldTypeIsArray(var4, "sample"))
					{
						JsonArray var6 = JsonUtils.getJsonObjectJsonArrayField(var4, "sample");

						if (var6.size() > 0)
						{
							GameProfile[] var7 = new GameProfile[var6.size()];

							for (int var8 = 0; var8 < var7.Length; ++var8)
							{
								JsonObject var9 = JsonUtils.getJsonElementAsJsonObject(var6.get(var8), "player[" + var8 + "]");
								string var10 = JsonUtils.getJsonObjectStringFieldValue(var9, "id");
								var7[var8] = new GameProfile(UUID.fromString(var10), JsonUtils.getJsonObjectStringFieldValue(var9, "name"));
							}

							var5.func_151330_a(var7);
						}
					}

					return var5;
				}

				public virtual JsonElement serialize(ServerStatusResponse.PlayerCountData p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
				{
					JsonObject var4 = new JsonObject();
					var4.addProperty("max", Convert.ToInt32(p_serialize_1_.func_151332_a()));
					var4.addProperty("online", Convert.ToInt32(p_serialize_1_.func_151333_b()));

					if (p_serialize_1_.func_151331_c() != null && p_serialize_1_.func_151331_c().Length > 0)
					{
						JsonArray var5 = new JsonArray();

						for (int var6 = 0; var6 < p_serialize_1_.func_151331_c().Length; ++var6)
						{
							JsonObject var7 = new JsonObject();
							UUID var8 = p_serialize_1_.func_151331_c()[var6].Id;
							var7.addProperty("id", var8 == null ? "" : var8.ToString());
							var7.addProperty("name", p_serialize_1_.func_151331_c()[var6].Name);
							var5.add(var7);
						}

						var4.add("sample", var5);
					}

					return var4;
				}

				public virtual JsonElement serialize(object p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
				{
					return this.serialize((ServerStatusResponse.PlayerCountData)p_serialize_1_, p_serialize_2_, p_serialize_3_);
				}
			}
		}

		public class Serializer : JsonDeserializer, JsonSerializer
		{
			

			public virtual ServerStatusResponse deserialize(JsonElement p_deserialize_1_, Type p_deserialize_2_, JsonDeserializationContext p_deserialize_3_)
			{
				JsonObject var4 = JsonUtils.getJsonElementAsJsonObject(p_deserialize_1_, "status");
				ServerStatusResponse var5 = new ServerStatusResponse();

				if (var4.has("description"))
				{
					var5.func_151315_a((IChatComponent)p_deserialize_3_.deserialize(var4.get("description"), typeof(IChatComponent)));
				}

				if (var4.has("players"))
				{
					var5.func_151319_a((ServerStatusResponse.PlayerCountData)p_deserialize_3_.deserialize(var4.get("players"), typeof(ServerStatusResponse.PlayerCountData)));
				}

				if (var4.has("version"))
				{
					var5.func_151321_a((ServerStatusResponse.MinecraftProtocolVersionIdentifier)p_deserialize_3_.deserialize(var4.get("version"), typeof(ServerStatusResponse.MinecraftProtocolVersionIdentifier)));
				}

				if (var4.has("favicon"))
				{
					var5.func_151320_a(JsonUtils.getJsonObjectStringFieldValue(var4, "favicon"));
				}

				return var5;
			}

			public virtual JsonElement serialize(ServerStatusResponse p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
			{
				JsonObject var4 = new JsonObject();

				if (p_serialize_1_.func_151317_a() != null)
				{
					var4.add("description", p_serialize_3_.serialize(p_serialize_1_.func_151317_a()));
				}

				if (p_serialize_1_.func_151318_b() != null)
				{
					var4.add("players", p_serialize_3_.serialize(p_serialize_1_.func_151318_b()));
				}

				if (p_serialize_1_.func_151322_c() != null)
				{
					var4.add("version", p_serialize_3_.serialize(p_serialize_1_.func_151322_c()));
				}

				if (p_serialize_1_.func_151316_d() != null)
				{
					var4.addProperty("favicon", p_serialize_1_.func_151316_d());
				}

				return var4;
			}

			public virtual JsonElement serialize(object p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
			{
				return this.serialize((ServerStatusResponse)p_serialize_1_, p_serialize_2_, p_serialize_3_);
			}
		}
	}

}