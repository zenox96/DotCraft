namespace DotCraftCore.Network.Login.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerLoginClient = DotCraftCore.Network.Login.INetHandlerLoginClient;
	using CryptManager = DotCraftCore.Util.CryptManager;

	public class S01PacketEncryptionRequest : Packet
	{
		private string field_149612_a;
		private PublicKey field_149610_b;
		private sbyte[] field_149611_c;
		

		public S01PacketEncryptionRequest()
		{
		}

		public S01PacketEncryptionRequest(string p_i45268_1_, PublicKey p_i45268_2_, sbyte[] p_i45268_3_)
		{
			this.field_149612_a = p_i45268_1_;
			this.field_149610_b = p_i45268_2_;
			this.field_149611_c = p_i45268_3_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149612_a = p_148837_1_.readStringFromBuffer(20);
			this.field_149610_b = CryptManager.decodePublicKey(readBlob(p_148837_1_));
			this.field_149611_c = readBlob(p_148837_1_);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149612_a);
			writeBlob(p_148840_1_, this.field_149610_b.Encoded);
			writeBlob(p_148840_1_, this.field_149611_c);
		}

		public virtual void processPacket(INetHandlerLoginClient p_148833_1_)
		{
			p_148833_1_.handleEncryptionRequest(this);
		}

		public virtual string func_149609_c()
		{
			return this.field_149612_a;
		}

		public virtual PublicKey func_149608_d()
		{
			return this.field_149610_b;
		}

		public virtual sbyte[] func_149607_e()
		{
			return this.field_149611_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerLoginClient)p_148833_1_);
		}
	}

}