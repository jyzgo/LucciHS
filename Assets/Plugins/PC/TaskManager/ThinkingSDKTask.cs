using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThinkingSDK.PC.Utils;

namespace ThinkingSDK.PC.TaskManager
{
    public class ThinkingSDKTask
    {
        private readonly List<Task> mTasks = new List<Task>();
        private System.Threading.Semaphore mSM = new Semaphore(1, 1);
        public void AddTask(Task task)
        {
            lock (mTasks)
            {
                mTasks.Add(task);
            }
        }
        public List<Task> Tasks()
        {
            return mTasks;
        }
        public Task CurrentTask()
        {
            return mTasks.Count > 0 ? mTasks[0] : null;
        }
        /// <summary>
        /// 持有信号
        /// </summary>
        public void WaitOne()
        {
            mSM.WaitOne();
        }
        /// <summary>
        /// 释放信号
        /// </summary>
        public void Release()
        {
            mSM.Release();
        }
        public void SyncInvokeAllTask()
        {
            Task.Run(() =>
            {
                lock (mTasks)
                {
                    while (mTasks.Count > 0)
                    {
                        WaitOne();
                        Task task = mTasks[0];
                        task.Start();
                        mTasks.RemoveAt(0);
                    }
                }
            });
        }
    }
}

