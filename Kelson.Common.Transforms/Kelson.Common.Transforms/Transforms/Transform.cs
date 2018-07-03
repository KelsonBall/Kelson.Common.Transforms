using System;

namespace Kelson.Common.Transforms
{
    public partial struct Transform
    {
        private readonly double
            i1, j1, k1, w1,
            i2, j2, k2, w2,
            i3, j3, k3, w3,
            i4, j4, k4, w4;

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return i1;
                    case 1:
                        return j1;
                    case 2:
                        return k1;
                    case 3:
                        return w1;
                    case 4:
                        return i2;
                    case 5:
                        return j2;
                    case 6:
                        return k2;
                    case 7:
                        return w2;
                    case 8:
                        return i3;
                    case 9:
                        return j3;
                    case 10:
                        return k3;
                    case 11:
                        return w3;
                    case 12:
                        return i4;
                    case 13:
                        return j4;
                    case 14:
                        return k4;
                    case 15:
                        return w4;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public Vector4d RowVector(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4d(i1, j1, k1, w1);
                case 1:
                    return new Vector4d(i2, j2, k2, w2);
                case 2:
                    return new Vector4d(i3, j3, k3, w3);
                case 3:
                    return new Vector4d(i4, j4, k4, w4);
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        public Vector4d ColumnVector(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4d(i1, i2, i3, i4);
                case 1:
                    return new Vector4d(j1, j2, j3, j4);
                case 2:
                    return new Vector4d(k1, k2, k3, k4);
                case 3:
                    return new Vector4d(w1, w2, w3, w4);
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private Transform(double[] row_major_array)
        {
            i1 = row_major_array[0]; j1 = row_major_array[1]; k1 = row_major_array[2]; w1 = row_major_array[3];
            i2 = row_major_array[4]; j2 = row_major_array[5]; k2 = row_major_array[6]; w2 = row_major_array[7];
            i3 = row_major_array[8]; j3 = row_major_array[9]; k3 = row_major_array[10]; w3 = row_major_array[11];
            i4 = row_major_array[12]; j4 = row_major_array[13]; k4 = row_major_array[14]; w4 = row_major_array[15];
        }

        public Transform(double i1, double j1, double k1, double w1,
                          double i2, double j2, double k2, double w2,
                          double i3, double j3, double k3, double w3,
                          double i4, double j4, double k4, double w4)
        {
            this.i1 = i1; this.j1 = j1; this.k1 = k1; this.w1 = w1;
            this.i2 = i2; this.j2 = j2; this.k2 = k2; this.w2 = w2;
            this.i3 = i3; this.j3 = j3; this.k3 = k3; this.w3 = w3;
            this.i4 = i4; this.j4 = j4; this.k4 = k4; this.w4 = w4;
        }

        public Transform Inverse()
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

            return new Transform(
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

        public Transform Multiply(Transform m)
        {
            var c1 = ColumnVector(0);
            var c2 = ColumnVector(1);
            var c3 = ColumnVector(2);
            var c4 = ColumnVector(3);
            var r1 = m.RowVector(0);
            var r2 = m.RowVector(1);
            var r3 = m.RowVector(2);
            var r4 = m.RowVector(3);

            return new Transform(
                i1: c1.Dot(r1), j1: c2.Dot(r1), k1: c3.Dot(r1), w1: c4.Dot(r1),
                i2: c1.Dot(r2), j2: c2.Dot(r2), k2: c3.Dot(r2), w2: c4.Dot(r2),
                i3: c1.Dot(r3), j3: c2.Dot(r3), k3: c3.Dot(r3), w3: c4.Dot(r3),
                i4: c1.Dot(r4), j4: c2.Dot(r4), k4: c3.Dot(r4), w4: c4.Dot(r4));
        }
    }
}

