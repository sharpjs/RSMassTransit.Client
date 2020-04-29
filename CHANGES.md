# Changes in RSMassTransit.Client
This file documents all notable changes.

Most lines should begin with one of these words:
*Add*, *Fix*, *Update*, *Change*, *Deprecate*, *Remove*.

## [Unreleased](https://github.com/sharpjs/RSMassTransit.Client/compare/v0.1.0...HEAD)
(none)

## [0.1.0](https://github.com/sharpjs/RSMassTransit.Client/compare/v0.0.1...v0.1.0)
- Change targets to .NET Framework 4.6.1 and .NET Standard 2.0
- Change test targets to .NET Framework 4.8 and .NET Core 3.1
- Update MassTransit to 6.2.5
- Add package `RSMassTransit.Client.AzureServiceBus.Core`, which uses the newer
  [`Microsoft.Azure.ServiceBus`](https://www.nuget.org/packages/Microsoft.Azure.ServiceBus/)
  package internally
- Remove package `RSMassTransit.Client.AzureServiceBus`, as the underlying
  package was removed from MassTransit.

## [0.0.1](https://github.com/sharpjs/RSMassTransit.Client/compare/v0.0.0...v0.0.1)
- Update MassTransit to 5.5.1

## [0.0.0](https://github.com/sharpjs/RSMassTransit.Client/tree/v0.0.0)
Initial release.  Supports report execution only.
