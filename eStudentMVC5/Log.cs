#define TRACE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace eStudentMVC5
{

    public static class Log
    {
        private static string logTimeFormat = "HH:mm:ss.fff";

        private static TraceSwitch globalTraceSwitch;

        static Log()
        {
            globalTraceSwitch = new TraceSwitch("TraceLevelSwitch", "Global trace level switch");
            Trace.WriteLine(string.Format("#Logging started at {0}", DateTime.Now.ToString()));
        }

        private static string getCallingType()
        {
            return (new StackFrame(2, false).GetMethod()).DeclaringType.ToString();
        }

        public static void Error(string message, Exception ex = null,
                                [CallerMemberName] string sourceMember = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (globalTraceSwitch.TraceError)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ERROR: ", DateTime.Now.ToString(logTimeFormat));
                sb.AppendLine(message);
                Trace.Write(sb.ToString());
            }
        }

        public static void Info(string message)
        {
            Trace.WriteLineIf(globalTraceSwitch.TraceInfo,
                string.Format("{0} INFO: {1}", DateTime.Now.ToString(logTimeFormat), message));

        }

        public static void Debug(string message)
        {
            Trace.WriteLineIf(globalTraceSwitch.TraceVerbose,
                string.Format("{0} DEBUG: {1}", DateTime.Now.ToString(logTimeFormat), message));
        }
    }
}