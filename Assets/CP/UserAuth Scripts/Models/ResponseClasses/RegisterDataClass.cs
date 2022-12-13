using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class RegisterReceivedData
{
    [JsonProperty("roles")]
    public List<string> Roles { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("is_deleted")]
    public bool IsDeleted { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("is_primary")]
    public bool IsPrimary { get; set; }

    [JsonProperty("is_verified")]
    public bool IsVerified { get; set; }

    [JsonProperty("is_dnc")]
    public bool IsDnc { get; set; }

    [JsonProperty("term_accepted")]
    public bool TermAccepted { get; set; }

    [JsonProperty("last_login")]
    public object LastLogin { get; set; }

    [JsonProperty("email_activated")]
    public bool EmailActivated { get; set; }

    [JsonProperty("preferred_gender_pronoun")]
    public string PreferredGenderPronoun { get; set; }

    [JsonProperty("activated_by")]
    public string ActivatedBy { get; set; }

    [JsonProperty("is_send_marketing_emails")]
    public bool IsSendMarketingEmails { get; set; }

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("organization_name")]
    public string OrganizationName { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public int V { get; set; }
}

public class RegisterReceivedDataClass
{
    [JsonProperty("data")]
    public RegisterReceivedData Data { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}
