using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextupdate.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextUpdate : TreeNode
{
    [JsonConstructor]
    private RichTextUpdate() : base() { }

    public RichTextUpdate(DocumentData workspace)
        : base(workspace) { }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + ":update()\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return "Update RichText animations";
    }

    public override object Clone()
    {
        var n = new RichTextUpdate(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
