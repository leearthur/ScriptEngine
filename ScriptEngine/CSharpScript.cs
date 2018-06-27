namespace ScriptEngine
{
    public class CSharpScript
    {
        public static T Evaluate<T>(string code)
        {
            return Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript.RunAsync<T>(code).Result.ReturnValue;
        }
    }
}
