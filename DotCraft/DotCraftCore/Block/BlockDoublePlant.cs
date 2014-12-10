using System;
using System.Collections;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using StatList = DotCraftCore.Stats.StatList;
	using IIcon = DotCraftCore.Util.IIcon;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockDoublePlant : BlockBush, IGrowable
	{
		public static readonly string[] field_149892_a = new string[] {"sunflower", "syringa", "grass", "fern", "rose", "paeonia"};
		private IIcon[] field_149893_M;
		private IIcon[] field_149894_N;
		public IIcon[] field_149891_b;
		private const string __OBFID = "CL_00000231";

		public BlockDoublePlant() : base(Material.plants)
		{
			this.Hardness = 0.0F;
			this.StepSound = soundTypeGrass;
			this.BlockName = "doublePlant";
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return 40;
			}
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

		public virtual int func_149885_e(IBlockAccess p_149885_1_, int p_149885_2_, int p_149885_3_, int p_149885_4_)
		{
			int var5 = p_149885_1_.getBlockMetadata(p_149885_2_, p_149885_3_, p_149885_4_);
			return !func_149887_c(var5) ? var5 & 7 : p_149885_1_.getBlockMetadata(p_149885_2_, p_149885_3_ - 1, p_149885_4_) & 7;
		}

		public override bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_) && p_149742_1_.isAirBlock(p_149742_2_, p_149742_3_ + 1, p_149742_4_);
		}

		protected internal override void func_149855_e(World p_149855_1_, int p_149855_2_, int p_149855_3_, int p_149855_4_)
		{
			if (!this.canBlockStay(p_149855_1_, p_149855_2_, p_149855_3_, p_149855_4_))
			{
				int var5 = p_149855_1_.getBlockMetadata(p_149855_2_, p_149855_3_, p_149855_4_);

				if (!func_149887_c(var5))
				{
					this.dropBlockAsItem(p_149855_1_, p_149855_2_, p_149855_3_, p_149855_4_, var5, 0);

					if (p_149855_1_.getBlock(p_149855_2_, p_149855_3_ + 1, p_149855_4_) == this)
					{
						p_149855_1_.setBlock(p_149855_2_, p_149855_3_ + 1, p_149855_4_, Blocks.air, 0, 2);
					}
				}

				p_149855_1_.setBlock(p_149855_2_, p_149855_3_, p_149855_4_, Blocks.air, 0, 2);
			}
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public override bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			int var5 = p_149718_1_.getBlockMetadata(p_149718_2_, p_149718_3_, p_149718_4_);
			return func_149887_c(var5) ? p_149718_1_.getBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_) == this : p_149718_1_.getBlock(p_149718_2_, p_149718_3_ + 1, p_149718_4_) == this && base.canBlockStay(p_149718_1_, p_149718_2_, p_149718_3_, p_149718_4_);
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			if (func_149887_c(p_149650_1_))
			{
				return null;
			}
			else
			{
				int var4 = func_149890_d(p_149650_1_);
				return var4 != 3 && var4 != 2 ? Item.getItemFromBlock(this) : null;
			}
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return func_149887_c(p_149692_1_) ? 0 : p_149692_1_ & 7;
		}

		public static bool func_149887_c(int p_149887_0_)
		{
			return (p_149887_0_ & 8) != 0;
		}

		public static int func_149890_d(int p_149890_0_)
		{
			return p_149890_0_ & 7;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return func_149887_c(p_149691_2_) ? this.field_149893_M[0] : this.field_149893_M[p_149691_2_ & 7];
		}

		public virtual IIcon func_149888_a(bool p_149888_1_, int p_149888_2_)
		{
			return p_149888_1_ ? this.field_149894_N[p_149888_2_] : this.field_149893_M[p_149888_2_];
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public virtual int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			int var5 = this.func_149885_e(p_149720_1_, p_149720_2_, p_149720_3_, p_149720_4_);
			return var5 != 2 && var5 != 3 ? 16777215 : p_149720_1_.getBiomeGenForCoords(p_149720_2_, p_149720_4_).getBiomeGrassColor(p_149720_2_, p_149720_3_, p_149720_4_);
		}

		public virtual void func_149889_c(World p_149889_1_, int p_149889_2_, int p_149889_3_, int p_149889_4_, int p_149889_5_, int p_149889_6_)
		{
			p_149889_1_.setBlock(p_149889_2_, p_149889_3_, p_149889_4_, this, p_149889_5_, p_149889_6_);
			p_149889_1_.setBlock(p_149889_2_, p_149889_3_ + 1, p_149889_4_, this, 8, p_149889_6_);
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			int var7 = ((MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3) + 2) % 4;
			p_149689_1_.setBlock(p_149689_2_, p_149689_3_ + 1, p_149689_4_, this, 8 | var7, 2);
		}

		public virtual void harvestBlock(World p_149636_1_, EntityPlayer p_149636_2_, int p_149636_3_, int p_149636_4_, int p_149636_5_, int p_149636_6_)
		{
			if (p_149636_1_.isClient || p_149636_2_.CurrentEquippedItem == null || p_149636_2_.CurrentEquippedItem.Item != Items.shears || func_149887_c(p_149636_6_) || !this.func_149886_b(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, p_149636_6_, p_149636_2_))
			{
				base.harvestBlock(p_149636_1_, p_149636_2_, p_149636_3_, p_149636_4_, p_149636_5_, p_149636_6_);
			}
		}

///    
///     <summary> * Called when the block is attempted to be harvested </summary>
///     
		public virtual void onBlockHarvested(World p_149681_1_, int p_149681_2_, int p_149681_3_, int p_149681_4_, int p_149681_5_, EntityPlayer p_149681_6_)
		{
			if (func_149887_c(p_149681_5_))
			{
				if (p_149681_1_.getBlock(p_149681_2_, p_149681_3_ - 1, p_149681_4_) == this)
				{
					if (!p_149681_6_.capabilities.isCreativeMode)
					{
						int var7 = p_149681_1_.getBlockMetadata(p_149681_2_, p_149681_3_ - 1, p_149681_4_);
						int var8 = func_149890_d(var7);

						if (var8 != 3 && var8 != 2)
						{
							p_149681_1_.func_147480_a(p_149681_2_, p_149681_3_ - 1, p_149681_4_, true);
						}
						else
						{
							if (!p_149681_1_.isClient && p_149681_6_.CurrentEquippedItem != null && p_149681_6_.CurrentEquippedItem.Item == Items.shears)
							{
								this.func_149886_b(p_149681_1_, p_149681_2_, p_149681_3_, p_149681_4_, var7, p_149681_6_);
							}

							p_149681_1_.setBlockToAir(p_149681_2_, p_149681_3_ - 1, p_149681_4_);
						}
					}
					else
					{
						p_149681_1_.setBlockToAir(p_149681_2_, p_149681_3_ - 1, p_149681_4_);
					}
				}
			}
			else if (p_149681_6_.capabilities.isCreativeMode && p_149681_1_.getBlock(p_149681_2_, p_149681_3_ + 1, p_149681_4_) == this)
			{
				p_149681_1_.setBlock(p_149681_2_, p_149681_3_ + 1, p_149681_4_, Blocks.air, 0, 2);
			}

			base.onBlockHarvested(p_149681_1_, p_149681_2_, p_149681_3_, p_149681_4_, p_149681_5_, p_149681_6_);
		}

		private bool func_149886_b(World p_149886_1_, int p_149886_2_, int p_149886_3_, int p_149886_4_, int p_149886_5_, EntityPlayer p_149886_6_)
		{
			int var7 = func_149890_d(p_149886_5_);

			if (var7 != 3 && var7 != 2)
			{
				return false;
			}
			else
			{
				p_149886_6_.addStat(StatList.mineBlockStatArray[Block.getIdFromBlock(this)], 1);
				sbyte var8 = 1;

				if (var7 == 3)
				{
					var8 = 2;
				}

				this.dropBlockAsItem_do(p_149886_1_, p_149886_2_, p_149886_3_, p_149886_4_, new ItemStack(Blocks.tallgrass, 2, var8));
				return true;
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149893_M = new IIcon[field_149892_a.Length];
			this.field_149894_N = new IIcon[field_149892_a.Length];

			for (int var2 = 0; var2 < this.field_149893_M.Length; ++var2)
			{
				this.field_149893_M[var2] = p_149651_1_.registerIcon("double_plant_" + field_149892_a[var2] + "_bottom");
				this.field_149894_N[var2] = p_149651_1_.registerIcon("double_plant_" + field_149892_a[var2] + "_top");
			}

			this.field_149891_b = new IIcon[2];
			this.field_149891_b[0] = p_149651_1_.registerIcon("double_plant_sunflower_front");
			this.field_149891_b[1] = p_149651_1_.registerIcon("double_plant_sunflower_back");
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 0; var4 < this.field_149893_M.Length; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public virtual int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			int var5 = p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_, p_149643_4_);
			return func_149887_c(var5) ? func_149890_d(p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_ - 1, p_149643_4_)) : func_149890_d(var5);
		}

		public virtual bool func_149851_a(World p_149851_1_, int p_149851_2_, int p_149851_3_, int p_149851_4_, bool p_149851_5_)
		{
			int var6 = this.func_149885_e(p_149851_1_, p_149851_2_, p_149851_3_, p_149851_4_);
			return var6 != 2 && var6 != 3;
		}

		public virtual bool func_149852_a(World p_149852_1_, Random p_149852_2_, int p_149852_3_, int p_149852_4_, int p_149852_5_)
		{
			return true;
		}

		public virtual void func_149853_b(World p_149853_1_, Random p_149853_2_, int p_149853_3_, int p_149853_4_, int p_149853_5_)
		{
			int var6 = this.func_149885_e(p_149853_1_, p_149853_3_, p_149853_4_, p_149853_5_);
			this.dropBlockAsItem_do(p_149853_1_, p_149853_3_, p_149853_4_, p_149853_5_, new ItemStack(this, 1, var6));
		}
	}

}