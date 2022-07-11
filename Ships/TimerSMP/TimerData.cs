
//using System.ComponentModel;

namespace ModelSMP.TimerSMP
{
    public class TimerData : INotifyPropertyChanged
    {
        private DispatcherTimer m_timer;

        public int i_multipleTimer = GeneralSettings.Settings.MultipleTimer;
        public int Tick = GeneralSettings.Settings.TimerTickInMS;

        private int i_seconds;
        public int Seconds
        {
            get { return i_seconds; }
            set
            {
                if(i_seconds > -1 && i_seconds < 60)
                    i_seconds = value;
                //OnPropertyChanged();
            }
        }
        private int i_minutes;
        public int Minutes  
        { 
            get 
            { 
                return i_minutes; 
            } 
            set 
            { 
                if(value > -1 && value < 60)
                    i_minutes = value;
                //OnPropertyChanged();
            } 
        }
        private int i_hours;
        public int Hours
        {
            get
            {
                return i_hours;
            }
            set
            {
                if (value > -1 && value < 24)
                    i_hours = value;
                //OnPropertyChanged();
            }
        }
        private string? s_timeFormat;
        public string TimeFormat
        {
            get { return s_timeFormat; }
            set { s_timeFormat = value; }
        }
        public delegate void PropertyChangedEventHandler();

        public static event PropertyChangedEventHandler? PropertyChanged;


        public TimerData()
        {
            Seconds = 0;
            Minutes = 0;
            Hours = 9;
            TimeFormat = "Время 09:00 AM";
            m_timer = new DispatcherTimer(Tick);
            m_timer.Tick += TimerTick;
        }

        public void TimerTick()
        {
            int msInSec = 1000;
            int secInMinute = 60;
            int minuteInHour = secInMinute;

            int sec = Tick * i_multipleTimer / msInSec;
            int minutes = 0;

            if(Seconds + sec > 59)
            {
                Seconds = (Seconds + sec) % secInMinute;
                minutes = (Seconds + sec) / secInMinute;
            }
            else Seconds += sec;

            if (Minutes + minutes > 59)
            {
                if (Hours + (Minutes + minutes) / minuteInHour > 23)
                    Hours = 0;
                Hours += (Minutes + minutes) / minuteInHour;
                Minutes = (Minutes + minutes) % minuteInHour;                
            }
            else Minutes += minutes;
            OnPropertyChanged();
            UpdateTimeFormat();
        }

        public void TimerOff()
        {
            m_timer.Pause();
        }

        public void TimerOn()
        {
            m_timer.Resume();
        }

        private void UpdateTimeFormat()
        {
            TimeFormat = string.Format("Время {0:00}:{1:00} AM", Hours, Minutes);
            Console.WriteLine(TimeFormat);
        }

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke();
        }
    }
}
