using EasyWechatModels.Other;
using System.Linq.Expressions;
using System.Reflection;

namespace CommonManager.Utity
{
    /// <summary>
    /// 查询条件帮助类
    /// </summary>
    public static class PageParamHelper
    {
        public static Expression<Func<TSource, bool>> GetIndividualPropertySearch<TSource>(PageParam pageObj)
        {
            Type type = typeof(TSource);
            PropertyInfo[] properties = type.GetProperties();
            var paramExpr = Expression.Parameter(typeof(TSource), "val");
            Expression whereExpr = Expression.Constant(true);
            #region 页面查询条件
            foreach (var item in pageObj.ParamData)
            {

                string searchKey = item.Key;

                string searchValue = item.Value.ToString().ToLower();

                string dateEqual = "equal";

                if (searchKey.Contains("_begin"))
                {
                    searchKey = searchKey.Replace("_begin", "");
                    dateEqual = "morethan";

                    searchValue = DateTime.Parse(searchValue).ToString("yyyy-MM-dd");
                }
                else if (searchKey.Contains("_end"))
                {
                    searchKey = searchKey.Replace("_end", "");
                    dateEqual = "lessthan";

                    searchValue = DateTime.Parse(searchValue).AddDays(1).ToString("yyyy-MM-dd");
                }
                searchValue = searchValue.Trim();
                //根据searchKey查询配置文件中的字段名和字段类型
                if (!properties.Any(x => x.Name == searchKey))
                {
                    continue;
                }
                bool isNum = false;
                bool isDate = false;
                Expression exprPty = null;
                var pty = properties.Where(x => x.Name == searchKey).FirstOrDefault();
                if (pty == null)
                    continue;
                if (pty.PropertyType == typeof(int) || pty.PropertyType == typeof(double))
                {
                    isNum = true;
                }
                else if (pty.PropertyType == typeof(DateTime))
                {
                    isDate = true;
                }

                exprPty = Expression.Property(paramExpr, pty);

                if (isNum)
                {
                    // val.{PropertyName}.Equals({query})
                    // val.{t1}.{PropertyName}.Equals({query})

                    var toStringCall = Expression.Call(
                                        Expression.Call(
                                            exprPty, "ToString", new Type[0]),
                                        typeof(string).GetMethod("ToLower", new Type[0]));

                    // reset where expression to also require the current contraint
                    whereExpr = Expression.AndAlso(whereExpr,
                                               Expression.Equal(toStringCall, Expression.Constant(searchValue)));
                }
                else if (isDate)
                {
                    if (dateEqual == "morethan")
                    {
                        // reset where expression to also require the current contraint
                        whereExpr = Expression.AndAlso(whereExpr,
                                                   Expression.GreaterThanOrEqual(exprPty, Expression.Constant(DateTime.Parse(searchValue))));
                    }
                    else if (dateEqual == "lessthan")
                    {
                        // reset where expression to also require the current contraint
                        whereExpr = Expression.AndAlso(whereExpr,
                                                   Expression.LessThan(exprPty, Expression.Constant(DateTime.Parse(searchValue))));
                    }
                    else
                    {
                        var toStringCall = Expression.Call(
                                        Expression.Call(
                                            exprPty, "ToString", new Type[0]),
                                        typeof(string).GetMethod("ToLower", new Type[0]));

                        // reset where expression to also require the current contraint
                        whereExpr = Expression.AndAlso(whereExpr,
                                                   Expression.Equal(toStringCall, Expression.Constant(searchValue)));
                    }
                }
                else
                {
                    var toStringCall = Expression.Call(Expression.Call(exprPty, "ToString", new Type[0]), typeof(string).GetMethod("ToLower", new Type[0]));
                    // reset where expression to also require the current contraint
                    whereExpr = Expression.AndAlso(whereExpr,
                                               Expression.Call(toStringCall,
                                                               typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                                                               Expression.Constant(searchValue)));
                }
            }
            #endregion

            return Expression.Lambda<Func<TSource, bool>>(whereExpr, paramExpr);
        }

        public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
        {
            return first.AndAlso<T>(second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.AndAlso<T>(second, Expression.OrElse);
        }

        private static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2,
        Func<Expression, Expression, BinaryExpression> func)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                func(left, right), parameter);
        }

        private class ReplaceExpressionVisitor
            : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }
}
