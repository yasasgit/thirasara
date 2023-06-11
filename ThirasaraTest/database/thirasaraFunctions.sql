-- function to get total size of all fields in the database
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

-- function to get total predicted yield per total field size of all crop cycles
-- Selects only the crop cycles with latest environment data
CREATE OR ALTER FUNCTION GetTotalPredictedYield()
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @totalPredictedYield DECIMAL(10, 2);

    SELECT @totalPredictedYield = SUM(predicted_yield_kg_ha*size_ha)
    FROM crop_cycle_data AS ccd 
JOIN (
    SELECT crop_cycle, MAX(update_date) AS LATEST
    FROM environment_data
    GROUP BY crop_cycle
) AS max_env ON ccd.crop_cycle_id = max_env.crop_cycle
JOIN field_data AS fd ON ccd.field = fd.field_id;

    RETURN @totalPredictedYield;
END;

SELECT dbo.GetTotalPredictedYield() AS TotalPredictedYield;