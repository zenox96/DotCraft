using System;

namespace DotCraftServer.nServer.nDedicated
{
	public class PropertyManager
	{
		/// <summary>
		/// The server properties object. </summary>
		private readonly Properties serverProperties = new Properties();

		/// <summary>
		/// The server properties file. </summary>
		private readonly File serverPropertiesFile;
		private const string __OBFID = "CL_00001782";

		public PropertyManager(File p_i45278_1_)
		{
			this.serverPropertiesFile = p_i45278_1_;

			if (p_i45278_1_.exists())
			{
				System.IO.FileStream var2 = null;

				try
				{
					var2 = new System.IO.FileStream(p_i45278_1_, System.IO.FileMode.Open, System.IO.FileAccess.Read);
					this.serverProperties.load(var2);
				}
				catch (Exception var12)
				{
					field_164440_a.warn("Failed to load " + p_i45278_1_, var12);
					this.generateNewProperties();
				}
				finally
				{
					if (var2 != null)
					{
						try
						{
							var2.Close();
						}
						catch (IOException)
						{
							;
						}
					}
				}
			}
			else
			{
				field_164440_a.warn(p_i45278_1_ + " does not exist");
				this.generateNewProperties();
			}
		}

		/// <summary>
		/// Generates a new properties file.
		/// </summary>
		public virtual void generateNewProperties()
		{
			field_164440_a.info("Generating new properties file");
			this.saveProperties();
		}

		/// <summary>
		/// Writes the properties to the properties file.
		/// </summary>
		public virtual void saveProperties()
		{
			System.IO.FileStream var1 = null;

			try
			{
				var1 = new System.IO.FileStream(this.serverPropertiesFile, System.IO.FileMode.Create, System.IO.FileAccess.Write);
				this.serverProperties.store(var1, "Minecraft server properties");
			}
			catch (Exception var11)
			{
				field_164440_a.warn("Failed to save " + this.serverPropertiesFile, var11);
				this.generateNewProperties();
			}
			finally
			{
				if (var1 != null)
				{
					try
					{
						var1.Close();
					}
					catch (IOException)
					{
						;
					}
				}
			}
		}

		/// <summary>
		/// Returns this PropertyManager's file object used for property saving.
		/// </summary>
		public virtual File PropertiesFile
		{
			get
			{
				return this.serverPropertiesFile;
			}
		}

		/// <summary>
		/// Returns a string property. If the property doesn't exist the default is returned.
		/// </summary>
		public virtual string getStringProperty(string p_736711_, string p_736712_)
		{
			if (!this.serverProperties.containsKey(p_736711_))
			{
				this.serverProperties.setProperty(p_736711_, p_736712_);
				this.saveProperties();
				this.saveProperties();
			}

			return this.serverProperties.getProperty(p_736711_, p_736712_);
		}

		/// <summary>
		/// Gets an integer property. If it does not exist, set it to the specified value.
		/// </summary>
		public virtual int getIntProperty(string p_736691_, int p_736692_)
		{
			try
			{
				return int.Parse(this.getStringProperty(p_736691_, "" + p_736692_));
			}
			catch (Exception)
			{
				this.serverProperties.setProperty(p_736691_, "" + p_736692_);
				this.saveProperties();
				return p_736692_;
			}
		}

		/// <summary>
		/// Gets a boolean property. If it does not exist, set it to the specified value.
		/// </summary>
		public virtual bool getBooleanProperty(string p_736701_, bool p_736702_)
		{
			try
			{
				return bool.Parse(this.getStringProperty(p_736701_, "" + p_736702_));
			}
			catch (Exception)
			{
				this.serverProperties.setProperty(p_736701_, "" + p_736702_);
				this.saveProperties();
				return p_736702_;
			}
		}

		/// <summary>
		/// Saves an Object with the given property name.
		/// </summary>
		public virtual void setProperty(string p_736671_, object p_736672_)
		{
			this.serverProperties.setProperty(p_736671_, "" + p_736672_);
		}
	}

}