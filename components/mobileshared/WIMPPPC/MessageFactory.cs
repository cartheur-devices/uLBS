/*
    Copyright (c) 2009, FindWhere

    All rights reserved.

    This composite work is copyrighted in its entirety. Permission to use this material may be granted only after we receive a written request from you, and you receive written permission from us.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace WIMP
{
    /// <summary>
    /// Message factory creates various types of messages. Based upon 'factory' design pattern.
    /// </summary>
    public class MessageFactory
    {
        private string _AppID;
        private string _UserID;

        // Message counters
        private int _MessageSequence = 0;
        private int _DumpMessageSequence = 0;

        public void DecreaseMessageSequence()
        {
            _MessageSequence--;
        }

        public MessageFactory(string AppID, string IMSI)
        {
            _AppID = AppID;
            _UserID = IMSI;
        }

        /// <summary>
        /// Responsible for creating classes which implement the Message superclass.
        /// </summary>
        /// <param name="MessageType">Message type to create.</param>
        /// <returns>Message object.</returns>
        public Message CreateMessage(Message.MessageTypes MessageType)
        {
            Message RealMessage = null;

            switch (MessageType)
            {
                case Message.MessageTypes.NAK:
                    RealMessage = new NAKMessage();
                    break;
                case Message.MessageTypes.ACK:
                    RealMessage = new ACKMessage();
                    break;
                case Message.MessageTypes.CON:
                    RealMessage = new CONMessage();
                    break;
                case Message.MessageTypes.PAN:
                    RealMessage = new PANMessage();
                    _DumpMessageSequence++;
                    RealMessage.MessageSequenceNumber = _DumpMessageSequence;
                    break;
                case Message.MessageTypes.TLM:
                    RealMessage = new TLMMessage();
                    _MessageSequence++;
                    RealMessage.MessageSequenceNumber = _MessageSequence;
                    break;
                case Message.MessageTypes.CAM:
                    RealMessage = new CAMMessage();
                    break;
                case Message.MessageTypes.CRQ:
                    RealMessage = new CRQMessage();
                    _MessageSequence++;
                    RealMessage.MessageSequenceNumber = _MessageSequence;
                    break;
                case Message.MessageTypes.DMP:
                    RealMessage = new DMPMessage();
                    _DumpMessageSequence++;
                    RealMessage.MessageSequenceNumber = _DumpMessageSequence;
                    break;
                case Message.MessageTypes.BAT:
                    RealMessage = new BATMessage();
                    _MessageSequence++;
                    RealMessage.MessageSequenceNumber = _MessageSequence;
                    break;
                case Message.MessageTypes.SDN:
                    RealMessage = new SDNMessage();
                    _MessageSequence++;
                    RealMessage.MessageSequenceNumber = _MessageSequence;
                    break;
                case Message.MessageTypes.SPD:
                    RealMessage = new SPDMessage();
                    _MessageSequence++;
                    RealMessage.MessageSequenceNumber = _MessageSequence;
                    break;
                case Message.MessageTypes.LVR:
                    RealMessage = new LVRMessage();
                    _MessageSequence++;
                    RealMessage.MessageSequenceNumber = _MessageSequence;
                    break;
                case Message.MessageTypes.LVA:
                    RealMessage = new LVAMessage();
                    break;
                case Message.MessageTypes.RFC:
                    RealMessage = new RFCMessage();
                    _MessageSequence++;
                    RealMessage.MessageSequenceNumber = _MessageSequence;
                    
                    break;
                default:
                    break;
            }

            RealMessage.AppID = _AppID;
            RealMessage.UserID = _UserID;
            
            return RealMessage;
        }

        /// <summary>
        /// Used for reading ACK/NAK messages from server response.
        /// </summary>
        /// <param name="Message">Server message response string.</param>
        public Message CreateMessage(string ServerResponse)
        {
            string[] MessageData;
            string Checksum = "";
            string CalculatedChecksum = "";
            Crc32 Crc = new Crc32();

            // Find Checksum separator '&'
            int ChecksumPosition = ServerResponse.IndexOf("&");

            // Is checksum available?
            if (ChecksumPosition > 0)
            {
                Checksum = ServerResponse.Substring(ChecksumPosition + 1, ServerResponse.Length - ChecksumPosition - 1);
                
                // Strip checksum, don't longer need it
                ServerResponse = ServerResponse.Substring(0, ChecksumPosition);

                // Calculate the CRC checksum;
                Crc.Reset();
                Crc.Update(Encoding.UTF8.GetBytes(ServerResponse)); // Message minus the checksum
                CalculatedChecksum = String.Format("{0:X8}", Crc.Value);

                if (Checksum != CalculatedChecksum)
                {
                    throw new Exception("Checksums don't match: " + CalculatedChecksum + " / " + Checksum);
                }
            }
            
            MessageData = ServerResponse.Split(';');

            // Create Message based on type
            Message RealMessage;
            if(MessageData[1] == "ACK")
                RealMessage = CreateMessage(Message.MessageTypes.ACK);
            else if (MessageData[1] == "NAK")
                RealMessage = CreateMessage(Message.MessageTypes.NAK);
            else if (MessageData[1] == "CON")
                RealMessage = CreateMessage(Message.MessageTypes.CON);
            else if (MessageData[1] == "LVA")
                RealMessage = CreateMessage(Message.MessageTypes.LVA);
            else
                throw new Exception("Unknown response message");

            // Need to have sequence number
            try
            {
                RealMessage.MessageSequenceNumber = Convert.ToInt32(MessageData[0]);
            }
            catch
            {
                throw new Exception("No sequence number available");
            }

            RealMessage.SetData(MessageData);

            RealMessage.AppID = _AppID;
            RealMessage.UserID = _UserID;

            return RealMessage;
        }
    }
}
