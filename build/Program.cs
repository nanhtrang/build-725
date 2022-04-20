using System;

namespace build // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Build b = new Build();
            b.runBatch("build.bat");
            string[] lines = b.readFileByLine("build.bat");
            b.readHtmlFe();
            b.replaceHtmlBE();
            b.runBatch("buildBE.bat");

        }
    }
}