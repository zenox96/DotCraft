using System;
using System.Text;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S13PacketDestroyEntities : Packet
	{
		private int[] field_149100_a;
		private const string __OBFID = "CL_00001320";

		public S13PacketDestroyEntities()
		{
		}

		public S13PacketDestroyEntities(params int[] p_i45211_1_)
		{
			this.field_149100_a = p_i45211_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149100_a = new int[p_148837_1_.readByte()];

			for (int var2 = 0; var2 < this.field_149100_a.Length; ++var2)
			{
				this.field_149100_a[var2] = p_148837_1_.readInt();
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149100_a.Length);

			for (int var2 = 0; var2 < this.field_149100_a.Length; ++var2)
			{
				p_148840_1_.writeInt(this.field_149100_a[var2]);
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleDestroyEntities(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			StringBuilder var1 = new StringBuilder();

			for (int var2 = 0; var2 < this.field_149100_a.Length; ++var2)
			{
				if (var2 > 0)
				{
					var1.Append(", ");
				}

				var1.Append(this.field_149100_a[var2]);
			}

			return string.Format("entities={0:D}[{1}]", new object[] {Convert.ToInt32(this.field_149100_a.Length), var1});
		}

		public virtual int[] func_149098_c()
		{
			return this.field_149100_a;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}