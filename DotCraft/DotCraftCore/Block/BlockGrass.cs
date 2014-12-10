using System;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Blocks = DotCraftCore.Init.Blocks;
	using Item = DotCraftCore.Item.Item;
	using IIcon = DotCraftCore.Util.IIcon;
	using ColorizerGrass = DotCraftCore.World.ColorizerGrass;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class BlockGrass : Block, IGrowable
	{
		private static readonly Logger logger = LogManager.Logger;
		private IIcon field_149991_b;
		private IIcon field_149993_M;
		private IIcon field_149994_N;
		private const string __OBFID = "CL_00000251";

		protected internal BlockGrass() : base(Material.grass)
		{
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 1 ? this.field_149991_b : (p_149691_1_ == 0 ? Blocks.dirt.getBlockTextureFromSide(p_149691_1_) : this.blockIcon);
		}

		public virtual IIcon getIcon(IBlockAccess p_149673_1_, int p_149673_2_, int p_149673_3_, int p_149673_4_, int p_149673_5_)
		{
			if (p_149673_5_ == 1)
			{
				return this.field_149991_b;
			}
			else if (p_149673_5_ == 0)
			{
				return Blocks.dirt.getBlockTextureFromSide(p_149673_5_);
			}
			else
			{
				Material var6 = p_149673_1_.getBlock(p_149673_2_, p_149673_3_ + 1, p_149673_4_).Material;
				return var6 != Material.field_151597_y && var6 != Material.craftedSnow ? this.blockIcon : this.field_149993_M;
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
			this.field_149991_b = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.field_149993_M = p_149651_1_.registerIcon(this.TextureName + "_side_snowed");
			this.field_149994_N = p_149651_1_.registerIcon(this.TextureName + "_side_overlay");
		}

		public virtual int BlockColor
		{
			get
			{
				double var1 = 0.5D;
				double var3 = 1.0D;
				return ColorizerGrass.getGrassColor(var1, var3);
			}
		}

///    
///     <summary> * Returns the color this block should be rendered. Used by leaves. </summary>
///     
		public virtual int getRenderColor(int p_149741_1_)
		{
			return this.BlockColor;
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public virtual int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			int var5 = 0;
			int var6 = 0;
			int var7 = 0;

			for (int var8 = -1; var8 <= 1; ++var8)
			{
				for (int var9 = -1; var9 <= 1; ++var9)
				{
					int var10 = p_149720_1_.getBiomeGenForCoords(p_149720_2_ + var9, p_149720_4_ + var8).getBiomeGrassColor(p_149720_2_ + var9, p_149720_3_, p_149720_4_ + var8);
					var5 += (var10 & 16711680) >> 16;
					var6 += (var10 & 65280) >> 8;
					var7 += var10 & 255;
				}
			}

			return (var5 / 9 & 255) << 16 | (var6 / 9 & 255) << 8 | var7 / 9 & 255;
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
							p_149674_1_.setBlock(var7, var8, var9, Blocks.grass);
						}
					}
				}
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Blocks.dirt.getItemDropped(0, p_149650_2_, p_149650_3_);
		}

		public static IIcon func_149990_e()
		{
			return Blocks.grass.field_149994_N;
		}

		public virtual bool func_149851_a(World p_149851_1_, int p_149851_2_, int p_149851_3_, int p_149851_4_, bool p_149851_5_)
		{
			return true;
		}

		public virtual bool func_149852_a(World p_149852_1_, Random p_149852_2_, int p_149852_3_, int p_149852_4_, int p_149852_5_)
		{
			return true;
		}

		public virtual void func_149853_b(World p_149853_1_, Random p_149853_2_, int p_149853_3_, int p_149853_4_, int p_149853_5_)
		{
			int var6 = 0;

			while (var6 < 128)
			{
				int var7 = p_149853_3_;
				int var8 = p_149853_4_ + 1;
				int var9 = p_149853_5_;
				int var10 = 0;

				while (true)
				{
					if (var10 < var6 / 16)
					{
						var7 += p_149853_2_.Next(3) - 1;
						var8 += (p_149853_2_.Next(3) - 1) * p_149853_2_.Next(3) / 2;
						var9 += p_149853_2_.Next(3) - 1;

						if (p_149853_1_.getBlock(var7, var8 - 1, var9) == Blocks.grass && !p_149853_1_.getBlock(var7, var8, var9).NormalCube)
						{
							++var10;
							continue;
						}
					}
					else if (p_149853_1_.getBlock(var7, var8, var9).blockMaterial == Material.air)
					{
						if (p_149853_2_.Next(8) != 0)
						{
							if (Blocks.tallgrass.canBlockStay(p_149853_1_, var7, var8, var9))
							{
								p_149853_1_.setBlock(var7, var8, var9, Blocks.tallgrass, 1, 3);
							}
						}
						else
						{
							string var13 = p_149853_1_.getBiomeGenForCoords(var7, var9).func_150572_a(p_149853_2_, var7, var8, var9);
							logger.debug("Flower in " + p_149853_1_.getBiomeGenForCoords(var7, var9).biomeName + ": " + var13);
							BlockFlower var11 = BlockFlower.func_149857_e(var13);

							if (var11 != null && var11.canBlockStay(p_149853_1_, var7, var8, var9))
							{
								int var12 = BlockFlower.func_149856_f(var13);
								p_149853_1_.setBlock(var7, var8, var9, var11, var12, 3);
							}
						}
					}

					++var6;
					break;
				}
			}
		}
	}

}