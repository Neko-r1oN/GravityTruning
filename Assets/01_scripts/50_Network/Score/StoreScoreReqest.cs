using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StoreScoreReqest : MonoBehaviour
{
    [JsonProperty("user_id")]
    public string Id { get; set; }
    [JsonProperty("user_name")]
    public string Name { get; set; }
    [JsonProperty("score")]
    public string Score { get; set; }
}
