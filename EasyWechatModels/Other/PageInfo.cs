using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Other
{
    /// <summary>
    /// 分页信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInfo<T>
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Total { get; private set; }

        public IEnumerable<T> Data { get; private set; }

        public PageInfo(int pageIndex, int pageSize, long count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = count;
            Data = data;
        }
    }
}
