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
using System.Threading;
using System.Threading.Tasks;
using RSMassTransit.Messages;

namespace RSMassTransit.Client
{
    /// <summary>
    ///   Interface implemented by RSMassTransit clients.
    /// </summary>
    public interface IReportingServicesClient : IDisposable
    {
        /// <summary>
        ///   Executes a report asynchronously.
        /// </summary>
        /// <param name="request">An object specifying parameters for report execution.</param>
        /// <param name="timeout">The duration after which the client will cease waiting for a response.</param>
        /// <param name="cancellationToken">A token that can cancel the operation.</param>
        /// <returns>An object containing the result of report execution.</returns>
        Task<IExecuteReportResponse> ExecuteAsync(
            IExecuteReportRequest request,
            TimeSpan?             timeout           = default,
            CancellationToken     cancellationToken = default);
    }
}
