using System.Collections;

namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using BlockColored = DotCraftCore.block.BlockColored;
	using BlockLog = DotCraftCore.block.BlockLog;
	using IGrowable = DotCraftCore.block.IGrowable;
	using Material = DotCraftCore.block.material.Material;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntitySheep = DotCraftCore.entity.passive.EntitySheep;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using IIcon = DotCraftCore.Util.IIcon;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class ItemDye : Item
	{
		public static readonly string[] field_150923_a = new string[] {"black", "red", "green", "brown", "blue", "purple", "cyan", "silver", "gray", "pink", "lime", "yellow", "lightBlue", "magenta", "orange", "white"};
		public static readonly string[] field_150921_b = new string[] {"black", "red", "green", "brown", "blue", "purple", "cyan", "silver", "gray", "pink", "lime", "yellow", "light_blue", "magenta", "orange", "white"};
		public static readonly int[] field_150922_c = new int[] {1973019, 11743532, 3887386, 5320730, 2437522, 8073150, 2651799, 11250603, 4408131, 14188952, 4312372, 14602026, 6719955, 12801229, 15435844, 15790320};
		private IIcon[] field_150920_d;
		

		public ItemDye()
		{
			this.HasSubtypes = true;
			this.MaxDamage = 0;
			this.CreativeTab = CreativeTabs.tabMaterials;
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public virtual IIcon getIconFromDamage(int p_77617_1_)
		{
			int var2 = MathHelper.clamp_int(p_77617_1_, 0, 15);
			return this.field_150920_d[var2];
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public virtual string getUnlocalizedName(ItemStack p_77667_1_)
		{
			int var2 = MathHelper.clamp_int(p_77667_1_.ItemDamage, 0, 15);
			return base.UnlocalizedName + "." + field_150923_a[var2];
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (!p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_))
			{
				return false;
			}
			else
			{
				if (p_77648_1_.ItemDamage == 15)
				{
					if (func_150919_a(p_77648_1_, p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_))
					{
						if (!p_77648_3_.isClient)
						{
							p_77648_3_.playAuxSFX(2005, p_77648_4_, p_77648_5_, p_77648_6_, 0);
						}

						return true;
					}
				}
				else if (p_77648_1_.ItemDamage == 3)
				{
					Block var11 = p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_);
					int var12 = p_77648_3_.getBlockMetadata(p_77648_4_, p_77648_5_, p_77648_6_);

					if (var11 == Blocks.log && BlockLog.func_150165_c(var12) == 3)
					{
						if (p_77648_7_ == 0)
						{
							return false;
						}

						if (p_77648_7_ == 1)
						{
							return false;
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

						if (p_77648_3_.isAirBlock(p_77648_4_, p_77648_5_, p_77648_6_))
						{
							int var13 = Blocks.cocoa.onBlockPlaced(p_77648_3_, p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_8_, p_77648_9_, p_77648_10_, 0);
							p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, Blocks.cocoa, var13, 2);

							if (!p_77648_2_.capabilities.isCreativeMode)
							{
								--p_77648_1_.stackSize;
							}
						}

						return true;
					}
				}

				return false;
			}
		}

		public static bool func_150919_a(ItemStack p_150919_0_, World p_150919_1_, int p_150919_2_, int p_150919_3_, int p_150919_4_)
		{
			Block var5 = p_150919_1_.getBlock(p_150919_2_, p_150919_3_, p_150919_4_);

			if (var5 is IGrowable)
			{
				IGrowable var6 = (IGrowable)var5;

				if (var6.func_149851_a(p_150919_1_, p_150919_2_, p_150919_3_, p_150919_4_, p_150919_1_.isClient))
				{
					if (!p_150919_1_.isClient)
					{
						if (var6.func_149852_a(p_150919_1_, p_150919_1_.rand, p_150919_2_, p_150919_3_, p_150919_4_))
						{
							var6.func_149853_b(p_150919_1_, p_150919_1_.rand, p_150919_2_, p_150919_3_, p_150919_4_);
						}

						--p_150919_0_.stackSize;
					}

					return true;
				}
			}

			return false;
		}

		public static void func_150918_a(World p_150918_0_, int p_150918_1_, int p_150918_2_, int p_150918_3_, int p_150918_4_)
		{
			if (p_150918_4_ == 0)
			{
				p_150918_4_ = 15;
			}

			Block var5 = p_150918_0_.getBlock(p_150918_1_, p_150918_2_, p_150918_3_);

			if (var5.Material != Material.air)
			{
				var5.setBlockBoundsBasedOnState(p_150918_0_, p_150918_1_, p_150918_2_, p_150918_3_);

				for (int var6 = 0; var6 < p_150918_4_; ++var6)
				{
					double var7 = itemRand.nextGaussian() * 0.02D;
					double var9 = itemRand.nextGaussian() * 0.02D;
					double var11 = itemRand.nextGaussian() * 0.02D;
					p_150918_0_.spawnParticle("happyVillager", (double)((float)p_150918_1_ + itemRand.nextFloat()), (double)p_150918_2_ + (double)itemRand.nextFloat() * var5.BlockBoundsMaxY, (double)((float)p_150918_3_ + itemRand.nextFloat()), var7, var9, var11);
				}
			}
		}

///    
///     <summary> * Returns true if the item can be used on the given entity, e.g. shears on sheep. </summary>
///     
		public virtual bool itemInteractionForEntity(ItemStack p_111207_1_, EntityPlayer p_111207_2_, EntityLivingBase p_111207_3_)
		{
			if (p_111207_3_ is EntitySheep)
			{
				EntitySheep var4 = (EntitySheep)p_111207_3_;
				int var5 = BlockColored.func_150032_b(p_111207_1_.ItemDamage);

				if (!var4.Sheared && var4.FleeceColor != var5)
				{
					var4.FleeceColor = var5;
					--p_111207_1_.stackSize;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
			for (int var4 = 0; var4 < 16; ++var4)
			{
				p_150895_3_.Add(new ItemStack(p_150895_1_, 1, var4));
			}
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			this.field_150920_d = new IIcon[field_150921_b.Length];

			for (int var2 = 0; var2 < field_150921_b.Length; ++var2)
			{
				this.field_150920_d[var2] = p_94581_1_.registerIcon(this.IconString + "_" + field_150921_b[var2]);
			}
		}
	}

}