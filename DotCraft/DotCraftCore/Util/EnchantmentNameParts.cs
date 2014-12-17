using System;

namespace DotCraftCore.nUtil
{


	public class EnchantmentNameParts
	{
		public static readonly EnchantmentNameParts instance = new EnchantmentNameParts();
		private Random rand = new Random();
		private string[] namePartsArray = "the elder scrolls klaatu berata niktu xyzzy bless curse light darkness fire air earth water hot dry cold wet ignite snuff embiggen twist shorten stretch fiddle destroy imbue galvanize enchant free limited range of towards inside sphere cube self other ball mental physical grow shrink demon elemental spirit animal creature beast humanoid undead fresh stale ".split(" ");
		

///    
///     <summary> * Randomly generates a new name built up of 3 or 4 randomly selected words. </summary>
///     
		public virtual string generateNewRandomName()
		{
			int var1 = this.rand.Next(2) + 3;
			string var2 = "";

			for(int var3 = 0; var3 < var1; ++var3)
			{
				if(var3 > 0)
				{
					var2 = var2 + " ";
				}

				var2 = var2 + this.namePartsArray[this.rand.Next(this.namePartsArray.Length)];
			}

			return var2;
		}

///    
///     <summary> * Resets the underlying random number generator using a given seed. </summary>
///     
		public virtual void reseedRandomGenerator(long p_148335_1_)
		{
			this.rand.Seed = p_148335_1_;
		}
	}

}