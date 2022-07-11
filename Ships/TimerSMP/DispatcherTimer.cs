

namespace ModelSMP.TimerSMP
{
    interface INotifyPropertyChanged
    {
        void OnPropertyChanged();
        
    }

    class DispatcherTimer
    {
        Timer m_timer;
        int i_tick;

        public delegate void TickEventHandler();
        public event TickEventHandler? Tick;
        public DispatcherTimer(int tick)
        {
            TimerCallback tm = new TimerCallback(Running);
            i_tick = tick;
            m_timer = new Timer(tm, null, 0, i_tick);
        }

        private void Running(object tick)
        {
            if (m_timer != null)
            {
                Tick?.Invoke();
            }
        }

        public void Pause()
        {
            m_timer.Change(0, 0);
            Console.WriteLine("Таймер был остановлен.");
        }

        public void Resume()
        {
            m_timer.Change(0, i_tick);
            Console.WriteLine("Таймер запущен.");
        }
    }
}
