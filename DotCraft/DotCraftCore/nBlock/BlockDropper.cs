namespace DotCraftCore.nBlock
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using BehaviorDefaultDispenseItem = DotCraftCore.nDispenser.BehaviorDefaultDispenseItem;
	using IBehaviorDispenseItem = DotCraftCore.nDispenser.IBehaviorDispenseItem;
	using IInventory = DotCraftCore.nInventory.IInventory;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using TileEntity = DotCraftCore.nTileEntity.TileEntity;
	using TileEntityDispenser = DotCraftCore.nTileEntity.TileEntityDispenser;
	using TileEntityDropper = DotCraftCore.nTileEntity.TileEntityDropper;
	using TileEntityHopper = DotCraftCore.nTileEntity.TileEntityHopper;
	using Facing = DotCraftCore.nUtil.Facing;
	using World = DotCraftCore.nWorld.World;

	public class BlockDropper : BlockDispenser
	{
		private readonly IBehaviorDispenseItem field_149947_P = new BehaviorDefaultDispenseItem();
		

		public override void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon("furnace_side");
			this.field_149944_M = p_149651_1_.registerIcon("furnace_top");
			this.field_149945_N = p_149651_1_.registerIcon(this.TextureName + "_front_horizontal");
			this.field_149946_O = p_149651_1_.registerIcon(this.TextureName + "_front_vertical");
		}

		protected internal override IBehaviorDispenseItem func_149940_a(ItemStack p_149940_1_)
		{
			return this.field_149947_P;
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public override TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityDropper();
		}

		protected internal override void func_149941_e(World p_149941_1_, int p_149941_2_, int p_149941_3_, int p_149941_4_)
		{
			BlockSourceImpl var5 = new BlockSourceImpl(p_149941_1_, p_149941_2_, p_149941_3_, p_149941_4_);
			TileEntityDispenser var6 = (TileEntityDispenser)var5.BlockTileEntity;

			if (var6 != null)
			{
				int var7 = var6.func_146017_i();

				if (var7 < 0)
				{
					p_149941_1_.playAuxSFX(1001, p_149941_2_, p_149941_3_, p_149941_4_, 0);
				}
				else
				{
					ItemStack var8 = var6.getStackInSlot(var7);
					int var9 = p_149941_1_.getBlockMetadata(p_149941_2_, p_149941_3_, p_149941_4_) & 7;
					IInventory var10 = TileEntityHopper.func_145893_b(p_149941_1_, (double)(p_149941_2_ + Facing.offsetsXForSide[var9]), (double)(p_149941_3_ + Facing.offsetsYForSide[var9]), (double)(p_149941_4_ + Facing.offsetsZForSide[var9]));
					ItemStack var11;

					if (var10 != null)
					{
						var11 = TileEntityHopper.func_145889_a(var10, var8.copy().splitStack(1), Facing.oppositeSide[var9]);

						if (var11 == null)
						{
							var11 = var8.copy();

							if (--var11.stackSize == 0)
							{
								var11 = null;
							}
						}
						else
						{
							var11 = var8.copy();
						}
					}
					else
					{
						var11 = this.field_149947_P.dispense(var5, var8);

						if (var11 != null && var11.stackSize == 0)
						{
							var11 = null;
						}
					}

					var6.setInventorySlotContents(var7, var11);
				}
			}
		}
	}

}