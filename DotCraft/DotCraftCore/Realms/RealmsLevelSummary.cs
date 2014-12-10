namespace DotCraftCore.Realms
{

	using SaveFormatComparator = DotCraftCore.World.Storage.SaveFormatComparator;

	public class RealmsLevelSummary : Comparable
	{
		private SaveFormatComparator levelSummary;
		private const string __OBFID = "CL_00001857";

		public RealmsLevelSummary(SaveFormatComparator p_i1109_1_)
		{
			this.levelSummary = p_i1109_1_;
		}

		public virtual int GameMode
		{
			get
			{
				return this.levelSummary.EnumGameType.ID;
			}
		}

		public virtual string LevelId
		{
			get
			{
				return this.levelSummary.FileName;
			}
		}

		public virtual bool hasCheats()
		{
			return this.levelSummary.CheatsEnabled;
		}

		public virtual bool isHardcore()
		{
			get
			{
				return this.levelSummary.HardcoreModeEnabled;
			}
		}

		public virtual bool isRequiresConversion()
		{
			get
			{
				return this.levelSummary.requiresConversion();
			}
		}

		public virtual string LevelName
		{
			get
			{
				return this.levelSummary.DisplayName;
			}
		}

		public virtual long LastPlayed
		{
			get
			{
				return this.levelSummary.LastTimePlayed;
			}
		}

		public virtual int compareTo(SaveFormatComparator p_compareTo_1_)
		{
			return this.levelSummary.CompareTo(p_compareTo_1_);
		}

		public virtual long SizeOnDisk
		{
			get
			{
				return this.levelSummary.func_154336_c();
			}
		}

		public virtual int compareTo(RealmsLevelSummary p_compareTo_1_)
		{
			return this.levelSummary.LastTimePlayed < p_compareTo_1_.LastPlayed ? 1 : (this.levelSummary.LastTimePlayed > p_compareTo_1_.LastPlayed ? -1 : this.levelSummary.FileName.CompareTo(p_compareTo_1_.LevelId));
		}

		public virtual int compareTo(object p_compareTo_1_)
		{
			return this.CompareTo((RealmsLevelSummary)p_compareTo_1_);
		}
	}

}