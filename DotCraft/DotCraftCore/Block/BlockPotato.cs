namespace DotCraftCore.Block
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;
	using World = DotCraftCore.world.World;

	public class BlockPotato : BlockCrops
	{
		private IIcon[] field_149869_a;
		private const string __OBFID = "CL_00000286";

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

				return this.field_149869_a[p_149691_2_ >> 1];
			}
			else
			{
				return this.field_149869_a[3];
			}
		}

		protected internal override Item func_149866_i()
		{
			return Items.potato;
		}

		protected internal override Item func_149865_P()
		{
			return Items.potato;
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public override void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			base.dropBlockAsItemWithChance(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, p_149690_5_, p_149690_6_, p_149690_7_);

			if (!p_149690_1_.isClient)
			{
				if (p_149690_5_ >= 7 && p_149690_1_.rand.Next(50) == 0)
				{
					this.dropBlockAsItem_do(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, new ItemStack(Items.poisonous_potato));
				}
			}
		}

		public override void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149869_a = new IIcon[4];

			for (int var2 = 0; var2 < this.field_149869_a.Length; ++var2)
			{
				this.field_149869_a[var2] = p_149651_1_.registerIcon(this.TextureName + "_stage_" + var2);
			}
		}
	}

}