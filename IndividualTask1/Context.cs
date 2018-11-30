using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class Context
    {
        Dictionary<string, Expression> variables;

        public Context()
        {
            variables = new Dictionary<string, Expression>();
        }
        // получаем значение переменной по ее имени
        public Expression GetVariable(string name)
        {
            return variables[name];
        }

        public void SetVariable(string name, int value)
        {
            if (variables.ContainsKey(name))
                variables[name] = Expression.Constant(value); //Expression.Variable 
            else variables.Add(name, Expression.Constant(value));
        }
    }
}
