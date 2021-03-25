using System.Configuration;

namespace FSSBHelper
{
    public sealed class Settings
    {
        public bool Debug { get; private set; }
        public string JoystickName { get; private set; }
        public int IntervalMS { get; private set; }
        public double MaxPercent { get; private set; }
        public int AlertHz { get; private set; }
        public int AlertDuration { get; private set; }

        public Settings()
        {
            Debug = bool.Parse(ConfigurationManager.AppSettings["Debug"].ToString());
            JoystickName = ConfigurationManager.AppSettings["joystickName"].ToString();
            IntervalMS = int.Parse(ConfigurationManager.AppSettings["intervalMS"].ToString());
            MaxPercent = double.Parse(ConfigurationManager.AppSettings["maxPercent"].ToString());
            AlertHz = int.Parse(ConfigurationManager.AppSettings["alertHz"].ToString());
            AlertDuration = int.Parse(ConfigurationManager.AppSettings["alertDurationMS"].ToString());
        }
    }
}
