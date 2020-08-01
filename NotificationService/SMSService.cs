using Amazon.CodePipeline.Model;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService
{
    public class SMSService
    {
        private const string KeyID = "AKIAR52JXGF3VVLR2JCC";
        private const string KeyAccess = "h897wlJtAOij8L2HbuVwVhrCXXuvGBvSvU8Udrpy";
        public string PhoneNumber { get; set; }
        public string Topic { get; set; }
        private List<string> TopicList { get; set; }

        private List<string> ListTopics(AmazonSimpleNotificationServiceClient snsClient)
        {
            List<string> list = new List<string>();
            var listTopicRequest = new ListTopicsRequest();
            ListTopicsResponse listTopicsResponse;

            listTopicsResponse = snsClient.ListTopics(listTopicRequest);
            foreach (var item in listTopicsResponse.Topics)
            {
                list.Add(item.TopicArn);
            }
            return list;
        }

        private void DeleteTopic(AmazonSimpleNotificationServiceClient snsClient)
        {
            var listTopicRequest = new ListTopicsRequest();
            ListTopicsResponse listTopicsResponse;
            listTopicsResponse = snsClient.ListTopics(listTopicRequest);
            DeleteTopicRequest deleteTopicRequest = new DeleteTopicRequest();
            DeleteTopicResponse deleteTopicResponse;
            foreach (var item in listTopicsResponse.Topics)
            {
                deleteTopicRequest.TopicArn = item.TopicArn;
                deleteTopicResponse = snsClient.DeleteTopic(deleteTopicRequest);
            }
        }

        private void DeleteTopic(AmazonSimpleNotificationServiceClient snsClient, string topic)
        {
            var listTopicRequest = new ListTopicsRequest();
            ListTopicsResponse listTopicsResponse;
            listTopicsResponse = snsClient.ListTopics(listTopicRequest);
            foreach (var item in listTopicsResponse.Topics)
            {
                if (item.TopicArn.Contains(topic))
                {
                    DeleteTopicRequest deleteTopicRequest = new DeleteTopicRequest(item.TopicArn);
                    DeleteTopicResponse deleteTopicResponse = snsClient.DeleteTopic(deleteTopicRequest);
                }
            }
        }

        private string CreateTopic(AmazonSimpleNotificationServiceClient snsClient, string topic)
        {
            //create a new SNS topic            
            CreateTopicRequest createTopicRequest = new CreateTopicRequest(topic);
            CreateTopicResponse createTopicResponse = snsClient.CreateTopic(createTopicRequest);
            //get request id for CreateTopicRequest from SNS metadata                                                
            return createTopicResponse.TopicArn;
        }

        private void SubscribeTopic(AmazonSimpleNotificationServiceClient snsClient, string topicArn, string protocol, string endpoint)
        {
            SubscribeRequest subscribeRequest = new SubscribeRequest(topicArn, protocol, endpoint);
            SubscribeResponse subscribeResponse = snsClient.Subscribe(subscribeRequest);
        }

        public bool SendSMS(string phoneNumber, string topic, string message)
        {
            var awsCredentials = new BasicAWSCredentials(KeyID, KeyAccess);
            try
            {
                using (AmazonSimpleNotificationServiceClient SnsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, Amazon.RegionEndpoint.USEast1))
                {
                    DeleteTopic(SnsClient);
                    string topicArn = CreateTopic(SnsClient, topic);
                    SubscribeTopic(SnsClient, topicArn, "sms", "9" + phoneNumber);

                    //PublishSNSMessageToTopic(snsClient, topicArn, MessageMetroTextBox.Text);                
                    PublishRequest pubRequest = new PublishRequest();
                    pubRequest.Message = message;
                    pubRequest.TopicArn = topicArn;
                    // add optional MessageAttributes...
                    //   pubRequest.MessageAttributes["AWS.SNS.SMS.SenderID"] = 
                    //     new MessageAttributeValue{ StringValue = "AliKaan", DataType = "String"};
                    PublishResponse pubResponse = SnsClient.Publish(pubRequest);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool SendSMS(List<string> phoneList, string topic, string message)
        {
            var awsCredentials = new BasicAWSCredentials(KeyID, KeyAccess);
            try
            {
                using (AmazonSimpleNotificationServiceClient SnsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, Amazon.RegionEndpoint.USEast1))
                {
                    DeleteTopic(SnsClient);
                    string topicArn = CreateTopic(SnsClient, topic);
                    foreach (string item in phoneList)
                    {
                        SubscribeTopic(SnsClient, topicArn, "sms", "9" + item);   // enter phone number with a city code
                                                                                  // example : "905542985313"
                    }
                    //PublishSNSMessageToTopic(snsClient, topicArn, MessageMetroTextBox.Text);                
                    PublishRequest pubRequest = new PublishRequest();
                    pubRequest.Message = message;
                    pubRequest.TopicArn = topicArn;
                    // add optional MessageAttributes...
                    //   pubRequest.MessageAttributes["AWS.SNS.SMS.SenderID"] = 
                    //     new MessageAttributeValue{ StringValue = "AliKaan", DataType = "String"};
                    PublishResponse pubResponse = SnsClient.Publish(pubRequest);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}