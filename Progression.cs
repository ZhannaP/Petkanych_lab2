using System;
using System.Collections.Generic;


namespace lab2
{
    [Serializable]
    class Sequence
    {
        public List<int> SEQUENCE { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get;  set; }

        public bool IsGrowing { get; set; }
        public bool IsNotGrowing { get; set; }
        public bool IsDescending { get; set; }
        public bool IsNotDescending { get; set; }
        public bool? ArythmProgression { get; set; }
        public bool? GeomProgression { get; set; }

        public Sequence()
        { 
        
        }

        public Sequence(params int[] numbers)
        {
            FillSequence(numbers);
        }

        private void FillSequence(params int[] numbers)
        {
            SEQUENCE = new List<int>();
            foreach (int x in numbers) SEQUENCE.Add(x);

            MinNumber = GetMin();
            MaxNumber = GetMax();

            IsGrowing = CheckGrowing().Item1;
            IsNotGrowing = CheckGrowing().Item2;
            IsDescending = CheckGrowing().Item3;
            IsNotDescending = CheckGrowing().Item4;

            var progression = IsProgression();

            if (progression == true)
            {
                ArythmProgression = true;
                GeomProgression = null;
            }
            else if (progression == false)
            {
                ArythmProgression = null;
                GeomProgression = true;
            }
            else
            {
                ArythmProgression = null;
                GeomProgression = null;
            }
        }

        private  (bool, bool, bool, bool) CheckGrowing()
        {
            int isGrowing = SEQUENCE[0];
            int isNotGrowing = SEQUENCE[0];
            int isDescending = SEQUENCE[0];
            int isNotDescending = SEQUENCE[0];

            for(int i = 1; i<SEQUENCE.Count; i++)
            {
                if (isGrowing > SEQUENCE[i]) isGrowing = SEQUENCE[i];
                if (isNotGrowing <= SEQUENCE[i]) isNotGrowing = SEQUENCE[i];
                if (isDescending < SEQUENCE[i]) isDescending = SEQUENCE[i];
                if (isNotDescending >= SEQUENCE[i]) isNotDescending = SEQUENCE[i];
            }

            return (isGrowing == SEQUENCE[0], isNotGrowing == SEQUENCE[0],
                isDescending == SEQUENCE[0], isNotDescending == SEQUENCE[0]);
        }

        private bool? IsProgression()
        {
            bool? isProgression = null;

            for (int i = 1; i < SEQUENCE.Count - 2; i++)
            {
                if ((SEQUENCE[i - 1] + SEQUENCE[i + 1]) / 2 == SEQUENCE[i]) isProgression = true;
                else if (SEQUENCE[i - 1] * SEQUENCE[i + 1] == Math.Pow(SEQUENCE[i], 2)) isProgression = false;
                else isProgression = null;
            }

            return isProgression;
        }

        public bool CheckElement(int number)
        {
            return SEQUENCE.Contains(number);

        }

        private int GetMax()
        {
            int number = SEQUENCE[0];
            foreach (int i in SEQUENCE) if (i > number) number = i;
            return number;
        }

        private int GetMin()
        {
            int number = SEQUENCE[0];
            foreach (int i in SEQUENCE) if (i < number) number = i;
            return number;
        }

        public bool Equals(Sequence other)
        {
            if (other == null) return false;

            return IsDescending.Equals(other.IsDescending)
                && IsNotDescending.Equals(other.IsNotDescending)
                && IsGrowing.Equals(other.IsNotGrowing)
                && IsNotGrowing.Equals(other.IsNotGrowing)
                && MinNumber.Equals(other.MinNumber)
                && MaxNumber.Equals(other.MaxNumber)
                && ArythmProgression.Equals(other.ArythmProgression)
                && GeomProgression.Equals(other.GeomProgression);
        }

        public  (int, int)? GetLocalExtr(int start, int end)
        {
            int min = SEQUENCE[0], max = SEQUENCE[0];

            if (start < 0 || end >= SEQUENCE.Count) return null;

            if (start > 0 && end < SEQUENCE.Count)
                for (int i = start; i < end; i++)
                {
                    if (SEQUENCE[i] > max) max = SEQUENCE[i];
                    if (SEQUENCE[i] < min) min = SEQUENCE[i];
                }

            return (max, min);
        }

       
    }
}
