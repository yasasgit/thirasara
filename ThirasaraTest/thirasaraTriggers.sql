-- possible scenarios for triggers

CREATE TRIGGER trg_environment_data_range_validation
BEFORE INSERT OR UPDATE ON environment_data
FOR EACH ROW
BEGIN
    IF NEW.temperature_c < -50 OR NEW.temperature_c > 50 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid temperature range';
    END IF;

    IF NEW.rainfall_irrigation_mm < 0 OR NEW.rainfall_irrigation_mm > 1000 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid rainfall/irrigation range';
    END IF;

    IF NEW.humidity_perc < 0 OR NEW.humidity_perc > 100 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid humidity range';
    END IF;

    IF NEW.wind_speed_m_s < 0 OR NEW.wind_speed_m_s > 30 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid wind speed range';
    END IF;

    IF NEW.sunlight_exposure_h_day < 0 OR NEW.sunlight_exposure_h_day > 24 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid sunlight exposure range';
    END IF;
END;

CREATE TRIGGER trg_crop_cycle_data_range_validation
BEFORE INSERT OR UPDATE ON crop_cycle_data
FOR EACH ROW
BEGIN
    IF NEW.plant_density_ha < 0 OR NEW.plant_density_ha > 100000 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid plant density range';
    END IF;

    IF NEW.human_hours_ha < 0 OR NEW.human_hours_ha > 1000 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid human hours range';
    END IF;

    IF NEW.predicted_yield_kg_ha < 0 OR NEW.predicted_yield_kg_ha > 100000 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid predicted yield range';
    END IF;

    IF NEW.yield_kg_ha < 0 OR NEW.yield_kg_ha > 100000 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid yield range';
    END IF;
END;

-- update - calculate average + prediction
CREATE TRIGGER update_values_trigger
BEFORE INSERT ON your_table
FOR EACH ROW
BEGIN
  SET NEW.column_name = (SELECT column_name FROM your_table WHERE primary_key = NEW.primary_key) + NEW.column_name;
END;


CREATE OR ALTER TRIGGER update_crop_cycle_predictions
ON crop_cycle_data
AFTER UPDATE
AS
BEGIN
  IF NOT UPDATE(predicted_yield_kg_ha)
  BEGIN
  DECLARE @crop_cycle_id INT;

  SELECT @crop_cycle_id = crop_cycle_id
  FROM inserted;

  EXEC sp_execute_external_script
    @language = N'R',
    @script = N'
    ',
    @input_data_1 = N'crop_cycle_id INT',
    @params = N'@crop_cycle_id INT',
    @crop_cycle_id = @crop_cycle_id;
END;
END;


CREATE OR ALTER TRIGGER update_crop_cycle_predictions
ON crop_cycle_data
AFTER UPDATE
AS
BEGIN
  IF NOT UPDATE(predicted_yield_kg_ha)
  BEGIN
    DECLARE @crop_cycle_id INT,
			@crop_id INT,
			@predictedValue FLOAT;
	DECLARE @train_df TABLE (
			  crop_cycle_id INT,
			  plant_density_ha FLOAT,
			  soil_ph FLOAT,
			  soil_texture_level VARCHAR(50),
			  nitrogen_kg_ha FLOAT,
			  phosphorus_kg_ha FLOAT,
			  potassium_kg_ha FLOAT,
			  other_nutrients_kg_ha FLOAT,
			  fertilizer_kg_ha FLOAT,
			  severity_level VARCHAR(50),
			  temperature_c FLOAT,
			  rainfall_irrigation_mm FLOAT,
			  humidity_perc FLOAT,
			  wind_speed_m_s FLOAT,
			  sunlight_exposure_h_day FLOAT,
			  human_hours_ha FLOAT,
			  yield_kg_ha FLOAT);
	DECLARE @test_df TABLE (
			  crop_cycle_id INT,
			  plant_density_ha FLOAT,
			  soil_ph FLOAT,
			  soil_texture_level VARCHAR(50),
			  nitrogen_kg_ha FLOAT,
			  phosphorus_kg_ha FLOAT,
			  potassium_kg_ha FLOAT,
			  other_nutrients_kg_ha FLOAT,
			  fertilizer_kg_ha FLOAT,
			  severity_level VARCHAR(50),
			  temperature_c FLOAT,
			  rainfall_irrigation_mm FLOAT,
			  humidity_perc FLOAT,
			  wind_speed_m_s FLOAT,
			  sunlight_exposure_h_day FLOAT,
			  human_hours_ha FLOAT,
			  yield_kg_ha FLOAT);

    SELECT @crop_cycle_id = crop_cycle_id
    FROM inserted;
	SELECT @crop_id = crop
    FROM crop_cycle_data
    WHERE crop_cycle_id = @crop_cycle_id;

    INSERT INTO @train_df
    SELECT cc.crop_cycle_id,
              cc.plant_density_ha,
              fd.soil_ph,
              std.soil_texture_level,
              (fd.soil_nitrogen_kg_ha + fert.fertilizer_nitrogen_kg_ha) AS nitrogen_kg_ha,
              (fd.soil_phosphorus_kg_ha + fert.fertilizer_phosphorus_kg_ha) AS phosphorus_kg_ha,
              (fd.soil_potassium_kg_ha + fert.fertilizer_potassium_kg_ha) AS potassium_kg_ha,
              fert.other_nutrients_kg_ha,
              fd.fertilizer_kg_ha,
              pd.severity_level,
              ed.temperature_c,
              ed.rainfall_irrigation_mm,
              ed.humidity_perc,
              ed.wind_speed_m_s,
              ed.sunlight_exposure_h_day,
              cc.human_hours_ha,
              cc.yield_kg_ha
    FROM 
		crop_cycle_data AS cc
		JOIN environment_data AS ed ON cc.environment = ed.environment_data_id
		JOIN field_data AS fd ON ed.field = fd.field_id
		JOIN fertilizer_data AS fert ON fd.fertilizer = fert.fertilizer_id
		JOIN pest_disease_data AS pd ON cc.pest_disease = pd.pest_disease_id
		JOIN soil_texture_data AS std ON fd.soil_texture = std.soil_texture_id
    WHERE 
		cc.yield_kg_ha IS NOT NULL
    AND 
		cc.crop = @crop_id;

    INSERT INTO @test_df
    SELECT cc.crop_cycle_id,
            cc.plant_density_ha,
            fd.soil_ph,
            std.soil_texture_level,
            (fd.soil_nitrogen_kg_ha + fert.fertilizer_nitrogen_kg_ha) AS nitrogen_kg_ha,
            (fd.soil_phosphorus_kg_ha + fert.fertilizer_phosphorus_kg_ha) AS phosphorus_kg_ha,
            (fd.soil_potassium_kg_ha + fert.fertilizer_potassium_kg_ha) AS potassium_kg_ha,
            fert.other_nutrients_kg_ha,
            fd.fertilizer_kg_ha,
            pd.severity_level,
            ed.temperature_c,
            ed.rainfall_irrigation_mm,
            ed.humidity_perc,
            ed.wind_speed_m_s,
            ed.sunlight_exposure_h_day,
            cc.human_hours_ha,
            cc.yield_kg_ha
    FROM 
		crop_cycle_data AS cc
		JOIN environment_data AS ed ON cc.environment = ed.environment_data_id
		JOIN field_data AS fd ON ed.field = fd.field_id
		JOIN fertilizer_data AS fert ON fd.fertilizer = fert.fertilizer_id
		JOIN pest_disease_data AS pd ON cc.pest_disease = pd.pest_disease_id
		JOIN soil_texture_data AS std ON fd.soil_texture = std.soil_texture_id
    WHERE 
		crop_cycle_id = @crop_cycle_id;

    EXEC sp_execute_external_script
      @language = N'R',
      @script = N'
        library(caret)
        set.seed(123)
        model <- train(yield_kg_ha ~ plant_density_ha + soil_ph + soil_texture_level + nitrogen_kg_ha + phosphorus_kg_ha + potassium_kg_ha + other_nutrients_kg_ha + fertilizer_kg_ha + severity_level + temperature_c + rainfall_irrigation_mm + humidity_perc + wind_speed_m_s + sunlight_exposure_h_day + human_hours_ha, data = train_df, method = "lm")
        predictedValue <- predict(model, newdata = test_df)
      ',
      @input_data_1 = N'@train_df AS "train_df", @test_df AS "test_df"',
      @params = N'@crop_cycle_id INT',
      @output_data_1 = N'@predictedValue AS "predictedValue"',
	  @crop_cycle_id = @crop_cycle_id;

	  UPDATE crop_cycle_data 
	  SET predicted_yield_kg_ha = @predictedValue 
	  WHERE crop_cycle_id = @crop_cycle_id;
  END;
END;
