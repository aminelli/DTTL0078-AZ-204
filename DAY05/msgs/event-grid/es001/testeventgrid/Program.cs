// See https://aka.ms/new-console-template for more information
using MQTTnet;

using System.Security.Cryptography.X509Certificates;

string hostName = "client1egndttl0078.northeurope-1.ts.eventgrid.azure.net";
int port = 8883;
string clientId = "client1-session1";
string x509_pem = @"D:\Corsi\Library\Code\Cloud\Azure\AZ-204\Courses\DTTL0078-AZ-204\DAY05\msgs\event-grid\es001\testeventgrid\client1-authn-ID.pem";
string x509_key = @"D:\Corsi\Library\Code\Cloud\Azure\AZ-204\Courses\DTTL0078-AZ-204\DAY05\msgs\event-grid\es001\testeventgrid\client1-authn-ID.key";



var certificate = new X509Certificate2(
    X509Certificate2.CreateFromPemFile(x509_pem, x509_key).Export(X509ContentType.Pkcs12)
);

var mqttClient = new MqttClientFactory().CreateMqttClient();

var connAck = await mqttClient.ConnectAsync(
    new MqttClientOptionsBuilder()
    .WithTcpServer(hostName, port)
    .WithClientId(clientId)
    .WithCredentials("client1-authn-ID", "")
    .WithTlsOptions(
        new MqttClientTlsOptionsBuilder()
        .UseTls(true)
        .WithClientCertificates(
            new X509Certificate2Collection() { certificate }
        ).Build()
        )
    .Build()
);


Console.WriteLine($"Client Connected: {mqttClient.IsConnected} ConnAck: {connAck.ResultCode} ");

mqttClient.ApplicationMessageReceivedAsync += async m => await Console.Out.WriteAsync($"Rec. Msg su Topic: {m.ApplicationMessage.Topic} Payload: {m.ApplicationMessage.ConvertPayloadToString()} ");

var suback = await mqttClient.SubscribeAsync("corsotopics/topic1");
suback.Items.ToList().ForEach(x => Console.WriteLine($"Subscribed to Topic: {x.TopicFilter.Topic} Result: {x.ResultCode} "));

ulong count = 0;
while (true)
{
    var puback = await mqttClient.PublishStringAsync("corsotopics/topic1","Hello World " + count);
    count++;
}

