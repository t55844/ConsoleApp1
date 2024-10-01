-- =============================================
-- Author:      Thiago
-- Create date: 01/08/2024
-- Description: Simple query to get product names by model year
-- =============================================
CREATE PROCEDURE [dbo].[ProductiesNamesByModelYear] 
    -- Add the parameters for the stored procedure here
    @MODELYEAR smallint = 0, 
    @BYONEYEAR varchar(1) = 'N'
AS
BEGIN
	--SELECT solto fora do ponto onde devera ter resultado atrapalha o retorno de valor

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Check if the filter is 'S'
    IF @BYONEYEAR = 'S'
    BEGIN
        -- Return product names for the given model year
        SELECT product_name 
        FROM production.products
        WHERE model_year = @MODELYEAR;
    END
    ELSE
    BEGIN
        -- Return a message indicating no products to display
        SELECT 'No products to display for the given parameters' AS Message;
    END
END