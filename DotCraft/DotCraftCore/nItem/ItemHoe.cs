namespace DotCraftCore.nItem
{

	using Block = DotCraftCore.nBlock.Block;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.nWorld.World;

	public class ItemHoe : Item
	{
		protected internal Item.ToolMaterial theToolMaterial;
		

		public ItemHoe(Item.ToolMaterial p_i45343_1_)
		{
			this.theToolMaterial = p_i45343_1_;
			this.maxStackSize = 1;
			this.MaxDamage = p_i45343_1_.MaxUses;
			this.CreativeTab = CreativeTabs.tabTools;
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			if (!p_77648_2_.canPlayerEdit(p_77648_4_, p_77648_5_, p_77648_6_, p_77648_7_, p_77648_1_))
			{
				return false;
			}
			else
			{
				Block var11 = p_77648_3_.getBlock(p_77648_4_, p_77648_5_, p_77648_6_);

				if (p_77648_7_ != 0 && p_77648_3_.getBlock(p_77648_4_, p_77648_5_ + 1, p_77648_6_).Material == Material.air && (var11 == Blocks.grass || var11 == Blocks.dirt))
				{
					Block var12 = Blocks.farmland;
					p_77648_3_.playSoundEffect((double)((float)p_77648_4_ + 0.5F), (double)((float)p_77648_5_ + 0.5F), (double)((float)p_77648_6_ + 0.5F), var12.stepSound.func_150498_e(), (var12.stepSound.GetVolume() + 1.0F) / 2.0F, var12.stepSound.GetPitch() * 0.8F);

					if (p_77648_3_.isClient)
					{
						return true;
					}
					else
					{
						p_77648_3_.setBlock(p_77648_4_, p_77648_5_, p_77648_6_, var12);
						p_77648_1_.damageItem(1, p_77648_2_);
						return true;
					}
				}
				else
				{
					return false;
				}
			}
		}

///    
///     <summary> * Returns True is the item is renderer in full 3D when hold. </summary>
///     
		public virtual bool isFull3D()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Returns the name of the material this tool is made from as it is declared in EnumToolMaterial (meaning diamond
///     * would return "EMERALD") </summary>
///     
		public virtual string MaterialName
		{
			get
			{
				return this.theToolMaterial.ToString();
			}
		}
	}

}