using System;

namespace DotCraftCore.nStats
{

	using Block = DotCraftCore.nBlock.Block;
	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.nUtil.EnumChatFormatting;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;
	using StatCollector = DotCraftCore.nUtil.StatCollector;

	public class Achievement : StatBase
	{
///    
///     <summary> * Is the column (related to center of achievement gui, in 24 pixels unit) that the achievement will be displayed. </summary>
///     
		public readonly int displayColumn;

///    
///     <summary> * Is the row (related to center of achievement gui, in 24 pixels unit) that the achievement will be displayed. </summary>
///     
		public readonly int displayRow;

///    
///     <summary> * Holds the parent achievement, that must be taken before this achievement is avaiable. </summary>
///     
		public readonly Achievement parentAchievement;

///    
///     <summary> * Holds the description of the achievement, ready to be formatted and/or displayed. </summary>
///     
		private readonly string achievementDescription;

///    
///     <summary> * Holds a string formatter for the achievement, some of then needs extra dynamic info - like the key used to open
///     * the inventory. </summary>
///     
		private IStatStringFormat statStringFormatter;

///    
///     <summary> * Holds the ItemStack that will be used to draw the achievement into the GUI. </summary>
///     
		public readonly ItemStack theItemStack;

///    
///     <summary> * Special achievements have a 'spiked' (on normal texture pack) frame, special achievements are the hardest ones to
///     * achieve. </summary>
///     
		private bool isSpecial;
		

		public Achievement(string p_i45300_1_, string p_i45300_2_, int p_i45300_3_, int p_i45300_4_, Item p_i45300_5_, Achievement p_i45300_6_) : this(p_i45300_1_, p_i45300_2_, p_i45300_3_, p_i45300_4_, new ItemStack(p_i45300_5_), p_i45300_6_)
		{
		}

		public Achievement(string p_i45301_1_, string p_i45301_2_, int p_i45301_3_, int p_i45301_4_, Block p_i45301_5_, Achievement p_i45301_6_) : this(p_i45301_1_, p_i45301_2_, p_i45301_3_, p_i45301_4_, new ItemStack(p_i45301_5_), p_i45301_6_)
		{
		}

		public Achievement(string p_i45302_1_, string p_i45302_2_, int p_i45302_3_, int p_i45302_4_, ItemStack p_i45302_5_, Achievement p_i45302_6_) : base(p_i45302_1_, new ChatComponentTranslation("achievement." + p_i45302_2_, new object[0]))
		{
			this.theItemStack = p_i45302_5_;
			this.achievementDescription = "achievement." + p_i45302_2_ + ".desc";
			this.displayColumn = p_i45302_3_;
			this.displayRow = p_i45302_4_;

			if(p_i45302_3_ < AchievementList.minDisplayColumn)
			{
				AchievementList.minDisplayColumn = p_i45302_3_;
			}

			if(p_i45302_4_ < AchievementList.minDisplayRow)
			{
				AchievementList.minDisplayRow = p_i45302_4_;
			}

			if(p_i45302_3_ > AchievementList.maxDisplayColumn)
			{
				AchievementList.maxDisplayColumn = p_i45302_3_;
			}

			if(p_i45302_4_ > AchievementList.maxDisplayRow)
			{
				AchievementList.maxDisplayRow = p_i45302_4_;
			}

			this.parentAchievement = p_i45302_6_;
		}

///    
///     <summary> * Initializes the current stat as independent (i.e., lacking prerequisites for being updated) and returns the
///     * current instance. </summary>
///     
		public override Achievement initIndependentStat()
		{
			this.isIndependent = true;
			return this;
		}

///    
///     <summary> * Special achievements have a 'spiked' (on normal texture pack) frame, special achievements are the hardest ones to
///     * achieve. </summary>
///     
		public virtual Achievement setSpecial()
		{
			this.isSpecial = true;
			return this;
		}

///    
///     <summary> * Register the stat into StatList. </summary>
///     
		public override Achievement registerStat()
		{
			base.registerStat();
			AchievementList.achievementList.Add(this);
			return this;
		}

///    
///     <summary> * Returns whether or not the StatBase-derived class is a statistic (running counter) or an achievement (one-shot). </summary>
///     
		public override bool isAchievement()
		{
			get
			{
				return true;
			}
		}

		public override IChatComponent func_150951_e()
		{
			IChatComponent var1 = base.func_150951_e();
			var1.ChatStyle.Color = this.Special ? EnumChatFormatting.DARK_PURPLE : EnumChatFormatting.GREEN;
			return var1;
		}

		public override Achievement func_150953_b(Type p_150953_1_)
		{
			return(Achievement)base.func_150953_b(p_150953_1_);
		}

///    
///     <summary> * Returns the fully description of the achievement - ready to be displayed on screen. </summary>
///     
		public virtual string Description
		{
			get
			{
				return this.statStringFormatter != null ? this.statStringFormatter.formatString(StatCollector.translateToLocal(this.achievementDescription)) : StatCollector.translateToLocal(this.achievementDescription);
			}
		}

///    
///     <summary> * Defines a string formatter for the achievement. </summary>
///     
		public virtual Achievement StatStringFormatter
		{
			set
			{
				this.statStringFormatter = value;
				return this;
			}
		}

///    
///     <summary> * Special achievements have a 'spiked' (on normal texture pack) frame, special achievements are the hardest ones to
///     * achieve. </summary>
///     
		public virtual bool Special
		{
			get
			{
				return this.isSpecial;
			}
		}
	}

}