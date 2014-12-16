using System.Collections;

namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Entity = DotCraftCore.entity.Entity;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using IIcon = DotCraftCore.Util.IIcon;
	using World = DotCraftCore.World.World;

	public class ItemBlock : Item
	{
		protected internal readonly Block field_150939_a;
		private IIcon field_150938_b;
		

		public ItemBlock(Block p_i45328_1_)
		{
			this.field_150939_a = p_i45328_1_;
		}

///    
///     <summary> * Sets the unlocalized name of this item to the string passed as the parameter, prefixed by "item." </summary>
///     
		public virtual ItemBlock UnlocalizedName
		{
			set
			{
				base.UnlocalizedName = value;
				return this;
			}
			get
			{
				return this.field_150939_a.UnlocalizedName;
			}
		}

///    
///     <summary> * Returns 0 for /terrain.png, 1 for /gui/items.png </summary>
///     
		public virtual int SpriteNumber
		{
			get
			{
				return this.field_150939_a.ItemIconName != null ? 1 : 0;
			}
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public virtual IIcon getIconFromDamage(int p_77617_1_)
		{
			return this.field_150938_b != null ? this.field_150938_b : this.field_150939_a.getBlockTextureFromSide(1);
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			Block var11 = p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_);

			if (var11 == Blocks.snow_layer && (p_77648_3_.getBlockMetadata(p_77648_4_, p_77648_5_, p_77648_6_) & 7) < 1)
			{
				p_77648_7_ = 1;
			}
			else if (var11 != Blocks.vine && var11 != Blocks.tallgrass && var11 != Blocks.deadbush)
			{
				if (p_77648_7_ == 0)
				{
					--p_77648_5_;
				}

				if (p_77648_7_ == 1)
				{
					++p_77648_5_;
				}

				if (p_77648_7_ == 2)
				{
					--p_77648_6_;
				}

				if (p_77648_7_ == 3)
				{
					++p_77648_6_;
				}

				if (p_77648_7_ == 4)
				{
					--p_77648_4_;
				}

				if (p_77648_7_ == 5)
				{
					++p_77648_4_;
				}
			}

			if (p_77648_1_.stackSize == 0)
			{
				return false;
			}
			else if (!p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_))
			{
				return false;
			}
			else if (p_77648_5_ == 255 && this.field_150939_a.Material.Solid)
			{
				return false;
			}
			else if (p_77648_3_.canPlaceEntityOnSide(this.field_150939_a, p_77648_4_, p_77648_5_, p_77648_6_, false, p_77648_7_, p_77648_2_, p_77648_1_))
			{
				int var12 = this.getMetadata(p_77648_1_.ItemDamage);
				int var13 = this.field_150939_a.onBlockPlaced(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_8_, p_77648_9_, p_77648_10_, var12);

				if (p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, this.field_150939_a, var13, 3))
				{
					if (p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_) == this.field_150939_a)
					{
						this.field_150939_a.onBlockPlacedBy(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_, p_77648_2_, p_77648_1_);
						this.field_150939_a.onPostBlockPlaced(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_, var13);
					}

					p_77648_3_.playSoundEffect((double)((float)p_77648_4_ + 0.5F), (double)((float)p_77648_5_ + 0.5F), (double)((float)p_77648_6_ + 0.5F), this.field_150939_a.stepSound.func_150496_b(), (this.field_150939_a.stepSound.func_150497_c() + 1.0F) / 2.0F, this.field_150939_a.stepSound.func_150494_d() * 0.8F);
					--p_77648_1_.stackSize;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual bool func_150936_a(World p_150936_1_, int p_150936_2_, int p_150936_3_, int p_150936_4_, int p_150936_5_, EntityPlayer p_150936_6_, ItemStack p_150936_7_)
		{
			Block var8 = p_150936_1_.getBlock(p_150936_2_, p_150936_3_, p_150936_4_);

			if (var8 == Blocks.snow_layer)
			{
				p_150936_5_ = 1;
			}
			else if (var8 != Blocks.vine && var8 != Blocks.tallgrass && var8 != Blocks.deadbush)
			{
				if (p_150936_5_ == 0)
				{
					--p_150936_3_;
				}

				if (p_150936_5_ == 1)
				{
					++p_150936_3_;
				}

				if (p_150936_5_ == 2)
				{
					--p_150936_4_;
				}

				if (p_150936_5_ == 3)
				{
					++p_150936_4_;
				}

				if (p_150936_5_ == 4)
				{
					--p_150936_2_;
				}

				if (p_150936_5_ == 5)
				{
					++p_150936_2_;
				}
			}

			return p_150936_1_.canPlaceEntityOnSide(this.field_150939_a, p_150936_2_, p_150936_3_, p_150936_4_, false, p_150936_5_, (Entity)null, p_150936_7_);
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public virtual string getUnlocalizedName(ItemStack p_77667_1_)
		{
			return this.field_150939_a.UnlocalizedName;
		}

///    
///     <summary> * Returns the unlocalized name of this item. </summary>
///     

///    
///     <summary> * gets the CreativeTab this item is displayed on </summary>
///     
		public virtual CreativeTabs CreativeTab
		{
			get
			{
				return this.field_150939_a.CreativeTabToDisplayOn;
			}
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
			this.field_150939_a.getSubBlocks(p_150895_1_, p_150895_2_, p_150895_3_);
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			string var2 = this.field_150939_a.ItemIconName;

			if (var2 != null)
			{
				this.field_150938_b = p_94581_1_.registerIcon(var2);
			}
		}
	}

}