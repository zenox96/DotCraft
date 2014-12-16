using System;
using System.Collections;

namespace DotCraftCore.Item
{

	using HashMultiset = com.google.common.collect.HashMultiset;
	using Iterables = com.google.common.collect.Iterables;
	using Multisets = com.google.common.collect.Multisets;
	using Block = DotCraftCore.block.Block;
	using MapColor = DotCraftCore.block.material.MapColor;
	using Entity = DotCraftCore.entity.Entity;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using Packet = DotCraftCore.Network.Packet;
	using S34PacketMaps = DotCraftCore.Network.Play.Server.S34PacketMaps;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;
	using Chunk = DotCraftCore.World.Chunk.Chunk;
	using MapData = DotCraftCore.World.Storage.MapData;

	public class ItemMap : ItemMapBase
	{
		

		protected internal ItemMap()
		{
			this.HasSubtypes = true;
		}

		public static MapData func_150912_a(int p_150912_0_, World p_150912_1_)
		{
			string var2 = "map_" + p_150912_0_;
			MapData var3 = (MapData)p_150912_1_.loadItemData(typeof(MapData), var2);

			if (var3 == null)
			{
				var3 = new MapData(var2);
				p_150912_1_.setItemData(var2, var3);
			}

			return var3;
		}

		public virtual MapData getMapData(ItemStack p_77873_1_, World p_77873_2_)
		{
			string var3 = "map_" + p_77873_1_.ItemDamage;
			MapData var4 = (MapData)p_77873_2_.loadItemData(typeof(MapData), var3);

			if (var4 == null && !p_77873_2_.isClient)
			{
				p_77873_1_.ItemDamage = p_77873_2_.getUniqueDataId("map");
				var3 = "map_" + p_77873_1_.ItemDamage;
				var4 = new MapData(var3);
				var4.scale = 3;
				int var5 = 128 * (1 << var4.scale);
				var4.xCenter = Math.Round((float)p_77873_2_.WorldInfo.SpawnX / (float)var5) * var5;
				var4.zCenter = Math.Round((float)(p_77873_2_.WorldInfo.SpawnZ / var5)) * var5;
				var4.dimension = (sbyte)p_77873_2_.provider.dimensionId;
				var4.markDirty();
				p_77873_2_.setItemData(var3, var4);
			}

			return var4;
		}

		public virtual void updateMapData(World p_77872_1_, Entity p_77872_2_, MapData p_77872_3_)
		{
			if (p_77872_1_.provider.dimensionId == p_77872_3_.dimension && p_77872_2_ is EntityPlayer)
			{
				int var4 = 1 << p_77872_3_.scale;
				int var5 = p_77872_3_.xCenter;
				int var6 = p_77872_3_.zCenter;
				int var7 = MathHelper.floor_double(p_77872_2_.posX - (double)var5) / var4 + 64;
				int var8 = MathHelper.floor_double(p_77872_2_.posZ - (double)var6) / var4 + 64;
				int var9 = 128 / var4;

				if (p_77872_1_.provider.hasNoSky)
				{
					var9 /= 2;
				}

				MapData.MapInfo var10 = p_77872_3_.func_82568_a((EntityPlayer)p_77872_2_);
				++var10.field_82569_d;

				for (int var11 = var7 - var9 + 1; var11 < var7 + var9; ++var11)
				{
					if ((var11 & 15) == (var10.field_82569_d & 15))
					{
						int var12 = 255;
						int var13 = 0;
						double var14 = 0.0D;

						for (int var16 = var8 - var9 - 1; var16 < var8 + var9; ++var16)
						{
							if (var11 >= 0 && var16 >= -1 && var11 < 128 && var16 < 128)
							{
								int var17 = var11 - var7;
								int var18 = var16 - var8;
								bool var19 = var17 * var17 + var18 * var18 > (var9 - 2) * (var9 - 2);
								int var20 = (var5 / var4 + var11 - 64) * var4;
								int var21 = (var6 / var4 + var16 - 64) * var4;
								HashMultiset var22 = HashMultiset.create();
								Chunk var23 = p_77872_1_.getChunkFromBlockCoords(var20, var21);

								if (!var23.Empty)
								{
									int var24 = var20 & 15;
									int var25 = var21 & 15;
									int var26 = 0;
									double var27 = 0.0D;
									int var29;

									if (p_77872_1_.provider.hasNoSky)
									{
										var29 = var20 + var21 * 231871;
										var29 = var29 * var29 * 31287121 + var29 * 11;

										if ((var29 >> 20 & 1) == 0)
										{
											var22.add(Blocks.dirt.getMapColor(0), 10);
										}
										else
										{
											var22.add(Blocks.stone.getMapColor(0), 100);
										}

										var27 = 100.0D;
									}
									else
									{
										for (var29 = 0; var29 < var4; ++var29)
										{
											for (int var30 = 0; var30 < var4; ++var30)
											{
												int var31 = var23.getHeightValue(var29 + var24, var30 + var25) + 1;
												Block var32 = Blocks.air;
												int var33 = 0;

												if (var31 > 1)
												{
													do
													{
														--var31;
														var32 = var23.func_150810_a(var29 + var24, var31, var30 + var25);
														var33 = var23.getBlockMetadata(var29 + var24, var31, var30 + var25);
													}
													while (var32.getMapColor(var33) == MapColor.field_151660_b && var31 > 0);

													if (var31 > 0 && var32.Material.Liquid)
													{
														int var34 = var31 - 1;
														Block var35;

														do
														{
															var35 = var23.func_150810_a(var29 + var24, var34--, var30 + var25);
															++var26;
														}
														while (var34 > 0 && var35.Material.Liquid);
													}
												}

												var27 += (double)var31 / (double)(var4 * var4);
												var22.add(var32.getMapColor(var33));
											}
										}
									}

									var26 /= var4 * var4;
									double var36 = (var27 - var14) * 4.0D / (double)(var4 + 4) + ((double)(var11 + var16 & 1) - 0.5D) * 0.4D;
									sbyte var37 = 1;

									if (var36 > 0.6D)
									{
										var37 = 2;
									}

									if (var36 < -0.6D)
									{
										var37 = 0;
									}

									MapColor var38 = (MapColor)Iterables.getFirst(Multisets.copyHighestCountFirst(var22), MapColor.field_151660_b);

									if (var38 == MapColor.field_151662_n)
									{
										var36 = (double)var26 * 0.1D + (double)(var11 + var16 & 1) * 0.2D;
										var37 = 1;

										if (var36 < 0.5D)
										{
											var37 = 2;
										}

										if (var36 > 0.9D)
										{
											var37 = 0;
										}
									}

									var14 = var27;

									if (var16 >= 0 && var17 * var17 + var18 * var18 < var9 * var9 && (!var19 || (var11 + var16 & 1) != 0))
									{
										sbyte var39 = p_77872_3_.colors[var11 + var16 * 128];
										sbyte var40 = (sbyte)(var38.colorIndex * 4 + var37);

										if (var39 != var40)
										{
											if (var12 > var16)
											{
												var12 = var16;
											}

											if (var13 < var16)
											{
												var13 = var16;
											}

											p_77872_3_.colors[var11 + var16 * 128] = var40;
										}
									}
								}
							}
						}

						if (var12 <= var13)
						{
							p_77872_3_.setColumnDirty(var11, var12, var13);
						}
					}
				}
			}
		}

///    
///     <summary> * Called each tick as long the item is on a player inventory. Uses by maps to check if is on a player hand and
///     * update it's contents. </summary>
///     
		public virtual void onUpdate(ItemStack p_77663_1_, World p_77663_2_, Entity p_77663_3_, int p_77663_4_, bool p_77663_5_)
		{
			if (!p_77663_2_.isClient)
			{
				MapData var6 = this.getMapData(p_77663_1_, p_77663_2_);

				if (p_77663_3_ is EntityPlayer)
				{
					EntityPlayer var7 = (EntityPlayer)p_77663_3_;
					var6.updateVisiblePlayers(var7, p_77663_1_);
				}

				if (p_77663_5_)
				{
					this.updateMapData(p_77663_2_, p_77663_3_, var6);
				}
			}
		}

		public override Packet func_150911_c(ItemStack p_150911_1_, World p_150911_2_, EntityPlayer p_150911_3_)
		{
			sbyte[] var4 = this.getMapData(p_150911_1_, p_150911_2_).getUpdatePacketData(p_150911_1_, p_150911_2_, p_150911_3_);
			return var4 == null ? null : new S34PacketMaps(p_150911_1_.ItemDamage, var4);
		}

///    
///     <summary> * Called when item is crafted/smelted. Used only by maps so far. </summary>
///     
		public virtual void onCreated(ItemStack p_77622_1_, World p_77622_2_, EntityPlayer p_77622_3_)
		{
			if (p_77622_1_.hasTagCompound() && p_77622_1_.TagCompound.getBoolean("map_is_scaling"))
			{
				MapData var4 = Items.filled_map.getMapData(p_77622_1_, p_77622_2_);
				p_77622_1_.ItemDamage = p_77622_2_.getUniqueDataId("map");
				MapData var5 = new MapData("map_" + p_77622_1_.ItemDamage);
				var5.scale = (sbyte)(var4.scale + 1);

				if (var5.scale > 4)
				{
					var5.scale = 4;
				}

				var5.xCenter = var4.xCenter;
				var5.zCenter = var4.zCenter;
				var5.dimension = var4.dimension;
				var5.markDirty();
				p_77622_2_.setItemData("map_" + p_77622_1_.ItemDamage, var5);
			}
		}

///    
///     <summary> * allows items to add custom lines of information to the mouseover description </summary>
///     
		public virtual void addInformation(ItemStack p_77624_1_, EntityPlayer p_77624_2_, IList p_77624_3_, bool p_77624_4_)
		{
			MapData var5 = this.getMapData(p_77624_1_, p_77624_2_.worldObj);

			if (p_77624_4_)
			{
				if (var5 == null)
				{
					p_77624_3_.Add("Unknown map");
				}
				else
				{
					p_77624_3_.Add("Scaling at 1:" + (1 << var5.scale));
					p_77624_3_.Add("(Level " + var5.scale + "/" + 4 + ")");
				}
			}
		}
	}

}