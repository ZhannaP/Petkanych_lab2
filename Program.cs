using System;
using System.IO;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            Sequence sequenceGeometric = new Sequence(2, 4, 8, 16, 32, 64);

            Sequence sequenceArythmetic = new Sequence(1, 2, 3, 4, 5, 6, 7);

            Sequence sequenceRandom1 = new Sequence(0, 100, -5, 20, 33, -20);

            Sequence sequenceRandom2 = new Sequence(0, 100, -5, 20, 33, -20);

            var check = sequenceGeometric.CheckElement(2);
            
            

            var extrLocal = sequenceGeometric.GetLocalExtr(1, 4);
            var extrLocalFalse1 = sequenceGeometric.GetLocalExtr(-10, 0);
            var extrLocalFalse2 = sequenceGeometric.GetLocalExtr(1, 100);

            string nameOfSequence = GetMemberName(() => sequenceGeometric) + ".json";
            Serialize(sequenceGeometric, nameOfSequence);

            var sequenceGeometricJson = Deserialize(nameOfSequence);
            var equalsFalse = sequenceArythmetic.Equals(sequenceGeometric);
            var equalsTrue1 = sequenceRandom1.Equals(sequenceRandom2);
            var equalsTrue2 = sequenceGeometric.Equals(sequenceGeometricJson);
            Console.ReadLine();
        }

        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

        static void Serialize(Sequence sequence, string nameOfSequence)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using FileStream fileStream = new FileStream(nameOfSequence, FileMode.OpenOrCreate);
            JsonSerializer.SerializeAsync(fileStream, sequence, options).Wait();
        }

        static Sequence Deserialize(string nameOfSequence)
        {
            using (FileStream fileStream = new FileStream(nameOfSequence, FileMode.OpenOrCreate))
            {
                var sequence = JsonSerializer.DeserializeAsync<Sequence>(fileStream).Result;
                return sequence;
            }


        }
    }
}