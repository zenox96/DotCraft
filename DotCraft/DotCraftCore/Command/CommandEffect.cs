using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using Potion = DotCraftCore.Potion.Potion;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;

	public class CommandEffect : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "effect";
			}
		}

///    
///     <summary> * Return the required permission level for this command. </summary>
///     
		public override int RequiredPermissionLevel
		{
			get
			{
				return 2;
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.effect.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 2)
			{
				throw new WrongUsageException("commands.effect.usage", new object[0]);
			}
			else
			{
				EntityPlayerMP var3 = getPlayer(p_71515_1_, p_71515_2_[0]);

				if(p_71515_2_[1].Equals("clear"))
				{
					if(var3.ActivePotionEffects.Empty)
					{
						throw new CommandException("commands.effect.failure.notActive.all", new object[] {var3.CommandSenderName});
					}

					var3.clearActivePotions();
					func_152373_a(p_71515_1_, this, "commands.effect.success.removed.all", new object[] {var3.CommandSenderName});
				}
				else
				{
					int var4 = parseIntWithMin(p_71515_1_, p_71515_2_[1], 1);
					int var5 = 600;
					int var6 = 30;
					int var7 = 0;

					if(var4 < 0 || var4 >= Potion.potionTypes.length || Potion.potionTypes[var4] == null)
					{
						throw new NumberInvalidException("commands.effect.notFound", new object[] {Convert.ToInt32(var4)});
					}

					if(p_71515_2_.Length >= 3)
					{
						var6 = parseIntBounded(p_71515_1_, p_71515_2_[2], 0, 1000000);

						if(Potion.potionTypes[var4].Instant)
						{
							var5 = var6;
						}
						else
						{
							var5 = var6 * 20;
						}
					}
					else if(Potion.potionTypes[var4].Instant)
					{
						var5 = 1;
					}

					if(p_71515_2_.Length >= 4)
					{
						var7 = parseIntBounded(p_71515_1_, p_71515_2_[3], 0, 255);
					}

					if(var6 == 0)
					{
						if(!var3.isPotionActive(var4))
						{
							throw new CommandException("commands.effect.failure.notActive", new object[] {new ChatComponentTranslation(Potion.potionTypes[var4].Name, new object[0]), var3.CommandSenderName});
						}

						var3.removePotionEffect(var4);
						func_152373_a(p_71515_1_, this, "commands.effect.success.removed", new object[] {new ChatComponentTranslation(Potion.potionTypes[var4].Name, new object[0]), var3.CommandSenderName});
					}
					else
					{
						PotionEffect var8 = new PotionEffect(var4, var5, var7);
						var3.addPotionEffect(var8);
						func_152373_a(p_71515_1_, this, "commands.effect.success", new object[] {new ChatComponentTranslation(var8.EffectName, new object[0]), Convert.ToInt32(var4), Convert.ToInt32(var7), var3.CommandSenderName, Convert.ToInt32(var6)});
					}
				}
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, this.AllUsernames) : null;
		}

		protected internal virtual string[] AllUsernames
		{
			get
			{
				return MinecraftServer.Server.AllUsernames;
			}
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public override bool isUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return p_82358_2_ == 0;
		}
	}

}