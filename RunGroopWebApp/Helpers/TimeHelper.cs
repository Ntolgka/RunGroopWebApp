namespace RunGroopWebApp.Helpers
{
    public class TimeHelper
    {
        public string GetTimeAgoString(DateTime dateTime)
        {
            TimeSpan timeSinceCreation = DateTime.UtcNow - dateTime;

            if (timeSinceCreation.TotalSeconds < 60)
                return $"{timeSinceCreation.TotalSeconds:0} seconds ago";
            if (timeSinceCreation.TotalMinutes < 60)
                return $"{timeSinceCreation.TotalMinutes:0} minutes ago";
            if (timeSinceCreation.TotalHours < 24)
                return $"{timeSinceCreation.TotalHours:0} hours ago";
            if (timeSinceCreation.TotalDays < 30)
                return $"{timeSinceCreation.TotalDays:0} days ago";

            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
