using System.Collections;

namespace DotCraftCore.Network.Play.Server
{

	using AttributeModifier = DotCraftCore.entity.ai.attributes.AttributeModifier;
	using IAttributeInstance = DotCraftCore.entity.ai.attributes.IAttributeInstance;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S20PacketEntityProperties : Packet
	{
		private int field_149445_a;
		private readonly IList field_149444_b = new ArrayList();
		private const string __OBFID = "CL_00001341";

		public S20PacketEntityProperties()
		{
		}

		public S20PacketEntityProperties(int p_i45236_1_, ICollection p_i45236_2_)
		{
			this.field_149445_a = p_i45236_1_;
			IEnumerator var3 = p_i45236_2_.GetEnumerator();

			while (var3.MoveNext())
			{
				IAttributeInstance var4 = (IAttributeInstance)var3.Current;
				this.field_149444_b.Add(new S20PacketEntityProperties.Snapshot(var4.Attribute.AttributeUnlocalizedName, var4.BaseValue, var4.func_111122_c()));
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149445_a = p_148837_1_.readInt();
			int var2 = p_148837_1_.readInt();

			for (int var3 = 0; var3 < var2; ++var3)
			{
				string var4 = p_148837_1_.readStringFromBuffer(64);
				double var5 = p_148837_1_.readDouble();
				ArrayList var7 = new ArrayList();
				short var8 = p_148837_1_.readShort();

				for (int var9 = 0; var9 < var8; ++var9)
				{
					UUID var10 = new UUID(p_148837_1_.readLong(), p_148837_1_.readLong());
					var7.Add(new AttributeModifier(var10, "Unknown synced attribute modifier", p_148837_1_.readDouble(), p_148837_1_.readByte()));
				}

				this.field_149444_b.Add(new S20PacketEntityProperties.Snapshot(var4, var5, var7));
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149445_a);
			p_148840_1_.writeInt(this.field_149444_b.Count);
			IEnumerator var2 = this.field_149444_b.GetEnumerator();

			while (var2.MoveNext())
			{
				S20PacketEntityProperties.Snapshot var3 = (S20PacketEntityProperties.Snapshot)var2.Current;
				p_148840_1_.writeStringToBuffer(var3.func_151409_a());
				p_148840_1_.writeDouble(var3.func_151410_b());
				p_148840_1_.writeShort(var3.func_151408_c().size());
				IEnumerator var4 = var3.func_151408_c().GetEnumerator();

				while (var4.MoveNext())
				{
					AttributeModifier var5 = (AttributeModifier)var4.Current;
					p_148840_1_.writeLong(var5.ID.MostSignificantBits);
					p_148840_1_.writeLong(var5.ID.LeastSignificantBits);
					p_148840_1_.writeDouble(var5.Amount);
					p_148840_1_.writeByte(var5.Operation);
				}
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityProperties(this);
		}

		public virtual int func_149442_c()
		{
			return this.field_149445_a;
		}

		public virtual IList func_149441_d()
		{
			return this.field_149444_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}

		public class Snapshot
		{
			private readonly string field_151412_b;
			private readonly double field_151413_c;
			private readonly ICollection field_151411_d;
			private const string __OBFID = "CL_00001342";

			public Snapshot(string p_i45235_2_, double p_i45235_3_, ICollection p_i45235_5_)
			{
				this.field_151412_b = p_i45235_2_;
				this.field_151413_c = p_i45235_3_;
				this.field_151411_d = p_i45235_5_;
			}

			public virtual string func_151409_a()
			{
				return this.field_151412_b;
			}

			public virtual double func_151410_b()
			{
				return this.field_151413_c;
			}

			public virtual ICollection func_151408_c()
			{
				return this.field_151411_d;
			}
		}
	}

}