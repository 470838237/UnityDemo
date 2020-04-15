using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;

namespace GCloud
{
	
	public delegate void QueryAllHandler(Result result, TreeCollection treeList);
	public delegate void QueryTreeHandler(Result result, TreeInfo nodeList);
	public delegate void QueryLeafHandler(Result result, NodeWrapper node);


	public interface ITdir
	{
        /// <summary>
        /// Callback of QueryAll
        /// </summary>
		event QueryAllHandler QueryAllEvent;
        
        /// <summary>
        /// Callback of QueryTree
        /// </summary>
		event QueryTreeHandler QueryTreeEvent;
        
        /// <summary>
        /// Callback of QueryLeaf
        /// </summary>
		event QueryLeafHandler QueryLeafEvent;

        /// <summary>
        /// Initialize tdir
        /// </summary>
        /// <returns>Result of Initialization data</returns>
        /// <param name="initInfo">Information to initialize tdir</param>
		bool Initialize(TdirInitInfo initInfo);
		//int QueryAll();

		/// <summary>
		/// Queries the tree by treeId.
		/// </summary>
		/// <returns>The SeqId of receive data,return -1 means QueryTree failed</returns>
		/// <param name="treeId">TreeId == PlatformId</param>
		int QueryTree(Int32 treeId);

		/// <summary>
		/// Queries the leaf by treeId and leafId.
		/// </summary>
		/// <returns>The SeqId of receive data,return -1 means QueryLeaf failed</returns>
		/// <param name="treeId">TreeId == PlatformId</param>
		/// <param name="leafId">LeafId == ZoneId</param>
		int QueryLeaf(Int32 treeId, Int32 leafId);

        /// <summary>
        /// Drive the Tdir underlying module to send and receive data
        /// </summary>
		void Update();
        
        /// <summary>
        /// GCloudSDK2.0.6 was used to query whether the current connection is normal.
        /// After the successful connection to the server, other subsequent operations can be performed.
        /// Deprecated after GCloudSDK 2.0.6
        /// </summary>
        /// <returns>is Connect</returns>
		bool IsConnected();
	};

	public class TdirFactory
	{
		public static ITdir CreateInstance()
		{
			return Tdir.Instance;
		}
	}
    
	public class  TdirInitInfo : ApolloBufferBase
	{
		public string OpenID;//OpenId of player
		public string Url;//The Url of the Tdir server can pass in multiple domain names/IPs, separated by ",".
		public bool EnableManualUpdate;//Whether the callback of the navigation module is driven by the game itself, currently only true
        public int MaxIdleTime=3;//The maximum idle time is connected, and the timeout is disconnected.
        
		public override void WriteTo (ApolloBufferWriter write)
		{
			write.Write (OpenID);
			write.Write (Url);
            write.Write (EnableManualUpdate);
            write.Write (MaxIdleTime);
		}
        
		public override void ReadFrom (ApolloBufferReader reader)
		{
			reader.Read (ref OpenID);
			reader.Read (ref Url);
            reader.Read (ref EnableManualUpdate);
            reader.Read (ref MaxIdleTime);
		}
	}
    
	public enum TreeNodeType
	{
		Category,
		Leaf,
	};
    
	[Flags]
	public enum TreeNodeFlag
	{
		Heavy = 0x10,
		Crown = 0x20,
		Fine = 0x40,
		Unavailable = 0x80,
        
	}
    
	[Flags]
	public enum TreeNodeTag
	{
		Hot = 0x01,
		Recommend = 0x02,
		New = 0x04,
		Limited = 0x08,
		Experience = 0x10,
	}

	
	public class TdirCustomData : ApolloBufferBase
	{
        //Corresponding backend custom value 1
		public int Attr1;
        //Corresponding backend custom value 2
		public int Attr2;
        //Corresponding backend custom data
		public string UserData;

		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(Attr1);
			writer.Write(Attr2);
			writer.Write(UserData);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref Attr1);
			reader.Read(ref Attr2);
			reader.Read(ref UserData);
		}
	};
    
	public abstract class TreeNodeBase : ApolloBufferBase
	{
		public Int32 Id;//Id of node
		public Int32 ParentId;//Id of parent
		public string Name;//Name of node
		public TreeNodeTag Tag;//Tag of node,such hot and new
		public TdirCustomData CustomData;
        
		public TreeNodeBase (TreeNodeType type)
		{
			this.type = type;
		}
    
		public TreeNodeType Type
		{
			get
			{
				return type;
			}
		}
        
		public bool IsCategory ()
		{
			return type == TreeNodeType.Category;
		}
        
		public bool IsLeaf ()
		{
			return type == TreeNodeType.Leaf;
		}
        
		public bool IsRoot ()
		{
			if (IsCategory ()) {
				return ParentId == -1;
			}
			return false;
		}
        
		protected override void BeforeEncode (ApolloBufferWriter writer)
		{
			writer.Write (type);
            
			writer.Write (Id);
			writer.Write (ParentId);
			writer.Write (Name);
			writer.Write (Tag);
			writer.Write (CustomData);
		}
        
		protected override void BeforeDecode (ApolloBufferReader reader)
		{
			reader.Read (ref type);
            
			reader.Read (ref Id);
			reader.Read (ref ParentId);
			reader.Read (ref Name);
			reader.Read (ref Tag);
			reader.Read (ref CustomData);
		}
        
		private TreeNodeType type;
	};
    
	public class CategoryNode : TreeNodeBase
	{
		public CategoryNode ()
        : base(TreeNodeType.Category)
		{
		}

		public override void WriteTo (ApolloBufferWriter write)
		{
		}
        
		public override void ReadFrom (ApolloBufferReader reader)
		{
		}
	};
    
	public class LeafNode : TreeNodeBase
	{
		public TreeNodeFlag Flag;//Flag to show the Congestion
		public string Url;//Url of game service
		public TDirRoleCollection RoleCollection;//information of role
    
		public LeafNode ()
        : base(TreeNodeType.Leaf)
		{
		}

		public override void WriteTo (ApolloBufferWriter writer)
		{
			writer.Write (Flag);
			writer.Write (Url);
			writer.Write (RoleCollection);
		}
        
		public override void ReadFrom (ApolloBufferReader reader)
		{
			reader.Read (ref Flag);
			reader.Read (ref Url);
			reader.Read (ref RoleCollection);
		}
	};
    
	public class NodeWrapper : ApolloBufferBase
	{
		public TreeNodeType Type;//Type of node
		public CategoryNode Category;//Data of non-leaf
		public LeafNode Leaf;//Data of leaf
    
		public TreeNodeBase GetNode ()
		{
			if (IsCategory ()) {
				return Category;
			} else if (IsLeaf ()) {
				return Leaf;
			}
			return null;
		}
    
		public bool IsCategory ()
		{
			return Type == TreeNodeType.Category;
		}
        
		public bool IsLeaf ()
		{
			return Type == TreeNodeType.Leaf;
		}
        
		public bool IsRoot ()
		{
			if (IsCategory ()) {
				return Category.IsRoot ();
			}
			return false;
		}
        
		public override void WriteTo (ApolloBufferWriter writer)
		{
			writer.Write (Type);
            
			switch (Type) {
			case TreeNodeType.Category:
				{
					writer.Write (Category);
					break;
				}
			case TreeNodeType.Leaf:
				{
					writer.Write (Leaf);
					break;
				}
			default:
				return;
			}
		}
        
		public override void ReadFrom (ApolloBufferReader reader)
		{
			int tmp = 0;
			reader.Read (ref tmp);
			Type = (TreeNodeType)tmp;
			switch (Type) {
			case TreeNodeType.Category:
				{
					Category = new CategoryNode();
					reader.Read (ref Category);
				}
				break;
			case TreeNodeType.Leaf:
				{
					Leaf = new LeafNode();
					reader.Read (ref Leaf);
					break;
				}
			default:
				return;
			}
		}
        
	};
    
	public class TreeInfo : ApolloBufferBase
	{
		private List<NodeWrapper> nodeList = new List<NodeWrapper>();
		public List<NodeWrapper> NodeList
		{
			get
			{
				if(nodeList == null)
				{
					nodeList = new List<NodeWrapper>();
				}
				return nodeList;
			}
		}
        
		public Int32 TreeId {
			get {
				if (NodeList != null && NodeList.Count > 0) {
					for (int i = 0; i<NodeList.Count; i++) {
						NodeWrapper node = NodeList[i];
						if (node != null) {
							if (node.IsRoot()) {
								TreeNodeBase nd = node.GetNode();
								if (nd != null) 
								{
									return nd.Id;
								}
							}
						}
					}
				}
				return -1;
			}
		}
        
		public override void WriteTo (ApolloBufferWriter writer)
		{
			writer.Write (NodeList);
		}
        
		public override void ReadFrom (ApolloBufferReader reader)
		{
			reader.Read(ref nodeList);
		}
        
	};
    
	public class TreeCollection : ApolloBufferBase
	{
		private List<TreeInfo> treeList = new List<TreeInfo>();
		public List<TreeInfo> TreeList
		{
			get
			{
				if(treeList == null)
				{
					treeList = new List<TreeInfo>();
				}
				return treeList;
			}
		}
        
		public override void WriteTo (ApolloBufferWriter writer)
		{
			writer.Write (TreeList);
		}
        
		public override void ReadFrom (ApolloBufferReader reader)
		{
			reader.Read (ref treeList);
		}
        
	};

	
	
	public class TDirRoleInfo : ApolloBufferBase
	{
		public string OpenId;//OpenId of player
		public Int32 TreeId; //Service id
		public Int32 LeafId;//Id of leaf node
		public UInt64 LastLoginTime;//Last login time
		public UInt64 RoleId;//Id of role
		public Int32 RoleLevel;//Level of role
		public string RoleName;//Name of role
		public string UserData;//Custom data
		
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(OpenId);
			writer.Write(TreeId);
			writer.Write(LeafId);
			writer.Write(LastLoginTime);
			writer.Write(RoleId);
			writer.Write(RoleLevel);
			writer.Write(RoleName);
			writer.Write(UserData);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref OpenId);
			reader.Read(ref TreeId);
			reader.Read(ref LeafId);
			reader.Read(ref LastLoginTime);
			reader.Read(ref RoleId);
			reader.Read(ref RoleLevel);
			reader.Read(ref RoleName);
			reader.Read(ref UserData);
		}
	};

	public class TDirRoleCollection : ApolloBufferBase
	{
		public List<TDirRoleInfo> RoleInfos;
		

		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(RoleInfos);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref RoleInfos);
		}
	};
}
