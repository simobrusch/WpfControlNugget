using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlNugget.Model;

namespace WpfControlNugget.ViewModel
{
    public class LocationTreeBuilder
    {
        public Node<LocationModel> BuildTree(List<LocationModel> locations)
        {
            if (locations == null) return new Node<LocationModel>();
            var nodeList = locations.ToList();
            var tree = FindTreeRoot(nodeList);
            BuildTree(tree, nodeList);
            return tree;
        }

        private void BuildTree(Node<LocationModel> locationNode, List<LocationModel> descendants)
        {
            var children = descendants.Where(node => node.ParentId == locationNode.ValueObject.Id).ToArray();
            foreach (var child in children)
            {
                var branch = Map(child, locationNode);
                locationNode.AddChildNode(branch);
                descendants.Remove(child);
            }
            foreach (var branch in locationNode.ChildNodesList)
            {
                BuildTree(branch, descendants);
            }
        }

        private Node<LocationModel> FindTreeRoot(List<LocationModel> nodes)
        {
            var rootNodes = nodes.Where(node => node.ParentId == 0);
            if (rootNodes.Count() != 1) return new Node<LocationModel>();
            var rootNode = rootNodes.Single();
            nodes.Remove(rootNode);
            return Map(rootNode, null);
        }

        private Node<LocationModel> Map(LocationModel loc, Node<LocationModel> parentnode)
        {
            return new Node<LocationModel>
            {
                ValueObject = loc,
                ParentNode = parentnode
            };
        }
    }
}
