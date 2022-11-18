namespace SnowFall_FNA_
{
    public class SnowClass
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Severity { get; set; }
        public double Rotation { get; set; }

        public SnowClass(int x, int y, int s)
        {
            PosX = x;
            PosY = y;
            Severity = s;
        }
    }
}