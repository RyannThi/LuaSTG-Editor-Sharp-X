using LuaSTGEditorSharp.EditorData;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Message;
using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using LuaSTGEditorSharp.EditorData.Node.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextcreate.png")]
public class ReichTextCreate : TreeNode
{
    [JsonConstructor]
    private ReichTextCreate() : base() { }

    public ReichTextCreate(DocumentData workspace)
        : this(workspace, "path", "", "24") { }

    public ReichTextCreate(DocumentData workspace, string type, string name, string size)
        : base(workspace)
    {
        CreateType = type;
        Name = name;
        Size = size;
    }

    [JsonIgnore, NodeAttribute]
    public string CreateType
    {
        get => DoubleCheckAttr(0, "richtexttype", "Type").attrInput;
        set => DoubleCheckAttr(0, "richtexttype", "Type").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string Name
    {
        get => DoubleCheckAttr(1, name: "Path").attrInput;
        set => DoubleCheckAttr(1, name: "Path").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string Size
    {
        get => DoubleCheckAttr(2).attrInput;
        set => DoubleCheckAttr(2).attrInput = value;
    }

    private string GetCreateType()
    {
        return Macrolize(0) switch
        {
            "path" => "",
            "font" => $"FromPool",
            "system" => "FromSystem",
            _ => "",
        };
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        yield return sp + $"last = lstg.RichText.create{GetCreateType()}({Macrolize(1)}, {Macrolize(2)})\n";
        foreach (var a in base.ToLua(spacing))
            yield return a;
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
        foreach (Tuple<int, TreeNode> t in GetChildLines())
            yield return t;
    }

    public override string ToString()
    {
        return $"Create a RichText object with font '{NonMacrolize(1)}' and size {NonMacrolize(2)}";
    }

    public override object Clone()
    {
        var n = new ReichTextCreate(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }

    public override List<MessageBase> GetMessage()
    {
        List<MessageBase> messages = [];
        if (string.IsNullOrEmpty(NonMacrolize(1)))
            messages.Add(new ArgNotNullMessage(attributes[1].AttrCap, 0, this));
        return messages;
    }
}
