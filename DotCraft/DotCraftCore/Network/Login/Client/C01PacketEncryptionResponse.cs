namespace DotCraftCore.Network.Login.Client
{

	using SecretKey = javax.crypto.SecretKey;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerLoginServer = DotCraftCore.Network.Login.INetHandlerLoginServer;
	using CryptManager = DotCraftCore.Util.CryptManager;

	public class C01PacketEncryptionResponse : Packet
	{
		private sbyte[] field_149302_a = new sbyte[0];
		private sbyte[] field_149301_b = new sbyte[0];
		private const string __OBFID = "CL_00001380";

		public C01PacketEncryptionResponse()
		{
		}

		public C01PacketEncryptionResponse(SecretKey p_i45271_1_, PublicKey p_i45271_2_, sbyte[] p_i45271_3_)
		{
			this.field_149302_a = CryptManager.encryptData(p_i45271_2_, p_i45271_1_.Encoded);
			this.field_149301_b = CryptManager.encryptData(p_i45271_2_, p_i45271_3_);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149302_a = readBlob(p_148837_1_);
			this.field_149301_b = readBlob(p_148837_1_);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			writeBlob(p_148840_1_, this.field_149302_a);
			writeBlob(p_148840_1_, this.field_149301_b);
		}

		public virtual void processPacket(INetHandlerLoginServer p_148833_1_)
		{
			p_148833_1_.processEncryptionResponse(this);
		}

		public virtual SecretKey func_149300_a(PrivateKey p_149300_1_)
		{
			return CryptManager.decryptSharedKey(p_149300_1_, this.field_149302_a);
		}

		public virtual sbyte[] func_149299_b(PrivateKey p_149299_1_)
		{
			return p_149299_1_ == null ? this.field_149301_b : CryptManager.decryptData(p_149299_1_, this.field_149301_b);
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerLoginServer)p_148833_1_);
		}
	}

}