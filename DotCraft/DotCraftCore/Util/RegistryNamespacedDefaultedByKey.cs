namespace DotCraftCore.Util
{

	public class RegistryNamespacedDefaultedByKey : RegistryNamespaced
	{
		private readonly string field_148760_d;
		private object field_148761_e;
		private const string __OBFID = "CL_00001196";

		public RegistryNamespacedDefaultedByKey(string p_i45127_1_)
		{
			this.field_148760_d = p_i45127_1_;
		}

///    
///     <summary> * Adds a new object to this registry, keyed by both the given integer ID and the given string. </summary>
///     
		public override void addObject(int p_148756_1_, string p_148756_2_, object p_148756_3_)
		{
			if(this.field_148760_d.Equals(p_148756_2_))
			{
				this.field_148761_e = p_148756_3_;
			}

			base.addObject(p_148756_1_, p_148756_2_, p_148756_3_);
		}

		public override object getObject(string p_82594_1_)
		{
			object var2 = base.getObject(p_82594_1_);
			return var2 == null ? this.field_148761_e : var2;
		}

///    
///     <summary> * Gets the object identified by the given ID. </summary>
///     
		public override object getObjectForID(int p_148754_1_)
		{
			object var2 = base.getObjectForID(p_148754_1_);
			return var2 == null ? this.field_148761_e : var2;
		}

		public override object getObject(object p_82594_1_)
		{
			return this.getObject((string)p_82594_1_);
		}
	}

}