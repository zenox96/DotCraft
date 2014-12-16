using System;

namespace DotCraftCore.Util
{

	using CrashReport = DotCraftCore.crash.CrashReport;

	public class ReportedException : Exception
	{
	/// <summary> Instance of CrashReport.  </summary>
		private readonly CrashReport theReportedExceptionCrashReport;
		

		public ReportedException(CrashReport p_i1356_1_)
		{
			this.theReportedExceptionCrashReport = p_i1356_1_;
		}

///    
///     <summary> * Gets the CrashReport wrapped by this exception. </summary>
///     
		public virtual CrashReport CrashReport
		{
			get
			{
				return this.theReportedExceptionCrashReport;
			}
		}

		public virtual Exception Cause
		{
			get
			{
				return this.theReportedExceptionCrashReport.CrashCause;
			}
		}

		public virtual string Message
		{
			get
			{
				return this.theReportedExceptionCrashReport.Description;
			}
		}
	}

}