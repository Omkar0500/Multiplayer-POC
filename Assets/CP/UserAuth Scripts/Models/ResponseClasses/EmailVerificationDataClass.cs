using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class EmailVerificationReceivedDataClass
{
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}

