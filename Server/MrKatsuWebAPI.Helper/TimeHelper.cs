namespace MrKatsuWebAPI.Helper
{
    public class TimeHelper
    {
        public DateTime Timestamp { get; }
        public string TimeZone { get; }
        public bool RemoveTick { get; }

        private TimeHelper(Builder builder)
        {
            Timestamp = builder.Timestamp;
            TimeZone = builder.TimeZone;
            RemoveTick = builder.RemoveTick;
        }

        public override string ToString()
        {
            return $"TimeProcessor {{ Timestamp='{Timestamp}', TimeZone='{TimeZone}', RemoveTick='{RemoveTick}' }}";
        }

        public static implicit operator DateTime(TimeHelper timeProcessor)
        {
            return timeProcessor.Timestamp;
        }

        public class Builder
        {
            public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
            public string TimeZone { get; private set; } = "UTC";
            public bool RemoveTick { get; private set; } = false;

            public Builder SetTimestamp(DateTime timestamp)
            {
                // Ensure the DateTimeKind is correctly set to Utc
                Timestamp = timestamp.Kind == DateTimeKind.Utc ? timestamp : DateTime.SpecifyKind(timestamp, DateTimeKind.Utc);
                return this;
            }

            public Builder SetTimeZone(string timeZone)
            {
                try
                {
                    TimeZoneInfo tzInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                    TimeZone = timeZone;
                    Timestamp = TimeZoneInfo.ConvertTimeFromUtc(Timestamp, tzInfo);
                }
                catch (TimeZoneNotFoundException)
                {
                    throw new ArgumentException("Invalid time zone ID.");
                }
                return this;
            }

            public Builder SetRemoveTick(bool removeTick)
            {
                RemoveTick = removeTick;
                if (removeTick)
                {
                    Timestamp = new DateTime(Timestamp.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond, DateTimeKind.Utc);
                }
                return this;
            }

            public TimeHelper Build()
            {
                return new TimeHelper(this);
            }
        }
    }
}
