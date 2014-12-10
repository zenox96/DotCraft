using System;
using System.Collections;

namespace DotCraftCore.World
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;

	public class GameRules
	{
		private SortedDictionary theGameRules = new SortedDictionary();
		private const string __OBFID = "CL_00000136";

		public GameRules()
		{
			this.addGameRule("doFireTick", "true");
			this.addGameRule("mobGriefing", "true");
			this.addGameRule("keepInventory", "false");
			this.addGameRule("doMobSpawning", "true");
			this.addGameRule("doMobLoot", "true");
			this.addGameRule("doTileDrops", "true");
			this.addGameRule("commandBlockOutput", "true");
			this.addGameRule("naturalRegeneration", "true");
			this.addGameRule("doDaylightCycle", "true");
		}

///    
///     <summary> * Define a game rule and its default value. </summary>
///     
		public virtual void addGameRule(string p_82769_1_, string p_82769_2_)
		{
			this.theGameRules.Add(p_82769_1_, new GameRules.Value(p_82769_2_));
		}

		public virtual void setOrCreateGameRule(string p_82764_1_, string p_82764_2_)
		{
			GameRules.Value var3 = (GameRules.Value)this.theGameRules.get(p_82764_1_);

			if (var3 != null)
			{
				var3.Value = p_82764_2_;
			}
			else
			{
				this.addGameRule(p_82764_1_, p_82764_2_);
			}
		}

///    
///     <summary> * Gets the string Game Rule value. </summary>
///     
		public virtual string getGameRuleStringValue(string p_82767_1_)
		{
			GameRules.Value var2 = (GameRules.Value)this.theGameRules.get(p_82767_1_);
			return var2 != null ? var2.GameRuleStringValue : "";
		}

///    
///     <summary> * Gets the boolean Game Rule value. </summary>
///     
		public virtual bool getGameRuleBooleanValue(string p_82766_1_)
		{
			GameRules.Value var2 = (GameRules.Value)this.theGameRules.get(p_82766_1_);
			return var2 != null ? var2.GameRuleBooleanValue : false;
		}

///    
///     <summary> * Return the defined game rules as NBT. </summary>
///     
		public virtual NBTTagCompound writeGameRulesToNBT()
		{
			NBTTagCompound var1 = new NBTTagCompound();
			IEnumerator var2 = this.theGameRules.Keys.GetEnumerator();

			while (var2.MoveNext())
			{
				string var3 = (string)var2.Current;
				GameRules.Value var4 = (GameRules.Value)this.theGameRules.get(var3);
				var1.setString(var3, var4.GameRuleStringValue);
			}

			return var1;
		}

///    
///     <summary> * Set defined game rules from NBT. </summary>
///     
		public virtual void readGameRulesFromNBT(NBTTagCompound p_82768_1_)
		{
			Set var2 = p_82768_1_.func_150296_c();
			IEnumerator var3 = var2.GetEnumerator();

			while (var3.MoveNext())
			{
				string var4 = (string)var3.Current;
				string var6 = p_82768_1_.getString(var4);
				this.setOrCreateGameRule(var4, var6);
			}
		}

///    
///     <summary> * Return the defined game rules. </summary>
///     
		public virtual string[] Rules
		{
			get
			{
				return (string[])this.theGameRules.Keys.ToArray(new string[0]);
			}
		}

///    
///     <summary> * Return whether the specified game rule is defined. </summary>
///     
		public virtual bool hasRule(string p_82765_1_)
		{
			return this.theGameRules.ContainsKey(p_82765_1_);
		}

		internal class Value
		{
			private string valueString;
			private bool valueBoolean;
			private int valueInteger;
			private double valueDouble;
			private const string __OBFID = "CL_00000137";

			public Value(string p_i1949_1_)
			{
				this.Value = p_i1949_1_;
			}

			public virtual string Value
			{
				set
				{
					this.valueString = value;
					this.valueBoolean = Convert.ToBoolean(value);
	
					try
					{
						this.valueInteger = Convert.ToInt32(value);
					}
					catch (NumberFormatException var4)
					{
						;
					}
	
					try
					{
						this.valueDouble = Convert.ToDouble(value);
					}
					catch (NumberFormatException var3)
					{
						;
					}
				}
			}

			public virtual string GameRuleStringValue
			{
				get
				{
					return this.valueString;
				}
			}

			public virtual bool GameRuleBooleanValue
			{
				get
				{
					return this.valueBoolean;
				}
			}
		}
	}

}