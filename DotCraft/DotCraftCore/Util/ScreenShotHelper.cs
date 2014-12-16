using System;

namespace DotCraftCore.Util
{

	using ImageIO = javax.imageio.ImageIO;
	using OpenGlHelper = DotCraftCore.client.renderer.OpenGlHelper;
	using TextureUtil = DotCraftCore.client.renderer.texture.TextureUtil;
	using Framebuffer = DotCraftCore.client.shader.Framebuffer;
	using ClickEvent = DotCraftCore.event.ClickEvent;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;
	using BufferUtils = org.lwjgl.BufferUtils;
	using GL11 = org.lwjgl.opengl.GL11;
	using GL12 = org.lwjgl.opengl.GL12;

	public class ScreenShotHelper
	{
		private static readonly Logger logger = LogManager.Logger;
		private static readonly DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd_HH.mm.ss");

	/// <summary> A buffer to hold pixel values returned by OpenGL.  </summary>
		private static IntBuffer pixelBuffer;

///    
///     <summary> * The built-up array that contains all the pixel values returned by OpenGL. </summary>
///     
		private static int[] pixelValues;
		

///    
///     <summary> * Saves a screenshot in the game directory with a time-stamped filename.  Args: gameDirectory,
///     * requestedWidthInPixels, requestedHeightInPixels, frameBuffer </summary>
///     
		public static IChatComponent saveScreenshot(File p_148260_0_, int p_148260_1_, int p_148260_2_, Framebuffer p_148260_3_)
		{
			return saveScreenshot(p_148260_0_, (string)null, p_148260_1_, p_148260_2_, p_148260_3_);
		}

///    
///     <summary> * Saves a screenshot in the game directory with the given file name (or null to generate a time-stamped name).
///     * Args: gameDirectory, fileName, requestedWidthInPixels, requestedHeightInPixels, frameBuffer </summary>
///     
		public static IChatComponent saveScreenshot(File p_148259_0_, string p_148259_1_, int p_148259_2_, int p_148259_3_, Framebuffer p_148259_4_)
		{
			try
			{
				File var5 = new File(p_148259_0_, "screenshots");
				var5.mkdir();

				if(OpenGlHelper.FramebufferEnabled)
				{
					p_148259_2_ = p_148259_4_.framebufferTextureWidth;
					p_148259_3_ = p_148259_4_.framebufferTextureHeight;
				}

				int var6 = p_148259_2_ * p_148259_3_;

				if(pixelBuffer == null || pixelBuffer.capacity() < var6)
				{
					pixelBuffer = BufferUtils.createIntBuffer(var6);
					pixelValues = new int[var6];
				}

				GL11.glPixelStorei(GL11.GL_PACK_ALIGNMENT, 1);
				GL11.glPixelStorei(GL11.GL_UNPACK_ALIGNMENT, 1);
				pixelBuffer.clear();

				if(OpenGlHelper.FramebufferEnabled)
				{
					GL11.glBindTexture(GL11.GL_TEXTURE_2D, p_148259_4_.framebufferTexture);
					GL11.glGetTexImage(GL11.GL_TEXTURE_2D, 0, GL12.GL_BGRA, GL12.GL_UNSIGNED_INT_8_8_8_8_REV, pixelBuffer);
				}
				else
				{
					GL11.glReadPixels(0, 0, p_148259_2_, p_148259_3_, GL12.GL_BGRA, GL12.GL_UNSIGNED_INT_8_8_8_8_REV, pixelBuffer);
				}

				pixelBuffer.get(pixelValues);
				TextureUtil.func_147953_a(pixelValues, p_148259_2_, p_148259_3_);
				BufferedImage var7 = null;

				if(OpenGlHelper.FramebufferEnabled)
				{
					var7 = new BufferedImage(p_148259_4_.framebufferWidth, p_148259_4_.framebufferHeight, 1);
					int var8 = p_148259_4_.framebufferTextureHeight - p_148259_4_.framebufferHeight;

					for(int var9 = var8; var9 < p_148259_4_.framebufferTextureHeight; ++var9)
					{
						for(int var10 = 0; var10 < p_148259_4_.framebufferWidth; ++var10)
						{
							var7.setRGB(var10, var9 - var8, pixelValues[var9 * p_148259_4_.framebufferTextureWidth + var10]);
						}
					}
				}
				else
				{
					var7 = new BufferedImage(p_148259_2_, p_148259_3_, 1);
					var7.setRGB(0, 0, p_148259_2_, p_148259_3_, pixelValues, 0, p_148259_2_);
				}

				File var12;

				if(p_148259_1_ == null)
				{
					var12 = getTimestampedPNGFileForDirectory(var5);
				}
				else
				{
					var12 = new File(var5, p_148259_1_);
				}

				ImageIO.write(var7, "png", var12);
				ChatComponentText var13 = new ChatComponentText(var12.Name);
				var13.ChatStyle.ChatClickEvent = new ClickEvent(ClickEvent.Action.OPEN_FILE, var12.AbsolutePath);
				var13.ChatStyle.Underlined = Convert.ToBoolean(true);
				return new ChatComponentTranslation("screenshot.success", new object[] {var13});
			}
			catch (Exception var11)
			{
				logger.warn("Couldn\'t save screenshot", var11);
				return new ChatComponentTranslation("screenshot.failure", new object[] {var11.Message});
			}
		}

///    
///     <summary> * Creates a unique PNG file in the given directory named by a timestamp.  Handles cases where the timestamp alone
///     * is not enough to create a uniquely named file, though it still might suffer from an unlikely race condition where
///     * the filename was unique when this method was called, but another process or thread created a file at the same
///     * path immediately after this method returned. </summary>
///     
		private static File getTimestampedPNGFileForDirectory(File p_74290_0_)
		{
			string var2 = dateFormat.format(DateTime.Now).ToString();
			int var3 = 1;

			while(true)
			{
				File var1 = new File(p_74290_0_, var2 + (var3 == 1 ? "" : "_" + var3) + ".png");

				if(!var1.exists())
				{
					return var1;
				}

				++var3;
			}
		}
	}

}