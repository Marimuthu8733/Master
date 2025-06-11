using System.Text.Json.Serialization;
using WebApi.Entities;
using WebApi.Models.Users;

namespace WebApi;

[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(List<User>))]
[JsonSerializable(typeof(CreateRequest))]
[JsonSerializable(typeof(UpdateRequest))]
[JsonSerializable(typeof(object))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
