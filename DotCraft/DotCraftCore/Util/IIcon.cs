namespace DotCraftCore.Util
{

	public interface IIcon
	{
///    
///     <summary> * Returns the width of the icon, in pixels. </summary>
///     
		int IconWidth {get;}

///    
///     <summary> * Returns the height of the icon, in pixels. </summary>
///     
		int IconHeight {get;}

///    
///     <summary> * Returns the minimum U coordinate to use when rendering with this icon. </summary>
///     
		float MinU {get;}

///    
///     <summary> * Returns the maximum U coordinate to use when rendering with this icon. </summary>
///     
		float MaxU {get;}

///    
///     <summary> * Gets a U coordinate on the icon. 0 returns uMin and 16 returns uMax. Other arguments return in-between values. </summary>
///     
		float getInterpolatedU(double p_94214_1_);

///    
///     <summary> * Returns the minimum V coordinate to use when rendering with this icon. </summary>
///     
		float MinV {get;}

///    
///     <summary> * Returns the maximum V coordinate to use when rendering with this icon. </summary>
///     
		float MaxV {get;}

///    
///     <summary> * Gets a V coordinate on the icon. 0 returns vMin and 16 returns vMax. Other arguments return in-between values. </summary>
///     
		float getInterpolatedV(double p_94207_1_);

		string IconName {get;}
	}

}