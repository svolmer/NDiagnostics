namespace NDiagnostics.Metering
{
    public enum MeterType : long
    {
        // Instant Meters 

        InstantValue = 0x00001000,

        InstantTime = 0x00001001,

        InstantRatio = 0x00001011,

        // Average Meters

        AverageValue = 0x00010000,

        AverageTime = 0x00010001,

        AverageRate = 0x00010010,

        AverageRatio = 0x00010011,

        // Difference Meters 

        DifferentialValue = 0x00100000,

        // Percentage of Time Meters

        Timer = 0x10000100,

        TimerInverse = 0x10000101,

        MultiTimer = 0x10000110,

        MultiTimerInverse = 0x10000111,

        Timer100Ns = 0x10001000,

        Timer100NsInverse = 0x10001001,

        MultiTimer100Ns = 0x10001010,

        MultiTimer100NsInverse = 0x10001011,
    }
}
