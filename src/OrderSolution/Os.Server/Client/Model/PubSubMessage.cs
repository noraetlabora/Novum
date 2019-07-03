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
  public class PubSubMessage 
  {
    /// <summary>
    /// Base64 encoded data
    /// </summary>
    /// <value>Base64 encoded data</value>
    [DataMember(Name="payload", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "payload")]
    public string Payload { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  
    {
      var sb = new StringBuilder();
      sb.Append("class PubSubMessage {\n");
      sb.Append("  Payload: ").Append(Payload).Append("\n");
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
