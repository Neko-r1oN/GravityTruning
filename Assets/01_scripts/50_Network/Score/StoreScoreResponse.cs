using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StoreScoreResponse
{
    [JsonProperty("user_id")]
    public string Id { get; set; }
}
