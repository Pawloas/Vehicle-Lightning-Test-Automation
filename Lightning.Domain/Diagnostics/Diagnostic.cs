using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;

namespace Lighting.Domain.Diagnostics
{
	public class Diagnostic<T> where T : IComparable<T>, new()
	{
		public DiagnosticCode Code { get; set; } = DiagnosticCode.None;
		public DiagnosticSeverity Severity { get; set; } = DiagnosticSeverity.Info;
		public DiagnosticParameter Parameter { get; set; } = DiagnosticParameter.None;
		public T ActualValue = new T();
		public string Message { get; set; } = string.Empty;
		public DateTime TimeStamp { get; set; } = DateTime.Now;


		public Diagnostic()
		{
			Code = DiagnosticCode.None;
			Severity = DiagnosticSeverity.Info;
			Parameter = DiagnosticParameter.None;
			ActualValue = T.Zero;
			Message = string.Empty;
			TimeStamp = DateTime.Now;
		}

		public Diagnostic(DiagnosticCode code, DiagnosticSeverity severity, DiagnosticParameter parameter, string message = "")
		{
			Code = code;
			Severity = severity;
			Parameter = parameter;
			ActualValue = T.Zero;
			Message = message;
			TimeStamp = DateTime.Now;
		}

		public void SetDefaultSetting()
		{
			Code = DiagnosticCode.None;
			Severity = DiagnosticSeverity.Info;
			Parameter = DiagnosticParameter.None;
			ActualValue = T.Zero;
			Message = string.Empty;
			TimeStamp = DateTime.Now;
		}

		public void SetDiagnosticSetting(DiagnosticCode code, DiagnosticSeverity severity, DiagnosticParameter parameter, string message = "")
		{
			Code = code;
			Severity = severity;
			Parameter = parameter;
			ActualValue = T.Zero;
			Message = message;
			TimeStamp = DateTime.Now;
		}
	}
}
