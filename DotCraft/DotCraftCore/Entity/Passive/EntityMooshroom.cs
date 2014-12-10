namespace DotCraftCore.Entity.Passive
{

	using EntityAgeable = DotCraftCore.Entity.EntityAgeable;
	using EntityItem = DotCraftCore.Entity.Item.EntityItem;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.item.ItemStack;
	using World = DotCraftCore.world.World;

	public class EntityMooshroom : EntityCow
	{
		private const string __OBFID = "CL_00001645";

		public EntityMooshroom(World p_i1687_1_) : base(p_i1687_1_)
		{
			this.setSize(0.9F, 1.3F);
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (var2 != null && var2.Item == Items.bowl && this.GrowingAge >= 0)
			{
				if (var2.stackSize == 1)
				{
					p_70085_1_.inventory.setInventorySlotContents(p_70085_1_.inventory.currentItem, new ItemStack(Items.mushroom_stew));
					return true;
				}

				if (p_70085_1_.inventory.addItemStackToInventory(new ItemStack(Items.mushroom_stew)) && !p_70085_1_.capabilities.isCreativeMode)
				{
					p_70085_1_.inventory.decrStackSize(p_70085_1_.inventory.currentItem, 1);
					return true;
				}
			}

			if (var2 != null && var2.Item == Items.shears && this.GrowingAge >= 0)
			{
				this.setDead();
				this.worldObj.spawnParticle("largeexplode", this.posX, this.posY + (double)(this.height / 2.0F), this.posZ, 0.0D, 0.0D, 0.0D);

				if (!this.worldObj.isClient)
				{
					EntityCow var3 = new EntityCow(this.worldObj);
					var3.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);
					var3.Health = this.Health;
					var3.renderYawOffset = this.renderYawOffset;
					this.worldObj.spawnEntityInWorld(var3);

					for (int var4 = 0; var4 < 5; ++var4)
					{
						this.worldObj.spawnEntityInWorld(new EntityItem(this.worldObj, this.posX, this.posY + (double)this.height, this.posZ, new ItemStack(Blocks.red_mushroom)));
					}

					var2.damageItem(1, p_70085_1_);
					this.playSound("mob.sheep.shear", 1.0F, 1.0F);
				}

				return true;
			}
			else
			{
				return base.interact(p_70085_1_);
			}
		}

		public override EntityMooshroom createChild(EntityAgeable p_90011_1_)
		{
			return new EntityMooshroom(this.worldObj);
		}
	}

}