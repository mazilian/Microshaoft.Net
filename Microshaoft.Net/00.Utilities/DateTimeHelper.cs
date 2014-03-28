﻿namespace Microshaoft
{
    using System;
    using System.IO;
    using System.Globalization;
    public static class DateTimeHelper
    {
#if NET45
//#endif
        public static void GetAlignSecondsDateTimes<T>
                                (
                                    DateTime time
                                    , params Tuple<long, Func<DateTime, long, DateTime, T, T>>[] processAlignSecondsDateTimesFuncs
                                )
        {
            T r = default(T);
            foreach (var x in processAlignSecondsDateTimesFuncs)
            {
                var alignSeconds = x.Item1;
                var alignTime = DateTimeHelper.GetAlignSecondsDateTime(time, alignSeconds);
                if (x.Item2 != null)
                {
                    r = x.Item2(time, alignSeconds, alignTime, r);
                }
            }
        }
//#if NET45
#endif
        public static bool IsVaildateTimestamp(DateTime timeStamp, int timeoutSeconds)
        {
            long l = SecondsDiffNow(timeStamp);
            return ((l > 0) && (l < timeoutSeconds));
        }
        public static long MillisecondsDiffNow(DateTime time)
        {
            long now = DateTime.Now.Ticks;
            long t = time.Ticks;
            return (t - now) / 10000;
        }
        public static long SecondsDiffNow(DateTime time)
        {
            return MillisecondsDiffNow(time) / 1000;
        }
        public static DateTime GetAlignSecondsDateTime(DateTime time, long alignSeconds)
        {
            long ticks = time.Ticks;
            ticks -= ticks % (10000 * 1000 * alignSeconds);
            DateTime dt = new DateTime(ticks);
            return dt;
        }
        public static string GetDateTimeString(DateTime time, string format)
        {
            return time.ToString(format);
        }
        public static DateTime? ParseExactNullableDateTime(string text, string format)
        {
            DateTime time;
            DateTime? r = DateTime.TryParseExact
                                        (
                                            text
                                            , format
                                            , DateTimeFormatInfo.InvariantInfo
                                            , DateTimeStyles.None
                                            , out time
                                        ) ? time as DateTime? : null;
            return r;
        }

       
    }
}