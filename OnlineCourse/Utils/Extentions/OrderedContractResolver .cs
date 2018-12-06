using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OnlineCourse.Panel.Utils.Extentions
{
    public class OrderedContractResolver : DefaultContractResolver
    {
        protected override System.Collections.Generic.IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
        {
            var ret= base.CreateProperties(type, memberSerialization).OrderBy(p => Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(p.PropertyName))).ToList();
            return ret;
        }
    }
}
