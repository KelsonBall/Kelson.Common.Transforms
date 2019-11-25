using Kelson.Common.Vectors;

namespace Kelson.Common.Transforms
{
    public readonly partial struct RefTransform
    {
        public RefTransform Inverse()
        {
            var m = this;
            var s0 = m.i1 * m.j2 - m.i2 * m.j1;
            var s1 = m.i1 * m.k2 - m.i2 * m.k1;
            var s2 = m.i1 * m.w2 - m.i2 * m.w1;
            var s3 = m.j1 * m.k2 - m.j2 * m.k1;
            var s4 = m.j1 * m.w2 - m.j2 * m.w1;
            var s5 = m.k1 * m.w2 - m.k2 * m.w1;
            var c5 = m.k3 * m.w4 - m.k4 * m.w3;
            var c4 = m.j3 * m.w4 - m.j4 * m.w3;
            var c3 = m.j3 * m.k4 - m.j4 * m.k3;
            var c2 = m.i3 * m.w4 - m.i4 * m.w3;
            var c1 = m.i3 * m.k4 - m.i4 * m.k3;
            var c0 = m.i3 * m.j4 - m.i4 * m.j3;
            var d = 1.0 / (s0 * c5 - s1 * c4 + s2 * c3 + s3 * c2 - s4 * c1 + s5 * c0);

            return new RefTransform(
                i1: (m.j2 * c5 - m.k2 * c4 + m.w2 * c3) * d,
                j1: (-m.j1 * c5 + m.k1 * c4 - m.w1 * c3) * d,
                k1: (m.j4 * s5 - m.k4 * s4 + m.w4 * s3) * d,
                w1: (-m.j3 * s5 + m.k3 * s4 - m.w3 * s3) * d,
                i2: (-m.i2 * c5 + m.k2 * c2 - m.w2 * c1) * d,
                j2: (m.i1 * c5 - m.k1 * c2 + m.w1 * c1) * d,
                k2: (-m.i4 * s5 + m.k4 * s2 - m.w4 * s1) * d,
                w2: (m.i3 * s5 - m.k3 * s2 + m.w3 * s1) * d,
                i3: (m.i2 * c4 - m.j2 * c2 + m.w2 * c0) * d,
                j3: (-m.i1 * c4 + m.j1 * c2 - m.w1 * c0) * d,
                k3: (m.i4 * s4 - m.j4 * s2 + m.w4 * s0) * d,
                w3: (-m.i3 * s4 + m.j3 * s2 - m.w3 * s0) * d,
                i4: (-m.i2 * c3 + m.j2 * c1 - m.k2 * c0) * d,
                j4: (m.i1 * c3 - m.j1 * c1 + m.k1 * c0) * d,
                k4: (-m.i4 * s3 + m.j4 * s1 - m.k4 * s0) * d,
                w4: (m.i3 * s3 - m.j3 * s1 + m.k3 * s0) * d);
        }

        public RefTransform Transpose() =>
            new RefTransform(
                i1: _i1, j1: _i2, k1: _i3, w1: _i4,
                i2: _j1, j2: _j2, k2: _j3, w2: _j4,
                i3: _k1, j3: _k2, k3: _k3, w3: _k4,
                i4: _w1, j4: _w2, k4: _w3, w4: _w4);

        public RefTransform Multiply(in Transform m)
        {
            var c1 = ColumnVectorRef(0);
            var c2 = ColumnVectorRef(1);
            var c3 = ColumnVectorRef(2);
            var c4 = ColumnVectorRef(3);
            var r1 = m.RowVectorRef(0);
            var r2 = m.RowVectorRef(1);
            var r3 = m.RowVectorRef(2);
            var r4 = m.RowVectorRef(3);

            return new RefTransform(
                i1: c1.Dot(r1), j1: c2.Dot(r1), k1: c3.Dot(r1), w1: c4.Dot(r1),
                i2: c1.Dot(r2), j2: c2.Dot(r2), k2: c3.Dot(r2), w2: c4.Dot(r2),
                i3: c1.Dot(r3), j3: c2.Dot(r3), k3: c3.Dot(r3), w3: c4.Dot(r3),
                i4: c1.Dot(r4), j4: c2.Dot(r4), k4: c3.Dot(r4), w4: c4.Dot(r4));
        }

        public RefTransform Multiply(RefTransform m)
        {
            var c1 = ColumnVectorRef(0);
            var c2 = ColumnVectorRef(1);
            var c3 = ColumnVectorRef(2);
            var c4 = ColumnVectorRef(3);
            var r1 = m.RowVectorRef(0);
            var r2 = m.RowVectorRef(1);
            var r3 = m.RowVectorRef(2);
            var r4 = m.RowVectorRef(3);

            return new RefTransform(
                i1: c1.Dot(r1), j1: c2.Dot(r1), k1: c3.Dot(r1), w1: c4.Dot(r1),
                i2: c1.Dot(r2), j2: c2.Dot(r2), k2: c3.Dot(r2), w2: c4.Dot(r2),
                i3: c1.Dot(r3), j3: c2.Dot(r3), k3: c3.Dot(r3), w3: c4.Dot(r3),
                i4: c1.Dot(r4), j4: c2.Dot(r4), k4: c3.Dot(r4), w4: c4.Dot(r4));
        }

        /// <summary>
        /// Adds two matricies together
        /// Same as Add but returns a RefTransform
        /// </summary>        
        public RefTransform Add(in Transform m) =>
            new RefTransform(
                i1 + m.i1, j1 + m.j1, k1 + m.k1, w1 + m.w1,
                i2 + m.i2, j2 + m.j2, k2 + m.k2, w2 + m.w2,
                i3 + m.i3, j3 + m.j3, k3 + m.k3, w3 + m.w3,
                i4 + m.i4, j4 + m.j4, k4 + m.k4, w4 + m.w4);

        /// <summary>
        /// Adds two matricies together
        /// Same as Add but returns a RefTransform
        /// </summary>        
        public RefTransform Add(RefTransform m) =>
            new RefTransform(
                i1 + m.i1, j1 + m.j1, k1 + m.k1, w1 + m.w1,
                i2 + m.i2, j2 + m.j2, k2 + m.k2, w2 + m.w2,
                i3 + m.i3, j3 + m.j3, k3 + m.k3, w3 + m.w3,
                i4 + m.i4, j4 + m.j4, k4 + m.k4, w4 + m.w4);               

        public RefVector3f AppliedTo(in Vector3fd v)
        {
            var a = new RefVector4f(v.X, v.Y, v.Z, 1);
            return new RefVector3f(this[MColumn.i].Dot(a), this[MColumn.j].Dot(a), this[MColumn.k].Dot(a));
        }

        public RefVector3f AppliedTo(RefVector3f v)
        {
            var a = new RefVector4f(v.X, v.Y, v.Z, 1);
            return new RefVector3f(this[MColumn.i].Dot(a), this[MColumn.j].Dot(a), this[MColumn.k].Dot(a));
        }

        public RefVector4f AppliedTo(in Vector4fd v) =>
            new RefVector4f(this[MColumn.i].Dot(v), this[MColumn.j].Dot(v), this[MColumn.k].Dot(v), this[MColumn.w].Dot(v));

        public RefVector4f AppliedTo(RefVector4f v) =>
            new RefVector4f(this[MColumn.i].Dot(v), this[MColumn.j].Dot(v), this[MColumn.k].Dot(v), this[MColumn.w].Dot(v));

        /// <summary>
        /// Copy into non ref struct representation
        /// </summary>        
        public Transform Deref() =>
            new Transform(_i1, _j1, _k1, _w1,
                          _i2, _j2, _k2, _w2,
                          _i3, _j3, _k3, _w3,
                          _i4, _j4, _k4, _w4);

        public static RefTransform operator -(RefTransform a, RefTransform b) => a.Add(-b);
        public static RefTransform operator -(RefTransform a) =>
            new RefTransform(
                -a._i1, -a._j1, -a._k1, -a._w1,
                -a._i2, -a._j2, -a._k2, -a._w2,
                -a._i3, -a._j3, -a._k3, -a._w3,
                -a._i4, -a._j4, -a._k4, -a._w4);
        public static RefTransform operator +(RefTransform a, Transform b) => a.Add(in b);
        public static RefTransform operator +(Transform a, RefTransform b) => a.AddRef(b);
        public static RefTransform operator *(RefTransform a, RefTransform b) => a.Multiply(b);
        public static RefTransform operator *(Transform a, RefTransform b) => a.MultiplyRef(b);
        public static RefTransform operator *(RefTransform a, double s) => a * Transform.ScaleRef(s);
        public static RefTransform operator *(double s, RefTransform b) => b * Transform.ScaleRef(s);
        public static RefVector3f operator *(RefTransform a, RefVector3f v) => a.AppliedTo(v);
        public static RefVector4f operator *(RefTransform a, RefVector4f v) => a.AppliedTo(v);
        public static RefVector3f operator *(RefVector3f v, RefTransform a) => a.AppliedTo(v);
        public static RefVector4f operator *(RefVector4f v, RefTransform a) => a.AppliedTo(v);
        public static Vector3fd operator *(RefTransform a, Vector3fd v) => a.AppliedTo(v);
        public static Vector4fd operator *(RefTransform a, Vector4fd v) => a.AppliedTo(v);
        public static Vector3fd operator *(Vector3fd v, RefTransform a) => a.AppliedTo(v);
        public static Vector4fd operator *(Vector4fd v, RefTransform a) => a.AppliedTo(v);
    }
}
