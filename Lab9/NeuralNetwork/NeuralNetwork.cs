using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    [Serializable]
    public class NeuralNetwork
    {
        private const int _threshold = 70;
        private readonly List<Neuron> _neurons;
        private readonly int _neuronsCount;

        public NeuralNetwork(int vectorSize, int neuronsCount)
        {
            _neuronsCount = neuronsCount;
            _neurons = GetStartNeurons(vectorSize, neuronsCount);
        }

        private static List<Neuron> GetStartNeurons(int vectorSize, int neuronsCount)
        {
            var neurons = new List<Neuron>();
            for (var i = 0; i < neuronsCount; i++)
            {
                neurons.Add(new Neuron(vectorSize));
            }

            return neurons;
        }

        public void Teaching(List<int> element, int correctClass)
        {
            var neurousThresholdAnswers = GetNeurousThresholdAnswers(element);

            while (! IsCorrectAnswer(neurousThresholdAnswers, correctClass))
            {
                TeachNeurons(element, correctClass, neurousThresholdAnswers);
                neurousThresholdAnswers = GetNeurousThresholdAnswers(element);
            }
        }

        private void TeachNeurons(List<int> element, int correctClass,
            List<bool> neurousThresholdAnswers)
        {
            for (var i = 0; i < _neuronsCount; i++)
            {
                _neurons[i].Teaching(element, GetNeuronFactor(neurousThresholdAnswers[i], i, correctClass));
            }
        }

        private List<bool> GetNeurousThresholdAnswers(IEnumerable<int> element)
        {
            return _neurons.Select(x => x.GetAnswer(element) > _threshold).ToList();
        }

        private bool IsCorrectAnswer(IReadOnlyList<bool> neurousThresholdAnswers, int correctClass)
        {
            return neurousThresholdAnswers[correctClass] && neurousThresholdAnswers.Count(x => x) == 1;
        }

        private int GetNeuronFactor(bool neuronThresholdAnswer, int neuronNumber, int correctClass)
        {
            if (neuronThresholdAnswer)
            {
                return neuronNumber == correctClass ? 0 : -1;
            }

            return neuronNumber == correctClass ? 1 : 0;
        }

        public int GetAnswer(List<int> element)
        {
            var neuronsAnswer = _neurons.Select(x => x.GetAnswer(element)).ToList();
            return GetMaxValueIndex(neuronsAnswer);
        }

        public List<int> GetAnswers(List<int> element)
        {
            return _neurons.Select(x => x.GetAnswer(element)).ToList();
        }

        private static int GetMaxValueIndex(List<int> neuronsAnswer)
        {
            var maxIndex = 0;
            for (var i = 0; i < neuronsAnswer.Count; i++)
            {
                if (neuronsAnswer[i] > neuronsAnswer[maxIndex])
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
    }
}