-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[DeleteOrder]
	-- Add the parameters for the stored procedure here
	@ORDER_ID INT = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE 
	@ITEM_ID INT,
	@QUANTITY INT,
	@STORE_ID INT
	
	IF (SELECT COUNT(ORDER_ID) FROM ORDERS WHERE ORDER_ID = @ORDER_ID) > 0
		BEGIN
			SELECT @STORE_ID = store_id FROM ORDERS WHERE ORDER_ID = @ORDER_ID
			SELECT @ITEM_ID = item_id, @QUANTITY = quantity FROM ORDER_ITEMS WHERE ORDER_ID = @ORDER_ID

			UPDATE STOCKS SET quantity = quantity+@QUANTITY WHERE product_id = @ITEM_ID AND store_id = @STORE_ID
			DELETE FROM ORDERS WHERE ORDER_ID = @ORDER_ID
			DELETE FROM ORDER_ITEMS WHERE ORDER_ID = @ORDER_ID

			--SELECT * FROM STOCKS WHERE product_id = 1 AND store_id = 1
			--SELECT * FROM ORDERS order by order_id desc
	
			RETURN 'Order Deleted'
		END
		ELSE
		BEGIN
			RETURN 'Order not Found'
		END
END
GO
