namespace DotCraftCore.Network.Login.Server
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerLoginClient = DotCraftCore.Network.Login.INetHandlerLoginClient;

	public class S02PacketLoginSuccess : Packet
	{
		private GameProfile field_149602_a;
		private const string __OBFID = "CL_00001375";

		public S02PacketLoginSuccess()
		{
		}

		public S02PacketLoginSuccess(GameProfile p_i45267_1_)
		{
			this.field_149602_a = p_i45267_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			string var2 = p_148837_1_.readStringFromBuffer(36);
			string var3 = p_148837_1_.readStringFromBuffer(16);
			UUID var4 = UUID.fromString(var2);
			this.field_149602_a = new GameProfile(var4, var3);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			UUID var2 = this.field_149602_a.Id;
			p_148840_1_.writeStringToBuffer(var2 == null ? "" : var2.ToString());
			p_148840_1_.writeStringToBuffer(this.field_149602_a.Name);
		}

		public virtual void processPacket(INetHandlerLoginClient p_148833_1_)
		{
			p_148833_1_.handleLoginSuccess(this);
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
			this.processPacket((INetHandlerLoginClient)p_148833_1_);
		}
	}

}