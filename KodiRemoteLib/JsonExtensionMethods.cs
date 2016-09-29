using BLTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiRemoteLib {
  public static class JsonExtensionMethods {

    public static string ToJson(this Dictionary<string, object> source) {
      if (source == null || source.Count == 0) {
        return "{}";
      }

      StringBuilder RetVal = new StringBuilder("{");

      foreach (KeyValuePair<string, object> KVPItem in source) {

        if (KVPItem.Value.GetType().IsGenericType) {
          RetVal.Append($"\"{KVPItem.Key}\" : ");
          RetVal.Append((KVPItem.Value as Dictionary<string, object>).ToJson());
          RetVal.Append(", ");
          continue;
        }

        switch (KVPItem.Value.GetType().Name.ToLower()) {
          case "int32":
            RetVal.Append($"\"{KVPItem.Key}\" : {KVPItem.Value}, ");
            break;
          case "boolean":
          case "bool":
            RetVal.Append($"\"{KVPItem.Key}\" : {KVPItem.Value.ToString().ToLower()}, ");
            break;
          default:
            RetVal.Append($"\"{KVPItem.Key}\" : \"{KVPItem.Value}\", ");
            break;
        }

      }
      RetVal.Truncate(2);
      RetVal.Append("}");
      return RetVal.ToString();
    }
  }
}
