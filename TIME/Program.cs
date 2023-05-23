    using System;
namespace TIME
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

        }
    }
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        private readonly byte hours;
        private readonly byte minutes;
        private readonly byte seconds;

        public byte Hours => hours;
        public byte Minutes => minutes;
        public byte Seconds => seconds;

        public Time(byte hours, byte minutes, byte seconds)
        {
            if (hours >= 24 || minutes >= 60 || seconds >= 60)
                throw new ArgumentException("Invalid time.");

            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public Time(byte hours, byte minutes) : this(hours, minutes, 0)
        {
        }

        public Time(byte hours) : this(hours, 0, 0)
        {
        }

        public Time(string timeString)
        {
            string[] parts = timeString.Split(':');
            if (parts.Length != 3 || !byte.TryParse(parts[0], out byte hours) ||
                !byte.TryParse(parts[1], out byte minutes) || !byte.TryParse(parts[2], out byte seconds))
                throw new ArgumentException("Invalid time string.");

            if (hours >= 24 || minutes >= 60 || seconds >= 60)
                throw new ArgumentException("Invalid time.");

            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public override string ToString()
        {
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public override bool Equals(object obj)
        {
            return obj is Time time && Equals(time);
        }

        public bool Equals(Time other)
        {
            return hours == other.hours && minutes == other.minutes && seconds == other.seconds;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(hours, minutes, seconds);
        }

        public int CompareTo(Time other)
        {
            if (hours != other.hours)
                return hours.CompareTo(other.hours);
            if (minutes != other.minutes)
                return minutes.CompareTo(other.minutes);
            return seconds.CompareTo(other.seconds);
        }

        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Time left, Time right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(Time left, Time right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Time left, Time right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Time left, Time right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Time left, Time right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static Time operator +(Time time, TimePeriod timePeriod)
        {
            long totalSeconds = time.Seconds + time.Minutes * 60 + time.Hours * 3600;
            totalSeconds += timePeriod.Seconds + timePeriod.Minutes * 60 + timePeriod.Hours * 3600;
            totalSeconds %= 86400; // modulo 24 hours

            byte hours = (byte)(totalSeconds / 3600);
            byte minutes = (byte)((totalSeconds % 3600) / 60);
            byte seconds = (byte)(totalSeconds % 60);

            return new Time(hours, minutes, seconds);
        }
    }

    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long seconds;

        public long Seconds => seconds;
        public byte Hours => (byte)(seconds / 3600);
        public byte Minutes => (byte)((seconds % 3600) / 60);

        public TimePeriod(byte hours, byte minutes, byte seconds)
        {
            if (hours >= 24 || minutes >= 60 || seconds >= 60)
                throw new ArgumentException("Invalid time period.");

            this.seconds = seconds + minutes * 60 + hours * 3600;
        }

        public TimePeriod(byte hours, byte minutes) : this(hours, minutes, 0)
        {
        }

        public TimePeriod(byte seconds) : this(0, 0, seconds)
        {
        }

        public TimePeriod(Time startTime, Time endTime)
        {
            long startSeconds = startTime.Seconds + startTime.Minutes * 60 + startTime.Hours * 3600;
            long endSeconds = endTime.Seconds + endTime.Minutes * 60 + endTime.Hours * 3600;

            if (endSeconds < startSeconds)
                throw new ArgumentException("Invalid time period.");

            this.seconds = endSeconds - startSeconds;
        }

        public TimePeriod(string timePeriodString)
        {
            string[] parts = timePeriodString.Split(':');
            if (parts.Length != 3 || !byte.TryParse(parts[0], out byte hours) ||
                !byte.TryParse(parts[1], out byte minutes) || !byte.TryParse(parts[2], out byte seconds))
                throw new ArgumentException("Invalid time period string.");

            if (hours >= 24 || minutes >= 60 || seconds >= 60)
                throw new ArgumentException("Invalid time period.");

            this.seconds = seconds + minutes * 60 + hours * 3600;
        }
        public long ToSeconds()
        {
            long totalSeconds = Hours * 3600 + Minutes * 60 + Seconds;
            return totalSeconds;
        }
        public static TimePeriod Plus(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            long totalSeconds = timePeriod1.ToSeconds() + timePeriod2.ToSeconds();

            
            int hours = (int)(totalSeconds / 3600) % 24;
            int minutes = (int)((totalSeconds / 60) % 60);
            int seconds = (int)(totalSeconds % 60);

            return new TimePeriod((byte)hours, (byte)minutes, (byte)seconds);
        }

        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}:{seconds % 60:D2}";
        }

        public override bool Equals(object obj)
        {
            return obj is TimePeriod period && Equals(period);
        }

        public bool Equals(TimePeriod other)
        {
            return seconds == other.seconds;
        }

        public override int GetHashCode()
        {
            return seconds.GetHashCode();
        }

        public int CompareTo(TimePeriod other)
        {
            return seconds.CompareTo(other.seconds);
        }

        public static bool operator ==(TimePeriod left, TimePeriod right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimePeriod left, TimePeriod right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(TimePeriod left, TimePeriod right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(TimePeriod left, TimePeriod right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(TimePeriod left, TimePeriod right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(TimePeriod left, TimePeriod right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static TimePeriod operator +(TimePeriod left, TimePeriod right)
        {
            long totalSeconds = left.Seconds + right.Seconds;

            byte hours = (byte)(totalSeconds / 3600);
            byte minutes = (byte)((totalSeconds % 3600) / 60);
            byte seconds = (byte)(totalSeconds % 60);

            return new TimePeriod(hours, minutes, seconds);
        }

    }

}