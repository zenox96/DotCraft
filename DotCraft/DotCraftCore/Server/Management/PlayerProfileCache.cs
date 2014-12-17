using System;
using System.Collections;

namespace DotCraftCore.nServer.nManagement
{

	using Charsets = com.google.common.base.Charsets;
	using Iterators = com.google.common.collect.Iterators;
	using Lists = com.google.common.collect.Lists;
	using Maps = com.google.common.collect.Maps;
	using Files = com.google.common.io.Files;
	using Gson = com.google.gson.Gson;
	using GsonBuilder = com.google.gson.GsonBuilder;
	using JsonDeserializationContext = com.google.gson.JsonDeserializationContext;
	using JsonDeserializer = com.google.gson.JsonDeserializer;
	using JsonElement = com.google.gson.JsonElement;
	using JsonObject = com.google.gson.JsonObject;
	using JsonSerializationContext = com.google.gson.JsonSerializationContext;
	using JsonSerializer = com.google.gson.JsonSerializer;
	using Agent = com.mojang.authlib.Agent;
	using GameProfile = com.mojang.authlib.GameProfile;
	using ProfileLookupCallback = com.mojang.authlib.ProfileLookupCallback;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using IOUtils = org.apache.commons.io.IOUtils;

	public class PlayerProfileCache
	{
		public static readonly SimpleDateFormat field_152659_a = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss Z");
		private readonly IDictionary field_152661_c = Maps.newHashMap();
		private readonly IDictionary field_152662_d = Maps.newHashMap();
		private readonly LinkedList field_152663_e = Lists.newLinkedList();
		private readonly MinecraftServer field_152664_f;
		protected internal readonly Gson field_152660_b;
		private readonly File field_152665_g;
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private static final ParameterizedType field_152666_h = new ParameterizedType()
//	{
//		
//		public Type[] getActualTypeArguments()
//		{
//			return new Type[] {PlayerProfileCache.ProfileEntry.class};
//		}
//		public Type getRawType()
//		{
//			return List.class;
//		}
//		public Type getOwnerType()
//		{
//			return null;
//		}
//	};
		

		public PlayerProfileCache(MinecraftServer p_i1171_1_, File p_i1171_2_)
		{
			this.field_152664_f = p_i1171_1_;
			this.field_152665_g = p_i1171_2_;
			GsonBuilder var3 = new GsonBuilder();
			var3.registerTypeHierarchyAdapter(typeof(PlayerProfileCache.ProfileEntry), new PlayerProfileCache.Serializer(null));
			this.field_152660_b = var3.create();
			this.func_152657_b();
		}

		private static GameProfile func_152650_a(MinecraftServer p_152650_0_, string p_152650_1_)
		{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final GameProfile[] var2 = new GameProfile[1];
			GameProfile[] var2 = new GameProfile[1];
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//			ProfileLookupCallback var3 = new ProfileLookupCallback()
//		{
//			
//			public void onProfileLookupSucceeded(GameProfile p_onProfileLookupSucceeded_1_)
//			{
//				var2[0] = p_onProfileLookupSucceeded_1_;
//			}
//			public void onProfileLookupFailed(GameProfile p_onProfileLookupFailed_1_, Exception p_onProfileLookupFailed_2_)
//			{
//				var2[0] = null;
//			}
//		};
			p_152650_0_.func_152359_aw().findProfilesByNames(new string[] {p_152650_1_}, Agent.MINECRAFT, var3);

			if(!p_152650_0_.ServerInOnlineMode && var2[0] == null)
			{
				UUID var4 = EntityPlayer.func_146094_a(new GameProfile((UUID)null, p_152650_1_));
				GameProfile var5 = new GameProfile(var4, p_152650_1_);
				var3.onProfileLookupSucceeded(var5);
			}

			return var2[0];
		}

		public virtual void func_152649_a(GameProfile p_152649_1_)
		{
			this.func_152651_a(p_152649_1_, (DateTime)null);
		}

		private void func_152651_a(GameProfile p_152651_1_, DateTime p_152651_2_)
		{
			UUID var3 = p_152651_1_.Id;

			if(p_152651_2_ == null)
			{
				Calendar var4 = Calendar.Instance;
				var4.Time = DateTime.Now;
				var4.add(2, 1);
				p_152651_2_ = var4.Time;
			}

			string var10 = p_152651_1_.Name.ToLower(Locale.ROOT);
			PlayerProfileCache.ProfileEntry var5 = new PlayerProfileCache.ProfileEntry(p_152651_1_, p_152651_2_, null);
			LinkedList var6 = this.field_152663_e;

			lock (this.field_152663_e)
			{
				if(this.field_152662_d.ContainsKey(var3))
				{
					PlayerProfileCache.ProfileEntry var7 = (PlayerProfileCache.ProfileEntry)this.field_152662_d.get(var3);
					this.field_152661_c.Remove(var7.func_152668_a().Name.ToLower(Locale.ROOT));
					this.field_152661_c.Add(p_152651_1_.Name.ToLower(Locale.ROOT), var5);
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET LinkedList equivalent to the Java 'remove' method:
					this.field_152663_e.remove(p_152651_1_);
				}
				else
				{
					this.field_152662_d.Add(var3, var5);
					this.field_152661_c.Add(var10, var5);
				}

				this.field_152663_e.AddFirst(p_152651_1_);
			}
		}

		public virtual GameProfile func_152655_a(string p_152655_1_)
		{
			string var2 = p_152655_1_.ToLower(Locale.ROOT);
			PlayerProfileCache.ProfileEntry var3 = (PlayerProfileCache.ProfileEntry)this.field_152661_c.get(var2);

			if(var3 != null && (DateTime.Now).Time >= var3.field_152673_c.Time)
			{
				this.field_152662_d.Remove(var3.func_152668_a().Id);
				this.field_152661_c.Remove(var3.func_152668_a().Name.ToLower(Locale.ROOT));
				LinkedList var4 = this.field_152663_e;

				lock (this.field_152663_e)
				{
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET LinkedList equivalent to the Java 'remove' method:
					this.field_152663_e.remove(var3.func_152668_a());
				}

				var3 = null;
			}

			GameProfile var9;

			if(var3 != null)
			{
				var9 = var3.func_152668_a();
				LinkedList var5 = this.field_152663_e;

				lock (this.field_152663_e)
				{
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET LinkedList equivalent to the Java 'remove' method:
					this.field_152663_e.remove(var9);
					this.field_152663_e.AddFirst(var9);
				}
			}
			else
			{
				var9 = func_152650_a(this.field_152664_f, var2);

				if(var9 != null)
				{
					this.func_152649_a(var9);
					var3 = (PlayerProfileCache.ProfileEntry)this.field_152661_c.get(var2);
				}
			}

			this.func_152658_c();
			return var3 == null ? null : var3.func_152668_a();
		}

		public virtual string[] func_152654_a()
		{
			ArrayList var1 = Lists.newArrayList(this.field_152661_c.Keys);
			return(string[])var1.ToArray();
		}

		public virtual GameProfile func_152652_a(UUID p_152652_1_)
		{
			PlayerProfileCache.ProfileEntry var2 = (PlayerProfileCache.ProfileEntry)this.field_152662_d.get(p_152652_1_);
			return var2 == null ? null : var2.func_152668_a();
		}

		private PlayerProfileCache.ProfileEntry func_152653_b(UUID p_152653_1_)
		{
			PlayerProfileCache.ProfileEntry var2 = (PlayerProfileCache.ProfileEntry)this.field_152662_d.get(p_152653_1_);

			if(var2 != null)
			{
				GameProfile var3 = var2.func_152668_a();
				LinkedList var4 = this.field_152663_e;

				lock (this.field_152663_e)
				{
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET LinkedList equivalent to the Java 'remove' method:
					this.field_152663_e.remove(var3);
					this.field_152663_e.AddFirst(var3);
				}
			}

			return var2;
		}

		public virtual void func_152657_b()
		{
			IList var1 = null;
			BufferedReader var2 = null;
			label81:
			{
				try
				{
					var2 = Files.newReader(this.field_152665_g, Charsets.UTF_8);
					var1 = (IList)this.field_152660_b.fromJson(var2, field_152666_h);
					goto label81;
				}
				catch (FileNotFoundException var10)
				{
					;
				}
				finally
				{
					IOUtils.closeQuietly(var2);
				}

				return;
			}

			if(var1 != null)
			{
				this.field_152661_c.Clear();
				this.field_152662_d.Clear();
				LinkedList var3 = this.field_152663_e;

				lock (this.field_152663_e)
				{
					this.field_152663_e.Clear();
				}

				var1 = Lists.reverse(var1);
				IEnumerator var12 = var1.GetEnumerator();

				while(var12.MoveNext())
				{
					PlayerProfileCache.ProfileEntry var4 = (PlayerProfileCache.ProfileEntry)var12.Current;

					if(var4 != null)
					{
						this.func_152651_a(var4.func_152668_a(), var4.func_152670_b());
					}
				}
			}
		}

		public virtual void func_152658_c()
		{
			string var1 = this.field_152660_b.toJson(this.func_152656_a(1000));
			BufferedWriter var2 = null;

			try
			{
				var2 = Files.newWriter(this.field_152665_g, Charsets.UTF_8);
				var2.write(var1);
				return;
			}
			catch (FileNotFoundException var8)
			{
				;
			}
			catch (IOException var9)
			{
				return;
			}
			finally
			{
				IOUtils.closeQuietly(var2);
			}
		}

		private IList func_152656_a(int p_152656_1_)
		{
			ArrayList var2 = Lists.newArrayList();
			LinkedList var4 = this.field_152663_e;
			ArrayList var3;

			lock (this.field_152663_e)
			{
				var3 = Lists.newArrayList(Iterators.limit(this.field_152663_e.GetEnumerator(), p_152656_1_));
			}

			IEnumerator var8 = var3.GetEnumerator();

			while(var8.MoveNext())
			{
				GameProfile var5 = (GameProfile)var8.Current;
				PlayerProfileCache.ProfileEntry var6 = this.func_152653_b(var5.Id);

				if(var6 != null)
				{
					var2.Add(var6);
				}
			}

			return var2;
		}

		internal class ProfileEntry
		{
			private readonly GameProfile field_152672_b;
			private readonly DateTime field_152673_c;
			

			private ProfileEntry(GameProfile p_i1165_2_, DateTime p_i1165_3_)
			{
				this.field_152672_b = p_i1165_2_;
				this.field_152673_c = p_i1165_3_;
			}

			public virtual GameProfile func_152668_a()
			{
				return this.field_152672_b;
			}

			public virtual DateTime func_152670_b()
			{
				return this.field_152673_c;
			}

			internal ProfileEntry(GameProfile p_i1166_2_, DateTime p_i1166_3_, object p_i1166_4_) : this(p_i1166_2_, p_i1166_3_)
			{
			}
		}

		internal class Serializer : JsonDeserializer, JsonSerializer
		{
			

			private Serializer()
			{
			}

			public virtual JsonElement func_152676_a(PlayerProfileCache.ProfileEntry p_152676_1_, Type p_152676_2_, JsonSerializationContext p_152676_3_)
			{
				JsonObject var4 = new JsonObject();
				var4.addProperty("name", p_152676_1_.func_152668_a().Name);
				UUID var5 = p_152676_1_.func_152668_a().Id;
				var4.addProperty("uuid", var5 == null ? "" : var5.ToString());
				var4.addProperty("expiresOn", PlayerProfileCache.field_152659_a.format(p_152676_1_.func_152670_b()));
				return var4;
			}

			public virtual PlayerProfileCache.ProfileEntry func_152675_a(JsonElement p_152675_1_, Type p_152675_2_, JsonDeserializationContext p_152675_3_)
			{
				if(p_152675_1_.JsonObject)
				{
					JsonObject var4 = p_152675_1_.AsJsonObject;
					JsonElement var5 = var4.get("name");
					JsonElement var6 = var4.get("uuid");
					JsonElement var7 = var4.get("expiresOn");

					if(var5 != null && var6 != null)
					{
						string var8 = var6.AsString;
						string var9 = var5.AsString;
						DateTime var10 = null;

						if(var7 != null)
						{
							try
							{
								var10 = PlayerProfileCache.field_152659_a.parse(var7.AsString);
							}
							catch (ParseException var14)
							{
								var10 = null;
							}
						}

						if(var9 != null && var8 != null)
						{
							UUID var11;

							try
							{
								var11 = UUID.fromString(var8);
							}
							catch (Exception var13)
							{
								return null;
							}

							PlayerProfileCache.ProfileEntry var12 = PlayerProfileCache.new ProfileEntry(new GameProfile(var11, var9), var10, null);
							return var12;
						}
						else
						{
							return null;
						}
					}
					else
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}

			public virtual JsonElement serialize(object p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
			{
				return this.func_152676_a((PlayerProfileCache.ProfileEntry)p_serialize_1_, p_serialize_2_, p_serialize_3_);
			}

			public virtual object deserialize(JsonElement p_deserialize_1_, Type p_deserialize_2_, JsonDeserializationContext p_deserialize_3_)
			{
				return this.func_152675_a(p_deserialize_1_, p_deserialize_2_, p_deserialize_3_);
			}

			internal Serializer(object p_i1163_2_) : this()
			{
			}
		}
	}

}