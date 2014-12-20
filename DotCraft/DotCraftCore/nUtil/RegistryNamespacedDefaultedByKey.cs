using DotCraftCore.nBlock;
namespace DotCraftCore.nUtil
{

	public class RegistryNamespacedDefaultedByKey : RegistryNamespaced<string, Block>
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
		public override void addObject(int intID, string stringID, Block obj)
		{
			if(this.defualtKey.Equals(stringID))
			{
				this.defaultValue = obj;
			}

			base.addObject(intID, stringID, obj);
		}

		public override Block getObject(string stringKey)
		{
			Block var2 = base.getObject(stringKey);
			return var2 == null ? this.defaultValue : var2;
		}

///    
///     <summary> * Gets the object identified by the given ID. </summary>
///     
		public override Block getObjectForID(int intKey)
		{
			Block var2 = base.getObjectForID(intKey);
			return var2 == null ? this.defaultValue : var2;
		}

		public override Block getObject(object objKey)
		{
			return this.getObject((string)objKey);
		}
	}
}