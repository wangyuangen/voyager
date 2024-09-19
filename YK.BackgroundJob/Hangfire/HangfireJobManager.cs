using System.Linq.Expressions;
using YK.BackgroundJob.Abstractions;
using HangfireBackgroundJob = Hangfire.BackgroundJob;

namespace YK.BackgroundJob.Hangfire;

public class HangfireJobManager : IBackgroundJob
{
    public void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, Func<string> cronExpression) =>
        RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression);

    public void AddOrUpdate(string recurringJobId, string queue, Expression<Action> methodCall, Func<string> cronExpression) =>
        RecurringJob.AddOrUpdate(recurringJobId, queue, methodCall, cronExpression);

    public void AddOrUpdate<T>(string recurringJobId, Expression<Action<T>> methodCall, Func<string> cronExpression) =>
        RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression);

    public void AddOrUpdate<T>(string recurringJobId, string queue, Expression<Action<T>> methodCall, Func<string> cronExpression) =>
        RecurringJob.AddOrUpdate(recurringJobId, queue, methodCall, cronExpression);

    public void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, string cronExpression) =>
        RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression);

    public void AddOrUpdate(string recurringJobId, string queue, Expression<Action> methodCall, string cronExpression) =>
        RecurringJob.AddOrUpdate(recurringJobId, queue, methodCall, cronExpression);

    public bool Delete(string jobId) =>
        HangfireBackgroundJob.Delete(jobId);

    public bool Delete(string jobId, string fromState) =>
        HangfireBackgroundJob.Delete(jobId, fromState);

    public string Enqueue(Expression<Func<Task>> methodCall) =>
        HangfireBackgroundJob.Enqueue(methodCall);

    public string Enqueue<T>(Expression<Action<T>> methodCall) =>
        HangfireBackgroundJob.Enqueue(methodCall);

    public string Enqueue(Expression<Action> methodCall) =>
        HangfireBackgroundJob.Enqueue(methodCall);

    public string Enqueue<T>(Expression<Func<T, Task>> methodCall) =>
        HangfireBackgroundJob.Enqueue(methodCall);

    public bool Requeue(string jobId) =>
        HangfireBackgroundJob.Requeue(jobId);

    public bool Requeue(string jobId, string fromState) =>
        HangfireBackgroundJob.Requeue(jobId, fromState);

    public string Schedule(Expression<Action> methodCall, TimeSpan delay) =>
        HangfireBackgroundJob.Schedule(methodCall, delay);

    public string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay) =>
        HangfireBackgroundJob.Schedule(methodCall, delay);

    public string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt) =>
        HangfireBackgroundJob.Schedule(methodCall, enqueueAt);

    public string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt) =>
        HangfireBackgroundJob.Schedule(methodCall, enqueueAt);

    public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay) =>
        HangfireBackgroundJob.Schedule(methodCall, delay);

    public string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay) =>
        HangfireBackgroundJob.Schedule(methodCall, delay);

    public string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt) =>
        HangfireBackgroundJob.Schedule(methodCall, enqueueAt);

    public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt) =>
        HangfireBackgroundJob.Schedule(methodCall, enqueueAt);
}
