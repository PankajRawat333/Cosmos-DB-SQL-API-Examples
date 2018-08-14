# Cosmos-DB-SQL-API-Examples

## Count document in collection

```
SELECT value count(c.id) FROM c
```

## Get specific value from collection as a arrary (not key value pair)

```
SELECT top 2 value c.AssetType FROM c
```
