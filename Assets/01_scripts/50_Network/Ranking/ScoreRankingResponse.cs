using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ScoreRankingResponse
{

    [JsonProperty("id")]
    public int StageID { get; set; }

    [JsonProperty("user_name")]
    public string UserName { get; set; }


    [JsonProperty("score")]
    public string Score { get; set; }


    [JsonProperty("created_at")]
    public string Created { get; set; }

    [JsonProperty("updated_at")]
    public string Updated { get; set; }
}
