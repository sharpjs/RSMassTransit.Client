# RSMassTransit.Client

[RSMassTransit](https://github.com/sharpjs/RSMassTransit) enables [Microsoft SQL Server Reporting Services](https://docs.microsoft.com/en-us/sql/reporting-services/create-deploy-and-manage-mobile-and-paginated-reports) to work with [MassTransit](http://masstransit-project.com/) message buses.

This project, **RSMassTransit.Client**, contains everything needed to send requests from an application to RSMassTransit:

* Request and response message types
* Service proxy with implementations for RabbitMQ and Azure Service Bus

Currently, RSMassTransit supports ad-hoc report execution only.  Future support for additional operations, such as catalog management, is planned.

## Status

Private beta testing.  Currently, only report execution is implemented.
