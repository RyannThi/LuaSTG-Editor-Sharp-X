using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetstate.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetState : TreeNode
{
    [JsonConstructor]
    private RichTextSetState() : base() { }

    public RichTextSetState(DocumentData workspace)
        : this(workspace, "\"\"", "255, 255, 255, 255") { }

    public RichTextSetState(DocumentData workspace, string blend, string color)
        : base(workspace)
    {
        BlendMode = blend;
        ARGB = color;
    }

    [JsonIgnore, NodeAttribute]
    public string BlendMode
    {
        get => DoubleCheckAttr(0, "blend", "Blend Mode").attrInput;
        set => DoubleCheckAttr(0, "blend", "Blend Mode").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string ARGB
    {
        get => DoubleCheckAttr(1, "ARGB").attrInput;
        set => DoubleCheckAttr(1, "ARGB").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setState({Macrolize(0)}, lstg.Color({Macrolize(1)}))\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set render state to blend mode ({NonMacrolize(0)}) with color ({NonMacrolize(1)})";
    }

    public override object Clone()
    {
        var n = new RichTextSetState(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
