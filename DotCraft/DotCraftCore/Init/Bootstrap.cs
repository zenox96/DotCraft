using System;

namespace DotCraftCore.Init
{

	using Block = DotCraftCore.block.Block;
	using BlockDispenser = DotCraftCore.block.BlockDispenser;
	using BlockFire = DotCraftCore.block.BlockFire;
	using Material = DotCraftCore.block.material.Material;
	using BehaviorDefaultDispenseItem = DotCraftCore.Dispenser.BehaviorDefaultDispenseItem;
	using BehaviorProjectileDispense = DotCraftCore.Dispenser.BehaviorProjectileDispense;
	using IBehaviorDispenseItem = DotCraftCore.Dispenser.IBehaviorDispenseItem;
	using IBlockSource = DotCraftCore.Dispenser.IBlockSource;
	using IPosition = DotCraftCore.Dispenser.IPosition;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IProjectile = DotCraftCore.Entity.IProjectile;
	using EntityBoat = DotCraftCore.Entity.Item.EntityBoat;
	using EntityExpBottle = DotCraftCore.Entity.Item.EntityExpBottle;
	using EntityFireworkRocket = DotCraftCore.Entity.Item.EntityFireworkRocket;
	using EntityTNTPrimed = DotCraftCore.Entity.Item.EntityTNTPrimed;
	using EntityArrow = DotCraftCore.Entity.Projectile.EntityArrow;
	using EntityEgg = DotCraftCore.Entity.Projectile.EntityEgg;
	using EntityPotion = DotCraftCore.Entity.Projectile.EntityPotion;
	using EntitySmallFireball = DotCraftCore.Entity.Projectile.EntitySmallFireball;
	using EntitySnowball = DotCraftCore.Entity.Projectile.EntitySnowball;
	using Item = DotCraftCore.item.Item;
	using ItemBucket = DotCraftCore.item.ItemBucket;
	using ItemDye = DotCraftCore.item.ItemDye;
	using ItemMonsterPlacer = DotCraftCore.item.ItemMonsterPlacer;
	using ItemPotion = DotCraftCore.item.ItemPotion;
	using ItemStack = DotCraftCore.item.ItemStack;
	using StatList = DotCraftCore.stats.StatList;
	using TileEntityDispenser = DotCraftCore.tileentity.TileEntityDispenser;
	using EnumFacing = DotCraftCore.util.EnumFacing;
	using World = DotCraftCore.world.World;

	public class Bootstrap
	{
		private static bool field_151355_a = false;
		private const string __OBFID = "CL_00001397";

		internal static void func_151353_a()
		{
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.arrow, new BehaviorProjectileDispense() { private static final string __OBFID = "CL_00001398"; protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_) { EntityArrow var3 = new EntityArrow(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z); var3.canBePickedUp = 1; return var3; } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.egg, new BehaviorProjectileDispense() { private static final string __OBFID = "CL_00001404"; protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_) { return new EntityEgg(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z); } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.snowball, new BehaviorProjectileDispense() { private static final string __OBFID = "CL_00001405"; protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_) { return new EntitySnowball(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z); } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.experience_bottle, new BehaviorProjectileDispense() { private static final string __OBFID = "CL_00001406"; protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_) { return new EntityExpBottle(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z); } protected float func_82498_a() { return base.func_82498_a() * 0.5F; } protected float func_82500_b() { return base.func_82500_b() * 1.25F; } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.potionitem, new IBehaviorDispenseItem() { private final BehaviorDefaultDispenseItem field_150843_b = new BehaviorDefaultDispenseItem(); private static final string __OBFID = "CL_00001407"; public ItemStack dispense(IBlockSource p_82482_1_, final ItemStack p_82482_2_) { return ItemPotion.isSplash(p_82482_2_.ItemDamage) ? (new BehaviorProjectileDispense() { private static final string __OBFID = "CL_00001408"; protected IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_) { return new EntityPotion(p_82499_1_, p_82499_2_.X, p_82499_2_.Y, p_82499_2_.Z, p_82482_2_.copy()); } protected float func_82498_a() { return base.func_82498_a() * 0.5F; } protected float func_82500_b() { return base.func_82500_b() * 1.25F; } }).dispense(p_82482_1_, p_82482_2_): this.field_150843_b.dispense(p_82482_1_, p_82482_2_); } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.spawn_egg, new BehaviorDefaultDispenseItem() { private static final string __OBFID = "CL_00001410"; public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_) { EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata); double var4 = p_82487_1_.X + (double)var3.FrontOffsetX; double var6 = (double)((float)p_82487_1_.YInt + 0.2F); double var8 = p_82487_1_.Z + (double)var3.FrontOffsetZ; Entity var10 = ItemMonsterPlacer.spawnCreature(p_82487_1_.World, p_82487_2_.ItemDamage, var4, var6, var8); if (var10 is EntityLivingBase && p_82487_2_.hasDisplayName()) { ((EntityLiving)var10).setCustomNameTag(p_82487_2_.DisplayName); } p_82487_2_.splitStack(1); return p_82487_2_; } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.fireworks, new BehaviorDefaultDispenseItem() { private static final string __OBFID = "CL_00001411"; public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_) { EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata); double var4 = p_82487_1_.X + (double)var3.FrontOffsetX; double var6 = (double)((float)p_82487_1_.YInt + 0.2F); double var8 = p_82487_1_.Z + (double)var3.FrontOffsetZ; EntityFireworkRocket var10 = new EntityFireworkRocket(p_82487_1_.World, var4, var6, var8, p_82487_2_); p_82487_1_.World.spawnEntityInWorld(var10); p_82487_2_.splitStack(1); return p_82487_2_; } protected void playDispenseSound(IBlockSource p_82485_1_) { p_82485_1_.World.playAuxSFX(1002, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0); } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.fire_charge, new BehaviorDefaultDispenseItem() { private static final string __OBFID = "CL_00001412"; public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_) { EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata); IPosition var4 = BlockDispenser.func_149939_a(p_82487_1_); double var5 = var4.X + (double)((float)var3.FrontOffsetX * 0.3F); double var7 = var4.Y + (double)((float)var3.FrontOffsetX * 0.3F); double var9 = var4.Z + (double)((float)var3.FrontOffsetZ * 0.3F); World var11 = p_82487_1_.World; Random var12 = var11.rand; double var13 = var12.nextGaussian() * 0.05D + (double)var3.FrontOffsetX; double var15 = var12.nextGaussian() * 0.05D + (double)var3.FrontOffsetY; double var17 = var12.nextGaussian() * 0.05D + (double)var3.FrontOffsetZ; var11.spawnEntityInWorld(new EntitySmallFireball(var11, var5, var7, var9, var13, var15, var17)); p_82487_2_.splitStack(1); return p_82487_2_; } protected void playDispenseSound(IBlockSource p_82485_1_) { p_82485_1_.World.playAuxSFX(1009, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0); } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.boat, new BehaviorDefaultDispenseItem() { private final BehaviorDefaultDispenseItem field_150842_b = new BehaviorDefaultDispenseItem(); private static final string __OBFID = "CL_00001413"; public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_) { EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata); World var4 = p_82487_1_.World; double var5 = p_82487_1_.X + (double)((float)var3.FrontOffsetX * 1.125F); double var7 = p_82487_1_.Y + (double)((float)var3.FrontOffsetY * 1.125F); double var9 = p_82487_1_.Z + (double)((float)var3.FrontOffsetZ * 1.125F); int var11 = p_82487_1_.XInt + var3.FrontOffsetX; int var12 = p_82487_1_.YInt + var3.FrontOffsetY; int var13 = p_82487_1_.ZInt + var3.FrontOffsetZ; Material var14 = var4.getBlock(var11, var12, var13).Material; double var15; if (Material.water.Equals(var14)) { var15 = 1.0D; } else { if (!Material.air.Equals(var14) || !Material.water.Equals(var4.getBlock(var11, var12 - 1, var13).Material)) { return this.field_150842_b.dispense(p_82487_1_, p_82487_2_); } var15 = 0.0D; } EntityBoat var17 = new EntityBoat(var4, var5, var7 + var15, var9); var4.spawnEntityInWorld(var17); p_82487_2_.splitStack(1); return p_82487_2_; } protected void playDispenseSound(IBlockSource p_82485_1_) { p_82485_1_.World.playAuxSFX(1000, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0); } });
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//			BehaviorDefaultDispenseItem var0 = new BehaviorDefaultDispenseItem()
//		{
//			private final BehaviorDefaultDispenseItem field_150841_b = new BehaviorDefaultDispenseItem();
//			private static final String __OBFID = "CL_00001399";
//			public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
//			{
//				ItemBucket var3 = (ItemBucket)p_82487_2_.getItem();
//				int var4 = p_82487_1_.getXInt();
//				int var5 = p_82487_1_.getYInt();
//				int var6 = p_82487_1_.getZInt();
//				EnumFacing var7 = BlockDispenser.func_149937_b(p_82487_1_.getBlockMetadata());
//
//				if (var3.tryPlaceContainedLiquid(p_82487_1_.getWorld(), var4 + var7.getFrontOffsetX(), var5 + var7.getFrontOffsetY(), var6 + var7.getFrontOffsetZ()))
//				{
//					p_82487_2_.func_150996_a(Items.bucket);
//					p_82487_2_.stackSize = 1;
//					return p_82487_2_;
//				}
//				else
//				{
//					return this.field_150841_b.dispense(p_82487_1_, p_82487_2_);
//				}
//			}
//		};
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.lava_bucket, var0);
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.water_bucket, var0);
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.bucket, new BehaviorDefaultDispenseItem() { private final BehaviorDefaultDispenseItem field_150840_b = new BehaviorDefaultDispenseItem(); private static final string __OBFID = "CL_00001400"; public ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_) { EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata); World var4 = p_82487_1_.World; int var5 = p_82487_1_.XInt + var3.FrontOffsetX; int var6 = p_82487_1_.YInt + var3.FrontOffsetY; int var7 = p_82487_1_.ZInt + var3.FrontOffsetZ; Material var8 = var4.getBlock(var5, var6, var7).Material; int var9 = var4.getBlockMetadata(var5, var6, var7); Item var10; if (Material.water.Equals(var8) && var9 == 0) { var10 = Items.water_bucket; } else { if (!Material.lava.Equals(var8) || var9 != 0) { return base.dispenseStack(p_82487_1_, p_82487_2_); } var10 = Items.lava_bucket; } var4.setBlockToAir(var5, var6, var7); if (--p_82487_2_.stackSize == 0) { p_82487_2_.func_150996_a(var10); p_82487_2_.stackSize = 1; } else if (((TileEntityDispenser)p_82487_1_.BlockTileEntity).func_146019_a(new ItemStack(var10)) < 0) { this.field_150840_b.dispense(p_82487_1_, new ItemStack(var10)); } return p_82487_2_; } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.flint_and_steel, new BehaviorDefaultDispenseItem() { private bool field_150839_b = true; private static final string __OBFID = "CL_00001401"; protected ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_) { EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata); World var4 = p_82487_1_.World; int var5 = p_82487_1_.XInt + var3.FrontOffsetX; int var6 = p_82487_1_.YInt + var3.FrontOffsetY; int var7 = p_82487_1_.ZInt + var3.FrontOffsetZ; if (var4.isAirBlock(var5, var6, var7)) { var4.setBlock(var5, var6, var7, Blocks.fire); if (p_82487_2_.attemptDamageItem(1, var4.rand)) { p_82487_2_.stackSize = 0; } } else if (var4.getBlock(var5, var6, var7) == Blocks.tnt) { Blocks.tnt.onBlockDestroyedByPlayer(var4, var5, var6, var7, 1); var4.setBlockToAir(var5, var6, var7); } else { this.field_150839_b = false; } return p_82487_2_; } protected void playDispenseSound(IBlockSource p_82485_1_) { if (this.field_150839_b) { p_82485_1_.World.playAuxSFX(1000, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0); } else { p_82485_1_.World.playAuxSFX(1001, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0); } } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Items.dye, new BehaviorDefaultDispenseItem() { private bool field_150838_b = true; private static final string __OBFID = "CL_00001402"; protected ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_) { if (p_82487_2_.ItemDamage == 15) { EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata); World var4 = p_82487_1_.World; int var5 = p_82487_1_.XInt + var3.FrontOffsetX; int var6 = p_82487_1_.YInt + var3.FrontOffsetY; int var7 = p_82487_1_.ZInt + var3.FrontOffsetZ; if (ItemDye.func_150919_a(p_82487_2_, var4, var5, var6, var7)) { if (!var4.isClient) { var4.playAuxSFX(2005, var5, var6, var7, 0); } } else { this.field_150838_b = false; } return p_82487_2_; } else { return base.dispenseStack(p_82487_1_, p_82487_2_); } } protected void playDispenseSound(IBlockSource p_82485_1_) { if (this.field_150838_b) { p_82485_1_.World.playAuxSFX(1000, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0); } else { p_82485_1_.World.playAuxSFX(1001, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0); } } });
			BlockDispenser.dispenseBehaviorRegistry.putObject(Item.getItemFromBlock(Blocks.tnt), new BehaviorDefaultDispenseItem() { private static final string __OBFID = "CL_00001403"; protected ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_) { EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata); World var4 = p_82487_1_.World; int var5 = p_82487_1_.XInt + var3.FrontOffsetX; int var6 = p_82487_1_.YInt + var3.FrontOffsetY; int var7 = p_82487_1_.ZInt + var3.FrontOffsetZ; EntityTNTPrimed var8 = new EntityTNTPrimed(var4, (double)((float)var5 + 0.5F), (double)((float)var6 + 0.5F), (double)((float)var7 + 0.5F), (EntityLivingBase)null); var4.spawnEntityInWorld(var8); --p_82487_2_.stackSize; return p_82487_2_; } });
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

}