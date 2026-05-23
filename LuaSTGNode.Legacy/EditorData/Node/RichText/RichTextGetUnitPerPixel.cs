using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextgetunitperpixel.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextGetUnitPerPixel : TreeNode
{
    [JsonConstructor]
    private RichTextGetUnitPerPixel() : base() { }

    public RichTextGetUnitPerPixel(DocumentData workspace)
        : base(workspace) { }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + ":getUnitPerPixel()\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return "Get units per pixel";
    }

    public override object Clone()
    {
        var n = new RichTextGetUnitPerPixel(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
