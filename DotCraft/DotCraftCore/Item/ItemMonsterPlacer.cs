using System;
using System.Collections;

namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using BlockLiquid = DotCraftCore.block.BlockLiquid;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Entity = DotCraftCore.entity.Entity;
	using EntityList = DotCraftCore.entity.EntityList;
	using EntityLiving = DotCraftCore.entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using IEntityLivingData = DotCraftCore.entity.IEntityLivingData;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Facing = DotCraftCore.Util.Facing;
	using IIcon = DotCraftCore.Util.IIcon;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using MovingObjectPosition = DotCraftCore.Util.MovingObjectPosition;
	using StatCollector = DotCraftCore.Util.StatCollector;
	using World = DotCraftCore.World.World;

	public class ItemMonsterPlacer : Item
	{
		private IIcon theIcon;
		

		public ItemMonsterPlacer()
		{
			this.HasSubtypes = true;
			this.CreativeTab = CreativeTabs.tabMisc;
		}

		public virtual string getItemStackDisplayName(ItemStack p_77653_1_)
		{
			string var2 = ("" + StatCollector.translateToLocal(this.UnlocalizedName + ".name")).Trim();
			string var3 = EntityList.getStringFromID(p_77653_1_.ItemDamage);

			if (var3 != null)
			{
				var2 = var2 + " " + StatCollector.translateToLocal("entity." + var3 + ".name");
			}

			return var2;
		}

		public virtual int getColorFromItemStack(ItemStack p_82790_1_, int p_82790_2_)
		{
			EntityList.EntityEggInfo var3 = (EntityList.EntityEggInfo)EntityList.entityEggs.get(Convert.ToInt32(p_82790_1_.ItemDamage));
			return var3 != null ? (p_82790_2_ == 0 ? var3.primaryColor : var3.secondaryColor) : 16777215;
		}

		public virtual bool requiresMultipleRenderPasses()
		{
			return true;
		}

///    
///     <summary> * Gets an icon index based on an item's damage value and the given render pass </summary>
///     
		public virtual IIcon getIconFromDamageForRenderPass(int p_77618_1_, int p_77618_2_)
		{
			return p_77618_2_ > 0 ? this.theIcon : base.getIconFromDamageForRenderPass(p_77618_1_, p_77618_2_);
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (p_77648_3_.isClient)
			{
				return true;
			}
			else
			{
				Block var11 = p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_);
				p_77648_4_ += Facing.offsetsXForSide[p_77648_7_];
				p_77648_5_ += Facing.offsetsYForSide[p_77648_7_];
				p_77648_6_ += Facing.offsetsZForSide[p_77648_7_];
				double var12 = 0.0D;

				if (p_77648_7_ == 1 && var11.RenderType == 11)
				{
					var12 = 0.5D;
				}

				Entity var14 = spawnCreature(p_77648_3_, p_77648_1_.ItemDamage, (double)p_77648_4_ + 0.5D, (double)p_77648_5_ + var12, (double)p_77648_6_ + 0.5D);

				if (var14 != null)
				{
					if (var14 is EntityLivingBase && p_77648_1_.hasDisplayName())
					{
						((EntityLiving)var14).CustomNameTag = p_77648_1_.DisplayName;
					}

					if (!p_77648_2_.capabilities.isCreativeMode)
					{
						--p_77648_1_.stackSize;
					}
				}

				return true;
			}
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			if (p_77659_2_.isClient)
			{
				return p_77659_1_;
			}
			else
			{
				MovingObjectPosition var4 = this.getMovingObjectPositionFromPlayer(p_77659_2_, p_77659_3_, true);

				if (var4 == null)
				{
					return p_77659_1_;
				}
				else
				{
					if (var4.typeOfHit == MovingObjectPosition.MovingObjectType.BLOCK)
					{
						int var5 = var4.blockX;
						int var6 = var4.blockY;
						int var7 = var4.blockZ;

						if (!p_77659_2_.canMineBlock(p_77659_3_, var5, var6, var7))
						{
							return p_77659_1_;
						}

						if (!p_77659_3_.canPlayerEdit(var5, var6, var7, var4.sideHit, p_77659_1_))
						{
							return p_77659_1_;
						}

						if (p_77659_2_.getBlock(var5, var6, var7) is BlockLiquid)
						{
							Entity var8 = spawnCreature(p_77659_2_, p_77659_1_.ItemDamage, (double)var5, (double)var6, (double)var7);

							if (var8 != null)
							{
								if (var8 is EntityLivingBase && p_77659_1_.hasDisplayName())
								{
									((EntityLiving)var8).CustomNameTag = p_77659_1_.DisplayName;
								}

								if (!p_77659_3_.capabilities.isCreativeMode)
								{
									--p_77659_1_.stackSize;
								}
							}
						}
					}

					return p_77659_1_;
				}
			}
		}

///    
///     <summary> * Spawns the creature specified by the egg's type in the location specified by the last three parameters.
///     * Parameters: world, entityID, x, y, z. </summary>
///     
		public static Entity spawnCreature(World p_77840_0_, int p_77840_1_, double p_77840_2_, double p_77840_4_, double p_77840_6_)
		{
			if (!EntityList.entityEggs.containsKey(Convert.ToInt32(p_77840_1_)))
			{
				return null;
			}
			else
			{
				Entity var8 = null;

				for (int var9 = 0; var9 < 1; ++var9)
				{
					var8 = EntityList.createEntityByID(p_77840_1_, p_77840_0_);

					if (var8 != null && var8 is EntityLivingBase)
					{
						EntityLiving var10 = (EntityLiving)var8;
						var8.setLocationAndAngles(p_77840_2_, p_77840_4_, p_77840_6_, MathHelper.wrapAngleTo180_float(p_77840_0_.rand.nextFloat() * 360.0F), 0.0F);
						var10.rotationYawHead = var10.rotationYaw;
						var10.renderYawOffset = var10.rotationYaw;
						var10.onSpawnWithEgg((IEntityLivingData)null);
						p_77840_0_.spawnEntityInWorld(var8);
						var10.playLivingSound();
					}
				}

				return var8;
			}
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
			IEnumerator var4 = EntityList.entityEggs.values().GetEnumerator();

			while (var4.MoveNext())
			{
				EntityList.EntityEggInfo var5 = (EntityList.EntityEggInfo)var4.Current;
				p_150895_3_.Add(new ItemStack(p_150895_1_, 1, var5.spawnedID));
			}
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			base.registerIcons(p_94581_1_);
			this.theIcon = p_94581_1_.registerIcon(this.IconString + "_overlay");
		}
	}

}