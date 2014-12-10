using System;
using System.Collections;

namespace DotCraftCore.Block
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Blocks = DotCraftCore.init.Blocks;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using IIcon = DotCraftCore.util.IIcon;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;
	using WorldGenBigTree = DotCraftCore.world.gen.feature.WorldGenBigTree;
	using WorldGenCanopyTree = DotCraftCore.world.gen.feature.WorldGenCanopyTree;
	using WorldGenForest = DotCraftCore.world.gen.feature.WorldGenForest;
	using WorldGenMegaJungle = DotCraftCore.world.gen.feature.WorldGenMegaJungle;
	using WorldGenMegaPineTree = DotCraftCore.world.gen.feature.WorldGenMegaPineTree;
	using WorldGenSavannaTree = DotCraftCore.world.gen.feature.WorldGenSavannaTree;
	using WorldGenTaiga2 = DotCraftCore.world.gen.feature.WorldGenTaiga2;
	using WorldGenTrees = DotCraftCore.world.gen.feature.WorldGenTrees;
	using WorldGenerator = DotCraftCore.world.gen.feature.WorldGenerator;

	public class BlockSapling : BlockBush, IGrowable
	{
		public static readonly string[] field_149882_a = new string[] {"oak", "spruce", "birch", "jungle", "acacia", "roofed_oak"};
		private static readonly IIcon[] field_149881_b = new IIcon[field_149882_a.Length];
		private const string __OBFID = "CL_00000305";

		protected internal BlockSapling()
		{
			float var1 = 0.4F;
			this.setBlockBounds(0.5F - var1, 0.0F, 0.5F - var1, 0.5F + var1, var1 * 2.0F, 0.5F + var1);
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!p_149674_1_.isClient)
			{
				base.updateTick(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_5_);

				if (p_149674_1_.getBlockLightValue(p_149674_2_, p_149674_3_ + 1, p_149674_4_) >= 9 && p_149674_5_.Next(7) == 0)
				{
					this.func_149879_c(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_5_);
				}
			}
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			p_149691_2_ &= 7;
			return field_149881_b[MathHelper.clamp_int(p_149691_2_, 0, 5)];
		}

		public virtual void func_149879_c(World p_149879_1_, int p_149879_2_, int p_149879_3_, int p_149879_4_, Random p_149879_5_)
		{
			int var6 = p_149879_1_.getBlockMetadata(p_149879_2_, p_149879_3_, p_149879_4_);

			if ((var6 & 8) == 0)
			{
				p_149879_1_.setBlockMetadataWithNotify(p_149879_2_, p_149879_3_, p_149879_4_, var6 | 8, 4);
			}
			else
			{
				this.func_149878_d(p_149879_1_, p_149879_2_, p_149879_3_, p_149879_4_, p_149879_5_);
			}
		}

		public virtual void func_149878_d(World p_149878_1_, int p_149878_2_, int p_149878_3_, int p_149878_4_, Random p_149878_5_)
		{
			int var6 = p_149878_1_.getBlockMetadata(p_149878_2_, p_149878_3_, p_149878_4_) & 7;
			object var7 = p_149878_5_.Next(10) == 0 ? new WorldGenBigTree(true) : new WorldGenTrees(true);
			int var8 = 0;
			int var9 = 0;
			bool var10 = false;

			switch (var6)
			{
				case 0:
				default:
					break;

				case 1:
					label78:
					for (var8 = 0; var8 >= -1; --var8)
					{
						for (var9 = 0; var9 >= -1; --var9)
						{
							if (this.func_149880_a(p_149878_1_, p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9, 1) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9, 1) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9 + 1, 1) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9 + 1, 1))
							{
								var7 = new WorldGenMegaPineTree(false, p_149878_5_.nextBoolean());
								var10 = true;
								goto label78;
							}
						}
					}

					if (!var10)
					{
						var9 = 0;
						var8 = 0;
						var7 = new WorldGenTaiga2(true);
					}

					break;

				case 2:
					var7 = new WorldGenForest(true, false);
					break;

				case 3:
					label93:
					for (var8 = 0; var8 >= -1; --var8)
					{
						for (var9 = 0; var9 >= -1; --var9)
						{
							if (this.func_149880_a(p_149878_1_, p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9, 3) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9, 3) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9 + 1, 3) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9 + 1, 3))
							{
								var7 = new WorldGenMegaJungle(true, 10, 20, 3, 3);
								var10 = true;
								goto label93;
							}
						}
					}

					if (!var10)
					{
						var9 = 0;
						var8 = 0;
						var7 = new WorldGenTrees(true, 4 + p_149878_5_.Next(7), 3, 3, false);
					}

					break;

				case 4:
					var7 = new WorldGenSavannaTree(true);
					break;

				case 5:
					label108:
					for (var8 = 0; var8 >= -1; --var8)
					{
						for (var9 = 0; var9 >= -1; --var9)
						{
							if (this.func_149880_a(p_149878_1_, p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9, 5) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9, 5) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9 + 1, 5) && this.func_149880_a(p_149878_1_, p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9 + 1, 5))
							{
								var7 = new WorldGenCanopyTree(true);
								var10 = true;
								goto label108;
							}
						}
					}

					if (!var10)
					{
						return;
					}
			}

			Block var11 = Blocks.air;

			if (var10)
			{
				p_149878_1_.setBlock(p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9, var11, 0, 4);
				p_149878_1_.setBlock(p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9, var11, 0, 4);
				p_149878_1_.setBlock(p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9 + 1, var11, 0, 4);
				p_149878_1_.setBlock(p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9 + 1, var11, 0, 4);
			}
			else
			{
				p_149878_1_.setBlock(p_149878_2_, p_149878_3_, p_149878_4_, var11, 0, 4);
			}

			if (!((WorldGenerator)var7).generate(p_149878_1_, p_149878_5_, p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9))
			{
				if (var10)
				{
					p_149878_1_.setBlock(p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9, this, var6, 4);
					p_149878_1_.setBlock(p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9, this, var6, 4);
					p_149878_1_.setBlock(p_149878_2_ + var8, p_149878_3_, p_149878_4_ + var9 + 1, this, var6, 4);
					p_149878_1_.setBlock(p_149878_2_ + var8 + 1, p_149878_3_, p_149878_4_ + var9 + 1, this, var6, 4);
				}
				else
				{
					p_149878_1_.setBlock(p_149878_2_, p_149878_3_, p_149878_4_, this, var6, 4);
				}
			}
		}

		public virtual bool func_149880_a(World p_149880_1_, int p_149880_2_, int p_149880_3_, int p_149880_4_, int p_149880_5_)
		{
			return p_149880_1_.getBlock(p_149880_2_, p_149880_3_, p_149880_4_) == this && (p_149880_1_.getBlockMetadata(p_149880_2_, p_149880_3_, p_149880_4_) & 7) == p_149880_5_;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return MathHelper.clamp_int(p_149692_1_ & 7, 0, 5);
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 2));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 3));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 4));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 5));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			for (int var2 = 0; var2 < field_149881_b.Length; ++var2)
			{
				field_149881_b[var2] = p_149651_1_.registerIcon(this.TextureName + "_" + field_149882_a[var2]);
			}
		}

		public virtual bool func_149851_a(World p_149851_1_, int p_149851_2_, int p_149851_3_, int p_149851_4_, bool p_149851_5_)
		{
			return true;
		}

		public virtual bool func_149852_a(World p_149852_1_, Random p_149852_2_, int p_149852_3_, int p_149852_4_, int p_149852_5_)
		{
			return (double)p_149852_1_.rand.nextFloat() < 0.45D;
		}

		public virtual void func_149853_b(World p_149853_1_, Random p_149853_2_, int p_149853_3_, int p_149853_4_, int p_149853_5_)
		{
			this.func_149879_c(p_149853_1_, p_149853_3_, p_149853_4_, p_149853_5_, p_149853_2_);
		}
	}

}