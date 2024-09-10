using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class Rootobject
{
    public StageResponse[] Property1 { get; set; }
}

public class StageResponse
{

    [JsonProperty("id")]
    public int StageID { get; set; }

    [JsonProperty("stage_name")]
    public string StageName { get; set; }

    [JsonProperty("created_at")]
    public string Created { get; set; }

    [JsonProperty("updated_at")]
    public string Updated { get; set; }
}
