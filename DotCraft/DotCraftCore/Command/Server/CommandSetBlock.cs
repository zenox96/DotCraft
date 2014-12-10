using System.Collections;

namespace DotCraftCore.Command.Server
{

	using Block = DotCraftCore.block.Block;
	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using JsonToNBT = DotCraftCore.nbt.JsonToNBT;
	using NBTBase = DotCraftCore.nbt.NBTBase;
	using NBTException = DotCraftCore.nbt.NBTException;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public class CommandSetBlock : CommandBase
	{
		private const string __OBFID = "CL_00000949";

		public virtual string CommandName
		{
			get
			{
				return "setblock";
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
			return "commands.setblock.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length >= 4)
			{
				int var3 = p_71515_1_.PlayerCoordinates.posX;
				int var4 = p_71515_1_.PlayerCoordinates.posY;
				int var5 = p_71515_1_.PlayerCoordinates.posZ;
				var3 = MathHelper.floor_double(func_110666_a(p_71515_1_, (double)var3, p_71515_2_[0]));
				var4 = MathHelper.floor_double(func_110666_a(p_71515_1_, (double)var4, p_71515_2_[1]));
				var5 = MathHelper.floor_double(func_110666_a(p_71515_1_, (double)var5, p_71515_2_[2]));
				Block var6 = CommandBase.getBlockByText(p_71515_1_, p_71515_2_[3]);
				int var7 = 0;

				if(p_71515_2_.Length >= 5)
				{
					var7 = parseIntBounded(p_71515_1_, p_71515_2_[4], 0, 15);
				}

				World var8 = p_71515_1_.EntityWorld;

				if(!var8.blockExists(var3, var4, var5))
				{
					throw new CommandException("commands.setblock.outOfWorld", new object[0]);
				}
				else
				{
					NBTTagCompound var9 = new NBTTagCompound();
					bool var10 = false;

					if(p_71515_2_.Length >= 7 && var6.hasTileEntity())
					{
						string var11 = func_147178_a(p_71515_1_, p_71515_2_, 6).UnformattedText;

						try
						{
							NBTBase var12 = JsonToNBT.func_150315_a(var11);

							if(!(var12 is NBTTagCompound))
							{
								throw new CommandException("commands.setblock.tagError", new object[] {"Not a valid tag"});
							}

							var9 = (NBTTagCompound)var12;
							var10 = true;
						}
						catch (NBTException var13)
						{
							throw new CommandException("commands.setblock.tagError", new object[] {var13.Message});
						}
					}

					if(p_71515_2_.Length >= 6)
					{
						if(p_71515_2_[5].Equals("destroy"))
						{
							var8.func_147480_a(var3, var4, var5, true);
						}
						else if(p_71515_2_[5].Equals("keep") && !var8.isAirBlock(var3, var4, var5))
						{
							throw new CommandException("commands.setblock.noChange", new object[0]);
						}
					}

					if(!var8.setBlock(var3, var4, var5, var6, var7, 3))
					{
						throw new CommandException("commands.setblock.noChange", new object[0]);
					}
					else
					{
						if(var10)
						{
							TileEntity var14 = var8.getTileEntity(var3, var4, var5);

							if(var14 != null)
							{
								var9.setInteger("x", var3);
								var9.setInteger("y", var4);
								var9.setInteger("z", var5);
								var14.readFromNBT(var9);
							}
						}

						func_152373_a(p_71515_1_, this, "commands.setblock.success", new object[0]);
					}
				}
			}
			else
			{
				throw new WrongUsageException("commands.setblock.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 4 ? getListOfStringsFromIterableMatchingLastWord(p_71516_2_, Block.blockRegistry.Keys) : (p_71516_2_.Length == 6 ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"replace", "destroy", "keep"}): null);
		}
	}

}