namespace DotCraftCore.nNetwork.nLogin.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerLoginClient = DotCraftCore.nNetwork.nLogin.INetHandlerLoginClient;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;

	public class S00PacketDisconnect : Packet
	{
		private IChatComponent field_149605_a;
		

		public S00PacketDisconnect()
		{
		}

		public S00PacketDisconnect(IChatComponent p_i45269_1_)
		{
			this.field_149605_a = p_i45269_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149605_a = IChatComponent.Serializer.func_150699_a(p_148837_1_.readStringFromBuffer(32767));
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(IChatComponent.Serializer.func_150696_a(this.field_149605_a));
		}

		public virtual void processPacket(INetHandlerLoginClient p_148833_1_)
		{
			p_148833_1_.handleDisconnect(this);
		}

///    
///     <summary> * If true, the network manager will process the packet immediately when received, otherwise it will queue it for
///     * processing. Currently true for: Disconnect, LoginSuccess, KeepAlive, ServerQuery/Info, Ping/Pong </summary>
///     
		public override bool hasPriority()
		{
			return true;
		}

		public virtual IChatComponent func_149603_c()
		{
			return this.field_149605_a;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerLoginClient)p_148833_1_);
		}
	}

}