# Net Core CQRS & Event Sourcing

## CQRS

CQRS separates reads and writes into different models, using __commands__ to update data, and __queries__ to read data.

## This repository

This repository implement CQRS with separate read and write models and uses event sourcing.

## Options for CQRS

_ | Not separated read/write models | Logically separate read/write models | Physically separate read/write models
---|---|---|---
No event souring | https://bitbucket.org/publicwichary/net-core-cqrs | This example is not done, however it would store all data to the same database, read data would be denormalised and stored as materialised views | This example is not done, however it would be the same as example on left (also not done) with some messaging via message broker. 
Event souring | It makes no sense, because client does not expect to read stored events instead it is expecting models | It makes very litte sense, however it would be possible for cost saving to store events and read models to the same database  | https://bitbucket.org/publicwichary/net-core-cqrs-event-sourcing

## Sources

- https://github.com/gregoryyoung/m-r
- https://martinfowler.com/eaaDev/EventSourcing.html
- https://microservices.io/patterns/data/event-sourcing.html
- https://medium.com/@hugo.oliveira.rocha/what-they-dont-tell-you-about-event-sourcing-6afc23c69e9a
- https://bulldogjob.pl/articles/122-cqrs-i-event-sourcing-czyli-latwa-droga-do-skalowalnosci-naszych-systemow_
- https://hackernoon.com/1-year-of-event-sourcing-and-cqrs-fb9033ccd1c6