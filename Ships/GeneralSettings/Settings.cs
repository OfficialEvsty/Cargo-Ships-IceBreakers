
namespace ModelSMP.GeneralSettings
{
    public static class Settings
    {
        //Время
        public static int MultiplyTimer = 10000;
        public static int TimerTickInMS = 1000 * TimerTickInSeconds;
        public const int TimerTickInSeconds = 1;

        //Мин/Макс дистанция создания портов
        /// <summary>
        /// Площадь одного тайла в квадратных километрах.
        /// </summary>
        public const int TileSquare = 6;
        /// <summary>
        /// Длина всего поля/карты в километрах.
        /// </summary>
        public const int FieldLength = 12000;
        /// <summary>
        /// Ширина всего поля/карты в километрах.
        /// </summary>
        public const int FieldWidth = 8000;

        //public const float MaxDistanceToSpawnNodes = 300;
    }
}
