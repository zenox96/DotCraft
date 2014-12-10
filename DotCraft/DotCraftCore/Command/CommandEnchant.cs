using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using Enchantment = DotCraftCore.enchantment.Enchantment;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;

	public class CommandEnchant : CommandBase
	{
		private const string __OBFID = "CL_00000377";

		public virtual string CommandName
		{
			get
			{
				return "enchant";
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
			return "commands.enchant.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 2)
			{
				throw new WrongUsageException("commands.enchant.usage", new object[0]);
			}
			else
			{
				EntityPlayerMP var3 = getPlayer(p_71515_1_, p_71515_2_[0]);
				int var4 = parseIntBounded(p_71515_1_, p_71515_2_[1], 0, Enchantment.enchantmentsList.length - 1);
				int var5 = 1;
				ItemStack var6 = var3.CurrentEquippedItem;

				if(var6 == null)
				{
					throw new CommandException("commands.enchant.noItem", new object[0]);
				}
				else
				{
					Enchantment var7 = Enchantment.enchantmentsList[var4];

					if(var7 == null)
					{
						throw new NumberInvalidException("commands.enchant.notFound", new object[] {Convert.ToInt32(var4)});
					}
					else if(!var7.canApply(var6))
					{
						throw new CommandException("commands.enchant.cantEnchant", new object[0]);
					}
					else
					{
						if(p_71515_2_.Length >= 3)
						{
							var5 = parseIntBounded(p_71515_1_, p_71515_2_[2], var7.MinLevel, var7.MaxLevel);
						}

						if(var6.hasTagCompound())
						{
							NBTTagList var8 = var6.EnchantmentTagList;

							if(var8 != null)
							{
								for (int var9 = 0; var9 < var8.tagCount(); ++var9)
								{
									short var10 = var8.getCompoundTagAt(var9).getShort("id");

									if(Enchantment.enchantmentsList[var10] != null)
									{
										Enchantment var11 = Enchantment.enchantmentsList[var10];

										if(!var11.canApplyTogether(var7))
										{
											throw new CommandException("commands.enchant.cantCombine", new object[] {var7.getTranslatedName(var5), var11.getTranslatedName(var8.getCompoundTagAt(var9).getShort("lvl"))});
										}
									}
								}
							}
						}

						var6.addEnchantment(var7, var5);
						func_152373_a(p_71515_1_, this, "commands.enchant.success", new object[0]);
					}
				}
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, this.ListOfPlayers) : null;
		}

		protected internal virtual string[] ListOfPlayers
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