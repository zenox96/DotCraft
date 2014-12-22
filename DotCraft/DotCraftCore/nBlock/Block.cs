using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nInventory;
using DotCraftCore.nTileEntity;
using DotCraftCore.nWorld;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nEntity.nItem;
using DotCraftCore.nStats;
using DotCraftCore.nEnchantment;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
    public class Block
    {
        public static readonly RegistryNamespacedDefaultedByKey blockRegistry = new RegistryNamespacedDefaultedByKey("air");
        private CreativeTabs displayOnCreativeTab;
        protected internal string textureName;

        public static readonly SoundType soundTypeStone = new SoundType("stone", 1.0F, 1.0F);
        public static readonly SoundType soundTypeWood = new SoundType("wood", 1.0F, 1.0F);
        public static readonly SoundType soundTypeGravel = new SoundType("gravel", 1.0F, 1.0F);
        public static readonly SoundType soundTypeGrass = new SoundType("grass", 1.0F, 1.0F);
        public static readonly SoundType soundTypePiston = new SoundType("stone", 1.0F, 1.0F);
        public static readonly SoundType soundTypeMetal = new SoundType("stone", 1.0F, 1.5F);
        public static readonly SoundType soundTypeGlass = new SoundType("stone", "glass", "stone", 1.0f, 1.0f);
        public static readonly SoundType soundTypeCloth = new SoundType("cloth", 1.0F, 1.0F);
        public static readonly SoundType soundTypeSand = new SoundType("sand", 1.0F, 1.0F);
        public static readonly SoundType soundTypeSnow = new SoundType("snow", 1.0F, 1.0F);
        public static readonly SoundType soundTypeLadder = new SoundType("ladder", "wood", "ladder", 1.0F, 1.0F);
        public static readonly SoundType soundTypeAnvil = new SoundType("anvil", "stone", "random.anvil_land", 0.3F, 1.0F);

        protected internal bool canBlockGrass;

        protected internal bool field_149783_u;

        /// <summary> Indicates how many hits it takes to break a block.  </summary>
        protected internal float blockHardness;
        protected internal float blockResistance;
        protected internal bool field_149791_x = true;
        protected internal bool enableStats = true;

        ///    
        ///     <summary> * Flags whether or not this block is of a type that needs random ticking. Ref-counted by ExtendedBlockStorage in
        ///     * order to broadly cull a chunk from the random chunk update list for efficiency's sake. </summary>
        ///     
        protected internal bool needsRandomTick;

        /// <summary> true if the Block contains a Tile Entity  </summary>
        protected internal bool isBlockContainer;
        protected internal double field_149759_B;
        protected internal double field_149760_C;
        protected internal double field_149754_D;
        protected internal double field_149755_E;
        protected internal double field_149756_F;
        protected internal double field_149757_G;

        /// <summary> Sound of stepping on the block  </summary>
        public SoundType stepSound;
        public float blockParticleGravity;

        ///    
        ///     <summary> * Determines how much velocity is maintained while moving on top of this block </summary>
        ///     
        public float slipperiness;

        public static int getIdFromBlock(Block p_149682_0_)
        {
            return blockRegistry.getIDForObject(p_149682_0_);
        }

        public static Block getBlockById(int p_149729_0_)
        {
            return (Block)blockRegistry.getObjectForID(p_149729_0_);
        }

        public static Block getBlockFromItem(Item p_149634_0_)
        {
            return getBlockById(Item.getIdFromItem(p_149634_0_));
        }

        public static Block getBlockFromName(string p_149684_0_)
        {
            if (blockRegistry.containsKey(p_149684_0_))
            {
                return (Block)blockRegistry.getObject(p_149684_0_);
            }
            else
            {
                return (Block)blockRegistry.getObjectForID(Convert.ToInt32(p_149684_0_));
            }
        }

        public virtual bool CanBlockGrass
        {
            get
            {
                return this.canBlockGrass;
            }
        }

        public virtual bool func_149710_n( )
        {
            return this.field_149783_u;
        }

        public virtual Material BlockMaterial
        {
            get;
            protected set;
        }

        public virtual MapColor getMapColor(int p_149728_1_)
        {
            return this.BlockMaterial.MaterialMapColor;
        }

        public static void registerBlocks( )
        {
            blockRegistry.addObject(0, "air", (new BlockAir( )).setBlockName("air"));
            blockRegistry.addObject(1, "stone", (new BlockStone( )).setHardness(1.5F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("stone").setBlockTextureName("stone"));
            blockRegistry.addObject(2, "grass", (new BlockGrass( )).setHardness(0.6F).setStepSound(soundTypeGrass).setBlockName("grass").setBlockTextureName("grass"));
            blockRegistry.addObject(3, "dirt", (new BlockDirt( )).setHardness(0.5F).setStepSound(soundTypeGravel).setBlockName("dirt").setBlockTextureName("dirt"));
            Block var0 = (new Block(Material.rock)).setHardness(2.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("stonebrick").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("cobblestone");
            blockRegistry.addObject(4, "cobblestone", var0);
            Block var1 = (new BlockWood( )).setHardness(2.0F).setResistance(5.0F).setStepSound(soundTypeWood).setBlockName("wood").setBlockTextureName("planks");
            blockRegistry.addObject(5, "planks", var1);
            blockRegistry.addObject(6, "sapling", (new BlockSapling( )).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("sapling").setBlockTextureName("sapling"));
            blockRegistry.addObject(7, "bedrock", (new Block(Material.rock)).setBlockUnbreakable( ).setResistance(6000000.0F).setStepSound(soundTypePiston).setBlockName("bedrock").disableStats( ).setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("bedrock"));
            blockRegistry.addObject(8, "flowing_water", (new BlockDynamicLiquid(Material.water)).setHardness(100.0F).setLightOpacity(3).setBlockName("water").disableStats( ).setBlockTextureName("water_flow"));
            blockRegistry.addObject(9, "water", (new BlockStaticLiquid(Material.water)).setHardness(100.0F).setLightOpacity(3).setBlockName("water").disableStats( ).setBlockTextureName("water_still"));
            blockRegistry.addObject(10, "flowing_lava", (new BlockDynamicLiquid(Material.lava)).setHardness(100.0F).setLightValue(1.0F).setBlockName("lava").disableStats( ).setBlockTextureName("lava_flow"));
            blockRegistry.addObject(11, "lava", (new BlockStaticLiquid(Material.lava)).setHardness(100.0F).setLightValue(1.0F).setBlockName("lava").disableStats( ).setBlockTextureName("lava_still"));
            blockRegistry.addObject(12, "sand", (new BlockSand( )).setHardness(0.5F).setStepSound(soundTypeSand).setBlockName("sand").setBlockTextureName("sand"));
            blockRegistry.addObject(13, "gravel", (new BlockGravel( )).setHardness(0.6F).setStepSound(soundTypeGravel).setBlockName("gravel").setBlockTextureName("gravel"));
            blockRegistry.addObject(14, "gold_ore", (new BlockOre( )).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("oreGold").setBlockTextureName("gold_ore"));
            blockRegistry.addObject(15, "iron_ore", (new BlockOre( )).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("oreIron").setBlockTextureName("iron_ore"));
            blockRegistry.addObject(16, "coal_ore", (new BlockOre( )).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("oreCoal").setBlockTextureName("coal_ore"));
            blockRegistry.addObject(17, "log", (new BlockOldLog( )).setBlockName("log").setBlockTextureName("log"));
            blockRegistry.addObject(18, "leaves", (new BlockOldLeaf( )).setBlockName("leaves").setBlockTextureName("leaves"));
            blockRegistry.addObject(19, "sponge", (new BlockSponge( )).setHardness(0.6F).setStepSound(soundTypeGrass).setBlockName("sponge").setBlockTextureName("sponge"));
            blockRegistry.addObject(20, "glass", (new BlockGlass(Material.glass, false)).setHardness(0.3F).setStepSound(soundTypeGlass).setBlockName("glass").setBlockTextureName("glass"));
            blockRegistry.addObject(21, "lapis_ore", (new BlockOre( )).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("oreLapis").setBlockTextureName("lapis_ore"));
            blockRegistry.addObject(22, "lapis_block", (new BlockCompressed(MapColor.field_151652_H)).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("blockLapis").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("lapis_block"));
            blockRegistry.addObject(23, "dispenser", (new BlockDispenser( )).setHardness(3.5F).setStepSound(soundTypePiston).setBlockName("dispenser").setBlockTextureName("dispenser"));
            Block var2 = (new BlockSandStone( )).setStepSound(soundTypePiston).setHardness(0.8F).setBlockName("sandStone").setBlockTextureName("sandstone");
            blockRegistry.addObject(24, "sandstone", var2);
            blockRegistry.addObject(25, "noteblock", (new BlockNote( )).setHardness(0.8F).setBlockName("musicBlock").setBlockTextureName("noteblock"));
            blockRegistry.addObject(26, "bed", (new BlockBed( )).setHardness(0.2F).setBlockName("bed").disableStats( ).setBlockTextureName("bed"));
            blockRegistry.addObject(27, "golden_rail", (new BlockRailPowered( )).setHardness(0.7F).setStepSound(soundTypeMetal).setBlockName("goldenRail").setBlockTextureName("rail_golden"));
            blockRegistry.addObject(28, "detector_rail", (new BlockRailDetector( )).setHardness(0.7F).setStepSound(soundTypeMetal).setBlockName("detectorRail").setBlockTextureName("rail_detector"));
            blockRegistry.addObject(29, "sticky_piston", (new BlockPistonBase(true)).setBlockName("pistonStickyBase"));
            blockRegistry.addObject(30, "web", (new BlockWeb( )).setLightOpacity(1).setHardness(4.0F).setBlockName("web").setBlockTextureName("web"));
            blockRegistry.addObject(31, "tallgrass", (new BlockTallGrass( )).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("tallgrass"));
            blockRegistry.addObject(32, "deadbush", (new BlockDeadBush( )).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("deadbush").setBlockTextureName("deadbush"));
            blockRegistry.addObject(33, "piston", (new BlockPistonBase(false)).setBlockName("pistonBase"));
            blockRegistry.addObject(34, "piston_head", new BlockPistonExtension( ));
            blockRegistry.addObject(35, "wool", (new BlockColored(Material.cloth)).setHardness(0.8F).setStepSound(soundTypeCloth).setBlockName("cloth").setBlockTextureName("wool_colored"));
            blockRegistry.addObject(36, "piston_extension", new BlockPistonMoving( ));
            blockRegistry.addObject(37, "yellow_flower", (new BlockFlower(0)).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("flower1").setBlockTextureName("flower_dandelion"));
            blockRegistry.addObject(38, "red_flower", (new BlockFlower(1)).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("flower2").setBlockTextureName("flower_rose"));
            blockRegistry.addObject(39, "brown_mushroom", (new BlockMushroom( )).setHardness(0.0F).setStepSound(soundTypeGrass).setLightValue(0.125F).setBlockName("mushroom").setBlockTextureName("mushroom_brown"));
            blockRegistry.addObject(40, "red_mushroom", (new BlockMushroom( )).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("mushroom").setBlockTextureName("mushroom_red"));
            blockRegistry.addObject(41, "gold_block", (new BlockCompressed(MapColor.field_151647_F)).setHardness(3.0F).setResistance(10.0F).setStepSound(soundTypeMetal).setBlockName("blockGold").setBlockTextureName("gold_block"));
            blockRegistry.addObject(42, "iron_block", (new BlockCompressed(MapColor.field_151668_h)).setHardness(5.0F).setResistance(10.0F).setStepSound(soundTypeMetal).setBlockName("blockIron").setBlockTextureName("iron_block"));
            blockRegistry.addObject(43, "double_stone_slab", (new BlockStoneSlab(true)).setHardness(2.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("stoneSlab"));
            blockRegistry.addObject(44, "stone_slab", (new BlockStoneSlab(false)).setHardness(2.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("stoneSlab"));
            Block var3 = (new Block(Material.rock)).setHardness(2.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("brick").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("brick");
            blockRegistry.addObject(45, "brick_block", var3);
            blockRegistry.addObject(46, "tnt", (new BlockTNT( )).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("tnt").setBlockTextureName("tnt"));
            blockRegistry.addObject(47, "bookshelf", (new BlockBookshelf( )).setHardness(1.5F).setStepSound(soundTypeWood).setBlockName("bookshelf").setBlockTextureName("bookshelf"));
            blockRegistry.addObject(48, "mossy_cobblestone", (new Block(Material.rock)).setHardness(2.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("stoneMoss").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("cobblestone_mossy"));
            blockRegistry.addObject(49, "obsidian", (new BlockObsidian( )).setHardness(50.0F).setResistance(2000.0F).setStepSound(soundTypePiston).setBlockName("obsidian").setBlockTextureName("obsidian"));
            blockRegistry.addObject(50, "torch", (new BlockTorch( )).setHardness(0.0F).setLightValue(0.9375F).setStepSound(soundTypeWood).setBlockName("torch").setBlockTextureName("torch_on"));
            blockRegistry.addObject(51, "fire", (new BlockFire( )).setHardness(0.0F).setLightValue(1.0F).setStepSound(soundTypeWood).setBlockName("fire").disableStats( ).setBlockTextureName("fire"));
            blockRegistry.addObject(52, "mob_spawner", (new BlockMobSpawner( )).setHardness(5.0F).setStepSound(soundTypeMetal).setBlockName("mobSpawner").disableStats( ).setBlockTextureName("mob_spawner"));
            blockRegistry.addObject(53, "oak_stairs", (new BlockStairs(var1, 0)).setBlockName("stairsWood"));
            blockRegistry.addObject(54, "chest", (new BlockChest(0)).setHardness(2.5F).setStepSound(soundTypeWood).setBlockName("chest"));
            blockRegistry.addObject(55, "redstone_wire", (new BlockRedstoneWire( )).setHardness(0.0F).setStepSound(soundTypeStone).setBlockName("redstoneDust").disableStats( ).setBlockTextureName("redstone_dust"));
            blockRegistry.addObject(56, "diamond_ore", (new BlockOre( )).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("oreDiamond").setBlockTextureName("diamond_ore"));
            blockRegistry.addObject(57, "diamond_block", (new BlockCompressed(MapColor.field_151648_G)).setHardness(5.0F).setResistance(10.0F).setStepSound(soundTypeMetal).setBlockName("blockDiamond").setBlockTextureName("diamond_block"));
            blockRegistry.addObject(58, "crafting_table", (new BlockWorkbench( )).setHardness(2.5F).setStepSound(soundTypeWood).setBlockName("workbench").setBlockTextureName("crafting_table"));
            blockRegistry.addObject(59, "wheat", (new BlockCrops( )).setBlockName("crops").setBlockTextureName("wheat"));
            Block var4 = (new BlockFarmland( )).setHardness(0.6F).setStepSound(soundTypeGravel).setBlockName("farmland").setBlockTextureName("farmland");
            blockRegistry.addObject(60, "farmland", var4);
            blockRegistry.addObject(61, "furnace", (new BlockFurnace(false)).setHardness(3.5F).setStepSound(soundTypePiston).setBlockName("furnace").setCreativeTab(CreativeTabs.tabDecorations));
            blockRegistry.addObject(62, "lit_furnace", (new BlockFurnace(true)).setHardness(3.5F).setStepSound(soundTypePiston).setLightValue(0.875F).setBlockName("furnace"));
            blockRegistry.addObject(63, "standing_sign", (new BlockSign(typeof(TileEntitySign), true)).setHardness(1.0F).setStepSound(soundTypeWood).setBlockName("sign").disableStats( ));
            blockRegistry.addObject(64, "wooden_door", (new BlockDoor(Material.wood)).setHardness(3.0F).setStepSound(soundTypeWood).setBlockName("doorWood").disableStats( ).setBlockTextureName("door_wood"));
            blockRegistry.addObject(65, "ladder", (new BlockLadder( )).setHardness(0.4F).setStepSound(soundTypeLadder).setBlockName("ladder").setBlockTextureName("ladder"));
            blockRegistry.addObject(66, "rail", (new BlockRail( )).setHardness(0.7F).setStepSound(soundTypeMetal).setBlockName("rail").setBlockTextureName("rail_normal"));
            blockRegistry.addObject(67, "stone_stairs", (new BlockStairs(var0, 0)).setBlockName("stairsStone"));
            blockRegistry.addObject(68, "wall_sign", (new BlockSign(typeof(TileEntitySign), false)).setHardness(1.0F).setStepSound(soundTypeWood).setBlockName("sign").disableStats( ));
            blockRegistry.addObject(69, "lever", (new BlockLever( )).setHardness(0.5F).setStepSound(soundTypeWood).setBlockName("lever").setBlockTextureName("lever"));
            blockRegistry.addObject(70, "stone_pressure_plate", (new BlockPressurePlate("stone", Material.rock, BlockPressurePlate.Sensitivity.mobs)).setHardness(0.5F).setStepSound(soundTypePiston).setBlockName("pressurePlate"));
            blockRegistry.addObject(71, "iron_door", (new BlockDoor(Material.iron)).setHardness(5.0F).setStepSound(soundTypeMetal).setBlockName("doorIron").disableStats( ).setBlockTextureName("door_iron"));
            blockRegistry.addObject(72, "wooden_pressure_plate", (new BlockPressurePlate("planks_oak", Material.wood, BlockPressurePlate.Sensitivity.everything)).setHardness(0.5F).setStepSound(soundTypeWood).setBlockName("pressurePlate"));
            blockRegistry.addObject(73, "redstone_ore", (new BlockRedstoneOre(false)).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("oreRedstone").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("redstone_ore"));
            blockRegistry.addObject(74, "lit_redstone_ore", (new BlockRedstoneOre(true)).setLightValue(0.625F).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("oreRedstone").setBlockTextureName("redstone_ore"));
            blockRegistry.addObject(75, "unlit_redstone_torch", (new BlockRedstoneTorch(false)).setHardness(0.0F).setStepSound(soundTypeWood).setBlockName("notGate").setBlockTextureName("redstone_torch_off"));
            blockRegistry.addObject(76, "redstone_torch", (new BlockRedstoneTorch(true)).setHardness(0.0F).setLightValue(0.5F).setStepSound(soundTypeWood).setBlockName("notGate").setCreativeTab(CreativeTabs.tabRedstone).setBlockTextureName("redstone_torch_on"));
            blockRegistry.addObject(77, "stone_button", (new BlockButtonStone( )).setHardness(0.5F).setStepSound(soundTypePiston).setBlockName("button"));
            blockRegistry.addObject(78, "snow_layer", (new BlockSnow( )).setHardness(0.1F).setStepSound(soundTypeSnow).setBlockName("snow").setLightOpacity(0).setBlockTextureName("snow"));
            blockRegistry.addObject(79, "ice", (new BlockIce( )).setHardness(0.5F).setLightOpacity(3).setStepSound(soundTypeGlass).setBlockName("ice").setBlockTextureName("ice"));
            blockRegistry.addObject(80, "snow", (new BlockSnowBlock( )).setHardness(0.2F).setStepSound(soundTypeSnow).setBlockName("snow").setBlockTextureName("snow"));
            blockRegistry.addObject(81, "cactus", (new BlockCactus( )).setHardness(0.4F).setStepSound(soundTypeCloth).setBlockName("cactus").setBlockTextureName("cactus"));
            blockRegistry.addObject(82, "clay", (new BlockClay( )).setHardness(0.6F).setStepSound(soundTypeGravel).setBlockName("clay").setBlockTextureName("clay"));
            blockRegistry.addObject(83, "reeds", (new BlockReed( )).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("reeds").disableStats( ).setBlockTextureName("reeds"));
            blockRegistry.addObject(84, "jukebox", (new BlockJukebox( )).setHardness(2.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("jukebox").setBlockTextureName("jukebox"));
            blockRegistry.addObject(85, "fence", (new BlockFence("planks_oak", Material.wood)).setHardness(2.0F).setResistance(5.0F).setStepSound(soundTypeWood).setBlockName("fence"));
            Block var5 = (new BlockPumpkin(false)).setHardness(1.0F).setStepSound(soundTypeWood).setBlockName("pumpkin").setBlockTextureName("pumpkin");
            blockRegistry.addObject(86, "pumpkin", var5);
            blockRegistry.addObject(87, "netherrack", (new BlockNetherrack( )).setHardness(0.4F).setStepSound(soundTypePiston).setBlockName("hellrock").setBlockTextureName("netherrack"));
            blockRegistry.addObject(88, "soul_sand", (new BlockSoulSand( )).setHardness(0.5F).setStepSound(soundTypeSand).setBlockName("hellsand").setBlockTextureName("soul_sand"));
            blockRegistry.addObject(89, "glowstone", (new BlockGlowstone(Material.glass)).setHardness(0.3F).setStepSound(soundTypeGlass).setLightValue(1.0F).setBlockName("lightgem").setBlockTextureName("glowstone"));
            blockRegistry.addObject(90, "portal", (new BlockPortal( )).setHardness(-1.0F).setStepSound(soundTypeGlass).setLightValue(0.75F).setBlockName("portal").setBlockTextureName("portal"));
            blockRegistry.addObject(91, "lit_pumpkin", (new BlockPumpkin(true)).setHardness(1.0F).setStepSound(soundTypeWood).setLightValue(1.0F).setBlockName("litpumpkin").setBlockTextureName("pumpkin"));
            blockRegistry.addObject(92, "cake", (new BlockCake( )).setHardness(0.5F).setStepSound(soundTypeCloth).setBlockName("cake").disableStats( ).setBlockTextureName("cake"));
            blockRegistry.addObject(93, "unpowered_repeater", (new BlockRedstoneRepeater(false)).setHardness(0.0F).setStepSound(soundTypeWood).setBlockName("diode").disableStats( ).setBlockTextureName("repeater_off"));
            blockRegistry.addObject(94, "powered_repeater", (new BlockRedstoneRepeater(true)).setHardness(0.0F).setLightValue(0.625F).setStepSound(soundTypeWood).setBlockName("diode").disableStats( ).setBlockTextureName("repeater_on"));
            blockRegistry.addObject(95, "stained_glass", (new BlockStainedGlass(Material.glass)).setHardness(0.3F).setStepSound(soundTypeGlass).setBlockName("stainedGlass").setBlockTextureName("glass"));
            blockRegistry.addObject(96, "trapdoor", (new BlockTrapDoor(Material.wood)).setHardness(3.0F).setStepSound(soundTypeWood).setBlockName("trapdoor").disableStats( ).setBlockTextureName("trapdoor"));
            blockRegistry.addObject(97, "monster_egg", (new BlockSilverfish( )).setHardness(0.75F).setBlockName("monsterStoneEgg"));
            Block var6 = (new BlockStoneBrick( )).setHardness(1.5F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("stonebricksmooth").setBlockTextureName("stonebrick");
            blockRegistry.addObject(98, "stonebrick", var6);
            blockRegistry.addObject(99, "brown_mushroom_block", (new BlockHugeMushroom(Material.wood, 0)).setHardness(0.2F).setStepSound(soundTypeWood).setBlockName("mushroom").setBlockTextureName("mushroom_block"));
            blockRegistry.addObject(100, "red_mushroom_block", (new BlockHugeMushroom(Material.wood, 1)).setHardness(0.2F).setStepSound(soundTypeWood).setBlockName("mushroom").setBlockTextureName("mushroom_block"));
            blockRegistry.addObject(101, "iron_bars", (new BlockPane("iron_bars", "iron_bars", Material.iron, true)).setHardness(5.0F).setResistance(10.0F).setStepSound(soundTypeMetal).setBlockName("fenceIron"));
            blockRegistry.addObject(102, "glass_pane", (new BlockPane("glass", "glass_pane_top", Material.glass, false)).setHardness(0.3F).setStepSound(soundTypeGlass).setBlockName("thinGlass"));
            Block var7 = (new BlockMelon( )).setHardness(1.0F).setStepSound(soundTypeWood).setBlockName("melon").setBlockTextureName("melon");
            blockRegistry.addObject(103, "melon_block", var7);
            blockRegistry.addObject(104, "pumpkin_stem", (new BlockStem(var5)).setHardness(0.0F).setStepSound(soundTypeWood).setBlockName("pumpkinStem").setBlockTextureName("pumpkin_stem"));
            blockRegistry.addObject(105, "melon_stem", (new BlockStem(var7)).setHardness(0.0F).setStepSound(soundTypeWood).setBlockName("pumpkinStem").setBlockTextureName("melon_stem"));
            blockRegistry.addObject(106, "vine", (new BlockVine( )).setHardness(0.2F).setStepSound(soundTypeGrass).setBlockName("vine").setBlockTextureName("vine"));
            blockRegistry.addObject(107, "fence_gate", (new BlockFenceGate( )).setHardness(2.0F).setResistance(5.0F).setStepSound(soundTypeWood).setBlockName("fenceGate"));
            blockRegistry.addObject(108, "brick_stairs", (new BlockStairs(var3, 0)).setBlockName("stairsBrick"));
            blockRegistry.addObject(109, "stone_brick_stairs", (new BlockStairs(var6, 0)).setBlockName("stairsStoneBrickSmooth"));
            blockRegistry.addObject(110, "mycelium", (new BlockMycelium( )).setHardness(0.6F).setStepSound(soundTypeGrass).setBlockName("mycel").setBlockTextureName("mycelium"));
            blockRegistry.addObject(111, "waterlily", (new BlockLilyPad( )).setHardness(0.0F).setStepSound(soundTypeGrass).setBlockName("waterlily").setBlockTextureName("waterlily"));
            Block var8 = (new Block(Material.rock)).setHardness(2.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("netherBrick").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("nether_brick");
            blockRegistry.addObject(112, "nether_brick", var8);
            blockRegistry.addObject(113, "nether_brick_fence", (new BlockFence("nether_brick", Material.rock)).setHardness(2.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("netherFence"));
            blockRegistry.addObject(114, "nether_brick_stairs", (new BlockStairs(var8, 0)).setBlockName("stairsNetherBrick"));
            blockRegistry.addObject(115, "nether_wart", (new BlockNetherWart( )).setBlockName("netherStalk").setBlockTextureName("nether_wart"));
            blockRegistry.addObject(116, "enchanting_table", (new BlockEnchantmentTable( )).setHardness(5.0F).setResistance(2000.0F).setBlockName("enchantmentTable").setBlockTextureName("enchanting_table"));
            blockRegistry.addObject(117, "brewing_stand", (new BlockBrewingStand( )).setHardness(0.5F).setLightValue(0.125F).setBlockName("brewingStand").setBlockTextureName("brewing_stand"));
            blockRegistry.addObject(118, "cauldron", (new BlockCauldron( )).setHardness(2.0F).setBlockName("cauldron").setBlockTextureName("cauldron"));
            blockRegistry.addObject(119, "end_portal", (new BlockEndPortal(Material.Portal)).setHardness(-1.0F).setResistance(6000000.0F));
            blockRegistry.addObject(120, "end_portal_frame", (new BlockEndPortalFrame( )).setStepSound(soundTypeGlass).setLightValue(0.125F).setHardness(-1.0F).setBlockName("endPortalFrame").setResistance(6000000.0F).setCreativeTab(CreativeTabs.tabDecorations).setBlockTextureName("endframe"));
            blockRegistry.addObject(121, "end_stone", (new Block(Material.rock)).setHardness(3.0F).setResistance(15.0F).setStepSound(soundTypePiston).setBlockName("whiteStone").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("end_stone"));
            blockRegistry.addObject(122, "dragon_egg", (new BlockDragonEgg( )).setHardness(3.0F).setResistance(15.0F).setStepSound(soundTypePiston).setLightValue(0.125F).setBlockName("dragonEgg").setBlockTextureName("dragon_egg"));
            blockRegistry.addObject(123, "redstone_lamp", (new BlockRedstoneLight(false)).setHardness(0.3F).setStepSound(soundTypeGlass).setBlockName("redstoneLight").setCreativeTab(CreativeTabs.tabRedstone).setBlockTextureName("redstone_lamp_off"));
            blockRegistry.addObject(124, "lit_redstone_lamp", (new BlockRedstoneLight(true)).setHardness(0.3F).setStepSound(soundTypeGlass).setBlockName("redstoneLight").setBlockTextureName("redstone_lamp_on"));
            blockRegistry.addObject(125, "double_wooden_slab", (new BlockWoodSlab(true)).setHardness(2.0F).setResistance(5.0F).setStepSound(soundTypeWood).setBlockName("woodSlab"));
            blockRegistry.addObject(126, "wooden_slab", (new BlockWoodSlab(false)).setHardness(2.0F).setResistance(5.0F).setStepSound(soundTypeWood).setBlockName("woodSlab"));
            blockRegistry.addObject(127, "cocoa", (new BlockCocoa( )).setHardness(0.2F).setResistance(5.0F).setStepSound(soundTypeWood).setBlockName("cocoa").setBlockTextureName("cocoa"));
            blockRegistry.addObject(128, "sandstone_stairs", (new BlockStairs(var2, 0)).setBlockName("stairsSandStone"));
            blockRegistry.addObject(129, "emerald_ore", (new BlockOre( )).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("oreEmerald").setBlockTextureName("emerald_ore"));
            blockRegistry.addObject(130, "ender_chest", (new BlockEnderChest( )).setHardness(22.5F).setResistance(1000.0F).setStepSound(soundTypePiston).setBlockName("enderChest").setLightValue(0.5F));
            blockRegistry.addObject(131, "tripwire_hook", (new BlockTripWireHook( )).setBlockName("tripWireSource").setBlockTextureName("trip_wire_source"));
            blockRegistry.addObject(132, "tripwire", (new BlockTripWire( )).setBlockName("tripWire").setBlockTextureName("trip_wire"));
            blockRegistry.addObject(133, "emerald_block", (new BlockCompressed(MapColor.field_151653_I)).setHardness(5.0F).setResistance(10.0F).setStepSound(soundTypeMetal).setBlockName("blockEmerald").setBlockTextureName("emerald_block"));
            blockRegistry.addObject(134, "spruce_stairs", (new BlockStairs(var1, 1)).setBlockName("stairsWoodSpruce"));
            blockRegistry.addObject(135, "birch_stairs", (new BlockStairs(var1, 2)).setBlockName("stairsWoodBirch"));
            blockRegistry.addObject(136, "jungle_stairs", (new BlockStairs(var1, 3)).setBlockName("stairsWoodJungle"));
            blockRegistry.addObject(137, "command_block", (new BlockCommandBlock( )).setBlockUnbreakable( ).setResistance(6000000.0F).setBlockName("commandBlock").setBlockTextureName("command_block"));
            blockRegistry.addObject(138, "beacon", (new BlockBeacon( )).setBlockName("beacon").setLightValue(1.0F).setBlockTextureName("beacon"));
            blockRegistry.addObject(139, "cobblestone_wall", (new BlockWall(var0)).setBlockName("cobbleWall"));
            blockRegistry.addObject(140, "flower_pot", (new BlockFlowerPot( )).setHardness(0.0F).setStepSound(soundTypeStone).setBlockName("flowerPot").setBlockTextureName("flower_pot"));
            blockRegistry.addObject(141, "carrots", (new BlockCarrot( )).setBlockName("carrots").setBlockTextureName("carrots"));
            blockRegistry.addObject(142, "potatoes", (new BlockPotato( )).setBlockName("potatoes").setBlockTextureName("potatoes"));
            blockRegistry.addObject(143, "wooden_button", (new BlockButtonWood( )).setHardness(0.5F).setStepSound(soundTypeWood).setBlockName("button"));
            blockRegistry.addObject(144, "skull", (new BlockSkull( )).setHardness(1.0F).setStepSound(soundTypePiston).setBlockName("skull").setBlockTextureName("skull"));
            blockRegistry.addObject(145, "anvil", (new BlockAnvil( )).setHardness(5.0F).setStepSound(soundTypeAnvil).setResistance(2000.0F).setBlockName("anvil"));
            blockRegistry.addObject(146, "trapped_chest", (new BlockChest(1)).setHardness(2.5F).setStepSound(soundTypeWood).setBlockName("chestTrap"));
            blockRegistry.addObject(147, "light_weighted_pressure_plate", (new BlockPressurePlateWeighted("gold_block", Material.iron, 15)).setHardness(0.5F).setStepSound(soundTypeWood).setBlockName("weightedPlate_light"));
            blockRegistry.addObject(148, "heavy_weighted_pressure_plate", (new BlockPressurePlateWeighted("iron_block", Material.iron, 150)).setHardness(0.5F).setStepSound(soundTypeWood).setBlockName("weightedPlate_heavy"));
            blockRegistry.addObject(149, "unpowered_comparator", (new BlockRedstoneComparator(false)).setHardness(0.0F).setStepSound(soundTypeWood).setBlockName("comparator").disableStats( ).setBlockTextureName("comparator_off"));
            blockRegistry.addObject(150, "powered_comparator", (new BlockRedstoneComparator(true)).setHardness(0.0F).setLightValue(0.625F).setStepSound(soundTypeWood).setBlockName("comparator").disableStats( ).setBlockTextureName("comparator_on"));
            blockRegistry.addObject(151, "daylight_detector", (new BlockDaylightDetector( )).setHardness(0.2F).setStepSound(soundTypeWood).setBlockName("daylightDetector").setBlockTextureName("daylight_detector"));
            blockRegistry.addObject(152, "redstone_block", (new BlockCompressedPowered(MapColor.field_151656_f)).setHardness(5.0F).setResistance(10.0F).setStepSound(soundTypeMetal).setBlockName("blockRedstone").setBlockTextureName("redstone_block"));
            blockRegistry.addObject(153, "quartz_ore", (new BlockOre( )).setHardness(3.0F).setResistance(5.0F).setStepSound(soundTypePiston).setBlockName("netherquartz").setBlockTextureName("quartz_ore"));
            blockRegistry.addObject(154, "hopper", (new BlockHopper( )).setHardness(3.0F).setResistance(8.0F).setStepSound(soundTypeWood).setBlockName("hopper").setBlockTextureName("hopper"));
            Block var9 = (new BlockQuartz( )).setStepSound(soundTypePiston).setHardness(0.8F).setBlockName("quartzBlock").setBlockTextureName("quartz_block");
            blockRegistry.addObject(155, "quartz_block", var9);
            blockRegistry.addObject(156, "quartz_stairs", (new BlockStairs(var9, 0)).setBlockName("stairsQuartz"));
            blockRegistry.addObject(157, "activator_rail", (new BlockRailPowered( )).setHardness(0.7F).setStepSound(soundTypeMetal).setBlockName("activatorRail").setBlockTextureName("rail_activator"));
            blockRegistry.addObject(158, "dropper", (new BlockDropper( )).setHardness(3.5F).setStepSound(soundTypePiston).setBlockName("dropper").setBlockTextureName("dropper"));
            blockRegistry.addObject(159, "stained_hardened_clay", (new BlockColored(Material.rock)).setHardness(1.25F).setResistance(7.0F).setStepSound(soundTypePiston).setBlockName("clayHardenedStained").setBlockTextureName("hardened_clay_stained"));
            blockRegistry.addObject(160, "stained_glass_pane", (new BlockStainedGlassPane( )).setHardness(0.3F).setStepSound(soundTypeGlass).setBlockName("thinStainedGlass").setBlockTextureName("glass"));
            blockRegistry.addObject(161, "leaves2", (new BlockNewLeaf( )).setBlockName("leaves").setBlockTextureName("leaves"));
            blockRegistry.addObject(162, "log2", (new BlockNewLog( )).setBlockName("log").setBlockTextureName("log"));
            blockRegistry.addObject(163, "acacia_stairs", (new BlockStairs(var1, 4)).setBlockName("stairsWoodAcacia"));
            blockRegistry.addObject(164, "dark_oak_stairs", (new BlockStairs(var1, 5)).setBlockName("stairsWoodDarkOak"));
            blockRegistry.addObject(170, "hay_block", (new BlockHay( )).setHardness(0.5F).setStepSound(soundTypeGrass).setBlockName("hayBlock").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("hay_block"));
            blockRegistry.addObject(171, "carpet", (new BlockCarpet( )).setHardness(0.1F).setStepSound(soundTypeCloth).setBlockName("woolCarpet").setLightOpacity(0));
            blockRegistry.addObject(172, "hardened_clay", (new BlockHardenedClay( )).setHardness(1.25F).setResistance(7.0F).setStepSound(soundTypePiston).setBlockName("clayHardened").setBlockTextureName("hardened_clay"));
            blockRegistry.addObject(173, "coal_block", (new Block(Material.rock)).setHardness(5.0F).setResistance(10.0F).setStepSound(soundTypePiston).setBlockName("blockCoal").setCreativeTab(CreativeTabs.tabBlock).setBlockTextureName("coal_block"));
            blockRegistry.addObject(174, "packed_ice", (new BlockPackedIce( )).setHardness(0.5F).setStepSound(soundTypeGlass).setBlockName("icePacked").setBlockTextureName("ice_packed"));
            blockRegistry.addObject(175, "double_plant", new BlockDoublePlant( ));
            IEnumerator var10 = blockRegistry.GetEnumerator( );

            while (var10.MoveNext( ))
            {
                Block var11 = (Block)var10.Current;

                if (var11.BlockMaterial == Material.air)
                {
                    var11.field_149783_u = false;
                }
                else
                {
                    bool var12 = false;
                    bool var13 = var11.RenderType == 10;
                    bool var14 = var11 is BlockSlab;
                    bool var15 = var11 == var4;
                    bool var16 = var11.canBlockGrass;
                    bool var17 = var11.LightOpacity == 0;

                    if (var13 || var14 || var15 || var16 || var17)
                    {
                        var12 = true;
                    }

                    var11.field_149783_u = var12;
                }
            }
        }

        protected internal Block(Material p_i45394_1_)
        {
            this.stepSound = soundTypeStone;
            this.blockParticleGravity = 1.0F;
            this.slipperiness = 0.6F;
            this.BlockMaterial = p_i45394_1_;
            this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
            this.OpaqueCube = true;
            this.LightOpacity = this.OpaqueCube ? 255 : 0;
            this.canBlockGrass = !p_i45394_1_.CanBlockGrass;
        }

        public virtual Block setBlockName(String name)
        {
            BlockName = name;
            return this;
        }

        public virtual String BlockName
        {
            get;
            set;
        }

        public virtual Block setHardness(float value)
        {
            Hardness = value;
            return this;
        }

        public virtual float Hardness
        {
            get;
            set;
        }

        public virtual Block setResistance(float value)
        {
            Resistance = value;
            return this;
        }

        public virtual float Resistance
        {
            get;
            set;
        }

        public virtual Block setStepSound(SoundType value)
        {
            StepSound = value;
            return this;
        }
   
        public virtual SoundType StepSound
        {
            get;
            set;
        }

        public virtual Block setBlockTextureName(String value)
        {
            BlockTextureName = value;
            return this;
        }

        public virtual String BlockTextureName
        {
            get;
            set;
        }

        public virtual Block setCreativeTab(CreativeTabs value)
        {
            CreativeTab = value;
            return this;
        }

        public virtual CreativeTabs CreativeTab
        {
            get;
            set;
        }

        public virtual Block setLightValue(float value)
        {
            LightValue = (int)(15.0F * value);
            return this;
        }

        public virtual int LightValue
        {
            get;
            set;
        }


        public virtual Block setLightOpacity(int value)
        {
            LightOpacity = value;
            return this;
        }

        public virtual int LightOpacity
        {
            get;
            set;
        }

        public virtual bool OpaqueCube
		{
			get;
            protected set;
		}

        ///    
        ///     <summary> * Sets whether this block type will receive random update ticks </summary>
        ///     
        protected internal virtual Boolean TickRandomly
        {
            get;
            set;
        }

        protected virtual Block setTickRandomly(bool value)
        {
            TickRandomly = value;
            return this;
        }

        ///    
        ///     <summary> * Indicate if a material is a normal solid opaque cube </summary>
        ///     
        public virtual bool isBlockNormalCube( )
		{
			return this.BlockMaterial.blocksMovement() && this.renderAsNormalBlock();
		}

        public virtual bool isNormalCube( )
		{
			return this.BlockMaterial.Opaque && this.renderAsNormalBlock() && !this.canProvidePower();
		}

        public virtual bool renderAsNormalBlock( )
        {
            return true;
        }

        public virtual bool getBlocksMovement(IBlockAccess p_149655_1_, int p_149655_2_, int p_149655_3_, int p_149655_4_)
        {
            return !this.BlockMaterial.blocksMovement( );
        }

        ///    
        ///     <summary> * The type of render function that is called for this block </summary>
        ///     
        public virtual int RenderType
        {
            get
            {
                return 0;
            }
        }

        protected internal virtual Block setBlockUnbreakable( )
        {
            this.Hardness = -1.0F;
            return this;
        }

        public virtual float getBlockHardness(World p_149712_1_, int p_149712_2_, int p_149712_3_, int p_149712_4_)
        {
            return this.blockHardness;
        }

        ///    
        ///     <summary> * Returns whether or not this block is of a type that needs random ticking. Called for ref-counting purposes by
        ///     * ExtendedBlockStorage in order to broadly cull a chunk from the random chunk update list for efficiency's sake. </summary>
        ///     

        public virtual bool hasTileEntity( )
        {
            return this.isBlockContainer;
        }

        protected internal void setBlockBounds(float p_149676_1_, float p_149676_2_, float p_149676_3_, float p_149676_4_, float p_149676_5_, float p_149676_6_)
        {
            this.field_149759_B = (double)p_149676_1_;
            this.field_149760_C = (double)p_149676_2_;
            this.field_149754_D = (double)p_149676_3_;
            this.field_149755_E = (double)p_149676_4_;
            this.field_149756_F = (double)p_149676_5_;
            this.field_149757_G = (double)p_149676_6_;
        }

        public virtual int getBlockBrightness(IBlockAccess p_149677_1_, int p_149677_2_, int p_149677_3_, int p_149677_4_)
        {
            Block var5 = p_149677_1_.getBlock(p_149677_2_, p_149677_3_, p_149677_4_);
            int var6 = p_149677_1_.getLightBrightnessForSkyBlocks(p_149677_2_, p_149677_3_, p_149677_4_, var5.LightValue);

            if (var6 == 0 && var5 is BlockSlab)
            {
                --p_149677_3_;
                var5 = p_149677_1_.getBlock(p_149677_2_, p_149677_3_, p_149677_4_);
                return p_149677_1_.getLightBrightnessForSkyBlocks(p_149677_2_, p_149677_3_, p_149677_4_, var5.LightValue);
            }
            else
            {
                return var6;
            }
        }

        public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
        {
            return p_149646_5_ == 0 && this.field_149760_C > 0.0D ? true : (p_149646_5_ == 1 && this.field_149756_F < 1.0D ? true : (p_149646_5_ == 2 && this.field_149754_D > 0.0D ? true : (p_149646_5_ == 3 && this.field_149757_G < 1.0D ? true : (p_149646_5_ == 4 && this.field_149759_B > 0.0D ? true : (p_149646_5_ == 5 && this.field_149755_E < 1.0D ? true : !p_149646_1_.getBlock(p_149646_2_, p_149646_3_, p_149646_4_).OpaqueCube)))));
        }

        public virtual bool isBlockSolid(IBlockAccess p_149747_1_, int p_149747_2_, int p_149747_3_, int p_149747_4_, int p_149747_5_)
        {
            return p_149747_1_.getBlock(p_149747_2_, p_149747_3_, p_149747_4_).BlockMaterial.Solid;
        }

        ///    
        ///     <summary> * Returns the bounding box of the wired rectangular prism to render. </summary>
        ///     
        public virtual AxisAlignedBB getSelectedBoundingBoxFromPool(World p_149633_1_, int p_149633_2_, int p_149633_3_, int p_149633_4_)
        {
            return AxisAlignedBB.getBoundingBox((double)p_149633_2_ + this.field_149759_B, (double)p_149633_3_ + this.field_149760_C, (double)p_149633_4_ + this.field_149754_D, (double)p_149633_2_ + this.field_149755_E, (double)p_149633_3_ + this.field_149756_F, (double)p_149633_4_ + this.field_149757_G);
        }

        public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
        {
            AxisAlignedBB var8 = this.getCollisionBoundingBoxFromPool(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_);

            if (var8 != null && p_149743_5_.intersectsWith(var8))
            {
                p_149743_6_.Add(var8);
            }
        }

        ///    
        ///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
        ///     * cleared to be reused) </summary>
        ///     
        public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
        {
            return AxisAlignedBB.getBoundingBox((double)p_149668_2_ + this.field_149759_B, (double)p_149668_3_ + this.field_149760_C, (double)p_149668_4_ + this.field_149754_D, (double)p_149668_2_ + this.field_149755_E, (double)p_149668_3_ + this.field_149756_F, (double)p_149668_4_ + this.field_149757_G);
        }

        

        ///    
        ///     * Returns whether this block is collideable based on the arguments passed in \n<param name="par1"> block metaData \n@param
        ///     * par2 whether the player right-clicked while holding a boat </param>
        ///     
        public virtual bool canCollideCheck(int p_149678_1_, bool p_149678_2_)
        {
            return isCollidable();
        }

        public virtual bool isCollidable()
		{
			return true;
		}

        ///    
        ///     <summary> * Ticks the block if it's been scheduled </summary>
        ///     
        public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
        {
        }

        ///    
        ///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
        ///     
        public virtual void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
        {
        }

        public virtual void onBlockDestroyedByPlayer(World p_149664_1_, int p_149664_2_, int p_149664_3_, int p_149664_4_, int p_149664_5_)
        {
        }

        public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
        {
        }

        public virtual int func_149738_a(World p_149738_1_)
        {
            return 10;
        }

        public virtual void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
        {
        }

        public virtual void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
        {
        }

        ///    
        ///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
        ///     
        public virtual int quantityDropped(Random p_149745_1_)
        {
            return 1;
        }

        public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
        {
            return Item.getItemFromBlock(this);
        }

        public virtual float getPlayerRelativeBlockHardness(EntityPlayer p_149737_1_, World p_149737_2_, int p_149737_3_, int p_149737_4_, int p_149737_5_)
        {
            float var6 = this.getBlockHardness(p_149737_2_, p_149737_3_, p_149737_4_, p_149737_5_);
            return var6 < 0.0F ? 0.0F : (!p_149737_1_.canHarvestBlock(this) ? p_149737_1_.getCurrentPlayerStrVsBlock(this, false) / var6 / 100.0F : p_149737_1_.getCurrentPlayerStrVsBlock(this, true) / var6 / 30.0F);
        }

        ///    
        ///     <summary> * Drops the specified block items </summary>
        ///     
        public void dropBlockAsItem(World p_149697_1_, int p_149697_2_, int p_149697_3_, int p_149697_4_, int p_149697_5_, int p_149697_6_)
        {
            this.dropBlockAsItemWithChance(p_149697_1_, p_149697_2_, p_149697_3_, p_149697_4_, p_149697_5_, 1.0F, p_149697_6_);
        }

        ///    
        ///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
        ///     
        public virtual void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
        {
            if (!p_149690_1_.isClient)
            {
                int var8 = this.quantityDroppedWithBonus(p_149690_7_, p_149690_1_.rand);

                for (int var9 = 0; var9 < var8; ++var9)
                {
                    if (p_149690_1_.rand.NextFloat( ) <= p_149690_6_)
                    {
                        Item var10 = this.getItemDropped(p_149690_5_, p_149690_1_.rand, p_149690_7_);

                        if (var10 != null)
                        {
                            this.dropBlockAsItem_do(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, new ItemStack(var10, 1, this.damageDropped(p_149690_5_)));
                        }
                    }
                }
            }
        }

        ///    
        ///     <summary> * Spawns EntityItem in the world for the given ItemStack if the world is not remote. </summary>
        ///     
        protected internal virtual void dropBlockAsItem_do(World p_149642_1_, int p_149642_2_, int p_149642_3_, int p_149642_4_, ItemStack p_149642_5_)
        {
            if (!p_149642_1_.isClient && p_149642_1_.GameRules.getGameRuleBooleanValue("doTileDrops"))
            {
                float var6 = 0.7F;
                double var7 = (double)(p_149642_1_.rand.NextFloat( ) * var6) + (double)(1.0F - var6) * 0.5D;
                double var9 = (double)(p_149642_1_.rand.NextFloat( ) * var6) + (double)(1.0F - var6) * 0.5D;
                double var11 = (double)(p_149642_1_.rand.NextFloat( ) * var6) + (double)(1.0F - var6) * 0.5D;
                EntityItem var13 = new EntityItem(p_149642_1_, (double)p_149642_2_ + var7, (double)p_149642_3_ + var9, (double)p_149642_4_ + var11, p_149642_5_);
                var13.delayBeforeCanPickup = 10;
                p_149642_1_.spawnEntityInWorld(var13);
            }
        }

        protected internal virtual void dropXpOnBlockBreak(World p_149657_1_, int p_149657_2_, int p_149657_3_, int p_149657_4_, int p_149657_5_)
        {
            if (!p_149657_1_.isClient)
            {
                while (p_149657_5_ > 0)
                {
                    int var6 = EntityXPOrb.getXPSplit(p_149657_5_);
                    p_149657_5_ -= var6;
                    p_149657_1_.spawnEntityInWorld(new EntityXPOrb(p_149657_1_, (double)p_149657_2_ + 0.5D, (double)p_149657_3_ + 0.5D, (double)p_149657_4_ + 0.5D, var6));
                }
            }
        }

        ///    
        ///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
        ///     
        public virtual int damageDropped(int p_149692_1_)
        {
            return 0;
        }

        ///    
        ///     <summary> * Returns how much this block can resist explosions from the passed in entity. </summary>
        ///     
        public virtual float getExplosionResistance(Entity p_149638_1_)
        {
            return this.blockResistance / 5.0F;
        }

        public virtual MovingObjectPosition collisionRayTrace(World p_149731_1_, int p_149731_2_, int p_149731_3_, int p_149731_4_, Vec3 p_149731_5_, Vec3 p_149731_6_)
        {
            this.setBlockBoundsBasedOnState(p_149731_1_, p_149731_2_, p_149731_3_, p_149731_4_);
            p_149731_5_ = p_149731_5_.addVector((double)(-p_149731_2_), (double)(-p_149731_3_), (double)(-p_149731_4_));
            p_149731_6_ = p_149731_6_.addVector((double)(-p_149731_2_), (double)(-p_149731_3_), (double)(-p_149731_4_));
            Vec3 var7 = p_149731_5_.getIntermediateWithXValue(p_149731_6_, this.field_149759_B);
            Vec3 var8 = p_149731_5_.getIntermediateWithXValue(p_149731_6_, this.field_149755_E);
            Vec3 var9 = p_149731_5_.getIntermediateWithYValue(p_149731_6_, this.field_149760_C);
            Vec3 var10 = p_149731_5_.getIntermediateWithYValue(p_149731_6_, this.field_149756_F);
            Vec3 var11 = p_149731_5_.getIntermediateWithZValue(p_149731_6_, this.field_149754_D);
            Vec3 var12 = p_149731_5_.getIntermediateWithZValue(p_149731_6_, this.field_149757_G);

            if (!this.isVecInsideYZBounds(var7))
            {
                var7 = null;
            }

            if (!this.isVecInsideYZBounds(var8))
            {
                var8 = null;
            }

            if (!this.isVecInsideXZBounds(var9))
            {
                var9 = null;
            }

            if (!this.isVecInsideXZBounds(var10))
            {
                var10 = null;
            }

            if (!this.isVecInsideXYBounds(var11))
            {
                var11 = null;
            }

            if (!this.isVecInsideXYBounds(var12))
            {
                var12 = null;
            }

            Vec3 var13 = null;

            if (var7 != null && (var13 == null || p_149731_5_.squareDistanceTo(var7) < p_149731_5_.squareDistanceTo(var13)))
            {
                var13 = var7;
            }

            if (var8 != null && (var13 == null || p_149731_5_.squareDistanceTo(var8) < p_149731_5_.squareDistanceTo(var13)))
            {
                var13 = var8;
            }

            if (var9 != null && (var13 == null || p_149731_5_.squareDistanceTo(var9) < p_149731_5_.squareDistanceTo(var13)))
            {
                var13 = var9;
            }

            if (var10 != null && (var13 == null || p_149731_5_.squareDistanceTo(var10) < p_149731_5_.squareDistanceTo(var13)))
            {
                var13 = var10;
            }

            if (var11 != null && (var13 == null || p_149731_5_.squareDistanceTo(var11) < p_149731_5_.squareDistanceTo(var13)))
            {
                var13 = var11;
            }

            if (var12 != null && (var13 == null || p_149731_5_.squareDistanceTo(var12) < p_149731_5_.squareDistanceTo(var13)))
            {
                var13 = var12;
            }

            if (var13 == null)
            {
                return null;
            }
            else
            {
                sbyte var14 = -1;

                if (var13 == var7)
                {
                    var14 = 4;
                }

                if (var13 == var8)
                {
                    var14 = 5;
                }

                if (var13 == var9)
                {
                    var14 = 0;
                }

                if (var13 == var10)
                {
                    var14 = 1;
                }

                if (var13 == var11)
                {
                    var14 = 2;
                }

                if (var13 == var12)
                {
                    var14 = 3;
                }

                return new MovingObjectPosition(p_149731_2_, p_149731_3_, p_149731_4_, var14, var13.addVector((double)p_149731_2_, (double)p_149731_3_, (double)p_149731_4_));
            }
        }

        ///    
        ///     <summary> * Checks if a vector is within the Y and Z bounds of the block. </summary>
        ///     
        private bool isVecInsideYZBounds(Vec3 p_149654_1_)
        {
            return p_149654_1_ == null ? false : p_149654_1_.yCoord >= this.field_149760_C && p_149654_1_.yCoord <= this.field_149756_F && p_149654_1_.zCoord >= this.field_149754_D && p_149654_1_.zCoord <= this.field_149757_G;
        }

        ///    
        ///     <summary> * Checks if a vector is within the X and Z bounds of the block. </summary>
        ///     
        private bool isVecInsideXZBounds(Vec3 p_149687_1_)
        {
            return p_149687_1_ == null ? false : p_149687_1_.xCoord >= this.field_149759_B && p_149687_1_.xCoord <= this.field_149755_E && p_149687_1_.zCoord >= this.field_149754_D && p_149687_1_.zCoord <= this.field_149757_G;
        }

        ///    
        ///     <summary> * Checks if a vector is within the X and Y bounds of the block. </summary>
        ///     
        private bool isVecInsideXYBounds(Vec3 p_149661_1_)
        {
            return p_149661_1_ == null ? false : p_149661_1_.xCoord >= this.field_149759_B && p_149661_1_.xCoord <= this.field_149755_E && p_149661_1_.yCoord >= this.field_149760_C && p_149661_1_.yCoord <= this.field_149756_F;
        }

        ///    
        ///     <summary> * Called upon the block being destroyed by an explosion </summary>
        ///     
        public virtual void onBlockDestroyedByExplosion(World p_149723_1_, int p_149723_2_, int p_149723_3_, int p_149723_4_, Explosion p_149723_5_)
        {
        }

        ///    
        ///     <summary> * Returns which pass should this block be rendered on. 0 for solids and 1 for alpha </summary>
        ///     
        public virtual int RenderBlockPass
        {
            get
            {
                return 0;
            }
        }

        public virtual bool canReplace(World p_149705_1_, int p_149705_2_, int p_149705_3_, int p_149705_4_, int p_149705_5_, ItemStack p_149705_6_)
        {
            return this.canPlaceBlockOnSide(p_149705_1_, p_149705_2_, p_149705_3_, p_149705_4_, p_149705_5_);
        }

        ///    
        ///     <summary> * checks to see if you can place this block can be placed on that side of a block: BlockLever overrides </summary>
        ///     
        public virtual bool canPlaceBlockOnSide(World p_149707_1_, int p_149707_2_, int p_149707_3_, int p_149707_4_, int p_149707_5_)
        {
            return this.canPlaceBlockAt(p_149707_1_, p_149707_2_, p_149707_3_, p_149707_4_);
        }

        public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
        {
            return p_149742_1_.getBlock(p_149742_2_, p_149742_3_, p_149742_4_).BlockMaterial.Replaceable;
        }

        ///    
        ///     <summary> * Called upon block activation (right click on the block.) </summary>
        ///     
        public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
        {
            return false;
        }

        public virtual void onEntityWalking(World p_149724_1_, int p_149724_2_, int p_149724_3_, int p_149724_4_, Entity p_149724_5_)
        {
        }

        public virtual int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
        {
            return p_149660_9_;
        }

        ///    
        ///     <summary> * Called when a player hits the block. Args: world, x, y, z, player </summary>
        ///     
        public virtual void onBlockClicked(World p_149699_1_, int p_149699_2_, int p_149699_3_, int p_149699_4_, EntityPlayer p_149699_5_)
        {
        }

        public virtual void velocityToAddToEntity(World p_149640_1_, int p_149640_2_, int p_149640_3_, int p_149640_4_, Entity p_149640_5_, Vec3 p_149640_6_)
        {
        }

        public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
        {
        }

        ///    
        ///     <summary> * returns the block bounderies minX value </summary>
        ///     
        public double BlockBoundsMinX
        {
            get
            {
                return this.field_149759_B;
            }
        }

        ///    
        ///     <summary> * returns the block bounderies maxX value </summary>
        ///     
        public double BlockBoundsMaxX
        {
            get
            {
                return this.field_149755_E;
            }
        }

        ///    
        ///     <summary> * returns the block bounderies minY value </summary>
        ///     
        public double BlockBoundsMinY
        {
            get
            {
                return this.field_149760_C;
            }
        }

        ///    
        ///     <summary> * returns the block bounderies maxY value </summary>
        ///     
        public double BlockBoundsMaxY
        {
            get
            {
                return this.field_149756_F;
            }
        }

        ///    
        ///     <summary> * returns the block bounderies minZ value </summary>
        ///     
        public double BlockBoundsMinZ
        {
            get
            {
                return this.field_149754_D;
            }
        }

        ///    
        ///     <summary> * returns the block bounderies maxZ value </summary>
        ///     
        public double BlockBoundsMaxZ
        {
            get
            {
                return this.field_149757_G;
            }
        }

        public virtual int BlockColor
        {
            get
            {
                return 16777215;
            }
        }

        ///    
        ///     <summary> * Returns the color this block should be rendered. Used by leaves. </summary>
        ///     
        public virtual int getRenderColor(int p_149741_1_)
        {
            return 16777215;
        }

        ///    
        ///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
        ///     * when first determining what to render. </summary>
        ///     
        public virtual int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
        {
            return 16777215;
        }

        public virtual int isProvidingWeakPower(IBlockAccess p_149709_1_, int p_149709_2_, int p_149709_3_, int p_149709_4_, int p_149709_5_)
        {
            return 0;
        }

        ///    
        ///     <summary> * Can this block provide power. Only wire currently seems to have this change based on its state. </summary>
        ///     
        public virtual bool canProvidePower( )
        {
            return false;
        }

        public virtual void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
        {
        }

        public virtual int isProvidingStrongPower(IBlockAccess p_149748_1_, int p_149748_2_, int p_149748_3_, int p_149748_4_, int p_149748_5_)
        {
            return 0;
        }

        ///    
        ///     <summary> * Sets the block's bounds for rendering it as an item </summary>
        ///     
        public virtual void setBlockBoundsForItemRender( )
        {
        }

        public virtual void harvestBlock(World p_149636_1_, EntityPlayer p_149636_2_, int p_149636_3_, int p_149636_4_, int p_149636_5_, int p_149636_6_)
        {
            p_149636_2_.addStat(StatList.mineBlockStatArray[getIdFromBlock(this)], 1);
            p_149636_2_.addExhaustion(0.025F);

            if (this.canSilkHarvest( ) && EnchantmentHelper.getSilkTouchModifier(p_149636_2_))
            {
                ItemStack var8 = this.createStackedBlock(p_149636_6_);

                if (var8 != null)
                {
                    this.dropBlockAsItem_do(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, var8);
                }
            }
            else
            {
                int var7 = EnchantmentHelper.getFortuneModifier(p_149636_2_);
                this.dropBlockAsItem(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, p_149636_6_, var7);
            }
        }

        protected internal virtual bool canSilkHarvest( )
        {
            return this.renderAsNormalBlock( ) && !this.isBlockContainer;
        }

        ///    
        ///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
        ///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
        ///     
        protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
        {
            int var2 = 0;
            Item var3 = Item.getItemFromBlock(this);

            if (var3 != null && var3.HasSubtypes)
            {
                var2 = p_149644_1_;
            }

            return new ItemStack(var3, 1, var2);
        }

        ///    
        ///     <summary> * Returns the usual quantity dropped by the block plus a bonus of 1 to 'i' (inclusive). </summary>
        ///     
        public virtual int quantityDroppedWithBonus(int p_149679_1_, Random p_149679_2_)
        {
            return this.quantityDropped(p_149679_2_);
        }

        ///    
        ///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
        ///     
        public virtual bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
        {
            return true;
        }

        ///    
        ///     <summary> * Called when the block is placed in the world. </summary>
        ///     
        public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
        {
        }

        ///    
        ///     <summary> * Called after a block is placed </summary>
        ///     
        public virtual void onPostBlockPlaced(World p_149714_1_, int p_149714_2_, int p_149714_3_, int p_149714_4_, int p_149714_5_)
        {
        }

        ///    
        ///     <summary> * Sets the mod-specific block name </summary>
        ///     
        

        ///    
        ///     <summary> * Gets the localized name of this block. Used for the statistics page. </summary>
        ///     
        public virtual string LocalizedName
        {
            get
            {
                return StatCollector.translateToLocal(this.UnlocalizedName + ".name");
            }
        }

        ///    
        ///     <summary> * Returns the unlocalized name of the block with "tile." appended to the front. </summary>
        ///     
        public virtual string UnlocalizedName
        {
            get
            {
                return "tile." + this.BlockName;
            }
        }

        public virtual bool onBlockEventReceived(World p_149696_1_, int p_149696_2_, int p_149696_3_, int p_149696_4_, int p_149696_5_, int p_149696_6_)
        {
            return false;
        }

        ///    
        ///     <summary> * Return the state of blocks statistics flags - if the block is counted for mined and placed. </summary>
        ///     
        public virtual bool EnableStats
        {
            get
            {
                return this.enableStats;
            }
        }

        protected internal virtual Block disableStats( )
        {
            this.enableStats = false;
            return this;
        }

        public virtual int MobilityFlag
        {
            get
            {
                return this.BlockMaterial.MaterialMobility;
            }
        }

        ///    
        ///     <summary> * Returns the default ambient occlusion value based on block opacity </summary>
        ///     
        public virtual float AmbientOcclusionLightValue
        {
            get
            {
                return isBlockNormalCube() ? 0.2F : 1.0F;
            }
        }

        ///    
        ///     <summary> * Block's chance to react to an entity falling on it. </summary>
        ///     
        public virtual void onFallenUpon(World p_149746_1_, int p_149746_2_, int p_149746_3_, int p_149746_4_, Entity p_149746_5_, float p_149746_6_)
        {
        }

        ///    
        ///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
        ///     
        public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
        {
            return Item.getItemFromBlock(this);
        }

        ///    
        ///     <summary> * Get the block's damage value (for use with pick block). </summary>
        ///     
        public virtual int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
        {
            return this.damageDropped(p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_, p_149643_4_));
        }

        public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
        {
            p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
        }

        ///    
        ///     <summary> * Returns the CreativeTab to display the given block on. </summary>
        ///     
        public virtual CreativeTabs CreativeTabToDisplayOn
        {
            get
            {
                return this.displayOnCreativeTab;
            }
        }

        ///    
        ///     <summary> * Called when the block is attempted to be harvested </summary>
        ///     
        public virtual void onBlockHarvested(World p_149681_1_, int p_149681_2_, int p_149681_3_, int p_149681_4_, int p_149681_5_, EntityPlayer p_149681_6_)
        {
        }

        public virtual void onBlockPreDestroy(World p_149725_1_, int p_149725_2_, int p_149725_3_, int p_149725_4_, int p_149725_5_)
        {
        }

        ///    
        ///     <summary> * currently only used by BlockCauldron to incrament meta-data during rain </summary>
        ///     
        public virtual void fillWithRain(World p_149639_1_, int p_149639_2_, int p_149639_3_, int p_149639_4_)
        {
        }

        ///    
        ///     <summary> * Returns true only if block is flowerPot </summary>
        ///     
        public virtual bool FlowerPot
		{
			get { return false; }
		}

        public virtual bool func_149698_L( )
        {
            return true;
        }

        ///    
        ///     <summary> * Return whether this block can drop from an explosion. </summary>
        ///     
        public virtual bool canDropFromExplosion(Explosion p_149659_1_)
        {
            return true;
        }

        public virtual bool func_149667_c(Block p_149667_1_)
        {
            return this == p_149667_1_;
        }

        public static bool isEqualTo(Block p_149680_0_, Block p_149680_1_)
        {
            return p_149680_0_ != null && p_149680_1_ != null ? (p_149680_0_ == p_149680_1_ ? true : p_149680_0_.func_149667_c(p_149680_1_)) : false;
        }

        public virtual bool hasComparatorInputOverride( )
        {
            return false;
        }

        public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
        {
            return 0;
        }

        protected internal virtual string TextureName
        {
            get
            {
                return this.textureName == null ? "MISSING_ICON_BLOCK_" + getIdFromBlock(this) + "_" + this.UnlocalizedName : this.textureName;
            }
        }
    }
}
