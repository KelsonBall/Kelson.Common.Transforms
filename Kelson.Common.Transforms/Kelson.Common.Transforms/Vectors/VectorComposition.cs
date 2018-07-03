namespace Kelson.Common.Transforms
{
    public partial struct Vector3d
    {
        public Vector3d(Vector2d xy, double z) : this(xy.X, xy.Y, z) { }
        public Vector3d(double x, Vector2d yz) : this(x, yz.X, yz.Y) { }
    }

    public partial struct Vector4d
    {
        public Vector4d(Vector2d xy, Vector2d zw) : this(xy.X, xy.Y, zw.X, zw.Y) { }
        public Vector4d(double x, Vector2d yz, double w) : this(x, yz.X, yz.Y, w) { }
        public Vector4d(double x, Vector3d yzw) : this(x, yzw.X, yzw.Y, yzw.Z) { }
        public Vector4d(Vector3d xyz, double w) : this(xyz.X, xyz.Y, xyz.Z, w) { }
    }    
}
