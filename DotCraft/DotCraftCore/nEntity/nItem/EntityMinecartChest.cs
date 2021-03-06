namespace DotCraftCore.nEntity.nItem
{

	using Block = DotCraftCore.nBlock.Block;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Item = DotCraftCore.nItem.Item;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using World = DotCraftCore.nWorld.World;

	public class EntityMinecartChest : EntityMinecartContainer
	{
		

		public EntityMinecartChest(World p_i1714_1_) : base(p_i1714_1_)
		{
		}

		public EntityMinecartChest(World p_i1715_1_, double p_i1715_2_, double p_i1715_4_, double p_i1715_6_) : base(p_i1715_1_, p_i1715_2_, p_i1715_4_, p_i1715_6_)
		{
		}

		public override void killMinecart(DamageSource p_94095_1_)
		{
			base.killMinecart(p_94095_1_);
			this.func_145778_a(Item.getItemFromBlock(Blocks.chest), 1, 0.0F);
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return 27;
			}
		}

		public override int MinecartType
		{
			get
			{
				return 1;
			}
		}

		public override Block func_145817_o()
		{
			return Blocks.chest;
		}

		public override int DefaultDisplayTileOffset
		{
			get
			{
				return 8;
			}
		}
	}

}