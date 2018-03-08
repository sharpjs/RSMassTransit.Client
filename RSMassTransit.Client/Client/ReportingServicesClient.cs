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
using System.Net;
using System.Threading.Tasks;
using MassTransit;
using RSMassTransit.Messages;

namespace RSMassTransit.Client
{
    /// <summary>
    ///   Base class for RSMassTransit clients.
    /// </summary>
    public abstract class ReportingServicesClient : IReportingServicesClient
    {
        public const string
            DefaultBusQueue       = "reports",
            RabbitMqScheme        = "rabbitmq",
            AzureServiceBusScheme = "sb";

        public const int
            DefaultTimeoutSeconds = 10;

        private bool _isDisposed;

        private IRequestClient<IExecuteReportRequest, IExecuteReportResponse> _executeClient;

        /// <summary>
        ///   Creates a new <see cref="ReportingServicesClient"/> instance.
        /// </summary>
        protected ReportingServicesClient() { }

        public Uri BusUri { get; set; }

        public string BusQueue { get; set; } = DefaultBusQueue;

        public NetworkCredential BusCredential { get; set; }

        public int TimeoutSeconds { get; set; } = DefaultTimeoutSeconds;

        protected IBusControl Bus { get; private set; }

        /// <inheritdoc/>
        public Task<IExecuteReportResponse> ExecuteAsync(IExecuteReportRequest request)
        {
            CreateBusClient(out _executeClient);

            return _executeClient.Request(request);
        }

        protected abstract IBusControl CreateBus();

        private void CreateBusClient<TRequest, TResponse>
            (out IRequestClient<TRequest, TResponse> client)
            where TRequest  : class
            where TResponse : class
        {
            client = Bus.CreateRequestClient<TRequest, TResponse>(
                new Uri(BusUri, BusQueue),
                TimeSpan.FromSeconds(TimeoutSeconds)
            );
        }

        /// <summary>
        ///   Stops the bus instance used by the client and releases any
        ///   managed or unmanaged resources owned by the client.
        /// </summary>
        public void Dispose()
        {
            Dispose(managed: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Invoked by either <see cref="Dispose()"/> or the finalizer thread.
        ///   Stops the bus instance used by the client and releases any
        ///   resources of the specified kind owned by the client.
        /// </summary>
        /// <param name="managed">
        ///   <c>true</c> to dispose managed an unmanaged resources;
        ///   <c>false</c> to dispose only unamanged resources.
        /// </param>
        /// <remarks>
        ///   The current <see cref="ReportingServicesClient"/> implementation
        ///   does not expect to own unmanaged resources and thus does not
        ///   provide a finalizer.  Thus the <paramref name="managed"/>
        ///   parameter always will be <c>true</c>.
        /// </remarks>
        protected virtual void Dispose(bool managed)
        {
            if (_isDisposed)
                return;

            if (managed)
                DisposeBus();

            _isDisposed = true;
        }

        private void DisposeBus()
        {
            Bus?.Stop();
            Bus = null;
        }
    }
}
