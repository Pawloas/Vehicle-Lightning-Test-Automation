using Lighting.Api.DTOs;
using Lighting.Domain;
using Lighting.Domain.Diagnostics;
using Lighting.Domain.Diagnostics.MeasurementsInfo;
using Microsoft.AspNetCore.Mvc;

namespace Lighting.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LightingController : ControllerBase
    {
        private readonly LightingSystem _lightingSystem;

        public LightingController(LightingSystem lightingSystem)
        {
            _lightingSystem = lightingSystem;
        }

        [HttpGet]
        public IActionResult Get()
        {
            DiagnosticManager diagnosticManager = _lightingSystem.DiagnosticManager;

            Diagnostic<Intensity> intensityDiagnostic = diagnosticManager.IntensityDiagnostic;
            Diagnostic<Temperature> temperatureDiagnostic = diagnosticManager.TemperatureDiagnostic;
            Diagnostic<Voltage> voltageDiagnostic = diagnosticManager.VoltageDiagnostic;


            var response = new LigtingSystemResponse
            {
                LightMode = _lightingSystem.LightMode.ToString(),

                IntensityDiagnostic = new DiagnosticResponse
                {
                    Code = intensityDiagnostic.Code.ToString(),
                    Severity = intensityDiagnostic.Severity.ToString(),
                    //Parameter = intensityDiagnostic.Parameter.ToString(),
                    Message = intensityDiagnostic.Message,
                    TimeStamp = intensityDiagnostic.TimeStamp.ToString(),
                },

                TemperatureDiagnostic = new DiagnosticResponse
                {
                    Code = temperatureDiagnostic.Code.ToString(),
                    Severity = temperatureDiagnostic.Severity.ToString(),
                    // Parameter = temperatureDiagnostic.Parameter.ToString(),
                    Message = temperatureDiagnostic.Message,
                    TimeStamp = temperatureDiagnostic.TimeStamp.ToString(),
                },

                VoltageDiagnostic = new DiagnosticResponse
                {
                    Code = voltageDiagnostic.Code.ToString(),
                    Severity = voltageDiagnostic.Severity.ToString(),
                    //Parameter = voltageDiagnostic.Parameter.ToString(),
                    Message = voltageDiagnostic.Message,
                    TimeStamp = voltageDiagnostic.TimeStamp.ToString(),
                }

            };

            return Ok(response);
        }

        [HttpGet("mode")]
        public IActionResult GetMode() => Ok(new ModeResponse { LightMode = _lightingSystem.LightMode.ToString() });

        [HttpPost("intensity")]
        public IActionResult GetIntensity() 
        {
            Diagnostic<Intensity> intensityDiagnostic = _lightingSystem.DiagnosticManager.IntensityDiagnostic;

            IntensityResponse response = new IntensityResponse
            {
                Intensity = _lightingSystem.Intensity.ToString(),
                IntensityDiagnostic = new DiagnosticResponse
                {
                    Code = intensityDiagnostic.Code.ToString(),
                    Severity = intensityDiagnostic.Severity.ToString(),
                    //Parameter = intensityDiagnostic.Parameter.ToString(),
                    Message = intensityDiagnostic.Message,
                    TimeStamp = intensityDiagnostic.TimeStamp.ToString(),
                },
            };

            return Ok(response);
        }

        [HttpPost("set_mode")]
        public ActionResult<string> Create(string mode = "")
        {
            if (_lightingSystem.LightMode.ToString().Equals(mode))
            {
                return Ok("OK");
            }
            else
            {
                return BadRequest("NOK");
            }
        }
	}
}