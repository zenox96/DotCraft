using System.Collections;

namespace DotCraftCore.nInventory
{
	using Enchantment = DotCraftCore.nEnchantment.Enchantment;
	using EnchantmentData = DotCraftCore.nEnchantment.EnchantmentData;
	using EnumEnchantmentType = DotCraftCore.nEnchantment.EnumEnchantmentType;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;

    public partial class CreativeTabs
    {
        public static readonly CreativeTabs[] creativeTabArray = new CreativeTabs[12];

        public static readonly CreativeTabs tabBlock = new CreativeTabs(0, "buildingBlocks", new TabIconItemDelegate(() => Item.getItemFromBlock(Blocks.brick_block)));
        public static readonly CreativeTabs tabDecorations = new CreativeTabs(1, "decorations", new TabIconItemDelegate(( ) => Item.getItemFromBlock(Blocks.double_plant)), new IconBlockDamageDelegate(() => 5));
        public static readonly CreativeTabs tabRedstone = new CreativeTabs(2, "redstone", new TabIconItemDelegate(( ) => Items.redstone));
        public static readonly CreativeTabs tabTransport = new CreativeTabs(3, "transportation", new TabIconItemDelegate(( ) => Item.getItemFromBlock(Blocks.golden_rail)));
        public static readonly CreativeTabs tabMisc = new CreativeTabs(4, "misc", new TabIconItemDelegate(( ) => Items.lava_bucket)).SetEnchantments(new EnumEnchantmentType[] {EnumEnchantmentType.all});
        public static readonly CreativeTabs tabAllSearch = new CreativeTabs(5, "search", new TabIconItemDelegate(( ) => Items.compass)).SetBackgroundImageName("item_search.png");
        public static readonly CreativeTabs tabFood = new CreativeTabs(6, "food", new TabIconItemDelegate(( ) => Items.apple));
        public static readonly CreativeTabs tabTools = new CreativeTabs(7, "tools", new TabIconItemDelegate(( ) => Items.iron_axe)).SetEnchantments(new EnumEnchantmentType[] { EnumEnchantmentType.digger, EnumEnchantmentType.fishing_rod, EnumEnchantmentType.breakable });
        public static readonly CreativeTabs tabCombat = new CreativeTabs(8, "combat", new TabIconItemDelegate(( ) => Items.golden_sword)).SetEnchantments(new EnumEnchantmentType[] { EnumEnchantmentType.armor, EnumEnchantmentType.armor_feet, EnumEnchantmentType.armor_head, EnumEnchantmentType.armor_legs, EnumEnchantmentType.armor_torso, EnumEnchantmentType.bow, EnumEnchantmentType.weapon });
        public static readonly CreativeTabs tabBrewing = new CreativeTabs(9, "brewing", new TabIconItemDelegate(( ) => Items.potionitem));
        public static readonly CreativeTabs tabMaterials = new CreativeTabs(10, "materials", new TabIconItemDelegate(( ) => Items.stick));
        public static readonly CreativeTabs tabInventory = new CreativeTabs(11, "inventory", new TabIconItemDelegate(( ) => Item.getItemFromBlock(Blocks.chest))).SetBackgroundImageName("inventory.png").SetNoScrollbar().SetNoTitle();
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
		private EnumEnchantmentType[] tabEnchantments;
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

        private CreativeTabs SetBackgroundImageName(string p)
        {
            BackgroundImageName = p;
            return this;
        }

		public virtual string BackgroundImageName
		{
			get;
			protected set;
		}

		public virtual bool drawInForegroundOfTab()
		{
			return this.drawTitle;
		}

		public virtual CreativeTabs SetNoTitle()
		{
			this.drawTitle = false;
			return this;
		}

		public virtual bool shouldHidePlayerInventory()
		{
			return this.hasScrollbar;
		}

		public virtual CreativeTabs SetNoScrollbar()
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

		public virtual EnumEnchantmentType[] GetEnchantments()
		{
			return this.tabEnchantments;
		}

		public virtual CreativeTabs SetEnchantments(EnumEnchantmentType[] enchantments)
		{
			this.tabEnchantments = enchantments;
			return this;
		}

		public virtual bool func_111226_a(EnumEnchantmentType enchantments)
		{
			if (this.tabEnchantments == null)
			{
				return false;
			}
			else
			{
				EnumEnchantmentType[] var2 = this.tabEnchantments;
				int var3 = var2.Length;

				for (int var4 = 0; var4 < var3; ++var4)
				{
					EnumEnchantmentType var5 = var2[var4];

					if (var5 == enchantments)
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

			if (this.GetEnchantments() != null)
			{
				this.addEnchantmentBooksToList(p_78018_1_, this.GetEnchantments());
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

					for (int var8 = 0; var8 < p_92116_2_.Length && !var7; ++var8)
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