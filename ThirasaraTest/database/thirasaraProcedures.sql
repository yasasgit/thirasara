-- load field data table for farmer using nic
CREATE OR ALTER PROCEDURE GetFieldData
    @nic VARCHAR(10)
AS
BEGIN
    SELECT 
    field_id ,
    size_ha,
    field_location,
    soil_nitrogen_kg_ha,
    soil_phosphorus_kg_ha,
    soil_potassium_kg_ha,
	soil_ph,
    soil_texture
FROM field_data
    WHERE cultivator = @nic;
END;
-- for farmer
EXEC GetFieldData @nic = '449683857v';

-- load crop cycle data table for farmer using field id
CREATE OR ALTER PROCEDURE GetCropCycleData
    @fieldId VARCHAR(10)
AS
BEGIN
    SELECT 
    crop_cycle_id,
    plant_density_ha,
    human_hours_ha,
    predicted_yield_kg_ha,
    yield_kg_ha,
    planted_date,
    harvest_date
 FROM crop_cycle_data
    WHERE field = @fieldId;
END;
-- for farmer
EXEC GetCropCycleData @fieldId = '449683857v';

-- load environment data table for farmer using crop cycle id
CREATE OR ALTER PROCEDURE GetEnvironmentData
    @cropCycleId VARCHAR(10)
AS
BEGIN
    SELECT 
    temperature_c,
    rainfall_irrigation_mm,
    humidity_perc,
    wind_speed_m_s,
    sunlight_exposure_h_day,
	update_date
FROM environment_data
    WHERE crop_cycle = @cropCycleId
END;
-- for farmer
EXEC GetEnvironmentData @cropCycleId = '449683857v';


-- load environment data table for farmer using crop cycle id
CREATE OR ALTER PROCEDURE RequiredFertilizer
    @cropCycleId smallint
AS
BEGIN
    SELECT 
    (suitable_nitrogen_kg_ha - soil_nitrogen_kg_ha) AS required_nitrogen_kg_ha,
    (suitable_phosphorus_kg_ha - soil_phosphorus_kg_ha) AS required_phosphorus_kg_ha,
    (suitable_potassium_kg_ha - soil_potassium_kg_ha) AS required_potassium_kg_ha,
    other_nutrients_kg_ha,
    fertilizer_kg_ha
FROM crop_cycle_data AS ccd 
JOIN field_data AS fd ON ccd.field = fd.field_id
JOIN crop_data AS cd ON ccd.crop = cd.crop_id 
JOIN fertilizer_data AS fert ON ccd.fertilizer = fert.fertilizer_id 
    WHERE crop_cycle_id = @cropCycleId;
END;
-- for farmer
EXEC RequiredFertilizer @cropCycleId = 1000;

-- Resets Predicted Values
CREATE PROCEDURE ResetPredictedYield
AS
BEGIN
    UPDATE crop_cycle_data
    SET predicted_yield_kg_ha = 0;
END;


-- NOT USED
-- Inserts Random Data for Prototype
CREATE OR ALTER PROCEDURE InsertTainData
AS
BEGIN
	-- disable the message that shows the number of rows affected 
	SET NOCOUNT ON;

    DECLARE @counter INT = 0, @user_id INT = 10, @crop_id INT = 2, @field_id INT = 50, @environment_id INT = 100, @crop_cycle_rows INT = 200,

			@land_owner INT, @size_ha DECIMAL(10, 2), @field_location VARCHAR(255), @fertilizer INT, @soil_nitrogen_kg_ha DECIMAL(10, 2), @soil_phosphorus_kg_ha DECIMAL(10, 2), @soil_potassium_kg_ha DECIMAL(10, 2), @fertilizer_kg_ha DECIMAL(10, 2), @soil_ph DECIMAL(10, 2), @soil_texture INT,

			@field INT, @temperature_c DECIMAL(10, 2), @rainfall_irrigation_mm DECIMAL(10, 2), @humidity_perc INT, @wind_speed_m_s DECIMAL(10, 2), @sunlight_exposure_h_day DECIMAL(10, 2), @update_date DATE,
			
			@cultivator INT, @crop INT, @environment INT, @planted_date DATE, @plant_density_ha INT,  @pest_disease INT, @human_hours_ha DECIMAL(10, 2), @harvest_date DATE , @yield_kg_ha INT;

    WHILE @counter < @field_id
    BEGIN
        SET @land_owner = CAST((1000 + ROUND(RAND() * (@user_id - 1), 0)) AS INT);
		SET @size_ha = ROUND(RAND() * 100, 2);
        SET @field_location = (CASE ROUND(RAND() * 30, 0)
                WHEN 0 THEN 'Arachchigoda'
                WHEN 1 THEN 'Artigala'
                WHEN 2 THEN 'Dampe'
                WHEN 3 THEN 'Diyagama'
                WHEN 4 THEN 'Habarakada'
                WHEN 5 THEN 'Homagama'
                WHEN 6 THEN 'Horagala East'
                WHEN 7 THEN 'Horakandawela West'
                WHEN 8 THEN 'Jaltara'
                WHEN 9 THEN 'Kahathuduwa North'
                WHEN 10 THEN 'Kahathuduwa South'
                WHEN 11 THEN 'Kiriberiyakele'
                WHEN 12 THEN 'Kiriwattuduwa'
                WHEN 13 THEN 'Kurugala'
                WHEN 14 THEN 'Liyanwala'
                WHEN 15 THEN 'Madulawa'
                WHEN 16 THEN 'Magammana'
                WHEN 17 THEN 'Mattegoda'
                WHEN 18 THEN 'Muthuhenawatta'
                WHEN 19 THEN 'Niyandagala'
                WHEN 20 THEN 'Ovitigama'
                WHEN 21 THEN 'Palagama'
                WHEN 22 THEN 'Panagoga East'
                WHEN 23 THEN 'Panaluwa'
                WHEN 24 THEN 'Pitipana'
                WHEN 25 THEN 'Pitipana North'
                WHEN 26 THEN 'Pitipana South'
                WHEN 27 THEN 'Watereka'
                WHEN 28 THEN 'Walpita'
                WHEN 29 THEN 'Wethara'
                ELSE 'Godagama'
            END
        );
        SET @fertilizer = CAST((1000 + ROUND(RAND() * 3, 0)) AS INT);
        SET @soil_nitrogen_kg_ha = ROUND(RAND() * 1000, 2);
        SET @soil_phosphorus_kg_ha = ROUND(RAND() * 1000, 2);
        SET @soil_potassium_kg_ha = ROUND(RAND() * 1000, 2);
		SET @fertilizer_kg_ha = RAND() * 200;
		SET @soil_ph = ROUND(RAND() * 14, 2) + 1;
        SET @soil_texture = CAST((1000 + ROUND(RAND() * 11, 0)) AS INT);
        

        INSERT INTO field_data (
            land_owner, field_location, size_ha, soil_ph, soil_nitrogen_kg_ha, soil_phosphorus_kg_ha, soil_potassium_kg_ha, fertilizer,  fertilizer_kg_ha,  soil_texture
        )
        VALUES (
            @land_owner, @field_location, @size_ha, @soil_ph, @soil_nitrogen_kg_ha, @soil_phosphorus_kg_ha, @soil_potassium_kg_ha, @fertilizer,  @fertilizer_kg_ha,  @soil_texture
        );

        SET @counter = @counter + 1;
    END;

	SET @counter = 0;
    WHILE @counter < @environment_id
    BEGIN
		SET @field = CAST((1000 + ROUND(RAND() * (@field_id - 1), 0)) AS INT);
		SET @temperature_c = ROUND((25 + RAND() * 15), 2); -- Random value between 15 and 40
        SET @rainfall_irrigation_mm = ROUND(RAND() * 500, 2); -- Random value between 0 and 500
        SET @humidity_perc = ROUND((30 + RAND() * 50), 0); -- Random value between 30 and 80
        SET @wind_speed_m_s = ROUND(RAND() * 10, 2); -- Random value between 0 and 10
        SET @sunlight_exposure_h_day = ROUND(RAND() * 2000, 2); -- Random value between 0 and 2000
        SET @update_date = DATEADD(DAY, CAST(RAND() * 365 AS INT), '2023-05-20');
            
        INSERT INTO environment_data (
			field, temperature_c, rainfall_irrigation_mm, humidity_perc, wind_speed_m_s, sunlight_exposure_h_day, update_date
        )
        VALUES (
			@field, @temperature_c, @rainfall_irrigation_mm, @humidity_perc, @wind_speed_m_s, @sunlight_exposure_h_day, @update_date
        );
        
        SET @counter = @counter + 1;
    END;

	SET @counter = 0;
	WHILE @counter < @crop_cycle_id
    BEGIN
        SET @cultivator = CAST((1000 + ROUND(RAND() * (@user_id - 1), 0)) AS INT);
        SET @crop = CAST((1000 + ROUND(RAND() * (@crop_id - 1), 0)) AS INT);
        SET @plant_density_ha = CAST((100 + RAND() * (120000 - 100 + 1)) AS INT);
        SET @environment = CAST((1000 + ROUND(RAND() * (@environment_id - 1), 0)) AS INT);
        SET @pest_disease = CAST((1000 + ROUND(RAND() * 4, 0)) AS INT);
        SET @human_hours_ha = RAND() * 553 + 7;
        SET @yield_kg_ha = CAST((500 + RAND() * (8000 - 500 + 1)) AS INT);
        SET @planted_date = DATEADD(DAY, CAST(RAND() * 365 AS INT), '2023-05-20');
        SET @harvest_date = DATEADD(DAY, CAST(RAND() * 365 AS INT), @planted_date);

        INSERT INTO crop_cycle_data (
			cultivator, crop,  plant_density_ha,  environment,  pest_disease,  human_hours_ha,  yield_kg_ha,  planted_date,  harvest_date
		)
        VALUES (
			@cultivator,  @crop, @plant_density_ha,  @environment,  @pest_disease,  @human_hours_ha,  @yield_kg_ha,  @planted_date,  @harvest_date
		);

        SET @counter = @counter + 1;
    END;

END;

EXEC InsertTrainData;