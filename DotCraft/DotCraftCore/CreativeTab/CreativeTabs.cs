using System.Collections;

namespace DotCraftCore.CreativeTab
{
	using Enchantment = DotCraftCore.Enchantment.Enchantment;
	using EnchantmentData = DotCraftCore.Enchantment.EnchantmentData;
	using EnumEnchantmentType = DotCraftCore.Enchantment.EnumEnchantmentType;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;

    public partial class CreativeTabs
    {
        public static readonly CreativeTabs[] creativeTabArray = new CreativeTabs[12];

        public static readonly CreativeTabs tabBlock = new CreativeTabs(0, "buildingBlocks", new TabIconItemDelegate(() => Item.getItemFromBlock(Blocks.brick_block)));
        public static readonly CreativeTabs tabBlock = new CreativeTabs(0, "buildingBlocks", new TabIconItemDelegate(() => Item.getItemFromBlock(Blocks.brick_block)));
        //JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
        //		public static final CreativeTabs tabDecorations = new CreativeTabs(1, "decorations")
        //	{
        //		
        //		public Item getTabIconItem()
        //		{
        //			return Item.getItemFromBlock(Blocks.double_plant);
        //		}
        //		public int func_151243_f()
        //		{
        //			return 5;
        //		}
        //	};
        //JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
        //		public static final CreativeTabs tabRedstone = new CreativeTabs(2, "redstone")
        //	{
        //		
        //		public Item getTabIconItem()
        //		{
        //			return Items.redstone;
        //		}
        //	};
        //JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
        //		public static final CreativeTabs tabTransport = new CreativeTabs(3, "transportation")
        //	{
        //		
        //		public Item getTabIconItem()
        //		{
        //			return Item.getItemFromBlock(Blocks.golden_rail);
        //		}
        //	};
        /*public static readonly CreativeTabs tabMisc = (new CreativeTabs(4, "misc") {  public Item TabIconItem { return Items.lava_bucket; } }).func_111229_a(new EnumEnchantmentType[] {EnumEnchantmentType.all});
        public static readonly CreativeTabs tabAllSearch = (new CreativeTabs(5, "search") {  public Item TabIconItem { return Items.compass; } }).BackgroundImageName = "item_search.png";
        //JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
        //		public static final CreativeTabs tabFood = new CreativeTabs(6, "food")
        //	{
        //		
        //		public Item getTabIconItem()
        //		{
        //			return Items.apple;
        //		}
        //	};
                public static readonly CreativeTabs tabTools = (new CreativeTabs(7, "tools") {  public Item TabIconItem { return Items.iron_axe; } }).func_111229_a(new EnumEnchantmentType[] {EnumEnchantmentType.digger, EnumEnchantmentType.fishing_rod, EnumEnchantmentType.breakable});
                public static readonly CreativeTabs tabCombat = (new CreativeTabs(8, "combat") {  public Item TabIconItem { return Items.golden_sword; } }).func_111229_a(new EnumEnchantmentType[] {EnumEnchantmentType.armor, EnumEnchantmentType.armor_feet, EnumEnchantmentType.armor_head, EnumEnchantmentType.armor_legs, EnumEnchantmentType.armor_torso, EnumEnchantmentType.bow, EnumEnchantmentType.weapon});
        //JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
        //		public static final CreativeTabs tabBrewing = new CreativeTabs(9, "brewing")
        //	{
        //		
        //		public Item getTabIconItem()
        //		{
        //			return Items.potionitem;
        //		}
        //	};
        //JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
        //		public static final CreativeTabs tabMaterials = new CreativeTabs(10, "materials")
        //	{
        //		
        //		public Item getTabIconItem()
        //		{
        //			return Items.stick;
        //		}
        //	};
        public static readonly CreativeTabs tabInventory = (new CreativeTabs(11, "inventory") {  public Item TabIconItem { return Item.getItemFromBlock(Blocks.chest); } }).setBackgroundImageName("inventory.png").setNoScrollbar().setNoTitle();*/
    }

	public partial class CreativeTabs
	{
		private readonly int tabIndex;
		private readonly string tabLabel;
        private readonly TabIconItemDelegate tabIconDel;
        private readonly IconBlockDamageDelegate iconDamageDel;

	/// <summary> Texture to use.  </summary>
		private string backgroundImageName = "items.png";
		private bool hasScrollbar = true;

	/// <summary> Whether to draw the title in the foreground of the creative GUI  </summary>
		private bool drawTitle = true;
		private EnumEnchantmentType[] field_111230_s;
		private ItemStack field_151245_t;

        public delegate Item TabIconItemDelegate();
        public delegate int IconBlockDamageDelegate();

		public CreativeTabs(int index, string label, TabIconItemDelegate tabIconItemDel)
		{
			this.tabIndex = index;
			this.tabLabel = label;
			creativeTabArray[index] = this;
            this.tabIconDel = tabIconItemDel;
            this.iconDamageDel = new IconBlockDamageDelegate(() => 0);
		}

        public CreativeTabs(int index, string label, TabIconItemDelegate tabIconIconDel, IconBlockDamageDelegate iconBlockDamageDel)
        {
            this.tabIndex = index;
            this.tabLabel = label;
            creativeTabArray[index] = this;
            this.tabIconDel = tabIconIconDel;
            this.iconDamageDel = iconBlockDamageDel;
        }

		public virtual int TabIndex
		{
			get
			{
				return this.tabIndex;
			}
		}

		public virtual string TabLabel
		{
			get
			{
				return this.tabLabel;
			}
		}

///    
///     <summary> * Gets the translated Label. </summary>
///     
		public virtual string TranslatedTabLabel
		{
			get
			{
				return "itemGroup." + this.TabLabel;
			}
		}

		public virtual ItemStack IconItemStack
		{
			get
			{
				if (this.field_151245_t == null)
				{
					this.field_151245_t = new ItemStack(TabIconItem(), 1, GetIconBlockDamage());
				}
	
				return this.field_151245_t;
			}
		}

        public virtual Item TabIconItem()
        {
            return tabIconDel();
        }

        public virtual int GetIconBlockDamage()
        {
            return iconDamageDel();
        }

		public virtual string BackgroundImageName
		{
			get
			{
				return this.backgroundImageName;
			}
			set
			{
				this.backgroundImageName = value;
			}
		}

		public virtual bool drawInForegroundOfTab()
		{
			return this.drawTitle;
		}

		public virtual CreativeTabs setNoTitle()
		{
			this.drawTitle = false;
			return this;
		}

		public virtual bool shouldHidePlayerInventory()
		{
			return this.hasScrollbar;
		}

		public virtual CreativeTabs setNoScrollbar()
		{
			this.hasScrollbar = false;
			return this;
		}

///    
///     <summary> * returns index % 6 </summary>
///     
		public virtual int TabColumn
		{
			get
			{
				return this.tabIndex % 6;
			}
		}

///    
///     <summary> * returns tabIndex < 6 </summary>
///     
		public virtual bool isTabInFirstRow
		{
			get
			{
				return this.tabIndex < 6;
			}
		}

		public virtual EnumEnchantmentType[] func_111225_m()
		{
			return this.field_111230_s;
		}

		public virtual CreativeTabs func_111229_a(params EnumEnchantmentType[] p_111229_1_)
		{
			this.field_111230_s = p_111229_1_;
			return this;
		}

		public virtual bool func_111226_a(EnumEnchantmentType p_111226_1_)
		{
			if (this.field_111230_s == null)
			{
				return false;
			}
			else
			{
				EnumEnchantmentType[] var2 = this.field_111230_s;
				int var3 = var2.Length;

				for (int var4 = 0; var4 < var3; ++var4)
				{
					EnumEnchantmentType var5 = var2[var4];

					if (var5 == p_111226_1_)
					{
						return true;
					}
				}

				return false;
			}
		}

///    
///     <summary> * only shows items which have tabToDisplayOn == this </summary>
///     
		public virtual void displayAllReleventItems(IList p_78018_1_)
		{
			IEnumerator var2 = Item.itemRegistry.GetEnumerator();

			while (var2.MoveNext())
			{
				Item var3 = (Item)var2.Current;

				if (var3 != null && var3.CreativeTab == this)
				{
					var3.getSubItems(var3, this, p_78018_1_);
				}
			}

			if (this.func_111225_m() != null)
			{
				this.addEnchantmentBooksToList(p_78018_1_, this.func_111225_m());
			}
		}

///    
///     <summary> * Adds the enchantment books from the supplied EnumEnchantmentType to the given list. </summary>
///     
		public virtual void addEnchantmentBooksToList(IList p_92116_1_, params EnumEnchantmentType[] p_92116_2_)
		{
			Enchantment[] var3 = Enchantment.enchantmentsList;
			int var4 = var3.Length;

			for (int var5 = 0; var5 < var4; ++var5)
			{
				Enchantment var6 = var3[var5];

				if (var6 != null && var6.type != null)
				{
					bool var7 = false;

					for (int var8 = 0; var8 < p_92116_2_.length && !var7; ++var8)
					{
						if (var6.type == p_92116_2_[var8])
						{
							var7 = true;
						}
					}

					if (var7)
					{
						p_92116_1_.Add(Items.enchanted_book.getEnchantedItemStack(new EnchantmentData(var6, var6.MaxLevel)));
					}
				}
			}
		}
	}

}