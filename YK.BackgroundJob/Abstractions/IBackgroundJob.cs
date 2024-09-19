using System.Linq.Expressions;

namespace YK.BackgroundJob.Abstractions;

public interface IBackgroundJob
{
    #region 循环任务
    
    void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, Func<string> cronExpression);

    void AddOrUpdate(string recurringJobId, string queue, Expression<Action> methodCall, Func<string> cronExpression);

    void AddOrUpdate<T>(string recurringJobId, Expression<Action<T>> methodCall, Func<string> cronExpression);

    void AddOrUpdate<T>(string recurringJobId, string queue, Expression<Action<T>> methodCall, Func<string> cronExpression);

    void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, string cronExpression);

    void AddOrUpdate(string recurringJobId, string queue, Expression<Action> methodCall, string cronExpression);

    #endregion

    #region 队列任务
    string Enqueue(Expression<Action> methodCall);

    string Enqueue(Expression<Func<Task>> methodCall);

    string Enqueue<T>(Expression<Action<T>> methodCall);

    string Enqueue<T>(Expression<Func<T, Task>> methodCall);
    #endregion

    #region 延时任务
    string Schedule(Expression<Action> methodCall, TimeSpan delay);

    string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay);

    string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt);

    string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt);

    string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);

    string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);

    string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt);

    string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt);
    #endregion

    bool Delete(string jobId);

    bool Delete(string jobId, string fromState);

    bool Requeue(string jobId);

    bool Requeue(string jobId, string fromState);
}
