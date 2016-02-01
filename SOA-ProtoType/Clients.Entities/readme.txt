

========== Entities ================
- l'l be used for serialization and deserialization by WCF Services and Proxies, so their namespaces are matching
- l'l all be DataContracts
- Business Entities are decorated with DataContract attribute and properties inside them are decorated with DataMember attribute
- Business Entities act as DTO along the Business side layer from Data-access Layer, Business engine Layer to Service-Layer 
- Each entity maps to a database table
- Data access layer l'l provide ORM mapping if needed
  - DB-Context class l'l use EF fluent-language
- Client Entities l'l be used for binding (xaml & web), validation, UI interaction by client, etc