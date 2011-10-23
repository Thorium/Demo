import sys
sys.path.append(r'C:\Windows\Microsoft.NET\Framework64\v4.0.30319')

import clr
clr.AddReferenceToFile('System.Xml.Linq.dll')
clr.AddReferenceToFile('System.Xml.dll')

from System import *
from System.IO import *
from System.Xml.Linq import *

def xn(ns,s):
    return XName.Get(s,ns)
 
def getFiles():
    return Directory.GetFiles(r'd:\tfs2010_P2P\Matching Smart Client\main\Src', '*.csproj', SearchOption.AllDirectories)
 
def getProjectInfo(fname):
    xml = XDocument.Load(fname)
    xns = lambda s:xn(xml.Root.Attribute(XName.Get("xmlns")).Value,s)

    silverlightfilter = lambda p:p.Value=="Silverlight"
    isSilverligthAssembly = any(
        filter(
            silverlightfilter, xml.Descendants(xns("TargetFrameworkIdentifier"))
        )
    )
    
    outputPaths = map(lambda i:i.Value, xml.Descendants(xns("OutputPath")))

    return (fname, isSilverligthAssembly, outputPaths)

def showInfo(projInfo):
    name,sl,outs = projInfo
    if (sl): print("SL-assembly " + name + " outputs:")
    else: print("Assembly " + name + " outputs:")
    print "\n".join(outs)

def test():
    map(showInfo, map(getProjectInfo, getFiles()))
