using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SocketIOClient;
using Newtonsoft.Json.Linq;
using SocketIOClient.Messages;


namespace SocketIO4Net.ClientTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.FormClosed += Form1_FormClosed;
            System.Net.WebRequest.DefaultWebProxy = null;
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseSocket();
        }

        Client socket;
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Starting TestSocketIOClient Example...");

            //socket = new Client("http://192.168.207.86/"); // url to nodejs 
            socket = new Client("http://localhost:808/"); // url to nodejs 
            socket.Opened += SocketOpened;
            socket.Message += SocketMessage;
            socket.SocketConnectionClosed += SocketConnectionClosed;
            socket.Error += SocketError;

            // register for 'get_kinect' event with io server
            socket.On("get_kinect", (fn) =>
            {
                Console.WriteLine("\r\nget_kinect event...\r\n");
                Console.WriteLine("Emit KinectData object");

                // emit Json Serializable object, anonymous types, or strings
                KinectData newData = new KinectData() { Foo = "foo", Bar = "bar" };
                socket.Emit("kinect_data", newData);
            });

            // make the socket.io connection
            try
            {
                socket.Connect();
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp.Message);
            }
        }


        //public void CallbackExample()
        //{
        //    Console.WriteLine("Emit [socket].[messageAck] - should recv callback [root].[messageAck]");
        //    socket.Emit("messageAck", new { hello = "mama" }, null,
        //        (callback) =>
        //        {
        //            var jsonMsg = callback as JsonEncodedEventMessage; // callback will be of type JsonEncodedEventMessage, cast for intellisense
        //            Console.WriteLine(string.Format("callback [root].[messageAck]: {0} \r\n", jsonMsg.Args));
        //        });
        //}

        void SocketError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("socket client error:");
            Console.WriteLine(e.Message);
        }

        void SocketConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("WebSocketConnection was terminated!");
        }

        void SocketMessage(object sender, MessageEventArgs e)
        {
            // uncomment to show any non-registered messages
            //if (string.IsNullOrEmpty(e.Message.Event))
            //    Console.WriteLine("Generic SocketMessage: {0}", e.Message.MessageText);
            //else
            //    Console.WriteLine("Generic SocketMessage: {0} : {1}", e.Message.Event, e.Message.JsonEncodedMessage.ToJsonString());
        }

        void SocketOpened(object sender, EventArgs e)
        {

        }

        public void CloseSocket()
        {
            if (this.socket != null)
            {
                socket.Opened -= SocketOpened;
                socket.Message -= SocketMessage;
                socket.SocketConnectionClosed -= SocketConnectionClosed;
                socket.Error -= SocketError;
                this.socket.Dispose(); // close & dispose of socket client
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KinectData newData = new KinectData() { Foo = "foo", Bar = "bar" };
            socket.Emit("kinect_data", newData);
        }


    }
}
