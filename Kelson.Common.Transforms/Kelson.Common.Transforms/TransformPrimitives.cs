using Kelson.Common.Vectors;
using System;
using static Kelson.Common.Vectors.VectorConstruction;

namespace Kelson.Common.Transforms
{
    public readonly partial struct Transform
    {
        public static Transform Identity() =>
            new Transform(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        public static RefTransform IdentityRef() =>
            new RefTransform(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        public static Transform Zero() =>
            new Transform(
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0);

        public static RefTransform ZeroRef() =>
            new RefTransform(
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0);

        public static Transform Translation(Vector3fd t) =>                    
            new Transform(
                  1,   0,   0, 0,
                  0,   1,   0, 0,
                  0,   0,   1, 0,
                t.X, t.Y, t.Z, 1);

        public static RefTransform TranslationRef(in Vector3fd t) =>
            new RefTransform(
                  1, 0, 0, 0,
                  0, 1, 0, 0,
                  0, 0, 1, 0,
                t.X, t.Y, t.Z, 1);

        public static RefTransform TranslationRef(RefVector3f t) =>
            new RefTransform(
                  1,   0,   0, 0,
                  0,   1,   0, 0,
                  0,   0,   1, 0,
                t.X, t.Y, t.Z, 1);

        public static Transform Translation(double x, double y, double z) =>
            new Transform(1, 0, 0, 0,
                          0, 1, 0, 0,
                          0, 0, 1, 0,
                          x, y, z, 1);

        public static RefTransform TranslationRef(double x, double y, double z) =>
            new RefTransform(1, 0, 0, 0,
                             0, 1, 0, 0,
                             0, 0, 1, 0,
                             x, y, z, 1);

        public static Transform RotationX(double theta)
        {
            var c = Math.Cos(theta);
            var s = Math.Sin(theta);
            return new Transform(
                          1, 0, 0, 0,
                          0, c, s, 0,
                          0,-s, c, 0,
                          0, 0, 0, 1);
        }

        public static RefTransform RotationXRef(double theta)
        {
            var c = Math.Cos(theta);
            var s = Math.Sin(theta);
            return new RefTransform(
                          1, 0, 0, 0,
                          0, c, s, 0,
                          0, -s, c, 0,
                          0, 0, 0, 1);
        }

        public static Transform RotationY(double theta)
        {
            var c = Math.Cos(theta);
            var s = Math.Sin(theta);
            return new Transform(
                          c, 0,-s, 0,
                          0, 1, 0, 0,
                          s, 0, c, 0,
                          0, 0, 0, 1);
        }

        public static RefTransform RotationYRef(double theta)
        {
            var c = Math.Cos(theta);
            var s = Math.Sin(theta);
            return new RefTransform(
                          c, 0, -s, 0,
                          0, 1, 0, 0,
                          s, 0, c, 0,
                          0, 0, 0, 1);
        }

        public static Transform RotationZ(double theta)
        {
            var c = Math.Cos(theta);
            var s = Math.Sin(theta);
            return new Transform(
                          c, s, 0, 0,
                         -s, c, 0, 0,
                          0, 0, 1, 0,
                          0, 0, 0, 1);
        }

        public static RefTransform RotationZRef(double theta)
        {
            var c = Math.Cos(theta);
            var s = Math.Sin(theta);
            return new RefTransform(
                          c, s, 0, 0,
                         -s, c, 0, 0,
                          0, 0, 1, 0,
                          0, 0, 0, 1);
        }

        public static Transform Scale(double s) =>
            new Transform(s, 0, 0, 0,
                          0, s, 0, 0,
                          0, 0, s, 0,
                          0, 0, 0, 1);

        public static RefTransform ScaleRef(double s) =>
            new RefTransform(
                          s, 0, 0, 0,
                          0, s, 0, 0,
                          0, 0, s, 0,
                          0, 0, 0, 1);

        public static Transform Scale(Vector3fd s) =>
            new Transform(s.X, 0, 0, 0,
                          0, s.Y, 0, 0,
                          0, 0, s.Z, 0,
                          0, 0, 0, 1 );

        public static RefTransform ScaleRef(RefVector3f s) =>
            new RefTransform(
                          s.X, 0, 0, 0,
                          0, s.Y, 0, 0,
                          0, 0, s.Z, 0,
                          0, 0, 0, 1);

        public static Transform Scale(double x, double y, double z) =>
            new Transform(x, 0, 0, 0,
                          0, y, 0, 0,
                          0, 0, z, 0,
                          0, 0, 0, 1);

        public static RefTransform ScaleRef(double x, double y, double z) =>
            new RefTransform(
                          x, 0, 0, 0,
                          0, y, 0, 0,
                          0, 0, z, 0,
                          0, 0, 0, 1);


        // translate by 'from'
        // get angle between [+1 forward] and target in forward-left plane
        // rotate around 'up' to align +forward with target in forward-left plane
        // get angle between [+1 forward] and target in forward-up plane
        // rotate around 'left' to align +left with target in forward-up plane
        /// <summary>
        /// Generate a Transform oriented toward 'target' from 'from'
        /// Treats 'Z' Axis as 'up' and 'X' axis as 'forward'
        /// </summary>
        /// <param name="target">Location to orient toward</param>
        /// <param name="from">Origin of resulting transform</param>
        public static Transform LookAt(Vector3fd target, Vector3fd from)
        {
            if (target == from)
                return Translation(from);
            var x = (target - from).Unit(); // forward
            var z = vec(0, 0, 1).Cross(x);  // right
            var y = x.Cross(z);             // up

            return new Transform(
                x.X, x.Y, x.Z, 0,
                z.X, z.Y, z.Z, 0,
                y.X, y.Y, y.Z, 0,                                                
                from.X, from.Y, from.Z, 1);
        }

        public static RefTransform LookAtRef(RefVector3f target, RefVector3f from)
        {
            if (target == from)
                return TranslationRef(from);
            var x = (target - from).Unit(); // forward
            var z = rvec(0, 0, 1).Cross(x); // right
            var y = x.Cross(z);             // up

            return new RefTransform(
                x.X, x.Y, x.Z, 0,
                z.X, z.Y, z.Z, 0,
                y.X, y.Y, y.Z, 0,
                from.X, from.Y, from.Z, 1);
        }
    }
}
