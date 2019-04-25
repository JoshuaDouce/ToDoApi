using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDoApi.Infrastructure
{
    public interface ISearchExpressionProvider
    {
        ConstantExpression GetValue(string input);
    }
}
