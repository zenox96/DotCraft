namespace DotCraftCore.Server.Integrated
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ServerConfigurationManager = DotCraftCore.Server.Management.ServerConfigurationManager;

	public class IntegratedPlayerList : ServerConfigurationManager
	{
///    
///     <summary> * Holds the NBT data for the host player's save file, so this can be written to level.dat. </summary>
///     
		private NBTTagCompound hostPlayerData;
		private const string __OBFID = "CL_00001128";

		public IntegratedPlayerList(IntegratedServer p_i1314_1_) : base(p_i1314_1_)
		{
			this.func_152611_a(10);
		}

///    
///     <summary> * also stores the NBTTags if this is an intergratedPlayerList </summary>
///     
		protected internal override void writePlayerData(EntityPlayerMP p_72391_1_)
		{
			if(p_72391_1_.CommandSenderName.Equals(this.ServerInstance.ServerOwner))
			{
				this.hostPlayerData = new NBTTagCompound();
				p_72391_1_.writeToNBT(this.hostPlayerData);
			}

			base.writePlayerData(p_72391_1_);
		}

		public override string func_148542_a(SocketAddress p_148542_1_, GameProfile p_148542_2_)
		{
			return p_148542_2_.Name.equalsIgnoreCase(this.ServerInstance.ServerOwner) && this.func_152612_a(p_148542_2_.Name) != null ? "That name is already taken." : base.func_148542_a(p_148542_1_, p_148542_2_);
		}

		public override IntegratedServer ServerInstance
		{
			get
			{
				return(IntegratedServer)base.ServerInstance;
			}
		}

///    
///     <summary> * On integrated servers, returns the host's player data to be written to level.dat. </summary>
///     
		public override NBTTagCompound HostPlayerData
		{
			get
			{
				return this.hostPlayerData;
			}
		}
	}

}