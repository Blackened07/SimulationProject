namespace Simulation.MapSim
{
    internal struct Coordinates(int x, int y) : IEquatable<Coordinates>
    {
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set {  y = value; }
        }

        public override bool Equals(object? obj)
        {
            return obj is Coordinates coordinates;
                   
        }

        public bool Equals(Coordinates coordinates)
        {
            return X == coordinates.X &&
                   Y == coordinates.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
