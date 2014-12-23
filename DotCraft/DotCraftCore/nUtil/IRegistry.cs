using System.Collections.Generic;
namespace DotCraftCore.nUtil
{
	public interface IRegistry<T, K>
	{
		public K GetObject(T key);
		public void putObject(T key, K value);
        public bool containsKey(T key);

        public ICollection<T> Keys
        {
            get;
        }
	}
}