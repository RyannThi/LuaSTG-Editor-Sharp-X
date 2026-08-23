using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.EditorData.Interfaces;

namespace LuaSTGEditorSharp.EditorData.Document
{
    public class VirtualDoc : IDocumentWithMeta
    {
        public string DocPath { get; set; }
        public AbstractMetaData UndecidedMeta { get; set; }

        public void SaveMeta()
        {
            string path = DocPath + ".lstgdef";
            using FileStream fs = new(path, FileMode.Create);
            using StreamWriter sw = new(fs);
            try
            {
                sw.Write(EditorSerializer.SerializeMetaData(UndecidedMeta));
            }
            catch (System.Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }

        public bool LoadMeta()
        {
            string path = DocPath + ".lstgdef";
            using FileStream fs = new(path, FileMode.Open);
            using StreamReader sr = new(fs);
            try
            {
                UndecidedMeta = (AbstractMetaData)EditorSerializer.DeserializeMetaData(sr.ReadToEnd());
                UndecidedMeta.CheckIntegrity();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
