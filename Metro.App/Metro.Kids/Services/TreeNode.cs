namespace Metro.Kids.Services
{
    internal class TreeNode
    {
        public TreeNode()
        {
        }

        public bool HasLeftChild { get { return LeftChild != null; } }
        public bool HasRightChild { get { return RightChild != null; } }

        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }
        public string Value { get; internal set; }
        public TreeNode Parent { get; internal set; }
    }
}