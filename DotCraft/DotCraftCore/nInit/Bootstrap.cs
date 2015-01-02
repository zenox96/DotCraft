using DotCraftCore.nBlock;
using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nDispenser;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nItem;
using DotCraftCore.nEntity.nProjectile;
using DotCraftCore.nItem;
using DotCraftCore.nStats;
using DotCraftCore.nTileEntity;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nInit
{
	public class Bootstrap
	{
		private static bool field_151355_a = false;

		internal static void func_151353_a()
		{
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.arrow, new BehaviorArrowDispense( ));
            BlockDispenser.dispenseBehaviorRegistry.putObject(Items.egg, new BehaviorEggDispense( ));
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.snowball, new BehaviorSnowballDispense( ));
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.experience_bottle, new BehaviorExperienceDispense( ));
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.potionitem, new BehaviorPotionDispense( ));
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.spawn_egg, new BehaviorSpawnEggDispense( ));
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.fireworks, new BehaviorFireworkDispense( ));
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.fire_charge, new BehaviorFirechargeDispense( ));
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.boat, new BehaviorBoatDispense( ));
            BlockDispenser.dispenseBehaviorRegistry.putObject(Items.lava_bucket, new BehaviorFilledBucketDispense( ));
            BlockDispenser.dispenseBehaviorRegistry.putObject(Items.water_bucket, new BehaviorFilledBucketDispense( ));
            BlockDispenser.dispenseBehaviorRegistry.putObject(Items.bucket, new BehaviorBucketDispense( ));
            BlockDispenser.dispenseBehaviorRegistry.putObject(Items.flint_and_steel, new BehaviorFlintSteelDispense( ));
            BlockDispenser.dispenseBehaviorRegistry.putObject(Items.dye, new BehaviorDyeDispense( ));
            BlockDispenser.dispenseBehaviorRegistry.putObject(Item.getItemFromBlock(Blocks.tnt), new BehaviorTNTDispense( ));
        }

		public static void func_151354_b()
		{
			if (!field_151355_a)
			{
				field_151355_a = true;
				Block.registerBlocks();
				BlockFire.func_149843_e();
				Item.registerItems();
				StatList.func_151178_a();
				func_151353_a();
			}
		}
	}

    protected internal class BehaviorArrowDispense : BehaviorProjectileDispense
    {
        protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_)
        {
            EntityArrow var3 = new EntityArrow(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z);
            var3.canBePickedUp = 1;
            return var3;
        }
    }

    protected internal class BehaviorEggDispense : BehaviorProjectileDispense
    {
        protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_)
        {
            return new EntityEgg(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z);
        }
    }

    protected internal class BehaviorSnowballDispense : BehaviorProjectileDispense
    {
        protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_)
        {
            return new EntitySnowball(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z); 
        }
    }

    protected internal class BehaviorExperienceDispense : BehaviorProjectileDispense
    {
        protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_)
        {
            return new EntityExpBottle(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z);
        }
        
        protected float func_82498_a( )
        {
            return base.func_82498_a( ) * 0.5F;
        }
        
        protected float func_82500_b( )
        {
            return base.func_82500_b( ) * 1.25F;
        }
    }

    protected internal class BehaviorPotionDispense : IBehaviorDispenseItem
    {
        private readonly BehaviorDefaultDispenseItem field_150843_b = new BehaviorDefaultDispenseItem();

        public ItemStack dispense(IBlockSource p_82482_1_, ItemStack p_82482_2_)
        {
            return ItemPotion.isSplash(p_82482_2_.ItemDamage) ? (new BehaviorSplashPotionDispense(p_82482_2_)).dispense(p_82482_1_, p_82482_2_): this.field_150843_b.dispense(p_82482_1_, p_82482_2_);
        }

        private class BehaviorSplashPotionDispense : BehaviorProjectileDispense
        {
            private readonly ItemStack potion;

            public BehaviorSplashPotionDispense(ItemStack itemstack) : base()
            {
                this.potion = itemstack;
            }

            protected IProjectile getProjectileEntity(World world, IPosition pos)
            {
                return new EntityPotion(world, pos.X, pos.Y, pos.Z, potion.copy( ));
            }

            protected float func_82498_a( )
            {
                return base.func_82498_a( ) * 0.5F;
            }

            protected float func_82500_b( )
            {
                return base.func_82500_b( ) * 1.25F;
            }
        }
    }

    protected internal class BehaviorSpawnEggDispense : BehaviorProjectileDispense
    {
        public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        {
            EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
            double var4 = p_82487_1_.X + (double)var3.FrontOffsetX;
            double var6 = (double)p_82487_1_.YInt + 0.2D;
            double var8 = p_82487_1_.Z + (double)var3.FrontOffsetZ;
            Entity var10 = ItemMonsterPlacer.spawnCreature(p_82487_1_.World, p_82487_2_.ItemDamage, var4, var6, var8);
            if (var10 is EntityLivingBase && p_82487_2_.hasDisplayName( ))
            {
                ((EntityLiving)var10).setCustomNameTag(p_82487_2_.DisplayName);
            }
            p_82487_2_.splitStack(1);
            return p_82487_2_;
        }
    }

    protected internal class BehaviorFireworkDispense : BehaviorProjectileDispense
    {
        public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        {
            EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
            double var4 = p_82487_1_.X + (double)var3.FrontOffsetX;
            double var6 = (double)p_82487_1_.YInt + 0.2D;
            double var8 = p_82487_1_.Z + (double)var3.FrontOffsetZ;
            EntityFireworkRocket var10 = new EntityFireworkRocket(p_82487_1_.World, var4, var6, var8, p_82487_2_);
            p_82487_1_.World.spawnEntityInWorld(var10);
            p_82487_2_.splitStack(1);
            return p_82487_2_;
        }
        
        protected void playDispenseSound(IBlockSource p_82485_1_)
        {
            p_82485_1_.World.playAuxSFX(1002, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
        }
    }

    protected internal class BehaviorFirechargeDispense : BehaviorProjectileDispense
    {
        public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        {
            EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
            IPosition var4 = BlockDispenser.func_149939_a(p_82487_1_);
            double var5 = var4.X + (double)var3.FrontOffsetX * 0.3D;
            double var7 = var4.Y + (double)var3.FrontOffsetX * 0.3D;
            double var9 = var4.Z + (double)var3.FrontOffsetZ * 0.3D;
            World var11 = p_82487_1_.World;
            Random var12 = var11.rand;
            double var13 = var12.NextGaussian( ) * 0.05D + (double)var3.FrontOffsetX;
            double var15 = var12.NextGaussian( ) * 0.05D + (double)var3.FrontOffsetY;
            double var17 = var12.NextGaussian( ) * 0.05D + (double)var3.FrontOffsetZ;
            var11.spawnEntityInWorld(new EntitySmallFireball(var11, var5, var7, var9, var13, var15, var17));
            p_82487_2_.splitStack(1);
            return p_82487_2_;
        }
        
        protected void playDispenseSound(IBlockSource p_82485_1_)
        {
            p_82485_1_.World.playAuxSFX(1009, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
        }
    }

    protected internal class BehaviorBoatDispense : BehaviorDefaultDispenseItem
    {
        private readonly BehaviorDefaultDispenseItem field_150842_b = new BehaviorDefaultDispenseItem();
        
        public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        {
            EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
            World var4 = p_82487_1_.World; double var5 = p_82487_1_.X + ((double)var3.FrontOffsetX * 1.125D);
            double var7 = p_82487_1_.Y + ((double)var3.FrontOffsetY * 1.125D);
            double var9 = p_82487_1_.Z + ((double)var3.FrontOffsetZ * 1.125D);
            int var11 = p_82487_1_.XInt + var3.FrontOffsetX;
            int var12 = p_82487_1_.YInt + var3.FrontOffsetY;
            int var13 = p_82487_1_.ZInt + var3.FrontOffsetZ;
            Material var14 = var4.getBlock(var11, var12, var13).BlockMaterial;
            double var15 = 0.0D;
            if (Material.water.Equals(var14))
            {
                var15 = 1.0D;
            }
            else if (!Material.air.Equals(var14) || !Material.water.Equals(var4.getBlock(var11, var12 - 1, var13).BlockMaterial))
            {
                return this.field_150842_b.dispense(p_82487_1_, p_82487_2_);
            }
            EntityBoat var17 = new EntityBoat(var4, var5, var7 + var15, var9);
            var4.spawnEntityInWorld(var17);
            p_82487_2_.splitStack(1);
            return p_82487_2_;
        }
        
        protected void playDispenseSound(IBlockSource p_82485_1_)
        {
            p_82485_1_.World.playAuxSFX(1000, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
        }
    }

    protected internal class BehaviorFilledBucketDispense : BehaviorDefaultDispenseItem
    {
		private readonly BehaviorDefaultDispenseItem field_150841_b = new BehaviorDefaultDispenseItem();
			
		public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
		{
			ItemBucket var3 = (ItemBucket)p_82487_2_.getItem();
			int var4 = p_82487_1_.getXInt();
			int var5 = p_82487_1_.getYInt();
			int var6 = p_82487_1_.getZInt();
			EnumFacing var7 = BlockDispenser.func_149937_b(p_82487_1_.getBlockMetadata());

			if (var3.tryPlaceContainedLiquid(p_82487_1_.getWorld(), var4 + var7.getFrontOffsetX(), var5 + var7.getFrontOffsetY(), var6 + var7.getFrontOffsetZ()))
			{
				p_82487_2_.func_150996_a(Items.bucket);
				p_82487_2_.stackSize = 1;
				return p_82487_2_;
			}
			else
			{
				return this.field_150841_b.dispense(p_82487_1_, p_82487_2_);
			}
		}
	}

    protected internal class BehaviorBucketDispense : BehaviorDefaultDispenseItem
    {
        private readonly BehaviorDefaultDispenseItem field_150840_b = new BehaviorDefaultDispenseItem(); 
        public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        {
            EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
            World var4 = p_82487_1_.World;
            int var5 = p_82487_1_.XInt + var3.FrontOffsetX;
            int var6 = p_82487_1_.YInt + var3.FrontOffsetY;
            int var7 = p_82487_1_.ZInt + var3.FrontOffsetZ;
            Material var8 = var4.getBlock(var5, var6, var7).BlockMaterial;
            int var9 = var4.getBlockMetadata(var5, var6, var7);
            Item var10;

            if (Material.water.Equals(var8) && var9 == 0)
            {
                var10 = Items.water_bucket;
            }
            else
            {
                if (!Material.lava.Equals(var8) || var9 != 0)
                {
                    return base.dispenseStack(p_82487_1_, p_82487_2_);
                }
                var10 = Items.lava_bucket;
            }
            var4.setBlockToAir(var5, var6, var7);
            if (--p_82487_2_.stackSize == 0)
            {
                p_82487_2_.func_150996_a(var10);
                p_82487_2_.stackSize = 1;
            }
            else if (((TileEntityDispenser)p_82487_1_.BlockTileEntity).func_146019_a(new ItemStack(var10)) < 0)
            {
                this.field_150840_b.dispense(p_82487_1_, new ItemStack(var10));
            }
            return p_82487_2_;
        }
    }

    protected internal class BehaviorFlintSteelDispense : BehaviorDefaultDispenseItem
    {
        private bool field_150839_b = true;

        protected ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        {
            EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
            World var4 = p_82487_1_.World;
            int var5 = p_82487_1_.XInt + var3.FrontOffsetX;
            int var6 = p_82487_1_.YInt + var3.FrontOffsetY;
            int var7 = p_82487_1_.ZInt + var3.FrontOffsetZ;
            if (var4.isAirBlock(var5, var6, var7))
            {
                var4.setBlock(var5, var6, var7, Blocks.fire);
                if (p_82487_2_.attemptDamageItem(1, var4.rand))
                {
                    p_82487_2_.stackSize = 0;
                }
            }
            else if (var4.getBlock(var5, var6, var7) == Blocks.tnt)
            {
                Blocks.tnt.onBlockDestroyedByPlayer(var4, var5, var6, var7, 1);
                var4.setBlockToAir(var5, var6, var7);
            }
            else
            {
                this.field_150839_b = false;
            }
            return p_82487_2_;
        }
        
        protected void playDispenseSound(IBlockSource p_82485_1_)
        {
            if (this.field_150839_b)
            {
                p_82485_1_.World.playAuxSFX(1000, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
            }
            else
            {
                p_82485_1_.World.playAuxSFX(1001, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
            }
        }
    }

    protected internal class BehaviorDyeDispense : BehaviorDefaultDispenseItem
    {
        private bool field_150838_b = true;

        protected ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        {
            if (p_82487_2_.ItemDamage == 15)
            {
                EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
                World var4 = p_82487_1_.World;
                int var5 = p_82487_1_.XInt + var3.FrontOffsetX;
                int var6 = p_82487_1_.YInt + var3.FrontOffsetY;
                int var7 = p_82487_1_.ZInt + var3.FrontOffsetZ;
                if (ItemDye.func_150919_a(p_82487_2_, var4, var5, var6, var7))
                {
                    if (!var4.isClient)
                    {
                        var4.playAuxSFX(2005, var5, var6, var7, 0);
                    }
                }
                else
                {
                    this.field_150838_b = false;
                }
                return p_82487_2_;
            }
            else
            {
                return base.dispenseStack(p_82487_1_, p_82487_2_);
            }
        }
        
        protected void playDispenseSound(IBlockSource p_82485_1_)
        {
            if (this.field_150838_b)
            {
                p_82485_1_.World.playAuxSFX(1000, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
            }
            else
            {
                p_82485_1_.World.playAuxSFX(1001, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
            }
        }
    }

    protected internal class BehaviorTNTDispense : BehaviorDefaultDispenseItem
    {
        protected ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        {
            EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
            World var4 = p_82487_1_.World;
            int var5 = p_82487_1_.XInt + var3.FrontOffsetX;
            int var6 = p_82487_1_.YInt + var3.FrontOffsetY;
            int var7 = p_82487_1_.ZInt + var3.FrontOffsetZ;
            EntityTNTPrimed var8 = new EntityTNTPrimed(var4, (double)var5 + 0.5D, (double)var6 + 0.5D, (double)var7 + 0.5D, (EntityLivingBase)null);
            var4.spawnEntityInWorld(var8);
            --p_82487_2_.stackSize;
            return p_82487_2_;
        }
    }
}