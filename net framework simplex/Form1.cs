using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Solvers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = System.Windows.Forms.Application;
using ComboBox = System.Windows.Forms.ComboBox;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;
using Microsoft.SolverFoundation;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Microsoft.SolverFoundation.Common;

namespace net_framework_simplex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
    }

        int a, b;
        double[] userinput; //rest
        double[] userinput2;  //mainf
        string[] userinput3; //znaki
        string userinput4; //results
        double[] userinput5;
        int[] var;
        int[] rest;

        Dictionary<string, int> names = new Dictionary<string, int>();


            


    private void button1_Click(object sender, EventArgs e)
        {

            a = int.Parse(textBox1.Text);
            b = int.Parse(textBox2.Text);
            textBox1.Enabled = false;
            textBox2.Enabled = false;

            if (a > 0 && b > 0)
            {
                this.Controls.Remove(button1);

                //label
                
                Label label111 = new Label();
                label111.Text = "Целевая функция";
                label111.Left = 250;
                label111.Top = 50;
                this.Controls.Add(label111);
                
                //main f

                for (int i = 0; i < a ; i++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = "textboxmainf" + i.ToString();
                    textBox.Left = 250 + 100*i;
                    textBox.Top = 100;
                    textBox.Width = 40;
                    textBox.Height = 20;
                    this.Controls.Add(textBox);
                    textBox.TextChanged += new System.EventHandler(this.GetText);


                    Label label = new Label();
                    label.Name = "mainf" + i.ToString();
                    label.Left = 300 + 100 * i;
                    label.Top = 100;
                    label.Width = 35;
                    label.Height = 20;
                    int bb = i + 1;
                    label.Text = 'x' + bb.ToString() + " +";
                    this.Controls.Add(label);
                    if (i == a - 1)
                    {
                        label.Text = 'x' + bb.ToString() + "  ->";
                    }
                    

                }
                ComboBox combo1 = new ComboBox();
                combo1.Name = "minmax";
                combo1.Items.Add("min");
                combo1.Items.Add("max");
                combo1.Left = 250 + 100*a;
                combo1.Top = 100;
                combo1.Text = "Стремится к";
                combo1.DropDownStyle = ComboBoxStyle.DropDownList;
                combo1.Width = 60;
                combo1.Height = 20;
                combo1.SelectedIndex = 0;
                this.Controls.Add(combo1);
                


                // rest

                Label labelrest = new Label();
                labelrest.Text = "Ограничения";
                labelrest.Left = 250;
                labelrest.Top = 150;
                this.Controls.Add(labelrest);

                Label labelrest1 = new Label();
                labelrest1.Text = "x1, x2... >= 0";
                labelrest1.Left = 250;
                labelrest1.Top = 175;
                this.Controls.Add(labelrest1);

                //rest textbox

                int starttop = 200;
                
                for (int i = 0; i < b; i++)
                {
                    for (int j = 0; j < a; j++)
                    {
                        
                        TextBox textBox = new TextBox();
                        textBox.Name = "textboxrest" + i.ToString() + "j" + j.ToString();
                        textBox.Left = 250 + 100 * j;
                        textBox.Top = starttop;
                        textBox.Width = 40;
                        textBox.Height = 20;
                        textBox.TextChanged += new System.EventHandler(this.GetText);
                        this.Controls.Add(textBox);

                        

                        Label label = new Label();
                        label.Name = "labelrest" + i.ToString() + j.ToString();
                        label.Left = 300 + 100 * j;
                        label.Top = starttop;
                        label.Width = 35;
                        label.Height = 20;
                        int bb = j + 1;
                        label.Text = 'x' + bb.ToString() + " +";
                        if (j == a - 1) 
                        { 
                            label.Text = 'x' + bb.ToString();
                        }
                        this.Controls.Add(label);
                    }
                    //uneq

                    ComboBox combouneq = new ComboBox();
                    combouneq.Name = "combouneq" + i.ToString();
                    combouneq.Items.Add("<=");
                    combouneq.Items.Add(">=");
                    combouneq.Left = 250 + 100 * a;
                    combouneq.Top = starttop;
                    combouneq.Width = 45;
                    combouneq.Height = 20;
                    combouneq.DropDownStyle = ComboBoxStyle.DropDownList;
                    combouneq.SelectedIndex = 0;
                    this.Controls.Add(combouneq);

                    //res

                    TextBox textBoxres = new TextBox();
                    textBoxres.Name = "res" + i.ToString();
                    textBoxres.Left = 300 + 100 * a;
                    textBoxres.Top = starttop;
                    textBoxres.Width = 40;
                    textBoxres.Height = 20;
                    textBoxres.TextChanged += new System.EventHandler(this.GetText);
                    this.Controls.Add(textBoxres);
                    

                    starttop += 50;
                }

                button1.Visible = false;
                button2.Visible = true;









                //TextBox textbox = new TextBox();
                //textbox.Left = 200;
                //textbox.Top = 100;
                //textbox.Text = "Целевая функция";
                //this.Controls.Add(textbox);

                //Label label111 = new Label();
                //label111.Text = "Целевая функция";
                //label111.Left = 200;
                //label111.Top = 200;
                //this.Controls.Add(label111);

                //Label label222 = new Label();
                //label222.Text = "Ограничения";
                //label222.Left = 200;
                //label222.Top = 300;
                //this.Controls.Add(label222);
            }
            else MessageBox.Show("Положительный введи!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            a = int.Parse(textBox1.Text);
            b = int.Parse(textBox2.Text);
            //TextBox tbx = this.Controls.Find("textBox1", true).FirstOrDefault() as TextBox;
            //textBox2.Text = tbx.Text;

            int mathcount = a * b  ;
            int mathcount2 = a ;
            int mathcount3 = b +1 ;
            int mathcount5 = b ;

            userinput = new double[mathcount];
            userinput2 = new double[mathcount2];
            userinput3 = new string[mathcount3];
            userinput5 = new double[mathcount5];
            

            for (int i = 0; i < a; i++)
            {
                string namestring = "textboxmainf" + i.ToString();
                TextBox tbx = this.Controls.Find(namestring, true).FirstOrDefault() as TextBox;
                userinput2[i] = Convert.ToDouble(tbx.Text);
                
               

            }
            int item = 0;
            for (int i = 0; i < b; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    string namestring = "textboxrest" + i.ToString() + "j" + j.ToString();
                    TextBox tbx = this.Controls.Find(namestring, true).FirstOrDefault() as TextBox;
                    userinput[item] = Convert.ToDouble(tbx.Text);
                    item++;
                }
            }

            ComboBox tbxx = this.Controls.Find("minmax", true).FirstOrDefault() as ComboBox;
            userinput4 = tbxx.SelectedItem.ToString();


            //userinput[item] = ComboBox.Text;
            //string sss = tbxx.SelectedItem.ToString();
            //if (tbxx.SelectedValue.ToString() == "min")




            //MessageBox.Show(tbxx.SelectedItem.ToString());





            //string bbb = tbxx.Text;
            //userinput[item] = bbb;
            //item++;


            for (int i = 0; i < b; i++)
            {
                string namestring =  "combouneq" + i.ToString();
                ComboBox tbx = this.Controls.Find(namestring, true).FirstOrDefault() as ComboBox;
                userinput3[i] = tbx.SelectedItem.ToString();
                
            }

            for (int i = 0; i < b; i++)
            {
                string namestring = "res" + i.ToString();
                TextBox tbx = this.Controls.Find(namestring, true).FirstOrDefault() as TextBox;
                userinput5[i] = Convert.ToDouble(tbx.Text);
                
            }

            //MessageBox.Show(item.ToString());

            int goal;

            //decisions
            //Decision vz = new Decision(Domain.RealNonnegative, "barrels_venezuela");

            SimplexSolver solver = new SimplexSolver();

            //int aa = a - 1;
            var = new int[a];
            //int bb = b - 1;
            rest = new int[b];

            //all vars, rest, goal

            for (int i = 0; i < a; i++)
            {
                string restname = "a" + i.ToString();
                solver.AddVariable(restname, out var[i]);
            }

            for (int i = 0; i < b; i++)
            {
                string restname = "b" + i.ToString();
                solver.AddRow(restname, out rest[i]);
            }

            solver.AddRow("goal", out goal);

            //coeff a*b
            // row = rest = const var = var
            int start = 0;
            for (int i = 0; i < b; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    solver.SetCoefficient(rest[i], var[j], userinput[start]);
                    start++;
                }

                if (userinput3[i] == "<=")
                {
                    solver.SetBounds(rest[i], Rational.NegativeInfinity, userinput5[i]);
                }
                else
                {
                    solver.SetBounds(rest[i], userinput5[i], Rational.PositiveInfinity);
                }
            }
            
            //goals

            for (int i = 0; i < a; i++)
            {
                solver.SetCoefficient(goal, var[i], userinput2[i]);
            }

            //min max

            if (userinput4 == "min")
            {
                solver.AddGoal(goal, 1, true);
            }
            else
            {
                solver.AddGoal(goal, 1, false);
            }

            SimplexSolverParams parameter = new SimplexSolverParams(); parameter.GetSensitivityReport = true;
            ILinearSolution solution = solver.Solve(parameter);

            string toout = "Result: ";
            toout += Math.Round((solver.GetValue(goal).ToDouble()),3);
            toout += "\nVars: \n";
            for (int i = 0; i < a; i++)
            {
                toout += "x" + ((i+1).ToString()) + ": ";
                toout += Math.Round(solver.GetValue(var[i]).ToDouble(), 3);
                toout += " \n";
            }

            label3.Visible = true;
            //for (int i = 0; i < a; i++)
            //{
               // if (solver.GetValue(var[i]).ToDouble() < 0) toout = "Решения  не существует";
            //}
            string sss = (solver.LpResult).ToString();
            toout += "\n" + sss;
            label3.Text = toout;
            
            //if (sss != "optimal")
            //{
               // label3.Text = (solver.LpResult).ToString();
            //};
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        void GetText(object sender, EventArgs e)
        {
            //TextBox t = (TextBox)sender;
            // t is the textbox you referred
            //MessageBox.Show(t.Text);
        }






    }
}
