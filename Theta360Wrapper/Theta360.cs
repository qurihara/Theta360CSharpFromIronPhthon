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
        protected static void Init(out ScriptEngine engine, out ScriptScope scope)
        {
            engine = IronPython.Hosting.Python.CreateEngine();
            scope = engine.CreateScope();
            ScriptSource src = engine.CreateScriptSourceFromFile("theta360.py");
            src.Execute(scope);
        }
        public static bool KeepAlive()
        {
            ScriptEngine engine;
            ScriptScope scope;
            Init(out engine, out scope);

            try
            {

                Action keepAlive = engine.Operations.GetMember<Action>(scope, "keep_alive");
                keepAlive();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static bool Capture(string saveDir)
        {
            ScriptEngine engine;
            ScriptScope scope;
            Init(out engine, out scope);

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
