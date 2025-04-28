using System;
using System.Xml;
using System.Collections.Generic;
using System.Reflection;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.repository;
using System.Web;

namespace com.teleca.fleetonline.web.bean
{
	[Serializable]
	public class GroupsAndMembersData : RepositoryData
	{

		virtual public System.Collections.IDictionary Groupnames
		{
			get
			{
				return groupnames;
			}
			
			set
			{
				this.groupnames = value;
			}
			
		}
		virtual public System.Collections.ArrayList Keys
		{
			get
			{
				return keys;
			}
			
			set
			{
				this.keys = value;
			}
			
		}
		virtual public System.Collections.IDictionary Memberlists
		{
			get
			{
				return memberlists;
			}
			
			set
			{
				this.memberlists = value;
			}
			
		}
		virtual public System.Collections.ArrayList AllMembers
		{
			get
			{
				return allMembers;
			}
			
			set
			{
				this.allMembers = value;
			}
			
		}
		
		// every key is associated with one groupname and one list of members
		private System.Collections.ArrayList keys = null;
		
		// map with the name of each group
		private System.Collections.IDictionary groupnames = null;
		
		// map with linked lists of members for each group
		private System.Collections.IDictionary memberlists = null;
		

		private System.Collections.ArrayList allMembers = null;
		
		/// <summary> Used by ClientManager to add a new team to the user's team
		/// setup, as defined by the user.
		/// </summary>
		/// <seealso cref="com.teleca.fleetonline.web.action.GroupAction">
		/// </seealso>
		/// <param name="members">who will be in the new group. length >= 0
		/// </param>
		public virtual void addNewGroup(System.String name, System.Collections.IList members)
		{
			// get a 'key' for the new group
			// this should be the last group key + 1
			System.Int32 newKey = (System.Int32) keys.Count;
			keys.Add(newKey);
			groupnames[newKey] = name; //store group name
			
			//		LinkedList members = this.createMemberList(foId,memberUid);
			memberlists[newKey] = members; // store list of members
		}
		
		/// <summary> Used by ClientManager to edit a user's team
		/// setup, as defined by the user.
		/// </summary>
		/// <seealso cref="com.teleca.fleetonline.web.action.GroupAction">
		/// </seealso>
		/// <param name="grpId">The group to adjust
		/// </param>
		/// <param name="members">who will be in the new group. length >= 0
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  editGroup(ref System.Int32 grpId, System.Collections.IList members)
		{
			
			//		List members = null; 
			//		members = (List)createMemberList( foId, membersUid);		
			memberlists[grpId] = members;
		}
		
		/// <summary> Used by ClientManager to delete a user's team.</summary>
		/// <seealso cref="com.teleca.fleetonline.web.action.GroupAction">
		/// </seealso>
		/// <param name="grpId">The group to delete
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  deleteGroup(ref System.Int32 grpId)
		{
			keys.Remove(grpId);
			groupnames.Remove(grpId);
			memberlists.Remove(grpId);
		}
		
		public virtual System.String getName(System.String fmId)
		{
			MemberData data = null;
			for (int i = 0; i < allMembers.Count; i++)
			{
				data = (MemberData) allMembers[i];
				if (data.Userid.Equals(fmId))
				{
					return HttpUtility.UrlDecode(data.Alias);
				}
			}
			return null;
		}

        public static List<GroupsAndMembersData> FromNode(XmlNode sourceNode)
        {
            //XmlDocument workingXmlDoc = new XmlDocument();
            //workingXmlDoc.LoadXml(sourceXML);

            //XmlNodeList nodeList_root = workingXmlDoc.DocumentElement.SelectNodes("//com.teleca.fleetonline.repository.GroupsAndMembersData");

            List<GroupsAndMembersData> list = new List<GroupsAndMembersData>();




            foreach (XmlNode groupsAndMemberDataNode in  sourceNode)
            {
                GroupsAndMembersData gmd = GroupsAndMembersData.DeSerializeSingle(groupsAndMemberDataNode);

                list.Add(gmd);
            }
            return list;
        }
         public static GroupsAndMembersData DeSerializeSingle(XmlNode  sourceNode)
        {
            

                GroupsAndMembersData gmd = new GroupsAndMembersData();

                Utils.FillProperties(gmd,  sourceNode);

                #region old
                //gmd.Memberlists = new Dictionary<string, object>();
                //gmd.AllMembers  = new System.Collections.ArrayList() ;


                //XmlNodeList nodeListMemberlistsData = workingXmlDoc.SelectNodes("//memberlists/com.teleca.fleetonline.repository.MemberData");
                //foreach (XmlNode book in nodeListMemberlistsData)
                //{
                //    // Dit zijn de xml's met memberdata...
                //    MemberData md = new MemberData();
                //    Utils.FillProperties(md, book);
                //    gmd.Memberlists.Add(md.Alias, md);
                //}

                //gmd.allMembers = new System.Collections.ArrayList();
                //XmlNodeList nodeListMemberData = workingXmlDoc.SelectNodes("//allMembers/com.teleca.fleetonline.repository.MemberData");

                //foreach (XmlNode book in nodeListMemberData)
                //{
                //    // Dit zijn de xml's met memberdata...
                //    MemberData md = new MemberData();
                //    Utils.FillProperties(md, book);
                //    gmd.allMembers.Add(md);
                //}

#endregion
               
                  return gmd;
            }
          
        }
	}



	
	
	/*
	
	* GROUPS *
	ID | FOID | NAME
	------------------
	1    1     Drivers
	2    1     Managers
	3    1     Friends
	4    2     Workers
	
	|
	|
	|
	-----
	| | |
	| | |
	
	* GROUPMEMBERS *
	ID | GROUPID | USERID
	-----------------------
	1      1       Olle
	2      1       Kalle
	3      1       Pelle
	4      2       Olle
	5      2       Nisse
	
	*/
