namespace YetAnotherNN
{
    using System;

    public class NNMath
    {

        public static double Sigmoid(double x) => 1 / (1 + Math.Exp(-x));
        public static double SigmoidDerivative(double x) => Sigmoid(x) * (1 - Sigmoid(x));

        public static double Tanh(double x) => Math.Tanh(x);
        public static double TanhDerivative(double x) => 1 - Tanh(x) * Tanh(x);
    }
}