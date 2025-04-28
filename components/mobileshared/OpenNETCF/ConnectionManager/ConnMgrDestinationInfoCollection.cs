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


using System.Collections;

namespace OpenNETCF.Net
{
    /// <summary>
    /// Summary description for DestinationInfoCollection.
    /// </summary>
    public class DestinationInfoCollection : CollectionBase
    {
        public DestinationInfoCollection()
        {
        }

        public DestinationInfoCollection(DestinationInfo[] items)
        {
            AddRange(items);
        }

        public DestinationInfoCollection(DestinationInfoCollection items)
        {
            AddRange(items);
        }

        public virtual void AddRange(DestinationInfo[] items)
        {
            foreach (DestinationInfo item in items)
            {
                List.Add(item);
            }
        }

        public virtual void AddRange(DestinationInfoCollection items)
        {
            foreach (DestinationInfo item in items)
            {
                List.Add(item);
            }
        }


        public virtual void Add(DestinationInfo value)
        {
            List.Add(value);
        }

        public virtual bool Contains(DestinationInfo value)
        {
            return List.Contains(value);
        }

        public virtual int IndexOf(DestinationInfo value)
        {
            return List.IndexOf(value);
        }

        public virtual void Insert(int index, DestinationInfo value)
        {
            List.Insert(index, value);
        }

        public virtual DestinationInfo this[int index]
        {
            get { return (DestinationInfo) List[index]; }
            set { List[index] = value; }
        }

        public virtual void Remove(DestinationInfo value)
        {
            List.Remove(value);
        }

        public class Enumerator : IEnumerator
        {
            private IEnumerator wrapped;

            public Enumerator(DestinationInfoCollection collection)
            {
                wrapped = ((CollectionBase) collection).GetEnumerator();
            }

            public DestinationInfo Current
            {
                get { return (DestinationInfo) (wrapped.Current); }
            }

            object IEnumerator.Current
            {
                get { return wrapped.Current; }
            }

            public bool MoveNext()
            {
                return wrapped.MoveNext();
            }

            public void Reset()
            {
                wrapped.Reset();
            }
        }

        public new virtual Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
    }
}