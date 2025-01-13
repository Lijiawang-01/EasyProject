using EasyWechatModels.Other;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Base
{
    /// <summary>
    /// 服务基类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>
    {
        /// <summary>
        /// 异步执行查询SQL语句
        /// 只支持查询操作，并且支持拉姆达分页
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> ExecuteSqlAsync(string sql);
        /// <summary>
        /// 同步执行查询SQL语句
        /// 只支持查询操作，并且支持拉姆达分页
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> ExecuteSql(string sql);
        /// <summary>
        /// 异步通过Ado方法执行SQL语句
        /// 支持任何SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="whereObj">参数</param>
        /// <returns></returns>
        Task<List<T>> ExecuteAllSqlAsync(string sql, object whereObj = null);
        /// <summary>
        /// 同步通过Ado方法执行SQL语句
        /// 支持任何SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="whereObj">参数</param>
        /// <returns></returns>
        List<T> ExecuteAllSql(string sql, object whereObj = null);
        /// <summary>
        /// 异步根据ID查询一条数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(object objId);
        /// <summary>
        /// 同步根据ID查询一条数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        T GetById(object objId);
        /// <summary>
        /// 异步根据IDs查询数据
        /// </summary>
        /// <param name="lstIds">id列表</param>
        /// <returns>数据实体列表</returns>
        Task<List<T>> GetByIdsAsync(List<object> lstIds);
        /// <summary>
        /// 同步根据IDs查询数据
        /// </summary>
        /// <param name="lstIds">id列表</param>
        /// <returns>数据实体列表</returns>
        List<T> GetByIds(List<object> lstIds);
        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<int> AddAsync(T entity);
        /// <summary>
        /// 同步插入实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        int Add(T entity);
        /// <summary>
        /// 异步批量插入实体
        /// </summary>
        /// <param name="lisT">实体集合</param>
        /// <returns>影响行数</returns>
        Task<int> AddListAsync(List<T> lisT);
        /// <summary>
        /// 同步批量插入实体
        /// </summary>
        /// <param name="lisT">实体集合</param>
        /// <returns>影响行数</returns>
        int AddList(List<T> lisT);
        /// <summary>
        /// 异步更新实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);
        /// <summary>
        /// 同步更新实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Update(T entity);
        /// <summary>
        /// 异步批量更新实体
        /// </summary>
        /// <param name="lisT">实体类</param>
        /// <returns></returns>
        Task<int> UpdateListAsync(List<T> lisT);
        /// <summary>
        /// 同步批量更新实体
        /// </summary>
        /// <param name="lisT">实体类</param>
        /// <returns></returns>
        int UpdateList(List<T> lisT);
        /// <summary>
        /// 异步根据实体删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);
        /// <summary>
        /// 同步根据实体删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// 异步根据实体集合批量删除数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<bool> DeleteListAsync(List<T> models);
        /// <summary>
        /// 同步根据实体集合批量删除数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        bool DeleteList(List<T> models);
        /// <summary>
        /// 异步根据ID删除数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(object id);
        /// <summary>
        /// 同步根据ID删除数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        bool DeleteById(object id);
        /// <summary>
        /// 异步根据IDs批量删除数据
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(List<object> ids);
        /// <summary>
        /// 同步根据IDs批量删除数据
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        bool DeleteByIds(List<object> ids);
        /// <summary>
        /// 异步根据条件查询数据是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<bool> GetAnyByFilterAsync(Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 同步根据条件查询数据是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool GetAnyByFilter(Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 异步根据条件查询一条数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<T> GetSingleByFilterAsync(Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 同步根据条件查询一条数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        T GetSingleByFilter(Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 异步查询所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync();
        /// <summary>
        /// 同步查询所有数据
        /// </summary>
        /// <returns></returns>
        List<T> GetList();
        /// <summary>
        /// 异步查询数据列表----按条件表达式
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        Task<List<T>> GetQueryListAsync(Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 查询数据列表----按条件表达式
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        List<T> GetQueryList(Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 异步查询数据列表----按条件表达式、排序表达式
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">是否升序排序</param>
        /// <returns></returns>
        Task<List<T>> GetQueryOrderListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool isAsc = true);
        /// <summary>
        /// 同步查询数据列表----按条件表达式、排序表达式
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">是否升序排序</param>
        /// <returns></returns>
        List<T> GetQueryOrderList(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool isAsc = true);
        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderDescSelector">排序字段</param>
        /// <returns></returns>
        Task<PageInfo<TResult>> GetSelectorPageDataAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, Expression<Func<T, object>> orderDescSelector = null);
        /// <summary>
        /// 同步分页查询
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderDescSelector">排序字段</param>
        /// <returns></returns>
        PageInfo<TResult> GetSelectorPageData<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, Expression<Func<T, object>> orderDescSelector = null);
        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderDescSelector">排序字段</param>
        /// <returns></returns>
        Task<PageInfo<T>> GetPageDataAsync(Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, Expression<Func<T, object>> orderDescSelector = null);
        /// <summary>
        /// 同步分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderDescSelector">排序字段</param>
        /// <returns></returns>
        PageInfo<T> GetPageData(Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, Expression<Func<T, object>> orderDescSelector = null);
    }
}
