using Microsoft.SolverFoundation.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using System.Windows.Forms;
using System.Numerics;
using System.Runtime.Remoting.Contexts;


namespace net_framework_simplex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            

            //Example6();
            //System.Threading.Thread.Sleep(5000000);
        }
        

        static void Example6()
        {
            //MyLpSolveSolver solver = new MyLpSolveSolver();
            SimplexSolver solver = new SimplexSolver();

            // add variables with their lower and upper bounds
            int savid, vzvid;
            solver.AddVariable("Saudi Arabia", out savid);
            //solver.SetBounds(savid, 0, 9000);
            solver.AddVariable("Venezuela", out vzvid);
            //solver.SetBounds(vzvid, 0, 6000);
            solver.SetIntegrality(vzvid, true);
            // add constraints to model
            int c1rid, c2rid, c3rid, goalrid;
            solver.AddRow("constraint1", out c1rid);
            solver.AddRow("constraint2", out c2rid);
            solver.AddRow("constraint3", out c3rid);
            solver.AddRow("goal", out goalrid);
            // add coefficients to constraint rows
            solver.SetCoefficient(c1rid, savid, 0.3);
            solver.SetCoefficient(c1rid, vzvid, 0.4);
            solver.SetBounds(c1rid, 2000, Rational.PositiveInfinity);
            solver.SetCoefficient(c2rid, savid, 0.4);
            solver.SetCoefficient(c2rid, vzvid, 0.2);
            solver.SetBounds(c2rid, 1500, Rational.PositiveInfinity);
            solver.SetCoefficient(c3rid, savid, 0.2);
            solver.SetCoefficient(c3rid, vzvid, 0.3);
            solver.SetBounds(c3rid, 500, Rational.PositiveInfinity);
            // add objective (goal) to model and specify minimization (==true)
            solver.SetCoefficient(goalrid, savid, 20);
            solver.SetCoefficient(goalrid, vzvid, 15);
            solver.AddGoal(goalrid, 1, true);
            // solve the model

            //LpSolveDirective simplex = new LpSolveDirective(); simplex.GetSensitivity = true; ; simplex.LpSolveVerbose = 4; LpSolveParams parameter = new LpSolveParams(simplex);
            SimplexSolverParams parameter = new SimplexSolverParams(); parameter.GetSensitivityReport = true;
            //SimplexSolverParams parameter = new DemoSimplexSolverParams(); parameter.GetSensitivityReport = true;

            ILinearSolution solution = solver.Solve(parameter);
            Console.WriteLine("SA {0}, VZ {1}, C1 {2}, C2 {3}, C3 {4}, Goal {5}",
            solver.GetValue(savid).ToDouble(), solver.GetValue(vzvid).ToDouble(),
            solver.GetValue(c1rid).ToDouble(), solver.GetValue(c2rid).ToDouble(),
            solver.GetValue(c3rid).ToDouble(), solver.GetValue(goalrid).ToDouble());
           
            
            Console.WriteLine("{0}", solver.PivotCount);
            Console.WriteLine("{0}", solver.AlgorithmUsed);

            
            

            ILinearSolverReport report = solver.GetReport(LinearSolverReportType.Sensitivity);

            Report Report = report as Report;
            Console.Write("{0}", report);
            LinearReport lpReport = report as LinearReport;
            Console.Write("{0}", report);
        }

        static void saveforlater()
        {
            int goal;
            // Get the context and create a new model.
            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();
            // Create two decision variables representing the number of barrels to
            // purchase from two countries.
            // AddDecisions tells the model about the two variables.
            
            Decision vz = new Decision(Domain.RealNonnegative, "barrels_venezuela");
            Decision sa = new Decision(Domain.RealNonnegative, "barrels_saudiarabia");
            model.AddDecisions(vz, sa);
            // Adding five constraints. The first line defines the allowable range // for the two decision variables. The other constraints put
            // minimums on the total yield of three products.
            //model.AddConstraints("limits",
            //0 <= vz <= 9000,
            //0 <= sa <= 6000);
            model.AddConstraints("production",
            0.3 * vz + 0.4 * sa >= 2000,
            0.4 * vz + 0.2 * sa >= 1500,
            0.2 * vz + 0.3 * sa >= 500);
            // AddGoal states that we want to minimize the total cost subject to the
            // above constraints
            

            model.AddGoal("cost", GoalKind.Minimize, 20 * vz + 15 * sa);

            // Solve the problem using the simplex solver
            SimplexDirective simplex = new SimplexDirective();
            //LpSolveDirective simplex = new LpSolveDirective();
            Solution solution = context.Solve(simplex);
            // Report the solution values
            Report report = solution.GetReport();
            Console.WriteLine("vz: {0}, sa: {1}", vz, sa);
            Console.Write("{0}", report);
            
            using (StreamWriter sw = new StreamWriter("Example2.mps"))
            {
                context.SaveModel(FileFormat.FreeMPS, sw);
            }
            Console.WriteLine();


        }


    }
}
