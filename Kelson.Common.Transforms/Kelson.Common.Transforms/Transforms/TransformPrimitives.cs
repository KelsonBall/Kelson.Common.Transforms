using System;
using System.Collections.Generic;
using System.Text;

namespace Kelson.Common.Transforms
{
    public partial struct Transform
    {
        public static Transform Identity() =>
            new Transform(1, 0, 0, 0,
                          0, 1, 0, 0,
                          0, 0, 1, 0,
                          0, 0, 0, 1);

        public static Transform Zero() =>
            new Transform(0, 0, 0, 0,
                          0, 0, 0, 0,
                          0, 0, 0, 0,
                          0, 0, 0, 0);

        public static Transform Translation(Vector3d t) =>
            new Transform(1, 0, 0, t.X,
                          0, 1, 0, t.Y,
                          0, 0, 1, t.Z,
                          0, 0, 0, 1);

        public static Transform Translation(double x, double y, double z) =>
            new Transform(1, 0, 0, x,
                          0, 1, 0, y,
                          0, 0, 1, z,
                          0, 0, 0, 1);

        public static Transform Scale(double s) =>
            new Transform(s, 0, 0, 0,
                          0, s, 0, 0,
                          0, 0, s, 0,
                          0, 0, 0, 1);

        public static Transform Scale(Vector3d s) =>
            new Transform(s.X, 0, 0, 0,
                          0, s.Y, 0, 0,
                          0, 0, s.Z, 0,
                          0, 0, 0, 1 );

        public static Transform Scale(double x, double y, double z) =>
            new Transform(x, 0, 0, 0,
                          0, y, 0, 0,
                          0, 0, z, 0,
                          0, 0, 0, 1);
    }
}
