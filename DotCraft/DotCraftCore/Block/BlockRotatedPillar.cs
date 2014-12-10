namespace DotCraftCore.Block
{

	
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using IIcon = DotCraftCore.Util.IIcon;
	using World = DotCraftCore.World.World;

	public abstract class BlockRotatedPillar : Block
	{
		protected internal IIcon field_150164_N;
		private const string __OBFID = "CL_00000302";

		protected internal BlockRotatedPillar(Material p_i45425_1_) : base(p_i45425_1_)
		{
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 31;
			}
		}

		public virtual int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
		{
			int var10 = p_149660_9_ & 3;
			sbyte var11 = 0;

			switch (p_149660_5_)
			{
				case 0:
				case 1:
					var11 = 0;
					break;

				case 2:
				case 3:
					var11 = 8;
					break;

				case 4:
				case 5:
					var11 = 4;
				break;
			}

			return var10 | var11;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			int var3 = p_149691_2_ & 12;
			int var4 = p_149691_2_ & 3;
			return var3 == 0 && (p_149691_1_ == 1 || p_149691_1_ == 0) ? this.func_150161_d(var4) : (var3 == 4 && (p_149691_1_ == 5 || p_149691_1_ == 4) ? this.func_150161_d(var4) : (var3 == 8 && (p_149691_1_ == 2 || p_149691_1_ == 3) ? this.func_150161_d(var4) : this.func_150163_b(var4)));
		}

		protected internal abstract IIcon func_150163_b(int p_150163_1_);

		protected internal virtual IIcon func_150161_d(int p_150161_1_)
		{
			return this.field_150164_N;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_ & 3;
		}

		public virtual int func_150162_k(int p_150162_1_)
		{
			return p_150162_1_ & 3;
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
		{
			return new ItemStack(Item.getItemFromBlock(this), 1, this.func_150162_k(p_149644_1_));
		}
	}

}