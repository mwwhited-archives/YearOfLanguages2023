using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace AnotherModel;

internal class Program
{
    static void Main(string[] args)
    {
        // https://www.3blue1brown.com/lessons/backpropagation
        var nn = new NeuralNetwork(2, new[] { 4, }, 3, new Random(0));

        var inputs = new[]
        {
          new {inputs=  new []{ 0.0, 0.0}, expected = new [] {0.0,0.0,0.0 } },
          new {inputs=  new []{ 0.0, 1.0}, expected = new [] {0.0,1.0,1.0 } },
          new {inputs=  new []{ 1.0, 0.0}, expected = new [] {0.0,1.0,1.0 } },
          new {inputs=  new []{ 1.0, 1.0}, expected = new [] {1.0,1.0,0.0 } },
        };

        var results = inputs
            //.Select(i => nn.Train(i.inputs.Select(x => -2 * x - 1).ToArray(), i.expected.Select(x => -2 * x - 1).ToArray()))
            .Select(i => nn.Train(i.inputs, i.expected))
            .ToArray();
        var losses = Enumerable.Range(0, nn.Outputs)
            .Select(i => results.Sum(r => r.losses.ElementAt(i)))
            .ToArray();

        // output = 
        /*
         * -> inputs[..] -> layer[..] -> output[..] 
         * 
         * map(inputs -> layerIndex -> 
         * tanh(
         *  sum(inputIndex=>input[inputIndex] * weight[inputIndex]+ bias)
         *  )
         *  ) -> [layerIndex]
         *  
         * map(inputs -> layerIndex -> 
         * tanh(
         *  sum(inputIndex=>input[inputIndex] * weight[inputIndex]+ bias)
         *  )
         *  ) -> [layerIndex]
         *  
         *  https://www.3blue1brown.com/lessons/backpropagation
         *  https://www.3blue1brown.com/lessons/backpropagation-calculus
         *  
         */

        //var losses = new List<double>();
        //foreach (var input in inputs)
        //{
        //    var computed = nn.Compute(input.inputs);
        //    var loss = input.expected.Zip(computed).Sum(i => 2 * Math.Pow(i.First - i.Second, 2.0));
        //    losses.Add(loss);
        //    Console.WriteLine($"{string.Join(" ", input.inputs)} => {string.Join(" ", computed)} ({loss})");
        //}
        //Console.WriteLine($"err = {losses.Average()}");

        //var weights = nn.GetWeights();
        //var json = JsonSerializer.Serialize(weights);
        //var read = JsonSerializer.Deserialize<double[][][]>(json);
        //var ann = new NeuralNetwork(read);
    }
}


public class NeuralNetwork
{
    private readonly Layer[] _layers;

    public NeuralNetwork(
        int inputs,
        int[] layers,
        int output,
        Random? random = null
        )
    {
        random = new Random();
        _layers = new[] { inputs }.Concat(layers).Zip(
            layers.Concat(new[] { output })
            ).Select(l => new Layer(l.First, l.Second, random)).ToArray();
    }

    public NeuralNetwork(double[][][] weights) =>
        _layers = weights.Select(l => new Layer(l)).ToArray();

    public Layer[] Layers => _layers;
    public int Outputs => Layers.Last().Neurons.Length;

    public double[] Compute(double[] inputs) =>
        _layers.Aggregate(
            inputs,
            (input, layer) => layer.Compute(input),
            output => output
            );

    public (double[] results, double[][] layers, double[] expected, double[] losses) Train(double[] inputs, double[] expected)
    {
        var temp = new List<double[]>();
        var current = inputs;
        foreach (var layer in _layers)
        {
            temp.Add(current = layer.Compute(current));
        }

        return (
            current, 
            temp.ToArray(), 
            expected, 
            expected.Zip(current).Select(i => 2 * Math.Pow(i.First - i.Second, 2)).ToArray()
            );
    }

    public double[][][] GetWeights() =>
        _layers.Select(n => n.GetWeights()).ToArray();

}
