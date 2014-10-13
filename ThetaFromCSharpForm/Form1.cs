using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

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
            ScriptEngine engine = Python.CreateEngine();

            ScriptSource src = engine.CreateScriptSourceFromFile("theta360.py");
            //ScriptSource src = engine.CreateScriptSourceFromString("print \"Hello Python World\"");
            src.Execute();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            ScriptSource src = engine.CreateScriptSourceFromFile("theta360.py");
            src.Execute(scope);

            Action hello = engine.Operations.GetMember<Action>(scope, "capture");
            hello(); // => 'hello, world'

            string f = engine.Execute<string>("recent_image", scope);
            MessageBox.Show(f);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            ScriptSource src = engine.CreateScriptSourceFromFile("theta360.py");

            object fooObject = engine.Operations.Invoke(scope.GetVariable("THETA360"));
            Action bar = engine.Operations.GetMember<Action>(fooObject, "InitiateCapture");
            bar(); // => 'Foo.bar()'

        }
    }
}
