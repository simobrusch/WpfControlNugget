using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlNugget.Model
{
    public class Node<TM>
    {
        public TM ValueObject { get; set; }
        public Node<TM> ParentNode { get; set; }
        public List<Node<TM>> ChildNodesList { get; set; }

        public Node()
        {
            this.ChildNodesList = new List<Node<TM>>();
        }
        public Node(TM valueObject)
        {
            this.ValueObject = valueObject;
            this.ParentNode = default;
            this.ChildNodesList = new List<Node<TM>>();
        }

        public Node(Node<TM> parentNode, TM valueObject)
        {
            this.ValueObject = valueObject;
            this.ParentNode = parentNode;
            this.ChildNodesList = new List<Node<TM>>();
        }

        public void AddChildNode(Node<TM> childNode)
        {
            this.ChildNodesList.Add(childNode);
        }

        public override string ToString()
        {
            if (ValueObject != null)
            {
                return ValueObject.ToString();
            }
            return "-1";
        }
    }
}
