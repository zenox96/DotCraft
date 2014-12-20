using DotCraftCore.nDispenser;
using DotCraftCore.nTileEntity;
using DotCraftCore.nWorld;
namespace DotCraftCore.nBlock
{
	public class BlockSourceImpl : IBlockSource
	{
		private readonly World worldObj;
		private readonly int xPos;
		private readonly int yPos;
		private readonly int zPos;
		

		public BlockSourceImpl(World p_i1365_1_, int p_i1365_2_, int p_i1365_3_, int p_i1365_4_)
		{
			this.worldObj = p_i1365_1_;
			this.xPos = p_i1365_2_;
			this.yPos = p_i1365_3_;
			this.zPos = p_i1365_4_;
		}

		public virtual World World
		{
			get
			{
				return this.worldObj;
			}
		}

		public virtual double X
		{
			get
			{
				return (double)this.xPos + 0.5D;
			}
		}

		public virtual double Y
		{
			get
			{
				return (double)this.yPos + 0.5D;
			}
		}

		public virtual double Z
		{
			get
			{
				return (double)this.zPos + 0.5D;
			}
		}

		public virtual int XInt
		{
			get
			{
				return this.xPos;
			}
		}

		public virtual int YInt
		{
			get
			{
				return this.yPos;
			}
		}

		public virtual int ZInt
		{
			get
			{
				return this.zPos;
			}
		}

		public virtual int BlockMetadata
		{
			get
			{
				return this.worldObj.getBlockMetadata(this.xPos, this.yPos, this.zPos);
			}
		}

		public virtual TileEntity BlockTileEntity
		{
			get
			{
				return this.worldObj.getTileEntity(this.xPos, this.yPos, this.zPos);
			}
		}
	}

}