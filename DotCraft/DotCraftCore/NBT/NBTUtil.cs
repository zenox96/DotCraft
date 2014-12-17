using System;
using System.Collections;

namespace DotCraftCore.nNBT
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using Property = com.mojang.authlib.properties.Property;
	using StringUtils = DotCraftCore.nUtil.StringUtils;

	public sealed class NBTUtil
	{
		

		public static GameProfile func_152459_a(NBTTagCompound p_152459_0_)
		{
			string var1 = null;
			string var2 = null;

			if (p_152459_0_.func_150297_b("Name", 8))
			{
				var1 = p_152459_0_.getString("Name");
			}

			if (p_152459_0_.func_150297_b("Id", 8))
			{
				var2 = p_152459_0_.getString("Id");
			}

			if (StringUtils.isNullOrEmpty(var1) && StringUtils.isNullOrEmpty(var2))
			{
				return null;
			}
			else
			{
				UUID var3;

				try
				{
					var3 = UUID.fromString(var2);
				}
				catch (Exception var12)
				{
					var3 = null;
				}

				GameProfile var4 = new GameProfile(var3, var1);

				if (p_152459_0_.func_150297_b("Properties", 10))
				{
					NBTTagCompound var5 = p_152459_0_.getCompoundTag("Properties");
					IEnumerator var6 = var5.func_150296_c().GetEnumerator();

					while (var6.MoveNext())
					{
						string var7 = (string)var6.Current;
						NBTTagList var8 = var5.getTagList(var7, 10);

						for (int var9 = 0; var9 < var8.tagCount(); ++var9)
						{
							NBTTagCompound var10 = var8.getCompoundTagAt(var9);
							string var11 = var10.getString("Value");

							if (var10.func_150297_b("Signature", 8))
							{
								var4.Properties.put(var7, new Property(var7, var11, var10.getString("Signature")));
							}
							else
							{
								var4.Properties.put(var7, new Property(var7, var11));
							}
						}
					}
				}

				return var4;
			}
		}

		public static void func_152460_a(NBTTagCompound p_152460_0_, GameProfile p_152460_1_)
		{
			if (!StringUtils.isNullOrEmpty(p_152460_1_.Name))
			{
				p_152460_0_.setString("Name", p_152460_1_.Name);
			}

			if (p_152460_1_.Id != null)
			{
				p_152460_0_.setString("Id", p_152460_1_.Id.ToString());
			}

			if (!p_152460_1_.Properties.Empty)
			{
				NBTTagCompound var2 = new NBTTagCompound();
				IEnumerator var3 = p_152460_1_.Properties.Keys.GetEnumerator();

				while (var3.MoveNext())
				{
					string var4 = (string)var3.Current;
					NBTTagList var5 = new NBTTagList();
					NBTTagCompound var8;

					for (IEnumerator var6 = p_152460_1_.Properties.get(var4).GetEnumerator(); var6.MoveNext(); var5.appendTag(var8))
					{
						Property var7 = (Property)var6.Current;
						var8 = new NBTTagCompound();
						var8.setString("Value", var7.Value);

						if (var7.hasSignature())
						{
							var8.setString("Signature", var7.Signature);
						}
					}

					var2.setTag(var4, var5);
				}

				p_152460_0_.setTag("Properties", var2);
			}
		}
	}

}