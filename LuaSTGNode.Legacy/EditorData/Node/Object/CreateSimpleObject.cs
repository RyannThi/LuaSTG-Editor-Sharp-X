using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;

namespace LuaSTGEditorSharp.EditorData.Node.Object
{
    [Serializable, NodeIcon("objectsimple.png")]
    [RequireAncestor(typeof(CodeAlikeTypes))]
    [RCInvoke(2)]
    class CreateSimpleObject : TreeNode
    {
        [JsonConstructor]
        public CreateSimpleObject() : base() { }

        public CreateSimpleObject(DocumentData workSpaceData)
            : this(workSpaceData, "self.x, self.y", "\"leaf\"", "LAYER_ENEMY_BULLET", "GROUP_ENEMY_BULLET", "false", "true"
                  , "false", "10", "true") { }

        public CreateSimpleObject(DocumentData workSpaceData, string pos, string image, string layer, string group
            , string hide, string bound, string autorot, string hp, string colli)
            : base(workSpaceData)
        {
            Position = pos;
            Image = image;
            Layer = layer;
            Group = group;
            Hide = hide;
            Bound = bound;
            AutoRotation = autorot;
            HP = hp;
            Collision = colli;
        }

        #region Attrs
        [JsonIgnore, NodeAttribute]
        public string Position
        {
            get => DoubleCheckAttr(0, "position").attrInput;
            set => DoubleCheckAttr(0, "position").attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Image
        {
            get => DoubleCheckAttr(1, "objimage").attrInput;
            set => DoubleCheckAttr(1, "objimage").attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Layer
        {
            get => DoubleCheckAttr(2, "layer").attrInput;
            set => DoubleCheckAttr(2, "layer").attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Group
        {
            get => DoubleCheckAttr(3, "group").attrInput;
            set => DoubleCheckAttr(3, "group").attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Hide
        {
            get => DoubleCheckAttr(4, "bool").attrInput;
            set => DoubleCheckAttr(4, "bool").attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Bound
        {
            get => DoubleCheckAttr(5, "bool").attrInput;
            set => DoubleCheckAttr(5, "bool").attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string AutoRotation
        {
            get => DoubleCheckAttr(6, "bool", "Auto Rotation").attrInput;
            set => DoubleCheckAttr(6, "bool", "Auto Rotation").attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string HP
        {
            get => DoubleCheckAttr(7).attrInput;
            set => DoubleCheckAttr(7).attrInput = value;
        }

        [JsonIgnore, NodeAttribute]
        public string Collision
        {
            get => DoubleCheckAttr(8, "bool").attrInput;
            set => DoubleCheckAttr(8, "bool").attrInput = value;
        }
        #endregion

        public override IEnumerable<string> ToLua(int spacing)
        {
            string sp = Indent(spacing);
            string s1 = Indent(1);
            yield return sp + $"last = Class(_object)\n";
            yield return sp + $"last.init = function(self, _x, _y, _)\n"
                + sp + s1 + "self.x, self.y = _x, _y\n"
                + sp + s1 + "self.img = " + Macrolize(1) + "\n"
                + sp + s1 + "self.layer = " + Macrolize(2) + "\n"
                + sp + s1 + "self.group = " + Macrolize(3) + "\n"
                + sp + s1 + "self.hide = " + Macrolize(4) + "\n"
                + sp + s1 + "self.bound = " + Macrolize(5) + "\n"
                + sp + s1 + "self.navi = " + Macrolize(6) + "\n"
                + sp + s1 + "self.hp = " + Macrolize(7) + "\n"
                + sp + s1 + "self.maxhp = " + Macrolize(7) + "\n"
                + sp + s1 + "self.colli = " + Macrolize(8) + "\n"
                + sp + s1 + "self._servants = {}\n"
                + sp + s1 + "self._blend, self._a, self._r, self._g, self._b = '', 255, 255, 255, 255\n";
            yield return sp + $"end\n";
            yield return sp + "last[1] = last.init or function() end\n";
            yield return sp + "last[2] = last.del or function() end\n";
            yield return sp + "last[3] = last.frame or function() end\n";
            yield return sp + "last[4] = last.render or lstg.DefaultRenderFunc\n";
            yield return sp + "last[5] = last.colli or function(other) end\n";
            yield return sp + "last[6] = last.kill or function() end\n";

            yield return sp + $"last = New(last, {Macrolize(0)}, _)\n";
            yield return sp + $"task.New(last, function()\n";
            yield return sp + s1 + "local self = task.GetSelf()\n";
            foreach (var a in base.ToLua(spacing + 1))
                yield return a;
            yield return sp + $"end)\n";
        }

        public override string ToString()
        {
            return $"Create simple object with task at ({NonMacrolize(0)})";
        }

        public override IEnumerable<Tuple<int, TreeNode>> GetLines()
        {
            yield return new Tuple<int, TreeNode>(24, this);
            foreach (Tuple<int, TreeNode> t in GetChildLines())
                yield return t;
            yield return new Tuple<int, TreeNode>(1, this);
        }

        public override object Clone()
        {
            var n = new CreateSimpleObject(parentWorkSpace);
            n.DeepCopyFrom(this);
            return n;
        }
    }
}
