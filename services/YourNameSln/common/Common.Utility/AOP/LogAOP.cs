using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Common.Utility.Extension;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;

namespace Common.Utility.AOP
{
    public class LogAOP : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var logInfoAop = new AopLogInfoModel() { 
                ProxyInterface= invocation.Method.DeclaringType.FullName,
                InvocationService= invocation.MethodInvocationTarget.DeclaringType.FullName,
                Method= invocation.MethodInvocationTarget.Name,
                Arguments= invocation.Arguments.Serialize(),
            };
            //在被拦截的方法执行完毕后 继续执行当前方法，注意是被拦截的是异步的
            //执行被拦截的方法之前
            MiniProfiler.Current.Step($"开始执行Service方法：{logInfoAop.InvocationService}.{logInfoAop.Method}() -> ");
            invocation.Proceed();
            MiniProfiler.Current.Step($"完成执行方法：{logInfoAop.InvocationService}.{logInfoAop.Method}() -> ");
            //执行被拦截的方法之后

            var type = invocation.Method.ReturnType;
            if (typeof(Task).IsAssignableFrom(type))
            {
                var resultProperty = type.GetProperty("Result");
                var taskStatus = (TaskStatus)((dynamic)invocation.ReturnValue).Status;
                if (taskStatus != TaskStatus.Faulted && taskStatus != TaskStatus.WaitingForActivation)
                {//当异常的时候不获取值否则会报错
                    var responesData = resultProperty.GetValue(invocation.ReturnValue);
                    logInfoAop.ReturnValue = responesData.Serialize();
                }
            }
            else
            {
                logInfoAop.ReturnValue = invocation.ReturnValue.Serialize();
            }
            logInfoAop.ReturnType = type.ToString();
            typeof(LogAOP).Logger().LogInfo(logInfoAop);
        }
    }
}
