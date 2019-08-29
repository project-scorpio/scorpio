using System;
using System.Collections.Generic;
using System.Text;

namespace System
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public class WeakEventManager<TArgs> where TArgs:EventArgs
    {
        private readonly List<WeakReference<EventHandler<TArgs>>> _delegateList;

        /// <summary>
        /// 
        /// </summary>
        public WeakEventManager()
        {
            _delegateList = new List<WeakReference<EventHandler<TArgs>>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void Register(EventHandler<TArgs> handler)
        {
            _delegateList.Add(new WeakReference<EventHandler<TArgs>>(handler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Trigger(object sender,TArgs args)
        {
            _delegateList.ForEach(d => { if (d.TryGetTarget(out var target)) { target.Invoke(sender, args); } });
        }
    }
}
