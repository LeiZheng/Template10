using Metro.Kids.Services;
using MetroLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Kids
{
    public class MathOperationGenerator
    {
        private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<MathOperationGenerator>();
        private Random _random;
        private Dictionary<NumberFactors, string> factorStringMap = new Dictionary<NumberFactors, string>();
        public string GenerateMathOperate(MathSeed seed)
        {
            var formular = "";
            
            var values = new List<double>();

            for (int i = 0; i < seed.CountNumber; i++)
            {

                var randomD = _random.NextDouble();
                var num = (1 - randomD) * seed.MinNumber + randomD * seed.MaxNumber;
                if (i == 0)
                {
                    formular += (int)num;
                }
                else
                {
                    formular += " " + factorStringMap[seed.Factors[(int)Math.Floor(randomD - 0.0001)]];
                    formular += " " + (int)num;
                }
            }
            var ret = MathEvaluator.Evaluate(formular);
            return formular;
        }
        public MathOperationGenerator()
        {
            _random = new Random((int)DateTime.Now.Ticks);
            factorStringMap.Add(NumberFactors.PLUS, "+");
            factorStringMap.Add(NumberFactors.MINUS, "-");
            factorStringMap.Add(NumberFactors.MULITPLE, "*");
            factorStringMap.Add(NumberFactors.DIV, "/");
        }

    }
    public class MathSeed
    {
        public double MinNumber { get; internal set; }
        public double MaxNumber { get; internal set; }
        public int CountNumber { get; internal set; }
        public List<NumberFactors> Factors {get;set;}
    }
    public enum NumberFactors
    {
        PLUS,
        MINUS,
        MULITPLE,
        DIV,
    }
}
