using System;

namespace DotCraftCore.nBlock
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class BlockCrops : BlockBush, IGrowable
	{
		private IIcon[] field_149867_a;
		

		protected internal BlockCrops()
		{
			this.TickRandomly = true;
			float var1 = 0.5F;
			this.setBlockBounds(0.5F - var1, 0.0F, 0.5F - var1, 0.5F + var1, 0.25F, 0.5F + var1);
			this.CreativeTab = (CreativeTabs)null;
			this.Hardness = 0.0F;
			this.StepSound = soundTypeGrass;
			this.disableStats();
		}

		protected internal override bool func_149854_a(Block p_149854_1_)
		{
			return p_149854_1_ == Blocks.farmland;
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			base.updateTick(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_5_);

			if (p_149674_1_.getBlockLightValue(p_149674_2_, p_149674_3_ + 1, p_149674_4_) >= 9)
			{
				int var6 = p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_);

				if (var6 < 7)
				{
					float var7 = this.func_149864_n(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_);

					if (p_149674_5_.Next((int)(25.0F / var7) + 1) == 0)
					{
						++var6;
						p_149674_1_.setBlockMetadataWithNotify(p_149674_2_, p_149674_3_, p_149674_4_, var6, 2);
					}
				}
			}
		}

		public virtual void func_149863_m(World p_149863_1_, int p_149863_2_, int p_149863_3_, int p_149863_4_)
		{
			int var5 = p_149863_1_.getBlockMetadata(p_149863_2_, p_149863_3_, p_149863_4_) + MathHelper.getRandomIntegerInRange(p_149863_1_.rand, 2, 5);

			if (var5 > 7)
			{
				var5 = 7;
			}

			p_149863_1_.setBlockMetadataWithNotify(p_149863_2_, p_149863_3_, p_149863_4_, var5, 2);
		}

		private float func_149864_n(World p_149864_1_, int p_149864_2_, int p_149864_3_, int p_149864_4_)
		{
			float var5 = 1.0F;
			Block var6 = p_149864_1_.getBlock(p_149864_2_, p_149864_3_, p_149864_4_ - 1);
			Block var7 = p_149864_1_.getBlock(p_149864_2_, p_149864_3_, p_149864_4_ + 1);
			Block var8 = p_149864_1_.getBlock(p_149864_2_ - 1, p_149864_3_, p_149864_4_);
			Block var9 = p_149864_1_.getBlock(p_149864_2_ + 1, p_149864_3_, p_149864_4_);
			Block var10 = p_149864_1_.getBlock(p_149864_2_ - 1, p_149864_3_, p_149864_4_ - 1);
			Block var11 = p_149864_1_.getBlock(p_149864_2_ + 1, p_149864_3_, p_149864_4_ - 1);
			Block var12 = p_149864_1_.getBlock(p_149864_2_ + 1, p_149864_3_, p_149864_4_ + 1);
			Block var13 = p_149864_1_.getBlock(p_149864_2_ - 1, p_149864_3_, p_149864_4_ + 1);
			bool var14 = var8 == this || var9 == this;
			bool var15 = var6 == this || var7 == this;
			bool var16 = var10 == this || var11 == this || var12 == this || var13 == this;

			for (int var17 = p_149864_2_ - 1; var17 <= p_149864_2_ + 1; ++var17)
			{
				for (int var18 = p_149864_4_ - 1; var18 <= p_149864_4_ + 1; ++var18)
				{
					float var19 = 0.0F;

					if (p_149864_1_.getBlock(var17, p_149864_3_ - 1, var18) == Blocks.farmland)
					{
						var19 = 1.0F;

						if (p_149864_1_.getBlockMetadata(var17, p_149864_3_ - 1, var18) > 0)
						{
							var19 = 3.0F;
						}
					}

					if (var17 != p_149864_2_ || var18 != p_149864_4_)
					{
						var19 /= 4.0F;
					}

					var5 += var19;
				}
			}

			if (var16 || var14 && var15)
			{
				var5 /= 2.0F;
			}

			return var5;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			if (p_149691_2_ < 0 || p_149691_2_ > 7)
			{
				p_149691_2_ = 7;
			}

			return this.field_149867_a[p_149691_2_];
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

		protected internal virtual Item func_149866_i()
		{
			return Items.wheat_seeds;
		}

		protected internal virtual Item func_149865_P()
		{
			return Items.wheat;
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public virtual void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			base.dropBlockAsItemWithChance(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, p_149690_5_, p_149690_6_, 0);

			if (!p_149690_1_.isClient)
			{
				if (p_149690_5_ >= 7)
				{
					int var8 = 3 + p_149690_7_;

					for (int var9 = 0; var9 < var8; ++var9)
					{
						if (p_149690_1_.rand.Next(15) <= p_149690_5_)
						{
							this.dropBlockAsItem_do(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, new ItemStack(this.func_149866_i(), 1, 0));
						}
					}
				}
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return p_149650_1_ == 7 ? this.func_149865_P() : this.func_149866_i();
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 1;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return this.func_149866_i();
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149867_a = new IIcon[8];

			for (int var2 = 0; var2 < this.field_149867_a.Length; ++var2)
			{
				this.field_149867_a[var2] = p_149651_1_.registerIcon(this.TextureName + "_stage_" + var2);
			}
		}

		public virtual bool func_149851_a(World p_149851_1_, int p_149851_2_, int p_149851_3_, int p_149851_4_, bool p_149851_5_)
		{
			return p_149851_1_.getBlockMetadata(p_149851_2_, p_149851_3_, p_149851_4_) != 7;
		}

		public virtual bool func_149852_a(World p_149852_1_, Random p_149852_2_, int p_149852_3_, int p_149852_4_, int p_149852_5_)
		{
			return true;
		}

		public virtual void func_149853_b(World p_149853_1_, Random p_149853_2_, int p_149853_3_, int p_149853_4_, int p_149853_5_)
		{
			this.func_149863_m(p_149853_1_, p_149853_3_, p_149853_4_, p_149853_5_);
		}
	}

}