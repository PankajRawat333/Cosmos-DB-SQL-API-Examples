# Cosmos-DB-SQL-API-Examples

## Count documents in collection

```
SELECT value count(c.id) FROM c
```

## Get specific value from collection as a arrary (not key value pair)

```
SELECT top 2 value c.AssetType FROM c
```
## Get documents using IN clause

select * FROM c where c.Device[0].SerialNumber IN ('A1','B1','C1')
