using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotCraftCore.Block
{
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using World = DotCraftCore.World.World;

	public class BlockAir : Block
	{
        protected internal BlockAir( )
            : base(Material.Material.air)
		{
		}

        public override String BlockName
        {
            get
            {
                return "air";
            }
        }
///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return -1;
			}
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			return null;
		}

		public virtual bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

///    
///     * Returns whether this block is collideable based on the arguments passed in \n<param name="par1"> block metaData \n@param
///     * par2 whether the player right-clicked while holding a boat </param>
///     
		public virtual bool canCollideCheck(int p_149678_1_, bool p_149678_2_)
		{
			return false;
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public virtual void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
		}
	}
}