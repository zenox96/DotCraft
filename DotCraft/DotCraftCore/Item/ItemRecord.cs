using System.Collections;

namespace DotCraftCore.Item
{

	using BlockJukebox = DotCraftCore.block.BlockJukebox;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using IIcon = DotCraftCore.Util.IIcon;
	using StatCollector = DotCraftCore.Util.StatCollector;
	using World = DotCraftCore.World.World;

	public class ItemRecord : Item
	{
		private static readonly IDictionary field_150928_b = new Hashtable();
		public readonly string field_150929_a;
		private const string __OBFID = "CL_00000057";

		protected internal ItemRecord(string p_i45350_1_)
		{
			this.field_150929_a = p_i45350_1_;
			this.maxStackSize = 1;
			this.CreativeTab = CreativeTabs.tabMisc;
			field_150928_b.Add(p_i45350_1_, this);
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public virtual IIcon getIconFromDamage(int p_77617_1_)
		{
			return this.itemIcon;
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_) == Blocks.jukebox && p_77648_3_.getBlockMetadata(p_77648_4_, p_77648_5_, p_77648_6_) == 0)
			{
				if (p_77648_3_.isClient)
				{
					return true;
				}
				else
				{
					((BlockJukebox)Blocks.jukebox).func_149926_b(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_, p_77648_1_);
					p_77648_3_.playAuxSFXAtEntity((EntityPlayer)null, 1005, p_77648_4_, p_77648_5_, p_77648_6_, Item.getIdFromItem(this));
					--p_77648_1_.stackSize;
					return true;
				}
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * allows items to add custom lines of information to the mouseover description </summary>
///     
		public virtual void addInformation(ItemStack p_77624_1_, EntityPlayer p_77624_2_, IList p_77624_3_, bool p_77624_4_)
		{
			p_77624_3_.Add(this.func_150927_i());
		}

		public virtual string func_150927_i()
		{
			return StatCollector.translateToLocal("item.record." + this.field_150929_a + ".desc");
		}

///    
///     <summary> * Return an item rarity from EnumRarity </summary>
///     
		public virtual EnumRarity getRarity(ItemStack p_77613_1_)
		{
			return EnumRarity.rare;
		}

		public static ItemRecord func_150926_b(string p_150926_0_)
		{
			return (ItemRecord)field_150928_b[p_150926_0_];
		}
	}

}