using Cysharp.Threading.Tasks;
using System;

public delegate UniTask EventHandlerAsync(object sender, EventArgs e);

public static class EventHandlerAsyncExtensions {
    public static UniTask InvokeAsync(this EventHandlerAsync handler, object sender, EventArgs e) {
        Delegate[] delegates = handler?.GetInvocationList();

        if (delegates == null || delegates.Length == 0)
            return UniTask.CompletedTask;

        var tasks = new UniTask[delegates.Length];

        for (var i = 0; i < delegates.Length; i++) {
            tasks[i] = (UniTask)delegates[i].DynamicInvoke(sender, e);
        }

        return UniTask.WhenAll(tasks);
    }
}