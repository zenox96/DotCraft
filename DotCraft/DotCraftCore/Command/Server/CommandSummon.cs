using System.Collections;

namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using Entity = DotCraftCore.entity.Entity;
	using EntityList = DotCraftCore.entity.EntityList;
	using EntityLiving = DotCraftCore.entity.EntityLiving;
	using IEntityLivingData = DotCraftCore.entity.IEntityLivingData;
	using JsonToNBT = DotCraftCore.nbt.JsonToNBT;
	using NBTBase = DotCraftCore.nbt.NBTBase;
	using NBTException = DotCraftCore.nbt.NBTException;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using IChatComponent = DotCraftCore.util.IChatComponent;
	using World = DotCraftCore.world.World;

	public class CommandSummon : CommandBase
	{
		private const string __OBFID = "CL_00001158";

		public virtual string CommandName
		{
			get
			{
				return "summon";
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
			return "commands.summon.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 1)
			{
				throw new WrongUsageException("commands.summon.usage", new object[0]);
			}
			else
			{
				string var3 = p_71515_2_[0];
				double var4 = (double)p_71515_1_.PlayerCoordinates.posX + 0.5D;
				double var6 = (double)p_71515_1_.PlayerCoordinates.posY;
				double var8 = (double)p_71515_1_.PlayerCoordinates.posZ + 0.5D;

				if(p_71515_2_.Length >= 4)
				{
					var4 = func_110666_a(p_71515_1_, var4, p_71515_2_[1]);
					var6 = func_110666_a(p_71515_1_, var6, p_71515_2_[2]);
					var8 = func_110666_a(p_71515_1_, var8, p_71515_2_[3]);
				}

				World var10 = p_71515_1_.EntityWorld;

				if(!var10.blockExists((int)var4, (int)var6, (int)var8))
				{
					func_152373_a(p_71515_1_, this, "commands.summon.outOfWorld", new object[0]);
				}
				else
				{
					NBTTagCompound var11 = new NBTTagCompound();
					bool var12 = false;

					if(p_71515_2_.Length >= 5)
					{
						IChatComponent var13 = func_147178_a(p_71515_1_, p_71515_2_, 4);

						try
						{
							NBTBase var14 = JsonToNBT.func_150315_a(var13.UnformattedText);

							if(!(var14 is NBTTagCompound))
							{
								func_152373_a(p_71515_1_, this, "commands.summon.tagError", new object[] {"Not a valid tag"});
								return;
							}

							var11 = (NBTTagCompound)var14;
							var12 = true;
						}
						catch (NBTException var17)
						{
							func_152373_a(p_71515_1_, this, "commands.summon.tagError", new object[] {var17.Message});
							return;
						}
					}

					var11.setString("id", var3);
					Entity var18 = EntityList.createEntityFromNBT(var11, var10);

					if(var18 == null)
					{
						func_152373_a(p_71515_1_, this, "commands.summon.failed", new object[0]);
					}
					else
					{
						var18.setLocationAndAngles(var4, var6, var8, var18.rotationYaw, var18.rotationPitch);

						if(!var12 && var18 is EntityLiving)
						{
							((EntityLiving)var18).onSpawnWithEgg((IEntityLivingData)null);
						}

						var10.spawnEntityInWorld(var18);
						Entity var19 = var18;

						for (NBTTagCompound var15 = var11; var19 != null && var15.func_150297_b("Riding", 10); var15 = var15.getCompoundTag("Riding"))
						{
							Entity var16 = EntityList.createEntityFromNBT(var15.getCompoundTag("Riding"), var10);

							if(var16 != null)
							{
								var16.setLocationAndAngles(var4, var6, var8, var16.rotationYaw, var16.rotationPitch);
								var10.spawnEntityInWorld(var16);
								var19.mountEntity(var16);
							}

							var19 = var16;
						}

						func_152373_a(p_71515_1_, this, "commands.summon.success", new object[0]);
					}
				}
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, this.func_147182_d()) : null;
		}

		protected internal virtual string[] func_147182_d()
		{
			return (string[])EntityList.func_151515_b().ToArray(new string[0]);
		}
	}

}