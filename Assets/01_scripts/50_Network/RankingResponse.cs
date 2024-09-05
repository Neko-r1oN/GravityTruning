using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class RankingResponse
{
    [JsonProperty("id")]
    public int StageID { get; set; }
}
