namespace MassageStudioLorem.Global
{
    using System.Collections.Generic;

    public static class DefaultTimeSchedule
    {
        public static ICollection<string> TimeSchedule
        { get; private set; }

        public static void SeedTimeTable()
        {
            TimeSchedule = new List<string>()
            {
                "9:00AM", "10:00AM", 
                "11:00AM", "12:00PM",
                "2:00PM", "3:00PM",
                "4:00PM", "5:00PM"
            };
        }
    }
}
