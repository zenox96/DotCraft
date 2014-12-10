namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using ItemStack = DotCraftCore.item.ItemStack;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using TileEntityBeacon = DotCraftCore.tileentity.TileEntityBeacon;
	using World = DotCraftCore.world.World;

	public class BlockBeacon : BlockContainer
	{
		private const string __OBFID = "CL_00000197";

		public BlockBeacon() : base(Material.glass)
		{
			this.Hardness = 3.0F;
			this.CreativeTab = CreativeTabs.tabMisc;
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityBeacon();
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
				TileEntityBeacon var10 = (TileEntityBeacon)p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_);

				if (var10 != null)
				{
					p_149727_5_.func_146104_a(var10);
				}

				return true;
			}
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 34;
			}
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			base.registerBlockIcons(p_149651_1_);
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			base.onBlockPlacedBy(p_149689_1_, p_149689_2_, p_149689_3_, p_149689_4_, p_149689_5_, p_149689_6_);

			if (p_149689_6_.hasDisplayName())
			{
				((TileEntityBeacon)p_149689_1_.getTileEntity(p_149689_2_, p_149689_3_, p_149689_4_)).func_145999_a(p_149689_6_.DisplayName);
			}
		}
	}

}