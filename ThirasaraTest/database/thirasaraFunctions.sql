-- possible scenarios for functions
CREATE OR ALTER FUNCTION GetTotalLandUsage()
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @totalLandUsage DECIMAL(10, 2);

    SELECT @totalLandUsage = SUM(size_ha)
    FROM field_data;

    RETURN @totalLandUsage;
END;

SELECT dbo.GetTotalLandUsage() AS TotalLandUsage;