﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace PryCertificaSIN.Utiles
{
    public class CustomAuthenticationBehaviour : IEndpointBehavior
    {
        readonly string _authToken;

        public CustomAuthenticationBehaviour(string authToken)
        {
            _authToken = authToken;
        }
        public void Validate(ServiceEndpoint endpoint)
        { }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new CustomMessageInspector(_authToken));
        }
    }

    public class CustomMessageInspector : IClientMessageInspector
    {
        readonly string _authToken;

        public CustomMessageInspector(string authToken)
        {
            _authToken = authToken;


        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var reqMsgProperty = new HttpRequestMessageProperty();
            reqMsgProperty.Headers.Add("Authorization", _authToken);
            request.Properties[HttpRequestMessageProperty.Name] = reqMsgProperty;
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        { }

    }
}
