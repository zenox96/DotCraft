namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Blocks = DotCraftCore.nInit.Blocks;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using World = DotCraftCore.nWorld.World;

	public class BlockWorkbench : Block
	{
		private IIcon field_150035_a;
		private IIcon field_150034_b;
		

		protected internal BlockWorkbench() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ == 1 ? this.field_150035_a : (p_149691_1_ == 0 ? Blocks.planks.getBlockTextureFromSide(p_149691_1_) : (p_149691_1_ != 2 && p_149691_1_ != 4 ? this.blockIcon : this.field_150034_b));
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
			this.field_150035_a = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.field_150034_b = p_149651_1_.registerIcon(this.TextureName + "_front");
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
				p_149727_5_.displayGUIWorkbench(p_149727_2_, p_149727_3_, p_149727_4_);
				return true;
			}
		}
	}

}