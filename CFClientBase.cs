﻿using System;
using System.Xml;

namespace Microsoft.Tools.ServiceModel
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.NetCFSvcUtil", "3.5.0.0")]
    public class CFClientBase<TChannel>
        where TChannel :  class
    {
        
        private System.ServiceModel.Channels.Binding binding;
        
        private System.ServiceModel.Channels.CustomBinding oneWayBinding;
        
        private System.ServiceModel.Channels.IChannelFactory<System.ServiceModel.Channels.IRequestChannel> _requestChannelFactory;
        
        private System.ServiceModel.Channels.IChannelFactory<System.ServiceModel.Channels.IOutputChannel> _outputChannelFactory;
        
        private System.ServiceModel.Description.ClientCredentials _clientCredentials;
        
        private System.ServiceModel.EndpointAddress remoteAddress;
        
        private System.ServiceModel.Channels.BindingParameterCollection _parameters;
        
        private object requestChannelFactorySyncObject;
        
        private object outputChannelFactorySyncObject;
        
        private System.Collections.Generic.Dictionary<CFContractSerializerInfo, System.Runtime.Serialization.XmlObjectSerializer> serializers = new System.Collections.Generic.Dictionary<CFContractSerializerInfo, System.Runtime.Serialization.XmlObjectSerializer>(2);
        
        public CFClientBase(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress)
        {
            if ((binding == null))
            {
                throw new System.ArgumentNullException("binding");
            }
            if ((remoteAddress == null))
            {
                throw new System.ArgumentNullException("remoteAddress");
            }
            this.remoteAddress = remoteAddress;
            this.binding = binding;
            this._clientCredentials = new System.ServiceModel.Description.ClientCredentials();
            this._parameters = new System.ServiceModel.Channels.BindingParameterCollection();
            this._parameters.Add(this._clientCredentials);
            this.outputChannelFactorySyncObject = new object();
            this.requestChannelFactorySyncObject = new object();
        }
        
        public System.ServiceModel.Description.ClientCredentials ClientCredentials
        {
            get
            {
                return this._clientCredentials;
            }
        }
        
        protected System.ServiceModel.Channels.BindingParameterCollection Parameters
        {
            get
            {
                return this._parameters;
            }
        }
        
        private System.ServiceModel.Channels.IChannelFactory<System.ServiceModel.Channels.IOutputChannel> OutputChannelFactory
        {
            get
            {
                if ((this._outputChannelFactory == null))
                {
                    System.Threading.Monitor.Enter(this.outputChannelFactorySyncObject);
                    try
                    {
                        if ((this._outputChannelFactory == null))
                        {
                            if ((this.oneWayBinding == null))
                            {
                                this.oneWayBinding = new System.ServiceModel.Channels.CustomBinding(this.binding);
                            }
                            if (this.oneWayBinding.CanBuildChannelFactory<System.ServiceModel.Channels.IOutputChannel>(this.Parameters))
                            {
                                this._outputChannelFactory = this.oneWayBinding.BuildChannelFactory<System.ServiceModel.Channels.IOutputChannel>(this.Parameters);
                                this._outputChannelFactory.Open();
                            }
                        }
                    }
                    finally
                    {
                        System.Threading.Monitor.Exit(this.outputChannelFactorySyncObject);
                    }
                }
                return this._outputChannelFactory;
            }
        }
        
        private System.ServiceModel.Channels.IChannelFactory<System.ServiceModel.Channels.IRequestChannel> RequestChannelFactory
        {
            get
            {
                if ((this._requestChannelFactory == null))
                {
                    System.Threading.Monitor.Enter(this.requestChannelFactorySyncObject);
                    try
                    {
                        if ((this._requestChannelFactory == null))
                        {
                            if (this.binding.CanBuildChannelFactory<System.ServiceModel.Channels.IRequestChannel>(this.Parameters))
                            {
                                this._requestChannelFactory = this.binding.BuildChannelFactory<System.ServiceModel.Channels.IRequestChannel>(this.Parameters);
                                this._requestChannelFactory.Open();
                            }
                        }
                    }
                    finally
                    {
                        System.Threading.Monitor.Exit(this.requestChannelFactorySyncObject);
                    }
                }
                return this._requestChannelFactory;
            }
        }
        
        protected static void ApplyProtection(string action, System.ServiceModel.Security.ScopedMessagePartSpecification parts, bool protection)
        {
            System.ServiceModel.Security.MessagePartSpecification partSpec;
            if (protection)
            {
                partSpec = new System.ServiceModel.Security.MessagePartSpecification(true);
            }
            else
            {
                partSpec = System.ServiceModel.Security.MessagePartSpecification.NoParts;
            }
            parts.AddParts(partSpec, action);
        }
        
        protected static bool IsSecureMessageBinding(System.ServiceModel.Channels.Binding binding)
        {
            if (typeof(System.ServiceModel.BasicHttpBinding).IsInstanceOfType(binding))
            {
                return false;
            }
            if (typeof(System.ServiceModel.Channels.CustomBinding).IsInstanceOfType(binding))
            {
                return ((System.ServiceModel.Channels.CustomBinding)(binding)).Elements.Contains(typeof(System.ServiceModel.Channels.AsymmetricSecurityBindingElement));
            }
            throw new System.NotSupportedException("Unsupported binding type.");
        }
        
        protected void Close()
        {
            if ((this.RequestChannelFactory != null))
            {
                System.Threading.Monitor.Enter(this.RequestChannelFactory);
                try
                {
                    this.RequestChannelFactory.Close();
                }
                finally
                {
                    System.Threading.Monitor.Exit(this.RequestChannelFactory);
                }
            }
            if ((this.OutputChannelFactory != null))
            {
                System.Threading.Monitor.Enter(this.OutputChannelFactory);
                try
                {
                    this.OutputChannelFactory.Close();
                }
                finally
                {
                    System.Threading.Monitor.Exit(this.OutputChannelFactory);
                }
            }
        }
        
        protected TRESPONSE Invoke<TREQUEST, TRESPONSE>(CFInvokeInfo info, TREQUEST request)
        
        
        {
            CFContractSerializerInfo serializerInfo = new CFContractSerializerInfo();
            serializerInfo.MessageContractType = typeof(TREQUEST);
            serializerInfo.IsWrapped = info.RequestIsWrapped;
            serializerInfo.ExtraTypes = info.ExtraTypes;
            serializerInfo.UseEncoded = info.UseEncoded;
            System.ServiceModel.Channels.Message msg = System.ServiceModel.Channels.Message.CreateMessage(this.binding.MessageVersion, info.Action, request, GetContractSerializer(serializerInfo));
            return this.getResult<TRESPONSE>(this.getReply(msg), info);
        }
        
        protected void Invoke<TREQUEST>(CFInvokeInfo info, TREQUEST request)
        
        {
            CFContractSerializerInfo serializerInfo = new CFContractSerializerInfo();
            serializerInfo.MessageContractType = typeof(TREQUEST);
            serializerInfo.IsWrapped = info.RequestIsWrapped;
            serializerInfo.ExtraTypes = info.ExtraTypes;
            serializerInfo.UseEncoded = info.UseEncoded;
            System.ServiceModel.Channels.Message msg = System.ServiceModel.Channels.Message.CreateMessage(this.binding.MessageVersion, info.Action, request, GetContractSerializer(serializerInfo));
            if (info.IsOneWay)
            {
                if ((this._outputChannelFactory != null))
                {
                    this.postOneWayMessage(msg);
                }
                else
                {
                    this.getReply(msg);
                }
            }
            else
            {
                this.processReply(this.getReply(msg));
            }
        }
        
        private void postOneWayMessage(System.ServiceModel.Channels.Message msg)
        {
            if ((this.OutputChannelFactory == null))
            {
                // transport doesn't support one-way messages
                throw new System.NotSupportedException();
            }
            System.ServiceModel.Channels.IOutputChannel outputChannel;
            System.Threading.Monitor.Enter(this.OutputChannelFactory);
            try
            {
                outputChannel = this.OutputChannelFactory.CreateChannel(this.remoteAddress);
            }
            finally
            {
                System.Threading.Monitor.Exit(this.OutputChannelFactory);
            }
            outputChannel.Open();
            try
            {
                outputChannel.Send(msg);
            }
            finally
            {
                outputChannel.Close();
            }
        }
        
        private System.ServiceModel.Channels.Message getReply(System.ServiceModel.Channels.Message msg)
        {
            if ((this.RequestChannelFactory == null))
            {
                // transport doesn't support requests
                throw new System.NotSupportedException();
            }
            System.ServiceModel.Channels.IRequestChannel requestChannel;
            System.Threading.Monitor.Enter(this.RequestChannelFactory);
            try
            {
                requestChannel = this.RequestChannelFactory.CreateChannel(this.remoteAddress);
            }
            finally
            {
                System.Threading.Monitor.Exit(this.RequestChannelFactory);
            }
            requestChannel.Open();
            try
            {
                return requestChannel.Request(msg);
            }
            finally
            {
                if ((requestChannel.State == System.ServiceModel.CommunicationState.Opened))
                {
                    requestChannel.Close();
                }
            }
        }
        
        private void processReply(System.ServiceModel.Channels.Message reply)
        {
            System.Diagnostics.Debug.Assert((reply != null));
            if (reply.IsFault)
            {
                System.Xml.XmlDictionaryReader reader = reply.GetReaderAtBodyContents();
                try
                {
                    throw new CFFaultException(reader.ReadOuterXml());
                }
                finally
                {
                    reader.Close();
                }
            }
        }
        
        protected virtual System.Runtime.Serialization.XmlObjectSerializer GetContractSerializer(CFContractSerializerInfo info)
        {
            System.Runtime.Serialization.XmlObjectSerializer serializer;
            System.Threading.Monitor.Enter(this.serializers);
            try
            {
                if (serializers.ContainsKey(info))
                {
                    serializer = this.serializers[info];
                }
                else
                {
                    serializer = new CFContractSerializer(info);
                    serializers[info] = serializer;
                }
            }
            finally
            {
                System.Threading.Monitor.Exit(this.serializers);
            }
            return serializer;
        }
        
        private TRESPONSE getResult<TRESPONSE>(System.ServiceModel.Channels.Message reply, CFInvokeInfo info)
        
        {
            System.Diagnostics.Debug.Assert((reply != null));
            this.processReply(reply);
            TRESPONSE retVal = default(TRESPONSE);
            if ((reply.IsEmpty == false))
            {
                CFContractSerializerInfo serializerInfo = new CFContractSerializerInfo();
                serializerInfo.MessageContractType = typeof(TRESPONSE);
                serializerInfo.IsWrapped = info.ResponseIsWrapped;
                serializerInfo.IsResponse = true;
                serializerInfo.ExtraTypes = info.ExtraTypes;
                serializerInfo.UseEncoded = info.UseEncoded;
                retVal = reply.GetBody<TRESPONSE>(this.GetContractSerializer(serializerInfo));
            }
            return retVal;
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.NetCFSvcUtil", "3.5.0.0")]
        private class CFContractSerializer : System.Runtime.Serialization.XmlObjectSerializer
        {
            
            private CFContractSerializerInfo info;
            
            private System.Xml.Serialization.XmlSerializer serializer;
            
            private static System.Xml.XmlQualifiedName artificialWrapper = new System.Xml.XmlQualifiedName("wrapper", "");
            
            public CFContractSerializer(CFContractSerializerInfo info)
            {
                this.info = info;
                if ((this.info.ExtraTypes == null))
                {
                    this.info.ExtraTypes = new System.Type[0];
                }
                this.createSerializer(null);
            }
            
            private void createSerializer(System.Xml.XmlQualifiedName wrapper)
            {
                if ((wrapper == null))
                {
                    if (((this.info.IsWrapped == false) 
                                && this.info.IsResponse))
                    {
                        wrapper = artificialWrapper;
                    }
                }
                if (this.info.UseEncoded)
                {
                    System.Xml.Serialization.SoapAttributeOverrides overrides = new System.Xml.Serialization.SoapAttributeOverrides();
                    if ((wrapper != null))
                    {
                        System.Xml.Serialization.SoapAttributes attributes = new System.Xml.Serialization.SoapAttributes();
                        attributes.SoapType = new System.Xml.Serialization.SoapTypeAttribute(wrapper.Name, wrapper.Namespace);
                        overrides.Add(this.info.MessageContractType, attributes);
                    }
                    System.Xml.Serialization.SoapReflectionImporter soapImporter = new System.Xml.Serialization.SoapReflectionImporter(overrides, this.info.DefaultNamespace);
                    for (int i = 0; (i < this.info.ExtraTypes.Length); i = (i + 1))
                    {
                        soapImporter.IncludeType(this.info.ExtraTypes[i]);
                    }
                    System.Xml.Serialization.XmlTypeMapping mapping = soapImporter.ImportTypeMapping(this.info.MessageContractType);
                    this.serializer = new System.Xml.Serialization.XmlSerializer(mapping);
                }
                else
                {
                    System.Xml.Serialization.XmlRootAttribute rootAttr = null;
                    if ((wrapper != null))
                    {
                        rootAttr = new System.Xml.Serialization.XmlRootAttribute();
                        rootAttr.ElementName = wrapper.Name;
                        rootAttr.Namespace = wrapper.Namespace;
                    }
                    this.serializer = new System.Xml.Serialization.XmlSerializer(this.info.MessageContractType, null, this.info.ExtraTypes, rootAttr, this.info.DefaultNamespace);
                }
            }
            
            public override bool IsStartObject(System.Xml.XmlDictionaryReader reader)
            {
                throw new System.NotImplementedException();
            }
            
            public override object ReadObject(System.Xml.XmlDictionaryReader reader, bool verifyObjectName)
            {
                if ((verifyObjectName == false))
                {
                    throw new System.NotSupportedException();
                }
                if (this.info.IsWrapped)
                {
                    // Some WSDLs incorrectly advertise their response message namespaces.
                    // Attempt to interop with these by coercing our expected namespace to match.
                    if ((this.serializer.CanDeserialize(reader) == false))
                    {
                        this.createSerializer(new System.Xml.XmlQualifiedName(System.Xml.XmlConvert.DecodeName(reader.LocalName), reader.NamespaceURI));
                    }
                    return this.serializer.Deserialize(reader);
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                    settings.OmitXmlDeclaration = true;
                    System.Xml.XmlWriter innerWriter = System.Xml.XmlDictionaryWriter.Create(ms, settings);
                    innerWriter.WriteStartElement(artificialWrapper.Name, artificialWrapper.Namespace);
                    string[] commonPrefixes = new string[] {
                            "xsi",
                            "xsd"};
                    for (int i = 0; (i < commonPrefixes.Length); i = (i + 1))
                    {
                        string ns = reader.LookupNamespace(commonPrefixes[i]);
                        if ((ns != null))
                        {
                            innerWriter.WriteAttributeString("xmlns", commonPrefixes[i], null, ns);
                        }
                    }
                    for (
                    ; ((reader.NodeType == System.Xml.XmlNodeType.EndElement) 
                                == false); 
                    )
                    {
                        innerWriter.WriteNode(reader, false);
                    }
                    innerWriter.WriteEndElement();
                    innerWriter.Close();
                    ms.Position = 0;
                    System.Xml.XmlReader innerReader = System.Xml.XmlDictionaryReader.Create(ms);
                    return this.serializer.Deserialize(innerReader);
                }
            }
            
            public override void WriteStartObject(System.Xml.XmlDictionaryWriter writer, object graph)
            {
                throw new System.NotImplementedException();
            }
            
            public override void WriteObjectContent(System.Xml.XmlDictionaryWriter writer, object graph)
            {
                throw new System.NotImplementedException();
            }
            
            public override void WriteEndObject(System.Xml.XmlDictionaryWriter writer)
            {
                throw new System.NotImplementedException();
            }
            
            public override void WriteObject(System.Xml.XmlDictionaryWriter writer, object graph)
            {
                if (this.info.IsWrapped)
                {
                    this.serializer.Serialize(writer, graph);
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                    settings.OmitXmlDeclaration = true;
                    System.Xml.XmlWriter innerWriter = System.Xml.XmlDictionaryWriter.Create(ms, settings);
                    this.serializer.Serialize(innerWriter, graph);
                    innerWriter.Close();
                    ms.Position = 0;
                    System.Xml.XmlReader innerReader = System.Xml.XmlDictionaryReader.Create(ms);
                    innerReader.Read();
                    writer.WriteAttributes(innerReader, false);
                    if ((innerReader.IsEmptyElement == false))
                    {
                        innerReader.Read();
                        for (
                        ; ((innerReader.NodeType == System.Xml.XmlNodeType.EndElement) 
                                    == false); 
                        )
                        {
                            writer.WriteNode(innerReader, false);
                        }
                    }
                    innerReader.Close();
                }
            }
        }
        
        protected struct CFContractSerializerInfo
        {
            
            public System.Type MessageContractType;
            
            public bool IsWrapped;
            
            public bool IsResponse;
            
            public System.Type[] ExtraTypes;
            
            public string DefaultNamespace;
            
            public bool UseEncoded;
        }
        
        protected class CFInvokeInfo
        {
            
            public string Action;
            
            public string ReplyAction;
            
            public bool IsOneWay;
            
            public bool RequestIsWrapped;
            
            public bool ResponseIsWrapped;
            
            public System.Type[] ExtraTypes;
            
            public bool UseEncoded;
            
            public override bool Equals(object obj)
            {
                if (((obj != null) 
                            && (obj.GetType() == typeof(CFInvokeInfo))))
                {
                    return (this.Action == ((CFInvokeInfo)(obj)).Action);
                }
                return false;
            }
            
            public override int GetHashCode()
            {
                if ((this.Action != null))
                {
                    return this.Action.GetHashCode();
                }
                else
                {
                    return base.GetHashCode();
                }
            }
        }
    }
    
    public class CFFaultException : System.ServiceModel.CommunicationException
    {
        
        private string _faultMessage;
        
        public CFFaultException(string faultMessage)
        {
            this._faultMessage = faultMessage;
        }
        
        public string FaultMessage
        {
            get
            {
                return this._faultMessage;
            }
        }
    }
}
