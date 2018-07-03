using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Transforms
{
    public partial struct Vector4d : IVector<Vector4d>
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public readonly double W;

        public Vector4d(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(Vector4d other) => (X * other.X) + (Y * other.Y) + (Z * other.Z) + (W * other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4d Add(Vector4d other) => new Vector4d(X + other.X, Y + other.Y, Z + other.Z, W + other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4d Sub(Vector4d other) => new Vector4d(X - other.X, Y - other.Y, Z - other.Z, W - other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4d Scale(double scalar) => new Vector4d(X * scalar, Y * scalar, Z * scalar, W * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4d Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(Vector4d other) => Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()) );      
    }

    public interface IVector<TSelf>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double MagnitudeSquared();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Magnitude();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Dot(TSelf other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TSelf Add(TSelf other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TSelf Sub(TSelf other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TSelf Scale(double scalar);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TSelf Unit();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        double Angle(TSelf other);
    }
}
