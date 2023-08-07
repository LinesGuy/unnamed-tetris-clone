namespace MyTetris
{
    public class LevelManager
    {
        private TetrisGame _game;
        // NOTE that Speed level is not the exact same as the level shown to the player
        // Speed level increases the same as shown level, however clearing a section
        // with a section COOL will increase the speed level by 100, but not the shown level.
        public int SpeedLevel = 0;
        public int ARE = 27; // Appearance Delay: Frames between piece locking and new piece appearing
        public int LineARE = 27; // Line Appearance Delay: Same as ARE but used when clearing a line
        public int DAS = 16; // Delayed Auto Shift: Frames between holding down left/right and the piece automatiaclly shifting
        public int ARR = 1; // Auto Repeat Rate: Frames between auto shifts
        public int Lock = 30; // Frames between the piece touching the floor unmoved and locking in place
        public int LineClear = 40; // Frames taken to clear a line, does not include Lock frames, which are applied after
        public int Gravity = 1024; // 1 gravity = 1/65536 G, 1G = 1 tile per second, 1310720 gravity = 20G
        public LevelManager(TetrisGame game)
        {
            _game = game;
        }
        public void Increase(int amount)
        {
            // Increases speed level by given amount and applies delays

            // Each section clear (100 levels) requires 2 or more levels to advance
            // E.g going from level 99 to 100, or 199 to 200 requires clearing at least two
            // lines at once, and clearing a single line will not advance
            if (SpeedLevel % 100 == 99 && amount == 1)
            {
                return;
            }

            // Increase level
            SpeedLevel += amount;

            // Apply gravity
            if (SpeedLevel < 30) Gravity = 1024;
            else if (SpeedLevel < 35) Gravity = 1536;
            else if (SpeedLevel < 40) Gravity = 2048;
            else if (SpeedLevel < 50) Gravity = 2560;
            else if (SpeedLevel < 60) Gravity = 3072;
            else if (SpeedLevel < 70) Gravity = 4096;
            else if (SpeedLevel < 80) Gravity = 8192;
            else if (SpeedLevel < 90) Gravity = 12288;
            else if (SpeedLevel < 100) Gravity = 16384;
            else if (SpeedLevel < 120) Gravity = 20480;
            else if (SpeedLevel < 140) Gravity = 24576;
            else if (SpeedLevel < 160) Gravity = 28672;
            else if (SpeedLevel < 170) Gravity = 32768;
            else if (SpeedLevel < 200) Gravity = 36864;
            else if (SpeedLevel < 220) Gravity = 1024;
            else if (SpeedLevel < 230) Gravity = 8192;
            else if (SpeedLevel < 233) Gravity = 16384;
            else if (SpeedLevel < 236) Gravity = 24576;
            else if (SpeedLevel < 239) Gravity = 32768;
            else if (SpeedLevel < 243) Gravity = 40960;
            else if (SpeedLevel < 247) Gravity = 49152;
            else if (SpeedLevel < 251) Gravity = 57344;
            else if (SpeedLevel < 300) Gravity = 65536;
            else if (SpeedLevel < 330) Gravity = 131072;
            else if (SpeedLevel < 360) Gravity = 196608;
            else if (SpeedLevel < 400) Gravity = 262144;
            else if (SpeedLevel < 420) Gravity = 327680;
            else if (SpeedLevel < 450) Gravity = 262144;
            else if (SpeedLevel < 500) Gravity = 196608;
            else Gravity = 1310720;

            // Apply delays
            if (SpeedLevel <= 499)
            {
                ARE = 27;
                LineARE = 27;
                DAS = 16;
                Lock = 30;
                LineClear = 40;
            } else if (SpeedLevel <= 599)
            {
                ARE = 27;
                LineARE = 27;
                DAS = 10;
                Lock = 30;
                LineClear = 25;
            } else if (SpeedLevel <= 699)
            {
                ARE = 27;
                LineARE = 18;
                DAS = 10;
                Lock = 30;
                LineClear = 16;
            } else if (SpeedLevel <= 799)
            {
                ARE = 18;
                LineARE = 14;
                DAS = 10;
                Lock = 30;
                LineClear = 12;
            } else if (SpeedLevel <= 899)
            {
                ARE = 14;
                LineARE = 8;
                DAS = 10;
                Lock = 30;
                LineClear = 6;
            } else if (SpeedLevel <= 999)
            {
                ARE = 14;
                LineARE = 8;
                DAS = 8;
                Lock = 17;
                LineClear = 6;
            } else if (SpeedLevel <= 1099)
            {
                ARE = 8;
                LineARE = 8;
                DAS = 8;
                Lock = 17;
                LineClear = 6;
            } else if (SpeedLevel <= 1199)
            {
                ARE = 7;
                LineARE = 7;
                DAS = 8;
                Lock = 15;
                LineClear = 6;
            } else // Level >= 1200
            {
                ARE = 6;
                LineARE = 6;
                DAS = 8;
                Lock = 15;
                LineClear = 6;
            }
        }
    }
}
