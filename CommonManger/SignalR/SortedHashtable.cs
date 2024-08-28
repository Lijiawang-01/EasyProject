using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.SignalR
{
    /// <summary>
    /// 可以排序的Hashtable.
    /// </summary>
    public class SortedHashtable : Hashtable
    {
        private ArrayList keys = new ArrayList();

        public SortedHashtable()
        {

        }


        public override void Add(object key, object value)
        {
            base.Add(key, value);
            keys.Add(key);
        }

        /// <summary>
        /// 插入key到指定位置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="keyIndex"></param>
        public void Insert(object key, object value, int keyIndex)
        {
            base.Add(key, value);
            keys.Insert(keyIndex, key);
        }

        public override ICollection Keys
        {
            get
            {
                return keys;
            }
        }

        public override void Clear()
        {
            base.Clear();
            keys.Clear();
        }

        /// <summary>
        /// 复制一个新的SortedHashtable
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            SortedHashtable result = new SortedHashtable();

            foreach (var keyStr in keys)
            {
                var value = this[keyStr];
                result.Add(keyStr, value);
            }

            return result;
        }

        public override void Remove(object key)

        {
            base.Remove(key);
            keys.Remove(key);
        }
        public override IDictionaryEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }

        /// <summary>
        /// 将所有的Key转换为string[]
        /// </summary>
        /// <returns></returns>
        public string[] KeyToArray()
        {
            string[] result = new string[keys.Count];

            keys.CopyTo(result);

            return result;
        }

    }
}
