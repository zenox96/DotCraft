using System;
using System.Collections;

namespace DotCraftCore.nCommand.nServer
{

	using Block = DotCraftCore.nBlock.Block;
	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using CommandException = DotCraftCore.nCommand.CommandException;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using NumberInvalidException = DotCraftCore.nCommand.NumberInvalidException;
	using WrongUsageException = DotCraftCore.nCommand.WrongUsageException;
	using JsonToNBT = DotCraftCore.nNBT.JsonToNBT;
	using NBTBase = DotCraftCore.nNBT.NBTBase;
	using NBTException = DotCraftCore.nNBT.NBTException;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using TileEntity = DotCraftCore.nTileEntity.TileEntity;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class CommandTestForBlock : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "testforblock";
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
			return "commands.testforblock.usage";
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
				Block var6 = Block.getBlockFromName(p_71515_2_[3]);

				if(var6 == null)
				{
					throw new NumberInvalidException("commands.setblock.notFound", new object[] {p_71515_2_[3]});
				}
				else
				{
					int var7 = -1;

					if(p_71515_2_.Length >= 5)
					{
						var7 = parseIntBounded(p_71515_1_, p_71515_2_[4], -1, 15);
					}

					World var8 = p_71515_1_.EntityWorld;

					if(!var8.blockExists(var3, var4, var5))
					{
						throw new CommandException("commands.testforblock.outOfWorld", new object[0]);
					}
					else
					{
						NBTTagCompound var9 = new NBTTagCompound();
						bool var10 = false;

						if(p_71515_2_.Length >= 6 && var6.hasTileEntity())
						{
							string var11 = func_147178_a(p_71515_1_, p_71515_2_, 5).UnformattedText;

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
							catch (NBTException var14)
							{
								throw new CommandException("commands.setblock.tagError", new object[] {var14.Message});
							}
						}

						Block var15 = var8.getBlock(var3, var4, var5);

						if(var15 != var6)
						{
							throw new CommandException("commands.testforblock.failed.tile", new object[] {Convert.ToInt32(var3), Convert.ToInt32(var4), Convert.ToInt32(var5), var15.LocalizedName, var6.LocalizedName});
						}
						else
						{
							if(var7 > -1)
							{
								int var16 = var8.getBlockMetadata(var3, var4, var5);

								if(var16 != var7)
								{
									throw new CommandException("commands.testforblock.failed.data", new object[] {Convert.ToInt32(var3), Convert.ToInt32(var4), Convert.ToInt32(var5), Convert.ToInt32(var16), Convert.ToInt32(var7)});
								}
							}

							if(var10)
							{
								TileEntity var17 = var8.getTileEntity(var3, var4, var5);

								if(var17 == null)
								{
									throw new CommandException("commands.testforblock.failed.tileEntity", new object[] {Convert.ToInt32(var3), Convert.ToInt32(var4), Convert.ToInt32(var5)});
								}

								NBTTagCompound var13 = new NBTTagCompound();
								var17.writeToNBT(var13);

								if(!this.func_147181_a(var9, var13))
								{
									throw new CommandException("commands.testforblock.failed.nbt", new object[] {Convert.ToInt32(var3), Convert.ToInt32(var4), Convert.ToInt32(var5)});
								}
							}

							p_71515_1_.AddChatMessage(new ChatComponentTranslation("commands.testforblock.success", new object[] {Convert.ToInt32(var3), Convert.ToInt32(var4), Convert.ToInt32(var5)}));
						}
					}
				}
			}
			else
			{
				throw new WrongUsageException("commands.testforblock.usage", new object[0]);
			}
		}

		public virtual bool func_147181_a(NBTBase p_147181_1_, NBTBase p_147181_2_)
		{
			if(p_147181_1_ == p_147181_2_)
			{
				return true;
			}
			else if(p_147181_1_ == null)
			{
				return true;
			}
			else if(p_147181_2_ == null)
			{
				return false;
			}
			else if(!p_147181_1_.GetType().Equals(p_147181_2_.GetType()))
			{
				return false;
			}
			else if(p_147181_1_ is NBTTagCompound)
			{
				NBTTagCompound var3 = (NBTTagCompound)p_147181_1_;
				NBTTagCompound var4 = (NBTTagCompound)p_147181_2_;
				IEnumerator var5 = var3.func_150296_c().GetEnumerator();
				string var6;
				NBTBase var7;

				do
				{
					if(!var5.MoveNext())
					{
						return true;
					}

					var6 = (string)var5.Current;
					var7 = var3.getTag(var6);
				}
				while (this.func_147181_a(var7, var4.getTag(var6)));

				return false;
			}
			else
			{
				return p_147181_1_.Equals(p_147181_2_);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 4 ? GetListOfStringsFromIterableMatchingLastWord(p_71516_2_, Block.blockRegistry.Keys) : null;
		}
	}

}