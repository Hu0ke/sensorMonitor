using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace sensorMonitor
{
    class MqttClientInterface
    {
        private MqttClient client = null;
        private static string user = "*******ing/device";
        private static string pwd = "iP**********************************vXI=";
        private string clientid;
        private byte[] qosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE }; // qos=1

        public MqttClientInterface()
        {
            string enpoint = "wz_iot.mqtt.iot.gz.baidubce.com";
            int port = 1884;
            clientid = Guid.NewGuid().ToString(); // 获取一个独一无二的id

            client = new MqttClient(enpoint,
                                                port,
                                                false, // 开启TLS 
                                                null,
                                                //MqttSslProtocols.TLSv1_0, // TLS版本
                                                null,
                                                null
                                               );
            client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
        }
        public bool connect()
        {
            client.Connect(clientid,
                                         user,
                                         pwd,
                                         true, // cleanSession
                                         60); // keepAlivePeriod
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
            client.MqttMsgPublished += client_MqttMsgPublished;
            client.ConnectionClosed += client_ConnectionClosed;
            return client.IsConnected;
        }
        public bool IsConnected()
        {
            return client.IsConnected;
        }
        public int subscribe(string topic)
        {
            string[] topics = new string[] { topic };
            return (client.Subscribe(topics, qosLevels)); // sub 的qos=1
        }
        public int publish(string topic, string msg)
        {
            byte[] pubMsg = Encoding.UTF8.GetBytes(msg);
            return (client.Publish(topic, // topic
                                              pubMsg, // message body
                                              0, // pub的QoS level
                                              false)); // retainid
        }
        public void disconnect()
        {
            client.Disconnect();
        }
        // sub后的操作
        private void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
        }
        // 接受消息后的操作
        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
        }
        // 发布消息后的操作
        private void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
        }
        // 关闭连接后的操作
        private void client_ConnectionClosed(object sender, EventArgs e)
        {
        }
        // 取消sub后的操作
        private void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
        }
    }
}
