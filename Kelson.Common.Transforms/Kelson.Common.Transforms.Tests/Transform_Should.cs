using Xunit;
using Kelson.Common.Vectors;
using FluentAssertions;
using System;
using FluentAssertions.Primitives;
using static Kelson.Common.Vectors.VectorConstruction;
using static Kelson.Common.Transforms.Transform;
using System.Collections.Generic;
using System.Linq;

namespace Kelson.Common.Transforms.Tests
{
    public static class ShouldExtensions
    {
        public static void BeAproximatelyEquivalentTo(this ObjectAssertions should, Vector3fd v, double tolerance = float.Epsilon)
        {
            var subject = (Vector3fd)should.Subject;
            subject.X.Should().BeApproximately(v.X, tolerance);
            subject.Y.Should().BeApproximately(v.Y, tolerance);
            subject.Z.Should().BeApproximately(v.Z, tolerance);
        }

        public static bool IsAproximatelyEquivalentTo(this Vector3fd subject, Vector3fd v, double tolerance = float.Epsilon) =>
                subject.X.IsAproximately(v.X, tolerance)
             && subject.Y.IsAproximately(v.Y, tolerance)
             && subject.Z.IsAproximately(v.Z, tolerance);

        public static bool IsAproximately(this double value, double other, double tolerance) => Math.Abs(value - other) <= tolerance;
    }

    public class Transform_Should
    {
        const double TEST_EPSILON = 1e-16;

        [Fact]
        public void TranslateCoordinates()
        {
            var t = Translation(1, 2, 3);
            t.AppliedTo(vec(0, 0, 0))
             .Should()
             .BeEquivalentTo(vec(1, 2, 3));
            t.Inverse()
             .AppliedTo(vec(1, 2, 3))
             .Should()
             .BeAproximatelyEquivalentTo(vec(0, 0, 0));
        }

        [Fact]
        public void RotateCoordinatesAroundZAxis()
        {
            var t = RotationZ(Math.PI / 2);
            t.AppliedTo(vec(1, 0, 0))
             .Should()
             .BeAproximatelyEquivalentTo(vec(0, 1, 0), TEST_EPSILON);
            t.Inverse()
             .AppliedTo(vec(0, 1, 0))
             .Should()
             .BeAproximatelyEquivalentTo(vec(1, 0, 0), TEST_EPSILON);
        }

        [Fact]
        public void RotateCoordinatesAroundYAxis()
        {
            var t = RotationY(Math.PI / 2);
            t.AppliedTo(vec(1, 0, 0))
             .Should()
             .BeAproximatelyEquivalentTo(vec(0, 0, -1), TEST_EPSILON);
            t.Inverse()
             .AppliedTo(vec(0, 0, -1))
             .Should()
             .BeAproximatelyEquivalentTo(vec(1, 0, 0), TEST_EPSILON);
        }

        [Fact]
        public void RotateCoordinatesAroundXAxis()
        {
            var t = RotationX(Math.PI / 2);
            t.AppliedTo(vec(0, 1, 0))
             .Should()
             .BeAproximatelyEquivalentTo(vec(0, 0, 1), TEST_EPSILON);
            t.Inverse()
             .AppliedTo(vec(0, 0, 1))
             .Should()
             .BeAproximatelyEquivalentTo(vec(0, 1, 0), TEST_EPSILON);
        }

        [Fact]
        public void ScaleCoordinates()
        {
            var t = Scale(2);
            t.AppliedTo(vec(1, 2, 3))
             .Should()
             .BeAproximatelyEquivalentTo(vec(2, 4, 6));
            t.Inverse()
             .AppliedTo(vec(2, 4, 6))
             .Should()
             .BeAproximatelyEquivalentTo(vec(1, 2, 3));
        }

        [Fact]
        public void LookAtPoint()
        {            
            var look_at = LookAt(target: vec(1, 1, 0), from: vec(1, 0, 0));

            var manual = Translation(1, 0, 0) * RotationZ(Math.PI / 2);

            var calculated_look_at = new Vector3fd[]
            {
                look_at * vec(0, 0, 0),  // [0] origin
                look_at * vec(1, 0, 0),  // [1] forward
                look_at * vec(-1, 0, 0), // [2] back
                look_at * vec(0, 1, 0),  // [3] up
                look_at * vec(0, -1, 0), // [4] down
                look_at * vec(0, 0, 1),  // [5] left
                look_at * vec(0, 0, -1)  // [6] right
            };

            var calculated_manual = new Vector3fd[]
            {
                manual * vec(0, 0, 0),  // [0] origin 
                manual * vec(1, 0, 0),  // [1] forward
                manual * vec(-1, 0, 0), // [2] back
                manual * vec(0, 1, 0),  // [3] up
                manual * vec(0, -1, 0), // [4] down
                manual * vec(0, 0, 1),  // [5] left
                manual * vec(0, 0, -1)  // [6] right
            };

            for (int i = 0; i < calculated_look_at.Length; i++)
                calculated_look_at[i].Should().BeAproximatelyEquivalentTo(calculated_manual[i], TEST_EPSILON);
        }

        [Fact]
        public void LookAtPoints()
        {
            var coords = new float[] { -2f, -1f, -.5f, .5f, 1f, 2f };

            IEnumerable<Vector3fd> allVecsFromCoords()
            {
                foreach (var x in coords)
                    foreach (var y in coords)
                        foreach (var z in coords)                                                    
                            yield return vec(x, y, z);                        
            }

            double ran = 0;
            double failed = 0;
            List<(Vector3fd direction, Vector3fd miss)> failures = new List<(Vector3fd, Vector3fd)>();
            foreach (var from in allVecsFromCoords())
            {
                foreach (var target in allVecsFromCoords())
                {
                    if (from != target || true)
                    {                        
                        var result = LookAt(target: target, from: from) * vec((float)(target - from).Magnitude(), 0, 0);
                        //result.Should().BeAproximatelyEquivalentTo(target, 0.01);
                        ran++;
                        if (!result.IsAproximatelyEquivalentTo(target, 0.5e-6))
                        {
                            failed++;
                            failures.Add((target - from, target - result));
                        }
                    }
                }
            }

            if (failed > 0)
            {
                var percent = 1 - failed / ran;
                throw new Exception($"Expected 100% of transforms to be correct, but got {(percent * 100).ToString("##0.00")}%\nFirst 10 failures:\n{string.Join('\n', failures.Take(10))}");
            }
        }
    }
}
