using System.Collections;

namespace DotCraftCore.Realms
{

	using AnvilConverterException = DotCraftCore.client.AnvilConverterException;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;
	using ISaveFormat = DotCraftCore.World.Storage.ISaveFormat;
	using SaveFormatComparator = DotCraftCore.World.Storage.SaveFormatComparator;

	public class RealmsAnvilLevelStorageSource
	{
		private ISaveFormat levelStorageSource;
		

		public RealmsAnvilLevelStorageSource(ISaveFormat p_i1106_1_)
		{
			this.levelStorageSource = p_i1106_1_;
		}

		public virtual string Name
		{
			get
			{
				return this.levelStorageSource.func_154333_a();
			}
		}

		public virtual bool levelExists(string p_levelExists_1_)
		{
			return this.levelStorageSource.canLoadWorld(p_levelExists_1_);
		}

		public virtual bool convertLevel(string p_convertLevel_1_, IProgressUpdate p_convertLevel_2_)
		{
			return this.levelStorageSource.convertMapFormat(p_convertLevel_1_, p_convertLevel_2_);
		}

		public virtual bool requiresConversion(string p_requiresConversion_1_)
		{
			return this.levelStorageSource.isOldMapFormat(p_requiresConversion_1_);
		}

		public virtual bool isNewLevelIdAcceptable(string p_isNewLevelIdAcceptable_1_)
		{
			return this.levelStorageSource.func_154335_d(p_isNewLevelIdAcceptable_1_);
		}

		public virtual bool deleteLevel(string p_deleteLevel_1_)
		{
			return this.levelStorageSource.deleteWorldDirectory(p_deleteLevel_1_);
		}

		public virtual bool isConvertible(string p_isConvertible_1_)
		{
			return this.levelStorageSource.func_154334_a(p_isConvertible_1_);
		}

		public virtual void renameLevel(string p_renameLevel_1_, string p_renameLevel_2_)
		{
			this.levelStorageSource.renameWorld(p_renameLevel_1_, p_renameLevel_2_);
		}

		public virtual void clearAll()
		{
			this.levelStorageSource.flushCache();
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public List getLevelList() throws AnvilConverterException
		public virtual IList LevelList
		{
			get
			{
				ArrayList var1 = new ArrayList();
				IEnumerator var2 = this.levelStorageSource.SaveList.GetEnumerator();
	
				while (var2.MoveNext())
				{
					SaveFormatComparator var3 = (SaveFormatComparator)var2.Current;
					var1.Add(new RealmsLevelSummary(var3));
				}
	
				return var1;
			}
		}
	}

}