namespace DotCraftCore.Network.Play
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using S00PacketKeepAlive = DotCraftCore.Network.Play.Server.S00PacketKeepAlive;
	using S01PacketJoinGame = DotCraftCore.Network.Play.Server.S01PacketJoinGame;
	using S02PacketChat = DotCraftCore.Network.Play.Server.S02PacketChat;
	using S03PacketTimeUpdate = DotCraftCore.Network.Play.Server.S03PacketTimeUpdate;
	using S04PacketEntityEquipment = DotCraftCore.Network.Play.Server.S04PacketEntityEquipment;
	using S05PacketSpawnPosition = DotCraftCore.Network.Play.Server.S05PacketSpawnPosition;
	using S06PacketUpdateHealth = DotCraftCore.Network.Play.Server.S06PacketUpdateHealth;
	using S07PacketRespawn = DotCraftCore.Network.Play.Server.S07PacketRespawn;
	using S08PacketPlayerPosLook = DotCraftCore.Network.Play.Server.S08PacketPlayerPosLook;
	using S09PacketHeldItemChange = DotCraftCore.Network.Play.Server.S09PacketHeldItemChange;
	using S0APacketUseBed = DotCraftCore.Network.Play.Server.S0APacketUseBed;
	using S0BPacketAnimation = DotCraftCore.Network.Play.Server.S0BPacketAnimation;
	using S0CPacketSpawnPlayer = DotCraftCore.Network.Play.Server.S0CPacketSpawnPlayer;
	using S0DPacketCollectItem = DotCraftCore.Network.Play.Server.S0DPacketCollectItem;
	using S0EPacketSpawnObject = DotCraftCore.Network.Play.Server.S0EPacketSpawnObject;
	using S0FPacketSpawnMob = DotCraftCore.Network.Play.Server.S0FPacketSpawnMob;
	using S10PacketSpawnPainting = DotCraftCore.Network.Play.Server.S10PacketSpawnPainting;
	using S11PacketSpawnExperienceOrb = DotCraftCore.Network.Play.Server.S11PacketSpawnExperienceOrb;
	using S12PacketEntityVelocity = DotCraftCore.Network.Play.Server.S12PacketEntityVelocity;
	using S13PacketDestroyEntities = DotCraftCore.Network.Play.Server.S13PacketDestroyEntities;
	using S14PacketEntity = DotCraftCore.Network.Play.Server.S14PacketEntity;
	using S18PacketEntityTeleport = DotCraftCore.Network.Play.Server.S18PacketEntityTeleport;
	using S19PacketEntityHeadLook = DotCraftCore.Network.Play.Server.S19PacketEntityHeadLook;
	using S19PacketEntityStatus = DotCraftCore.Network.Play.Server.S19PacketEntityStatus;
	using S1BPacketEntityAttach = DotCraftCore.Network.Play.Server.S1BPacketEntityAttach;
	using S1CPacketEntityMetadata = DotCraftCore.Network.Play.Server.S1CPacketEntityMetadata;
	using S1DPacketEntityEffect = DotCraftCore.Network.Play.Server.S1DPacketEntityEffect;
	using S1EPacketRemoveEntityEffect = DotCraftCore.Network.Play.Server.S1EPacketRemoveEntityEffect;
	using S1FPacketSetExperience = DotCraftCore.Network.Play.Server.S1FPacketSetExperience;
	using S20PacketEntityProperties = DotCraftCore.Network.Play.Server.S20PacketEntityProperties;
	using S21PacketChunkData = DotCraftCore.Network.Play.Server.S21PacketChunkData;
	using S22PacketMultiBlockChange = DotCraftCore.Network.Play.Server.S22PacketMultiBlockChange;
	using S23PacketBlockChange = DotCraftCore.Network.Play.Server.S23PacketBlockChange;
	using S24PacketBlockAction = DotCraftCore.Network.Play.Server.S24PacketBlockAction;
	using S25PacketBlockBreakAnim = DotCraftCore.Network.Play.Server.S25PacketBlockBreakAnim;
	using S26PacketMapChunkBulk = DotCraftCore.Network.Play.Server.S26PacketMapChunkBulk;
	using S27PacketExplosion = DotCraftCore.Network.Play.Server.S27PacketExplosion;
	using S28PacketEffect = DotCraftCore.Network.Play.Server.S28PacketEffect;
	using S29PacketSoundEffect = DotCraftCore.Network.Play.Server.S29PacketSoundEffect;
	using S2APacketParticles = DotCraftCore.Network.Play.Server.S2APacketParticles;
	using S2BPacketChangeGameState = DotCraftCore.Network.Play.Server.S2BPacketChangeGameState;
	using S2CPacketSpawnGlobalEntity = DotCraftCore.Network.Play.Server.S2CPacketSpawnGlobalEntity;
	using S2DPacketOpenWindow = DotCraftCore.Network.Play.Server.S2DPacketOpenWindow;
	using S2EPacketCloseWindow = DotCraftCore.Network.Play.Server.S2EPacketCloseWindow;
	using S2FPacketSetSlot = DotCraftCore.Network.Play.Server.S2FPacketSetSlot;
	using S30PacketWindowItems = DotCraftCore.Network.Play.Server.S30PacketWindowItems;
	using S31PacketWindowProperty = DotCraftCore.Network.Play.Server.S31PacketWindowProperty;
	using S32PacketConfirmTransaction = DotCraftCore.Network.Play.Server.S32PacketConfirmTransaction;
	using S33PacketUpdateSign = DotCraftCore.Network.Play.Server.S33PacketUpdateSign;
	using S34PacketMaps = DotCraftCore.Network.Play.Server.S34PacketMaps;
	using S35PacketUpdateTileEntity = DotCraftCore.Network.Play.Server.S35PacketUpdateTileEntity;
	using S36PacketSignEditorOpen = DotCraftCore.Network.Play.Server.S36PacketSignEditorOpen;
	using S37PacketStatistics = DotCraftCore.Network.Play.Server.S37PacketStatistics;
	using S38PacketPlayerListItem = DotCraftCore.Network.Play.Server.S38PacketPlayerListItem;
	using S39PacketPlayerAbilities = DotCraftCore.Network.Play.Server.S39PacketPlayerAbilities;
	using S3APacketTabComplete = DotCraftCore.Network.Play.Server.S3APacketTabComplete;
	using S3BPacketScoreboardObjective = DotCraftCore.Network.Play.Server.S3BPacketScoreboardObjective;
	using S3CPacketUpdateScore = DotCraftCore.Network.Play.Server.S3CPacketUpdateScore;
	using S3DPacketDisplayScoreboard = DotCraftCore.Network.Play.Server.S3DPacketDisplayScoreboard;
	using S3EPacketTeams = DotCraftCore.Network.Play.Server.S3EPacketTeams;
	using S3FPacketCustomPayload = DotCraftCore.Network.Play.Server.S3FPacketCustomPayload;
	using S40PacketDisconnect = DotCraftCore.Network.Play.Server.S40PacketDisconnect;

	public interface INetHandlerPlayClient : INetHandler
	{
///    
///     <summary> * Spawns an instance of the objecttype indicated by the packet and sets its position and momentum </summary>
///     
		void handleSpawnObject(S0EPacketSpawnObject p_147235_1_);

///    
///     <summary> * Spawns an experience orb and sets its value (amount of XP) </summary>
///     
		void handleSpawnExperienceOrb(S11PacketSpawnExperienceOrb p_147286_1_);

///    
///     <summary> * Handles globally visible entities. Used in vanilla for lightning bolts </summary>
///     
		void handleSpawnGlobalEntity(S2CPacketSpawnGlobalEntity p_147292_1_);

///    
///     <summary> * Spawns the mob entity at the specified location, with the specified rotation, momentum and type. Updates the
///     * entities Datawatchers with the entity metadata specified in the packet </summary>
///     
		void handleSpawnMob(S0FPacketSpawnMob p_147281_1_);

///    
///     <summary> * May create a scoreboard objective, remove an objective from the scoreboard or update an objectives' displayname </summary>
///     
		void handleScoreboardObjective(S3BPacketScoreboardObjective p_147291_1_);

///    
///     <summary> * Handles the spawning of a painting object </summary>
///     
		void handleSpawnPainting(S10PacketSpawnPainting p_147288_1_);

///    
///     <summary> * Handles the creation of a nearby player entity, sets the position and held item </summary>
///     
		void handleSpawnPlayer(S0CPacketSpawnPlayer p_147237_1_);

///    
///     <summary> * Renders a specified animation: Waking up a player, a living entity swinging its currently held item, being hurt
///     * or receiving a critical hit by normal or magical means </summary>
///     
		void handleAnimation(S0BPacketAnimation p_147279_1_);

///    
///     <summary> * Updates the players statistics or achievements </summary>
///     
		void handleStatistics(S37PacketStatistics p_147293_1_);

///    
///     <summary> * Updates all registered IWorldAccess instances with destroyBlockInWorldPartially </summary>
///     
		void handleBlockBreakAnim(S25PacketBlockBreakAnim p_147294_1_);

///    
///     <summary> * Creates a sign in the specified location if it didn't exist and opens the GUI to edit its text </summary>
///     
		void handleSignEditorOpen(S36PacketSignEditorOpen p_147268_1_);

///    
///     <summary> * Updates the NBTTagCompound metadata of instances of the following entitytypes: Mob spawners, command blocks,
///     * beacons, skulls, flowerpot </summary>
///     
		void handleUpdateTileEntity(S35PacketUpdateTileEntity p_147273_1_);

///    
///     <summary> * Triggers Block.onBlockEventReceived, which is implemented in BlockPistonBase for extension/retraction, BlockNote
///     * for setting the instrument (including audiovisual feedback) and in BlockContainer to set the number of players
///     * accessing a (Ender)Chest </summary>
///     
		void handleBlockAction(S24PacketBlockAction p_147261_1_);

///    
///     <summary> * Updates the block and metadata and generates a blockupdate (and notify the clients) </summary>
///     
		void handleBlockChange(S23PacketBlockChange p_147234_1_);

///    
///     <summary> * Prints a chatmessage in the chat GUI </summary>
///     
		void handleChat(S02PacketChat p_147251_1_);

///    
///     <summary> * Displays the available command-completion options the server knows of </summary>
///     
		void handleTabComplete(S3APacketTabComplete p_147274_1_);

///    
///     <summary> * Received from the servers PlayerManager if between 1 and 64 blocks in a chunk are changed. If only one block
///     * requires an update, the server sends S23PacketBlockChange and if 64 or more blocks are changed, the server sends
///     * S21PacketChunkData </summary>
///     
		void handleMultiBlockChange(S22PacketMultiBlockChange p_147287_1_);

///    
///     <summary> * Updates the worlds MapStorage with the specified MapData for the specified map-identifier and invokes a
///     * MapItemRenderer for it </summary>
///     
		void handleMaps(S34PacketMaps p_147264_1_);

///    
///     <summary> * Verifies that the server and client are synchronized with respect to the inventory/container opened by the player
///     * and confirms if it is the case. </summary>
///     
		void handleConfirmTransaction(S32PacketConfirmTransaction p_147239_1_);

///    
///     <summary> * Resets the ItemStack held in hand and closes the window that is opened </summary>
///     
		void handleCloseWindow(S2EPacketCloseWindow p_147276_1_);

///    
///     <summary> * Handles the placement of a specified ItemStack in a specified container/inventory slot </summary>
///     
		void handleWindowItems(S30PacketWindowItems p_147241_1_);

///    
///     <summary> * Displays a GUI by ID. In order starting from id 0: Chest, Workbench, Furnace, Dispenser, Enchanting table,
///     * Brewing stand, Villager merchant, Beacon, Anvil, Hopper, Dropper, Horse </summary>
///     
		void handleOpenWindow(S2DPacketOpenWindow p_147265_1_);

///    
///     <summary> * Sets the progressbar of the opened window to the specified value </summary>
///     
		void handleWindowProperty(S31PacketWindowProperty p_147245_1_);

///    
///     <summary> * Handles pickin up an ItemStack or dropping one in your inventory or an open (non-creative) container </summary>
///     
		void handleSetSlot(S2FPacketSetSlot p_147266_1_);

///    
///     <summary> * Handles packets that have room for a channel specification. Vanilla implemented channels are "MC|TrList" to
///     * acquire a MerchantRecipeList trades for a villager merchant, "MC|Brand" which sets the server brand? on the
///     * player instance and finally "MC|RPack" which the server uses to communicate the identifier of the default server
///     * resourcepack for the client to load. </summary>
///     
		void handleCustomPayload(S3FPacketCustomPayload p_147240_1_);

///    
///     <summary> * Closes the network channel </summary>
///     
		void handleDisconnect(S40PacketDisconnect p_147253_1_);

///    
///     <summary> * Retrieves the player identified by the packet, puts him to sleep if possible (and flags whether all players are
///     * asleep) </summary>
///     
		void handleUseBed(S0APacketUseBed p_147278_1_);

///    
///     <summary> * Invokes the entities' handleUpdateHealth method which is implemented in LivingBase (hurt/death),
///     * MinecartMobSpawner (spawn delay), FireworkRocket & MinecartTNT (explosion), IronGolem (throwing,...), Witch
///     * (spawn particles), Zombie (villager transformation), Animal (breeding mode particles), Horse (breeding/smoke
///     * particles), Sheep (...), Tameable (...), Villager (particles for breeding mode, angry and happy), Wolf (...) </summary>
///     
		void handleEntityStatus(S19PacketEntityStatus p_147236_1_);

		void handleEntityAttach(S1BPacketEntityAttach p_147243_1_);

///    
///     <summary> * Initiates a new explosion (sound, particles, drop spawn) for the affected blocks indicated by the packet. </summary>
///     
		void handleExplosion(S27PacketExplosion p_147283_1_);

		void handleChangeGameState(S2BPacketChangeGameState p_147252_1_);

		void handleKeepAlive(S00PacketKeepAlive p_147272_1_);

///    
///     <summary> * Updates the specified chunk with the supplied data, marks it for re-rendering and lighting recalculation </summary>
///     
		void handleChunkData(S21PacketChunkData p_147263_1_);

		void handleMapChunkBulk(S26PacketMapChunkBulk p_147269_1_);

		void handleEffect(S28PacketEffect p_147277_1_);

///    
///     <summary> * Registers some server properties (gametype,hardcore-mode,terraintype,difficulty,player limit), creates a new
///     * WorldClient and sets the player initial dimension </summary>
///     
		void handleJoinGame(S01PacketJoinGame p_147282_1_);

///    
///     <summary> * Updates the specified entity's position by the specified relative moment and absolute rotation. Note that
///     * subclassing of the packet allows for the specification of a subset of this data (e.g. only rel. position, abs.
///     * rotation or both). </summary>
///     
		void handleEntityMovement(S14PacketEntity p_147259_1_);

///    
///     <summary> * Handles changes in player positioning and rotation such as when travelling to a new dimension, (re)spawning,
///     * mounting horses etc. Seems to immediately reply to the server with the clients post-processing perspective on the
///     * player positioning </summary>
///     
		void handlePlayerPosLook(S08PacketPlayerPosLook p_147258_1_);

///    
///     <summary> * Spawns a specified number of particles at the specified location with a randomized displacement according to
///     * specified bounds </summary>
///     
		void handleParticles(S2APacketParticles p_147289_1_);

		void handlePlayerAbilities(S39PacketPlayerAbilities p_147270_1_);

		void handlePlayerListItem(S38PacketPlayerListItem p_147256_1_);

///    
///     <summary> * Locally eliminates the entities. Invoked by the server when the items are in fact destroyed, or the player is no
///     * longer registered as required to monitor them. The latter  happens when distance between the player and item
///     * increases beyond a certain treshold (typically the viewing distance) </summary>
///     
		void handleDestroyEntities(S13PacketDestroyEntities p_147238_1_);

		void handleRemoveEntityEffect(S1EPacketRemoveEntityEffect p_147262_1_);

		void handleRespawn(S07PacketRespawn p_147280_1_);

///    
///     <summary> * Updates the direction in which the specified entity is looking, normally this head rotation is independent of the
///     * rotation of the entity itself </summary>
///     
		void handleEntityHeadLook(S19PacketEntityHeadLook p_147267_1_);

///    
///     <summary> * Updates which hotbar slot of the player is currently selected </summary>
///     
		void handleHeldItemChange(S09PacketHeldItemChange p_147257_1_);

///    
///     <summary> * Removes or sets the ScoreObjective to be displayed at a particular scoreboard position (list, sidebar, below
///     * name) </summary>
///     
		void handleDisplayScoreboard(S3DPacketDisplayScoreboard p_147254_1_);

///    
///     <summary> * Invoked when the server registers new proximate objects in your watchlist or when objects in your watchlist have
///     * changed -> Registers any changes locally </summary>
///     
		void handleEntityMetadata(S1CPacketEntityMetadata p_147284_1_);

///    
///     <summary> * Sets the velocity of the specified entity to the specified value </summary>
///     
		void handleEntityVelocity(S12PacketEntityVelocity p_147244_1_);

		void handleEntityEquipment(S04PacketEntityEquipment p_147242_1_);

		void handleSetExperience(S1FPacketSetExperience p_147295_1_);

		void handleUpdateHealth(S06PacketUpdateHealth p_147249_1_);

///    
///     <summary> * Updates a team managed by the scoreboard: Create/Remove the team registration, Register/Remove the player-team-
///     * memberships, Set team displayname/prefix/suffix and/or whether friendly fire is enabled </summary>
///     
		void handleTeams(S3EPacketTeams p_147247_1_);

///    
///     <summary> * Either updates the score with a specified value or removes the score for an objective </summary>
///     
		void handleUpdateScore(S3CPacketUpdateScore p_147250_1_);

		void handleSpawnPosition(S05PacketSpawnPosition p_147271_1_);

		void handleTimeUpdate(S03PacketTimeUpdate p_147285_1_);

///    
///     <summary> * Updates a specified sign with the specified text lines </summary>
///     
		void handleUpdateSign(S33PacketUpdateSign p_147248_1_);

		void handleSoundEffect(S29PacketSoundEffect p_147255_1_);

		void handleCollectItem(S0DPacketCollectItem p_147246_1_);

///    
///     <summary> * Updates an entity's position and rotation as specified by the packet </summary>
///     
		void handleEntityTeleport(S18PacketEntityTeleport p_147275_1_);

///    
///     <summary> * Updates en entity's attributes and their respective modifiers, which are used for speed bonusses (player
///     * sprinting, animals fleeing, baby speed), weapon/tool attackDamage, hostiles followRange randomization, zombie
///     * maxHealth and knockback resistance as well as reinforcement spawning chance. </summary>
///     
		void handleEntityProperties(S20PacketEntityProperties p_147290_1_);

		void handleEntityEffect(S1DPacketEntityEffect p_147260_1_);
	}

}