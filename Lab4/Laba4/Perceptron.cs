using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Laba4
{
    public class Perceptron
    {
        public class PerceptronClass
        {
            public readonly List<PerceptronObject> objects = new List<PerceptronObject>();
        }

        public class PerceptronObject
        {
            public readonly List<int> attributes = new List<int>();
        }

        private const int C = 1;
        private const int RANDOM = 10;
        private const int MAX_ITERATIONS_COUNT = 1000;

        private int classesCount;
        private int objectsInClassCount;
        private int attributesCount;
        private List<PerceptronClass> classes = new List<PerceptronClass>();
        private List<PerceptronObject> weigths = new List<PerceptronObject>();
        private List<PerceptronObject> decisionFunctions;
        private List<int> decisions = new List<int>();

        public Perceptron(int classes, int objectsInclass, int attribute)
        {
            classesCount = classes;
            objectsInClassCount = objectsInclass;
            attributesCount = attribute;
        }

        public void Calculate()
        {
            CreateRandomClasses();
            CalculateDecisionFunctions();
        }

        private void CreateRandomClasses()
        {
            var rand = new Random();

            for (int i = 0; i < classesCount; i++)
            {
                var currentClass = new PerceptronClass();

                for (int j = 0; j < objectsInClassCount; j++)
                {
                    var currentObject = new PerceptronObject();

                    for (int k = 0; k < attributesCount; k++)
                        currentObject.attributes.Add(rand.Next(RANDOM) - RANDOM / 2);

                    currentClass.objects.Add(currentObject);
                }
                classes.Add(currentClass);
            }

            foreach (PerceptronClass perceptronClass in classes)
            {
                var weigth = new PerceptronObject();

                for (int i = 0; i <= attributesCount; i++)
                    weigth.attributes.Add(0);

                weigths.Add(weigth);
                decisions.Add(0);
            }

            foreach (PerceptronClass perceptronClass in classes)
                foreach (PerceptronObject perceptronObject in perceptronClass.objects)
                    perceptronObject.attributes.Add(1);
        }

        private void CalculateDecisionFunctions()
        {
            bool IsClassification = true;
            int iteration = 0;

            while (IsClassification && (iteration < MAX_ITERATIONS_COUNT))
            {
                for (int i = 0; i < classes.Count; i++)
                {
                    PerceptronClass currentClass = classes[i];
                    PerceptronObject currentWeigth = weigths[i];

                    for (int j = 0; j < currentClass.objects.Count; j++)
                    {
                        PerceptronObject currentObject = currentClass.objects[j];

                        IsClassification = CorrectWeigth(currentObject, currentWeigth, i);
                    }
                }
                iteration++;
            }

            if (iteration == MAX_ITERATIONS_COUNT)
                MessageBox.Show("Количество итераций превысило 1000." + "\n" + 
                    "Решаюшие функции, возможно, найдены неправильно.");

            decisionFunctions = weigths;
        }

        private bool CorrectWeigth(PerceptronObject currentObject, 
            PerceptronObject currentWeigth, int classNumber)
        {
            bool result = false;
            int objectDecision = ObjectMultiplication(currentWeigth, currentObject);

            for (int i = 0; i < weigths.Count; i++)
            {
                decisions[i] = ObjectMultiplication(weigths[i], currentObject);

                if (i != classNumber)
                {
                    int currentDecision = decisions[i];
                    if (objectDecision <= currentDecision)
                    {
                        ChangeWeigth(weigths[i], currentObject, -1);

                        result = true;
                    }
                }
            }
            if (result)
                ChangeWeigth(currentWeigth, currentObject, 1);

            return result;
        }

        private int ObjectMultiplication(PerceptronObject weigth, PerceptronObject obj)
        {
            int result = 0;

            for (int i = 0; i < weigth.attributes.Count; i++)
                result += weigth.attributes[i]*obj.attributes[i];

            return result;
        }

        private void ChangeWeigth(PerceptronObject weigth, PerceptronObject perceptronObject, int sign)
        {
            for (int i = 0; i < weigth.attributes.Count; i++)
                weigth.attributes[i] += sign * perceptronObject.attributes[i];
        }

        public void FillListBox(ListBox listBoxClasses, ListBox listBoxFunc)
        {
            int indexCurrentClass = 1;

            listBoxClasses.Items.Clear();
            listBoxFunc.Items.Clear();

            foreach (PerceptronClass currClass in classes)
            {
                int indexCurrentObject = 1;

                listBoxClasses.Items.Add(String.Format("Класс {0}:", indexCurrentClass));
                foreach (PerceptronObject currentObject in currClass.objects)
                {
                    string str = String.Format("    Объект {0}: (", indexCurrentObject);

                    for (int j = 0; j < currentObject.attributes.Count - 1; j++)
                    {
                        int attribute = currentObject.attributes[j];
                        str += attribute + ",";
                    }
                    str = str.Remove(str.Length - 1, 1);
                    str += ")";
                    listBoxClasses.Items.Add(str);
                    indexCurrentObject++;
                }
                listBoxClasses.Items.Add("");
                indexCurrentClass++;
            }

            listBoxFunc.Items.Add("Решающие функции: ");
            for (int i = 0; i < decisionFunctions.Count; i++)
            {
                string str = String.Format("d{0}(x) = ", i + 1);

                for (int j = 0; j < decisionFunctions[i].attributes.Count; j++)
                {
                    int attribute = decisionFunctions[i].attributes[j];

                    if (j < decisionFunctions[i].attributes.Count - 1)
                        if (attribute >= 0 && j != 0)
                            str += "+" + attribute + String.Format("*x{0}", j + 1);
                        else
                            str += attribute + String.Format("*x{0}", j + 1);
                    else
                        if (attribute >= 0 && j != 0)
                            str += "+" + attribute;
                        else
                            str += attribute;
                }
                listBoxFunc.Items.Add(str);
            }
        }

        public int FindClass(PerceptronObject perceptronObject)
        {
            int resultClass = 0;
            int decisionMax;

            perceptronObject.attributes.Add(1);
            decisionMax = ObjectMultiplication(weigths[0], perceptronObject);

            for (int i = 1; i < weigths.Count; i++)
            {
                PerceptronObject weigth = weigths[i];

                if (ObjectMultiplication(weigth, perceptronObject) > decisionMax)
                {
                    decisionMax = ObjectMultiplication(weigth, perceptronObject);
                    resultClass = i;
                }
            }

            return (resultClass + 1);
        }
    }
}