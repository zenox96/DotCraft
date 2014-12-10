namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using IChatComponent = DotCraftCore.Util.IChatComponent;

	public class S02PacketChat : Packet
	{
		private IChatComponent field_148919_a;
		private bool field_148918_b;
		private const string __OBFID = "CL_00001289";

		public S02PacketChat()
		{
			this.field_148918_b = true;
		}

		public S02PacketChat(IChatComponent p_i45179_1_) : this(p_i45179_1_, true)
		{
		}

		public S02PacketChat(IChatComponent p_i45180_1_, bool p_i45180_2_)
		{
			this.field_148918_b = true;
			this.field_148919_a = p_i45180_1_;
			this.field_148918_b = p_i45180_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148919_a = IChatComponent.Serializer.func_150699_a(p_148837_1_.readStringFromBuffer(32767));
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(IChatComponent.Serializer.func_150696_a(this.field_148919_a));
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleChat(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("message=\'{0}\'", new object[] {this.field_148919_a});
		}

		public virtual IChatComponent func_148915_c()
		{
			return this.field_148919_a;
		}

		public virtual bool func_148916_d()
		{
			return this.field_148918_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}