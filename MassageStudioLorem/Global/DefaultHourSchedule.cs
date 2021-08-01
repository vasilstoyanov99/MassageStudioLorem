namespace MassageStudioLorem.Global
{
    using System.Collections.Generic;

    public static class DefaultHourSchedule
    {
        public static ICollection<string> HourScheduleAsString
        { get; private set; }

        public static void SeedHourScheduleAsString()
        {
            HourScheduleAsString = new List<string>()
            {
                "09:00", "10:00",
                "11:00", "12:00",
                "14:00", "15:00",
                "16:00", "17:00"
            };
        }
    }
}
