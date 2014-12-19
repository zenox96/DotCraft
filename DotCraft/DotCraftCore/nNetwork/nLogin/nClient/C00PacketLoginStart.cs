namespace DotCraftCore.nNetwork.nLogin.nClient
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerLoginServer = DotCraftCore.nNetwork.nLogin.INetHandlerLoginServer;

	public class C00PacketLoginStart : Packet
	{
		private GameProfile field_149305_a;
		

		public C00PacketLoginStart()
		{
		}

		public C00PacketLoginStart(GameProfile p_i45270_1_)
		{
			this.field_149305_a = p_i45270_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149305_a = new GameProfile((UUID)null, p_148837_1_.readStringFromBuffer(16));
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149305_a.Name);
		}

		public virtual void processPacket(INetHandlerLoginServer p_148833_1_)
		{
			p_148833_1_.processLoginStart(this);
		}

		public virtual GameProfile func_149304_c()
		{
			return this.field_149305_a;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerLoginServer)p_148833_1_);
		}
	}

}