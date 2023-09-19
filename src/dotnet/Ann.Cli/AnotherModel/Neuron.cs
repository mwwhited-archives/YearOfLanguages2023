namespace AnotherModel;

public class Neuron
{
    private readonly double[] _weights;
    private readonly double _bias;

    public Neuron(int inputs, Random random) :
        this(
            random.NextDouble(),
            Enumerable.Range(0, inputs).Select(_ => random.NextDouble()).ToArray())
    {
    }

    public Neuron(double bias, double[] weights)
    {
        _weights = weights;
        _bias = bias;
    }

    public double[] Weights => _weights;
    public double Bias => _bias;

    public double Compute(double[] inputs) =>
        inputs.Length == _weights.Length ?
            Math.Tanh(inputs.Zip(_weights).Sum(i => i.First * i.Second) + _bias) :
            throw new NotSupportedException();

    public double[] GetWeights() =>
        new[] { _bias }.Concat(Weights).ToArray();

    public static Neuron Create(double[] weights) =>
        new Neuron(weights.FirstOrDefault(), weights.Skip(1).ToArray());

}