using YamlDotNet.Serialization;

namespace ApplicationCore.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToYaml(this object item)
        {
            var serializer = new SerializerBuilder().Build();
            return serializer.Serialize(item);
        }
    }
}