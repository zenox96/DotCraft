namespace DotCraftCore.World.Storage
{

	public interface IThreadedFileIO
	{
///    
///     <summary> * Returns a boolean stating if the write was unsuccessful. </summary>
///     
		bool writeNextIO();
	}

}