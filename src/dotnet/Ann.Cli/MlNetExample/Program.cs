//using Microsoft.ML;

//namespace MlNetExample
//{
//    using System;
//    using System.Linq;
//    using Microsoft.ML;
//    using Microsoft.ML.Data;

//    class Program
//    {
//        // Define your data classes
//        public class InputData
//        {
//            [LoadColumn(0)] public float Feature1;
//            [LoadColumn(1)] public float Feature2;
//        }

//        public class OutputData
//        {
//            public float[] Scores;
//        }

//        public class ModelPrediction
//        {
//            [ColumnName("PredictedLabel")]
//            public uint PredictedClass;
//        }

//        static void Main(string[] args)
//        {

//            //internal class Program
//            //{
//            //    static void Main(string[] args)
//            //    {
//            //        var ml = new MLContext();
//            //        var trainingData = ml.Data.LoadFromEnumerable(new[]
//            //        {
//            //            new PredictionDataModel{ A = 0.0, B = 0.0, And = 0.0, Or = 0.0, Xor = 0.0 },
//            //            new PredictionDataModel{ A = 0.0, B = 1.0, And = 0.0, Or = 1.0, Xor = 1.0 },
//            //            new PredictionDataModel{ A = 1.0, B = 0.0, And = 0.0, Or = 1.0, Xor = 1.0 },
//            //            new PredictionDataModel{ A = 1.0, B = 1.0, And = 1.0, Or = 1.0, Xor = 0.0 },
//            //        });
//            //        //ml.Transforms.CopyColumns()
//            //        //Console.WriteLine("Hello, World!");
//            //    }
//            //}

//            // Create a new MLContext
//            var context = new MLContext();

//            // Load your data from a text file or any other data source
//            var data = context.Data.LoadFromTextFile<InputData>("your_data_file.txt", separatorChar: ',');

//            // Define your data preprocessing pipeline
//            var pipeline = context.Transforms.Conversion.MapValueToKey("Label")
//                .Append(context.Transforms.Concatenate("Features", "Feature1", "Feature2"))
//                .Append(context.Transforms.NormalizeMinMax("Features"))
//                .Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

//            // Create the neural network model
//            var trainer = context.MulticlassClassification.Trainers.FeedForwardWithTwoHiddenLayers(
//                hiddenLayerSize: new int[] { 4, 4 },
//                numberOfIterations: 100);

//            var trainingPipeline = pipeline.Append(trainer)
//                .Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

//            // Train the model
//            var model = trainingPipeline.Fit(data);

//            // Make predictions
//            var predictions = model.Transform(data);

//            // Evaluate the model's performance (if you have a test dataset)
//            var metrics = context.MulticlassClassification.Evaluate(predictions);

//            Console.WriteLine($"Log-loss: {metrics.LogLoss}");
//            Console.WriteLine($"Per-class log-loss:");
//            for (int i = 0; i < metrics.PerClassLogLoss.Length; i++)
//            {
//                Console.WriteLine($"Class {i}: {metrics.PerClassLogLoss[i]}");
//            }

//            // Save the model to a file
//            context.Model.Save(model, data.Schema, "your_model.zip");

//            Console.WriteLine("Model trained and saved.");
//        }
//    }
//}