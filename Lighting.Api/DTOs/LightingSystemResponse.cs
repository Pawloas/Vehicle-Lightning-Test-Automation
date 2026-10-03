using Lighting.Api.DTOs;
using Lighting.Domain;
using Lighting.Domain.Diagnostics;
using Lighting.Domain.Diagnostics.MeasurementsInfo;

public class LigtingSystemResponse
{
    public string LightMode { get; init; }
    public DiagnosticResponse IntensityDiagnostic { get; init; }
    public DiagnosticResponse TemperatureDiagnostic { get; init; }
    public DiagnosticResponse VoltageDiagnostic { get; init; }
}