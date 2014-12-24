using System;

namespace DotCraftCore.nCommand.nServer
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using ICommandManager = DotCraftCore.nCommand.ICommandManager;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentText = DotCraftCore.nUtil.ChatComponentText;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;
	using World = DotCraftCore.nWorld.World;

	public abstract class CommandBlockLogic : ICommandSender
	{
		private static readonly SimpleDateFormat field_145766_a = new SimpleDateFormat("HH:mm:ss");
		private int field_145764_b;
		private bool field_145765_c = true;
		private IChatComponent field_145762_d = null;
		private string field_145763_e = "";
		private string field_145761_f = "@";
		

		public virtual int func_145760_g()
		{
			return this.field_145764_b;
		}

		public virtual IChatComponent func_145749_h()
		{
			return this.field_145762_d;
		}

		public virtual void func_145758_a(NBTTagCompound p_145758_1_)
		{
			p_145758_1_.setString("Command", this.field_145763_e);
			p_145758_1_.setInteger("SuccessCount", this.field_145764_b);
			p_145758_1_.setString("CustomName", this.field_145761_f);

			if(this.field_145762_d != null)
			{
				p_145758_1_.setString("LastOutput", IChatComponent.Serializer.func_150696_a(this.field_145762_d));
			}

			p_145758_1_.setBoolean("TrackOutput", this.field_145765_c);
		}

		public virtual void func_145759_b(NBTTagCompound p_145759_1_)
		{
			this.field_145763_e = p_145759_1_.getString("Command");
			this.field_145764_b = p_145759_1_.getInteger("SuccessCount");

			if(p_145759_1_.func_150297_b("CustomName", 8))
			{
				this.field_145761_f = p_145759_1_.getString("CustomName");
			}

			if(p_145759_1_.func_150297_b("LastOutput", 8))
			{
				this.field_145762_d = IChatComponent.Serializer.func_150699_a(p_145759_1_.getString("LastOutput"));
			}

			if(p_145759_1_.func_150297_b("TrackOutput", 1))
			{
				this.field_145765_c = p_145759_1_.getBoolean("TrackOutput");
			}
		}

///    
///     <summary> * Returns true if the command sender is allowed to use the given command. </summary>
///     
		public virtual bool CanCommandSenderUseCommand(int p_70003_1_, string p_70003_2_)
		{
			return p_70003_1_ <= 2;
		}

		public virtual void func_145752_a(string p_145752_1_)
		{
			this.field_145763_e = p_145752_1_;
		}

		public virtual string func_145753_i()
		{
			return this.field_145763_e;
		}

		public virtual void func_145755_a(World p_145755_1_)
		{
			if(p_145755_1_.isClient)
			{
				this.field_145764_b = 0;
			}

			MinecraftServer var2 = MinecraftServer.Server;

			if(var2 != null && var2.CommandBlockEnabled)
			{
				ICommandManager var3 = var2.CommandManager;
				this.field_145764_b = var3.executeCommand(this, this.field_145763_e);
			}
			else
			{
				this.field_145764_b = 0;
			}
		}

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public virtual string CommandSenderName
		{
			get
			{
				return this.field_145761_f;
			}
		}

		public virtual IChatComponent func_145748_c_()
		{
			return new ChatComponentText(this.CommandSenderName);
		}

		public virtual void func_145754_b(string p_145754_1_)
		{
			this.field_145761_f = p_145754_1_;
		}

///    
///     <summary> * Notifies this sender of some sort of information.  This is for messages intended to display to the user.  Used
///     * for typical output (like "you asked for whether or not this game rule is set, so here's your answer"), warnings
///     * (like "I fetched this block for you by ID, but I'd like you to know that every time you do this, I die a little
///     * inside"), and errors (like "it's not called iron_pixacke, silly"). </summary>
///     
		public virtual void AddChatMessage(IChatComponent p_145747_1_)
		{
			if(this.field_145765_c && this.EntityWorld != null && !this.EntityWorld.isClient)
			{
				this.field_145762_d = (new ChatComponentText("[" + field_145766_a.format(DateTime.Now) + "] ")).appendSibling(p_145747_1_);
				this.func_145756_e();
			}
		}

		public abstract void func_145756_e();

		public abstract int func_145751_f();

		public abstract void func_145757_a(ByteBuf p_145757_1_);

		public virtual void func_145750_b(IChatComponent p_145750_1_)
		{
			this.field_145762_d = p_145750_1_;
		}
	}

}