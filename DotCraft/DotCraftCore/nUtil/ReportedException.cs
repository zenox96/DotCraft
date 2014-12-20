using System;

namespace DotCraftCore.nUtil
{

	using CrashReport = DotCraftCore.nCrash.CrashReport;

	public class ReportedException : Exception
	{
	/// <summary> Instance of CrashReport.  </summary>
		private readonly CrashReport theReportedExceptionCrashReport;
		

		public ReportedException(CrashReport crash)
		{
			this.theReportedExceptionCrashReport = crash;
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