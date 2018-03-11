﻿/*
    Copyright (C) 2018 Jeffrey Sharp

    Permission to use, copy, modify, and distribute this software for any
    purpose with or without fee is hereby granted, provided that the above
    copyright notice and this permission notice appear in all copies.

    THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
    WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
    MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
    ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
    WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
    ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
    OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
*/

using System;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Microsoft.ServiceBus;

namespace RSMassTransit.Client.AzureServiceBus
{
    /// <summary>
    ///   Client that invokes actions on a RSMassTransit instance via messages
    ///   in an Azure Service Bus namespace.
    /// </summary>
    public class AzureServiceBusReportingServices : ReportingServices
    {
        /// <summary>
        ///   The scheme component required in message bus URIs.
        /// </summary>
        public const string
            UriScheme = "sb";

        /// <summary>
        ///   Creates a new <see cref="AzureServiceBusReportingServices"/>
        ///   instance with the specified configuration.
        /// </summary>
        /// <param name="configuration">
        ///   The configuration for the client, specifying how to communicate
        ///   with RSMassTransit.
        /// </param>
        public AzureServiceBusReportingServices(ReportingServicesConfiguration configuration)
            : base(configuration) { }

        /// <inheritdoc/>
        protected override IBusControl CreateBus(out Uri queueUri)
        {
            var c = Configuration;

            var uri = ServiceBusEnvironment.CreateServiceUri(
                UriScheme, c.BusUri.Host, servicePath: ""
            );

            var bus = Bus.Factory.CreateUsingAzureServiceBus(b =>
            {
                b.Host(uri, h =>
                {
                    h.SharedAccessSignature(s =>
                    {
                        s.KeyName         = c.BusCredential.UserName;
                        s.SharedAccessKey = c.BusCredential.Password;
                        s.TokenTimeToLive = TimeSpan.FromDays(1);
                        s.TokenScope      = TokenScope.Namespace;
                    });
                });
            });

            queueUri = new Uri(uri, c.BusQueue);
            return bus;
        }
    }
}