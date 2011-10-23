using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApplication16
{
    public class Program
    {
        public static IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(@"d:\tfs2010_P2P\Common\", "*.csproj", SearchOption.AllDirectories);
        }

        public static Tuple<string, bool, IEnumerable<string>> GetProjectInfo(string fname)
        {
            var xml = XDocument.Load(fname);
            XNamespace xns = xml.Root.Attribute("xmlns").Value;

            var isSilverligthAssembly = xml.Descendants(xns + "TargetFrameworkIdentifier")
                                           .Where(p => p.Value == "Silverlight").Any();

            var outputPaths = xml.Descendants(xns + "OutputPath").Select(x => x.Value);

            return Tuple.Create(fname, isSilverligthAssembly, outputPaths);
        }

        public static void ShowInfo(Tuple<string, bool, IEnumerable<string>> projInfo)
        {

            var name = projInfo.Item1;
            var sl = projInfo.Item2;
            var outs = projInfo.Item3;

            Console.WriteLine(sl ? "Assembly " + name + " outputs:" :
                                  "SL-assembly " + name + " outputs:");

            outs.ToList().ForEach(Console.WriteLine);
        }

        public static void Test()
        {
            GetFiles().Select(GetProjectInfo).ToList().ForEach(ShowInfo);
        }

    }
}
