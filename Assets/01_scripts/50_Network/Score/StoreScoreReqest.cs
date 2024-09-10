using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StoreScoreReqest
{
    [JsonProperty("user_id")]
    public int Id { get; set; }
    [JsonProperty("user_name")]
    public string Name { get; set; }
    [JsonProperty("score")]
    public int Score { get; set; }
}
