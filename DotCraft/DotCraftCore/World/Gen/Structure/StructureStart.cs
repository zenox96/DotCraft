using System;
using System.Collections;

namespace DotCraftCore.World.Gen.Structure
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using World = DotCraftCore.World.World;

	public abstract class StructureStart
	{
	/// <summary> List of all StructureComponents that are part of this structure  </summary>
		protected internal LinkedList components = new LinkedList();
		protected internal StructureBoundingBox boundingBox;
		private int field_143024_c;
		private int field_143023_d;
		private const string __OBFID = "CL_00000513";

		public StructureStart()
		{
		}

		public StructureStart(int p_i43002_1_, int p_i43002_2_)
		{
			this.field_143024_c = p_i43002_1_;
			this.field_143023_d = p_i43002_2_;
		}

		public virtual StructureBoundingBox BoundingBox
		{
			get
			{
				return this.boundingBox;
			}
		}

		public virtual LinkedList Components
		{
			get
			{
				return this.components;
			}
		}

///    
///     <summary> * Keeps iterating Structure Pieces and spawning them until the checks tell it to stop </summary>
///     
		public virtual void generateStructure(World p_75068_1_, Random p_75068_2_, StructureBoundingBox p_75068_3_)
		{
			IEnumerator var4 = this.components.GetEnumerator();

			while (var4.MoveNext())
			{
				StructureComponent var5 = (StructureComponent)var4.Current;

				if (var5.BoundingBox.intersectsWith(p_75068_3_) && !var5.addComponentParts(p_75068_1_, p_75068_2_, p_75068_3_))
				{
					var4.remove();
				}
			}
		}

///    
///     <summary> * Calculates total bounding box based on components' bounding boxes and saves it to boundingBox </summary>
///     
		protected internal virtual void updateBoundingBox()
		{
			this.boundingBox = StructureBoundingBox.NewBoundingBox;
			IEnumerator var1 = this.components.GetEnumerator();

			while (var1.MoveNext())
			{
				StructureComponent var2 = (StructureComponent)var1.Current;
				this.boundingBox.expandTo(var2.BoundingBox);
			}
		}

		public virtual NBTTagCompound func_143021_a(int p_143021_1_, int p_143021_2_)
		{
			NBTTagCompound var3 = new NBTTagCompound();
			var3.setString("id", MapGenStructureIO.func_143033_a(this));
			var3.setInteger("ChunkX", p_143021_1_);
			var3.setInteger("ChunkZ", p_143021_2_);
			var3.setTag("BB", this.boundingBox.func_151535_h());
			NBTTagList var4 = new NBTTagList();
			IEnumerator var5 = this.components.GetEnumerator();

			while (var5.MoveNext())
			{
				StructureComponent var6 = (StructureComponent)var5.Current;
				var4.appendTag(var6.func_143010_b());
			}

			var3.setTag("Children", var4);
			this.func_143022_a(var3);
			return var3;
		}

		public virtual void func_143022_a(NBTTagCompound p_143022_1_)
		{
		}

		public virtual void func_143020_a(World p_143020_1_, NBTTagCompound p_143020_2_)
		{
			this.field_143024_c = p_143020_2_.getInteger("ChunkX");
			this.field_143023_d = p_143020_2_.getInteger("ChunkZ");

			if (p_143020_2_.hasKey("BB"))
			{
				this.boundingBox = new StructureBoundingBox(p_143020_2_.getIntArray("BB"));
			}

			NBTTagList var3 = p_143020_2_.getTagList("Children", 10);

			for (int var4 = 0; var4 < var3.tagCount(); ++var4)
			{
				this.components.AddLast(MapGenStructureIO.func_143032_b(var3.getCompoundTagAt(var4), p_143020_1_));
			}

			this.func_143017_b(p_143020_2_);
		}

		public virtual void func_143017_b(NBTTagCompound p_143017_1_)
		{
		}

///    
///     <summary> * offsets the structure Bounding Boxes up to a certain height, typically 63 - 10 </summary>
///     
		protected internal virtual void markAvailableHeight(World p_75067_1_, Random p_75067_2_, int p_75067_3_)
		{
			int var4 = 63 - p_75067_3_;
			int var5 = this.boundingBox.YSize + 1;

			if (var5 < var4)
			{
				var5 += p_75067_2_.Next(var4 - var5);
			}

			int var6 = var5 - this.boundingBox.maxY;
			this.boundingBox.offset(0, var6, 0);
			IEnumerator var7 = this.components.GetEnumerator();

			while (var7.MoveNext())
			{
				StructureComponent var8 = (StructureComponent)var7.Current;
				var8.BoundingBox.offset(0, var6, 0);
			}
		}

		protected internal virtual void setRandomHeight(World p_75070_1_, Random p_75070_2_, int p_75070_3_, int p_75070_4_)
		{
			int var5 = p_75070_4_ - p_75070_3_ + 1 - this.boundingBox.YSize;
			bool var6 = true;
			int var10;

			if (var5 > 1)
			{
				var10 = p_75070_3_ + p_75070_2_.Next(var5);
			}
			else
			{
				var10 = p_75070_3_;
			}

			int var7 = var10 - this.boundingBox.minY;
			this.boundingBox.offset(0, var7, 0);
			IEnumerator var8 = this.components.GetEnumerator();

			while (var8.MoveNext())
			{
				StructureComponent var9 = (StructureComponent)var8.Current;
				var9.BoundingBox.offset(0, var7, 0);
			}
		}

///    
///     <summary> * currently only defined for Villages, returns true if Village has more than 2 non-road components </summary>
///     
		public virtual bool isSizeableStructure()
		{
			get
			{
				return true;
			}
		}

		public virtual int func_143019_e()
		{
			return this.field_143024_c;
		}

		public virtual int func_143018_f()
		{
			return this.field_143023_d;
		}
	}

}