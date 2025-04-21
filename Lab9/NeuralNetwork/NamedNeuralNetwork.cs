using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    [Serializable]
    public class NamedNeuralNetwork
    {
        private readonly NeuralNetwork _neuralNetwork;
        private readonly List<string> _neuronsNames;

        public NamedNeuralNetwork(int vectorSize, List<String> neuronsNames)
        {
            _neuronsNames = neuronsNames;
            _neuralNetwork = new NeuralNetwork(vectorSize, neuronsNames.Count);
        }

        public void Teaching(List<int> element, String correctClassName)
        {
            var correctClass = _neuronsNames.FindIndex(x => x == correctClassName);
            if (correctClass == -1)
                throw new ArgumentException("Unknown class name!");

            _neuralNetwork.Teaching(element, correctClass);
        }

        public String GetAnswer(List<int> element)
        {
            return _neuronsNames[_neuralNetwork.GetAnswer(element)];
        }

        public Dictionary<string, double> GetProbabilities(List<int> element)
        {
            var rawOutputs = _neuralNetwork.GetAnswers(element).Select(x => (double)x).ToList();

            double maxLogit = rawOutputs.Max();
            var expValues = rawOutputs.Select(v => Math.Exp(v - maxLogit)).ToList();
            double sum = expValues.Sum();

            var probabilities = new Dictionary<string, double>();
            for (int i = 0; i < _neuronsNames.Count; i++)
            {
                probabilities[_neuronsNames[i]] = expValues[i] / sum;
            }

            return probabilities;
        }
    }
}
