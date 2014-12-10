namespace DotCraftCore.World.Gen
{

	using Block = DotCraftCore.block.Block;

	public class FlatLayerInfo
	{
		private Block field_151537_a;

	/// <summary> Amount of layers for this set of layers.  </summary>
		private int layerCount;

	/// <summary> Block metadata used on this set of laeyrs.  </summary>
		private int layerFillBlockMeta;
		private int layerMinimumY;
		private const string __OBFID = "CL_00000441";

		public FlatLayerInfo(int p_i45467_1_, Block p_i45467_2_)
		{
			this.layerCount = 1;
			this.layerCount = p_i45467_1_;
			this.field_151537_a = p_i45467_2_;
		}

		public FlatLayerInfo(int p_i45468_1_, Block p_i45468_2_, int p_i45468_3_) : this(p_i45468_1_, p_i45468_2_)
		{
			this.layerFillBlockMeta = p_i45468_3_;
		}

///    
///     <summary> * Return the amount of layers for this set of layers. </summary>
///     
		public virtual int LayerCount
		{
			get
			{
				return this.layerCount;
			}
		}

		public virtual Block func_151536_b()
		{
			return this.field_151537_a;
		}

///    
///     <summary> * Return the block metadata used on this set of layers. </summary>
///     
		public virtual int FillBlockMeta
		{
			get
			{
				return this.layerFillBlockMeta;
			}
		}

///    
///     <summary> * Return the minimum Y coordinate for this layer, set during generation. </summary>
///     
		public virtual int MinY
		{
			get
			{
				return this.layerMinimumY;
			}
			set
			{
				this.layerMinimumY = value;
			}
		}

///    
///     <summary> * Set the minimum Y coordinate for this layer. </summary>
///     

		public override string ToString()
		{
			string var1 = int.ToString(Block.getIdFromBlock(this.field_151537_a));

			if (this.layerCount > 1)
			{
				var1 = this.layerCount + "x" + var1;
			}

			if (this.layerFillBlockMeta > 0)
			{
				var1 = var1 + ":" + this.layerFillBlockMeta;
			}

			return var1;
		}
	}

}