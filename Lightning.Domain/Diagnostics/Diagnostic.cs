using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;

namespace Lighting.Domain.Diagnostics
{
	public abstract class Diagnostic
	{
		public DiagnosticCode Code { get; set; } = DiagnosticCode.None;
		public DiagnosticSeverity Severity { get; set; } = DiagnosticSeverity.Info;
		public DiagnosticParameter Parameter { get; private set; } = DiagnosticParameter.None;
		public string Message { get; set; } = string.Empty;
		public DateTime TimeStamp { get; set; } = DateTime.Now;

		public Diagnostic(DiagnosticParameter parameter)
		{
			Code = DiagnosticCode.None;
			Severity = DiagnosticSeverity.Info;
			Parameter = parameter;	
			Message = string.Empty;
			TimeStamp = DateTime.Now;
		}

		public Diagnostic(DiagnosticCode code, DiagnosticSeverity severity, DiagnosticParameter parameter, string message = "")
		{
			Code = code;
			Severity = severity;
			Parameter = parameter;
			Message = message;
			TimeStamp = DateTime.Now;
		}

		public void SetDefaultSetting()
		{
			Code = DiagnosticCode.None;
			Severity = DiagnosticSeverity.Info;
			Message = string.Empty;
			TimeStamp = DateTime.Now;
		}

		public void SetDiagnosticSetting(DiagnosticCode code, DiagnosticSeverity severity, string message = "")
		{
			Code = code;
			Severity = severity;
			Message = message;
			TimeStamp = DateTime.Now;
		}
	}

	public class  Diagnostic<T> : Diagnostic where T : MeasurementInfo<double>, new()
	{
		public T ActualValue = new T();

		private static DiagnosticParameter FindDiagnosticParameterFromTType(T actualValue)
		{
			return typeof(T) switch
			{
				var t when t == typeof(Temperature) => DiagnosticParameter.Temperature,
				var t when t == typeof(Voltage) => DiagnosticParameter.Voltage,
				var t when t == typeof(Intensity) => DiagnosticParameter.Intensity,
				_ => throw new ArgumentException(
					$"Unsupported type {typeof(T).Name} for Diagnostic<T>")
			};
		}

		public Diagnostic(T actualValue) : base(FindDiagnosticParameterFromTType(actualValue))
		{
			ActualValue = actualValue;
		}
	}
}
