/*
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
    public class AzureServiceBusReportingServicesClient : ReportingServicesClient
    {
        /// <inheritdoc/>
        protected override IBusControl CreateBus()
        {
            var _BusUri = ServiceBusEnvironment.CreateServiceUri(
                AzureServiceBusScheme, BusUri.Host, servicePath: ""
            );

            //WriteVerbose($"Using Azure Service Bus: {BusUri}");

            var _Bus = MassTransit.Bus.Factory.CreateUsingAzureServiceBus(b =>
            {
                b.Host(BusUri, h =>
                {
                    h.SharedAccessSignature(s =>
                    {
                        s.KeyName         = BusCredential.UserName;
                        s.SharedAccessKey = BusCredential.Password;
                        s.TokenTimeToLive = TimeSpan.FromDays(1);
                        s.TokenScope      = TokenScope.Namespace;
                    });
                });
            });

            return _Bus;
        }
    }
}
