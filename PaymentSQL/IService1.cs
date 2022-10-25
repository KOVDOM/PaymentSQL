﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PaymentSQL
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getCustomers")]

        List<Customer> GetCustomers();

        //[OperationContract]
        //[WebInvoke(Method = "PUT",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "putCustomer")]

        //string putCustomer(Customer customer);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "postCustomer/{name}/{city}/{age}")]

        string postCustomer(string name, string city, string age);
    }
}
