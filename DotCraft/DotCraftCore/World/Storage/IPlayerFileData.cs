namespace DotCraftCore.World.Storage
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;

	public interface IPlayerFileData
	{
///    
///     <summary> * Writes the player data to disk from the specified PlayerEntityMP. </summary>
///     
		void writePlayerData(EntityPlayer p_75753_1_);

///    
///     <summary> * Reads the player data from disk into the specified PlayerEntityMP. </summary>
///     
		NBTTagCompound readPlayerData(EntityPlayer p_75752_1_);

///    
///     <summary> * Returns an array of usernames for which player.dat exists for. </summary>
///     
		string[] AvailablePlayerDat {get;}
	}

}