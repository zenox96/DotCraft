using System;

namespace DotCraftCore.Entity.AI
{

	using Block = DotCraftCore.block.Block;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using Blocks = DotCraftCore.Init.Blocks;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public class EntityAIEatGrass : EntityAIBase
	{
		private EntityLiving field_151500_b;
		private World field_151501_c;
		internal int field_151502_a;
		private const string __OBFID = "CL_00001582";

		public EntityAIEatGrass(EntityLiving p_i45314_1_)
		{
			this.field_151500_b = p_i45314_1_;
			this.field_151501_c = p_i45314_1_.worldObj;
			this.MutexBits = 7;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.field_151500_b.RNG.Next(this.field_151500_b.Child ? 50 : 1000) != 0)
			{
				return false;
			}
			else
			{
				int var1 = MathHelper.floor_double(this.field_151500_b.posX);
				int var2 = MathHelper.floor_double(this.field_151500_b.posY);
				int var3 = MathHelper.floor_double(this.field_151500_b.posZ);
				return this.field_151501_c.getBlock(var1, var2, var3) == Blocks.tallgrass && this.field_151501_c.getBlockMetadata(var1, var2, var3) == 1 ? true : this.field_151501_c.getBlock(var1, var2 - 1, var3) == Blocks.grass;
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.field_151502_a = 40;
			this.field_151501_c.setEntityState(this.field_151500_b, (sbyte)10);
			this.field_151500_b.Navigator.clearPathEntity();
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.field_151502_a = 0;
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.field_151502_a > 0;
		}

		public virtual int func_151499_f()
		{
			return this.field_151502_a;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.field_151502_a = Math.Max(0, this.field_151502_a - 1);

			if (this.field_151502_a == 4)
			{
				int var1 = MathHelper.floor_double(this.field_151500_b.posX);
				int var2 = MathHelper.floor_double(this.field_151500_b.posY);
				int var3 = MathHelper.floor_double(this.field_151500_b.posZ);

				if (this.field_151501_c.getBlock(var1, var2, var3) == Blocks.tallgrass)
				{
					if (this.field_151501_c.GameRules.getGameRuleBooleanValue("mobGriefing"))
					{
						this.field_151501_c.func_147480_a(var1, var2, var3, false);
					}

					this.field_151500_b.eatGrassBonus();
				}
				else if (this.field_151501_c.getBlock(var1, var2 - 1, var3) == Blocks.grass)
				{
					if (this.field_151501_c.GameRules.getGameRuleBooleanValue("mobGriefing"))
					{
						this.field_151501_c.playAuxSFX(2001, var1, var2 - 1, var3, Block.getIdFromBlock(Blocks.grass));
						this.field_151501_c.setBlock(var1, var2 - 1, var3, Blocks.dirt, 0, 2);
					}

					this.field_151500_b.eatGrassBonus();
				}
			}
		}
	}

}