namespace DotCraftCore.Network.Play
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using C00PacketKeepAlive = DotCraftCore.Network.Play.Client.C00PacketKeepAlive;
	using C01PacketChatMessage = DotCraftCore.Network.Play.Client.C01PacketChatMessage;
	using C02PacketUseEntity = DotCraftCore.Network.Play.Client.C02PacketUseEntity;
	using C03PacketPlayer = DotCraftCore.Network.Play.Client.C03PacketPlayer;
	using C07PacketPlayerDigging = DotCraftCore.Network.Play.Client.C07PacketPlayerDigging;
	using C08PacketPlayerBlockPlacement = DotCraftCore.Network.Play.Client.C08PacketPlayerBlockPlacement;
	using C09PacketHeldItemChange = DotCraftCore.Network.Play.Client.C09PacketHeldItemChange;
	using C0APacketAnimation = DotCraftCore.Network.Play.Client.C0APacketAnimation;
	using C0BPacketEntityAction = DotCraftCore.Network.Play.Client.C0BPacketEntityAction;
	using C0CPacketInput = DotCraftCore.Network.Play.Client.C0CPacketInput;
	using C0DPacketCloseWindow = DotCraftCore.Network.Play.Client.C0DPacketCloseWindow;
	using C0EPacketClickWindow = DotCraftCore.Network.Play.Client.C0EPacketClickWindow;
	using C0FPacketConfirmTransaction = DotCraftCore.Network.Play.Client.C0FPacketConfirmTransaction;
	using C10PacketCreativeInventoryAction = DotCraftCore.Network.Play.Client.C10PacketCreativeInventoryAction;
	using C11PacketEnchantItem = DotCraftCore.Network.Play.Client.C11PacketEnchantItem;
	using C12PacketUpdateSign = DotCraftCore.Network.Play.Client.C12PacketUpdateSign;
	using C13PacketPlayerAbilities = DotCraftCore.Network.Play.Client.C13PacketPlayerAbilities;
	using C14PacketTabComplete = DotCraftCore.Network.Play.Client.C14PacketTabComplete;
	using C15PacketClientSettings = DotCraftCore.Network.Play.Client.C15PacketClientSettings;
	using C16PacketClientStatus = DotCraftCore.Network.Play.Client.C16PacketClientStatus;
	using C17PacketCustomPayload = DotCraftCore.Network.Play.Client.C17PacketCustomPayload;

	public interface INetHandlerPlayServer : INetHandler
	{
///    
///     <summary> * Processes the player swinging its held item </summary>
///     
		void processAnimation(C0APacketAnimation p_147350_1_);

///    
///     <summary> * Process chat messages (broadcast back to clients) and commands (executes) </summary>
///     
		void processChatMessage(C01PacketChatMessage p_147354_1_);

///    
///     <summary> * Retrieves possible tab completions for the requested command string and sends them to the client </summary>
///     
		void processTabComplete(C14PacketTabComplete p_147341_1_);

///    
///     <summary> * Processes the client status updates: respawn attempt from player, opening statistics or achievements, or
///     * acquiring 'open inventory' achievement </summary>
///     
		void processClientStatus(C16PacketClientStatus p_147342_1_);

///    
///     <summary> * Updates serverside copy of client settings: language, render distance, chat visibility, chat colours, difficulty,
///     * and whether to show the cape </summary>
///     
		void processClientSettings(C15PacketClientSettings p_147352_1_);

///    
///     <summary> * Received in response to the server requesting to confirm that the client-side open container matches the servers'
///     * after a mismatched container-slot manipulation. It will unlock the player's ability to manipulate the container
///     * contents </summary>
///     
		void processConfirmTransaction(C0FPacketConfirmTransaction p_147339_1_);

///    
///     <summary> * Enchants the item identified by the packet given some convoluted conditions (matching window, which
///     * should/shouldn't be in use?) </summary>
///     
		void processEnchantItem(C11PacketEnchantItem p_147338_1_);

///    
///     <summary> * Executes a container/inventory slot manipulation as indicated by the packet. Sends the serverside result if they
///     * didn't match the indicated result and prevents further manipulation by the player until he confirms that it has
///     * the same open container/inventory </summary>
///     
		void processClickWindow(C0EPacketClickWindow p_147351_1_);

///    
///     <summary> * Processes the client closing windows (container) </summary>
///     
		void processCloseWindow(C0DPacketCloseWindow p_147356_1_);

///    
///     <summary> * Synchronizes serverside and clientside book contents and signing </summary>
///     
		void processVanilla250Packet(C17PacketCustomPayload p_147349_1_);

///    
///     <summary> * Processes interactions ((un)leashing, opening command block GUI) and attacks on an entity with players currently
///     * equipped item </summary>
///     
		void processUseEntity(C02PacketUseEntity p_147340_1_);

///    
///     <summary> * Updates a players' ping statistics </summary>
///     
		void processKeepAlive(C00PacketKeepAlive p_147353_1_);

///    
///     <summary> * Processes clients perspective on player positioning and/or orientation </summary>
///     
		void processPlayer(C03PacketPlayer p_147347_1_);

///    
///     <summary> * Processes a player starting/stopping flying </summary>
///     
		void processPlayerAbilities(C13PacketPlayerAbilities p_147348_1_);

///    
///     <summary> * Processes the player initiating/stopping digging on a particular spot, as well as a player dropping items?. (0:
///     * initiated, 1: reinitiated, 2? , 3-4 drop item (respectively without or with player control), 5: stopped; x,y,z,
///     * side clicked on;) </summary>
///     
		void processPlayerDigging(C07PacketPlayerDigging p_147345_1_);

///    
///     <summary> * Processes a range of action-types: sneaking, sprinting, waking from sleep, opening the inventory or setting jump
///     * height of the horse the player is riding </summary>
///     
		void processEntityAction(C0BPacketEntityAction p_147357_1_);

///    
///     <summary> * Processes player movement input. Includes walking, strafing, jumping, sneaking; excludes riding and toggling
///     * flying/sprinting </summary>
///     
		void processInput(C0CPacketInput p_147358_1_);

///    
///     <summary> * Updates which quickbar slot is selected </summary>
///     
		void processHeldItemChange(C09PacketHeldItemChange p_147355_1_);

///    
///     <summary> * Update the server with an ItemStack in a slot. </summary>
///     
		void processCreativeInventoryAction(C10PacketCreativeInventoryAction p_147344_1_);

		void processUpdateSign(C12PacketUpdateSign p_147343_1_);

///    
///     <summary> * Processes block placement and block activation (anvil, furnace, etc.) </summary>
///     
		void processPlayerBlockPlacement(C08PacketPlayerBlockPlacement p_147346_1_);
	}

}