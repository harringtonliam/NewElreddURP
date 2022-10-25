using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.DialogueControl
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName ="RPG/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();
        [SerializeField] Vector2 newNodeOffSet = new Vector2(250, 0);


        Dictionary<string, DialogueNode> nodeLookUp = new Dictionary<string, DialogueNode>();
        Dictionary<string, DialogueNode> childNodeLookUp = new Dictionary<string, DialogueNode>();

        private void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            nodeLookUp.Clear();
            childNodeLookUp.Clear();
            foreach (var node in GetAllNodes())
            {
                nodeLookUp[node.name] = node;

            }
            foreach (var node in GetAllNodes())
            {
                foreach (var childNode in GetAllChildren(node))
                {
                    childNodeLookUp[childNode.name] = childNode;
                }
            }

        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public IEnumerable<DialogueNode> GetRootNodes()
        {
             return GetNodesWithNoParent();
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            List<DialogueNode> result = new List<DialogueNode>();
            foreach (var childId in parentNode.Children)
            {
                if (nodeLookUp.ContainsKey(childId))
                {
                    result.Add(nodeLookUp[childId]);
                }
                
            }
            return result;
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {

            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (node.IsPlayerSpeaking)
                {
                    yield return node;
                }

            }

        }

        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (!node.IsPlayerSpeaking)
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<DialogueNode> GetNodesWithNoParent()
        {

            List<DialogueNode> nodesFound = new List<DialogueNode>();
            foreach (DialogueNode node in nodes)
            {
                if (!childNodeLookUp.ContainsKey(node.name))
                {
                    nodesFound.Add(node);
                }
            }

            return nodesFound;
        }

#if UNITY_EDITOR
        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialog Node");
            AddNode(newNode);
        }



        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialog Node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);

            OnValidate();
        }

        private  DialogueNode MakeNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();

            newNode.name = Guid.NewGuid().ToString();


            if (parent != null)
            {

                parent.AddChild(newNode.name);
                newNode.SetIsPlayerSpeaking(!parent.IsPlayerSpeaking);
                newNode.SetPosition(parent.DialogRect.position + newNodeOffSet);
            }

            return newNode;
        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                AddNode(newNode);
            }
            OnValidate();

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (var node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif 
        }

        public void OnAfterDeserialize()
        {
            //No need to do anything here
        }
    }
}


