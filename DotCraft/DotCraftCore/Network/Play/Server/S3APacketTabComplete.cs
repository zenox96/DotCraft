namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using ArrayUtils = org.apache.commons.lang3.ArrayUtils;

	public class S3APacketTabComplete : Packet
	{
		private string[] field_149632_a;
		

		public S3APacketTabComplete()
		{
		}

		public S3APacketTabComplete(string[] p_i45178_1_)
		{
			this.field_149632_a = p_i45178_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149632_a = new string[p_148837_1_.readVarIntFromBuffer()];

			for (int var2 = 0; var2 < this.field_149632_a.Length; ++var2)
			{
				this.field_149632_a[var2] = p_148837_1_.readStringFromBuffer(32767);
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_149632_a.Length);
			string[] var2 = this.field_149632_a;
			int var3 = var2.Length;

			for (int var4 = 0; var4 < var3; ++var4)
			{
				string var5 = var2[var4];
				p_148840_1_.writeStringToBuffer(var5);
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleTabComplete(this);
		}

		public virtual string[] func_149630_c()
		{
			return this.field_149632_a;
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("candidates=\'{0}\'", new object[] {ArrayUtils.ToString(this.field_149632_a)});
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}