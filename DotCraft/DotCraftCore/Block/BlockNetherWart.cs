using System;

namespace DotCraftCore.Block
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;
	using World = DotCraftCore.world.World;

	public class BlockNetherWart : BlockBush
	{
		private IIcon[] field_149883_a;
		private const string __OBFID = "CL_00000274";

		protected internal BlockNetherWart()
		{
			this.TickRandomly = true;
			float var1 = 0.5F;
			this.setBlockBounds(0.5F - var1, 0.0F, 0.5F - var1, 0.5F + var1, 0.25F, 0.5F + var1);
			this.CreativeTab = (CreativeTabs)null;
		}

		protected internal override bool func_149854_a(Block p_149854_1_)
		{
			return p_149854_1_ == Blocks.soul_sand;
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public override bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			return this.func_149854_a(p_149718_1_.getBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_));
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			int var6 = p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_);

			if (var6 < 3 && p_149674_5_.Next(10) == 0)
			{
				++var6;
				p_149674_1_.setBlockMetadataWithNotify(p_149674_2_, p_149674_3_, p_149674_4_, var6, 2);
			}

			base.updateTick(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_5_);
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_2_ >= 3 ? this.field_149883_a[2] : (p_149691_2_ > 0 ? this.field_149883_a[1] : this.field_149883_a[0]);
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return 6;
			}
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public virtual void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			if (!p_149690_1_.isClient)
			{
				int var8 = 1;

				if (p_149690_5_ >= 3)
				{
					var8 = 2 + p_149690_1_.rand.Next(3);

					if (p_149690_7_ > 0)
					{
						var8 += p_149690_1_.rand.Next(p_149690_7_ + 1);
					}
				}

				for (int var9 = 0; var9 < var8; ++var9)
				{
					this.dropBlockAsItem_do(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, new ItemStack(Items.nether_wart));
				}
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return null;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Items.nether_wart;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149883_a = new IIcon[3];

			for (int var2 = 0; var2 < this.field_149883_a.Length; ++var2)
			{
				this.field_149883_a[var2] = p_149651_1_.registerIcon(this.TextureName + "_stage_" + var2);
			}
		}
	}

}