namespace Getodoro
{
    internal class TimeTick
    {
        private int _minute;
        private int _second;

        public TimeTick(int minutes)
        {
            this._minute = minutes;
            this._second = 0;
            this.Minutes = minutes;
        }

        public int Minute
        {
            get { return this._minute; }
            set { }
        }

        public int Second
        {
            get { return this._second; }
            set { }
        }

        public int Minutes { get; set; }

        public bool Tick()
        {
            if (this._second <= 0)
            {
                if (this._minute <= 0)
                {
                    return true;
                }
                else
                {
                    this._minute -= 1;
                    this._second = 59;
                }
            }
            else
            {
                this._second -= 1;
            }
            return false;
        }
    }
}