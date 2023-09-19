namespace YetAnotherNN
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the neural network parameters
            int inputSize = 2;
            int hiddenLayer1Size = 4;
            int hiddenLayer2Size = 4;
            int outputSize = 1;
            double learningRate = 0.1;
            int epochs = 10000;

            // Create the neural network
            NeuralNetwork neuralNetwork = new NeuralNetwork(inputSize, hiddenLayer1Size, hiddenLayer2Size, outputSize, learningRate);
        }
    }
}