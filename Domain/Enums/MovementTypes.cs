
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace CuentasPorCobrar.Shared;

[JsonConverter(typeof(StringEnumConverter))]
public enum MovementTypes
{
    Debito, 
    Credito
}

