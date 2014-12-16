using System;
using System.Collections;

namespace DotCraftCore.TileEntity
{

	using Block = DotCraftCore.block.Block;
	using BlockJukebox = DotCraftCore.block.BlockJukebox;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using Blocks = DotCraftCore.init.Blocks;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using Packet = DotCraftCore.network.Packet;
	using World = DotCraftCore.World.World;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class TileEntity
	{
		private static readonly Logger logger = LogManager.Logger;

///    
///     <summary> * A HashMap storing string names of classes mapping to the actual java.lang.Class type. </summary>
///     
		private static IDictionary nameToClassMap = new Hashtable();

///    
///     <summary> * A HashMap storing the classes and mapping to the string names (reverse of nameToClassMap). </summary>
///     
		private static IDictionary classToNameMap = new Hashtable();

	/// <summary> the instance of the world the tile entity is in.  </summary>
		protected internal World worldObj;
		public int field_145851_c;
		public int field_145848_d;
		public int field_145849_e;
		protected internal bool tileEntityInvalid;
		public int blockMetadata = -1;

	/// <summary> the Block type that this TileEntity is contained within  </summary>
		public Block blockType;
		

		private static void func_145826_a(Type p_145826_0_, string p_145826_1_)
		{
			if(nameToClassMap.ContainsKey(p_145826_1_))
			{
				throw new System.ArgumentException("Duplicate id: " + p_145826_1_);
			}
			else
			{
				nameToClassMap.Add(p_145826_1_, p_145826_0_);
				classToNameMap.Add(p_145826_0_, p_145826_1_);
			}
		}

///    
///     <summary> * Returns the worldObj for this tileEntity. </summary>
///     
		public virtual World WorldObj
		{
			get
			{
				return this.worldObj;
			}
			set
			{
				this.worldObj = value;
			}
		}

///    
///     <summary> * Sets the worldObj for this tileEntity. </summary>
///     

///    
///     <summary> * Returns true if the worldObj isn't null. </summary>
///     
		public virtual bool hasWorldObj()
		{
			return this.worldObj != null;
		}

		public virtual void readFromNBT(NBTTagCompound p_145839_1_)
		{
			this.field_145851_c = p_145839_1_.getInteger("x");
			this.field_145848_d = p_145839_1_.getInteger("y");
			this.field_145849_e = p_145839_1_.getInteger("z");
		}

		public virtual void writeToNBT(NBTTagCompound p_145841_1_)
		{
			string var2 = (string)classToNameMap[this.GetType()];

			if(var2 == null)
			{
				throw new Exception(this.GetType() + " is missing a mapping! This is a bug!");
			}
			else
			{
				p_145841_1_.setString("id", var2);
				p_145841_1_.setInteger("x", this.field_145851_c);
				p_145841_1_.setInteger("y", this.field_145848_d);
				p_145841_1_.setInteger("z", this.field_145849_e);
			}
		}

		public virtual void updateEntity()
		{
		}

///    
///     <summary> * Creates a new entity and loads its data from the specified NBT. </summary>
///     
		public static TileEntity createAndLoadEntity(NBTTagCompound p_145827_0_)
		{
			TileEntity var1 = null;

			try
			{
				Type var2 = (Class)nameToClassMap[p_145827_0_.getString("id")];

				if(var2 != null)
				{
					var1 = (TileEntity)var2.newInstance();
				}
			}
			catch (Exception var3)
			{
				var3.printStackTrace();
			}

			if(var1 != null)
			{
				var1.readFromNBT(p_145827_0_);
			}
			else
			{
				logger.warn("Skipping BlockEntity with id " + p_145827_0_.getString("id"));
			}

			return var1;
		}

		public virtual int BlockMetadata
		{
			get
			{
				if(this.blockMetadata == -1)
				{
					this.blockMetadata = this.worldObj.getBlockMetadata(this.field_145851_c, this.field_145848_d, this.field_145849_e);
				}
	
				return this.blockMetadata;
			}
		}

///    
///     <summary> * Called when an the contents of an Inventory change, usually </summary>
///     
		public virtual void onInventoryChanged()
		{
			if(this.worldObj != null)
			{
				this.blockMetadata = this.worldObj.getBlockMetadata(this.field_145851_c, this.field_145848_d, this.field_145849_e);
				this.worldObj.func_147476_b(this.field_145851_c, this.field_145848_d, this.field_145849_e, this);

				if(this.BlockType != Blocks.air)
				{
					this.worldObj.func_147453_f(this.field_145851_c, this.field_145848_d, this.field_145849_e, this.BlockType);
				}
			}
		}

///    
///     <summary> * Returns the square of the distance between this entity and the passed in coordinates. </summary>
///     
		public virtual double getDistanceFrom(double p_145835_1_, double p_145835_3_, double p_145835_5_)
		{
			double var7 = (double)this.field_145851_c + 0.5D - p_145835_1_;
			double var9 = (double)this.field_145848_d + 0.5D - p_145835_3_;
			double var11 = (double)this.field_145849_e + 0.5D - p_145835_5_;
			return var7 * var7 + var9 * var9 + var11 * var11;
		}

		public virtual double MaxRenderDistanceSquared
		{
			get
			{
				return 4096.0D;
			}
		}

///    
///     <summary> * Gets the block type at the location of this entity (client-only). </summary>
///     
		public virtual Block BlockType
		{
			get
			{
				if(this.blockType == null)
				{
					this.blockType = this.worldObj.getBlock(this.field_145851_c, this.field_145848_d, this.field_145849_e);
				}
	
				return this.blockType;
			}
		}

///    
///     <summary> * Overriden in a sign to provide the text. </summary>
///     
		public virtual Packet DescriptionPacket
		{
			get
			{
				return null;
			}
		}

		public virtual bool isInvalid()
		{
			get
			{
				return this.tileEntityInvalid;
			}
		}

///    
///     <summary> * invalidates a tile entity </summary>
///     
		public virtual void invalidate()
		{
			this.tileEntityInvalid = true;
		}

///    
///     <summary> * validates a tile entity </summary>
///     
		public virtual void validate()
		{
			this.tileEntityInvalid = false;
		}

		public virtual bool receiveClientEvent(int p_145842_1_, int p_145842_2_)
		{
			return false;
		}

		public virtual void updateContainingBlockInfo()
		{
			this.blockType = null;
			this.blockMetadata = -1;
		}

		public virtual void func_145828_a(CrashReportCategory p_145828_1_)
		{
			p_145828_1_.addCrashSectionCallable("Name", new Callable() {  public string call() { return(string)TileEntity.classToNameMap.get(TileEntity.GetType()) + " // " + TileEntity.GetType().CanonicalName; } });
			CrashReportCategory.func_147153_a(p_145828_1_, this.field_145851_c, this.field_145848_d, this.field_145849_e, this.BlockType, this.BlockMetadata);
			p_145828_1_.addCrashSectionCallable("Actual block type", new Callable() {  public string call() { int var1 = Block.getIdFromBlock(TileEntity.worldObj.getBlock(TileEntity.field_145851_c, TileEntity.field_145848_d, TileEntity.field_145849_e)); try { return string.Format("ID #{0:D} ({1} // {2})", new object[] {Convert.ToInt32(var1), Block.getBlockById(var1).UnlocalizedName, Block.getBlockById(var1).GetType().CanonicalName}); } catch (Exception var3) { return "ID #" + var1; } } });
			p_145828_1_.addCrashSectionCallable("Actual block data value", new Callable() {  public string call() { int var1 = TileEntity.worldObj.getBlockMetadata(TileEntity.field_145851_c, TileEntity.field_145848_d, TileEntity.field_145849_e); if(var1 < 0) { return "Unknown? (Got " + var1 + ")"; } else { string var2 = string.Format("{0,4}", new object[] {int.toBinaryString(var1)}).replace(" ", "0"); return string.Format("{0:D} / 0x{0:X} / 0b{1}", new object[] {Convert.ToInt32(var1), var2}); } } });
		}

		static TileEntity()
		{
			func_145826_a(typeof(TileEntityFurnace), "Furnace");
			func_145826_a(typeof(TileEntityChest), "Chest");
			func_145826_a(typeof(TileEntityEnderChest), "EnderChest");
			func_145826_a(typeof(BlockJukebox.TileEntityJukebox), "RecordPlayer");
			func_145826_a(typeof(TileEntityDispenser), "Trap");
			func_145826_a(typeof(TileEntityDropper), "Dropper");
			func_145826_a(typeof(TileEntitySign), "Sign");
			func_145826_a(typeof(TileEntityMobSpawner), "MobSpawner");
			func_145826_a(typeof(TileEntityNote), "Music");
			func_145826_a(typeof(TileEntityPiston), "Piston");
			func_145826_a(typeof(TileEntityBrewingStand), "Cauldron");
			func_145826_a(typeof(TileEntityEnchantmentTable), "EnchantTable");
			func_145826_a(typeof(TileEntityEndPortal), "Airportal");
			func_145826_a(typeof(TileEntityCommandBlock), "Control");
			func_145826_a(typeof(TileEntityBeacon), "Beacon");
			func_145826_a(typeof(TileEntitySkull), "Skull");
			func_145826_a(typeof(TileEntityDaylightDetector), "DLDetector");
			func_145826_a(typeof(TileEntityHopper), "Hopper");
			func_145826_a(typeof(TileEntityComparator), "Comparator");
			func_145826_a(typeof(TileEntityFlowerPot), "FlowerPot");
		}
	}

}