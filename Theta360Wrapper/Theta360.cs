using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Theta360Wrapper
{
    public class Theta360
    {
        public static bool Capture(string saveDir)
        {
            ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            ScriptSource src = engine.CreateScriptSourceFromFile("theta360.py");
            src.Execute(scope);

            try
            {
                engine.Execute("Save_Dir = \"" + saveDir + "\"", scope);
                string bar = engine.Execute<string>("Save_Dir", scope);

                Action capture = engine.Operations.GetMember<Action>(scope, "capture");
                capture();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
