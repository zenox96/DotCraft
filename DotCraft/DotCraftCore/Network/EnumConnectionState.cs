using System;

namespace DotCraftCore.Network
{

	using BiMap = com.google.common.collect.BiMap;
	using HashBiMap = com.google.common.collect.HashBiMap;
	using Iterables = com.google.common.collect.Iterables;
	using Maps = com.google.common.collect.Maps;
	using TIntObjectMap = gnu.trove.map.TIntObjectMap;
	using TIntObjectHashMap = gnu.trove.map.hash.TIntObjectHashMap;
	using C00Handshake = DotCraftCore.Network.Handshake.Client.C00Handshake;
	using C00PacketLoginStart = DotCraftCore.Network.Login.Client.C00PacketLoginStart;
	using C01PacketEncryptionResponse = DotCraftCore.Network.Login.Client.C01PacketEncryptionResponse;
	using S00PacketDisconnect = DotCraftCore.Network.Login.Server.S00PacketDisconnect;
	using S01PacketEncryptionRequest = DotCraftCore.Network.Login.Server.S01PacketEncryptionRequest;
	using S02PacketLoginSuccess = DotCraftCore.Network.Login.Server.S02PacketLoginSuccess;
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
	using C00PacketServerQuery = DotCraftCore.Network.Status.Client.C00PacketServerQuery;
	using C01PacketPing = DotCraftCore.Network.Status.Client.C01PacketPing;
	using S00PacketServerInfo = DotCraftCore.Network.Status.Server.S00PacketServerInfo;
	using S01PacketPong = DotCraftCore.Network.Status.Server.S01PacketPong;
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