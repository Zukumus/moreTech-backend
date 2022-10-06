using System.Text.Json.Serialization;
using Newscatcher.Client.Contracts.Models;

namespace Newscatcher.Client.SerializationContext;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(GetNewsByKeyResponseModel))]
[JsonSerializable(typeof(NewsModel))]
public partial class NewsCatcherClientMetadataContext : JsonSerializerContext
{
}