using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Kids.Services
{
    public class MathEvaluator
    {

        public static double EvaluteByNCalc(string expr)
        {
            NCalc.Expression exprCalc = new NCalc.Expression(expr);
            var eval = exprCalc.Evaluate();
            if (eval.GetType() == typeof(Int32))
            {
                return ((int)eval) * 1.0;
            }
            else if(eval.GetType() == typeof(double))
            {
                return (double)eval;
            }
            else if(eval.GetType() == typeof(float))
            {
                return ((float)eval) * 1.0;
            }
            else
            {
                return (double)exprCalc.Evaluate();
            }
            
        }
        public static double EvaluteByTree(string expr)
        {
            var rootNode = new TreeNode();
            var currentNode = rootNode;
            var stack = new Stack<char>(expr.Reverse());
           
            PreBuildTree(rootNode, stack);
            return 1;
        }

        private static void PreBuildTree(TreeNode parentNode, Stack<char> stack)
        {
            if (!stack.Any()) return;
            var currentNode = parentNode;
            TreeNode tmpParentNode;
            
            while(stack.Any())
            {
                char val = stack.Pop();
                switch (val)
                {
                    case '(':
                        currentNode.Value = val.ToString();

                        break;
                    case ')':
                        tmpParentNode = currentNode;
                        while (tmpParentNode.Parent.Value != "(")
                        {
                            tmpParentNode = tmpParentNode.Parent;
                        }
                        currentNode = tmpParentNode;
                        break;
                    case '+':
                        tmpParentNode = currentNode.Parent;
                        while(tmpParentNode.Value != "*" && tmpParentNode.Value != "/")
                        {
                            tmpParentNode = tmpParentNode.Parent;
                        }

                        break;
                    case '-':
                        tmpParentNode = currentNode.Parent;
                        if (tmpParentNode.Value == "*" || tmpParentNode.Value == "/")
                        {

                        }
                        break;
                    case '*':
                        break;
                    case '/':
                        break;
                    case ' ':
                        break;
                    default://handle the number string

                        break;
                }
            }            
        }
    }
}
