namespace DotCraftCore.Util
{

	public interface IRegistry<T, K>
	{
		object getObject(T key);

///    
///     <summary> * Register an object on this registry. </summary>
///     
		void putObject(T key, K value);
	}

}