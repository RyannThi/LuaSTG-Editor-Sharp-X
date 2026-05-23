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

/// <summary>
/// This is a reference node for RichText instances.
/// It must be existing as a parent node for the setters, as they need a parent.
/// </summary>
[Serializable, NodeIcon("richtextcreate.png")]
public class ReichTextRef : TreeNode
{
    [JsonConstructor]
    private ReichTextRef() : base() { }

    public ReichTextRef(DocumentData workspace)
        : this(workspace, "last") { }

    public ReichTextRef(DocumentData workspace, string obj)
        : base(workspace)
    {
        Obj = obj;
    }

    [JsonIgnore, NodeAttribute]
    public string Obj
    {
        get => DoubleCheckAttr(0, "target", "RichText Instance").attrInput;
        set => DoubleCheckAttr(0, "target", "RichText Instance").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        yield return sp + $"{Macrolize(0)}\n";
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
        return $"References '{NonMacrolize(0)}' for RichText operations";
    }

    public override object Clone()
    {
        var n = new ReichTextRef(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }

    public override List<MessageBase> GetMessage()
    {
        List<MessageBase> messages = [];
        if (string.IsNullOrEmpty(NonMacrolize(0)))
            messages.Add(new ArgNotNullMessage(attributes[0].AttrCap, 0, this));
        return messages;
    }
}
