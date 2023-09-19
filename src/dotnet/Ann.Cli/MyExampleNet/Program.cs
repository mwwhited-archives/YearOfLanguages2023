using System.Reflection.Emit;
using System.Text;

namespace MyExampleNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ann = new NeuralNetwork(
                2,
                new[] { 4 },
                3,
                seed: 0
                );

            var dataSet = new[] {
                new []{0.0,0.0 },
                new []{0.0,1.0 },
                new []{1.0,0.0 },
                new []{1.0,1.0 },
            };

            foreach (var data in dataSet)
            {
                Console.WriteLine($"{string.Join(" ", data)} -> {string.Join(" ", ann.Compute(data))}");
            }
        }
    }

    public class TrainingNode : Node
    {
        protected readonly Stack<(double[] inputs, double output, double[] expected)> _captured = new();
        protected double[] Expected { get; set; }

        public TrainingNode(double[] weights, double bias) : base(weights, bias)
        {
        }

        public override double Compute(double[] inputs)
        {
            var result = base.Compute(inputs);
            _captured.Push((inputs, result, Expected));
            return result;
        }
    }

    public class Node
    {
        protected readonly double[] _weights;
        protected double _bias;

        public Node(
            double[] weights,
            double bias
            )
        {
            _weights = weights;
            _bias = bias;
        }

        public virtual double Compute(double[] inputs) =>
           Math.Tanh(inputs.Zip(_weights).Sum(v => v.First * v.Second) + _bias);

        public double[] Weights => _weights;
        public double Bias => _bias;

        public override string ToString() =>
            $"w= {string.Join(" ", _weights)} b= {_bias}";
    }

    public class Layer
    {
        private readonly Node[] _nodes;

        public Layer(
            Node[] nodes,
            bool capture = false
            )
        {
            _nodes = nodes;
        }

        public double[] Compute(double[] inputs) =>
            _nodes.Select(n => n.Compute(inputs)).ToArray();

        public override string ToString() =>
            _nodes.Aggregate(
                new StringBuilder(),
                (sb, v) => sb.AppendLine(v.ToString()),
                sb => sb.ToString()
                );
    }

    public class NeuralNetwork
    {
        private readonly int _inputs;
        private readonly List<Layer> _layers = new();

        public NeuralNetwork(
            int inputs,
            int[] hidden,
            int output,
            int? seed = null
            )
        {
            _inputs = inputs;

            var rnd = seed.HasValue ? new Random(seed.Value) : new Random();

            var lastLayer = inputs;
            foreach (var layer in hidden.Concat(new[] { output }))
            {
                Console.WriteLine("l= ");
                var generateWeights = Enumerable.Range(0, lastLayer).Select(_ => rnd.NextDouble());
                var nodes = Enumerable.Range(0, layer).Select(_ => new TrainingNode(generateWeights.ToArray(), rnd.NextDouble())).ToArray();
                var l = new Layer(nodes);
                Console.WriteLine(l);
                _layers.Add(l);
            }
        }


        public double[] Compute(double[] inputs)
        {
            if (inputs.Length != _inputs)
                throw new NotSupportedException();

            var current = inputs;
            foreach (var layer in _layers)
            {
                Console.WriteLine("i= " + string.Join(" ", current));
                current = layer.Compute(current);
            }

            Console.WriteLine("o= " + string.Join(" ", current));
            return current;
        }
    }
}