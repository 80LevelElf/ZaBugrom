using System;

namespace Engine
{
    public class TimeChecker
    {
        public static TimeChecker EveryDay
        {
            get
            {
                return new TimeChecker(new TimeSpan(24, 0, 0));
            }
        }

        public static TimeChecker EveryHour
        {
            get
            {
                return new TimeChecker(new TimeSpan(0, 1, 0));
            }
        }

        public static TimeChecker Every6Hours
        {
            get
            {
                return new TimeChecker(new TimeSpan(0, 6, 0));
            }
        }

        public static TimeChecker Every12Hours
        {
            get
            {
                return new TimeChecker(new TimeSpan(0, 12, 0));
            }
        }

        private DateTime BorderTime { get; set; }
        public TimeSpan TimeInterval { get; set; }

        public TimeChecker(TimeSpan timeInterval)
        {
            TimeInterval = timeInterval;
        }

        public bool Check()
        {
            var now = DateTime.Now;

            if (BorderTime < now)
            {
                MoveToNextBorderTime();
                return true;
            }

            return false;
        }

        private void MoveToNextBorderTime()
        {
            BorderTime = DateTime.Now + TimeInterval;
        }
    }
}
