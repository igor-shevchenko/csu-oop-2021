using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Set
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [Serializable]
    public class BinaryTreeNode<T> where T : IComparable
    {
        public T Value;
        public BinaryTreeNode<T> Left;
        public BinaryTreeNode<T> Right;
        public BinaryTreeNode<T> Parent;
    }

    [Serializable]
    public class BinaryTree<T> where T : IComparable
    {
        private BinaryTreeNode<T> _root;

        public bool Find(T element)
        {
            var current = _root;

            while (current != null)
            {
                var compareResult = element.CompareTo(current.Value);
                if (compareResult < 0)
                {
                    current = current.Left;
                }
                else if (compareResult > 0)
                {
                    current = current.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public bool Insert(T element)
        {
            if (_root == null)
            {
                _root = new BinaryTreeNode<T>()
                {
                    Value = element
                };
                return true;
            };

            var current = _root;

            while (current != null)
            {
                var compareResult = element.CompareTo(current.Value);
                if (compareResult < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new BinaryTreeNode<T>()
                        {
                            Value = element,
                            Parent = current,
                        };
                        return true;
                    }

                    current = current.Left;
                }
                else if (compareResult > 0)
                {

                    if (current.Right == null)
                    {
                        current.Right = new BinaryTreeNode<T>()
                        {
                            Value = element,
                            Parent = current,
                        };
                        return true;
                    }

                    current = current.Right;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public bool Remove(T element)
        {
            var current = _root;

            while (current != null)
            {
                var compareResult = element.CompareTo(current.Value);
                if (compareResult < 0)
                {
                    current = current.Left;
                }
                else if (compareResult > 0)
                {
                    current = current.Right;
                }
                else
                {
                    if (current.Left == null && current.Right == null)
                    {
                        if (current.Parent == null)
                        {
                            _root = null;
                        }
                        else if (current == current.Parent.Left)
                        {
                            current.Parent.Left = null;
                        }
                        else
                        {
                            current.Parent.Right = null;
                        }
                    } else if (current.Left == null)
                    {
                        current.Right.Parent = current.Parent;
                        if (current.Parent == null)
                        {
                            _root = current.Right;
                        }
                        else if (current == current.Parent.Left)
                        {
                            current.Parent.Left = current.Right;
                        }
                        else
                        {
                            current.Parent.Right = current.Right;
                        }
                    }
                    else if (current.Right == null)
                    {
                        current.Left.Parent = current.Parent;
                        if (current.Parent == null)
                        {
                            _root = current.Left;
                        }
                        else if (current == current.Parent.Left)
                        {
                            current.Parent.Left = current.Left;
                        }
                        else
                        {
                            current.Parent.Right = current.Left;
                        }
                    }
                    else
                    {
                        if (current.Parent == null)
                        {
                            _root = current.Right;
                        }
                        else if (current == current.Parent.Left)
                        {
                            current.Parent.Left = current.Right;
                        }
                        else
                        {
                            current.Parent.Right = current.Right;
                        }

                        var newParentRight = current.Right;
                        while (newParentRight.Left != null)
                            newParentRight = newParentRight.Left;

                        newParentRight.Left = current.Left;
                        current.Left.Parent = newParentRight;
                    }

                    return true;
                }
            }

            return false;
        }

        public IEnumerable<T> Traverse()
        {
            return TraverseNode(_root);
        }

        private IEnumerable<T> TraverseNode(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                foreach (var element in TraverseNode(node.Left))
                    yield return element;

                yield return node.Value;

                foreach (var element in TraverseNode(node.Right))
                    yield return element;
            }
        }
    }

    [Serializable]
    public class BinaryTreeSet<T> : ISet<T> where T: IComparable
    {
        private BinaryTree<T> _tree = new BinaryTree<T>();

        public int Count => _tree.Traverse().Count();

        public bool IsReadOnly => false;

        public bool Add(T item)
        {
            return _tree.Insert(item);
        }

        public void Clear()
        {
            _tree = new BinaryTree<T>();
        }

        public bool Contains(T item)
        {
            return _tree.Find(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in _tree.Traverse())
            {
                array[arrayIndex++] = item;
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                _tree.Remove(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _tree.Traverse().GetEnumerator();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            var otherList = other.ToList();
            var currentElements = _tree.Traverse().ToList();
            foreach (var element in currentElements)
            {
                if (!otherList.Contains(element))
                {
                    _tree.Remove(element);
                }
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            var otherList = other.ToList();
            return IsSubsetOf(otherList) && otherList.Count < Count;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            var otherList = other.ToList();
            return IsSupersetOf(otherList) && otherList.Count > Count;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            var otherList = other.ToList();
            return _tree.Traverse().All(
                e => otherList.Contains(e)
            );
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return other.All(e => _tree.Find(e));
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return other.Any(e => _tree.Find(e));
        }

        public bool Remove(T item)
        {
            return _tree.Remove(item);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            var otherList = other.ToList();
            return IsSubsetOf(otherList) && IsSupersetOf(otherList);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            var otherList = other.ToList();
            foreach (var element in otherList)
            {
                if (_tree.Find(element))
                    _tree.Remove(element);
                else
                    _tree.Insert(element);
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            var otherList = other.ToList();
            foreach (var element in otherList)
            {
                _tree.Insert(element);
            }
        }

        void ICollection<T>.Add(T item)
        {
            _tree.Insert(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tree.Traverse().GetEnumerator();
        }
    }

    public interface IFileService
    {
        Stream GetReadStream(string name);
        Stream GetWriteStream(string name);
    }

    public class FileService : IFileService
    {
        public Stream GetReadStream(string name)
        {
            return new FileStream(name, FileMode.Open);
        }

        public Stream GetWriteStream(string name)
        {
            return new FileStream(name, FileMode.OpenOrCreate);
        }
    }

    public class BinaryTreeSetSerializer
    {
        private IFileService fileService;

        public BinaryTreeSetSerializer(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public void Serialize<T>(BinaryTreeSet<T> set, string filename, bool closeStream = true) where T: IComparable
        {
            var formatter = new DataContractJsonSerializer(typeof(BinaryTreeSet<T>));

            Stream fs = fileService.GetWriteStream(filename);
            formatter.WriteObject(fs, set);

            if (closeStream)
                fs.Close();
        }

        public BinaryTreeSet<T> Deserialize<T>(string filename, bool closeStream = true) where T: IComparable
        {
            var formatter = new DataContractJsonSerializer(typeof(BinaryTreeSet<T>));
            Stream fs = fileService.GetReadStream(filename);

            var result = (BinaryTreeSet<T>) formatter.ReadObject(fs);

            if (closeStream)
                fs.Close();

            return result;
        }
    }
}
