namespace DotCraftCore.Network.Status.Server
{

	using Gson = com.google.gson.Gson;
	using GsonBuilder = com.google.gson.GsonBuilder;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using ServerStatusResponse = DotCraftCore.Network.ServerStatusResponse;
	using INetHandlerStatusClient = DotCraftCore.Network.Status.INetHandlerStatusClient;
	using ChatStyle = DotCraftCore.Util.ChatStyle;
	using EnumTypeAdapterFactory = DotCraftCore.Util.EnumTypeAdapterFactory;
	using IChatComponent = DotCraftCore.Util.IChatComponent;

	public class S00PacketServerInfo : Packet
	{
		private static readonly Gson field_149297_a = (new GsonBuilder()).registerTypeAdapter(typeof(ServerStatusResponse.MinecraftProtocolVersionIdentifier), new ServerStatusResponse.MinecraftProtocolVersionIdentifier.Serializer()).registerTypeAdapter(typeof(ServerStatusResponse.PlayerCountData), new ServerStatusResponse.PlayerCountData.Serializer()).registerTypeAdapter(typeof(ServerStatusResponse), new ServerStatusResponse.Serializer()).registerTypeHierarchyAdapter(typeof(IChatComponent), new IChatComponent.Serializer()).registerTypeHierarchyAdapter(typeof(ChatStyle), new ChatStyle.Serializer()).registerTypeAdapterFactory(new EnumTypeAdapterFactory()).create();
		private ServerStatusResponse field_149296_b;
		private const string __OBFID = "CL_00001384";

		public S00PacketServerInfo()
		{
		}

		public S00PacketServerInfo(ServerStatusResponse p_i45273_1_)
		{
			this.field_149296_b = p_i45273_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149296_b = (ServerStatusResponse)field_149297_a.fromJson(p_148837_1_.readStringFromBuffer(32767), typeof(ServerStatusResponse));
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(field_149297_a.toJson(this.field_149296_b));
		}

		public virtual void processPacket(INetHandlerStatusClient p_148833_1_)
		{
			p_148833_1_.handleServerInfo(this);
		}

		public virtual ServerStatusResponse func_149294_c()
		{
			return this.field_149296_b;
		}

///    
///     <summary> * If true, the network manager will process the packet immediately when received, otherwise it will queue it for
///     * processing. Currently true for: Disconnect, LoginSuccess, KeepAlive, ServerQuery/Info, Ping/Pong </summary>
///     
		public override bool hasPriority()
		{
			return true;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerStatusClient)p_148833_1_);
		}
	}

}