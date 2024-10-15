using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingLib.Math;

namespace UnitTesting.ArtP0rche
{
    public class CalculatorTest
    {
        private readonly BasicCalc _calculator;

        public CalculatorTest()
        {
            _calculator = new BasicCalc();
        }

        [Fact]
        public void Sqrt_ShouldReturnCorrectRoot()
        {
            double result = _calculator.Sqrt(9);
            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData(2, 1.41)]
        [InlineData(2.25, 1.5)]
        [InlineData(65536, 256)]
        public void Sqrt_Theory(double a, double expectedResult)
        {
            double result = _calculator.Sqrt(a);
            Assert.Equal(expectedResult, result, 0.01);
        }

        [Fact]
        public void Sqrt_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Sqrt(-5));
        }

        [Fact]
        public void SolveQuadraticEquation_ShoulReturnCorrectEquationRoots()
        {
            (double?, double?) result = _calculator.SolveQuadraticEquation(2, -5, 2);
            Assert.Equal(2, result.Item1);
            Assert.Equal(0.5, result.Item2);
        }

        [Theory]
        [InlineData(2, -5, 2, 2, 0.5)]
        [InlineData(1, 3, -4, 1, -4)]
        [InlineData(1, -4, 4, 2, 2)]
        public void SolveQuadraticEquation_Theory(double a, double b, double c, double expectedResult1, double expectedResult2)
        {
            (double?, double?) result = _calculator.SolveQuadraticEquation(a, b, c);
            Assert.Equal(expectedResult1, result.Item1);
            Assert.Equal(expectedResult2, result.Item2);
        }

        [Fact]
        public void SolveQuadraticEquation_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.SolveQuadraticEquation(0, 4, 7));
        }
    }
}

