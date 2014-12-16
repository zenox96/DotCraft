using System;
using System.Collections;

namespace DotCraftCore.Network.Play.Server
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using Property = com.mojang.authlib.properties.Property;
	using DataWatcher = DotCraftCore.entity.DataWatcher;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class S0CPacketSpawnPlayer : Packet
	{
		private int field_148957_a;
		private GameProfile field_148955_b;
		private int field_148956_c;
		private int field_148953_d;
		private int field_148954_e;
		private sbyte field_148951_f;
		private sbyte field_148952_g;
		private int field_148959_h;
		private DataWatcher field_148960_i;
		private IList field_148958_j;
		

		public S0CPacketSpawnPlayer()
		{
		}

		public S0CPacketSpawnPlayer(EntityPlayer p_i45171_1_)
		{
			this.field_148957_a = p_i45171_1_.EntityId;
			this.field_148955_b = p_i45171_1_.GameProfile;
			this.field_148956_c = MathHelper.floor_double(p_i45171_1_.posX * 32.0D);
			this.field_148953_d = MathHelper.floor_double(p_i45171_1_.posY * 32.0D);
			this.field_148954_e = MathHelper.floor_double(p_i45171_1_.posZ * 32.0D);
			this.field_148951_f = (sbyte)((int)(p_i45171_1_.rotationYaw * 256.0F / 360.0F));
			this.field_148952_g = (sbyte)((int)(p_i45171_1_.rotationPitch * 256.0F / 360.0F));
			ItemStack var2 = p_i45171_1_.inventory.CurrentItem;
			this.field_148959_h = var2 == null ? 0 : Item.getIdFromItem(var2.Item);
			this.field_148960_i = p_i45171_1_.DataWatcher;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148957_a = p_148837_1_.readVarIntFromBuffer();
			UUID var2 = UUID.fromString(p_148837_1_.readStringFromBuffer(36));
			this.field_148955_b = new GameProfile(var2, p_148837_1_.readStringFromBuffer(16));
			int var3 = p_148837_1_.readVarIntFromBuffer();

			for (int var4 = 0; var4 < var3; ++var4)
			{
				string var5 = p_148837_1_.readStringFromBuffer(32767);
				string var6 = p_148837_1_.readStringFromBuffer(32767);
				string var7 = p_148837_1_.readStringFromBuffer(32767);
				this.field_148955_b.Properties.put(var5, new Property(var5, var6, var7));
			}

			this.field_148956_c = p_148837_1_.readInt();
			this.field_148953_d = p_148837_1_.readInt();
			this.field_148954_e = p_148837_1_.readInt();
			this.field_148951_f = p_148837_1_.readByte();
			this.field_148952_g = p_148837_1_.readByte();
			this.field_148959_h = p_148837_1_.readShort();
			this.field_148958_j = DataWatcher.readWatchedListFromPacketBuffer(p_148837_1_);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_148957_a);
			UUID var2 = this.field_148955_b.Id;
			p_148840_1_.writeStringToBuffer(var2 == null ? "" : var2.ToString());
			p_148840_1_.writeStringToBuffer(this.field_148955_b.Name);
			p_148840_1_.writeVarIntToBuffer(this.field_148955_b.Properties.size());
			IEnumerator var3 = this.field_148955_b.Properties.values().GetEnumerator();

			while (var3.MoveNext())
			{
				Property var4 = (Property)var3.Current;
				p_148840_1_.writeStringToBuffer(var4.Name);
				p_148840_1_.writeStringToBuffer(var4.Value);
				p_148840_1_.writeStringToBuffer(var4.Signature);
			}

			p_148840_1_.writeInt(this.field_148956_c);
			p_148840_1_.writeInt(this.field_148953_d);
			p_148840_1_.writeInt(this.field_148954_e);
			p_148840_1_.writeByte(this.field_148951_f);
			p_148840_1_.writeByte(this.field_148952_g);
			p_148840_1_.writeShort(this.field_148959_h);
			this.field_148960_i.func_151509_a(p_148840_1_);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSpawnPlayer(this);
		}

		public virtual IList func_148944_c()
		{
			if (this.field_148958_j == null)
			{
				this.field_148958_j = this.field_148960_i.AllWatched;
			}

			return this.field_148958_j;
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, gameProfile=\'{1}\', x={2:F2}, y={3:F2}, z={4:F2}, carried={5:D}", new object[] {Convert.ToInt32(this.field_148957_a), this.field_148955_b, Convert.ToSingle((float)this.field_148956_c / 32.0F), Convert.ToSingle((float)this.field_148953_d / 32.0F), Convert.ToSingle((float)this.field_148954_e / 32.0F), Convert.ToInt32(this.field_148959_h)});
		}

		public virtual int func_148943_d()
		{
			return this.field_148957_a;
		}

		public virtual GameProfile func_148948_e()
		{
			return this.field_148955_b;
		}

		public virtual int func_148942_f()
		{
			return this.field_148956_c;
		}

		public virtual int func_148949_g()
		{
			return this.field_148953_d;
		}

		public virtual int func_148946_h()
		{
			return this.field_148954_e;
		}

		public virtual sbyte func_148941_i()
		{
			return this.field_148951_f;
		}

		public virtual sbyte func_148945_j()
		{
			return this.field_148952_g;
		}

		public virtual int func_148947_k()
		{
			return this.field_148959_h;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}