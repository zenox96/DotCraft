using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ChunkCoordinates = DotCraftCore.util.ChunkCoordinates;
	using IChatComponent = DotCraftCore.util.IChatComponent;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;
	using WorldSettings = DotCraftCore.world.WorldSettings;

	public class PlayerSelector
	{
///    
///     <summary> * This matches the at-tokens introduced for command blocks, including their arguments, if any. </summary>
///     
		private static readonly Pattern tokenPattern = Pattern.compile("^@([parf])(?:\\[([\\w=,!-]*)\\])?$");

///    
///     <summary> * This matches things like "-1,,4", and is used for getting x,y,z,range from the token's argument list. </summary>
///     
		private static readonly Pattern intListPattern = Pattern.compile("\\G([-!]?[\\w-]*)(?:$|,)");

///    
///     <summary> * This matches things like "rm=4,c=2" and is used for handling named token arguments. </summary>
///     
		private static readonly Pattern keyValueListPattern = Pattern.compile("\\G(\\w+)=([-!]?[\\w-]*)(?:$|,)");
		private const string __OBFID = "CL_00000086";

///    
///     <summary> * Returns the one player that matches the given at-token.  Returns null if more than one player matches. </summary>
///     
		public static EntityPlayerMP matchOnePlayer(ICommandSender p_82386_0_, string p_82386_1_)
		{
			EntityPlayerMP[] var2 = matchPlayers(p_82386_0_, p_82386_1_);
			return var2 != null && var2.Length == 1 ? var2[0] : null;
		}

		public static IChatComponent func_150869_b(ICommandSender p_150869_0_, string p_150869_1_)
		{
			EntityPlayerMP[] var2 = matchPlayers(p_150869_0_, p_150869_1_);

			if(var2 != null && var2.Length != 0)
			{
				IChatComponent[] var3 = new IChatComponent[var2.Length];

				for (int var4 = 0; var4 < var3.Length; ++var4)
				{
					var3[var4] = var2[var4].func_145748_c_();
				}

				return CommandBase.joinNiceString(var3);
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Returns an array of all players matched by the given at-token. </summary>
///     
		public static EntityPlayerMP[] matchPlayers(ICommandSender p_82380_0_, string p_82380_1_)
		{
			Matcher var2 = tokenPattern.matcher(p_82380_1_);

			if(var2.matches())
			{
				IDictionary var3 = getArgumentMap(var2.group(2));
				string var4 = var2.group(1);
				int var5 = getDefaultMinimumRange(var4);
				int var6 = getDefaultMaximumRange(var4);
				int var7 = getDefaultMinimumLevel(var4);
				int var8 = getDefaultMaximumLevel(var4);
				int var9 = getDefaultCount(var4);
				int var10 = WorldSettings.GameType.NOT_SET.ID;
				ChunkCoordinates var11 = p_82380_0_.PlayerCoordinates;
				IDictionary var12 = func_96560_a(var3);
				string var13 = null;
				string var14 = null;
				bool var15 = false;

				if(var3.ContainsKey("rm"))
				{
					var5 = MathHelper.parseIntWithDefault((string)var3["rm"], var5);
					var15 = true;
				}

				if(var3.ContainsKey("r"))
				{
					var6 = MathHelper.parseIntWithDefault((string)var3["r"], var6);
					var15 = true;
				}

				if(var3.ContainsKey("lm"))
				{
					var7 = MathHelper.parseIntWithDefault((string)var3["lm"], var7);
				}

				if(var3.ContainsKey("l"))
				{
					var8 = MathHelper.parseIntWithDefault((string)var3["l"], var8);
				}

				if(var3.ContainsKey("x"))
				{
					var11.posX = MathHelper.parseIntWithDefault((string)var3["x"], var11.posX);
					var15 = true;
				}

				if(var3.ContainsKey("y"))
				{
					var11.posY = MathHelper.parseIntWithDefault((string)var3["y"], var11.posY);
					var15 = true;
				}

				if(var3.ContainsKey("z"))
				{
					var11.posZ = MathHelper.parseIntWithDefault((string)var3["z"], var11.posZ);
					var15 = true;
				}

				if(var3.ContainsKey("m"))
				{
					var10 = MathHelper.parseIntWithDefault((string)var3["m"], var10);
				}

				if(var3.ContainsKey("c"))
				{
					var9 = MathHelper.parseIntWithDefault((string)var3["c"], var9);
				}

				if(var3.ContainsKey("team"))
				{
					var14 = (string)var3["team"];
				}

				if(var3.ContainsKey("name"))
				{
					var13 = (string)var3["name"];
				}

				World var16 = var15 ? p_82380_0_.EntityWorld : null;
				IList var17;

				if(!var4.Equals("p") && !var4.Equals("a"))
				{
					if(var4.Equals("r"))
					{
						var17 = MinecraftServer.Server.ConfigurationManager.findPlayers(var11, var5, var6, 0, var10, var7, var8, var12, var13, var14, var16);
						Collections.shuffle(var17);
						var17 = var17.subList(0, Math.Min(var9, var17.Count));
						return var17.Count == 0 ? new EntityPlayerMP[0] : (EntityPlayerMP[])var17.ToArray();
					}
					else
					{
						return null;
					}
				}
				else
				{
					var17 = MinecraftServer.Server.ConfigurationManager.findPlayers(var11, var5, var6, var9, var10, var7, var8, var12, var13, var14, var16);
					return var17.Count == 0 ? new EntityPlayerMP[0] : (EntityPlayerMP[])var17.ToArray();
				}
			}
			else
			{
				return null;
			}
		}

		public static IDictionary func_96560_a(IDictionary p_96560_0_)
		{
			Hashtable var1 = new Hashtable();
			IEnumerator var2 = p_96560_0_.Keys.GetEnumerator();

			while (var2.MoveNext())
			{
				string var3 = (string)var2.Current;

				if(var3.StartsWith("score_") && var3.Length > "score_".Length)
				{
					string var4 = var3.Substring("score_".Length);
					var1.Add(var4, Convert.ToInt32(MathHelper.parseIntWithDefault((string)p_96560_0_[var3], 1)));
				}
			}

			return var1;
		}

///    
///     <summary> * Returns whether the given pattern can match more than one player. </summary>
///     
		public static bool matchesMultiplePlayers(string p_82377_0_)
		{
			Matcher var1 = tokenPattern.matcher(p_82377_0_);

			if(var1.matches())
			{
				IDictionary var2 = getArgumentMap(var1.group(2));
				string var3 = var1.group(1);
				int var4 = getDefaultCount(var3);

				if(var2.ContainsKey("c"))
				{
					var4 = MathHelper.parseIntWithDefault((string)var2["c"], var4);
				}

				return var4 != 1;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Returns whether the given token (parameter 1) has exactly the given arguments (parameter 2). </summary>
///     
		public static bool hasTheseArguments(string p_82383_0_, string p_82383_1_)
		{
			Matcher var2 = tokenPattern.matcher(p_82383_0_);

			if(var2.matches())
			{
				string var3 = var2.group(1);
				return p_82383_1_ == null || p_82383_1_.Equals(var3);
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Returns whether the given token has any arguments set. </summary>
///     
		public static bool hasArguments(string p_82378_0_)
		{
			return hasTheseArguments(p_82378_0_, (string)null);
		}

///    
///     <summary> * Gets the default minimum range (argument rm). </summary>
///     
		private static int getDefaultMinimumRange(string p_82384_0_)
		{
			return 0;
		}

///    
///     <summary> * Gets the default maximum range (argument r). </summary>
///     
		private static int getDefaultMaximumRange(string p_82379_0_)
		{
			return 0;
		}

///    
///     <summary> * Gets the default maximum experience level (argument l) </summary>
///     
		private static int getDefaultMaximumLevel(string p_82376_0_)
		{
			return int.MaxValue;
		}

///    
///     <summary> * Gets the default minimum experience level (argument lm) </summary>
///     
		private static int getDefaultMinimumLevel(string p_82375_0_)
		{
			return 0;
		}

///    
///     <summary> * Gets the default number of players to return (argument c, 0 for infinite) </summary>
///     
		private static int getDefaultCount(string p_82382_0_)
		{
			return p_82382_0_.Equals("a") ? 0 : 1;
		}

///    
///     <summary> * Parses the given argument string, turning it into a HashMap&lt;String, String&gt; of name-&gt;value. </summary>
///     
		private static IDictionary getArgumentMap(string p_82381_0_)
		{
			Hashtable var1 = new Hashtable();

			if(p_82381_0_ == null)
			{
				return var1;
			}
			else
			{
				Matcher var2 = intListPattern.matcher(p_82381_0_);
				int var3 = 0;
				int var4;

				for (var4 = -1; var2.find(); var4 = var2.end())
				{
					string var5 = null;

					switch (var3++)
					{
						case 0:
							var5 = "x";
							break;

						case 1:
							var5 = "y";
							break;

						case 2:
							var5 = "z";
							break;

						case 3:
							var5 = "r";
						break;
					}

					if(var5 != null && var2.group(1).Length > 0)
					{
						var1.Add(var5, var2.group(1));
					}
				}

				if(var4 < p_82381_0_.Length)
				{
					var2 = keyValueListPattern.matcher(var4 == -1 ? p_82381_0_ : p_82381_0_.Substring(var4));

					while (var2.find())
					{
						var1.Add(var2.group(1), var2.group(2));
					}
				}

				return var1;
			}
		}
	}

}