using DotCraftCore.nServer.nManagement;
using System;

namespace DotCraftServer.nServer.nDedicated
{
	public class DedicatedPlayerList : ServerConfigurationManager
	{
		public DedicatedPlayerList(DedicatedServer p_i1503_1_) : base(p_i1503_1_)
		{
			this.func_152611_a(p_i1503_1_.getIntProperty("view-distance", 10));
			this.maxPlayers = p_i1503_1_.getIntProperty("max-players", 20);
			this.WhiteListEnabled = p_i1503_1_.getBooleanProperty("white-list", false);

			if (!p_i1503_1_.SinglePlayer)
			{
				this.func_152608_h().func_152686_a(true);
				this.BannedIPs.func_152686_a(true);
			}

			this.func_152620_y();
			this.func_152617_w();
			this.func_152619_x();
			this.func_152618_v();
			this.loadOpsList();
			this.readWhiteList();
			this.saveOpsList();

			if (!this.func_152599_k().func_152691_c().exists())
			{
				this.saveWhiteList();
			}
		}

		public virtual bool WhiteListEnabled
		{
			set
			{
				base.WhiteListEnabled = value;
				this.ServerInstance.setProperty("white-list", Convert.ToBoolean(value));
				this.ServerInstance.saveProperties();
			}
		}

		public virtual void func_152605_a(GameProfile p_1526051_)
		{
			base.func_152605_a(p_1526051_);
			this.saveOpsList();
		}

		public virtual void func_152610_b(GameProfile p_1526101_)
		{
			base.func_152610_b(p_1526101_);
			this.saveOpsList();
		}

		public virtual void func_152597_c(GameProfile p_1525971_)
		{
			base.func_152597_c(p_1525971_);
			this.saveWhiteList();
		}

		public virtual void func_152601d(GameProfile p_1526011_)
		{
			base.func_152601d(p_1526011_);
			this.saveWhiteList();
		}

		/// <summary>
		/// Either does nothing, or calls readWhiteList.
		/// </summary>
		public virtual void loadWhiteList()
		{
			this.readWhiteList();
		}

		private void func_152618_v()
		{
			try
			{
				this.BannedIPs.func_152678f();
			}
			catch (IOException var2)
			{
				field_164439d.warn("Failed to save ip banlist: ", var2);
			}
		}

		private void func_152617_w()
		{
			try
			{
				this.func_152608_h().func_152678f();
			}
			catch (IOException var2)
			{
				field_164439d.warn("Failed to save user banlist: ", var2);
			}
		}

		private void func_152619_x()
		{
			try
			{
				this.BannedIPs.func_152679_g();
			}
			catch (IOException var2)
			{
				field_164439d.warn("Failed to load ip banlist: ", var2);
			}
		}

		private void func_152620_y()
		{
			try
			{
				this.func_152608_h().func_152679_g();
			}
			catch (IOException var2)
			{
				field_164439d.warn("Failed to load user banlist: ", var2);
			}
		}

		private void loadOpsList()
		{
			try
			{
				this.func_152603m().func_152679_g();
			}
			catch (Exception var2)
			{
				field_164439d.warn("Failed to load operators list: ", var2);
			}
		}

		private void saveOpsList()
		{
			try
			{
				this.func_152603m().func_152678f();
			}
			catch (Exception var2)
			{
				field_164439d.warn("Failed to save operators list: ", var2);
			}
		}

		private void readWhiteList()
		{
			try
			{
				this.func_152599_k().func_152679_g();
			}
			catch (Exception var2)
			{
				field_164439d.warn("Failed to load white-list: ", var2);
			}
		}

		private void saveWhiteList()
		{
			try
			{
				this.func_152599_k().func_152678f();
			}
			catch (Exception var2)
			{
				field_164439d.warn("Failed to save white-list: ", var2);
			}
		}

		public virtual bool func_152607_e(GameProfile p_1526071_)
		{
			return !this.WhiteListEnabled || this.func_152596_g(p_1526071_) || this.func_152599_k().func_152705_a(p_1526071_);
		}

		public virtual DedicatedServer ServerInstance
		{
			get
			{
				return (DedicatedServer)base.ServerInstance;
			}
		}
	}

}