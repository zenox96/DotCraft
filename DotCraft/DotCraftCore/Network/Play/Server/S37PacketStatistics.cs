using System;
using System.Collections;

namespace DotCraftCore.Network.Play.Server
{

	using Maps = com.google.common.collect.Maps;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using StatBase = DotCraftCore.Stats.StatBase;
	using StatList = DotCraftCore.Stats.StatList;

	public class S37PacketStatistics : Packet
	{
		private IDictionary field_148976_a;
		private const string __OBFID = "CL_00001283";

		public S37PacketStatistics()
		{
		}

		public S37PacketStatistics(IDictionary p_i45173_1_)
		{
			this.field_148976_a = p_i45173_1_;
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleStatistics(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			int var2 = p_148837_1_.readVarIntFromBuffer();
			this.field_148976_a = Maps.newHashMap();

			for (int var3 = 0; var3 < var2; ++var3)
			{
				StatBase var4 = StatList.func_151177_a(p_148837_1_.readStringFromBuffer(32767));
				int var5 = p_148837_1_.readVarIntFromBuffer();

				if (var4 != null)
				{
					this.field_148976_a.Add(var4, Convert.ToInt32(var5));
				}
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_148976_a.Count);
			IEnumerator var2 = this.field_148976_a.GetEnumerator();

			while (var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;
				p_148840_1_.writeStringToBuffer(((StatBase)var3.Key).statId);
				p_148840_1_.writeVarIntToBuffer((int)((int?)var3.Value));
			}
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("count={0:D}", new object[] {Convert.ToInt32(this.field_148976_a.Count)});
		}

		public virtual IDictionary func_148974_c()
		{
			return this.field_148976_a;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}