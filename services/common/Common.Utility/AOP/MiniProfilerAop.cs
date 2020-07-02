using Castle.DynamicProxy;
using Common.Utility.AOP;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AOP
{
    public class MiniProfilerAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var logInfoAop = new AopLogInfoModel()
            {
                InvocationService = invocation.MethodInvocationTarget.DeclaringType.FullName,
                Method = invocation.MethodInvocationTarget.Name,
            };
            MiniProfiler.Current.Step($"开始执行Service方法：{logInfoAop.InvocationService}.{logInfoAop.Method}() -> ");
            invocation.Proceed();
            MiniProfiler.Current.Step($"完成执行方法：{logInfoAop.InvocationService}.{logInfoAop.Method}() -> ");
        }
    }
}
