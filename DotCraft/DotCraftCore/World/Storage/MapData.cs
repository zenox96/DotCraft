using System;
using System.Collections;

namespace DotCraftCore.nWorld.nStorage
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using World = DotCraftCore.nWorld.World;
	using WorldSavedData = DotCraftCore.nWorld.WorldSavedData;

	public class MapData : WorldSavedData
	{
		public int xCenter;
		public int zCenter;
		public sbyte dimension;
		public sbyte scale;

	/// <summary> colours  </summary>
		public sbyte[] colors = new sbyte[16384];

///    
///     <summary> * Holds a reference to the MapInfo of the players who own a copy of the map </summary>
///     
		public IList playersArrayList = new ArrayList();

///    
///     <summary> * Holds a reference to the players who own a copy of the map and a reference to their MapInfo </summary>
///     
		private IDictionary playersHashMap = new Hashtable();
		public IDictionary playersVisibleOnMap = new LinkedHashMap();
		

		public MapData(string p_i2140_1_) : base(p_i2140_1_)
		{
		}

///    
///     <summary> * reads in data from the NBTTagCompound into this MapDataBase </summary>
///     
		public override void readFromNBT(NBTTagCompound p_76184_1_)
		{
			this.dimension = p_76184_1_.getByte("dimension");
			this.xCenter = p_76184_1_.getInteger("xCenter");
			this.zCenter = p_76184_1_.getInteger("zCenter");
			this.scale = p_76184_1_.getByte("scale");

			if (this.scale < 0)
			{
				this.scale = 0;
			}

			if (this.scale > 4)
			{
				this.scale = 4;
			}

			short var2 = p_76184_1_.getShort("width");
			short var3 = p_76184_1_.getShort("height");

			if (var2 == 128 && var3 == 128)
			{
				this.colors = p_76184_1_.getByteArray("colors");
			}
			else
			{
				sbyte[] var4 = p_76184_1_.getByteArray("colors");
				this.colors = new sbyte[16384];
				int var5 = (128 - var2) / 2;
				int var6 = (128 - var3) / 2;

				for (int var7 = 0; var7 < var3; ++var7)
				{
					int var8 = var7 + var6;

					if (var8 >= 0 || var8 < 128)
					{
						for (int var9 = 0; var9 < var2; ++var9)
						{
							int var10 = var9 + var5;

							if (var10 >= 0 || var10 < 128)
							{
								this.colors[var10 + var8 * 128] = var4[var9 + var7 * var2];
							}
						}
					}
				}
			}
		}

///    
///     <summary> * write data to NBTTagCompound from this MapDataBase, similar to Entities and TileEntities </summary>
///     
		public override void writeToNBT(NBTTagCompound p_76187_1_)
		{
			p_76187_1_.setByte("dimension", this.dimension);
			p_76187_1_.setInteger("xCenter", this.xCenter);
			p_76187_1_.setInteger("zCenter", this.zCenter);
			p_76187_1_.setByte("scale", this.scale);
			p_76187_1_.setShort("width", (short)128);
			p_76187_1_.setShort("height", (short)128);
			p_76187_1_.setByteArray("colors", this.colors);
		}

///    
///     <summary> * Adds the player passed to the list of visible players and checks to see which players are visible </summary>
///     
		public virtual void updateVisiblePlayers(EntityPlayer p_76191_1_, ItemStack p_76191_2_)
		{
			if (!this.playersHashMap.ContainsKey(p_76191_1_))
			{
				MapData.MapInfo var3 = new MapData.MapInfo(p_76191_1_);
				this.playersHashMap.Add(p_76191_1_, var3);
				this.playersArrayList.Add(var3);
			}

			if (!p_76191_1_.inventory.hasItemStack(p_76191_2_))
			{
				this.playersVisibleOnMap.Remove(p_76191_1_.CommandSenderName);
			}

			for (int var5 = 0; var5 < this.playersArrayList.Count; ++var5)
			{
				MapData.MapInfo var4 = (MapData.MapInfo)this.playersArrayList.get(var5);

				if (!var4.entityplayerObj.isDead && (var4.entityplayerObj.inventory.hasItemStack(p_76191_2_) || p_76191_2_.OnItemFrame))
				{
					if (!p_76191_2_.OnItemFrame && var4.entityplayerObj.dimension == this.dimension)
					{
						this.func_82567_a(0, var4.entityplayerObj.worldObj, var4.entityplayerObj.CommandSenderName, var4.entityplayerObj.posX, var4.entityplayerObj.posZ, (double)var4.entityplayerObj.rotationYaw);
					}
				}
				else
				{
					this.playersHashMap.Remove(var4.entityplayerObj);
					this.playersArrayList.Remove(var4);
				}
			}

			if (p_76191_2_.OnItemFrame)
			{
				this.func_82567_a(1, p_76191_1_.worldObj, "frame-" + p_76191_2_.ItemFrame.EntityId, (double)p_76191_2_.ItemFrame.field_146063_b, (double)p_76191_2_.ItemFrame.field_146062_d, (double)(p_76191_2_.ItemFrame.hangingDirection * 90));
			}
		}

		private void func_82567_a(int p_82567_1_, World p_82567_2_, string p_82567_3_, double p_82567_4_, double p_82567_6_, double p_82567_8_)
		{
			int var10 = 1 << this.scale;
			float var11 = (float)(p_82567_4_ - (double)this.xCenter) / (float)var10;
			float var12 = (float)(p_82567_6_ - (double)this.zCenter) / (float)var10;
			sbyte var13 = (sbyte)((int)((double)(var11 * 2.0F) + 0.5D));
			sbyte var14 = (sbyte)((int)((double)(var12 * 2.0F) + 0.5D));
			sbyte var16 = 63;
			sbyte var15;

			if (var11 >= (float)(-var16) && var12 >= (float)(-var16) && var11 <= (float)var16 && var12 <= (float)var16)
			{
				p_82567_8_ += p_82567_8_ < 0.0D ? -8.0D : 8.0D;
				var15 = (sbyte)((int)(p_82567_8_ * 16.0D / 360.0D));

				if (this.dimension < 0)
				{
					int var17 = (int)(p_82567_2_.WorldInfo.WorldTime / 10L);
					var15 = (sbyte)(var17 * var17 * 34187121 + var17 * 121 >> 15 & 15);
				}
			}
			else
			{
				if (Math.Abs(var11) >= 320.0F || Math.Abs(var12) >= 320.0F)
				{
					this.playersVisibleOnMap.Remove(p_82567_3_);
					return;
				}

				p_82567_1_ = 6;
				var15 = 0;

				if (var11 <= (float)(-var16))
				{
					var13 = (sbyte)((int)((double)(var16 * 2) + 2.5D));
				}

				if (var12 <= (float)(-var16))
				{
					var14 = (sbyte)((int)((double)(var16 * 2) + 2.5D));
				}

				if (var11 >= (float)var16)
				{
					var13 = (sbyte)(var16 * 2 + 1);
				}

				if (var12 >= (float)var16)
				{
					var14 = (sbyte)(var16 * 2 + 1);
				}
			}

			this.playersVisibleOnMap.Add(p_82567_3_, new MapData.MapCoord((sbyte)p_82567_1_, var13, var14, var15));
		}

///    
///     <summary> * Get byte array of packet data to send to players on map for updating map data </summary>
///     
		public virtual sbyte[] getUpdatePacketData(ItemStack p_76193_1_, World p_76193_2_, EntityPlayer p_76193_3_)
		{
			MapData.MapInfo var4 = (MapData.MapInfo)this.playersHashMap.get(p_76193_3_);
			return var4 == null ? null : var4.getPlayersOnMap(p_76193_1_);
		}

///    
///     <summary> * Marks a vertical range of pixels as being modified so they will be resent to clients. Parameters: X, lowest Y,
///     * highest Y </summary>
///     
		public virtual void setColumnDirty(int p_76194_1_, int p_76194_2_, int p_76194_3_)
		{
			base.markDirty();

			for (int var4 = 0; var4 < this.playersArrayList.Count; ++var4)
			{
				MapData.MapInfo var5 = (MapData.MapInfo)this.playersArrayList.get(var4);

				if (var5.field_76209_b[p_76194_1_] < 0 || var5.field_76209_b[p_76194_1_] > p_76194_2_)
				{
					var5.field_76209_b[p_76194_1_] = p_76194_2_;
				}

				if (var5.field_76210_c[p_76194_1_] < 0 || var5.field_76210_c[p_76194_1_] < p_76194_3_)
				{
					var5.field_76210_c[p_76194_1_] = p_76194_3_;
				}
			}
		}

///    
///     <summary> * Updates the client's map with information from other players in MP </summary>
///     
		public virtual void updateMPMapData(sbyte[] p_76192_1_)
		{
			int var2;

			if (p_76192_1_[0] == 0)
			{
				var2 = p_76192_1_[1] & 255;
				int var3 = p_76192_1_[2] & 255;

				for (int var4 = 0; var4 < p_76192_1_.Length - 3; ++var4)
				{
					this.colors[(var4 + var3) * 128 + var2] = p_76192_1_[var4 + 3];
				}

				this.markDirty();
			}
			else if (p_76192_1_[0] == 1)
			{
				this.playersVisibleOnMap.Clear();

				for (var2 = 0; var2 < (p_76192_1_.Length - 1) / 3; ++var2)
				{
					sbyte var7 = (sbyte)(p_76192_1_[var2 * 3 + 1] >> 4);
					sbyte var8 = p_76192_1_[var2 * 3 + 2];
					sbyte var5 = p_76192_1_[var2 * 3 + 3];
					sbyte var6 = (sbyte)(p_76192_1_[var2 * 3 + 1] & 15);
					this.playersVisibleOnMap.Add("icon-" + var2, new MapData.MapCoord(var7, var8, var5, var6));
				}
			}
			else if (p_76192_1_[0] == 2)
			{
				this.scale = p_76192_1_[1];
			}
		}

		public virtual MapData.MapInfo func_82568_a(EntityPlayer p_82568_1_)
		{
			MapData.MapInfo var2 = (MapData.MapInfo)this.playersHashMap.get(p_82568_1_);

			if (var2 == null)
			{
				var2 = new MapData.MapInfo(p_82568_1_);
				this.playersHashMap.Add(p_82568_1_, var2);
				this.playersArrayList.Add(var2);
			}

			return var2;
		}

		public class MapCoord
		{
			public sbyte iconSize;
			public sbyte centerX;
			public sbyte centerZ;
			public sbyte iconRotation;
			

			public MapCoord(sbyte p_i2139_2_, sbyte p_i2139_3_, sbyte p_i2139_4_, sbyte p_i2139_5_)
			{
				this.iconSize = p_i2139_2_;
				this.centerX = p_i2139_3_;
				this.centerZ = p_i2139_4_;
				this.iconRotation = p_i2139_5_;
			}
		}

		public class MapInfo
		{
			public readonly EntityPlayer entityplayerObj;
			public int[] field_76209_b = new int[128];
			public int[] field_76210_c = new int[128];
			private int currentRandomNumber;
			private int ticksUntilPlayerLocationMapUpdate;
			private sbyte[] lastPlayerLocationOnMap;
			public int field_82569_d;
			private bool field_82570_i;
			

			public MapInfo(EntityPlayer p_i2138_2_)
			{
				this.entityplayerObj = p_i2138_2_;

				for (int var3 = 0; var3 < this.field_76209_b.Length; ++var3)
				{
					this.field_76209_b[var3] = 0;
					this.field_76210_c[var3] = 127;
				}
			}

			public virtual sbyte[] getPlayersOnMap(ItemStack p_76204_1_)
			{
				sbyte[] var2;

				if (!this.field_82570_i)
				{
					var2 = new sbyte[] {(sbyte)2, MapData.scale};
					this.field_82570_i = true;
					return var2;
				}
				else
				{
					int var3;
					int var11;

					if (--this.ticksUntilPlayerLocationMapUpdate < 0)
					{
						this.ticksUntilPlayerLocationMapUpdate = 4;
						var2 = new sbyte[MapData.playersVisibleOnMap.size() * 3 + 1];
						var2[0] = 1;
						var3 = 0;

						for (IEnumerator var4 = MapData.playersVisibleOnMap.values().GetEnumerator(); var4.MoveNext(); ++var3)
						{
							MapData.MapCoord var5 = (MapData.MapCoord)var4.Current;
							var2[var3 * 3 + 1] = (sbyte)(var5.iconSize << 4 | var5.iconRotation & 15);
							var2[var3 * 3 + 2] = var5.centerX;
							var2[var3 * 3 + 3] = var5.centerZ;
						}

						bool var9 = !p_76204_1_.OnItemFrame;

						if (this.lastPlayerLocationOnMap != null && this.lastPlayerLocationOnMap.Length == var2.Length)
						{
							for (var11 = 0; var11 < var2.Length; ++var11)
							{
								if (var2[var11] != this.lastPlayerLocationOnMap[var11])
								{
									var9 = false;
									break;
								}
							}
						}
						else
						{
							var9 = false;
						}

						if (!var9)
						{
							this.lastPlayerLocationOnMap = var2;
							return var2;
						}
					}

					for (int var8 = 0; var8 < 1; ++var8)
					{
						var3 = this.currentRandomNumber++ * 11 % 128;

						if (this.field_76209_b[var3] >= 0)
						{
							int var10 = this.field_76210_c[var3] - this.field_76209_b[var3] + 1;
							var11 = this.field_76209_b[var3];
							sbyte[] var6 = new sbyte[var10 + 3];
							var6[0] = 0;
							var6[1] = (sbyte)var3;
							var6[2] = (sbyte)var11;

							for (int var7 = 0; var7 < var6.Length - 3; ++var7)
							{
								var6[var7 + 3] = MapData.colors[(var7 + var11) * 128 + var3];
							}

							this.field_76210_c[var3] = -1;
							this.field_76209_b[var3] = -1;
							return var6;
						}
					}

					return null;
				}
			}
		}
	}

}