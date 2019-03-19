using System;
using UnityEngine;
using Azarashi.Utilities.Collections;

namespace Azarashi.CerkeOnline.Application
{
    public class SystemLogHandler : ILogHandler
    {
        readonly string loggerTag = " System > ";
        readonly ILogHandler defaultLogHandler = Debug.unityLogger.logHandler;

        void ILogHandler.LogException(Exception exception, UnityEngine.Object context)
        {
            defaultLogHandler.LogException(new Exception(loggerTag, exception), context);
        }
        
        void ILogHandler.LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            args.ForEach((i, obj) => loggerTag + obj);
            Debug.unityLogger.LogFormat(logType, context, format, args);
        }
    }
}