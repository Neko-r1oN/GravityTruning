using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class CreateTokenResponse
{
    [JsonProperty("user_id")]
    public int UserID { get; set; }
    public int AuthToken { get; set; }
}
