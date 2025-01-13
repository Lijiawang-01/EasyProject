using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    /// <summary>
    /// 反射类
    /// </summary>
    public class ReflectionClass
    {
        private object m_rowObj = null;

        private Type m_typeObj = null;

        public ReflectionClass(object rowObj)
        {
            m_rowObj = rowObj;
            m_typeObj = rowObj.GetType();
        }

        public object GetPValue(string pName)
        {
            object value = DBNull.Value;
            PropertyInfo[] properties = m_typeObj.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name.ToUpper() == pName.ToUpper())
                {
                    value = propertyInfo.GetValue(m_rowObj, null);
                    if (value == null || value.Equals(DateTime.MaxValue) || value.Equals(int.MaxValue) || value.Equals(decimal.MaxValue) || value.Equals(double.MaxValue))
                    {
                        value = DBNull.Value;
                    }

                    break;
                }
            }

            return value;
        }

        public void SetPValue(string pName, object pValue, bool replaceHTML = true)
        {
            PropertyInfo[] properties = m_typeObj.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!(propertyInfo.Name.ToUpper() == pName.ToUpper()) || propertyInfo.PropertyType.ToString().IndexOf("Eps") >= 0 || propertyInfo.PropertyType.ToString().IndexOf("IList") >= 0)
                {
                    continue;
                }

                Type propertyType = propertyInfo.PropertyType;
                if ((propertyType != typeof(string) && pValue == null) || (propertyType != typeof(string) && pValue.ToString() == ""))
                {
                    continue;
                }

                MethodInfo setMethod = propertyInfo.GetSetMethod();
                if (!(setMethod != null))
                {
                    break;
                }

                try
                {
                    Type type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                    if (type == typeof(string) && replaceHTML)
                    {
                        pValue = SecurityHelper.SafeHtmlFragment(pValue.ToString());
                    }

                    propertyInfo.SetValue(m_rowObj, Convert.ChangeType(pValue, type), null);
                }
                catch (Exception)
                {
                    throw new Exception("ReflectionClass.SetPValue报错：为实体类" + m_rowObj?.ToString() + "中的" + propertyInfo.Name + "字段赋值时，将" + pValue?.ToString() + "转换为" + propertyType.Name);
                }

                break;
            }
        }

        public static object GetPValue(object orgModel, string pName)
        {
            if (orgModel == null)
            {
                throw new Exception("GetPValue 对象不能为空");
            }

            if (pName == null)
            {
                throw new Exception("GetPValue  属性不能为空");
            }

            object obj = null;
            Type type = orgModel.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name.ToUpper() == pName.ToUpper())
                {
                    obj = propertyInfo.GetValue(orgModel, null);
                    if (obj == null || obj.Equals(DateTime.MaxValue) || obj.Equals(int.MaxValue) || obj.Equals(decimal.MaxValue) || obj.Equals(double.MaxValue))
                    {
                        obj = DBNull.Value;
                    }

                    break;
                }
            }

            if (obj == null)
            {
                obj = DBNull.Value;
            }

            return obj;
        }

        public static void SetPValue(object orgModel, string pName, object pValue, bool replaceHTML = true)
        {
            if (orgModel == null)
            {
                throw new Exception("SetPValue 对象不能为空");
            }

            if (pName == null)
            {
                throw new Exception("SetPValue  属性不能为空");
            }

            Type type = orgModel.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!(propertyInfo.Name.ToUpper() == pName.ToUpper()))
                {
                    continue;
                }

                Type propertyType = propertyInfo.PropertyType;
                if ((propertyType != typeof(string) && pValue == null) || (propertyType != typeof(string) && pValue.ToString() == ""))
                {
                    continue;
                }

                MethodInfo setMethod = propertyInfo.GetSetMethod();
                if (!(setMethod != null))
                {
                    break;
                }

                try
                {
                    Type type2 = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                    if (type2 == typeof(string) && replaceHTML)
                    {
                        pValue = SecurityHelper.SafeHtmlFragment(pValue.ToString());
                    }

                    propertyInfo.SetValue(orgModel, Convert.ChangeType(pValue, type2), null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                break;
            }
        }

        public static void GetObjectValueByDataRow(object desObj, DataRow rowObj)
        {
            if (desObj == null)
            {
                throw new Exception("GetObjectValueByDataRow");
            }

            if (rowObj == null)
            {
                throw new Exception("GetObjectValueByDataRow");
            }

            ReflectionClass reflectionClass = new ReflectionClass(desObj);
            for (int i = 0; i < rowObj.Table.Columns.Count; i++)
            {
                DataColumn dataColumn = rowObj.Table.Columns[i];
                string pValue = rowObj[dataColumn.ColumnName]?.ToString() ?? "";
                reflectionClass.SetPValue(dataColumn.ColumnName, (object)pValue, replaceHTML: false);
            }
        }

        public static DataTable GetDataTableByObject(object sourceObj)
        {
            if (sourceObj == null)
            {
                throw new Exception("GetDataTableByObject");
            }

            if (sourceObj == null)
            {
                throw new Exception("GetDataTableByObject");
            }

            ReflectionClass reflectionClass = new ReflectionClass(sourceObj);
            DataTable dataTable = new DataTable(sourceObj.GetType().FullName);
            PropertyInfo[] properties = sourceObj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!propertyInfo.PropertyType.FullName!.ToString().Contains("List"))
                {
                    dataTable.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
                }
            }

            DataRow dataRow = dataTable.NewRow();
            PropertyInfo[] properties2 = sourceObj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo2 in properties2)
            {
                if (!propertyInfo2.PropertyType.FullName!.ToString().Contains("List"))
                {
                    dataRow[propertyInfo2.Name] = reflectionClass.GetPValue(propertyInfo2.Name);
                }
            }

            dataTable.Rows.Add(dataRow);
            return dataTable;
        }

        public static void GetObjectValueByObject(object desObj, object orgObj, string desObjTip = "", string orgObjTip = "")
        {
            if (desObj == null)
            {
                throw new Exception("GetObjectValueByObject");
            }

            if (orgObj == null)
            {
                throw new Exception("GetObjectValueByObject");
            }

            ReflectionClass reflectionClass = new ReflectionClass(desObj);
            ReflectionClass reflectionClass2 = new ReflectionClass(orgObj);
            PropertyInfo[] properties = desObj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                string text = propertyInfo.Name;
                if (!string.IsNullOrEmpty(desObjTip))
                {
                    text = text.Replace(desObjTip, "");
                }

                object pValue = reflectionClass2.GetPValue(text);
                if (pValue != DBNull.Value)
                {
                    string pValue2 = pValue.ToString();
                    string text2 = propertyInfo.Name;
                    if (!string.IsNullOrEmpty(orgObjTip))
                    {
                        text2 = text2.Replace(orgObjTip, "");
                    }

                    reflectionClass.SetPValue(text2, (object)pValue2, replaceHTML: false);
                }
            }
        }

        public static void GetDynamicObjectValueByFormObject(dynamic desObj, NameValueCollection orgObj)
        {
            if (desObj == null)
            {
                throw new Exception("GetDynamicObjectValueByFormObject");
            }

            if (orgObj == null)
            {
                throw new Exception("GetDynamicObjectValueByFormObject");
            }

            dynamic val = desObj.Properties;
            string[] allKeys = orgObj.AllKeys;
            foreach (string text in allKeys)
            {
                if (string.IsNullOrEmpty(text))
                {
                    continue;
                }

                string[] values = orgObj.GetValues(text);
                if (values == null)
                {
                    continue;
                }

                object obj;
                if (values.Length <= 1)
                {
                    obj = SecurityHelper.SafeHtmlFragment(orgObj[text]);
                }
                else
                {
                    for (int j = 0; j < values.Length; j++)
                    {
                        values[j] = SecurityHelper.SafeHtmlFragment(values[j]);
                    }

                    obj = values;
                }

                val[text] = obj;
            }
        }

        public static void ClearDynamicObjectValue(dynamic desObj)
        {
            if (desObj == null)
            {
                throw new Exception("ClearDynamicObjectValue");
            }

            Dictionary<string, object> dictionary = desObj.Properties;
            for (int num = dictionary.Count - 1; num >= 0; num--)
            {
                dictionary.Remove(dictionary.ElementAt(num).Key);
            }
        }

        public static void GetDynamicObjectValueByObject(dynamic desObj, object orgObj)
        {
            if (desObj == null)
            {
                throw new Exception("GetDynamicObjectValueByObject");
            }

            if (orgObj == null)
            {
                throw new Exception("GetDynamicObjectValueByObject");
            }

            dynamic val = desObj.Properties;
            ReflectionClass reflectionClass = new ReflectionClass(orgObj);
            PropertyInfo[] properties = orgObj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.PropertyType.ToString().IndexOf("Eps") < 0 && propertyInfo.PropertyType.ToString().IndexOf("IList") < 0)
                {
                    object pValue = reflectionClass.GetPValue(propertyInfo.Name);
                    val[propertyInfo.Name] = pValue;
                }
            }
        }

        public static void GetDynamicObjectValueByDynamicObject(dynamic desObj, dynamic orgObj)
        {
            if (desObj == null)
            {
                throw new Exception("GetDynamicObjectValueByDynamicObject");
            }

            if (orgObj == null)
            {
                throw new Exception("GetDynamicObjectValueByDynamicObject");
            }

            dynamic val = orgObj.Properties;
            object obj = desObj.Properties;
            foreach (dynamic item in val)
            {
                object obj2 = val[item.Key];
                desObj.SetValue(item.Key, obj2);
            }
        }

        public static void GetDynamicObjectValueByDataRow(dynamic desObj, DataRow orgObj)
        {
            if (desObj == null)
            {
                throw new Exception("GetDynamicObjectValueByDataRow");
            }

            if (orgObj == null)
            {
                throw new Exception("GetDynamicObjectValueByDataRow");
            }

            DataColumnCollection columns = orgObj.Table.Columns;
            object obj = desObj.Properties;
            foreach (DataColumn item in columns)
            {
                object obj2 = orgObj[item.ColumnName];
                desObj.SetValue(item.ColumnName, obj2);
            }
        }

        public static void GetDynamicObjectValueByHashtable(dynamic desObj, Hashtable orgObj)
        {
            if (desObj == null)
            {
                throw new Exception("GetDynamicObjectValueByHashtable");
            }

            if (orgObj == null)
            {
                throw new Exception("GetDynamicObjectValueByHashtable");
            }

            dynamic val = desObj.Properties;
            ReflectionClass reflectionClass = new ReflectionClass(orgObj);
            IDictionaryEnumerator enumerator = orgObj.GetEnumerator();
            while (enumerator.MoveNext())
            {
                val[enumerator.Key.ToString()] = enumerator.Value;
            }
        }
    }
}
