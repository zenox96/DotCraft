namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPig = DotCraftCore.entity.passive.EntityPig;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using World = DotCraftCore.World.World;

	public class ItemCarrotOnAStick : Item
	{
		private const string __OBFID = "CL_00000001";

		public ItemCarrotOnAStick()
		{
			this.CreativeTab = CreativeTabs.tabTransport;
			this.MaxStackSize = 1;
			this.MaxDamage = 25;
		}

///    
///     <summary> * Returns True is the item is renderer in full 3D when hold. </summary>
///     
		public virtual bool isFull3D()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Returns true if this item should be rotated by 180 degrees around the Y axis when being held in an entities
///     * hands. </summary>
///     
		public virtual bool shouldRotateAroundWhenRendering()
		{
			return true;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			if (p_77659_3_.Riding && p_77659_3_.ridingEntity is EntityPig)
			{
				EntityPig var4 = (EntityPig)p_77659_3_.ridingEntity;

				if (var4.AIControlledByPlayer.ControlledByPlayer && p_77659_1_.MaxDamage - p_77659_1_.ItemDamage >= 7)
				{
					var4.AIControlledByPlayer.boostSpeed();
					p_77659_1_.damageItem(7, p_77659_3_);

					if (p_77659_1_.stackSize == 0)
					{
						ItemStack var5 = new ItemStack(Items.fishing_rod);
						var5.TagCompound = p_77659_1_.stackTagCompound;
						return var5;
					}
				}
			}

			return p_77659_1_;
		}
	}

}