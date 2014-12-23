using DotCraftCore.nBlock;
namespace DotCraftCore.nUtil
{
	public class RegistryNamespacedDefaultedByKey : RegistryNamespaced<Block>
	{
		private readonly string defualtKey;
		private Block defaultValue;
		

		public RegistryNamespacedDefaultedByKey(string keyDefault)
		{
			this.defualtKey = keyDefault;
		}

///    
///     <summary> * Adds a new object to this registry, keyed by both the given integer ID and the given string. </summary>
///     
		public override void AddObject(int intID, string stringID, Block obj)
		{
			if(this.defualtKey.Equals(stringID))
			{
				this.defaultValue = obj;
			}

			base.AddObject(intID, stringID, obj);
		}

		public override Block GetObject(string stringKey)
		{
			Block var2 = base.GetObject(stringKey);
			return var2 == null ? this.defaultValue : var2;
		}

///    
///     <summary> * Gets the object identified by the given ID. </summary>
///     
		public override Block GetObjectForID(int intKey)
		{
			Block var2 = base.GetObjectForID(intKey);
			return var2 == null ? this.defaultValue : var2;
		}
	}
}