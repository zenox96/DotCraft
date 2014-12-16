﻿using System.Collections;

namespace DotCraftCore.Block
{
    using IIconRegister = DotCraftCore.Client.renderer.texture.IIconRegister;
    using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
    using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
    using EntityFallingBlock = DotCraftCore.Entity.Item.EntityFallingBlock;
    using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
    using Item = DotCraftCore.Item.Item;
    using ItemStack = DotCraftCore.Item.ItemStack;
    using IIcon = DotCraftCore.Util.IIcon;
    using MathHelper = DotCraftCore.Util.MathHelper;
    using IBlockAccess = DotCraftCore.World.IBlockAccess;
    using World = DotCraftCore.World.World;

    public class BlockAnvil : BlockFalling
    {
        public static readonly string[] field_149834_a = new string[] { "intact", "slightlyDamaged", "veryDamaged" };
        private static readonly string[] field_149835_N = new string[] { "anvil_top_damaged_0", "anvil_top_damaged_1", "anvil_top_damaged_2" };
        public int field_149833_b;
        private IIcon[] field_149836_O;
        

        protected internal BlockAnvil( )
            : base(Material.anvil)
        {
            this.LightOpacity = 0;
            this.CreativeTab = CreativeTabs.tabDecorations;
        }

        public virtual bool renderAsNormalBlock( )
        {
            return false;
        }

        public virtual bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

        ///    
        ///     <summary> * Gets the block's texture. Args: side, meta </summary>
        ///     
        public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
        {
            if (this.field_149833_b == 3 && p_149691_1_ == 1)
            {
                int var3 = (p_149691_2_ >> 2) % this.field_149836_O.Length;
                return this.field_149836_O[var3];
            }
            else
            {
                return this.blockIcon;
            }
        }

        public virtual void registerBlockIcons(IIconRegister p_149651_1_)
        {
            this.blockIcon = p_149651_1_.registerIcon("anvil_base");
            this.field_149836_O = new IIcon[field_149835_N.Length];

            for (int var2 = 0; var2 < this.field_149836_O.Length; ++var2)
            {
                this.field_149836_O[var2] = p_149651_1_.registerIcon(field_149835_N[var2]);
            }
        }

        ///    
        ///     <summary> * Called when the block is placed in the world. </summary>
        ///     
        public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
        {
            int var7 = MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3;
            int var8 = p_149689_1_.getBlockMetadata(p_149689_2_, p_149689_3_, p_149689_4_) >> 2;
            ++var7;
            var7 %= 4;

            if (var7 == 0)
            {
                p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 2 | var8 << 2, 2);
            }

            if (var7 == 1)
            {
                p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 3 | var8 << 2, 2);
            }

            if (var7 == 2)
            {
                p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 0 | var8 << 2, 2);
            }

            if (var7 == 3)
            {
                p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 1 | var8 << 2, 2);
            }
        }

        ///    
        ///     <summary> * Called upon block activation (right click on the block.) </summary>
        ///     
        public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
        {
            if (p_149727_1_.isClient)
            {
                return true;
            }
            else
            {
                p_149727_5_.displayGUIAnvil(p_149727_2_, p_149727_3_, p_149727_4_);
                return true;
            }
        }

        ///    
        ///     <summary> * The type of render function that is called for this block </summary>
        ///     
        public virtual int RenderType
        {
            get
            {
                return 35;
            }
        }

        ///    
        ///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
        ///     
        public virtual int damageDropped(int p_149692_1_)
        {
            return p_149692_1_ >> 2;
        }

        public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
        {
            int var5 = p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_) & 3;

            if (var5 != 3 && var5 != 1)
            {
                this.setBlockBounds(0.125F, 0.0F, 0.0F, 0.875F, 1.0F, 1.0F);
            }
            else
            {
                this.setBlockBounds(0.0F, 0.0F, 0.125F, 1.0F, 1.0F, 0.875F);
            }
        }

        public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
        {
            p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
            p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
            p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 2));
        }

        protected internal virtual void func_149829_a(EntityFallingBlock p_149829_1_)
        {
            p_149829_1_.func_145806_a(true);
        }

        public virtual void func_149828_a(World p_149828_1_, int p_149828_2_, int p_149828_3_, int p_149828_4_, int p_149828_5_)
        {
            p_149828_1_.playAuxSFX(1022, p_149828_2_, p_149828_3_, p_149828_4_, 0);
        }

        public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
        {
            return true;
        }
    }

}