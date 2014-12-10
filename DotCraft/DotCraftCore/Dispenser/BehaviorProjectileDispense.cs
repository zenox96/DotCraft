namespace DotCraftCore.Dispenser
{

	using BlockDispenser = DotCraftCore.block.BlockDispenser;
	using Entity = DotCraftCore.Entity.Entity;
	using IProjectile = DotCraftCore.Entity.IProjectile;
	using ItemStack = DotCraftCore.item.ItemStack;
	using EnumFacing = DotCraftCore.util.EnumFacing;
	using World = DotCraftCore.world.World;

	public abstract class BehaviorProjectileDispense : BehaviorDefaultDispenseItem
	{
		private const string __OBFID = "CL_00001394";

///    
///     <summary> * Dispense the specified stack, play the dispense sound and spawn particles. </summary>
///     
		public override ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
		{
			World var3 = p_82487_1_.World;
			IPosition var4 = BlockDispenser.func_149939_a(p_82487_1_);
			EnumFacing var5 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
			IProjectile var6 = this.getProjectileEntity(var3, var4);
			var6.setThrowableHeading((double)var5.FrontOffsetX, (double)((float)var5.FrontOffsetY + 0.1F), (double)var5.FrontOffsetZ, this.func_82500_b(), this.func_82498_a());
			var3.spawnEntityInWorld((Entity)var6);
			p_82487_2_.splitStack(1);
			return p_82487_2_;
		}

///    
///     <summary> * Play the dispense sound from the specified block. </summary>
///     
		protected internal override void playDispenseSound(IBlockSource p_82485_1_)
		{
			p_82485_1_.World.playAuxSFX(1002, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
		}

///    
///     <summary> * Return the projectile entity spawned by this dispense behavior. </summary>
///     
		protected internal abstract IProjectile getProjectileEntity(World p_82499_1_, IPosition p_82499_2_);

		protected internal virtual float func_82498_a()
		{
			return 6.0F;
		}

		protected internal virtual float func_82500_b()
		{
			return 1.1F;
		}
	}

}