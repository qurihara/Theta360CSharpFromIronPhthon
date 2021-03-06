﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using IronPython.Hosting;
//using Microsoft.Scripting.Hosting;

// see these links:
// http://st63jun.hatenablog.jp/entry/2013/07/22/001423
// http://symfoware.blog68.fc2.com/blog-entry-416.html


namespace ThetaFromCSharpForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ScriptEngine engine = Python.CreateEngine();

            //ScriptSource src = engine.CreateScriptSourceFromFile("theta360.py");
            ////ScriptSource src = engine.CreateScriptSourceFromString("print \"Hello Python World\"");
            //src.Execute();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            //ScriptScope scope = engine.CreateScope();
            //ScriptSource src = engine.CreateScriptSourceFromFile("theta360.py");
            //src.Execute(scope);

            //engine.Execute("Save_Dir = \"./image/\"", scope);
            //string bar = engine.Execute<string>("Save_Dir", scope);

            //Action capture = engine.Operations.GetMember<Action>(scope, "capture");
            //capture();

            ////var f = engine.Operations.GetMember<string>(scope,"recent_image");// .Execute<string>("recent_image", scope);
            ////MessageBox.Show(f);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            //ScriptScope scope = engine.CreateScope();
            //ScriptSource src = engine.CreateScriptSourceFromFile("theta360.py");

            //object fooObject = engine.Operations.Invoke(scope.GetVariable("THETA360"));
            //Action bar = engine.Operations.GetMember<Action>(fooObject, "InitiateCapture");
            //bar(); // => 'Foo.bar()'

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DoCapture();

            //MessageBox.Show(suc.ToString());
        }
        private void DoCapture()
        {
            timer1.Stop();
            string saveDir = "C:/tmp/server/imgs/";
            bool suc = Theta360Wrapper.Theta360.Capture(saveDir);

            timer1.Start();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                timer1.Start();
            else
                timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                Theta360Wrapper.Theta360.KeepAlive();
            if (checkBox2.Checked)
                DoCapture();
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                timer1.Start();
            else
                timer1.Stop();
        }
    }
}
