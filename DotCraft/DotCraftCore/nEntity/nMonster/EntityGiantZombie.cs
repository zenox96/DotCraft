namespace DotCraftCore.nEntity.nMonster
{

	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using World = DotCraftCore.nWorld.World;

	public class EntityGiantZombie : EntityMob
	{
		

		public EntityGiantZombie(World p_i1736_1_) : base(p_i1736_1_)
		{
			this.yOffset *= 6.0F;
			this.setSize(this.width * 6.0F, this.height * 6.0F);
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 100.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.5D;
			this.getEntityAttribute(SharedMonsterAttributes.attackDamage).BaseValue = 50.0D;
		}

///    
///     <summary> * Takes a coordinate in and returns a weight to determine how likely this creature will try to path to the block.
///     * Args: x, y, z </summary>
///     
		public override float getBlockPathWeight(int p_70783_1_, int p_70783_2_, int p_70783_3_)
		{
			return this.worldObj.getLightBrightness(p_70783_1_, p_70783_2_, p_70783_3_) - 0.5F;
		}
	}

}