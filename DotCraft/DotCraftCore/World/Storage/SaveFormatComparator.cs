namespace DotCraftCore.World.Storage
{

	using WorldSettings = DotCraftCore.World.WorldSettings;

	public class SaveFormatComparator : Comparable
	{
	/// <summary> the file name of this save  </summary>
		private readonly string fileName;

	/// <summary> the displayed name of this save file  </summary>
		private readonly string displayName;
		private readonly long lastTimePlayed;
		private readonly long sizeOnDisk;
		private readonly bool requiresConversion;

	/// <summary> Instance of EnumGameType.  </summary>
		private readonly WorldSettings.GameType theEnumGameType;
		private readonly bool hardcore;
		private readonly bool cheatsEnabled;
		

		public SaveFormatComparator(string p_i2161_1_, string p_i2161_2_, long p_i2161_3_, long p_i2161_5_, WorldSettings.GameType p_i2161_7_, bool p_i2161_8_, bool p_i2161_9_, bool p_i2161_10_)
		{
			this.fileName = p_i2161_1_;
			this.displayName = p_i2161_2_;
			this.lastTimePlayed = p_i2161_3_;
			this.sizeOnDisk = p_i2161_5_;
			this.theEnumGameType = p_i2161_7_;
			this.requiresConversion = p_i2161_8_;
			this.hardcore = p_i2161_9_;
			this.cheatsEnabled = p_i2161_10_;
		}

///    
///     <summary> * return the file name </summary>
///     
		public virtual string FileName
		{
			get
			{
				return this.fileName;
			}
		}

///    
///     <summary> * return the display name of the save </summary>
///     
		public virtual string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		public virtual long func_154336_c()
		{
			return this.sizeOnDisk;
		}

		public virtual bool requiresConversion()
		{
			return this.requiresConversion;
		}

		public virtual long LastTimePlayed
		{
			get
			{
				return this.lastTimePlayed;
			}
		}

		public virtual int compareTo(SaveFormatComparator p_compareTo_1_)
		{
			return this.lastTimePlayed < p_compareTo_1_.lastTimePlayed ? 1 : (this.lastTimePlayed > p_compareTo_1_.lastTimePlayed ? -1 : this.fileName.CompareTo(p_compareTo_1_.fileName));
		}

///    
///     <summary> * Gets the EnumGameType. </summary>
///     
		public virtual WorldSettings.GameType EnumGameType
		{
			get
			{
				return this.theEnumGameType;
			}
		}

		public virtual bool isHardcoreModeEnabled()
		{
			get
			{
				return this.hardcore;
			}
		}

///    
///     * <returns> {@code true} if cheats are enabled for this world </returns>
///     
		public virtual bool CheatsEnabled
		{
			get
			{
				return this.cheatsEnabled;
			}
		}

		public virtual int compareTo(object p_compareTo_1_)
		{
			return this.CompareTo((SaveFormatComparator)p_compareTo_1_);
		}
	}

}