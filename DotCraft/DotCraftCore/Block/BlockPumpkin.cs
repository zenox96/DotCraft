namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EntityIronGolem = DotCraftCore.nEntity.nMonster.EntityIronGolem;
	using EntitySnowman = DotCraftCore.nEntity.nMonster.EntitySnowman;
	using Blocks = DotCraftCore.nInit.Blocks;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class BlockPumpkin : BlockDirectional
	{
		private bool field_149985_a;
		private IIcon field_149984_b;
		private IIcon field_149986_M;
		

		protected internal BlockPumpkin(bool p_i45419_1_) : base(Material.field_151572_C)
		{
			this.TickRandomly = true;
			this.field_149985_a = p_i45419_1_;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 1 ? this.field_149984_b : (p_149691_1_ == 0 ? this.field_149984_b : (p_149691_2_ == 2 && p_149691_1_ == 2 ? this.field_149986_M : (p_149691_2_ == 3 && p_149691_1_ == 5 ? this.field_149986_M : (p_149691_2_ == 0 && p_149691_1_ == 3 ? this.field_149986_M : (p_149691_2_ == 1 && p_149691_1_ == 4 ? this.field_149986_M : this.blockIcon)))));
		}

		public virtual void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);

			if (p_149726_1_.getBlock(p_149726_2_, p_149726_3_ - 1, p_149726_4_) == Blocks.snow && p_149726_1_.getBlock(p_149726_2_, p_149726_3_ - 2, p_149726_4_) == Blocks.snow)
			{
				if (!p_149726_1_.isClient)
				{
					p_149726_1_.setBlock(p_149726_2_, p_149726_3_, p_149726_4_, getBlockById(0), 0, 2);
					p_149726_1_.setBlock(p_149726_2_, p_149726_3_ - 1, p_149726_4_, getBlockById(0), 0, 2);
					p_149726_1_.setBlock(p_149726_2_, p_149726_3_ - 2, p_149726_4_, getBlockById(0), 0, 2);
					EntitySnowman var9 = new EntitySnowman(p_149726_1_);
					var9.setLocationAndAngles((double)p_149726_2_ + 0.5D, (double)p_149726_3_ - 1.95D, (double)p_149726_4_ + 0.5D, 0.0F, 0.0F);
					p_149726_1_.spawnEntityInWorld(var9);
					p_149726_1_.notifyBlockChange(p_149726_2_, p_149726_3_, p_149726_4_, getBlockById(0));
					p_149726_1_.notifyBlockChange(p_149726_2_, p_149726_3_ - 1, p_149726_4_, getBlockById(0));
					p_149726_1_.notifyBlockChange(p_149726_2_, p_149726_3_ - 2, p_149726_4_, getBlockById(0));
				}

				for (int var10 = 0; var10 < 120; ++var10)
				{
					p_149726_1_.spawnParticle("snowshovel", (double)p_149726_2_ + p_149726_1_.rand.NextDouble(), (double)(p_149726_3_ - 2) + p_149726_1_.rand.NextDouble() * 2.5D, (double)p_149726_4_ + p_149726_1_.rand.NextDouble(), 0.0D, 0.0D, 0.0D);
				}
			}
			else if (p_149726_1_.getBlock(p_149726_2_, p_149726_3_ - 1, p_149726_4_) == Blocks.iron_block && p_149726_1_.getBlock(p_149726_2_, p_149726_3_ - 2, p_149726_4_) == Blocks.iron_block)
			{
				bool var5 = p_149726_1_.getBlock(p_149726_2_ - 1, p_149726_3_ - 1, p_149726_4_) == Blocks.iron_block && p_149726_1_.getBlock(p_149726_2_ + 1, p_149726_3_ - 1, p_149726_4_) == Blocks.iron_block;
				bool var6 = p_149726_1_.getBlock(p_149726_2_, p_149726_3_ - 1, p_149726_4_ - 1) == Blocks.iron_block && p_149726_1_.getBlock(p_149726_2_, p_149726_3_ - 1, p_149726_4_ + 1) == Blocks.iron_block;

				if (var5 || var6)
				{
					p_149726_1_.setBlock(p_149726_2_, p_149726_3_, p_149726_4_, getBlockById(0), 0, 2);
					p_149726_1_.setBlock(p_149726_2_, p_149726_3_ - 1, p_149726_4_, getBlockById(0), 0, 2);
					p_149726_1_.setBlock(p_149726_2_, p_149726_3_ - 2, p_149726_4_, getBlockById(0), 0, 2);

					if (var5)
					{
						p_149726_1_.setBlock(p_149726_2_ - 1, p_149726_3_ - 1, p_149726_4_, getBlockById(0), 0, 2);
						p_149726_1_.setBlock(p_149726_2_ + 1, p_149726_3_ - 1, p_149726_4_, getBlockById(0), 0, 2);
					}
					else
					{
						p_149726_1_.setBlock(p_149726_2_, p_149726_3_ - 1, p_149726_4_ - 1, getBlockById(0), 0, 2);
						p_149726_1_.setBlock(p_149726_2_, p_149726_3_ - 1, p_149726_4_ + 1, getBlockById(0), 0, 2);
					}

					EntityIronGolem var7 = new EntityIronGolem(p_149726_1_);
					var7.PlayerCreated = true;
					var7.setLocationAndAngles((double)p_149726_2_ + 0.5D, (double)p_149726_3_ - 1.95D, (double)p_149726_4_ + 0.5D, 0.0F, 0.0F);
					p_149726_1_.spawnEntityInWorld(var7);

					for (int var8 = 0; var8 < 120; ++var8)
					{
						p_149726_1_.spawnParticle("snowballpoof", (double)p_149726_2_ + p_149726_1_.rand.NextDouble(), (double)(p_149726_3_ - 2) + p_149726_1_.rand.NextDouble() * 3.9D, (double)p_149726_4_ + p_149726_1_.rand.NextDouble(), 0.0D, 0.0D, 0.0D);
					}

					p_149726_1_.notifyBlockChange(p_149726_2_, p_149726_3_, p_149726_4_, getBlockById(0));
					p_149726_1_.notifyBlockChange(p_149726_2_, p_149726_3_ - 1, p_149726_4_, getBlockById(0));
					p_149726_1_.notifyBlockChange(p_149726_2_, p_149726_3_ - 2, p_149726_4_, getBlockById(0));

					if (var5)
					{
						p_149726_1_.notifyBlockChange(p_149726_2_ - 1, p_149726_3_ - 1, p_149726_4_, getBlockById(0));
						p_149726_1_.notifyBlockChange(p_149726_2_ + 1, p_149726_3_ - 1, p_149726_4_, getBlockById(0));
					}
					else
					{
						p_149726_1_.notifyBlockChange(p_149726_2_, p_149726_3_ - 1, p_149726_4_ - 1, getBlockById(0));
						p_149726_1_.notifyBlockChange(p_149726_2_, p_149726_3_ - 1, p_149726_4_ + 1, getBlockById(0));
					}
				}
			}
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return p_149742_1_.getBlock(p_149742_2_, p_149742_3_, p_149742_4_).blockMaterial.Replaceable && World.doesBlockHaveSolidTopSurface(p_149742_1_, p_149742_2_, p_149742_3_ - 1, p_149742_4_);
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			int var7 = MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 2.5D) & 3;
			p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, var7, 2);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_149986_M = p_149651_1_.registerIcon(this.TextureName + "_face_" + (this.field_149985_a ? "on" : "off"));
			this.field_149984_b = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
		}
	}

}