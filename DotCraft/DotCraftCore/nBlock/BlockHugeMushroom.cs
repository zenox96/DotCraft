using System;

namespace DotCraftCore.nBlock
{
    using DotCraftCore.nBlock.nMaterial;
    using DotCraftCore.nInit;
    using DotCraftCore.nItem;
    using DotCraftCore.nUtil;
    using DotCraftCore.nWorld;

	public class BlockHugeMushroom : Block
	{
		private static readonly string[] field_149793_a = new string[] {"skin_brown", "skin_red"};
		private readonly int field_149792_b;
		private IIcon[] field_149794_M;
		private IIcon field_149795_N;
		private IIcon field_149796_O;
		

		public BlockHugeMushroom(Material p_i45412_1_, int p_i45412_2_) : base(p_i45412_1_)
		{
			this.field_149792_b = p_i45412_2_;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_2_ == 10 && p_149691_1_ > 1 ? this.field_149795_N : (p_149691_2_ >= 1 && p_149691_2_ <= 9 && p_149691_1_ == 1 ? this.field_149794_M[this.field_149792_b] : (p_149691_2_ >= 1 && p_149691_2_ <= 3 && p_149691_1_ == 2 ? this.field_149794_M[this.field_149792_b] : (p_149691_2_ >= 7 && p_149691_2_ <= 9 && p_149691_1_ == 3 ? this.field_149794_M[this.field_149792_b] : ((p_149691_2_ == 1 || p_149691_2_ == 4 || p_149691_2_ == 7) && p_149691_1_ == 4 ? this.field_149794_M[this.field_149792_b] : ((p_149691_2_ == 3 || p_149691_2_ == 6 || p_149691_2_ == 9) && p_149691_1_ == 5 ? this.field_149794_M[this.field_149792_b] : (p_149691_2_ == 14 ? this.field_149794_M[this.field_149792_b] : (p_149691_2_ == 15 ? this.field_149795_N : this.field_149796_O)))))));
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			int var2 = p_149745_1_.Next(10) - 7;

			if (var2 < 0)
			{
				var2 = 0;
			}

			return var2;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemById(Block.getIdFromBlock(Blocks.brown_mushroom) + this.field_149792_b);
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemById(Block.getIdFromBlock(Blocks.brown_mushroom) + this.field_149792_b);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149794_M = new IIcon[field_149793_a.Length];

			for (int var2 = 0; var2 < this.field_149794_M.Length; ++var2)
			{
				this.field_149794_M[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + field_149793_a[var2]);
			}

			this.field_149796_O = p_149651_1_.registerIcon(this.TextureName + "_" + "inside");
			this.field_149795_N = p_149651_1_.registerIcon(this.TextureName + "_" + "skin_stem");
		}
	}

}