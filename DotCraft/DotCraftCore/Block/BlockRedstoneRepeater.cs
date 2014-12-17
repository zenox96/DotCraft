using System;

namespace DotCraftCore.nBlock
{

	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using IBlockAccess = DotCraftCore.nWorld.IBlockAccess;
	using World = DotCraftCore.nWorld.World;

	public class BlockRedstoneRepeater : BlockRedstoneDiode
	{
		public static readonly double[] field_149973_b = new double[] { -0.0625D, 0.0625D, 0.1875D, 0.3125D};
		private static readonly int[] field_149974_M = new int[] {1, 2, 3, 4};
		

		protected internal BlockRedstoneRepeater(bool p_i45424_1_) : base(p_i45424_1_)
		{
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			int var10 = p_149727_1_.getBlockMetadata(p_149727_2_, p_149727_3_, p_149727_4_);
			int var11 = (var10 & 12) >> 2;
			var11 = var11 + 1 << 2 & 12;
			p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_, p_149727_4_, var11 | var10 & 3, 3);
			return true;
		}

		protected internal override int func_149901_b(int p_149901_1_)
		{
			return field_149974_M[(p_149901_1_ & 12) >> 2] * 2;
		}

		protected internal override BlockRedstoneDiode func_149906_e()
		{
			return Blocks.powered_repeater;
		}

		protected internal override BlockRedstoneDiode func_149898_i()
		{
			return Blocks.unpowered_repeater;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.repeater;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Items.repeater;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return 15;
			}
		}

		public override bool func_149910_g(IBlockAccess p_149910_1_, int p_149910_2_, int p_149910_3_, int p_149910_4_, int p_149910_5_)
		{
			return this.func_149902_h(p_149910_1_, p_149910_2_, p_149910_3_, p_149910_4_, p_149910_5_) > 0;
		}

		protected internal override bool func_149908_a(Block p_149908_1_)
		{
			return func_149909_d(p_149908_1_);
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public virtual void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			if (this.field_149914_a)
			{
				int var6 = p_149734_1_.getBlockMetadata(p_149734_2_, p_149734_3_, p_149734_4_);
				int var7 = func_149895_l(var6);
				double var8 = (double)((float)p_149734_2_ + 0.5F) + (double)(p_149734_5_.nextFloat() - 0.5F) * 0.2D;
				double var10 = (double)((float)p_149734_3_ + 0.4F) + (double)(p_149734_5_.nextFloat() - 0.5F) * 0.2D;
				double var12 = (double)((float)p_149734_4_ + 0.5F) + (double)(p_149734_5_.nextFloat() - 0.5F) * 0.2D;
				double var14 = 0.0D;
				double var16 = 0.0D;

				if (p_149734_5_.Next(2) == 0)
				{
					switch (var7)
					{
						case 0:
							var16 = -0.3125D;
							break;

						case 1:
							var14 = 0.3125D;
							break;

						case 2:
							var16 = 0.3125D;
							break;

						case 3:
							var14 = -0.3125D;
						break;
					}
				}
				else
				{
					int var18 = (var6 & 12) >> 2;

					switch (var7)
					{
						case 0:
							var16 = field_149973_b[var18];
							break;

						case 1:
							var14 = -field_149973_b[var18];
							break;

						case 2:
							var16 = -field_149973_b[var18];
							break;

						case 3:
							var14 = field_149973_b[var18];
						break;
					}
				}

				p_149734_1_.spawnParticle("reddust", var8 + var14, var10, var12 + var16, 0.0D, 0.0D, 0.0D);
			}
		}

		public virtual void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
			this.func_149911_e(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_);
		}
	}

}