using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class BookCatogaryReceivedData
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("rows")]
    public List<Row> Rows { get; set; }
}

public class BookCatogaryReceivedDataClass
{
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("data")]
    public BookCatogaryReceivedData Data { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}

public class Row
{
    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("is_deleted")]
    public bool IsDeleted { get; set; }

    [JsonProperty("created_by")]
    public string CreatedBy { get; set; }

    [JsonProperty("category_thumbnail")]
    public string CategoryThumbnail { get; set; }

    [JsonProperty("category_title")]
    public string CategoryTitle { get; set; }

    [JsonProperty("modifiedAt")]
    public string ModifiedAt { get; set; }

    [JsonProperty("modified_by")]
    public string ModifiedBy { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public int V { get; set; }

    [JsonProperty("string")]
    public List<StringData> String { get; set; }
}

public class StringData
{
    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("is_deleted")]
    public bool IsDeleted { get; set; }

    [JsonProperty("created_by")]
    public string CreatedBy { get; set; }

    [JsonProperty("category_id")]
    public string CategoryId { get; set; }

    [JsonProperty("collection_thumbnail")]
    public string CollectionThumbnail { get; set; }

    [JsonProperty("collection_title")]
    public string CollectionTitle { get; set; }

    [JsonProperty("modifiedAt")]
    public string ModifiedAt { get; set; }

    [JsonProperty("modified_by")]
    public string ModifiedBy { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public int V { get; set; }
}

