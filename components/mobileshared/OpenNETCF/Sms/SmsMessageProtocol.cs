#region OpenNETCF Copyright Information
/*
 *******************************************************************
|                                                                   |
|           OpenNETCF Smart Device Framework 2.2                    |
|                                                                   |
|                                                                   |
|       Copyright (c) 2000-2008 OpenNETCF Consulting LLC            |
|       ALL RIGHTS RESERVED                                         |
|                                                                   |
|   The entire contents of this file is protected by U.S. and       |
|   International Copyright Laws. Unauthorized reproduction,        |
|   reverse-engineering, and distribution of all or any portion of  |
|   the code contained in this file is strictly prohibited and may  |
|   result in severe civil and criminal penalties and will be       |
|   prosecuted to the maximum extent possible under the law.        |
|                                                                   |
|   RESTRICTIONS                                                    |
|                                                                   |
|   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           |
|   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          |
|   SECRETS OF OPENNETCF CONSULTING LLC THE REGISTERED DEVELOPER IS |
|   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    |
|   CONTROLS AS PART OF A COMPILED EXECUTABLE PROGRAM ONLY.         |
|                                                                   |
|   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      |
|   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        |
|   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       |
|   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  |
|   AND PERMISSION FROM OPENNETCF CONSULTING LLC                    |
|                                                                   |
|   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       |
|   ADDITIONAL RESTRICTIONS.                                        |
|                                                                   |
 ******************************************************************* 
*/
#endregion

using System;

namespace OpenNETCF.Phone.Sms
{
	/// <summary>
	/// SMS Protocol Identifier (PID) constants.
	/// </summary>
	public enum MessageProtocol : int
	{
		Unknown  = 0x00000000 ,
		SmeToSme  = 0x00000001 ,
		Implicit  = 0x00000002 ,
		Telex  = 0x00000003 ,
		TeleFaxGroup3  = 0x00000004 ,
		TeleFaxGroup4  = 0x00000005 ,
		VoicePhone  = 0x00000006 ,
		Ermes  = 0x00000007 ,
		Paging  = 0x00000008 ,
		VideoTex  = 0x00000009 ,
		TeleTex  = 0x0000000a ,
		TeleTexPspdn  = 0x0000000b ,
		TeleTexCspdn  = 0x0000000c ,
		TeleTexPstn  = 0x0000000d ,
		TeleTexIsdn  = 0x0000000e ,
		Uci  = 0x0000000f ,
		MsgHandling  = 0x00000010 ,
		X400  = 0x00000011 ,
		EMail  = 0x00000012 ,
		SCSpecific1  = 0x00000013 ,
		SCSpecific2  = 0x00000014 ,
		SCSpecific3  = 0x00000015 ,
		SCSpecific4  = 0x00000016 ,
		SCSpecific5  = 0x00000017 ,
		SCSpecific6  = 0x00000018 ,
		SCSpecific7  = 0x00000019 ,
		GsmStation  = 0x0000001a ,
		SM_TYPE0  = 0x0000001b ,
		RSM_TYPE1  = 0x0000001c ,
		RSM_TYPE2  = 0x0000001d ,
		RSM_TYPE3  = 0x0000001e ,
		RSM_TYPE4  = 0x0000001f ,
		RSM_TYPE5  = 0x00000020 ,
		RSM_TYPE6  = 0x00000021 ,
		RSM_TYPE7  = 0x00000022 ,
		ReturnCall  = 0x00000023 ,
		MeDownload  = 0x00000024 ,
		Depersonalization  = 0x00000025 ,
		SimDownload  = 0x00000026 ,
	}
}
