using System;

namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Item = DotCraftCore.nItem.Item;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using IBlockAccess = DotCraftCore.nWorld.IBlockAccess;
	using World = DotCraftCore.nWorld.World;

	public class BlockMycelium : Block
	{
		private IIcon field_150200_a;
		private IIcon field_150199_b;
		

		protected internal BlockMycelium() : base(Material.grass)
		{
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 1 ? this.field_150200_a : (p_149691_1_ == 0 ? Blocks.dirt.getBlockTextureFromSide(p_149691_1_) : this.blockIcon);
		}

		public virtual IIcon getIcon(IBlockAccess p_149673_1_, int p_149673_2_, int p_149673_3_, int p_149673_4_, int p_149673_5_)
		{
			if (p_149673_5_ == 1)
			{
				return this.field_150200_a;
			}
			else if (p_149673_5_ == 0)
			{
				return Blocks.dirt.getBlockTextureFromSide(p_149673_5_);
			}
			else
			{
				Material var6 = p_149673_1_.getBlock(p_149673_2_, p_149673_3_ + 1, p_149673_4_).Material;
				return var6 != Material.field_151597_y && var6 != Material.craftedSnow ? this.blockIcon : this.field_150199_b;
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
			this.field_150200_a = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.field_150199_b = p_149651_1_.registerIcon("grass_side_snowed");
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!p_149674_1_.isClient)
			{
				if (p_149674_1_.getBlockLightValue(p_149674_2_, p_149674_3_ + 1, p_149674_4_) < 4 && p_149674_1_.getBlock(p_149674_2_, p_149674_3_ + 1, p_149674_4_).LightOpacity > 2)
				{
					p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, Blocks.dirt);
				}
				else if (p_149674_1_.getBlockLightValue(p_149674_2_, p_149674_3_ + 1, p_149674_4_) >= 9)
				{
					for (int var6 = 0; var6 < 4; ++var6)
					{
						int var7 = p_149674_2_ + p_149674_5_.Next(3) - 1;
						int var8 = p_149674_3_ + p_149674_5_.Next(5) - 3;
						int var9 = p_149674_4_ + p_149674_5_.Next(3) - 1;
						Block var10 = p_149674_1_.getBlock(var7, var8 + 1, var9);

						if (p_149674_1_.getBlock(var7, var8, var9) == Blocks.dirt && p_149674_1_.getBlockMetadata(var7, var8, var9) == 0 && p_149674_1_.getBlockLightValue(var7, var8 + 1, var9) >= 4 && var10.LightOpacity <= 2)
						{
							p_149674_1_.setBlock(var7, var8, var9, this);
						}
					}
				}
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public virtual void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			base.randomDisplayTick(p_149734_1_, p_149734_2_, p_149734_3_, p_149734_4_, p_149734_5_);

			if (p_149734_5_.Next(10) == 0)
			{
				p_149734_1_.spawnParticle("townaura", (double)((float)p_149734_2_ + p_149734_5_.nextFloat()), (double)((float)p_149734_3_ + 1.1F), (double)((float)p_149734_4_ + p_149734_5_.nextFloat()), 0.0D, 0.0D, 0.0D);
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Blocks.dirt.getItemDropped(0, p_149650_2_, p_149650_3_);
		}
	}

}