using System.Collections;

namespace DotCraftCore.nItem
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using BlockSkull = DotCraftCore.nBlock.BlockSkull;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using NBTUtil = DotCraftCore.nNBT.NBTUtil;
	using TileEntity = DotCraftCore.nTileEntity.TileEntity;
	using TileEntitySkull = DotCraftCore.nTileEntity.TileEntitySkull;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using StatCollector = DotCraftCore.nUtil.StatCollector;
	using World = DotCraftCore.nWorld.World;

	public class ItemSkull : Item
	{
		private static readonly string[] skullTypes = new string[] {"skeleton", "wither", "zombie", "char", "creeper"};
		public static readonly string[] field_94587_a = new string[] {"skeleton", "wither", "zombie", "steve", "creeper"};
		private IIcon[] field_94586_c;
		

		public ItemSkull()
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
			this.MaxDamage = 0;
			this.HasSubtypes = true;
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (p_77648_7_ == 0)
			{
				return false;
			}
			else if (!p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_).Material.Solid)
			{
				return false;
			}
			else
			{
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

				if (!p_77648_3_.isClient)
				{
					p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, Blocks.skull, p_77648_7_, 2);
					int var11 = 0;

					if (p_77648_7_ == 1)
					{
						var11 = MathHelper.floor_double((double)(p_77648_2_.rotationYaw * 16.0F / 360.0F) + 0.5D) & 15;
					}

					TileEntity var12 = p_77648_3_.getTileEntity(p_77648_4_, p_77648_5_, p_77648_6_);

					if (var12 != null && var12 is TileEntitySkull)
					{
						if (p_77648_1_.ItemDamage == 3)
						{
							GameProfile var13 = null;

							if (p_77648_1_.hasTagCompound())
							{
								NBTTagCompound var14 = p_77648_1_.TagCompound;

								if (var14.func_150297_b("SkullOwner", 10))
								{
									var13 = NBTUtil.func_152459_a(var14.getCompoundTag("SkullOwner"));
								}
								else if (var14.func_150297_b("SkullOwner", 8) && var14.getString("SkullOwner").Length > 0)
								{
									var13 = new GameProfile((UUID)null, var14.getString("SkullOwner"));
								}
							}

							((TileEntitySkull)var12).func_152106_a(var13);
						}
						else
						{
							((TileEntitySkull)var12).func_152107_a(p_77648_1_.ItemDamage);
						}

						((TileEntitySkull)var12).func_145903_a(var11);
						((BlockSkull)Blocks.skull).func_149965_a(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_, (TileEntitySkull)var12);
					}

					--p_77648_1_.stackSize;
				}

				return true;
			}
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
			for (int var4 = 0; var4 < skullTypes.Length; ++var4)
			{
				p_150895_3_.Add(new ItemStack(p_150895_1_, 1, var4));
			}
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public virtual IIcon getIconFromDamage(int p_77617_1_)
		{
			if (p_77617_1_ < 0 || p_77617_1_ >= skullTypes.Length)
			{
				p_77617_1_ = 0;
			}

			return this.field_94586_c[p_77617_1_];
		}

///    
///     <summary> * Returns the metadata of the block which this Item (ItemBlock) can place </summary>
///     
		public virtual int getMetadata(int p_77647_1_)
		{
			return p_77647_1_;
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public virtual string getUnlocalizedName(ItemStack p_77667_1_)
		{
			int var2 = p_77667_1_.ItemDamage;

			if (var2 < 0 || var2 >= skullTypes.Length)
			{
				var2 = 0;
			}

			return base.UnlocalizedName + "." + skullTypes[var2];
		}

		public virtual string getItemStackDisplayName(ItemStack p_77653_1_)
		{
			if (p_77653_1_.ItemDamage == 3 && p_77653_1_.hasTagCompound())
			{
				if (p_77653_1_.TagCompound.func_150297_b("SkullOwner", 10))
				{
					return StatCollector.translateToLocalFormatted("item.skull.player.name", new object[] {NBTUtil.func_152459_a(p_77653_1_.TagCompound.getCompoundTag("SkullOwner")).Name});
				}

				if (p_77653_1_.TagCompound.func_150297_b("SkullOwner", 8))
				{
					return StatCollector.translateToLocalFormatted("item.skull.player.name", new object[] {p_77653_1_.TagCompound.getString("SkullOwner")});
				}
			}

			return base.getItemStackDisplayName(p_77653_1_);
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			this.field_94586_c = new IIcon[field_94587_a.Length];

			for (int var2 = 0; var2 < field_94587_a.Length; ++var2)
			{
				this.field_94586_c[var2] = p_94581_1_.registerIcon(this.IconString + "_" + field_94587_a[var2]);
			}
		}
	}

}