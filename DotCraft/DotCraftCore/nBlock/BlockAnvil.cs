using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nItem;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System.Collections;

namespace DotCraftCore.nBlock
{
    public class BlockAnvil : BlockFalling
    {
        public static readonly string[] field_149834_a = new string[] { "intact", "slightlyDamaged", "veryDamaged" };
        private static readonly string[] field_149835_N = new string[] { "anvil_top_damaged_0", "anvil_top_damaged_1", "anvil_top_damaged_2" };
        public int field_149833_b;

        protected internal BlockAnvil( )
            : base(Material.anvil)
        {
            this.LightOpacity = 0;
            this.CreativeTab = CreativeTabs.tabDecorations;
        }

        public override bool renderAsNormalBlock( )
        {
            return false;
        }

        public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

        ///    
        ///     <summary> * Called when the block is placed in the world. </summary>
        ///     
        public override void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
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
        public override bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
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
        public override int RenderType
        {
            get
            {
                return 35;
            }
        }

        ///    
        ///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
        ///     
        public override int damageDropped(int p_149692_1_)
        {
            return p_149692_1_ >> 2;
        }

        public override void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
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

        public override void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
        {
            p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
            p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
            p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 2));
        }

        protected internal override void func_149829_a(EntityFallingBlock p_149829_1_)
        {
            p_149829_1_.func_145806_a(true);
        }

        public override void func_149828_a(World p_149828_1_, int p_149828_2_, int p_149828_3_, int p_149828_4_, int p_149828_5_)
        {
            p_149828_1_.playAuxSFX(1022, p_149828_2_, p_149828_3_, p_149828_4_, 0);
        }

        public override bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
        {
            return true;
        }
    }

}