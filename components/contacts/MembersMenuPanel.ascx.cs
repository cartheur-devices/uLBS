using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.teleca.fleetonline.repository;
using com.teleca.fleetonline.utils;
using System.Text;

namespace FindWhere.UserControls
{
    public partial class UserControl_MembersMenuPanel : System.Web.UI.UserControl
    {
        private Boolean isMainMenu = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!alreadyLoaded)
            {
                AddClientJavaScript();
                tvMembers.Attributes.Add("onClick", "client_OnTreeNodeCheckedSelectSubnodes" + tvMembers.ClientID + "(event);UpdateSelectedMembers" + tvMembers.ClientID + "();");                
            }            
        }

        private void AddClientJavaScript()
        {
            string script1 = @" function client_OnTreeNodeCheckedSelectSubnodes" + tvMembers.ClientID + @"(evt)
{
    
 //var src = window.event != window.undefined ? window.event.srcElement : evt.target;
var src =  evt.target;

if (evt.target ==null) src= window.event.srcElement;
        var isChkBoxClick = (src.tagName.toLowerCase() == 'input' && src.type == 'checkbox');
        if(isChkBoxClick)
        {
            var parentTable = GetParentByTagName" + tvMembers.ClientID + @"('table', src);
            var nxtSibling = parentTable.nextSibling;
            if(nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            {
                if(nxtSibling.tagName.toLowerCase() == 'div') //if node has children
                {
                    //check or uncheck children at all levels
                    CheckUncheckChildren" + tvMembers.ClientID + @"(parentTable.nextSibling, src.checked);
                }
            }
            TreeNodeUpdateSelectedMember" + tvMembers.ClientID + @"(src.value, src.checked);

            //check or uncheck parents at all levels
            CheckUncheckParents" + tvMembers.ClientID + @"(src, src.checked);
        }
   } 
   function CheckUncheckChildren" + tvMembers.ClientID + @"(childContainer, check)
   {
      var childChkBoxes = childContainer.getElementsByTagName('input');
      var childChkBoxCount = childChkBoxes.length;
      for(var i = 0; i<childChkBoxCount; i++)
      {
        childChkBoxes[i].checked = check;
        TreeNodeUpdateSelectedMember" + tvMembers.ClientID + @"(childChkBoxes[i].value, check);
      }
   }
   function CheckUncheckParents" + tvMembers.ClientID + @"(srcChild, check)
   {
       var parentDiv = GetParentByTagName" + tvMembers.ClientID + @"('div', srcChild);
       var parentNodeTable = parentDiv.previousSibling;
       
       if(parentNodeTable)
        {
            var checkUncheckSwitch;
            
            if(check) //checkbox checked
            {
                var isAllSiblingsChecked = AreAllSiblingsChecked" + tvMembers.ClientID + @"(srcChild);
                if(isAllSiblingsChecked)
                    checkUncheckSwitch = true;
                else    
                    return; //do not need to check parent if any(one or more) child not checked
            }
            else //checkbox unchecked
            {
                checkUncheckSwitch = false;
            }
            
            var inpElemsInParentTable = parentNodeTable.getElementsByTagName('input');
            if(inpElemsInParentTable.length > 0)
            {
                var parentNodeChkBox = inpElemsInParentTable[0]; 
                parentNodeChkBox.checked = checkUncheckSwitch; 
                //do the same recursively
                CheckUncheckParents" + tvMembers.ClientID + @"(parentNodeChkBox, checkUncheckSwitch);
            }
        }
   }
   function AreAllSiblingsChecked" + tvMembers.ClientID + @"(chkBox)
   {
     var parentDiv = GetParentByTagName" + tvMembers.ClientID + @"('div', chkBox);
     var childCount = parentDiv.childNodes.length;
     for(var i=0; i<childCount; i++)
     {
        if(parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
        {
            if(parentDiv.childNodes[i].tagName.toLowerCase() == 'table')
            {
               var prevChkBox = parentDiv.childNodes[i].getElementsByTagName('input')[0];
              //if any of sibling nodes are not checked, return false
              if(!prevChkBox.checked) 
              {
                return false;
              } 
            }
        }
     }
     return true;
   }
   //utility function to get the container of an element by tagname
   function GetParentByTagName" + tvMembers.ClientID + @"(parentTagName, childElementObj)
   {
      var parent = childElementObj.parentNode;
      while(parent.tagName.toLowerCase() != parentTagName.toLowerCase())
      {
         parent = parent.parentNode;
      }
    return parent;    
   }


                ";

            string scriptKey1 = "menuScript1" + this.ClientID;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), scriptKey1, script1, true);

            string script2 = @" function TreeNodeUpdateSelectedMember" + tvMembers.ClientID + @"(memberID2, checkstate2)
{

    var treeview2 = document.getElementById('" + tvMembers.ClientID + @"');
    var inputElements2 = treeview2.getElementsByTagName('input');

    for(i2=0;i2<inputElements2.length;i2++)
        {
            if(inputElements2[i2].type == 'checkbox')
                {
                   if(inputElements2[i2].value == memberID2)
                        {
                            inputElements2[i2].checked = checkstate2;
                        }
                }
        }      
}

                ";
            string scriptKey2 = "menuScript2" + this.ClientID;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), scriptKey2, script2, true);

            string script3 = @" function UpdateSelectedMembers" + tvMembers.ClientID + @"()         
    {
        var result = '';
        var count  = 0;
        
        var memberPanelTreeView_local = $get('" + tvMembers.ClientID + @"');
        
        var inputElements = memberPanelTreeView_local.getElementsByTagName('input');   // memberPanelTreeViewID defined in usercontrol.

        for(i=0; i<inputElements.length; i++)
        {
            if(inputElements[i].type == 'checkbox')
            {
                if(inputElements[i].checked == true)
                {
                    if (count>0 && inputElements[i].value != '-1' && inputElements[i].value.substring(0,1)!='G')
                    {
                        result = result + '|';
                    }
                       
                    if (inputElements[i].value != '-1' && inputElements[i].value.substring(0,1)!='G')
                    {
                        result = result + inputElements[i].value;
                        count = count + 1 ;
                    }
                }
            }
        }   
       var shidden = document.getElementById('" + hdnSelected.ClientID + @"');
        shidden.value = result;
    }    



    function CheckUncheckAll" + tvMembers.ClientID + @"(CheckValue)
        {
         var treeview2 = document.getElementById('" + tvMembers.ClientID + @"');
         var inputElements2 = treeview2.getElementsByTagName('input');

            for(i2=0;i2<inputElements2.length;i2++)
                {
                    if(inputElements2[i2].type == 'checkbox')
                        {
                                inputElements2[i2].checked = CheckValue;
                        }
                }   
        }



                ";
            string scriptKey3 = "menuScript3" + this.ClientID;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), scriptKey3, script3, true);
        }

        private Boolean alreadyLoaded
        {
            get
            {
                if (ViewState["alreadyLoaded"] == null) return false;
                else return (Boolean)ViewState["alreadyLoaded"];
            }
            set
            {
                ViewState["alreadyLoaded"] = value;
            }
        }

        public string GetHiddenFieldSelectedMembersClientID()
        {
            return this.hdnSelected.ClientID;
        }

        public string GetHiddenFieldAllMembersClientID()
        {
            return this.hdnAllMembers.ClientID;
        }

        public string GetUncheckAllJavascriptFunctionName(Boolean CheckValue)
        {
            return string.Concat("CheckUncheckAll", tvMembers.ClientID, "(" + CheckValue.ToString().ToLower() + ")");            
        }

        public void BuildMainMenu(Boolean IgnoreOndemand)
        {
            isMainMenu = true;
            BuildMenuNew(IgnoreOndemand, null);            
        }

        public void BuildMenu(Boolean IgnoreOndemand)
        {
            BuildMenuNew(IgnoreOndemand, null);
        }       

        public void BuildMenu(Boolean IgnoreOndemand, string[] deviceTypes)
        {
            BuildMenuNew(IgnoreOndemand, deviceTypes);          
        }
     
        public string[] GetSelectedMembers()
        {            
            string[] s = hdnSelected.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            ArrayList arTemp = new ArrayList();
            foreach (string st in s)
            {
                if (!arTemp.Contains(st))                
                    arTemp.Add(st);                
            }
            return (string[])arTemp.ToArray(typeof(string)); ;
        }

        public string GetSelectedMembersCombined()
        {
            StringBuilder retString = new StringBuilder();
            retString.Append("|");
            foreach (string s in GetSelectedMembers())            
                retString.Append(s + "|");            
            return retString.ToString();
        }

        public string GetSelectedMembersCombined(string separator)
        {
            StringBuilder retString = new StringBuilder();
            foreach (string s in GetSelectedMembers())
            {
                retString.Append(s);
                retString.Append(separator);
            }
            return retString.ToString();
        }


        public void LoadMembersWithDeviceTypes(string[] deviceTypes)
        {
            BuildMenuNew(true, deviceTypes);
        }

        public void CheckMembers(string[] members)
        {
            foreach (TreeNode mainNode in this.tvMembers.Nodes)
            {
                foreach (TreeNode subNode in mainNode.ChildNodes)
                {
                    if (((IList)members).Contains(subNode.Value))
                        subNode.Checked = true;
                }
            }

            //update the selected members
            this.hdnSelected.Value = "";
            ArrayList arTemp = new ArrayList();

            int counter = 0;
            foreach (TreeNode mainNode in this.tvMembers.Nodes)
            {
                foreach (TreeNode subNode in mainNode.ChildNodes)
                {
                    if (subNode.Checked == true)
                    {
                        if (!arTemp.Contains(subNode.Value))                        
                            arTemp.Add(subNode.Value);                        
                    }
                }
            }

            for (int i = 0; i < arTemp.Count; i++)
            {
                if (i > 0)                
                    hdnSelected.Value = hdnSelected.Value + "|" + (string)arTemp[i];                
                else                
                    hdnSelected.Value = (string)arTemp[i];                
                counter++;
            }
        }

        public string GetMenuTreeClientID()
        {
            return tvMembers.ClientID;
        }

        public string GetMenuTreeJavascriptFunctionName()
        {
            return "GetSelectedMembers" + tvMembers.ClientID;
        }

        private void BuildMenuNew(Boolean IgnoreOndemand, string[] deviceTypes)
        {
            this.hdnAllMembers.Value = "";
            this.hdnSelected.Value = "";
            tvMembers.Nodes.Clear();
            List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];

            tvMembers.Width = 100;
            tvMembers.Font.Size = 7;

            foreach (com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd in gmds)
            {
                int counter = 0;
                //default group
                CustomTreeNode main = new CustomTreeNode((String)this.GetGlobalResourceObject("Taal", "MembersMenuPanel_Label_AllDevices"), "-1"); // groupid voor default = -1
                main.SpecialShowCheckbox = true;
                main.SelectAction = TreeNodeSelectAction.None;
                main.MainMenuNode = isMainMenu;

                if (isMainMenu)
                {
                    main.Value = "";
                    // on the main node we need the id's for all devices
                    string ids = "";
                    foreach (com.teleca.fleetonline.repository.MemberData md in gmd.AllMembers)                    
                        ids = ids + "|" + md.Userid;                    
                    main.Value = ids;
                }

                tvMembers.Nodes.Add(main);

                int counterAll = 0;
                foreach (MemberData mdAll in gmd.AllMembers)
                {
                    // check for owner
                    if (mdAll.UserType != 0)
                    {
                        bool ignore4thisDevice = mdAll.UserType == 40; //mobile

                        if (IgnoreOndemand || (!IgnoreOndemand && mdAll.OnDemand == 2 && !mdAll.Blocked) || ignore4thisDevice)
                        {
                            CustomTreeNode member = new CustomTreeNode(mdAll.Alias, mdAll.Userid, FindWhere.Utils.Utils.getImgUrlByUserType(Request, mdAll.IconId));
                            member.SelectAction = TreeNodeSelectAction.None;
                            member.MainMenuNode = isMainMenu;
                            member.Value = mdAll.Userid;
                            member.SpecialShowCheckbox = IgnoreOndemand || (mdAll.OnDemand == 2 && !mdAll.Blocked) || ignore4thisDevice;

                            if (deviceTypes == null)
                                tvMembers.Nodes[counter].ChildNodes.Add(member);

                            else if (deviceTypes != null && ((IList)deviceTypes).Contains(mdAll.UserType.ToString()))
                                tvMembers.Nodes[counter].ChildNodes.Add(member);

                            if (counterAll > 0)
                                hdnAllMembers.Value = hdnAllMembers.Value + "|" + mdAll.Userid;
                            else
                                hdnAllMembers.Value = mdAll.Userid;
                            counterAll++;
                        }
                    }
                }
                counter++;

                if (gmd.Groupnames != null)
                {
                    foreach (KeyValuePair<int, string> kvp in (Dictionary<int, string>)gmd.Groupnames)
                    {
                        CustomTreeNode group;
                        if (isMainMenu)
                            group = new CustomTreeNode(kvp.Value, "");
                        else
                            group = new CustomTreeNode(kvp.Value, "G" + kvp.Key);

                        group.SpecialShowCheckbox = true;
                        group.MainMenuNode = isMainMenu;
                        group.SelectAction = TreeNodeSelectAction.None;
                        if (isMainMenu)
                            group.Value = "";

                        foreach (com.teleca.fleetonline.repository.MemberData md in gmd.AllMembers)
                        {
                            if (md.UserType != 0)
                            {
                                bool ignore4thisDevice = md.UserType == 40; //mobile
                                if (IgnoreOndemand || (!IgnoreOndemand && md.OnDemand == 2 && !md.Blocked) || ignore4thisDevice)
                                {
                                    CustomTreeNode member = new CustomTreeNode(md.Alias, md.Userid, FindWhere.Utils.Utils.getImgUrlByUserType(Request, md.IconId));
                                    member.SelectAction = TreeNodeSelectAction.None;
                                    member.MainMenuNode = isMainMenu;
                                    if (md.OnDemand != 2 || md.Blocked)
                                    {
                                        member.SpecialShowCheckbox = false;
                                        member.Value = md.Userid;
                                    }

                                    //Look if the member is in the group
                                    foreach (KeyValuePair<int, com.teleca.fleetonline.repository.MemberData[]> kvpMemList in (Dictionary<int, com.teleca.fleetonline.repository.MemberData[]>)gmd.Memberlists)
                                    {
                                        if (kvpMemList.Key == kvp.Key)
                                        {
                                            foreach (com.teleca.fleetonline.repository.MemberData mdd in kvpMemList.Value)
                                            {
                                                if (md.Userid == mdd.Userid)
                                                {
                                                    member.Value = md.Userid;
                                                    member.SpecialShowCheckbox = IgnoreOndemand || (md.OnDemand == 2 && !md.Blocked) || ignore4thisDevice;

                                                    if (isMainMenu)
                                                        group.Value = group.Value + "|" + md.Userid;

                                                    if (deviceTypes == null)
                                                        group.ChildNodes.Add(member);

                                                    else if (deviceTypes != null && ((IList)deviceTypes).Contains(md.UserType.ToString()))
                                                        group.ChildNodes.Add(member);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (group.ChildNodes.Count > 0)
                            tvMembers.Nodes.Add(group);
                        counter++;
                    }
                }
            }
            tvMembers.ExpandAll();
        }
    }
}