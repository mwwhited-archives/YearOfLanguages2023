namespace YetAnotherNN
{
    using System;
    using System.Linq;

    public class NeuralNetwork
    {
        private int inputSize;
        private int hiddenLayer1Size;
        private int hiddenLayer2Size;
        private int outputSize;
        private double[][] inputs;
        private double[][] hiddenLayer1Weights;
        private double[][] hiddenLayer2Weights;
        private double[] hiddenLayer1Biases;
        private double[] hiddenLayer2Biases;
        private double[][] outputWeights;
        private double[] outputBiases;
        private double[] hiddenLayer1Outputs;
        private double[] hiddenLayer2Outputs;
        private double[] finalOutputs;
        private double learningRate;

        private readonly Func<double, double> _activationFunction;
        private readonly Func<double, double> _derivativeFunction;

        public NeuralNetwork(
            int inputSize, 
            int hiddenLayer1Size, 
            int hiddenLayer2Size, 
            int outputSize, 
            double learningRate,
            Func<double, double>? activationFunction = null,
            Func<double, double>? derivativeFunction = null
            )
        {
            _activationFunction = activationFunction ?? NNMath.Tanh;
            _derivativeFunction = derivativeFunction ?? NNMath.TanhDerivative;

            this.inputSize = inputSize;
            this.hiddenLayer1Size = hiddenLayer1Size;
            this.hiddenLayer2Size = hiddenLayer2Size;
            this.outputSize = outputSize;
            this.learningRate = learningRate;

            // Initialize weights and biases randomly (you can use more advanced initialization methods).
            InitializeWeightsAndBiases();
        }

        private void InitializeWeightsAndBiases()
        {
            hiddenLayer1Weights = InitializeWeights(hiddenLayer1Size, inputSize);
            hiddenLayer2Weights = InitializeWeights(hiddenLayer2Size, hiddenLayer1Size);
            outputWeights = InitializeWeights(outputSize, hiddenLayer2Size);

            hiddenLayer1Biases = InitializeBiases(hiddenLayer1Size);
            hiddenLayer2Biases = InitializeBiases(hiddenLayer2Size);
            outputBiases = InitializeBiases(outputSize);
        }

        private double[][] InitializeWeights(int rows, int cols)
        {
            Random random = new Random();
            return Enumerable.Range(0, rows)
                .Select(_ => Enumerable.Range(0, cols)
                    .Select(_ => random.NextDouble())
                    .ToArray())
                .ToArray();
        }

        private double[] InitializeBiases(int size)
        {
            Random random = new Random();
            return Enumerable.Range(0, size)
                .Select(_ => random.NextDouble())
                .ToArray();
        }

        public double[][] Train(double[][] trainingData, double[][] targetData, int epochs)
        {
            int dataSize = trainingData.Length;
            double[][] errors = new double[epochs][];

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double[] epochErrors = new double[dataSize];

                for (int i = 0; i < dataSize; i++)
                {
                    // Forward pass
                    var inputs = trainingData[i];
                    hiddenLayer1Outputs = CalculateLayerOutput(inputs, hiddenLayer1Weights, hiddenLayer1Biases);
                    hiddenLayer2Outputs = CalculateLayerOutput(hiddenLayer1Outputs, hiddenLayer2Weights, hiddenLayer2Biases);
                    finalOutputs = CalculateLayerOutput(hiddenLayer2Outputs, outputWeights, outputBiases);

                    // Backpropagation
                    double[] outputErrors = new double[outputSize];
                    for (int j = 0; j < outputSize; j++)
                    {
                        outputErrors[j] = targetData[i][j] - finalOutputs[j];
                    }

                    double[] hiddenLayer2Errors = CalculateErrors(outputErrors, outputWeights, hiddenLayer2Outputs);
                    double[] hiddenLayer1Errors = CalculateErrors(hiddenLayer2Errors, hiddenLayer2Weights, hiddenLayer1Outputs);

                    UpdateWeightsAndBiases(outputErrors, hiddenLayer2Outputs, ref outputWeights, ref outputBiases);
                    UpdateWeightsAndBiases(hiddenLayer2Errors, hiddenLayer1Outputs, ref hiddenLayer2Weights, ref hiddenLayer2Biases);
                    UpdateWeightsAndBiases(hiddenLayer1Errors, inputs, ref hiddenLayer1Weights, ref hiddenLayer1Biases);

                    epochErrors[i] = outputErrors.Sum(err => Math.Pow(err, 2)) / 2.0;
                }

                errors[epoch] = epochErrors;
            }

            return errors;
        }

        private double[] CalculateLayerOutput(double[] inputs, double[][] weights, double[] biases)
        {
            double[] outputs = new double[weights.Length];

            for (int i = 0; i < weights.Length; i++)
            {
                double weightedSum = 0;
                for (int j = 0; j < weights[i].Length; j++)
                {
                    weightedSum += weights[i][j] * inputs[j];
                }
                outputs[i] = _activationFunction(weightedSum + biases[i]);
            }

            return outputs;
        }


        private double[] CalculateErrors(
            double[] nextLayerErrors,
            double[][] nextLayerWeights, 
            double[] currentLayerOutputs
            )
        {
            double[] errors = new double[currentLayerOutputs.Length];

            for (int i = 0; i < currentLayerOutputs.Length; i++)
            {
                double weightedErrorSum = 0;
                for (int j = 0; j < nextLayerErrors.Length; j++)
                {
                    weightedErrorSum += nextLayerErrors[j] * nextLayerWeights[j][i];
                }
                errors[i] = weightedErrorSum * _derivativeFunction(currentLayerOutputs[i]);
            }

            return errors;
        }

        private void UpdateWeightsAndBiases(double[] errors, double[] inputs, ref double[][] weights, ref double[] biases)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    weights[i][j] += learningRate * errors[i] * inputs[j];
                }
                biases[i] += learningRate * errors[i];
            }
        }

        public double[] Predict(double[] inputData)
        {
            double[] hiddenLayer1Outputs = CalculateLayerOutput(inputData, hiddenLayer1Weights, hiddenLayer1Biases);
            double[] hiddenLayer2Outputs = CalculateLayerOutput(hiddenLayer1Outputs, hiddenLayer2Weights, hiddenLayer2Biases);
            double[] finalOutputs = CalculateLayerOutput(hiddenLayer2Outputs, outputWeights, outputBiases);

            return finalOutputs;
        }
    }
}