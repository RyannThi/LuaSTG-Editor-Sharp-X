using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetalignment.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetAlignment : TreeNode
{
    [JsonConstructor]
    private RichTextSetAlignment() : base() { }

    public RichTextSetAlignment(DocumentData workspace)
        : this(workspace, "nil", "nil") { }

    public RichTextSetAlignment(DocumentData workspace, string h, string v)
        : base(workspace)
    {
        HAlign = h;
        VAlign = v;
    }

    [JsonIgnore, NodeAttribute]
    public string HAlign
    {
        get => DoubleCheckAttr(0, "richtexthalign", "H Alignment").attrInput;
        set => DoubleCheckAttr(0, "richtexthalign", "H Alignment").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string VAlign
    {
        get => DoubleCheckAttr(1, "richtextvalign", "V Alignment").attrInput;
        set => DoubleCheckAttr(1, "richtextvalign", "V Alignment").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setAlignment({Macrolize(0)}, {Macrolize(1)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set horizontal align to {NonMacrolize(0)} and vertical to {NonMacrolize(1)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetAlignment(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
