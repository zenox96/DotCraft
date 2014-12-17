namespace DotCraftCore.nBlock.nMaterial
{
	public class Material
	{
		public static readonly Material air = new MaterialTransparent(MapColor.field_151660_b);
		public static readonly Material grass = new Material(MapColor.field_151661_c);
		public static readonly Material ground = new Material(MapColor.field_151664_l);
		public static readonly Material wood = (new Material(MapColor.field_151663_o)).setBurning();
		public static readonly Material rock = (new Material(MapColor.field_151665_m)).setRequiresTool();
		public static readonly Material iron = (new Material(MapColor.field_151668_h)).setRequiresTool();
		public static readonly Material anvil = (new Material(MapColor.field_151668_h)).setRequiresTool().setImmovableMobility();
		public static readonly Material water = (new MaterialLiquid(MapColor.field_151662_n)).setNoPushMobility();
		public static readonly Material lava = (new MaterialLiquid(MapColor.field_151656_f)).setNoPushMobility();
		public static readonly Material leaves = (new Material(MapColor.field_151669_i)).setBurning().setTranslucent().setNoPushMobility();
		public static readonly Material plants = (new MaterialLogic(MapColor.field_151669_i)).setNoPushMobility();
		public static readonly Material vine = (new MaterialLogic(MapColor.field_151669_i)).setBurning().setNoPushMobility().setReplaceable();
		public static readonly Material sponge = new Material(MapColor.field_151659_e);
		public static readonly Material cloth = (new Material(MapColor.field_151659_e)).setBurning();
		public static readonly Material fire = (new MaterialTransparent(MapColor.field_151660_b)).setNoPushMobility();
		public static readonly Material sand = new Material(MapColor.field_151658_d);
		public static readonly Material circuits = (new MaterialLogic(MapColor.field_151660_b)).setNoPushMobility();
		public static readonly Material carpet = (new MaterialLogic(MapColor.field_151659_e)).setBurning();
		public static readonly Material glass = (new Material(MapColor.field_151660_b)).setTranslucent().setAdventureModeExempt();
		public static readonly Material redstoneLight = (new Material(MapColor.field_151660_b)).setAdventureModeExempt();
		public static readonly Material tnt = (new Material(MapColor.field_151656_f)).setBurning().setTranslucent();
		public static readonly Material coral = (new Material(MapColor.field_151669_i)).setNoPushMobility();
		public static readonly Material ice = (new Material(MapColor.field_151657_g)).setTranslucent().setAdventureModeExempt();
		public static readonly Material field_151598_x = (new Material(MapColor.field_151657_g)).setAdventureModeExempt();
		public static readonly Material field_151597_y = (new MaterialLogic(MapColor.field_151666_j)).setReplaceable().setTranslucent().setRequiresTool().setNoPushMobility();

	/// <summary> The material for crafted snow.  </summary>
		public static readonly Material craftedSnow = (new Material(MapColor.field_151666_j)).setRequiresTool();
		public static readonly Material field_151570_A = (new Material(MapColor.field_151669_i)).setTranslucent().setNoPushMobility();
		public static readonly Material field_151571_B = new Material(MapColor.field_151667_k);
		public static readonly Material field_151572_C = (new Material(MapColor.field_151669_i)).setNoPushMobility();
		public static readonly Material dragonEgg = (new Material(MapColor.field_151669_i)).setNoPushMobility();
		public static readonly Material Portal = (new MaterialPortal(MapColor.field_151660_b)).setImmovableMobility();
		public static readonly Material field_151568_F = (new Material(MapColor.field_151660_b)).setNoPushMobility();
		
        /* TODO annonymous inner class
         * public static readonly Material field_151569_G = (new Material(MapColor.field_151659_e) {  public bool blocksMovement() { return false; } }).setRequiresTool().setNoPushMobility();
         */

	/// <summary> Pistons' material.  </summary>
		public static readonly Material piston = (new Material(MapColor.field_151665_m)).setImmovableMobility();

	/// <summary> Bool defining if the block can burn or not.  </summary>
		private bool canBurn;

///    
///     <summary> * Determines whether blocks with this material can be "overwritten" by other blocks when placed - eg snow, vines
///     * and tall grass. </summary>
///     
		private bool replaceable;

	/// <summary> Indicates if the material is translucent  </summary>
		private bool isTranslucent;

	/// <summary> The color index used to draw the blocks of this material on maps.  </summary>
		private readonly MapColor materialMapColor;

///    
///     <summary> * Determines if the material can be harvested without a tool (or with the wrong tool) </summary>
///     
		private bool requiresNoTool = true;

///    
///     <summary> * Mobility information flag. 0 indicates that this block is normal, 1 indicates that it can't push other blocks, 2
///     * indicates that it can't be pushed. </summary>
///     
		private int mobilityFlag;
		private bool isAdventureModeExempt;
		

		public Material(MapColor p_i2116_1_)
		{
			this.materialMapColor = p_i2116_1_;
		}

///    
///     <summary> * Returns if blocks of these materials are liquids. </summary>
///     
		public virtual bool Liquid
		{
			get
			{
				return false;
			}
		}

		public virtual bool Solid
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Will prevent grass from growing on dirt underneath and kill any grass below it if it returns true </summary>
///     
		public virtual bool CanBlockGrass
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Returns if this material is considered solid or not </summary>
///     
		public virtual bool blocksMovement()
		{
			return true;
		}

///    
///     <summary> * Marks the material as translucent </summary>
///     
		private Material setTranslucent()
		{
			this.isTranslucent = true;
			return this;
		}

///    
///     <summary> * Makes blocks with this material require the correct tool to be harvested. </summary>
///     
		protected internal virtual Material setRequiresTool()
		{
			this.requiresNoTool = false;
			return this;
		}

///    
///     <summary> * Set the canBurn bool to True and return the current object. </summary>
///     
		protected internal virtual Material setBurning()
		{
			this.canBurn = true;
			return this;
		}

///    
///     <summary> * Returns if the block can burn or not. </summary>
///     
		public virtual bool CanBurn
		{
			get
			{
				return this.canBurn;
			}
		}

///    
///     <summary> * Sets <seealso cref="#replaceable"/> to true. </summary>
///     
		public virtual Material setReplaceable()
		{
			this.replaceable = true;
			return this;
		}

///    
///     <summary> * Returns whether the material can be replaced by other blocks when placed - eg snow, vines and tall grass. </summary>
///     
		public virtual bool Replaceable
		{
			get
			{
				return this.replaceable;
			}
		}

///    
///     <summary> * Indicate if the material is opaque </summary>
///     
		public virtual bool Opaque
		{
			get
			{
				return this.isTranslucent ? false : this.blocksMovement();
			}
		}

///    
///     <summary> * Returns true if the material can be harvested without a tool (or with the wrong tool) </summary>
///     
		public virtual bool ToolNotRequired
		{
			get
			{
				return this.requiresNoTool;
			}
		}

///    
///     <summary> * Returns the mobility information of the material, 0 = free, 1 = can't push but can move over, 2 = total
///     * immobility and stop pistons. </summary>
///     
		public virtual int MaterialMobility
		{
			get
			{
				return this.mobilityFlag;
			}
		}

///    
///     <summary> * This type of material can't be pushed, but pistons can move over it. </summary>
///     
		protected internal virtual Material setNoPushMobility()
		{
			this.mobilityFlag = 1;
			return this;
		}

///    
///     <summary> * This type of material can't be pushed, and pistons are blocked to move. </summary>
///     
		protected internal virtual Material setImmovableMobility()
		{
			this.mobilityFlag = 2;
			return this;
		}

///    
///     * <seealso cref= #isAdventureModeExempt() </seealso>
///     
		protected internal virtual Material setAdventureModeExempt()
		{
			this.isAdventureModeExempt = true;
			return this;
		}

///    
///     <summary> * Returns true if blocks with this material can always be mined in adventure mode. </summary>
///     
		public virtual bool AdventureModeExempt
		{
			get
			{
				return this.isAdventureModeExempt;
			}
		}

		public virtual MapColor MaterialMapColor
		{
			get
			{
				return this.materialMapColor;
			}
		}
	}

}