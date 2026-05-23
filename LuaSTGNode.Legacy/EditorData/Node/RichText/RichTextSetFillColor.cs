using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetfillcolor.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetFillColor : TreeNode
{
    [JsonConstructor]
    private RichTextSetFillColor() : base() { }

    public RichTextSetFillColor(DocumentData workspace)
        : this(workspace, "255, 255, 255, 255") { }

    public RichTextSetFillColor(DocumentData workspace, string color)
        : base(workspace)
    {
        ARGB = color;
    }

    [JsonIgnore, NodeAttribute]
    public string ARGB
    {
        get => DoubleCheckAttr(0, "ARGB").attrInput;
        set => DoubleCheckAttr(0, "ARGB").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setFillColor(lstg.Color({Macrolize(0)}))\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set text fill color to ({NonMacrolize(0)})";
    }

    public override object Clone()
    {
        var n = new RichTextSetFillColor(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
