namespace DotCraftCore.Block
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using IIcon = DotCraftCore.Util.IIcon;

	public class BlockCarrot : BlockCrops
	{
		private IIcon[] field_149868_a;
		private const string __OBFID = "CL_00000212";

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public override IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			if (p_149691_2_ < 7)
			{
				if (p_149691_2_ == 6)
				{
					p_149691_2_ = 5;
				}

				return this.field_149868_a[p_149691_2_ >> 1];
			}
			else
			{
				return this.field_149868_a[3];
			}
		}

		protected internal override Item func_149866_i()
		{
			return Items.carrot;
		}

		protected internal override Item func_149865_P()
		{
			return Items.carrot;
		}

		public override void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149868_a = new IIcon[4];

			for (int var2 = 0; var2 < this.field_149868_a.Length; ++var2)
			{
				this.field_149868_a[var2] = p_149651_1_.registerIcon(this.TextureName + "_stage_" + var2);
			}
		}
	}

}