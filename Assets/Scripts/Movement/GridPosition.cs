namespace RPG.Movement
{

    public struct GridPostion
    {

        public int x;
        public int z;

        public GridPostion(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public override string ToString()
        {
            return $"x: {x} z:  + {z}";
        }

    }
}

