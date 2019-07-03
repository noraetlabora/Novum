using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Os.Server.Client.Model
{
  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ClientStatus 
  {
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or Sets SessionAddress
    /// </summary>
    [DataMember(Name="sessionAddress", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "sessionAddress")]
    public string SessionAddress { get; set; }

    /// <summary>
    /// Gets or Sets SessionMedium
    /// </summary>
    [DataMember(Name="sessionMedium", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "sessionMedium")]
    public string SessionMedium { get; set; }

    /// <summary>
    /// Gets or Sets Timestamp
    /// </summary>
    [DataMember(Name="timestamp", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "timestamp")]
    public int? Timestamp { get; set; }

    /// <summary>
    /// If the client supports simple printing this is the path to the printer to use for getting printer status information and post print jobs; the same path is also provided via the registerClient request to the POS
    /// </summary>
    /// <value>If the client supports simple printing this is the path to the printer to use for getting printer status information and post print jobs; the same path is also provided via the registerClient request to the POS</value>
    [DataMember(Name="printerPath", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "printerPath")]
    public string PrinterPath { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  
    {
      var sb = new StringBuilder();
      sb.Append("class ClientStatus {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  SessionAddress: ").Append(SessionAddress).Append("\n");
      sb.Append("  SessionMedium: ").Append(SessionMedium).Append("\n");
      sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
      sb.Append("  PrinterPath: ").Append(PrinterPath).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() 
    {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
