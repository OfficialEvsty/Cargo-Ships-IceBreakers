
namespace ModelSMP.Ship_Engine
{
    class Engine
    {
        public float MaxSpeedInKM { get; init; }
        public float AverageSpeedInKM { get; private set; }
        public float CaravanSpeedInKM { get; private set; }        
        public bool IsStartEngine;

        private float f_currentSpeed;

        public Engine()
        {
            MaxSpeedInKM = 40;
            AverageSpeedInKM = MaxSpeedInKM * 3 / 4;
            f_currentSpeed = AverageSpeedInKM;
        }

        public float Running()
        {
            if(IsStartEngine)
                return f_currentSpeed;
            return 0;
        }

        public void SwitchSpeedInCaravan()
        {
            f_currentSpeed = CaravanSpeedInKM;
        }
    }
}
