using System;

namespace DotCraftCore.nNetwork
{

	using BiMap = com.google.common.collect.BiMap;
	using HashBiMap = com.google.common.collect.HashBiMap;
	using Iterables = com.google.common.collect.Iterables;
	using Maps = com.google.common.collect.Maps;
	using TIntObjectMap = gnu.trove.map.TIntObjectMap;
	using TIntObjectHashMap = gnu.trove.map.hash.TIntObjectHashMap;
	using C00Handshake = DotCraftCore.nNetwork.nHandshake.nClient.C00Handshake;
	using C00PacketLoginStart = DotCraftCore.nNetwork.nLogin.nClient.C00PacketLoginStart;
	using C01PacketEncryptionResponse = DotCraftCore.nNetwork.nLogin.nClient.C01PacketEncryptionResponse;
	using S00PacketDisconnect = DotCraftCore.nNetwork.nLogin.nServer.S00PacketDisconnect;
	using S01PacketEncryptionRequest = DotCraftCore.nNetwork.nLogin.nServer.S01PacketEncryptionRequest;
	using S02PacketLoginSuccess = DotCraftCore.nNetwork.nLogin.nServer.S02PacketLoginSuccess;
	using C00PacketKeepAlive = DotCraftCore.nNetwork.nPlay.nClient.C00PacketKeepAlive;
	using C01PacketChatMessage = DotCraftCore.nNetwork.nPlay.nClient.C01PacketChatMessage;
	using C02PacketUseEntity = DotCraftCore.nNetwork.nPlay.nClient.C02PacketUseEntity;
	using C03PacketPlayer = DotCraftCore.nNetwork.nPlay.nClient.C03PacketPlayer;
	using C07PacketPlayerDigging = DotCraftCore.nNetwork.nPlay.nClient.C07PacketPlayerDigging;
	using C08PacketPlayerBlockPlacement = DotCraftCore.nNetwork.nPlay.nClient.C08PacketPlayerBlockPlacement;
	using C09PacketHeldItemChange = DotCraftCore.nNetwork.nPlay.nClient.C09PacketHeldItemChange;
	using C0APacketAnimation = DotCraftCore.nNetwork.nPlay.nClient.C0APacketAnimation;
	using C0BPacketEntityAction = DotCraftCore.nNetwork.nPlay.nClient.C0BPacketEntityAction;
	using C0CPacketInput = DotCraftCore.nNetwork.nPlay.nClient.C0CPacketInput;
	using C0DPacketCloseWindow = DotCraftCore.nNetwork.nPlay.nClient.C0DPacketCloseWindow;
	using C0EPacketClickWindow = DotCraftCore.nNetwork.nPlay.nClient.C0EPacketClickWindow;
	using C0FPacketConfirmTransaction = DotCraftCore.nNetwork.nPlay.nClient.C0FPacketConfirmTransaction;
	using C10PacketCreativeInventoryAction = DotCraftCore.nNetwork.nPlay.nClient.C10PacketCreativeInventoryAction;
	using C11PacketEnchantItem = DotCraftCore.nNetwork.nPlay.nClient.C11PacketEnchantItem;
	using C12PacketUpdateSign = DotCraftCore.nNetwork.nPlay.nClient.C12PacketUpdateSign;
	using C13PacketPlayerAbilities = DotCraftCore.nNetwork.nPlay.nClient.C13PacketPlayerAbilities;
	using C14PacketTabComplete = DotCraftCore.nNetwork.nPlay.nClient.C14PacketTabComplete;
	using C15PacketClientSettings = DotCraftCore.nNetwork.nPlay.nClient.C15PacketClientSettings;
	using C16PacketClientStatus = DotCraftCore.nNetwork.nPlay.nClient.C16PacketClientStatus;
	using C17PacketCustomPayload = DotCraftCore.nNetwork.nPlay.nClient.C17PacketCustomPayload;
	using S00PacketKeepAlive = DotCraftCore.nNetwork.nPlay.nServer.S00PacketKeepAlive;
	using S01PacketJoinGame = DotCraftCore.nNetwork.nPlay.nServer.S01PacketJoinGame;
	using S02PacketChat = DotCraftCore.nNetwork.nPlay.nServer.S02PacketChat;
	using S03PacketTimeUpdate = DotCraftCore.nNetwork.nPlay.nServer.S03PacketTimeUpdate;
	using S04PacketEntityEquipment = DotCraftCore.nNetwork.nPlay.nServer.S04PacketEntityEquipment;
	using S05PacketSpawnPosition = DotCraftCore.nNetwork.nPlay.nServer.S05PacketSpawnPosition;
	using S06PacketUpdateHealth = DotCraftCore.nNetwork.nPlay.nServer.S06PacketUpdateHealth;
	using S07PacketRespawn = DotCraftCore.nNetwork.nPlay.nServer.S07PacketRespawn;
	using S08PacketPlayerPosLook = DotCraftCore.nNetwork.nPlay.nServer.S08PacketPlayerPosLook;
	using S09PacketHeldItemChange = DotCraftCore.nNetwork.nPlay.nServer.S09PacketHeldItemChange;
	using S0APacketUseBed = DotCraftCore.nNetwork.nPlay.nServer.S0APacketUseBed;
	using S0BPacketAnimation = DotCraftCore.nNetwork.nPlay.nServer.S0BPacketAnimation;
	using S0CPacketSpawnPlayer = DotCraftCore.nNetwork.nPlay.nServer.S0CPacketSpawnPlayer;
	using S0DPacketCollectItem = DotCraftCore.nNetwork.nPlay.nServer.S0DPacketCollectItem;
	using S0EPacketSpawnObject = DotCraftCore.nNetwork.nPlay.nServer.S0EPacketSpawnObject;
	using S0FPacketSpawnMob = DotCraftCore.nNetwork.nPlay.nServer.S0FPacketSpawnMob;
	using S10PacketSpawnPainting = DotCraftCore.nNetwork.nPlay.nServer.S10PacketSpawnPainting;
	using S11PacketSpawnExperienceOrb = DotCraftCore.nNetwork.nPlay.nServer.S11PacketSpawnExperienceOrb;
	using S12PacketEntityVelocity = DotCraftCore.nNetwork.nPlay.nServer.S12PacketEntityVelocity;
	using S13PacketDestroyEntities = DotCraftCore.nNetwork.nPlay.nServer.S13PacketDestroyEntities;
	using S14PacketEntity = DotCraftCore.nNetwork.nPlay.nServer.S14PacketEntity;
	using S18PacketEntityTeleport = DotCraftCore.nNetwork.nPlay.nServer.S18PacketEntityTeleport;
	using S19PacketEntityHeadLook = DotCraftCore.nNetwork.nPlay.nServer.S19PacketEntityHeadLook;
	using S19PacketEntityStatus = DotCraftCore.nNetwork.nPlay.nServer.S19PacketEntityStatus;
	using S1BPacketEntityAttach = DotCraftCore.nNetwork.nPlay.nServer.S1BPacketEntityAttach;
	using S1CPacketEntityMetadata = DotCraftCore.nNetwork.nPlay.nServer.S1CPacketEntityMetadata;
	using S1DPacketEntityEffect = DotCraftCore.nNetwork.nPlay.nServer.S1DPacketEntityEffect;
	using S1EPacketRemoveEntityEffect = DotCraftCore.nNetwork.nPlay.nServer.S1EPacketRemoveEntityEffect;
	using S1FPacketSetExperience = DotCraftCore.nNetwork.nPlay.nServer.S1FPacketSetExperience;
	using S20PacketEntityProperties = DotCraftCore.nNetwork.nPlay.nServer.S20PacketEntityProperties;
	using S21PacketChunkData = DotCraftCore.nNetwork.nPlay.nServer.S21PacketChunkData;
	using S22PacketMultiBlockChange = DotCraftCore.nNetwork.nPlay.nServer.S22PacketMultiBlockChange;
	using S23PacketBlockChange = DotCraftCore.nNetwork.nPlay.nServer.S23PacketBlockChange;
	using S24PacketBlockAction = DotCraftCore.nNetwork.nPlay.nServer.S24PacketBlockAction;
	using S25PacketBlockBreakAnim = DotCraftCore.nNetwork.nPlay.nServer.S25PacketBlockBreakAnim;
	using S26PacketMapChunkBulk = DotCraftCore.nNetwork.nPlay.nServer.S26PacketMapChunkBulk;
	using S27PacketExplosion = DotCraftCore.nNetwork.nPlay.nServer.S27PacketExplosion;
	using S28PacketEffect = DotCraftCore.nNetwork.nPlay.nServer.S28PacketEffect;
	using S29PacketSoundEffect = DotCraftCore.nNetwork.nPlay.nServer.S29PacketSoundEffect;
	using S2APacketParticles = DotCraftCore.nNetwork.nPlay.nServer.S2APacketParticles;
	using S2BPacketChangeGameState = DotCraftCore.nNetwork.nPlay.nServer.S2BPacketChangeGameState;
	using S2CPacketSpawnGlobalEntity = DotCraftCore.nNetwork.nPlay.nServer.S2CPacketSpawnGlobalEntity;
	using S2DPacketOpenWindow = DotCraftCore.nNetwork.nPlay.nServer.S2DPacketOpenWindow;
	using S2EPacketCloseWindow = DotCraftCore.nNetwork.nPlay.nServer.S2EPacketCloseWindow;
	using S2FPacketSetSlot = DotCraftCore.nNetwork.nPlay.nServer.S2FPacketSetSlot;
	using S30PacketWindowItems = DotCraftCore.nNetwork.nPlay.nServer.S30PacketWindowItems;
	using S31PacketWindowProperty = DotCraftCore.nNetwork.nPlay.nServer.S31PacketWindowProperty;
	using S32PacketConfirmTransaction = DotCraftCore.nNetwork.nPlay.nServer.S32PacketConfirmTransaction;
	using S33PacketUpdateSign = DotCraftCore.nNetwork.nPlay.nServer.S33PacketUpdateSign;
	using S34PacketMaps = DotCraftCore.nNetwork.nPlay.nServer.S34PacketMaps;
	using S35PacketUpdateTileEntity = DotCraftCore.nNetwork.nPlay.nServer.S35PacketUpdateTileEntity;
	using S36PacketSignEditorOpen = DotCraftCore.nNetwork.nPlay.nServer.S36PacketSignEditorOpen;
	using S37PacketStatistics = DotCraftCore.nNetwork.nPlay.nServer.S37PacketStatistics;
	using S38PacketPlayerListItem = DotCraftCore.nNetwork.nPlay.nServer.S38PacketPlayerListItem;
	using S39PacketPlayerAbilities = DotCraftCore.nNetwork.nPlay.nServer.S39PacketPlayerAbilities;
	using S3APacketTabComplete = DotCraftCore.nNetwork.nPlay.nServer.S3APacketTabComplete;
	using S3BPacketScoreboardObjective = DotCraftCore.nNetwork.nPlay.nServer.S3BPacketScoreboardObjective;
	using S3CPacketUpdateScore = DotCraftCore.nNetwork.nPlay.nServer.S3CPacketUpdateScore;
	using S3DPacketDisplayScoreboard = DotCraftCore.nNetwork.nPlay.nServer.S3DPacketDisplayScoreboard;
	using S3EPacketTeams = DotCraftCore.nNetwork.nPlay.nServer.S3EPacketTeams;
	using S3FPacketCustomPayload = DotCraftCore.nNetwork.nPlay.nServer.S3FPacketCustomPayload;
	using S40PacketDisconnect = DotCraftCore.nNetwork.nPlay.nServer.S40PacketDisconnect;
	using C00PacketServerQuery = DotCraftCore.nNetwork.nStatus.nClient.C00PacketServerQuery;
	using C01PacketPing = DotCraftCore.nNetwork.nStatus.nClient.C01PacketPing;
	using S00PacketServerInfo = DotCraftCore.nNetwork.nStatus.nServer.S00PacketServerInfo;
	using S01PacketPong = DotCraftCore.nNetwork.nStatus.nServer.S01PacketPong;
	using LogManager = org.apache.logging.log4j.LogManager;

	public enum EnumConnectionState
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private static final TIntObjectMap field_150764_e = new TIntObjectHashMap();
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private static final Map field_150761_f = Maps.newHashMap();
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final int field_150762_g;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final BiMap field_150769_h;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final BiMap field_150770_i;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		

//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		private EnumConnectionState(int p_i45152_3_)
//	{
//		this.field_150769_h = HashBiMap.create();
//		this.field_150770_i = HashBiMap.create();
//		this.field_150762_g = p_i45152_3_;
//	}








//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		public static EnumConnectionState func_150760_a(int p_150760_0_)
//	{
//		return (EnumConnectionState)field_150764_e.get(p_150760_0_);
//	}

//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		public static EnumConnectionState func_150752_a(Packet p_150752_0_)
//	{
//		return (EnumConnectionState)field_150761_f.get(p_150752_0_.getClass());
//	}

//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		EnumConnectionState(String ignore1, int ignore2, int p_i1197_3_, Object p_i1197_4_)
//	{
//		this(p_i1197_3_);
//	}

//JAVA TO VB & C# CONVERTER NOTE: This static initializer block is converted to a static constructor, but there is no current class:
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		static ImpliedClass()
//	{
//		EnumConnectionState[] var0 = values();
//		int var1 = var0.length;
//
//		for (int var2 = 0; var2 < var1; ++var2)
//		{
//			EnumConnectionState var3 = var0[var2];
//			field_150764_e.put(var3.func_150759_c(), var3);
//			Iterator var4 = Iterables.concat(var3.func_150755_b().values(), var3.func_150753_a().values()).iterator();
//
//			while (var4.hasNext())
//			{
//				Class var5 = (Class)var4.next();
//
//				if (field_150761_f.containsKey(var5) && field_150761_f.get(var5) != var3)
//				{
//					throw new Error("Packet " + var5 + " is already assigned to protocol " + field_150761_f.get(var5) + " - can\'t reassign to " + var3);
//				}
//
//				field_150761_f.put(var5, var3);
//			}
//		}
//	}
	}
	public static partial class EnumExtensionMethods
	{
			internal HANDSHAKING(this EnumConnectionState instance, "HANDSHAKING", 0, -1, null)
		{
			
			{
				instance.func_150751_a(0, typeof(C00Handshake));
			}
		}
		   internal , PLAY(this EnumConnectionState instance, "PLAY", 1, 0, null)
		{
			
			{
				instance.func_150756_b(0, typeof(S00PacketKeepAlive));
				instance.func_150756_b(1, typeof(S01PacketJoinGame));
				instance.func_150756_b(2, typeof(S02PacketChat));
				instance.func_150756_b(3, typeof(S03PacketTimeUpdate));
				instance.func_150756_b(4, typeof(S04PacketEntityEquipment));
				instance.func_150756_b(5, typeof(S05PacketSpawnPosition));
				instance.func_150756_b(6, typeof(S06PacketUpdateHealth));
				instance.func_150756_b(7, typeof(S07PacketRespawn));
				instance.func_150756_b(8, typeof(S08PacketPlayerPosLook));
				instance.func_150756_b(9, typeof(S09PacketHeldItemChange));
				instance.func_150756_b(10, typeof(S0APacketUseBed));
				instance.func_150756_b(11, typeof(S0BPacketAnimation));
				instance.func_150756_b(12, typeof(S0CPacketSpawnPlayer));
				instance.func_150756_b(13, typeof(S0DPacketCollectItem));
				instance.func_150756_b(14, typeof(S0EPacketSpawnObject));
				instance.func_150756_b(15, typeof(S0FPacketSpawnMob));
				instance.func_150756_b(16, typeof(S10PacketSpawnPainting));
				instance.func_150756_b(17, typeof(S11PacketSpawnExperienceOrb));
				instance.func_150756_b(18, typeof(S12PacketEntityVelocity));
				instance.func_150756_b(19, typeof(S13PacketDestroyEntities));
				instance.func_150756_b(20, typeof(S14PacketEntity));
				instance.func_150756_b(21, typeof(S14PacketEntity.S15PacketEntityRelMove));
				instance.func_150756_b(22, typeof(S14PacketEntity.S16PacketEntityLook));
				instance.func_150756_b(23, typeof(S14PacketEntity.S17PacketEntityLookMove));
				instance.func_150756_b(24, typeof(S18PacketEntityTeleport));
				instance.func_150756_b(25, typeof(S19PacketEntityHeadLook));
				instance.func_150756_b(26, typeof(S19PacketEntityStatus));
				instance.func_150756_b(27, typeof(S1BPacketEntityAttach));
				instance.func_150756_b(28, typeof(S1CPacketEntityMetadata));
				instance.func_150756_b(29, typeof(S1DPacketEntityEffect));
				instance.func_150756_b(30, typeof(S1EPacketRemoveEntityEffect));
				instance.func_150756_b(31, typeof(S1FPacketSetExperience));
				instance.func_150756_b(32, typeof(S20PacketEntityProperties));
				instance.func_150756_b(33, typeof(S21PacketChunkData));
				instance.func_150756_b(34, typeof(S22PacketMultiBlockChange));
				instance.func_150756_b(35, typeof(S23PacketBlockChange));
				instance.func_150756_b(36, typeof(S24PacketBlockAction));
				instance.func_150756_b(37, typeof(S25PacketBlockBreakAnim));
				instance.func_150756_b(38, typeof(S26PacketMapChunkBulk));
				instance.func_150756_b(39, typeof(S27PacketExplosion));
				instance.func_150756_b(40, typeof(S28PacketEffect));
				instance.func_150756_b(41, typeof(S29PacketSoundEffect));
				instance.func_150756_b(42, typeof(S2APacketParticles));
				instance.func_150756_b(43, typeof(S2BPacketChangeGameState));
				instance.func_150756_b(44, typeof(S2CPacketSpawnGlobalEntity));
				instance.func_150756_b(45, typeof(S2DPacketOpenWindow));
				instance.func_150756_b(46, typeof(S2EPacketCloseWindow));
				instance.func_150756_b(47, typeof(S2FPacketSetSlot));
				instance.func_150756_b(48, typeof(S30PacketWindowItems));
				instance.func_150756_b(49, typeof(S31PacketWindowProperty));
				instance.func_150756_b(50, typeof(S32PacketConfirmTransaction));
				instance.func_150756_b(51, typeof(S33PacketUpdateSign));
				instance.func_150756_b(52, typeof(S34PacketMaps));
				instance.func_150756_b(53, typeof(S35PacketUpdateTileEntity));
				instance.func_150756_b(54, typeof(S36PacketSignEditorOpen));
				instance.func_150756_b(55, typeof(S37PacketStatistics));
				instance.func_150756_b(56, typeof(S38PacketPlayerListItem));
				instance.func_150756_b(57, typeof(S39PacketPlayerAbilities));
				instance.func_150756_b(58, typeof(S3APacketTabComplete));
				instance.func_150756_b(59, typeof(S3BPacketScoreboardObjective));
				instance.func_150756_b(60, typeof(S3CPacketUpdateScore));
				instance.func_150756_b(61, typeof(S3DPacketDisplayScoreboard));
				instance.func_150756_b(62, typeof(S3EPacketTeams));
				instance.func_150756_b(63, typeof(S3FPacketCustomPayload));
				instance.func_150756_b(64, typeof(S40PacketDisconnect));
				instance.func_150751_a(0, typeof(C00PacketKeepAlive));
				instance.func_150751_a(1, typeof(C01PacketChatMessage));
				instance.func_150751_a(2, typeof(C02PacketUseEntity));
				instance.func_150751_a(3, typeof(C03PacketPlayer));
				instance.func_150751_a(4, typeof(C03PacketPlayer.C04PacketPlayerPosition));
				instance.func_150751_a(5, typeof(C03PacketPlayer.C05PacketPlayerLook));
				instance.func_150751_a(6, typeof(C03PacketPlayer.C06PacketPlayerPosLook));
				instance.func_150751_a(7, typeof(C07PacketPlayerDigging));
				instance.func_150751_a(8, typeof(C08PacketPlayerBlockPlacement));
				instance.func_150751_a(9, typeof(C09PacketHeldItemChange));
				instance.func_150751_a(10, typeof(C0APacketAnimation));
				instance.func_150751_a(11, typeof(C0BPacketEntityAction));
				instance.func_150751_a(12, typeof(C0CPacketInput));
				instance.func_150751_a(13, typeof(C0DPacketCloseWindow));
				instance.func_150751_a(14, typeof(C0EPacketClickWindow));
				instance.func_150751_a(15, typeof(C0FPacketConfirmTransaction));
				instance.func_150751_a(16, typeof(C10PacketCreativeInventoryAction));
				instance.func_150751_a(17, typeof(C11PacketEnchantItem));
				instance.func_150751_a(18, typeof(C12PacketUpdateSign));
				instance.func_150751_a(19, typeof(C13PacketPlayerAbilities));
				instance.func_150751_a(20, typeof(C14PacketTabComplete));
				instance.func_150751_a(21, typeof(C15PacketClientSettings));
				instance.func_150751_a(22, typeof(C16PacketClientStatus));
				instance.func_150751_a(23, typeof(C17PacketCustomPayload));
			}
		}
		   internal , STATUS(this EnumConnectionState instance, "STATUS", 2, 1, null)
		{
			
			{
				instance.func_150751_a(0, typeof(C00PacketServerQuery));
				instance.func_150756_b(0, typeof(S00PacketServerInfo));
				instance.func_150751_a(1, typeof(C01PacketPing));
				instance.func_150756_b(1, typeof(S01PacketPong));
			}
		}
		   internal , LOGIN(this EnumConnectionState instance, "LOGIN", 3, 2, null)
		{
			
			{
				instance.func_150756_b(0, typeof(S00PacketDisconnect));
				instance.func_150756_b(1, typeof(S01PacketEncryptionRequest));
				instance.func_150756_b(2, typeof(S02PacketLoginSuccess));
				instance.func_150751_a(0, typeof(C00PacketLoginStart));
				instance.func_150751_a(1, typeof(C01PacketEncryptionResponse));
			}
		}
			protected internal EnumConnectionState func_150751_a(this EnumConnectionState instance, int p_150751_1_, Type p_150751_2_)
		{
			string var3;

			if (instance.field_150769_h.containsKey(Convert.ToInt32(p_150751_1_)))
			{
				var3 = "Serverbound packet ID " + p_150751_1_ + " is already assigned to " + instance.field_150769_h.get(Convert.ToInt32(p_150751_1_)) + "; cannot re-assign to " + p_150751_2_;
				LogManager.Logger.fatal(var3);
				throw new System.ArgumentException(var3);
			}
			else if (instance.field_150769_h.containsValue(p_150751_2_))
			{
				var3 = "Serverbound packet " + p_150751_2_ + " is already assigned to ID " + instance.field_150769_h.inverse().get(p_150751_2_) + "; cannot re-assign to " + p_150751_1_;
				LogManager.Logger.fatal(var3);
				throw new System.ArgumentException(var3);
			}
			else
			{
				instance.field_150769_h.put(Convert.ToInt32(p_150751_1_), p_150751_2_);
				return instance;
			}
		}
			protected internal EnumConnectionState func_150756_b(this EnumConnectionState instance, int p_150756_1_, Type p_150756_2_)
		{
			string var3;

			if (instance.field_150770_i.containsKey(Convert.ToInt32(p_150756_1_)))
			{
				var3 = "Clientbound packet ID " + p_150756_1_ + " is already assigned to " + instance.field_150770_i.get(Convert.ToInt32(p_150756_1_)) + "; cannot re-assign to " + p_150756_2_;
				LogManager.Logger.fatal(var3);
				throw new System.ArgumentException(var3);
			}
			else if (instance.field_150770_i.containsValue(p_150756_2_))
			{
				var3 = "Clientbound packet " + p_150756_2_ + " is already assigned to ID " + instance.field_150770_i.inverse().get(p_150756_2_) + "; cannot re-assign to " + p_150756_1_;
				LogManager.Logger.fatal(var3);
				throw new System.ArgumentException(var3);
			}
			else
			{
				instance.field_150770_i.put(Convert.ToInt32(p_150756_1_), p_150756_2_);
				return instance;
			}
		}
			public BiMap func_150753_a(this EnumConnectionState instance)
		{
			return instance.field_150769_h;
		}
			public BiMap func_150755_b(this EnumConnectionState instance)
		{
			return instance.field_150770_i;
		}
			public BiMap func_150757_a(this EnumConnectionState instance, bool p_150757_1_)
		{
			return p_150757_1_ ? instance.func_150755_b() : instance.func_150753_a();
		}
			public BiMap func_150754_b(this EnumConnectionState instance, bool p_150754_1_)
		{
			return p_150754_1_ ? instance.func_150753_a() : instance.func_150755_b();
		}
			public int func_150759_c(this EnumConnectionState instance)
		{
			return instance.field_150762_g;
		}
	}

}