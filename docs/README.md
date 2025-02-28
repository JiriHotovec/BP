# Project Orders

## REST API

Url: *~/api/orders*

### Endpoints

* Get all orders
  * Method: [GET]
  * Route: */*
  * Payload: *none*
* Get order by identifier
  * Method: [GET]
  * Route: */{OrderNumber}*
  * Payload: *none*
* Create order
  * Method: [POST]
  * Route: */*
  * Payload: *Json*
* Delete order by identifier
  * Method: [DELETE]
  * Route: */{OrderNumber}*
  * Payload: *none*

## Invariants

### Monetary information

* Monetary calculations do not round consecutive operations.
* Monetary operation is rounded at the end of calculation in this way:
  * round to two decimal places
  * when decimal being rounded is greater or equal to five it is rounded away from zero
  * when decimal being rounded is less than five it is rounded towards zero
* Monetary information is always tied to a three letter currency code *ISO 4217 (year 2024)*. Codes not defined in this standard are not allowed.
* Monetary information is displayed rounded to two decimal places.
* Monetary information is displayed in this form CURRENCY_CODE VALUE e.g. `USD 126,55`.
* Monetary information uses standard czech rules for displaying decimal numbers (no thousands separator, comma as decimal separator).

### Order Number

* Order number is a string in this format "ORDER_NUMBER".
* ORDER is a static prefix text literal.
* *NUMBER* parameter number is a number from a non-decreasing number series.
* Leading zeroes of Number component are striped if present e.g. `ORDER_00215` is accepted but number is normalized to `ORDER_215`.

### Order

* Order must have at least one item.
