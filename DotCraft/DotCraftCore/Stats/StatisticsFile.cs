using System;
using System.Collections;

namespace DotCraftCore.Stats
{

	using Maps = com.google.common.collect.Maps;
	using Sets = com.google.common.collect.Sets;
	using JsonElement = com.google.gson.JsonElement;
	using JsonObject = com.google.gson.JsonObject;
	using JsonParseException = com.google.gson.JsonParseException;
	using JsonParser = com.google.gson.JsonParser;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using S37PacketStatistics = DotCraftCore.network.play.server.S37PacketStatistics;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using IJsonSerializable = DotCraftCore.Util.IJsonSerializable;
	using TupleIntJsonSerializable = DotCraftCore.Util.TupleIntJsonSerializable;
	using FileUtils = org.apache.commons.io.FileUtils;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class StatisticsFile : StatFileWriter
	{
		private static readonly Logger logger = LogManager.Logger;
		private readonly MinecraftServer field_150890_c;
		private readonly File field_150887_d;
		private readonly Set field_150888_e = Sets.newHashSet();
		private int field_150885_f = -300;
		private bool field_150886_g = false;
		private const string __OBFID = "CL_00001471";

		public StatisticsFile(MinecraftServer p_i45306_1_, File p_i45306_2_)
		{
			this.field_150890_c = p_i45306_1_;
			this.field_150887_d = p_i45306_2_;
		}

		public virtual void func_150882_a()
		{
			if(this.field_150887_d.File)
			{
				try
				{
					this.field_150875_a.Clear();
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'putAll' method:
					this.field_150875_a.putAll(this.func_150881_a(FileUtils.readFileToString(this.field_150887_d)));
				}
				catch (IOException var2)
				{
					logger.error("Couldn\'t read statistics file " + this.field_150887_d, var2);
				}
				catch (JsonParseException var3)
				{
					logger.error("Couldn\'t parse statistics file " + this.field_150887_d, var3);
				}
			}
		}

		public virtual void func_150883_b()
		{
			try
			{
				FileUtils.writeStringToFile(this.field_150887_d, func_150880_a(this.field_150875_a));
			}
			catch (IOException var2)
			{
				logger.error("Couldn\'t save stats", var2);
			}
		}

		public override void func_150873_a(EntityPlayer p_150873_1_, StatBase p_150873_2_, int p_150873_3_)
		{
			int var4 = p_150873_2_.Achievement ? this.writeStat(p_150873_2_) : 0;
			base.func_150873_a(p_150873_1_, p_150873_2_, p_150873_3_);
			this.field_150888_e.add(p_150873_2_);

			if(p_150873_2_.Achievement && var4 == 0 && p_150873_3_ > 0)
			{
				this.field_150886_g = true;

				if(this.field_150890_c.func_147136_ar())
				{
					this.field_150890_c.ConfigurationManager.func_148539_a(new ChatComponentTranslation("chat.type.achievement", new object[] {p_150873_1_.func_145748_c_(), p_150873_2_.func_150955_j()}));
				}
			}
		}

		public virtual Set func_150878_c()
		{
			HashSet var1 = Sets.newHashSet(this.field_150888_e);
			this.field_150888_e.clear();
			this.field_150886_g = false;
			return var1;
		}

		public virtual IDictionary func_150881_a(string p_150881_1_)
		{
			JsonElement var2 = (new JsonParser()).parse(p_150881_1_);

			if(!var2.JsonObject)
			{
				return Maps.newHashMap();
			}
			else
			{
				JsonObject var3 = var2.AsJsonObject;
				Hashtable var4 = Maps.newHashMap();
				IEnumerator var5 = var3.entrySet().GetEnumerator();

				while(var5.MoveNext())
				{
					Entry var6 = (Entry)var5.Current;
					StatBase var7 = StatList.func_151177_a((string)var6.Key);

					if(var7 != null)
					{
						TupleIntJsonSerializable var8 = new TupleIntJsonSerializable();

						if(((JsonElement)var6.Value).JsonPrimitive && ((JsonElement)var6.Value).AsJsonPrimitive.Number)
						{
							var8.IntegerValue = ((JsonElement)var6.Value).AsInt;
						}
						else if(((JsonElement)var6.Value).JsonObject)
						{
							JsonObject var9 = ((JsonElement)var6.Value).AsJsonObject;

							if(var9.has("value") && var9.get("value").JsonPrimitive && var9.get("value").AsJsonPrimitive.Number)
							{
								var8.IntegerValue = var9.getAsJsonPrimitive("value").AsInt;
							}

							if(var9.has("progress") && var7.func_150954_l() != null)
							{
								try
								{
									Constructor var10 = var7.func_150954_l().GetConstructor(new Class[0]);
									IJsonSerializable var11 = (IJsonSerializable)var10.newInstance(new object[0]);
									var11.func_152753_a(var9.get("progress"));
									var8.JsonSerializableValue = var11;
								}
								catch (Exception var12)
								{
									logger.warn("Invalid statistic progress in " + this.field_150887_d, var12);
								}
							}
						}

						var4.Add(var7, var8);
					}
					else
					{
						logger.warn("Invalid statistic in " + this.field_150887_d + ": Don\'t know what " + (string)var6.Key + " is");
					}
				}

				return var4;
			}
		}

		public static string func_150880_a(IDictionary p_150880_0_)
		{
			JsonObject var1 = new JsonObject();
			IEnumerator var2 = p_150880_0_.GetEnumerator();

			while(var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;

				if(((TupleIntJsonSerializable)var3.Value).JsonSerializableValue != null)
				{
					JsonObject var4 = new JsonObject();
					var4.addProperty("value", Convert.ToInt32(((TupleIntJsonSerializable)var3.Value).IntegerValue));

					try
					{
						var4.add("progress", ((TupleIntJsonSerializable)var3.Value).JsonSerializableValue.SerializableElement);
					}
					catch (Exception var6)
					{
						logger.warn("Couldn\'t save statistic " + ((StatBase)var3.Key).func_150951_e() + ": error serializing progress", var6);
					}

					var1.add(((StatBase)var3.Key).statId, var4);
				}
				else
				{
					var1.addProperty(((StatBase)var3.Key).statId, Convert.ToInt32(((TupleIntJsonSerializable)var3.Value).IntegerValue));
				}
			}

			return var1.ToString();
		}

		public virtual void func_150877_d()
		{
			IEnumerator var1 = this.field_150875_a.Keys.GetEnumerator();

			while(var1.MoveNext())
			{
				StatBase var2 = (StatBase)var1.Current;
				this.field_150888_e.add(var2);
			}
		}

		public virtual void func_150876_a(EntityPlayerMP p_150876_1_)
		{
			int var2 = this.field_150890_c.TickCounter;
			Hashtable var3 = Maps.newHashMap();

			if(this.field_150886_g || var2 - this.field_150885_f > 300)
			{
				this.field_150885_f = var2;
				IEnumerator var4 = this.func_150878_c().GetEnumerator();

				while(var4.MoveNext())
				{
					StatBase var5 = (StatBase)var4.Current;
					var3.Add(var5, Convert.ToInt32(this.writeStat(var5)));
				}
			}

			p_150876_1_.playerNetServerHandler.sendPacket(new S37PacketStatistics(var3));
		}

		public virtual void func_150884_b(EntityPlayerMP p_150884_1_)
		{
			Hashtable var2 = Maps.newHashMap();
			IEnumerator var3 = AchievementList.achievementList.GetEnumerator();

			while(var3.MoveNext())
			{
				Achievement var4 = (Achievement)var3.Current;

				if(this.hasAchievementUnlocked(var4))
				{
					var2.Add(var4, Convert.ToInt32(this.writeStat(var4)));
					this.field_150888_e.remove(var4);
				}
			}

			p_150884_1_.playerNetServerHandler.sendPacket(new S37PacketStatistics(var2));
		}

		public virtual bool func_150879_e()
		{
			return this.field_150886_g;
		}
	}

}