
CREATE PROCEDURE dbo.AddOrder
    @CUS_FIR_NAME VARCHAR(255) = '',
    @CUS_LAS_NAME VARCHAR(255) = '',
    @STORE_NAME VARCHAR(255) = '',
    @STAF_FIR_NAME VARCHAR(255) = '',
    @STAF_LAS_NAME VARCHAR(255) = '',
    @ORDER_STATUS TINYINT = 0,
    @ORDER_DATE DATE = '',
    @REQUIRED_DATE DATE = '',
    @SHIPPED_DATE DATE = '',
    @PRODUCT_NAME VARCHAR(255) = '',
    @DISCOUNT FLOAT = 0,
    @QUANTITY INT = 0 
AS
BEGIN
   SET NOCOUNT ON;
	--SELECT @CUS_LAS_NAME, @STORE_NAME, @STAF_FIR_NAME, @STAF_LAS_NAME, @ORDER_STATUS, @ORDER_DATE, @REQUIRED_DATE, @SHIPPED_DATE, @PRODUCT_NAME, @DISCOUNT, @QUANTITY
   DECLARE 
        @CUSTOMER_ID INT,
        @STORE_ID INT,
        @STAFF_ID INT,
        @PRODUCT_ID INT,
        @ITEM_ID INT,
        @LIST_PRICE FLOAT,
        @STOCKS INT,
        @ORDER_ID INT;

    SELECT @CUSTOMER_ID = CUSTOMER_ID 
    FROM customers 
    WHERE first_name = @CUS_FIR_NAME AND last_name = @CUS_LAS_NAME;

    SELECT @STORE_ID = STORE_ID 
    FROM stores 
    WHERE store_name = @STORE_NAME;

    SELECT @STAFF_ID = STAFF_ID 
    FROM staffs 
    WHERE first_name = @STAF_FIR_NAME AND last_name = @STAF_LAS_NAME;

    INSERT INTO orders (order_status, order_date, required_date, shipped_date, customer_id, store_id, staff_id)
    VALUES (@ORDER_STATUS, @ORDER_DATE, @REQUIRED_DATE, @SHIPPED_DATE, @CUSTOMER_ID, @STORE_ID, @STAFF_ID);

    SELECT @PRODUCT_ID = product_id, @LIST_PRICE = list_price 
    FROM PRODUCTS
    WHERE product_name = @PRODUCT_NAME;

    SELECT @ITEM_ID = product_id, @STOCKS = quantity 
    FROM stocks 
    WHERE product_id = @PRODUCT_ID AND store_id = @STORE_ID;

    SELECT @ORDER_ID = MAX(order_id) FROM orders;

    IF @QUANTITY <= @STOCKS
    BEGIN
        INSERT INTO order_items (order_id, item_id, product_id, quantity, list_price, discount)
        VALUES (@ORDER_ID, @ITEM_ID, @PRODUCT_ID, @QUANTITY, @LIST_PRICE, @DISCOUNT);

        UPDATE stocks 
        SET quantity = quantity - @QUANTITY 
        WHERE store_id = @STORE_ID AND product_id = @PRODUCT_ID;

        SELECT TOP 1 
            O.order_id, 
            O.customer_id, 
            O.order_status, 
            O.order_date, 
            O.required_date, 
            O.shipped_date, 
            O.store_id, 
            O.staff_id, 
            OT.item_id, 
            OT.product_id, 
            OT.quantity, 
            OT.list_price, 
            OT.discount  
        FROM orders O 
        JOIN order_items OT ON O.order_id = OT.order_id 
        ORDER BY O.order_id DESC;
    END
    ELSE
    BEGIN
        THROW 50001, 'Insufficient Stock', 1;
    END
END