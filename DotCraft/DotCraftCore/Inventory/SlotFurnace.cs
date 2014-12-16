using System;
using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityXPOrb = DotCraftCore.Entity.Item.EntityXPOrb;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using FurnaceRecipes = DotCraftCore.Item.Crafting.FurnaceRecipes;
	using AchievementList = DotCraftCore.Stats.AchievementList;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class SlotFurnace : Slot
	{
	/// <summary> The player that is using the GUI where this slot resides.  </summary>
		private EntityPlayer thePlayer;
		private int field_75228_b;
		

		public SlotFurnace(EntityPlayer p_i1813_1_, IInventory p_i1813_2_, int p_i1813_3_, int p_i1813_4_, int p_i1813_5_) : base(p_i1813_2_, p_i1813_3_, p_i1813_4_, p_i1813_5_)
		{
			this.thePlayer = p_i1813_1_;
		}

///    
///     <summary> * Check if the stack is a valid item for this slot. Always true beside for the armor slots. </summary>
///     
		public override bool isItemValid(ItemStack p_75214_1_)
		{
			return false;
		}

///    
///     <summary> * Decrease the size of the stack in slot (first int arg) by the amount of the second int arg. Returns the new
///     * stack. </summary>
///     
		public override ItemStack decrStackSize(int p_75209_1_)
		{
			if (this.HasStack)
			{
				this.field_75228_b += Math.Min(p_75209_1_, this.Stack.stackSize);
			}

			return base.decrStackSize(p_75209_1_);
		}

		public override void onPickupFromSlot(EntityPlayer p_82870_1_, ItemStack p_82870_2_)
		{
			this.onCrafting(p_82870_2_);
			base.onPickupFromSlot(p_82870_1_, p_82870_2_);
		}

///    
///     <summary> * the itemStack passed in is the output - ie, iron ingots, and pickaxes, not ore and wood. Typically increases an
///     * internal count then calls onCrafting(item). </summary>
///     
		protected internal override void onCrafting(ItemStack p_75210_1_, int p_75210_2_)
		{
			this.field_75228_b += p_75210_2_;
			this.onCrafting(p_75210_1_);
		}

///    
///     <summary> * the itemStack passed in is the output - ie, iron ingots, and pickaxes, not ore and wood. </summary>
///     
		protected internal override void onCrafting(ItemStack p_75208_1_)
		{
			p_75208_1_.onCrafting(this.thePlayer.worldObj, this.thePlayer, this.field_75228_b);

			if (!this.thePlayer.worldObj.isClient)
			{
				int var2 = this.field_75228_b;
				float var3 = FurnaceRecipes.smelting().func_151398_b(p_75208_1_);
				int var4;

				if (var3 == 0.0F)
				{
					var2 = 0;
				}
				else if (var3 < 1.0F)
				{
					var4 = MathHelper.floor_float((float)var2 * var3);

					if (var4 < MathHelper.ceiling_float_int((float)var2 * var3) && (float)new Random(1).NextDouble() < (float)var2 * var3 - (float)var4)
					{
						++var4;
					}

					var2 = var4;
				}

				while (var2 > 0)
				{
					var4 = EntityXPOrb.getXPSplit(var2);
					var2 -= var4;
					this.thePlayer.worldObj.spawnEntityInWorld(new EntityXPOrb(this.thePlayer.worldObj, this.thePlayer.posX, this.thePlayer.posY + 0.5D, this.thePlayer.posZ + 0.5D, var4));
				}
			}

			this.field_75228_b = 0;

			if (p_75208_1_.Item == Items.iron_ingot)
			{
				this.thePlayer.addStat(AchievementList.acquireIron, 1);
			}

			if (p_75208_1_.Item == Items.cooked_fished)
			{
				this.thePlayer.addStat(AchievementList.cookFish, 1);
			}
		}
	}

}