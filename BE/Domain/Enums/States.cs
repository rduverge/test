using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace CuentasPorCobrar.Shared;

[JsonConverter(typeof(StringEnumConverter))]
public enum States
{

    Pendiente,
    Aprobado,
    Rechazado

}


