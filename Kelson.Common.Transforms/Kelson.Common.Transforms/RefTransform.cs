using Kelson.Common.Vectors;
using System;
using System.Runtime.InteropServices;

namespace Kelson.Common.Transforms
{
    [StructLayout(LayoutKind.Explicit)]
    public readonly ref partial struct RefTransform
    {
        [FieldOffset(0)]
        public readonly float _i1;
        [FieldOffset(4)]
        public readonly float _j1;
        [FieldOffset(8)]
        public readonly float _k1;
        [FieldOffset(12)]
        public readonly float _w1;
        [FieldOffset(16)]
        public readonly float _i2;
        [FieldOffset(20)]
        public readonly float _j2;
        [FieldOffset(24)]
        public readonly float _k2;
        [FieldOffset(28)]
        public readonly float _w2;
        [FieldOffset(32)]
        public readonly float _i3;
        [FieldOffset(36)]
        public readonly float _j3;
        [FieldOffset(40)]
        public readonly float _k3;
        [FieldOffset(44)]
        public readonly float _w3;
        [FieldOffset(48)]
        public readonly float _i4;
        [FieldOffset(52)]
        public readonly float _j4;
        [FieldOffset(56)]
        public readonly float _k4;
        [FieldOffset(60)]
        public readonly float _w4;

        public double i1 => _i1;
        public double j1 => _j1;
        public double k1 => _k1;
        public double w1 => _w1;
        public double i2 => _i2;
        public double j2 => _j2;
        public double k2 => _k2;
        public double w2 => _w2;
        public double i3 => _i3;
        public double j3 => _j3;
        public double k3 => _k3;
        public double w3 => _w3;
        public double i4 => _i4;
        public double j4 => _j4;
        public double k4 => _k4;
        public double w4 => _w4;

        public unsafe double this[int index]
        {
            get
            {
                fixed (RefTransform* t = &this)
                    return *((float*)((void*)t) + index);
            }
        }

        public RefVector4f this[MRow row] => RowVectorRef((int)row);
        public RefVector4f this[MColumn col] => ColumnVectorRef((int)col);

        public enum MRow { one = 0, two = 1, three = 2, four = 3 }
        public enum MColumn { i = 0, j = 1, k = 2, w = 3 }

        public Vector4fd RowVector(MRow row) => RowVector((int)row);
        public Vector4fd RowVector(int index) => index switch
        {
            0 => new Vector4fd(_i1, _j1, _k1, _w1),
            1 => new Vector4fd(_i2, _j2, _k2, _w2),
            2 => new Vector4fd(_i3, _j3, _k3, _w3),
            3 => new Vector4fd(_i4, _j4, _k4, _w4),
            _ => throw new IndexOutOfRangeException(),
        };

        public Vector4fd ColumnVector(MColumn col) => ColumnVector((int)col);
        public Vector4fd ColumnVector(int index) => index switch
        {
            0 => new Vector4fd(_i1, _i2, _i3, _i4),
            1 => new Vector4fd(_j1, _j2, _j3, _j4),
            2 => new Vector4fd(_k1, _k2, _k3, _k4),
            3 => new Vector4fd(_w1, _w2, _w3, _w4),
            _ => throw new IndexOutOfRangeException(),
        };

        public RefVector4f RowVectorRef(MRow row) => RowVectorRef((int)row);
        public RefVector4f RowVectorRef(int index) => index switch
        {
            0 => new RefVector4f(_i1, _j1, _k1, _w1),
            1 => new RefVector4f(_i2, _j2, _k2, _w2),
            2 => new RefVector4f(_i3, _j3, _k3, _w3),
            3 => new RefVector4f(_i4, _j4, _k4, _w4),
            _ => throw new IndexOutOfRangeException(),
        };

        public RefVector4f ColumnVectorRef(MColumn col) => ColumnVectorRef((int)col);
        public RefVector4f ColumnVectorRef(int index) => index switch
        {
            0 => new RefVector4f(_i1, _i2, _i3, _i4),
            1 => new RefVector4f(_j1, _j2, _j3, _j4),
            2 => new RefVector4f(_k1, _k2, _k3, _k4),
            3 => new RefVector4f(_w1, _w2, _w3, _w4),
            _ => throw new IndexOutOfRangeException(),
        };


        public static RefTransform FromRowMajorArray(double[] row_major_array) =>
            new RefTransform(
                i1: row_major_array[0], j1: row_major_array[1], k1: row_major_array[2], w1: row_major_array[3],
                i2: row_major_array[4], j2: row_major_array[5], k2: row_major_array[6], w2: row_major_array[7],
                i3: row_major_array[8], j3: row_major_array[9], k3: row_major_array[10], w3: row_major_array[11],
                i4: row_major_array[12], j4: row_major_array[13], k4: row_major_array[14], w4: row_major_array[15]);

        public static RefTransform FromRowMajorArray(float[] row_major_array) =>
            new RefTransform(
                i1: row_major_array[0], j1: row_major_array[1], k1: row_major_array[2], w1: row_major_array[3],
                i2: row_major_array[4], j2: row_major_array[5], k2: row_major_array[6], w2: row_major_array[7],
                i3: row_major_array[8], j3: row_major_array[9], k3: row_major_array[10], w3: row_major_array[11],
                i4: row_major_array[12], j4: row_major_array[13], k4: row_major_array[14], w4: row_major_array[15]);

        public RefTransform(
                double i1, double j1, double k1, double w1,
                double i2, double j2, double k2, double w2,
                double i3, double j3, double k3, double w3,
                double i4, double j4, double k4, double w4)
            : this(
                i1: (float)i1, j1: (float)j1, k1: (float)k1, w1: (float)w1,
                i2: (float)i2, j2: (float)j2, k2: (float)k2, w2: (float)w2,
                i3: (float)i3, j3: (float)j3, k3: (float)k3, w3: (float)w3,
                i4: (float)i4, j4: (float)j4, k4: (float)k4, w4: (float)w4)
        {
        }

        public static unsafe explicit operator RefTransform(Transform v) => *(RefTransform*)&v;         

        public RefTransform(
            float i1, float j1, float k1, float w1,
            float i2, float j2, float k2, float w2,
            float i3, float j3, float k3, float w3,
            float i4, float j4, float k4, float w4)
        {
            _i1 = i1; _j1 = j1; _k1 = k1; _w1 = w1;
            _i2 = i2; _j2 = j2; _k2 = k2; _w2 = w2;
            _i3 = i3; _j3 = j3; _k3 = k3; _w3 = w3;
            _i4 = i4; _j4 = j4; _k4 = k4; _w4 = w4;
        }

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (RefTransform* t = &this)
                return new ReadOnlySpan<float>(t, 16);
        }

        public override string ToString()
        {
            var x = this * VectorConstruction.rvec(1, 0, 0);
            var y = this * VectorConstruction.rvec(0, 1, 0);
            var z = this * VectorConstruction.rvec(0, 0, 1);
            return $"[x:{x},y:{y},z:{z}]";
        }
    }    
}
