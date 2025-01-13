using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        public static Array GetValues(Type enumType)
        {
            return Enum.GetValues(enumType);
        }
        #region 静态方法
        public static Dictionary<string, string> GetEnumDescription<T>()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            FieldInfo[] fields = typeof(T).GetFields();

            foreach (FieldInfo field in fields)
            {

                if (field.FieldType.IsEnum)
                {

                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;

                    dic.Add(field.Name, description);

                }
            }

            return dic;
        }

        /// <summary>        
        /// 获取对应的枚举描述        
        /// </summary>        
        public static List<KeyValuePair<string, string>> GetEnumDescriptionList<T>()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            FieldInfo[] fields = typeof(T).GetFields();

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {

                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;

                    result.Add(new KeyValuePair<string, string>(field.Name, description));

                }

            }
            return result;
        }


        /// <summary>
        /// 获取枚举的 值和描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetEnumValueDescriptionList<T>()
        {
            List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                    result.Add(new KeyValuePair<int, string>(Convert.ToInt32(field.GetValue(null)), description));
                }
            }

            return result;
        }

        public static string GetDescriptionByEnumName<T>(string name)
        {
            try
            {
                Dictionary<string, string> dic = GetEnumDescription<T>();
                string description = dic[name];
                return description;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
    }
}
