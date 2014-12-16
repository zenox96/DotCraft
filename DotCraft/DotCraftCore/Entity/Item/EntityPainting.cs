using System.Collections;

namespace DotCraftCore.Entity.Item
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityHanging = DotCraftCore.Entity.EntityHanging;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using World = DotCraftCore.World.World;

	public class EntityPainting : EntityHanging
	{
		public EntityPainting.EnumArt art;
		

		public EntityPainting(World p_i1599_1_) : base(p_i1599_1_)
		{
		}

		public EntityPainting(World p_i1600_1_, int p_i1600_2_, int p_i1600_3_, int p_i1600_4_, int p_i1600_5_) : base(p_i1600_1_, p_i1600_2_, p_i1600_3_, p_i1600_4_, p_i1600_5_)
		{
			ArrayList var6 = new ArrayList();
			EntityPainting.EnumArt[] var7 = EntityPainting.EnumArt.values();
			int var8 = var7.Length;

			for (int var9 = 0; var9 < var8; ++var9)
			{
				EntityPainting.EnumArt var10 = var7[var9];
				this.art = var10;
				this.Direction = p_i1600_5_;

				if (this.onValidSurface())
				{
					var6.Add(var10);
				}
			}

			if (!var6.Count == 0)
			{
				this.art = (EntityPainting.EnumArt)var6[this.rand.Next(var6.Count)];
			}

			this.Direction = p_i1600_5_;
		}

		public EntityPainting(World p_i1601_1_, int p_i1601_2_, int p_i1601_3_, int p_i1601_4_, int p_i1601_5_, string p_i1601_6_) : this(p_i1601_1_, p_i1601_2_, p_i1601_3_, p_i1601_4_, p_i1601_5_)
		{
			EntityPainting.EnumArt[] var7 = EntityPainting.EnumArt.values();
			int var8 = var7.Length;

			for (int var9 = 0; var9 < var8; ++var9)
			{
				EntityPainting.EnumArt var10 = var7[var9];

				if (var10.title.Equals(p_i1601_6_))
				{
					this.art = var10;
					break;
				}
			}

			this.Direction = p_i1601_5_;
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setString("Motive", this.art.title);
			base.writeEntityToNBT(p_70014_1_);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			string var2 = p_70037_1_.getString("Motive");
			EntityPainting.EnumArt[] var3 = EntityPainting.EnumArt.values();
			int var4 = var3.Length;

			for (int var5 = 0; var5 < var4; ++var5)
			{
				EntityPainting.EnumArt var6 = var3[var5];

				if (var6.title.Equals(var2))
				{
					this.art = var6;
				}
			}

			if (this.art == null)
			{
				this.art = EntityPainting.EnumArt.Kebab;
			}

			base.readEntityFromNBT(p_70037_1_);
		}

		public override int WidthPixels
		{
			get
			{
				return this.art.sizeX;
			}
		}

		public override int HeightPixels
		{
			get
			{
				return this.art.sizeY;
			}
		}

///    
///     <summary> * Called when this entity is broken. Entity parameter may be null. </summary>
///     
		public override void onBroken(Entity p_110128_1_)
		{
			if (p_110128_1_ is EntityPlayer)
			{
				EntityPlayer var2 = (EntityPlayer)p_110128_1_;

				if (var2.capabilities.isCreativeMode)
				{
					return;
				}
			}

			this.entityDropItem(new ItemStack(Items.painting), 0.0F);
		}

		public enum EnumArt
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Kebab("Kebab", 0, "Kebab", 16, 16, 0, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Aztec("Aztec", 1, "Aztec", 16, 16, 16, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Alban("Alban", 2, "Alban", 16, 16, 32, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Aztec2("Aztec2", 3, "Aztec2", 16, 16, 48, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Bomb("Bomb", 4, "Bomb", 16, 16, 64, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Plant("Plant", 5, "Plant", 16, 16, 80, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Wasteland("Wasteland", 6, "Wasteland", 16, 16, 96, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Pool("Pool", 7, "Pool", 32, 16, 0, 32),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Courbet("Courbet", 8, "Courbet", 32, 16, 32, 32),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Sea("Sea", 9, "Sea", 32, 16, 64, 32),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Sunset("Sunset", 10, "Sunset", 32, 16, 96, 32),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Creebet("Creebet", 11, "Creebet", 32, 16, 128, 32),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Wanderer("Wanderer", 12, "Wanderer", 16, 32, 0, 64),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Graham("Graham", 13, "Graham", 16, 32, 16, 64),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Match("Match", 14, "Match", 32, 32, 0, 128),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Bust("Bust", 15, "Bust", 32, 32, 32, 128),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Stage("Stage", 16, "Stage", 32, 32, 64, 128),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Void("Void", 17, "Void", 32, 32, 96, 128),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SkullAndRoses("SkullAndRoses", 18, "SkullAndRoses", 32, 32, 128, 128),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Wither("Wither", 19, "Wither", 32, 32, 160, 128),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Fighters("Fighters", 20, "Fighters", 64, 32, 0, 96),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Pointer("Pointer", 21, "Pointer", 64, 64, 0, 192),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Pigscene("Pigscene", 22, "Pigscene", 64, 64, 64, 192),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			BurningSkull("BurningSkull", 23, "BurningSkull", 64, 64, 128, 192),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			Skeleton("Skeleton", 24, "Skeleton", 64, 48, 192, 64),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			DonkeyKong("DonkeyKong", 25, "DonkeyKong", 64, 48, 192, 112);
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			public static final int maxArtTitleLength = "SkullAndRoses".length();
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			public final String title;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			public final int sizeX;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			public final int sizeY;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			public final int offsetX;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			public final int offsetY;

			@private static final EntityPainting.EnumArt[] $VALUES = new EntityPainting.EnumArt[]{Kebab, Aztec, Alban, Aztec2, Bomb, Plant, Wasteland, Pool, Courbet, Sea, Sunset, Creebet, Wanderer, Graham, Match, Bust, Stage, Void, SkullAndRoses, Wither, Fighters, Pointer, Pigscene, BurningSkull, Skeleton, DonkeyKong
		}
			

			private EnumArt(string p_i1598_1_, int p_i1598_2_, string p_i1598_3_, int p_i1598_4_, int p_i1598_5_, int p_i1598_6_, int p_i1598_7_)
			{
				this.title = p_i1598_3_;
				this.sizeX = p_i1598_4_;
				this.sizeY = p_i1598_5_;
				this.offsetX = p_i1598_6_;
				this.offsetY = p_i1598_7_;
			}
		}
	}

}