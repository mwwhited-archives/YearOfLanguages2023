namespace AnotherModel;

public class Layer
{
    private readonly Neuron[] _neurons;

    public Layer(int inputs, int nodes, Random random) :
        this(Enumerable.Range(0, nodes).Select(_ => new Neuron(inputs, random)).ToArray())
    {
    }

    public Layer(Neuron[] neurons) =>
        _neurons = neurons;

    public Layer(double[][] weights) :
        this(weights.Select(Neuron.Create).ToArray())
    {
    }

    public Neuron[] Neurons => _neurons;

    public double[] Compute(double[] inputs) =>
        _neurons.Select(n => n.Compute(inputs)).ToArray();

    public double[][] GetWeights() =>
        _neurons.Select(n => n.GetWeights()).ToArray();
}
