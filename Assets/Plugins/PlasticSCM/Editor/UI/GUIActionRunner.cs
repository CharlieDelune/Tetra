﻿using System;
using System.Threading;

using log4net;

namespace Codice.UI
{
    internal static class GUIActionRunner
    {
        internal delegate void ActionDelegate();

        internal static void RunGUIAction(ActionDelegate action)
        {
            if (EditorDispatcher.IsOnMainThread)
            {
                action();
                return;
            }

            ManualResetEvent syncEvent = new ManualResetEvent(false);

            EditorDispatcher.Dispatch(delegate {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    mLog.ErrorFormat("GUI action failed: {0}", e.Message);
                    mLog.DebugFormat("Stack trace:{0}{1}", Environment.NewLine, e.StackTrace);
                    throw;
                }
                finally
                {
                    syncEvent.Set();
                }
            });

            syncEvent.WaitOne();
        }

        static readonly ILog mLog = LogManager.GetLogger("GUIActionRunner");
    }
}
