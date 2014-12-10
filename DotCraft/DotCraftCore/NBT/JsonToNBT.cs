using System;
using System.Collections;

namespace DotCraftCore.NBT
{

	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class JsonToNBT
	{
		private static readonly Logger logger = LogManager.Logger;
		private const string __OBFID = "CL_00001232";

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static NBTBase func_150315_a(String p_150315_0_) throws NBTException
		public static NBTBase func_150315_a(string p_150315_0_)
		{
			p_150315_0_ = p_150315_0_.Trim();
			int var1 = func_150310_b(p_150315_0_);

			if (var1 != 1)
			{
				throw new NBTException("Encountered multiple top tags, only one expected");
			}
			else
			{
				JsonToNBT.Any var2 = null;

				if (p_150315_0_.StartsWith("{"))
				{
					var2 = func_150316_a("tag", p_150315_0_);
				}
				else
				{
					var2 = func_150316_a(func_150313_b(p_150315_0_, false), func_150311_c(p_150315_0_, false));
				}

				return var2.func_150489_a();
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: static int func_150310_b(String p_150310_0_) throws NBTException
		internal static int func_150310_b(string p_150310_0_)
		{
			int var1 = 0;
			bool var2 = false;
			Stack var3 = new Stack();

			for (int var4 = 0; var4 < p_150310_0_.Length; ++var4)
			{
				char var5 = p_150310_0_[var4];

				if (var5 == 34)
				{
					if (var4 > 0 && p_150310_0_[var4 - 1] == 92)
					{
						if (!var2)
						{
							throw new NBTException("Illegal use of \\\": " + p_150310_0_);
						}
					}
					else
					{
						var2 = !var2;
					}
				}
				else if (!var2)
				{
					if (var5 != 123 && var5 != 91)
					{
						if (var5 == 125 && (var3.Count == 0 || (char)((char?)var3.Pop()) != 123))
						{
							throw new NBTException("Unbalanced curly brackets {}: " + p_150310_0_);
						}

						if (var5 == 93 && (var3.Count == 0 || (char)((char?)var3.Pop()) != 91))
						{
							throw new NBTException("Unbalanced square brackets []: " + p_150310_0_);
						}
					}
					else
					{
						if (var3.Count == 0)
						{
							++var1;
						}

						var3.Push(Convert.ToChar(var5));
					}
				}
			}

			if (var2)
			{
				throw new NBTException("Unbalanced quotation: " + p_150310_0_);
			}
			else if (!var3.Count == 0)
			{
				throw new NBTException("Unbalanced brackets: " + p_150310_0_);
			}
			else if (var1 == 0 && !p_150310_0_.Length == 0)
			{
				return 1;
			}
			else
			{
				return var1;
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: static JsonToNBT.Any func_150316_a(String p_150316_0_, String p_150316_1_) throws NBTException
		static JsonToNBT.Any func_150316_a(string p_150316_0_, string p_150316_1_)
		{
			p_150316_1_ = p_150316_1_.Trim();
			func_150310_b(p_150316_1_);
			string var3;
			string var4;
			string var5;
			char var6;

			if (p_150316_1_.StartsWith("{"))
			{
				if (!p_150316_1_.EndsWith("}"))
				{
					throw new NBTException("Unable to locate ending bracket for: " + p_150316_1_);
				}
				else
				{
					p_150316_1_ = p_150316_1_.Substring(1, p_150316_1_.Length - 1 - 1);
					JsonToNBT.Compound var7 = new JsonToNBT.Compound(p_150316_0_);

					while (p_150316_1_.Length > 0)
					{
						var3 = func_150314_a(p_150316_1_, false);

						if (var3.Length > 0)
						{
							var4 = func_150313_b(var3, false);
							var5 = func_150311_c(var3, false);
							var7.field_150491_b.Add(func_150316_a(var4, var5));

							if (p_150316_1_.Length < var3.Length + 1)
							{
								break;
							}

							var6 = p_150316_1_[var3.Length];

							if (var6 != 44 && var6 != 123 && var6 != 125 && var6 != 91 && var6 != 93)
							{
								throw new NBTException("Unexpected token \'" + var6 + "\' at: " + p_150316_1_.Substring(var3.Length));
							}

							p_150316_1_ = p_150316_1_.Substring(var3.Length + 1);
						}
					}

					return var7;
				}
			}
			else if (p_150316_1_.StartsWith("[") && !p_150316_1_.matches("\\[[-\\d|,\\s]+\\]"))
			{
				if (!p_150316_1_.EndsWith("]"))
				{
					throw new NBTException("Unable to locate ending bracket for: " + p_150316_1_);
				}
				else
				{
					p_150316_1_ = p_150316_1_.Substring(1, p_150316_1_.Length - 1 - 1);
					JsonToNBT.List var2 = new JsonToNBT.List(p_150316_0_);

					while (p_150316_1_.Length > 0)
					{
						var3 = func_150314_a(p_150316_1_, true);

						if (var3.Length > 0)
						{
							var4 = func_150313_b(var3, true);
							var5 = func_150311_c(var3, true);
							var2.field_150492_b.Add(func_150316_a(var4, var5));

							if (p_150316_1_.Length < var3.Length + 1)
							{
								break;
							}

							var6 = p_150316_1_[var3.Length];

							if (var6 != 44 && var6 != 123 && var6 != 125 && var6 != 91 && var6 != 93)
							{
								throw new NBTException("Unexpected token \'" + var6 + "\' at: " + p_150316_1_.Substring(var3.Length));
							}

							p_150316_1_ = p_150316_1_.Substring(var3.Length + 1);
						}
						else
						{
							logger.debug(p_150316_1_);
						}
					}

					return var2;
				}
			}
			else
			{
				return new JsonToNBT.Primitive(p_150316_0_, p_150316_1_);
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static String func_150314_a(String p_150314_0_, boolean p_150314_1_) throws NBTException
		private static string func_150314_a(string p_150314_0_, bool p_150314_1_)
		{
			int var2 = func_150312_a(p_150314_0_, ':');

			if (var2 < 0 && !p_150314_1_)
			{
				throw new NBTException("Unable to locate name/value separator for string: " + p_150314_0_);
			}
			else
			{
				int var3 = func_150312_a(p_150314_0_, ',');

				if (var3 >= 0 && var3 < var2 && !p_150314_1_)
				{
					throw new NBTException("Name error at: " + p_150314_0_);
				}
				else
				{
					if (p_150314_1_ && (var2 < 0 || var2 > var3))
					{
						var2 = -1;
					}

					Stack var4 = new Stack();
					int var5 = var2 + 1;
					bool var6 = false;
					bool var7 = false;
					bool var8 = false;

					for (int var9 = 0; var5 < p_150314_0_.Length; ++var5)
					{
						char var10 = p_150314_0_[var5];

						if (var10 == 34)
						{
							if (var5 > 0 && p_150314_0_[var5 - 1] == 92)
							{
								if (!var6)
								{
									throw new NBTException("Illegal use of \\\": " + p_150314_0_);
								}
							}
							else
							{
								var6 = !var6;

								if (var6 && !var8)
								{
									var7 = true;
								}

								if (!var6)
								{
									var9 = var5;
								}
							}
						}
						else if (!var6)
						{
							if (var10 != 123 && var10 != 91)
							{
								if (var10 == 125 && (var4.Count == 0 || (char)((char?)var4.Pop()) != 123))
								{
									throw new NBTException("Unbalanced curly brackets {}: " + p_150314_0_);
								}

								if (var10 == 93 && (var4.Count == 0 || (char)((char?)var4.Pop()) != 91))
								{
									throw new NBTException("Unbalanced square brackets []: " + p_150314_0_);
								}

								if (var10 == 44 && var4.Count == 0)
								{
									return p_150314_0_.Substring(0, var5);
								}
							}
							else
							{
								var4.Push(Convert.ToChar(var10));
							}
						}

						if (!char.IsWhiteSpace(var10))
						{
							if (!var6 && var7 && var9 != var5)
							{
								return p_150314_0_.Substring(0, var9 + 1);
							}

							var8 = true;
						}
					}

					return p_150314_0_.Substring(0, var5);
				}
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static String func_150313_b(String p_150313_0_, boolean p_150313_1_) throws NBTException
		private static string func_150313_b(string p_150313_0_, bool p_150313_1_)
		{
			if (p_150313_1_)
			{
				p_150313_0_ = p_150313_0_.Trim();

				if (p_150313_0_.StartsWith("{") || p_150313_0_.StartsWith("["))
				{
					return "";
				}
			}

			int var2 = p_150313_0_.IndexOf(58);

			if (var2 < 0)
			{
				if (p_150313_1_)
				{
					return "";
				}
				else
				{
					throw new NBTException("Unable to locate name/value separator for string: " + p_150313_0_);
				}
			}
			else
			{
				return p_150313_0_.Substring(0, var2).Trim();
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static String func_150311_c(String p_150311_0_, boolean p_150311_1_) throws NBTException
		private static string func_150311_c(string p_150311_0_, bool p_150311_1_)
		{
			if (p_150311_1_)
			{
				p_150311_0_ = p_150311_0_.Trim();

				if (p_150311_0_.StartsWith("{") || p_150311_0_.StartsWith("["))
				{
					return p_150311_0_;
				}
			}

			int var2 = p_150311_0_.IndexOf(58);

			if (var2 < 0)
			{
				if (p_150311_1_)
				{
					return p_150311_0_;
				}
				else
				{
					throw new NBTException("Unable to locate name/value separator for string: " + p_150311_0_);
				}
			}
			else
			{
				return p_150311_0_.Substring(var2 + 1).Trim();
			}
		}

		private static int func_150312_a(string p_150312_0_, char p_150312_1_)
		{
			int var2 = 0;

			for (bool var3 = false; var2 < p_150312_0_.Length; ++var2)
			{
				char var4 = p_150312_0_[var2];

				if (var4 == 34)
				{
					if (var2 <= 0 || p_150312_0_[var2 - 1] != 92)
					{
						var3 = !var3;
					}
				}
				else if (!var3)
				{
					if (var4 == p_150312_1_)
					{
						return var2;
					}

					if (var4 == 123 || var4 == 91)
					{
						return -1;
					}
				}
			}

			return -1;
		}

		internal abstract class Any
		{
			protected internal string field_150490_a;
			private const string __OBFID = "CL_00001233";

			public abstract NBTBase func_150489_a();
		}

		internal class Compound : JsonToNBT.Any
		{
			protected internal ArrayList field_150491_b = new ArrayList();
			private const string __OBFID = "CL_00001234";

			public Compound(string p_i45137_1_)
			{
				this.field_150490_a = p_i45137_1_;
			}

			public virtual NBTBase func_150489_a()
			{
				NBTTagCompound var1 = new NBTTagCompound();
				IEnumerator var2 = this.field_150491_b.GetEnumerator();

				while (var2.MoveNext())
				{
					JsonToNBT.Any var3 = (JsonToNBT.Any)var2.Current;
					var1.setTag(var3.field_150490_a, var3.func_150489_a());
				}

				return var1;
			}
		}

		internal class List : JsonToNBT.Any
		{
			protected internal ArrayList field_150492_b = new ArrayList();
			private const string __OBFID = "CL_00001235";

			public IList(string p_i45138_1_)
			{
				this.field_150490_a = p_i45138_1_;
			}

			public virtual NBTBase func_150489_a()
			{
				NBTTagList var1 = new NBTTagList();
				IEnumerator var2 = this.field_150492_b.GetEnumerator();

				while (var2.MoveNext())
				{
					JsonToNBT.Any var3 = (JsonToNBT.Any)var2.Current;
					var1.appendTag(var3.func_150489_a());
				}

				return var1;
			}
		}

		internal class Primitive : JsonToNBT.Any
		{
			protected internal string field_150493_b;
			private const string __OBFID = "CL_00001236";

			public Primitive(string p_i45139_1_, string p_i45139_2_)
			{
				this.field_150490_a = p_i45139_1_;
				this.field_150493_b = p_i45139_2_;
			}

			public virtual NBTBase func_150489_a()
			{
				try
				{
					if (this.field_150493_b.matches("[-+]?[0-9]*\\.?[0-9]+[d|D]"))
					{
						return new NBTTagDouble(Convert.ToDouble(this.field_150493_b.Substring(0, this.field_150493_b.Length - 1)));
					}
					else if (this.field_150493_b.matches("[-+]?[0-9]*\\.?[0-9]+[f|F]"))
					{
						return new NBTTagFloat(Convert.ToSingle(this.field_150493_b.Substring(0, this.field_150493_b.Length - 1)));
					}
					else if (this.field_150493_b.matches("[-+]?[0-9]+[b|B]"))
					{
						return new NBTTagByte(Convert.ToByte(this.field_150493_b.Substring(0, this.field_150493_b.Length - 1)));
					}
					else if (this.field_150493_b.matches("[-+]?[0-9]+[l|L]"))
					{
						return new NBTTagLong(Convert.ToInt64(this.field_150493_b.Substring(0, this.field_150493_b.Length - 1)));
					}
					else if (this.field_150493_b.matches("[-+]?[0-9]+[s|S]"))
					{
						return new NBTTagShort(Convert.ToInt16(this.field_150493_b.Substring(0, this.field_150493_b.Length - 1)));
					}
					else if (this.field_150493_b.matches("[-+]?[0-9]+"))
					{
						return new NBTTagInt(Convert.ToInt32(this.field_150493_b.Substring(0, this.field_150493_b.Length)));
					}
					else if (this.field_150493_b.matches("[-+]?[0-9]*\\.?[0-9]+"))
					{
						return new NBTTagDouble(Convert.ToDouble(this.field_150493_b.Substring(0, this.field_150493_b.Length)));
					}
					else if (!this.field_150493_b.equalsIgnoreCase("true") && !this.field_150493_b.equalsIgnoreCase("false"))
					{
						if (this.field_150493_b.StartsWith("[") && this.field_150493_b.EndsWith("]"))
						{
							if (this.field_150493_b.Length > 2)
							{
								string var1 = this.field_150493_b.Substring(1, this.field_150493_b.Length - 1 - 1);
								string[] var2 = StringHelperClass.StringSplit(var1, ",", true);

								try
								{
									if (var2.Length <= 1)
									{
										return new NBTTagIntArray(new int[] {Convert.ToInt32(var1.Trim())});
									}
									else
									{
										int[] var3 = new int[var2.Length];

										for (int var4 = 0; var4 < var2.Length; ++var4)
										{
											var3[var4] = Convert.ToInt32(var2[var4].Trim());
										}

										return new NBTTagIntArray(var3);
									}
								}
								catch (NumberFormatException var5)
								{
									return new NBTTagString(this.field_150493_b);
								}
							}
							else
							{
								return new NBTTagIntArray();
							}
						}
						else
						{
							if (this.field_150493_b.StartsWith("\"") && this.field_150493_b.EndsWith("\"") && this.field_150493_b.Length > 2)
							{
								this.field_150493_b = this.field_150493_b.Substring(1, this.field_150493_b.Length - 1 - 1);
							}

							this.field_150493_b = this.field_150493_b.replaceAll("\\\\\"", "\"");
							return new NBTTagString(this.field_150493_b);
						}
					}
					else
					{
						return new NBTTagByte((sbyte)(Convert.ToBoolean(this.field_150493_b) ? 1 : 0));
					}
				}
				catch (NumberFormatException var6)
				{
					this.field_150493_b = this.field_150493_b.replaceAll("\\\\\"", "\"");
					return new NBTTagString(this.field_150493_b);
				}
			}
		}
	}

}