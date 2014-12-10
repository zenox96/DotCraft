namespace DotCraftCore.Dispenser
{

	using BlockDispenser = DotCraftCore.block.BlockDispenser;
	using EntityItem = DotCraftCore.Entity.Item.EntityItem;
	using ItemStack = DotCraftCore.item.ItemStack;
	using EnumFacing = DotCraftCore.util.EnumFacing;
	using World = DotCraftCore.world.World;

	public class BehaviorDefaultDispenseItem : IBehaviorDispenseItem
	{
		private const string __OBFID = "CL_00001195";

///    
///     <summary> * Dispenses the specified ItemStack from a dispenser. </summary>
///     
		public ItemStack dispense(IBlockSource p_82482_1_, ItemStack p_82482_2_)
		{
			ItemStack var3 = this.dispenseStack(p_82482_1_, p_82482_2_);
			this.playDispenseSound(p_82482_1_);
			this.spawnDispenseParticles(p_82482_1_, BlockDispenser.func_149937_b(p_82482_1_.BlockMetadata));
			return var3;
		}

///    
///     <summary> * Dispense the specified stack, play the dispense sound and spawn particles. </summary>
///     
		protected internal virtual ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
		{
			EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.BlockMetadata);
			IPosition var4 = BlockDispenser.func_149939_a(p_82487_1_);
			ItemStack var5 = p_82487_2_.splitStack(1);
			doDispense(p_82487_1_.World, var5, 6, var3, var4);
			return p_82487_2_;
		}

		public static void doDispense(World p_82486_0_, ItemStack p_82486_1_, int p_82486_2_, EnumFacing p_82486_3_, IPosition p_82486_4_)
		{
			double var5 = p_82486_4_.X;
			double var7 = p_82486_4_.Y;
			double var9 = p_82486_4_.Z;
			EntityItem var11 = new EntityItem(p_82486_0_, var5, var7 - 0.3D, var9, p_82486_1_);
			double var12 = p_82486_0_.rand.NextDouble() * 0.1D + 0.2D;
			var11.motionX = (double)p_82486_3_.FrontOffsetX * var12;
			var11.motionY = 0.20000000298023224D;
			var11.motionZ = (double)p_82486_3_.FrontOffsetZ * var12;
			var11.motionX += p_82486_0_.rand.nextGaussian() * 0.007499999832361937D * (double)p_82486_2_;
			var11.motionY += p_82486_0_.rand.nextGaussian() * 0.007499999832361937D * (double)p_82486_2_;
			var11.motionZ += p_82486_0_.rand.nextGaussian() * 0.007499999832361937D * (double)p_82486_2_;
			p_82486_0_.spawnEntityInWorld(var11);
		}

///    
///     <summary> * Play the dispense sound from the specified block. </summary>
///     
		protected internal virtual void playDispenseSound(IBlockSource p_82485_1_)
		{
			p_82485_1_.World.playAuxSFX(1000, p_82485_1_.XInt, p_82485_1_.YInt, p_82485_1_.ZInt, 0);
		}

///    
///     <summary> * Order clients to display dispense particles from the specified block and facing. </summary>
///     
		protected internal virtual void spawnDispenseParticles(IBlockSource p_82489_1_, EnumFacing p_82489_2_)
		{
			p_82489_1_.World.playAuxSFX(2000, p_82489_1_.XInt, p_82489_1_.YInt, p_82489_1_.ZInt, this.func_82488_a(p_82489_2_));
		}

		private int func_82488_a(EnumFacing p_82488_1_)
		{
			return p_82488_1_.FrontOffsetX + 1 + (p_82488_1_.FrontOffsetZ + 1) * 3;
		}
	}

}