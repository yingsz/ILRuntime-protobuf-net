using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HotFix_Project
{
    class TestProto
    {
        public static void RunTest()
        {
            var fam = new RiseProto.Family();
            var p1 = new RiseProto.Person();
            
            p1.age = 11;
            p1.name = "t1";
            fam.person.Add(p1);
            Debug.Log("TestProto1");
            var stream = new FileStream("output.txt", FileMode.OpenOrCreate);
            Debug.Log("TestProto2");
            ProtoBuf.Serializer.Serialize<RiseProto.Family>(stream, fam);
            stream.Close();
            stream.Dispose();
            Debug.Log("TestProto3");
        }
    }
}
