using AutoMapper;
using CommonManager.Base.IService;
using CommonManager.Helper;
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
    //通用仓储实现
    //表示T必须是引用类型（而不是int等值类型，或者ValueType的其他子类），new()表示这个类型T还必须至少具备一个无参的构造函数（还可以有其他有参数的构造函数）。
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        public IMapper _mapper { get; set; }
        public ISqlSugarClient _db { get; set; }
        /// <summary>
        /// 异步执行查询SQL语句
        /// 只支持查询操作，并且支持拉姆达分页
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> ExecuteSqlAsync(string sql)
        {
            //return await _db.AsTenant().GetConnection(1).SqlQueryable<T>(sql).ToListAsync();
            return await _db.SqlQueryable<T>(sql).ToListAsync();
        }
        /// <summary>
        /// 同步执行查询SQL语句
        /// 只支持查询操作，并且支持拉姆达分页
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> ExecuteSql(string sql)
        {
            return _db.SqlQueryable<T>(sql).ToList();
        }
        /// <summary>
        /// 异步通过Ado方法执行SQL语句
        /// 支持任何SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="whereObj">参数</param>
        /// <returns></returns>
        public async Task<List<T>> ExecuteAllSqlAsync(string sql, object whereObj = null)
        {
            _db.Aop.DataExecuting = (oldValue, entityInfo) =>
            {

                /*** 列级别事件：插入的每个列都会进事件 ***/
                if (entityInfo.PropertyName == "CreateTime" && entityInfo.OperationType == DataFilterType.InsertByObject)
                {
                    entityInfo.SetValue(DateTime.Now);//修改CreateTime字段

                    /*entityInfo有字段所有参数*/

                    /*oldValue表示当前字段值 等同于下面写法*/
                    //var value=entityInfo.EntityColumnInfo.PropertyInfo.GetValue(entityInfo.EntityValue);

                    /*获取当前列特性*/
                    //5.1.3.23 +
                    //entityInfo.IsAnyAttribute<特性>()
                    //entityInfo.GetAttribute<特性>()
                }


                /*** 行级别事件 ：一条记录只会进一次 ***/
                if (entityInfo.EntityColumnInfo.IsPrimarykey)
                {
                    //entityInfo.EntityValue 拿到单条实体对象
                }

                //可以写多个IF

            };
            return await Task.Run(() => _db.Ado.SqlQuery<T>(sql, whereObj));
        }

        /// <summary>
        /// 同步通过Ado方法执行SQL语句
        /// 支持任何SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="whereObj">参数</param>
        /// <returns></returns>
        public List<T> ExecuteAllSql(string sql, object whereObj = null)
        {
            _db.Aop.DataExecuting = (oldValue, entityInfo) =>
            {

                /*** 列级别事件：插入的每个列都会进事件 ***/
                if (entityInfo.PropertyName == "CreateTime" && entityInfo.OperationType == DataFilterType.InsertByObject)
                {
                    entityInfo.SetValue(DateTime.Now);//修改CreateTime字段

                    /*entityInfo有字段所有参数*/

                    /*oldValue表示当前字段值 等同于下面写法*/
                    //var value=entityInfo.EntityColumnInfo.PropertyInfo.GetValue(entityInfo.EntityValue);

                    /*获取当前列特性*/
                    //5.1.3.23 +
                    //entityInfo.IsAnyAttribute<特性>()
                    //entityInfo.GetAttribute<特性>()
                }


                /*** 行级别事件 ：一条记录只会进一次 ***/
                if (entityInfo.EntityColumnInfo.IsPrimarykey)
                {
                    //entityInfo.EntityValue 拿到单条实体对象
                }

                //可以写多个IF

            };
            return _db.Ado.SqlQuery<T>(sql, whereObj);
        }
        /// <summary>
        /// 异步根据ID查询一条数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(object objId)
        {
            //主库查询
            return await _db.Queryable<T>().In(objId).SingleAsync();
            //从库查询
            // return await _db.SlaveQueryable<T>().In(objId).SingleAsync();
        }

        /// <summary>
        /// 同步根据ID查询一条数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public T GetById(object objId)
        {
            //主库查询
            return _db.Queryable<T>().In(objId).Single();
            //从库查询
            // return await _db.SlaveQueryable<T>().In(objId).SingleAsync();
        }
        /// <summary>
        /// 异步根据IDs查询数据
        /// </summary>
        /// <param name="lstIds">id列表</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<T>> GetByIdsAsync(List<object> lstIds)
        {
            return await _db.Queryable<T>().In(lstIds).ToListAsync();
        }

        /// <summary>
        /// 同步根据IDs查询数据
        /// </summary>
        /// <param name="lstIds">id列表</param>
        /// <returns>数据实体列表</returns>
        public List<T> GetByIds(List<object> lstIds)
        {
            return _db.Queryable<T>().In(lstIds).ToList();
        }
        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<int> AddAsync(T entity)
        {
            var insert = _db.Insertable(entity);
            return await insert.ExecuteCommandAsync();
        }
        /// <summary>
        /// 同步插入实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public int Add(T entity)
        {
            var insert = _db.Insertable(entity);
            return insert.ExecuteCommand();
        }
        /// <summary>
        /// 异步批量插入实体
        /// </summary>
        /// <param name="lisT">实体集合</param>
        /// <returns>影响行数</returns>
        public async Task<int> AddListAsync(List<T> lisT)
        {
            return await _db.Insertable(lisT.ToArray()).ExecuteCommandAsync();
        }
        /// <summary>
        /// 同步批量插入实体
        /// </summary>
        /// <param name="lisT">实体集合</param>
        /// <returns>影响行数</returns>
        public int AddList(List<T> lisT)
        {
            return _db.Insertable(lisT.ToArray()).ExecuteCommand();
        }
        /// <summary>
        /// 异步更新实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 同步更新实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            return _db.Updateable(entity).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 异步批量更新实体
        /// </summary>
        /// <param name="lisT">实体类</param>
        /// <returns></returns>
        public async Task<int> UpdateListAsync(List<T> lisT)
        {
            return await _db.Updateable(lisT).ExecuteCommandAsync();
        }
        /// <summary>
        /// 同步批量更新实体
        /// </summary>
        /// <param name="lisT">实体类</param>
        /// <returns></returns>
        public int UpdateList(List<T> lisT)
        {
            return _db.Updateable(lisT).ExecuteCommand();
        }
        /// <summary>
        /// 异步根据实体删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            return await _db.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 同步根据实体删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            return _db.Deleteable(entity).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 异步根据实体集合批量删除数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> DeleteListAsync(List<T> models)
        {
            return await _db.Deleteable(models).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 同步根据实体集合批量删除数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool DeleteList(List<T> models)
        {
            return _db.Deleteable(models).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 异步根据ID删除数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(object id)
        {
            return await _db.Deleteable<T>(id).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 同步根据ID删除数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            return _db.Deleteable<T>(id).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 异步根据IDs批量删除数据
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsAsync(List<object> ids)
        {
            return await _db.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 同步根据IDs批量删除数据
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        public bool DeleteByIds(List<object> ids)
        {
            return _db.Deleteable<T>().In(ids).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 异步根据条件查询数据是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<bool> GetAnyByFilterAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _db.Queryable<T>().AnyAsync(whereExpression);
        }
        /// <summary>
        /// 同步根据条件查询数据是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool GetAnyByFilter(Expression<Func<T, bool>> whereExpression)
        {
            return _db.Queryable<T>().Any(whereExpression);
        }
        /// <summary>
        /// 异步根据条件查询一条数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<T> GetSingleByFilterAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _db.Queryable<T>().FirstAsync(whereExpression);
        }
        /// <summary>
        /// 同步根据条件查询一条数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public T GetSingleByFilter(Expression<Func<T, bool>> whereExpression)
        {
            return _db.Queryable<T>().First(whereExpression);
        }
        /// <summary>
        /// 异步查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync()
        {
            return await _db.Queryable<T>().ToListAsync();
        }
        /// <summary>
        /// 同步查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            return _db.Queryable<T>().ToList();
        }
        /// <summary>
        /// 异步查询数据列表----按条件表达式
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        public async Task<List<T>> GetQueryListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }
        /// <summary>
        /// 查询数据列表----按条件表达式
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        public List<T> GetQueryList(Expression<Func<T, bool>> whereExpression)
        {
            return _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).ToList();
        }
        /// <summary>
        /// 异步查询数据列表----按条件表达式、排序表达式
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">是否升序排序</param>
        /// <returns></returns>
        public async Task<List<T>> GetQueryOrderListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool isAsc = true)
        {
            return await _db.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }
        /// <summary>
        /// 同步查询数据列表----按条件表达式、排序表达式
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">是否升序排序</param>
        /// <returns></returns>
        public List<T> GetQueryOrderList(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool isAsc = true)
        {
            return _db.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList();
        }
        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderDescSelector">排序字段</param>
        /// <returns></returns>
        public async Task<PageInfo<TResult>> GetSelectorPageDataAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, Expression<Func<T, object>> orderDescSelector = null)
        {
            var query = _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression);
            query = query.OrderByIF(orderDescSelector != null, orderDescSelector);
            var totalCount = 0;
            var results = query.Select(selector).ToPageList(intPageIndex, intPageSize, ref totalCount).ToList();

            var basePage = new PageInfo<TResult>(intPageIndex, intPageSize, totalCount, results);

            return await Task.FromResult(basePage);
        }
        /// <summary>
        /// 同步分页查询
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderDescSelector">排序字段</param>
        /// <returns></returns>
        public PageInfo<TResult> GetSelectorPageData<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, Expression<Func<T, object>> orderDescSelector = null)
        {
            var query = _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression);
            query = query.OrderByIF(orderDescSelector != null, orderDescSelector);
            var totalCount = 0;
            var results = query.Select(selector).ToPageList(intPageIndex, intPageSize, ref totalCount).ToList();

            var basePage = new PageInfo<TResult>(intPageIndex, intPageSize, totalCount, results);

            return basePage;
        }
        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderDescSelector">排序字段</param>
        /// <returns></returns>
        public async Task<PageInfo<T>> GetPageDataAsync(Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, Expression<Func<T, object>> orderDescSelector = null)
        {
            var query = _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression);
            query = query.OrderByIF(orderDescSelector != null, orderDescSelector);
            var totalCount = 0;
            var results = query.ToPageList(intPageIndex, intPageSize, ref totalCount).ToList();

            var basePage = new PageInfo<T>(intPageIndex, intPageSize, totalCount, results);

            return await Task.FromResult(basePage);
        }
        /// <summary>
        /// 同步分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderDescSelector">排序字段</param>
        /// <returns></returns>
        public PageInfo<T> GetPageData(Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, Expression<Func<T, object>> orderDescSelector = null)
        {
            var query = _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression);
            query = query.OrderByIF(orderDescSelector != null, orderDescSelector);
            var totalCount = 0;
            var results = query.ToPageList(intPageIndex, intPageSize, ref totalCount).ToList();

            var basePage = new PageInfo<T>(intPageIndex, intPageSize, totalCount, results);

            return basePage;
        }

        /// <summary>
        /// 当前db
        /// </summary>
        public void CurrentBeginTran()
        {
            _db.Ado.BeginTran();
        }

        /// <summary>
        /// 当前db
        /// </summary>
        public void CurrentCommitTran()
        {
            _db.Ado.CommitTran();
        }

        /// <summary>
        /// 当前db
        /// </summary>
        public void CurrentRollbackTran()
        {
            _db.Ado.RollbackTran();
        }

        /// <summary>
        /// 所有db
        /// </summary>
        public void BeginTran()
        {
            _db.AsTenant().BeginTran();
        }

        /// <summary>
        /// 所有db
        /// </summary>
        public void CommitTran()
        {
            _db.AsTenant().CommitTran();
        }

        /// <summary>
        /// 所有db
        /// </summary>
        public void RollbackTran()
        {
            _db.AsTenant().RollbackTran();
        }
    }
}
