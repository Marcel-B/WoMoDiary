using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.Android
{
    public static class CastJavaObject
    {
        public static T Cast<T>(Java.Lang.Object obj) where T : Place
        {
            var propInfo = obj.GetType().GetProperty("Instance");
            return propInfo == null ? null : propInfo.GetValue(obj, null) as T;
        }
    }
}