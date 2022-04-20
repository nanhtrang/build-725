using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace build
{
    internal class Build
    {
        public const string indexFe = @"\dist\index.html";
        public const string indexBE = @"\src\main\resources\templates\index.html";
        public string FE { get;set; }
        public string BE { get; set; }
        public string Css { get; set; }
        public string Manifest { get; set; }
        public string Vendor { get; set; }
        public string App { get; set; }
        public void runBatch(string file)
        {
            string current = Directory.GetCurrentDirectory();
            current += @"\" + file;
            Process p = null;
            p = Process.Start(current);
            p.WaitForExit();
        }
        public string [] readFileByLine(string file)
        {
            string [] lines = File.ReadAllLines(file);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].ToUpper().Contains("SET FE="))
                {
                    this.FE = Regex.Replace(lines[i], @"^\s*\w{3}\s+FE=", "").Trim();
                    //this.FE = Regex.Replace(lines[i], @"\s+", "").Trim();
                }
                if (lines[i].ToUpper().Contains("SET BE="))
                {
                    this.BE = Regex.Replace(lines[i], @"^\s*\w{3}\s+BE=", "").Trim();
                }
            }
            return lines;
        }
        public void readHtmlFe()
        {
            string htmlFe = this.FE + indexFe;
            string text = File.ReadAllText(htmlFe);
            string patternCss = @"=/static/css/app\.(\w+)\.css";
            string patternManifest = @"=/static/js/manifest\.(\w+)\.js";
            string patternVendor = @"=/static/js/vendor\.(\w+)\.js";
            string patternApp = @"=/static/js/app\.(\w+)\.js";
            this.Css = getKey(text, patternCss);
            this.Manifest = getKey(text, patternManifest);
            this.Vendor = getKey(text, patternVendor);
            this.App = getKey(text, patternApp);
        }

        public string getKey(string html, string pattern)
        {
            Match match = Regex.Match(html, pattern);
            string b = Regex.Replace(match.Value, pattern, "$1");
            return b;
        }

        public void replaceHtmlBE()
        {
            string htmlBE = this.BE + indexBE;
            string textBE = File.ReadAllText(htmlBE);
            string patternCss = @"=static/css/app\.(\w+)\.css";
            string patternManifest = @"=static/js/manifest\.(\w+)\.js";
            string patternVendor = @"=static/js/vendor\.(\w+)\.js";
            string patternApp = @"=static/js/app\.(\w+)\.js";
            textBE = Regex.Replace(textBE, patternCss, @"=static/css/app." + this.Css + @".css");
            textBE = Regex.Replace(textBE, patternManifest, @"=static/js/manifest." + this.Manifest + @".js");
            textBE = Regex.Replace(textBE, patternVendor, @"=static/js/vendor." + this.Vendor + @".js");
            textBE = Regex.Replace(textBE, patternApp, @"=static/js/app." + this.App + @".js");
            File.WriteAllText(htmlBE, textBE);
        }

        public void runScript(string folderPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = this.BE;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = "/C mvn clean install";
            p.Start();

        }
    }
}
