using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockQuartz : Block
	{
		public static readonly string[] field_150191_a = new string[] {"default", "chiseled", "lines"};
		private static readonly string[] field_150189_b = new string[] {"side", "chiseled", "lines", null, null};

		public BlockQuartz() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public virtual int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
		{
			if (p_149660_9_ == 2)
			{
				switch (p_149660_5_)
				{
					case 0:
					case 1:
						p_149660_9_ = 2;
						break;

					case 2:
					case 3:
						p_149660_9_ = 4;
						break;

					case 4:
					case 5:
						p_149660_9_ = 3;
					break;
				}
			}

			return p_149660_9_;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_ != 3 && p_149692_1_ != 4 ? p_149692_1_ : 2;
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
		{
			return p_149644_1_ != 3 && p_149644_1_ != 4 ? base.createStackedBlock(p_149644_1_) : new ItemStack(Item.getItemFromBlock(this), 1, 2);
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 39;
			}
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 2));
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151677_p;
		}
	}
}