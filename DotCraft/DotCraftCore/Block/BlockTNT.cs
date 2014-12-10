using System;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Entity = DotCraftCore.entity.Entity;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityTNTPrimed = DotCraftCore.entity.item.EntityTNTPrimed;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityArrow = DotCraftCore.entity.projectile.EntityArrow;
	using Items = DotCraftCore.init.Items;
	using IIcon = DotCraftCore.util.IIcon;
	using Explosion = DotCraftCore.world.Explosion;
	using World = DotCraftCore.world.World;

	public class BlockTNT : Block
	{
		private IIcon field_150116_a;
		private IIcon field_150115_b;
		private const string __OBFID = "CL_00000324";

		public BlockTNT() : base(Material.tnt)
		{
			this.CreativeTab = CreativeTabs.tabRedstone;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 0 ? this.field_150115_b : (p_149691_1_ == 1 ? this.field_150116_a : this.blockIcon);
		}

		public virtual void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);

			if (p_149726_1_.isBlockIndirectlyGettingPowered(p_149726_2_, p_149726_3_, p_149726_4_))
			{
				this.onBlockDestroyedByPlayer(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_, 1);
				p_149726_1_.setBlockToAir(p_149726_2_, p_149726_3_, p_149726_4_);
			}
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_))
			{
				this.onBlockDestroyedByPlayer(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, 1);
				p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
			}
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 1;
		}

///    
///     <summary> * Called upon the block being destroyed by an explosion </summary>
///     
		public virtual void onBlockDestroyedByExplosion(World p_149723_1_, int p_149723_2_, int p_149723_3_, int p_149723_4_, Explosion p_149723_5_)
		{
			if (!p_149723_1_.isClient)
			{
				EntityTNTPrimed var6 = new EntityTNTPrimed(p_149723_1_, (double)((float)p_149723_2_ + 0.5F), (double)((float)p_149723_3_ + 0.5F), (double)((float)p_149723_4_ + 0.5F), p_149723_5_.ExplosivePlacedBy);
				var6.fuse = p_149723_1_.rand.Next(var6.fuse / 4) + var6.fuse / 8;
				p_149723_1_.spawnEntityInWorld(var6);
			}
		}

		public virtual void onBlockDestroyedByPlayer(World p_149664_1_, int p_149664_2_, int p_149664_3_, int p_149664_4_, int p_149664_5_)
		{
			this.func_150114_a(p_149664_1_, p_149664_2_, p_149664_3_, p_149664_4_, p_149664_5_, (EntityLivingBase)null);
		}

		public virtual void func_150114_a(World p_150114_1_, int p_150114_2_, int p_150114_3_, int p_150114_4_, int p_150114_5_, EntityLivingBase p_150114_6_)
		{
			if (!p_150114_1_.isClient)
			{
				if ((p_150114_5_ & 1) == 1)
				{
					EntityTNTPrimed var7 = new EntityTNTPrimed(p_150114_1_, (double)((float)p_150114_2_ + 0.5F), (double)((float)p_150114_3_ + 0.5F), (double)((float)p_150114_4_ + 0.5F), p_150114_6_);
					p_150114_1_.spawnEntityInWorld(var7);
					p_150114_1_.playSoundAtEntity(var7, "game.tnt.primed", 1.0F, 1.0F);
				}
			}
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (p_149727_5_.CurrentEquippedItem != null && p_149727_5_.CurrentEquippedItem.Item == Items.flint_and_steel)
			{
				this.func_150114_a(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, 1, p_149727_5_);
				p_149727_1_.setBlockToAir(p_149727_2_, p_149727_3_, p_149727_4_);
				p_149727_5_.CurrentEquippedItem.damageItem(1, p_149727_5_);
				return true;
			}
			else
			{
				return base.onBlockActivated(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, p_149727_5_, p_149727_6_, p_149727_7_, p_149727_8_, p_149727_9_);
			}
		}

		public virtual void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
		{
			if (p_149670_5_ is EntityArrow && !p_149670_1_.isClient)
			{
				EntityArrow var6 = (EntityArrow)p_149670_5_;

				if (var6.Burning)
				{
					this.func_150114_a(p_149670_1_, p_149670_2_, p_149670_3_, p_149670_4_, 1, var6.shootingEntity is EntityLivingBase ? (EntityLivingBase)var6.shootingEntity : null);
					p_149670_1_.setBlockToAir(p_149670_2_, p_149670_3_, p_149670_4_);
				}
			}
		}

///    
///     <summary> * Return whether this block can drop from an explosion. </summary>
///     
		public virtual bool canDropFromExplosion(Explosion p_149659_1_)
		{
			return false;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
			this.field_150116_a = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.field_150115_b = p_149651_1_.registerIcon(this.TextureName + "_bottom");
		}
	}

}